using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screen_recorder
{
    public partial class IdentifierHelpDialog : Form
    {
        public IdentifierHelpDialog()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Cursor.Position.X - (Width / 2), Math.Max(Cursor.Position.Y - Height - 18, 0));
        }

        private void IdentifierHelpDialog_Load(object sender, EventArgs e)
        {
            ControlDecoration.MakeControlRounded(pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IdentifierHelpDialog_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}
