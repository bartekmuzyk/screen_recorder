using System.Runtime.InteropServices;

namespace screen_recorder
{
    internal static class ControlDecoration
    {
        private static readonly Color foreColor = Color.FromArgb(189, 230, 251);

        private static readonly Color controlBackColor = Color.FromArgb(41, 51, 56);

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
            form.ForeColor = foreColor;

            Decorate(form);
        }

        private static void Decorate(Control control)
        {
            foreach (var button in control.Controls.OfType<Button>())
            {
                if (button.FlatStyle == FlatStyle.Flat) continue;

                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 73, 80);
                button.FlatAppearance.BorderColor = controlBackColor;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(73, 88, 96);
                button.BackColor = controlBackColor;
                button.ForeColor = foreColor;
                MakeControlRounded(button);
            }

            foreach (var textBox in control.Controls.OfType<TextBox>())
            {
                var height = textBox.Height;
                textBox.BackColor = controlBackColor;
                textBox.BorderStyle = BorderStyle.None;
                textBox.ForeColor = Color.White;
                textBox.Location = new Point(textBox.Location.X, textBox.Location.Y + 4);
                textBox.Height = height;
                MakeControlRounded(textBox, 0, 5);
            }

            foreach (var comboBox in control.Controls.OfType<ComboBox>())
            {
                comboBox.FlatStyle = FlatStyle.Flat;
                comboBox.BackColor = controlBackColor;
                comboBox.ForeColor = Color.White;
                MakeControlRounded(comboBox, 2, 0, 2, 2);
            }

            foreach (var progressBar in control.Controls.OfType<ProgressBar>())
            {
                progressBar.BackColor = controlBackColor;
            }

            foreach (var panel in control.Controls.OfType<Panel>())
            {
                if (panel.BackColor == SystemColors.ControlLight)
                {
                    panel.BackColor = controlBackColor;
                }

                Decorate(panel);
            }

            foreach (var groupBox in control.Controls.OfType<GroupBox>())
            {
                groupBox.ForeColor = foreColor;
                Decorate(groupBox);
            }

            foreach (var listView in control.Controls.OfType<ListView>())
            {
                listView.BackColor = controlBackColor;
                listView.ForeColor = foreColor;
                listView.BorderStyle = BorderStyle.None;
                MakeControlRounded(listView);
            }
        }
    }
}
