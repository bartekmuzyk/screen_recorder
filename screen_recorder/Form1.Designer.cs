namespace screen_recorder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            timerDisplay = new Label();
            startRecordingButton = new Button();
            label2 = new Label();
            appChooser = new ComboBox();
            changeRecordingLocationBtn = new Button();
            recordingsDirectoryDisplay = new Label();
            recordingsDirectoryChooser = new FolderBrowserDialog();
            recordingIcon = new PictureBox();
            recordDiscordAndMic = new CheckBox();
            groupBox1 = new GroupBox();
            recordDiscordWarning = new Panel();
            label3 = new Label();
            label1 = new Label();
            refreshAppsBtn = new Label();
            refreshAppsBtnTooltip = new ToolTip(components);
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)recordingIcon).BeginInit();
            groupBox1.SuspendLayout();
            recordDiscordWarning.SuspendLayout();
            SuspendLayout();
            // 
            // timerDisplay
            // 
            timerDisplay.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            timerDisplay.Location = new Point(16, 9);
            timerDisplay.Name = "timerDisplay";
            timerDisplay.Size = new Size(200, 45);
            timerDisplay.TabIndex = 2;
            timerDisplay.Text = "00:00:00";
            timerDisplay.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // startRecordingButton
            // 
            startRecordingButton.Location = new Point(114, 57);
            startRecordingButton.Name = "startRecordingButton";
            startRecordingButton.Size = new Size(146, 23);
            startRecordingButton.TabIndex = 3;
            startRecordingButton.Text = "Rozpocznij nagrywanie";
            startRecordingButton.UseVisualStyleBackColor = true;
            startRecordingButton.Click += startRecordingButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 95);
            label2.Name = "label2";
            label2.Size = new Size(212, 15);
            label2.TabIndex = 5;
            label2.Text = "Wybierz aplikację do przechwytywania:";
            // 
            // appChooser
            // 
            appChooser.DropDownStyle = ComboBoxStyle.DropDownList;
            appChooser.FormattingEnabled = true;
            appChooser.Location = new Point(12, 113);
            appChooser.Name = "appChooser";
            appChooser.Size = new Size(228, 23);
            appChooser.TabIndex = 6;
            // 
            // changeRecordingLocationBtn
            // 
            changeRecordingLocationBtn.Location = new Point(6, 59);
            changeRecordingLocationBtn.Name = "changeRecordingLocationBtn";
            changeRecordingLocationBtn.Size = new Size(236, 23);
            changeRecordingLocationBtn.TabIndex = 7;
            changeRecordingLocationBtn.Text = "Wybierz lokalizację nagrań";
            changeRecordingLocationBtn.UseVisualStyleBackColor = true;
            changeRecordingLocationBtn.Click += changeRecordingLocationBtn_Click;
            // 
            // recordingsDirectoryDisplay
            // 
            recordingsDirectoryDisplay.AutoEllipsis = true;
            recordingsDirectoryDisplay.Location = new Point(6, 19);
            recordingsDirectoryDisplay.Name = "recordingsDirectoryDisplay";
            recordingsDirectoryDisplay.Size = new Size(236, 37);
            recordingsDirectoryDisplay.TabIndex = 8;
            recordingsDirectoryDisplay.Text = "Nie wybrano";
            recordingsDirectoryDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // recordingIcon
            // 
            recordingIcon.Image = Properties.Resources.rec_icon;
            recordingIcon.Location = new Point(20, 20);
            recordingIcon.Name = "recordingIcon";
            recordingIcon.Size = new Size(26, 26);
            recordingIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            recordingIcon.TabIndex = 9;
            recordingIcon.TabStop = false;
            recordingIcon.Visible = false;
            // 
            // recordDiscordAndMic
            // 
            recordDiscordAndMic.Location = new Point(12, 151);
            recordDiscordAndMic.Name = "recordDiscordAndMic";
            recordDiscordAndMic.Size = new Size(248, 38);
            recordDiscordAndMic.TabIndex = 11;
            recordDiscordAndMic.Text = "Nagrywaj dodatkowo rozmowę z Discord (jeżeli otwarty) oraz mój mikrofon";
            recordDiscordAndMic.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(changeRecordingLocationBtn);
            groupBox1.Controls.Add(recordingsDirectoryDisplay);
            groupBox1.Location = new Point(281, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(248, 88);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Zapisz nagrania w folderze:";
            // 
            // recordDiscordWarning
            // 
            recordDiscordWarning.BackColor = Color.DarkGoldenrod;
            recordDiscordWarning.Controls.Add(label3);
            recordDiscordWarning.Controls.Add(label1);
            recordDiscordWarning.Location = new Point(12, 192);
            recordDiscordWarning.Name = "recordDiscordWarning";
            recordDiscordWarning.Size = new Size(248, 37);
            recordDiscordWarning.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe MDL2 Assets", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(3, 9);
            label3.Name = "label3";
            label3.Size = new Size(28, 19);
            label3.TabIndex = 1;
            label3.Text = "";
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(32, 2);
            label1.Name = "label1";
            label1.Size = new Size(210, 33);
            label1.TabIndex = 0;
            label1.Text = "Co najmniej jedna osoba powinna włączyć tę opcję.";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // refreshAppsBtn
            // 
            refreshAppsBtn.Font = new Font("Segoe MDL2 Assets", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            refreshAppsBtn.Location = new Point(244, 112);
            refreshAppsBtn.Name = "refreshAppsBtn";
            refreshAppsBtn.Size = new Size(24, 24);
            refreshAppsBtn.TabIndex = 14;
            refreshAppsBtn.Text = "";
            refreshAppsBtn.TextAlign = ContentAlignment.MiddleCenter;
            refreshAppsBtn.Click += refreshAppsBtn_Click;
            refreshAppsBtn.MouseDown += refreshAppsBtn_MouseDown;
            refreshAppsBtn.MouseEnter += refreshAppsBtn_MouseEnter;
            refreshAppsBtn.MouseLeave += refreshAppsBtn_MouseLeave;
            refreshAppsBtn.MouseUp += refreshAppsBtn_MouseUp;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(64, 64, 64);
            panel1.Location = new Point(274, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1, 225);
            panel1.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(537, 243);
            Controls.Add(panel1);
            Controls.Add(refreshAppsBtn);
            Controls.Add(recordDiscordWarning);
            Controls.Add(timerDisplay);
            Controls.Add(groupBox1);
            Controls.Add(recordDiscordAndMic);
            Controls.Add(recordingIcon);
            Controls.Add(appChooser);
            Controls.Add(label2);
            Controls.Add(startRecordingButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Nagrywarka";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)recordingIcon).EndInit();
            groupBox1.ResumeLayout(false);
            recordDiscordWarning.ResumeLayout(false);
            recordDiscordWarning.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label timerDisplay;
        private Button startRecordingButton;
        private Label label2;
        private ComboBox appChooser;
        private Button changeRecordingLocationBtn;
        private Label recordingsDirectoryDisplay;
        private FolderBrowserDialog recordingsDirectoryChooser;
        private PictureBox recordingIcon;
        private CheckBox recordDiscordAndMic;
        private GroupBox groupBox1;
        private Panel recordDiscordWarning;
        private Label label1;
        private Label refreshAppsBtn;
        private ToolTip refreshAppsBtnTooltip;
        private Label label3;
        private Panel panel1;
    }
}