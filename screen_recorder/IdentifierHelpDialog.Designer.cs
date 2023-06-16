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
            // IdentifierHelpDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 149);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IdentifierHelpDialog";
            ShowIcon = false;
            Text = "Czym jest identyfikator?";
            Load += IdentifierHelpDialog_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private PictureBox pictureBox1;
    }
}