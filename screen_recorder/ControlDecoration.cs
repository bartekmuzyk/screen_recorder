using System.Runtime.InteropServices;

namespace screen_recorder
{
    internal static class ControlDecoration
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, // x-coordinate of upper-left corner
                                                        int nTopRect, // y-coordinate of upper-left corner
                                                        int nRightRect, // x-coordinate of lower-right corner
                                                        int nBottomRect, // y-coordinate of lower-right corner
                                                        int nWidthEllipse, // height of ellipse
                                                        int nHeightEllipse // width of ellipse
                                                        );

        public static void MakeControlRounded(Control control, int padding = 0, int radius = 7)
        {
            control.Region = Region.FromHrgn(CreateRoundRectRgn(0 + padding, 0 + padding, control.Width - padding, control.Height - padding, radius, radius));
        }

        public static void MakeControlRounded(Control control, int leftPadding, int rightPadding, int topPadding, int bottomPadding, int radius = 7)
        {
            control.Region = Region.FromHrgn(CreateRoundRectRgn(0 + leftPadding, 0 + topPadding, control.Width - rightPadding, control.Height - bottomPadding, radius, radius));
        }

        public static void AutoDecorate(Form form)
        {
            DarkMode.ApplyDarkTitleBar(form);
            form.BackColor = Color.FromArgb(25, 31, 34);
            form.ForeColor = Color.FromArgb(189, 230, 251);

            Decorate(form);
        }

        private static void Decorate(Control control)
        {
            foreach (var button in control.Controls.OfType<Button>())
            {
                if (button.FlatStyle == FlatStyle.Flat) continue;

                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 80);
                button.FlatAppearance.BorderColor = Color.FromArgb(41, 51, 56);
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(73, 88, 96);
                button.BackColor = Color.FromArgb(41, 51, 56);
                button.ForeColor = Color.FromArgb(189, 230, 251);
                MakeControlRounded(button);
            }

            foreach (var textBox in control.Controls.OfType<TextBox>())
            {
                textBox.BackColor = Color.FromArgb(41, 51, 56);
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.ForeColor = Color.White;
                MakeControlRounded(textBox, 1);
            }

            foreach (var comboBox in control.Controls.OfType<ComboBox>())
            {
                comboBox.FlatStyle = FlatStyle.Flat;
                comboBox.BackColor = Color.FromArgb(41, 51, 56);
                comboBox.ForeColor = Color.White;
                MakeControlRounded(comboBox, 2, 0, 2, 2);
            }

            foreach (var progressBar in control.Controls.OfType<ProgressBar>())
            {
                progressBar.BackColor = Color.FromArgb(41, 51, 56);
            }

            foreach (var panel in control.Controls.OfType<Panel>()) Decorate(panel);

            foreach (var groupBox in control.Controls.OfType<GroupBox>())
            {
                groupBox.ForeColor = Color.FromArgb(189, 230, 251);
                Decorate(groupBox);
            }
        }
    }
}
