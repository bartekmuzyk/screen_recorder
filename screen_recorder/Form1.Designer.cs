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
            changeRecordingLocationBtn = new Button();
            saveDirectoryDisplay = new Label();
            recordingsDirectoryChooser = new FolderBrowserDialog();
            recordingIcon = new PictureBox();
            groupBox1 = new GroupBox();
            openSaveDirBtn = new Button();
            refreshAppsBtn = new Label();
            refreshAppsBtnTooltip = new ToolTip(components);
            identifierTextBox = new TextBox();
            appChooser = new AppChooser();
            identifierHelpBtn = new Button();
            recordWholeScreen = new CheckBox();
            optionsBlocker = new Panel();
            label3 = new Label();
            label1 = new Label();
            seeRecordingsToMixBtn = new Button();
            recordingsToMixCounterDisplay = new Label();
            recordingTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)recordingIcon).BeginInit();
            groupBox1.SuspendLayout();
            optionsBlocker.SuspendLayout();
            SuspendLayout();
            // 
            // timerDisplay
            // 
            timerDisplay.AutoSize = true;
            timerDisplay.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            timerDisplay.Location = new Point(16, 9);
            timerDisplay.Name = "timerDisplay";
            timerDisplay.Size = new Size(146, 45);
            timerDisplay.TabIndex = 2;
            timerDisplay.Text = "00:00:00";
            timerDisplay.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // startRecordingButton
            // 
            startRecordingButton.Location = new Point(124, 57);
            startRecordingButton.Name = "startRecordingButton";
            startRecordingButton.Size = new Size(136, 23);
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
            // changeRecordingLocationBtn
            // 
            changeRecordingLocationBtn.Location = new Point(6, 59);
            changeRecordingLocationBtn.Name = "changeRecordingLocationBtn";
            changeRecordingLocationBtn.Size = new Size(168, 23);
            changeRecordingLocationBtn.TabIndex = 7;
            changeRecordingLocationBtn.Text = "Wybierz lokalizację nagrań";
            changeRecordingLocationBtn.UseVisualStyleBackColor = true;
            changeRecordingLocationBtn.Click += changeRecordingLocationBtn_Click;
            // 
            // saveDirectoryDisplay
            // 
            saveDirectoryDisplay.AutoEllipsis = true;
            saveDirectoryDisplay.Location = new Point(6, 19);
            saveDirectoryDisplay.Name = "saveDirectoryDisplay";
            saveDirectoryDisplay.Size = new Size(236, 37);
            saveDirectoryDisplay.TabIndex = 8;
            saveDirectoryDisplay.Text = "Nie wybrano";
            saveDirectoryDisplay.TextAlign = ContentAlignment.MiddleCenter;
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
            // groupBox1
            // 
            groupBox1.Controls.Add(openSaveDirBtn);
            groupBox1.Controls.Add(changeRecordingLocationBtn);
            groupBox1.Controls.Add(saveDirectoryDisplay);
            groupBox1.Location = new Point(12, 164);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(248, 88);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Zapisz nagrania w folderze:";
            // 
            // openSaveDirBtn
            // 
            openSaveDirBtn.Enabled = false;
            openSaveDirBtn.Location = new Point(180, 59);
            openSaveDirBtn.Name = "openSaveDirBtn";
            openSaveDirBtn.Size = new Size(62, 23);
            openSaveDirBtn.TabIndex = 9;
            openSaveDirBtn.Text = "Otwórz";
            openSaveDirBtn.UseVisualStyleBackColor = true;
            openSaveDirBtn.Click += openSaveDirBtn_Click;
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
            // identifierTextBox
            // 
            identifierTextBox.CharacterCasing = CharacterCasing.Upper;
            identifierTextBox.Location = new Point(12, 58);
            identifierTextBox.MaxLength = 10;
            identifierTextBox.Name = "identifierTextBox";
            identifierTextBox.PlaceholderText = "Identyfikator";
            identifierTextBox.Size = new Size(75, 23);
            identifierTextBox.TabIndex = 16;
            identifierTextBox.TextChanged += identifierTextBox_TextChanged;
            // 
            // appChooser
            // 
            appChooser.DrawMode = DrawMode.OwnerDrawFixed;
            appChooser.DropDownStyle = ComboBoxStyle.DropDownList;
            appChooser.DropDownWidth = 500;
            appChooser.FormattingEnabled = true;
            appChooser.Location = new Point(12, 112);
            appChooser.Name = "appChooser";
            appChooser.Size = new Size(226, 24);
            appChooser.TabIndex = 17;
            // 
            // identifierHelpBtn
            // 
            identifierHelpBtn.BackColor = Color.DodgerBlue;
            identifierHelpBtn.FlatStyle = FlatStyle.Flat;
            identifierHelpBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            identifierHelpBtn.ForeColor = Color.White;
            identifierHelpBtn.Location = new Point(90, 58);
            identifierHelpBtn.Name = "identifierHelpBtn";
            identifierHelpBtn.Size = new Size(20, 23);
            identifierHelpBtn.TabIndex = 18;
            identifierHelpBtn.Text = "?";
            identifierHelpBtn.UseVisualStyleBackColor = false;
            identifierHelpBtn.Click += identifierHelpBtn_Click;
            // 
            // recordWholeScreen
            // 
            recordWholeScreen.AutoSize = true;
            recordWholeScreen.Location = new Point(12, 139);
            recordWholeScreen.Name = "recordWholeScreen";
            recordWholeScreen.Size = new Size(222, 19);
            recordWholeScreen.TabIndex = 19;
            recordWholeScreen.Text = "Nagrywaj główny ekran zamiast okna";
            recordWholeScreen.UseVisualStyleBackColor = true;
            // 
            // optionsBlocker
            // 
            optionsBlocker.Controls.Add(label3);
            optionsBlocker.Controls.Add(label1);
            optionsBlocker.Location = new Point(0, 301);
            optionsBlocker.Name = "optionsBlocker";
            optionsBlocker.Size = new Size(273, 185);
            optionsBlocker.TabIndex = 20;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 84);
            label3.Name = "label3";
            label3.Size = new Size(229, 15);
            label3.TabIndex = 1;
            label3.Text = "Opcje pokażą się po zakończeniu nagrania";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(18, 45);
            label1.Name = "label1";
            label1.Size = new Size(238, 37);
            label1.TabIndex = 0;
            label1.Text = "Trwa nagrywanie";
            // 
            // seeRecordingsToMixBtn
            // 
            seeRecordingsToMixBtn.Enabled = false;
            seeRecordingsToMixBtn.Location = new Point(12, 258);
            seeRecordingsToMixBtn.Name = "seeRecordingsToMixBtn";
            seeRecordingsToMixBtn.Size = new Size(189, 23);
            seeRecordingsToMixBtn.TabIndex = 21;
            seeRecordingsToMixBtn.Text = "Skanowanie nagrań...";
            seeRecordingsToMixBtn.UseVisualStyleBackColor = true;
            seeRecordingsToMixBtn.Click += seeRecordingsToMixBtn_Click;
            // 
            // recordingsToMixCounterDisplay
            // 
            recordingsToMixCounterDisplay.BackColor = Color.Red;
            recordingsToMixCounterDisplay.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            recordingsToMixCounterDisplay.ForeColor = Color.White;
            recordingsToMixCounterDisplay.Location = new Point(207, 258);
            recordingsToMixCounterDisplay.Name = "recordingsToMixCounterDisplay";
            recordingsToMixCounterDisplay.Size = new Size(23, 23);
            recordingsToMixCounterDisplay.TabIndex = 22;
            recordingsToMixCounterDisplay.Text = "0";
            recordingsToMixCounterDisplay.TextAlign = ContentAlignment.MiddleCenter;
            recordingsToMixCounterDisplay.Visible = false;
            // 
            // recordingTimer
            // 
            recordingTimer.Interval = 1000;
            recordingTimer.Tick += recordingTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(273, 291);
            Controls.Add(optionsBlocker);
            Controls.Add(recordingsToMixCounterDisplay);
            Controls.Add(seeRecordingsToMixBtn);
            Controls.Add(recordWholeScreen);
            Controls.Add(identifierHelpBtn);
            Controls.Add(appChooser);
            Controls.Add(identifierTextBox);
            Controls.Add(refreshAppsBtn);
            Controls.Add(timerDisplay);
            Controls.Add(groupBox1);
            Controls.Add(recordingIcon);
            Controls.Add(label2);
            Controls.Add(startRecordingButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nagrywarka";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)recordingIcon).EndInit();
            groupBox1.ResumeLayout(false);
            optionsBlocker.ResumeLayout(false);
            optionsBlocker.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label timerDisplay;
        private Button startRecordingButton;
        private Label label2;
        private Button changeRecordingLocationBtn;
        private Label saveDirectoryDisplay;
        private FolderBrowserDialog recordingsDirectoryChooser;
        private PictureBox recordingIcon;
        private GroupBox groupBox1;
        private Label refreshAppsBtn;
        private ToolTip refreshAppsBtnTooltip;
        private TextBox identifierTextBox;
        private AppChooser appChooser;
        private Button identifierHelpBtn;
        private CheckBox recordWholeScreen;
        private Panel optionsBlocker;
        private Label label1;
        private Label label3;
        private Button seeRecordingsToMixBtn;
        private Label recordingsToMixCounterDisplay;
        private Button openSaveDirBtn;
        private System.Windows.Forms.Timer recordingTimer;
    }
}