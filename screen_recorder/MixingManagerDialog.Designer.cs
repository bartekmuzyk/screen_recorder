namespace screen_recorder
{
    partial class MixingManagerDialog
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MixingManagerDialog));
            recordingsListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            capMainPanel = new Panel();
            capMainLabel = new Label();
            label1 = new Label();
            label2 = new Label();
            capAudioPanel = new Panel();
            capAudioLabel = new Label();
            mixResultPanel = new Panel();
            mixResultLabel = new Label();
            pictureBox1 = new PictureBox();
            labelSlideTimer = new System.Windows.Forms.Timer(components);
            bottomPanel = new Panel();
            capMainPanel.SuspendLayout();
            capAudioPanel.SuspendLayout();
            mixResultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // recordingsListView
            // 
            recordingsListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            recordingsListView.FullRowSelect = true;
            recordingsListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            recordingsListView.Location = new Point(12, 12);
            recordingsListView.Name = "recordingsListView";
            recordingsListView.Size = new Size(387, 172);
            recordingsListView.TabIndex = 0;
            recordingsListView.UseCompatibleStateImageBehavior = false;
            recordingsListView.View = View.Details;
            recordingsListView.SelectedIndexChanged += recordingsListView_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Nazwa nagrywanej gry";
            columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Nr nagr.";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Data nagrania";
            columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Identyfikator";
            columnHeader4.Width = 80;
            // 
            // capMainPanel
            // 
            capMainPanel.BackColor = SystemColors.ControlLight;
            capMainPanel.Controls.Add(capMainLabel);
            capMainPanel.Controls.Add(label1);
            capMainPanel.Location = new Point(0, 0);
            capMainPanel.Name = "capMainPanel";
            capMainPanel.Size = new Size(170, 40);
            capMainPanel.TabIndex = 1;
            // 
            // capMainLabel
            // 
            capMainLabel.AutoEllipsis = true;
            capMainLabel.Location = new Point(1, 21);
            capMainLabel.Name = "capMainLabel";
            capMainLabel.Size = new Size(166, 15);
            capMainLabel.TabIndex = 3;
            capMainLabel.Text = "-";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(1, 1);
            label1.Name = "label1";
            label1.Size = new Size(162, 17);
            label1.TabIndex = 2;
            label1.Text = "Przechwytywanie główne";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(1, 1);
            label2.Name = "label2";
            label2.Size = new Size(152, 17);
            label2.TabIndex = 4;
            label2.Text = "Przechwytywanie audio";
            // 
            // capAudioPanel
            // 
            capAudioPanel.BackColor = SystemColors.ControlLight;
            capAudioPanel.Controls.Add(capAudioLabel);
            capAudioPanel.Controls.Add(label2);
            capAudioPanel.Location = new Point(0, 51);
            capAudioPanel.Name = "capAudioPanel";
            capAudioPanel.Size = new Size(170, 40);
            capAudioPanel.TabIndex = 3;
            // 
            // capAudioLabel
            // 
            capAudioLabel.AutoEllipsis = true;
            capAudioLabel.Location = new Point(1, 21);
            capAudioLabel.Name = "capAudioLabel";
            capAudioLabel.Size = new Size(166, 15);
            capAudioLabel.TabIndex = 5;
            capAudioLabel.Text = "-";
            // 
            // mixResultPanel
            // 
            mixResultPanel.BackColor = SystemColors.ControlLight;
            mixResultPanel.Controls.Add(mixResultLabel);
            mixResultPanel.Location = new Point(217, 33);
            mixResultPanel.Name = "mixResultPanel";
            mixResultPanel.Size = new Size(170, 24);
            mixResultPanel.TabIndex = 5;
            // 
            // mixResultLabel
            // 
            mixResultLabel.AutoEllipsis = true;
            mixResultLabel.Location = new Point(0, 4);
            mixResultLabel.Name = "mixResultLabel";
            mixResultLabel.Size = new Size(166, 15);
            mixResultLabel.TabIndex = 4;
            mixResultLabel.Text = "-";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.mixing_icon;
            pictureBox1.Location = new Point(176, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 35);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // labelSlideTimer
            // 
            labelSlideTimer.Enabled = true;
            labelSlideTimer.Interval = 125;
            labelSlideTimer.Tick += labelSlideTimer_Tick;
            // 
            // bottomPanel
            // 
            bottomPanel.Controls.Add(capMainPanel);
            bottomPanel.Controls.Add(mixResultPanel);
            bottomPanel.Controls.Add(pictureBox1);
            bottomPanel.Controls.Add(capAudioPanel);
            bottomPanel.Location = new Point(12, 197);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(387, 91);
            bottomPanel.TabIndex = 8;
            bottomPanel.Visible = false;
            // 
            // MixingManagerDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(411, 300);
            Controls.Add(bottomPanel);
            Controls.Add(recordingsListView);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MixingManagerDialog";
            Text = "Menedżer mixowania";
            Load += MixingManagerDialog_Load;
            capMainPanel.ResumeLayout(false);
            capMainPanel.PerformLayout();
            capAudioPanel.ResumeLayout(false);
            capAudioPanel.PerformLayout();
            mixResultPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView recordingsListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Panel capMainPanel;
        private Label label1;
        private Label label2;
        private Panel capAudioPanel;
        private Panel mixResultPanel;
        private PictureBox pictureBox1;
        private Label capMainLabel;
        private Label capAudioLabel;
        private Label mixResultLabel;
        private System.Windows.Forms.Timer labelSlideTimer;
        private Panel bottomPanel;
    }
}