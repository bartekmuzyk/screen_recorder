namespace screen_recorder
{
    partial class FFMpegDownloadDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFMpegDownloadDialog));
            label1 = new Label();
            label2 = new Label();
            progressBar = new ProgressBar();
            percentageDisplay = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(254, 32);
            label1.TabIndex = 0;
            label1.Text = "Pobieranie FFMpeg...";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 41);
            label2.Name = "label2";
            label2.Size = new Size(330, 30);
            label2.TabIndex = 1;
            label2.Text = "Dodatkowe oprogramowanie jest wymagane do poprawnego\r\ndziałania aplikacji.";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 83);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(352, 23);
            progressBar.TabIndex = 2;
            // 
            // percentageDisplay
            // 
            percentageDisplay.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            percentageDisplay.Location = new Point(264, 109);
            percentageDisplay.Name = "percentageDisplay";
            percentageDisplay.Size = new Size(100, 25);
            percentageDisplay.TabIndex = 3;
            percentageDisplay.Text = "0%";
            percentageDisplay.TextAlign = ContentAlignment.TopRight;
            // 
            // FFMpegDownloadDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(376, 136);
            ControlBox = false;
            Controls.Add(percentageDisplay);
            Controls.Add(progressBar);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FFMpegDownloadDialog";
            Text = "Jeszcze tylko chwila!";
            FormClosing += FFMpegDownloadDialog_FormClosing;
            Load += FFMpegDownloadDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ProgressBar progressBar;
        private Label percentageDisplay;
    }
}