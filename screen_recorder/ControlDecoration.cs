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
    }
}
