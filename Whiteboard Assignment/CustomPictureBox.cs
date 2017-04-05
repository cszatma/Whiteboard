using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Whiteboard_Assignment
{
    class CustomPictureBox : System.Windows.Forms.PictureBox
    {
        Bitmap bitmap;
        Bitmap background;

        public void AddBorder(Color color)
        {
            bitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Color c = color;
            Pen p = new Pen(c, 4);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawRectangle(p, 0, 0, this.Width - 2, this.Height - 2);
            }
            this.BackgroundImage = bitmap;

        }

        public void RemoveBorder()
        {
            background = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(background))
            {
                g.Clear(this.BackColor);
            }
            this.BackgroundImage = background;
        }
    }
}
