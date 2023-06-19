using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace screen_recorder
{
    public partial class FFMpegDownloadDialog : Form
    {
        private bool finished = false;

        public FFMpegDownloadDialog()
        {
            InitializeComponent();
        }

        private void FFMpegDownloadDialog_Load(object sender, EventArgs e)
        {
            FFmpegDownloader.GetLatestVersion(
                FFmpegVersion.Official,
                new FFMpegDownloadProgress(percentage =>
                {
                    progressBar.Value = percentage;
                    percentageDisplay.Text = $"{percentage}%";
                })
            ).GetAwaiter().OnCompleted(() =>
            {
                Invoke(() =>
                {
                    finished = true;
                    Close();
                });
            });
        }

        private void FFMpegDownloadDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !finished;
        }
    }

    internal class FFMpegDownloadProgress : IProgress<ProgressInfo>
    {
        private readonly Action<int> onProgress;

        private Dictionary<long, long> progresses = new();

        private static decimal decimalHundred = Convert.ToDecimal(100);

        public FFMpegDownloadProgress(Action<int> onProgress)
        {
            this.onProgress = onProgress;
        }

        public void Report(ProgressInfo value)
        {
            if (!progresses.ContainsKey(value.TotalBytes))
            {
                progresses.Add(value.TotalBytes, 0);
            }

            progresses[value.TotalBytes] = value.DownloadedBytes;

            var percentage = (int)Math.Floor((decimal)progresses.Values.Sum() / progresses.Keys.Sum() * decimalHundred);
            onProgress(percentage);
        }
    }
}
