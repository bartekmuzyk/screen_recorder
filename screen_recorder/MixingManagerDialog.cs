using screen_recorder.Models;
using System.Diagnostics;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Exceptions;

namespace screen_recorder
{
    public partial class MixingManagerDialog : Form
    {
        private List<RecordingToMix> recordingsToMix;

        private RecordingToMix? chosenRecording = null;

        private string capMainFileName = string.Empty;

        private string capAudioFileName = string.Empty;

        private readonly LabelSlideEffect labelSlideEffect = new();

        private bool mixInProgress = false;

        public MixingManagerDialog(List<RecordingToMix> recordingsToMix)
        {
            this.recordingsToMix = recordingsToMix;

            if (recordingsToMix.Count > 0)
            {
                chosenRecording = recordingsToMix[0];
            }

            InitializeComponent();
        }

        private void MixingManagerDialog_Load(object sender, EventArgs e)
        {
            if (recordingsToMix.Count == 0)
            {
                MessageBox.Show("Nie ma żadnych nagrań do nadających się do mixu.", "Brak nagrań", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            ControlDecoration.AutoDecorate(this);

            foreach (var recording in recordingsToMix)
            {
                recordingsListView.Items.Add(new ListViewItem(new[] { recording.GameName, recording.RecordingNumber.ToString(), recording.Date.ToString("d"), recording.Identifier }));
            }

            ControlDecoration.MakeControlRounded(capMainPanel);
            ControlDecoration.MakeControlRounded(capAudioPanel);
            ControlDecoration.MakeControlRounded(mixResultPanel);
            ControlDecoration.MakeControlRounded(recordingsListHeader);

            labelSlideEffect.AddLabel(capMainLabel);
            labelSlideEffect.AddLabel(capAudioLabel);
            labelSlideEffect.AddLabel(mixResultLabel);
        }

        private void recordingsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var self = (ListView)sender;

            bottomPanel.Visible = self.SelectedItems.Count > 0;

            if (self.SelectedItems.Count == 0)
            {
                labelSlideEffect.SetText(capMainLabel, string.Empty);
                labelSlideEffect.SetText(capAudioLabel, string.Empty);
                return;
            }

            chosenRecording = recordingsToMix[self.SelectedItems[0].Index];

            capMainFileName = Path.GetFileName(chosenRecording.CapMainRecording.Path);
            labelSlideEffect.SetText(capMainLabel, capMainFileName);
            capAudioFileName = Path.GetFileName(chosenRecording.CapAudioRecording.Path);
            labelSlideEffect.SetText(capAudioLabel, capAudioFileName);
            labelSlideEffect.SetText(mixResultLabel, Path.GetFileName(RecordingFilePathProvider.GetMixedFilePathForRecording(chosenRecording)));
        }

        private void labelSlideTimer_Tick(object sender, EventArgs e)
        {
            labelSlideEffect.Tick();
        }

        private async void StartMix(RecordingToMix recording)
        {
            startMixBtn.Enabled = false;
            mixInProgress = true;

            var capMainMediaInfo = await FFmpeg.GetMediaInfo(recording.CapMainRecording.Path);
            var capAudioMediaInfo = await FFmpeg.GetMediaInfo(recording.CapAudioRecording.Path);

            var conversion = FFmpeg.Conversions.New()
                .AddStream(capMainMediaInfo.Streams)
                .AddStream(capAudioMediaInfo.Streams)
                .AddParameter("-c:v copy")
                .SetOutput(RecordingFilePathProvider.GetMixedFilePathForRecording(recording));

            conversion.OnProgress += (sender, args) =>
            {
                var percent = (int)(Math.Round(args.Duration.TotalSeconds / args.TotalLength.TotalSeconds, 2) * 100);

                Invoke(() =>
                {
                    if (percent < 100) mixProgressBar.Value = percent + 1;
                    mixProgressBar.Value = percent;
                });
            };

            try
            {
                await conversion.Start();
                mixProgressBar.Value = 100;
                MessageBox.Show($"Zapisano do {conversion.OutputFilePath}", "Zakończono mix", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ConversionException ex)
            {
                MessageBox.Show(ex.Message, "Wystąpił błąd podczas mixowania", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            mixProgressBar.Value = 0;
            startMixBtn.Enabled = true;
            mixInProgress = false;
            mixInProgressWarning.Visible = false;
        }

        private void startMixBtn_Click(object sender, EventArgs e)
        {
            if (chosenRecording is null) return;

            if (File.Exists(RecordingFilePathProvider.GetMixedFilePathForRecording(chosenRecording)))
            {
                MessageBox.Show("To nagranie już zostało zmixowane. Jeżeli chcesz odświeżyć listę nagrań, zamknij to okno i otwórz je ponownie.", "Plik już istnieje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StartMix(chosenRecording);
        }

        private void MixingManagerDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mixInProgress)
            {
                mixInProgressWarning.Visible = true;
                e.Cancel = true;
            }
        }
    }
}
