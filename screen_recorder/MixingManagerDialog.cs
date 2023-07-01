using screen_recorder.Models;
using System.Diagnostics;

namespace screen_recorder
{
    public partial class MixingManagerDialog : Form
    {
        private List<RecordingToMix> recordingsToMix;

        private RecordingToMix? chosenRecording = null;

        private string capMainFileName = string.Empty;

        private string capAudioFileName = string.Empty;

        private readonly LabelSlideEffect labelSlideEffect = new();

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

            var recordingToMix = chosenRecording = recordingsToMix[self.SelectedItems[0].Index];

            capMainFileName = Path.GetFileName(recordingToMix.CapMainRecording.Path);
            labelSlideEffect.SetText(capMainLabel, capMainFileName);
            capAudioFileName = Path.GetFileName(recordingToMix.CapAudioRecording.Path);
            labelSlideEffect.SetText(capAudioLabel, capAudioFileName);
        }

        private void labelSlideTimer_Tick(object sender, EventArgs e)
        {
            labelSlideEffect.Tick();
        }

        private void startMixBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
