using NAudio.Wave;
using screen_recorder.AudioCapture;
using screen_recorder.Models;
using ScreenRecorderLib;
using System.Diagnostics;
using Transitions;
using ScreenRecorder = ScreenRecorderLib.Recorder;

namespace screen_recorder
{
    public partial class Form1 : Form
    {
        private ThreadedProcessAudioRecorder? capAudioRecorder = null;

        private ScreenRecorder? capMainRecorder = null;

        private ProcessInfo[] availableAppsToCapture = Array.Empty<ProcessInfo>();

        private string saveDirectory = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void RefreshApps()
        {
            availableAppsToCapture = Process.GetProcesses()
                .Where(proc => !string.IsNullOrEmpty(proc.MainWindowTitle) && proc.Id != Environment.ProcessId)
                .Select(proc => new ProcessInfo(proc.Id, proc.ProcessName, proc.MainWindowTitle, proc.MainModule?.FileName))
                .ToArray();
            appChooser.Items.Clear();
            appChooser.Items.AddRange(availableAppsToCapture);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MinimumSize = MaximumSize = Size;

            saveDirectory = Properties.Settings.Default.saveDir;

            if (saveDirectory != string.Empty)
            {
                saveDirectoryDisplay.Text = saveDirectory;
            }

            identifierTextBox.Text = Properties.Settings.Default.identifier;

            ControlDecoration.MakeControlRounded(refreshAppsBtn, 1, 1, 2, 0);
            identifierHelpBtn.FlatAppearance.BorderSize = 0;
            ControlDecoration.MakeControlRounded(identifierHelpBtn, 1, 1, 3, 2, 20);
            refreshAppsBtnTooltip.SetToolTip(refreshAppsBtn, "Odœwie¿ listê aplikacji");

            RefreshApps();
        }

        private void changeRecordingLocationBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = recordingsDirectoryChooser.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(recordingsDirectoryChooser.SelectedPath))
            {
                saveDirectory = Properties.Settings.Default.saveDir = saveDirectoryDisplay.Text = recordingsDirectoryChooser.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            var self = (Button)sender;

            if (capAudioRecorder is null)
            {
                // Save possibly modified identifier.
                // Saving settings immediately in identifierTextBox_TextChanged causes
                // massive memory usage which would need to be cleaned using GC.Collect().
                // I decided not to do this since:
                // 1. Garbage collection is heavy on resources
                // 2. Garbage collection on demand causes a little bit of lag and could make the text box unresponsive
                // 3. Garbage collection on demand should be a last resort
                Properties.Settings.Default.Save();

                void FailedToStart(string reason)
                {
                    self.Text = "Rozpocznij nagrywanie";
                    MessageBox.Show(reason, "Nie uda³o siê rozpocz¹æ nagrania.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                self.Text = "Rozpoczynanie...";

                if (saveDirectory == string.Empty)
                {
                    FailedToStart("Nie wybrano œcie¿ki zapisywania nagrañ.");
                    return;
                }

                var identifier = identifierTextBox.Text;

                if (identifier == string.Empty)
                {
                    FailedToStart("Nie podano identyfikatora.");
                    return;
                }

                var selectedProcessToCapture = (ProcessInfo)appChooser.SelectedItem;

                if (selectedProcessToCapture is null)
                {
                    FailedToStart("Nie wybrano procesu do przechwytywania.");
                    return;
                }

                Process processToCapture;

                try
                {
                    processToCapture = Process.GetProcessById(selectedProcessToCapture.Id);
                }
                catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
                {
                    FailedToStart("Wybrany proces do przechwytywania ju¿ nie istnieje lub zmieni³ swój identyfikator.");
                    RefreshApps();
                    return;
                }

                var pathProvider = new RecordingFilePathProvider(saveDirectory, processToCapture.ProcessName, identifier);

                capAudioRecorder = new ThreadedProcessAudioRecorder(processToCapture, pathProvider.CapAudioFilePath);
                capMainRecorder = ScreenRecorder.CreateRecorder(
                    new()
                    {
                        SourceOptions = new()
                        {
                            RecordingSources = new()
                            {
                                recordWholeScreen.Checked ?
                                    new DisplayRecordingSource(DisplayRecordingSource.MainMonitor)
                                    :
                                    new WindowRecordingSource(processToCapture.MainWindowHandle)
                            }
                        },
                        VideoEncoderOptions = new()
                        {
                            Bitrate = 8000 * 1000,
                            Framerate = 60,
                            IsFixedFramerate = true,
                            IsFragmentedMp4Enabled = true,
                            IsLowLatencyEnabled = false
                        },
                        AudioOptions = new()
                        {
                            IsAudioEnabled = true,
                            IsOutputDeviceEnabled = false,
                            IsInputDeviceEnabled = true,
                            //AudioInputDevice = ScreenRecorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices).FirstOrDefault()?.DeviceName
                        },
                        MouseOptions = new()
                        {
                            IsMouseClicksDetected = false,
                            IsMousePointerEnabled = true
                        }
                    }
                );
                capMainRecorder.OnRecordingFailed += (sender, e) =>
                {
                    EndRecording();
                    MessageBox.Show(e.Error, "Wyst¹pi³ b³¹d podczas nagrywania");
                };

                capAudioRecorder.Start();
                capMainRecorder.Record(pathProvider.CapMainFilePath);

                self.Text = "Zatrzymaj nagrywanie";
                recordingIcon.Visible = true;

                var transition = new Transition(new TransitionType_Deceleration(350));
                transition.add(timerDisplay, "Left", 52);
                transition.add(optionsBlocker, "Top", 96);
                transition.run();
            }
            else
            {
                EndRecording();
            }
        }

        private void EndRecording()
        {
            var self = startRecordingButton;

            self.Text = "Koñczenie...";

            capAudioRecorder?.Stop();
            capAudioRecorder = null;

            capMainRecorder?.Stop();
            capMainRecorder?.Dispose();
            capMainRecorder = null;

            self.Text = "Rozpocznij nagrywanie";
            recordingIcon.Visible = false;
            var transition = new Transition(new TransitionType_Acceleration(350));
            transition.add(timerDisplay, "Left", 16);
            transition.add(optionsBlocker, "Top", 263);
            transition.run();
        }

        private void refreshAppsBtn_Click(object sender, EventArgs e)
        {
            RefreshApps();
        }

        private void refreshAppsBtn_MouseEnter(object sender, EventArgs e)
        {
            Transition.run(sender, "BackColor", Color.FromArgb(100, Color.DarkGray), new TransitionType_Linear(125));
        }

        private void refreshAppsBtn_MouseLeave(object sender, EventArgs e)
        {
            Transition.run(sender, "BackColor", Color.FromArgb(0, Color.DarkGray), new TransitionType_Linear(125));
        }

        private void refreshAppsBtn_MouseDown(object sender, MouseEventArgs e)
        {
            ((Label)sender).BackColor = Color.FromArgb(130, Color.DarkGray);
        }

        private void refreshAppsBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ((Label)sender).BackColor = Color.FromArgb(100, Color.DarkGray);
        }

        private void identifierHelpBtn_Click(object sender, EventArgs e)
        {
            new IdentifierHelpDialog().ShowDialog();
        }

        private void identifierTextBox_TextChanged(object sender, EventArgs e)
        {
            var self = (TextBox)sender;

            self.Text = self.Text.Replace(" ", "");
            Properties.Settings.Default.identifier = self.Text;
        }
    }
}