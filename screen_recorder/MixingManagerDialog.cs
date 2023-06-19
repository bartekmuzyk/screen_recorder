using screen_recorder.Models;

namespace screen_recorder
{
    public partial class MixingManagerDialog : Form
    {
        private List<RecordingToMix> recordingToMix;

        public MixingManagerDialog(List<RecordingToMix> recordingToMix)
        {
            this.recordingToMix = recordingToMix;
            InitializeComponent();
        }

        private void MixingManagerDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
