using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard_Assignment.Classes
{
    /// <summary>
    /// A simple static class that provides methods for drawing graphics in a clean and contained manner.
    /// </summary>
    public static class GraphicsHandler
    {
        public enum ShapeType { Rect, Elli };

        /// <summary>
        /// Draws a line to the given Bitmap
        /// </summary>
        /// <param name="image">The Bitmap that the line will be drawn on.</param>
        /// <param name="tool">The Pen object to be used for drawing the line.</param>
        /// <param name="start">The Point at which the tail of the line while be located.</param>
        /// <param name="end">The Point at which the tip of the line while be located.</param>
        public static void DrawLine(Bitmap image, Pen tool, Point start, Point end)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawLine(tool, start, end);
            }
        }

        /// <summary>
        /// Draws a rectangle or an eclipse to the given Bitmap
        /// </summary>
        /// <param name="type">The type of shape to be drawn.</param>
        /// <param name="image">The Bitmap that the shape will be drawn on.</param>
        /// <param name="tool">The Pen object to be used for drawing the line.</param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="dim">The dimensions of the shape.</param>
        /// <param name="isFilled">A boolean indicating whether or not the same should be filled.</param>
        public static void DrawShape(ShapeType type, Bitmap image, Pen tool, Point first, Point last, Size dim, bool isFilled)
        {
            var origin = GetPoint(first, last);
            Rectangle rect = new Rectangle(origin, dim);
            using (Graphics g = Graphics.FromImage(image))
            {
                if (type == ShapeType.Rect)
                {
                    if (isFilled)
                    {
                        g.FillRectangle(new SolidBrush(tool.Color), rect);
                        return;
                    }
                    g.DrawRectangle(tool, rect);
                }
                else if (type == ShapeType.Elli)
                {
                    if (isFilled)
                    {
                        g.FillEllipse(new SolidBrush(tool.Color), rect);
                        return;
                    }
                    g.DrawEllipse(tool, rect);
                }
            }
        }
        
        private static Point GetPoint(Point first, Point last)
        {
            if (last.X > first.X && last.Y > first.Y)
            {
                return first;
            }
            else if (last.X < first.X && last.Y < first.Y)
            {
                return last;
            }
            else if (last.X < first.X && last.Y > first.Y)
            {
                return new Point(last.X, first.Y);
            }
            else if (last.X > first.X && last.Y < last.X)
            {
                return new Point(first.X, last.Y);
            }
            return new Point(0, 0);
        }

    }
}
