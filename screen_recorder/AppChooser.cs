using screen_recorder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder
{
    internal class AppChooser : ComboBox
    {
        public AppChooser()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            DropDownWidth = 500;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index >= 0 && e.Index < Items.Count)
            {
                var item = (ProcessInfo)Items[e.Index];
                var appIcon = !string.IsNullOrEmpty(item.FileName) ?
                    Icon.ExtractAssociatedIcon(item.FileName)?.ToBitmap()
                    :
                    null;

                if (appIcon != null)
                {
                    e.Graphics.DrawImage(appIcon, e.Bounds.Left, e.Bounds.Top, 18, 18);
                }
                e.Graphics.DrawString($"{item.Name} - {item.MainWindowTitle}", e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 20, e.Bounds.Top + 1);
            }

            base.OnDrawItem(e);
        }
    }
}
