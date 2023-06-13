using NAudio.Wave;
using screen_recorder.AudioCapture;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Transitions;

namespace screen_recorder
{
    public partial class Form1 : Form
    {
        private bool isRecording = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MinimumSize = MaximumSize = Size;
            ControlDecoration.MakeControlRounded(recordDiscordWarning);
            ControlDecoration.MakeControlRounded(refreshAppsBtn, 1, 1, 2, 0);
            refreshAppsBtnTooltip.SetToolTip(refreshAppsBtn, "Odœwie¿ listê aplikacji");
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
            if (!isRecording)
            {
                var process = Process.GetProcessesByName("vlc")[0];

                var outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
                Directory.CreateDirectory(outputFolder);
                var outputFilePath = Path.Combine(outputFolder, "recorded.wav");
                var writer = new WaveFileWriter(outputFilePath, CapturingService.Format);

                var capturingService = new CapturingService(process.Id);



                recordingIcon.Visible = true;
                Transition.run(timerDisplay, "Left", 52, new TransitionType_Deceleration(350));
                isRecording = true;
            }
            else
            {
                recordingIcon.Visible = false;
                Transition.run(timerDisplay, "Left", 16, new TransitionType_Acceleration(350));
                isRecording = false;
            }
        }

        private void refreshAppsBtn_Click(object sender, EventArgs e)
        {

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