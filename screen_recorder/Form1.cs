using NAudio.Wave;
using screen_recorder.AudioCapture;
using screen_recorder.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Transitions;

namespace screen_recorder
{
    public partial class Form1 : Form
    {
        private ThreadedRecorder? recorder = null;

        private ProcessInfo[] availableAppsToCapture = Array.Empty<ProcessInfo>();

        public Form1()
        {
            InitializeComponent();
        }

        private void RefreshApps()
        {
            availableAppsToCapture = Process.GetProcesses()
                .Where(proc => !string.IsNullOrEmpty(proc.MainWindowTitle))
                .Select(proc => new ProcessInfo(proc.Id, proc.ProcessName, proc.MainWindowTitle))
                .ToArray();
            appChooser.Items.Clear();
            appChooser.Items.AddRange(availableAppsToCapture.Select(processInfo => $"{processInfo.Name} - {processInfo.MainWindowTitle}").ToArray());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MinimumSize = MaximumSize = Size;
            ControlDecoration.MakeControlRounded(refreshAppsBtn, 1, 1, 2, 0);
            refreshAppsBtnTooltip.SetToolTip(refreshAppsBtn, "Odœwie¿ listê aplikacji");
            RefreshApps();
        }

        private void changeRecordingLocationBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = recordingsDirectoryChooser.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(recordingsDirectoryChooser.SelectedPath))
            {
                recordingsDirectoryDisplay.Text = recordingsDirectoryChooser.SelectedPath;
            }
        }

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            var self = (Button)sender;

            if (recorder is null)
            {
                self.Text = "Zatrzymaj nagrywanie";
                recorder = new ThreadedRecorder();

                //recorder.CreateProcessRecordingThread();
                //recorder.StartAll();

                recordingIcon.Visible = true;
                Transition.run(timerDisplay, "Left", 52, new TransitionType_Deceleration(350));
            }
            else
            {
                self.Text = "Rozpocznij nagrywanie";
                //recorder.StopAll();
                recorder = null;

                recordingIcon.Visible = false;
                Transition.run(timerDisplay, "Left", 16, new TransitionType_Acceleration(350));
            }
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
    }
}