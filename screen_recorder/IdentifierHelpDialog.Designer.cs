namespace screen_recorder
{
    partial class IdentifierHelpDialog
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
            label2 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(268, 75);
            label2.TabIndex = 1;
            label2.Text = "Identyfikator pozwala na automatyczne dopisanie\r\ndanego ciągu znaków do nazwy pliku nagrania.\r\n\r\nDla identyfikatora \"ABC\" zostaną wygenerowane\r\nnastępujące pliki po zakończeniu nagrania:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.identifierHelpScreenshot;
            pictureBox1.Location = new Point(8, 87);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(276, 55);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(64, 64, 64);
            button1.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(32, 32, 32);
            button1.FlatAppearance.MouseOverBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(211, 147);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Zamknij";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(0, -8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(292, 184);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // IdentifierHelpDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(292, 178);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            ForeColor = Color.FromArgb(224, 224, 224);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IdentifierHelpDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Czym jest identyfikator?";
            TopMost = true;
            Deactivate += IdentifierHelpDialog_Deactivate;
            Load += IdentifierHelpDialog_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private PictureBox pictureBox1;
        private Button button1;
        private GroupBox groupBox1;
    }
}