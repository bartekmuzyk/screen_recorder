using NAudio.Wave;
using screen_recorder.AudioCapture;
using screen_recorder.Models;
using ScreenRecorderLib;
using System.Diagnostics;
using System.Globalization;
using Transitions;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;
using ScreenRecorder = ScreenRecorderLib.Recorder;

namespace screen_recorder
{
    public partial class Form1 : Form
    {
        private ThreadedProcessAudioRecorder? capAudioRecorder = null;

        private ScreenRecorder? capMainRecorder = null;

        private ProcessInfo[] availableAppsToCapture = Array.Empty<ProcessInfo>();

        private string saveDirectory = string.Empty;

        private int hiddenOptionsBlockerPosition;

        private readonly List<RecordingToMix> recordingsToMix = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MinimumSize = MaximumSize = Size;

            saveDirectory = Properties.Settings.Default.saveDir;

            if (!Directory.Exists(saveDirectory))
            {
                Properties.Settings.Default.saveDir = saveDirectory = string.Empty;
                Properties.Settings.Default.Save();
                MessageBox.Show("Poprzednio ustawiony folder dla nagrañ nie mo¿e zostaæ teraz znaleziony.", "Zresetowano œcie¿kê nagrañ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (saveDirectory != string.Empty)
            {
                saveDirectoryDisplay.Text = saveDirectory;
                openSaveDirBtn.Enabled = true;
            }

            identifierTextBox.Text = Properties.Settings.Default.identifier;

            ControlDecoration.MakeControlRounded(refreshAppsBtn, 1, 1, 2, 0);
            identifierHelpBtn.FlatAppearance.BorderSize = 0;
            ControlDecoration.MakeControlRounded(identifierHelpBtn, 1, 1, 3, 2, 20);
            ControlDecoration.MakeControlRounded(recordingsToMixCounterDisplay, 1, 2, 3, 1, 20);
            refreshAppsBtnTooltip.SetToolTip(refreshAppsBtn, "Odœwie¿ listê aplikacji");

            RefreshApps();

            hiddenOptionsBlockerPosition = optionsBlocker.Top;

            if (saveDirectory == string.Empty)
            {
                seeRecordingsToMixBtn.Enabled = false;
                seeRecordingsToMixBtn.Text = "Brak nagrañ do mixu";
            }

            RefreshRecordingsToMix();

            if (!File.Exists("ffmpeg.exe") || !File.Exists("ffprobe.exe"))
            {
                new FFMpegDownloadDialog().ShowDialog();
            }
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

        private void RefreshRecordingsToMix()
        {
            seeRecordingsToMixBtn.Enabled = false;
            seeRecordingsToMixBtn.Text = "Skanowanie nagrañ...";

            recordingsToMix.Clear();

            new Thread(() =>
            {
                RecordingInfo PathToRecording(string filePath)
                {
                    var fileName = Path.GetFileName(filePath);

                    var split = fileName.Split('_');
                    var splitFirstFragment = split[0].Split('-');
                    var splitLastFragment = split[split.Length - 1].Split('.');
                    split = new string[] {
                        splitFirstFragment[0], splitFirstFragment[1],
                        split[1], split[2],
                        splitLastFragment[0], splitLastFragment[1]
                    };

                    var recordingDate = DateTime.ParseExact(split[0], RecordingFilePathProvider.FILE_NAME_DATE_FORMAT, CultureInfo.InvariantCulture);

                    return new RecordingInfo(
                        recordingDate,
                        int.Parse(split[1]),
                        split[2],
                        split[3] switch
                        {
                            "CapMain" => RecordingType.CapMain,
                            "CapAudio" => RecordingType.CapAudio,
                            _ => throw new Exception($"Unkown recording file type: {split[3]}")
                        },
                        filePath
                    );
                }

                bool RecordingMatches(RecordingInfo left, RecordingInfo right)
                {
                    return (
                        left.GameName == right.GameName &&
                        left.RecordingNumber == right.RecordingNumber &&
                        left.Date == right.Date &&
                        left.Type != right.Type
                    );
                }

                var recordings = Directory.GetFiles(saveDirectory).Select(PathToRecording);
                var matches = new List<RecordingInfo[]>();

                foreach (var recording in recordings)
                {
                    var matchFound = false;

                    for (int i = 0; i < matches.Count; i++)
                    {
                        var match = matches[i];

                        if (match.Length == 2) continue;

                        if (RecordingMatches(match[0], recording))
                        {
                            matches[i] = new[] { match[0], recording };
                            matchFound = true;
                            break;
                        }
                    }

                    if (!matchFound)
                    {
                        matches.Add(new[] { recording });
                    }
                }

                matches.RemoveAll(match => match.Length < 2);

                foreach (var match in matches)
                {
                    recordingsToMix.Add(
                        new RecordingToMix(
                            match[0].Date,
                            match[0].RecordingNumber,
                            match[0].GameName,
                            match[0].Type == RecordingType.CapAudio ? match[0] : match[1],
                            match[1].Type == RecordingType.CapMain ? match[1] : match[0]
                        )
                    );
                }

                if (recordingsToMix.Count > 0)
                {
                    Invoke(() =>
                    {
                        recordingsToMixCounterDisplay.Text = recordingsToMix.Count.ToString();
                        recordingsToMixCounterDisplay.Visible = true;
                        seeRecordingsToMixBtn.Enabled = true;
                        seeRecordingsToMixBtn.Text = "Zobacz nagrania do mixu";
                    });
                }
                else
                {
                    Invoke(() =>
                    {
                        recordingsToMixCounterDisplay.Text = "0";
                        recordingsToMixCounterDisplay.Visible = false;
                        seeRecordingsToMixBtn.Enabled = false;
                        seeRecordingsToMixBtn.Text = "Brak nagrañ do mixu";
                    });
                }
            }).Start();
        }


        private void RefreshRecordingsToMixSync()
        {
            seeRecordingsToMixBtn.Enabled = false;
            seeRecordingsToMixBtn.Text = "Skanowanie nagrañ...";

            recordingsToMix.Clear();

            RecordingInfo PathToRecording(string filePath)
            {
                var fileName = Path.GetFileName(filePath);

                var split = fileName.Split('_');
                var splitFirstFragment = split[0].Split('-');
                var splitLastFragment = split[split.Length - 1].Split('.');
                split = new string[] {
                        splitFirstFragment[0], splitFirstFragment[1],
                        split[1], split[2],
                        splitLastFragment[0], splitLastFragment[1]
                    };

                var recordingDate = DateTime.ParseExact(split[0], RecordingFilePathProvider.FILE_NAME_DATE_FORMAT, CultureInfo.InvariantCulture);

                return new RecordingInfo(
                    recordingDate,
                    int.Parse(split[1]),
                    split[2],
                    split[3] switch
                    {
                        "CapMain" => RecordingType.CapMain,
                        "CapAudio" => RecordingType.CapAudio,
                        _ => throw new Exception($"Unkown recording file type: {split[3]}")
                    },
                    filePath
                );
            }

            bool RecordingMatches(RecordingInfo left, RecordingInfo right)
            {
                return (
                    left.GameName == right.GameName &&
                    left.RecordingNumber == right.RecordingNumber &&
                    left.Date == right.Date &&
                    left.Type != right.Type
                );
            }

            var recordings = Directory.GetFiles(saveDirectory).Select(PathToRecording);
            var matches = new List<RecordingInfo[]>();

            foreach (var recording in recordings)
            {
                var matchFound = false;

                for (int i = 0; i < matches.Count; i++)
                {
                    var match = matches[i];

                    if (match.Length == 2) continue;

                    if (RecordingMatches(match[0], recording))
                    {
                        matches[i] = new[] { match[0], recording };
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    matches.Add(new[] { recording });
                }
            }

            matches.RemoveAll(match => match.Length < 2);

            foreach (var match in matches)
            {
                recordingsToMix.Add(
                    new RecordingToMix(
                        match[0].Date,
                        match[0].RecordingNumber,
                        match[0].GameName,
                        match[0].Type == RecordingType.CapAudio ? match[0] : match[1],
                        match[1].Type == RecordingType.CapMain ? match[1] : match[0]
                    )
                );
            }

            if (recordingsToMix.Count > 0)
            {
                recordingsToMixCounterDisplay.Text = recordingsToMix.Count.ToString();
                recordingsToMixCounterDisplay.Visible = true;
                seeRecordingsToMixBtn.Enabled = true;
                seeRecordingsToMixBtn.Text = "Zobacz nagrania do mixu";
            }
            else
            {
                recordingsToMixCounterDisplay.Text = "0";
                recordingsToMixCounterDisplay.Visible = false;
                seeRecordingsToMixBtn.Enabled = false;
                seeRecordingsToMixBtn.Text = "Brak nagrañ do mixu";
            }
        }

        private void changeRecordingLocationBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = recordingsDirectoryChooser.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(recordingsDirectoryChooser.SelectedPath))
            {
                saveDirectory = Properties.Settings.Default.saveDir = saveDirectoryDisplay.Text = recordingsDirectoryChooser.SelectedPath;
                Properties.Settings.Default.Save();
                openSaveDirBtn.Enabled = true;
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
                // I decided not to do this, since:
                // 1. Garbage collection is heavy on resources
                // 2. Garbage collection on demand causes a little bit of lag and could make the text box unresponsive
                // 3. Garbage collection on demand should be a last resort anyways
                Properties.Settings.Default.Save();

                void FailedToStart(string reason)
                {
                    self.Text = "Rozpocznij nagrywanie";
                    self.Enabled = true;
                    MessageBox.Show(reason, "Nie uda³o siê rozpocz¹æ nagrania.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                self.Text = "Rozpoczynanie...";
                self.Enabled = false;

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

                if (string.IsNullOrEmpty(processToCapture.MainWindowTitle))
                {
                    FailedToStart("Wybrany proces do przechwytywania ju¿ nie posiada okna.");
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
                            IsFixedFramerate = false,
                            IsFragmentedMp4Enabled = true,
                            IsLowLatencyEnabled = true
                        },
                        AudioOptions = new()
                        {
                            IsAudioEnabled = true,
                            IsOutputDeviceEnabled = false,
                            IsInputDeviceEnabled = true
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

                var successfulCapAudioInit = capAudioRecorder.Start();

                if (!successfulCapAudioInit)
                {
                    File.Delete(pathProvider.CapAudioFilePath);
                    FailedToStart("Nie uda³o siê zainicjowaæ bufora audio. Ponowne rozpoczêcie nagrania mo¿e pomóc.");
                    EndRecording();
                    return;
                }

                capMainRecorder.Record(pathProvider.CapMainFilePath);

                self.Text = "Zatrzymaj nagrywanie";
                self.Enabled = true;
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

            self.Enabled = false;
            self.Text = "Koñczenie...";

            capAudioRecorder?.Stop();
            capAudioRecorder = null;

            capMainRecorder?.Stop();
            capMainRecorder?.Dispose();
            capMainRecorder = null;

            self.Text = "Rozpocznij nagrywanie";
            self.Enabled = true;
            recordingIcon.Visible = false;
            var transition = new Transition(new TransitionType_Acceleration(350));
            transition.add(timerDisplay, "Left", 16);
            transition.add(optionsBlocker, "Top", hiddenOptionsBlockerPosition);
            transition.run();

            var mixImmediately = MessageBox.Show("Czy chcesz od razu rozpocz¹æ mixowanie nagrania? Mo¿na zawsze to zrobiæ póŸniej.", "Mixuj nagranie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

            if (mixImmediately)
            {
                RefreshRecordingsToMixSync();

                if (recordingsToMix.Count == 0)
                {
                    MessageBox.Show("Nie ma ju¿ ¿adnych dostêpnych nagrañ do mixu.", "Brak nagrañ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                new MixingManagerDialog(recordingsToMix).ShowDialog();
            }

            RefreshRecordingsToMix();
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

        private void seeRecordingsToMixBtn_Click(object sender, EventArgs e)
        {
            RefreshRecordingsToMixSync();

            if (recordingsToMix.Count == 0)
            {
                MessageBox.Show("Nie ma ju¿ ¿adnych dostêpnych nagrañ do mixu.", "Brak nagrañ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new MixingManagerDialog(recordingsToMix).ShowDialog();
            RefreshRecordingsToMix();
        }

        private void openSaveDirBtn_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", saveDirectory);
        }
    }
}