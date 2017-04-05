using System;
using System.Drawing;
using System.Windows.Forms;
using Whiteboard_Assignment.Classes;

namespace Whiteboard_Assignment
{
    public partial class MainWindowForm : Form
    {
        enum ToolType { Pen, Eraser, Highlighter, Line, Rectangle, Ellipse, Triangle, Bucket };
        DrawingTool[] tools; //Array of all the available tools.
        DrawingTool userTool; //Stores the tool that is currently in use.
        bool isDrawing = false; //Used to determine if the user is currently drawing -> Necessary for the mouse events
        CustomPictureBox selectedToolPic; //Stores picturebox that matches the current tool
        CustomPictureBox selectedColorPic; //Stores the picturebox that matches the color of the current tool if there is one
        Bitmap image;
        Point firstPoint;
        Point lastPoint;
        Size shapeDim;
        Point?[] triPoints;

        public MainWindowForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setUpPens();
            userTool = tools[0];
            image = new Bitmap(cstpnlWhiteboard.ClientSize.Width, cstpnlWhiteboard.ClientSize.Height);
            SetupEvents(cstpnlTools);
            SetupEvents(cstpnlToolColor);
            cstpicPenTool.AddBorder(Color.Black);
            selectedToolPic = cstpicPenTool;
            cstpicToolBlack.AddBorder(Color.Gray);
            selectedColorPic = cstpicToolBlack;
            triPoints = new Point?[3];
            cstpnlInfo.Visible = true;
        }

        private void setUpPens()
        {
            tools = new DrawingTool[8];
            tools[0] = new DrawingTool("pen", new Pen(Color.Black, 1), false);
            tools[1] = new DrawingTool("eraser", new Pen(Color.White, 1), false);
            tools[2] = new DrawingTool("highlighter", new Pen(Color.FromArgb(100, 0, 0, 0), 1), false);
            tools[3] = new DrawingTool("line", new Pen(Color.Black, 1), false);
            tools[4] = new DrawingTool("rectangle", new Pen(Color.Black, 1), false);
            tools[5] = new DrawingTool("ellipse", new Pen(Color.Black, 1), false);
            tools[6] = new DrawingTool("triangle", new Pen(Color.Black, 1), false);
            tools[7] = new DrawingTool("bucket", new Pen(Color.Black, 0), false);

            foreach (DrawingTool t in tools)
            {
                t.Tool.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                t.Tool.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (DrawingTool t in tools)
            {
                t.Tool.Dispose();
            }
            image.Dispose();
        }

        private void cstpnlWhiteboard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (userTool == tools[(int)ToolType.Pen] || userTool == tools[(int)ToolType.Eraser] || userTool == tools[(int)ToolType.Highlighter] || userTool == tools[(int)ToolType.Line])
                {
                    cstpnlToolThickness.Visible = true;
                    cstpnlToolThickness.Location = e.Location;
                    tkbToolThickness.Value = (int)userTool.Tool.Width;
                    txtToolThickness.Text = Convert.ToString(userTool.Tool.Width);
                }


            }
            else if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                firstPoint = e.Location;
                cstpnlToolThickness.Visible = false;
            }

        }

        private void cstpnlWhiteboard_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            if (userTool == tools[(int)ToolType.Line])
            {
                GraphicsHandler.DrawLine(image, userTool.Tool, firstPoint, lastPoint);
                cstpnlWhiteboard.Invalidate();
                firstPoint = lastPoint;
            }
            else if (userTool == tools[(int)ToolType.Rectangle])
            {
                GraphicsHandler.DrawShape(GraphicsHandler.ShapeType.Rect, image, userTool.Tool, firstPoint, lastPoint, shapeDim, userTool.IsFilled);
                cstpnlWhiteboard.Invalidate();
            }
            else if (userTool == tools[(int)ToolType.Ellipse])
            {
                GraphicsHandler.DrawShape(GraphicsHandler.ShapeType.Elli, image, userTool.Tool, firstPoint, lastPoint, shapeDim, userTool.IsFilled);
                cstpnlWhiteboard.Invalidate();
            }
            else if (userTool == tools[(int)ToolType.Triangle])
            {
                if (triPoints[0] == null)
                {
                    triPoints[0] = new Point(e.X, e.Y);
                }
                else if (triPoints[1] == null)
                {
                    triPoints[1] = new Point(e.X, e.Y);
                }
                else if (triPoints[2] == null)
                {
                    triPoints[2] = new Point(e.X, e.Y);
                    Point[] trianglePoints = { triPoints[0].Value, triPoints[1].Value, triPoints[2].Value };
                    using (Graphics g = Graphics.FromImage(image))
                    {
                        if (chkFilledShape.Checked == true)
                        {
                            g.FillPolygon(new SolidBrush(userTool.Color), trianglePoints);
                        }
                        else if (chkFilledShape.Checked == false)
                        {
                            g.DrawPolygon(userTool.Tool, trianglePoints);
                        }
                    }
                    cstpnlWhiteboard.Invalidate();
                    triPoints[0] = null;
                    triPoints[1] = null;
                    triPoints[2] = null;
                }
            }

            else if (userTool == tools[(int)ToolType.Bucket])
            {
                cstpnlWhiteboard.BackColor = userTool.Color;
            }
        }

        private void cstpnlWhiteboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                lastPoint = e.Location;
                if (userTool == tools[(int)ToolType.Pen] || userTool == tools[(int)ToolType.Eraser] || userTool == tools[(int)ToolType.Highlighter])
                {
                    GraphicsHandler.DrawLine(image, userTool.Tool, firstPoint, lastPoint);
                    cstpnlWhiteboard.Invalidate();
                    firstPoint = lastPoint;
                }
                else if (userTool == tools[(int)ToolType.Rectangle] || userTool == tools[(int)ToolType.Rectangle])
                {
                    shapeDim = new Size();
                    shapeDim.Width = lastPoint.X > firstPoint.X ? lastPoint.X - firstPoint.X : firstPoint.X - lastPoint.X;
                    shapeDim.Height = lastPoint.Y > firstPoint.Y ? lastPoint.Y - firstPoint.Y : firstPoint.Y - lastPoint.Y;
                }

            }
        }

        private void cstpnlWhiteboard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, Point.Empty);
        }

        private void clearWhiteboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(cstpnlWhiteboard.BackColor);
            }
            cstpnlWhiteboard.Invalidate();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Thickness
        private void tkbToolThickness_Scroll(object sender, EventArgs e)
        {
            userTool.Tool.Width = tkbToolThickness.Value;
            txtToolThickness.Text = Convert.ToString(tkbToolThickness.Value);
        }

        private void txtToolThickness_TextChanged(object sender, EventArgs e)
        {
            try
            {
                userTool.Tool.Width = Convert.ToSingle(txtToolThickness.Text);
                tkbToolThickness.Value = Convert.ToInt32(txtToolThickness.Text);
            }
            catch
            {
                MessageBox.Show("Invalid Thickness entry. Please enter an integer between 1 and 99", "Whiteboard");
            }
        }
        #endregion

        #region Events
        private void SetupEvents(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is CustomPictureBox)
                {
                    control.Click += HandleClicks;
                }
                else if (control is TrackBar)
                {
                    (control as TrackBar).Scroll += HandleScroll;
                }
                else if (control is TextBox)
                {
                    (control as TextBox).TextChanged += HandleTextChanged;
                }
                
            }
        }

        private void HandleClicks(object sender, EventArgs e)
        {
            var control = (CustomPictureBox)sender;
            int index;
            if (int.TryParse((string)control.Tag, out index))
            {
                HandleToolChange(control, index);
            }
            else if ((string)control.Tag == "color")
            {
                HandleColorChange(control);
            }

        }

        private void HandleToolChange(CustomPictureBox toolPic, int index)
        {
            selectedToolPic.RemoveBorder(); //Remove Border from previous tool
            selectedToolPic = toolPic;
            toolPic.AddBorder(Color.Black); //Add Border to selected tool picturebox
            if (toolPic == cstpicEraserTool)
            {
                cstpnlToolColor.Enabled = false;
                userTool = tools[index];
                return;
            }
            chkFilledShape.Visible = (toolPic == cstpicRectangleTool || toolPic == cstpicEllipseTool || toolPic == cstpicTriangleTool) ? true : false;
            cstpnlToolColor.Enabled = true;

            if (userTool.Color != tools[index].Color)
            {
                Color toolColor = tools[index].Color;
                userTool = tools[index];
                chkFilledShape.Checked = tools[index].IsFilled;
                chkCustomToolColor.Checked = isCustomColor(tools[index].Color);
                if (isCustomColor(toolColor))
                {
                    int[] rgb = { toolColor.R, toolColor.G, toolColor.B };
                    txtToolR.Text = Convert.ToString(rgb[0]);
                    txtToolG.Text = Convert.ToString(rgb[1]);
                    txtToolB.Text = Convert.ToString(rgb[2]);
                }
                else
                {
                    userTool.Color = toolColor;
                    foreach (Control c in cstpnlToolColor.Controls)
                    {
                        if (c is CustomPictureBox)
                        {
                            if (c.BackColor == toolColor)
                            {
                                (c as CustomPictureBox).AddBorder(Color.Gray);
                            }
                        }
                    }
                }
                
            }
            userTool = tools[index]; //Update the current tool
            chkFilledShape.Checked = tools[index].IsFilled;
        }

        private void HandleColorChange(CustomPictureBox colorPic)
        {
            selectedColorPic.RemoveBorder();
            colorPic.AddBorder(Color.Gray);
            selectedColorPic = colorPic;
            userTool.Color = colorPic.BackColor;
        }

        private void HandleScroll(object sender, EventArgs e)
        {
            var trackbar = (TrackBar)sender;
            TextBox textbox;
            if (trackbar == tkbToolR)
            {
                textbox = txtToolR;
            }
            else
            {
                textbox = trackbar == tkbToolG ? txtToolG : txtToolB;
            }
            textbox.Text = Convert.ToString(trackbar.Value);
        }

        private void HandleTextChanged(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            try
            {
                userTool.Color = Color.FromArgb(Convert.ToInt32(txtToolR.Text), Convert.ToInt32(txtToolG.Text), Convert.ToInt32(txtToolB.Text));
                tkbToolR.Value = Convert.ToInt32(txtToolR.Text);
                tkbToolG.Value = Convert.ToInt32(txtToolG.Text);
                tkbToolB.Value = Convert.ToInt32(txtToolB.Text);
            }
            catch
            {
                MessageBox.Show("Error Invalid Integer Entered", "Whiteboard");
            }
        }

        #endregion

        #region WhiteboardColor
        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WhiteboardColorHandler(true);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                WhiteboardColorHandler(false);
                cstpnlWhiteboard.BackColor = ColorTranslator.FromHtml(txtWhiteboardColor.Text);
                tools[(int)ToolType.Eraser].Color = cstpnlWhiteboard.BackColor;
            }
            catch
            {
                MessageBox.Show("Invalid Color Entered.", "Whiteboard");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            WhiteboardColorHandler(false);
        }

        private void WhiteboardColorHandler(bool isOpen)
        {
            cstpnlWhiteboardColor.Visible = isOpen;
            cstpnlWhiteboardColor.Enabled = isOpen;
            cstpnlWhiteboard.Enabled = !isOpen;
            cstpnlToolColor.Enabled = !isOpen;
            cstpnlTools.Enabled = !isOpen;

        }
        #endregion

        private void chkCustomToolColor_CheckedChanged(object sender, EventArgs e)
        {
            ToggleCustomColor(chkCustomToolColor.Checked);
            if (chkCustomToolColor.Checked)
            {
                if (selectedColorPic != null)
                {
                    selectedColorPic.RemoveBorder();
                }
                selectedColorPic = null;
                userTool.Color = Color.FromArgb(Convert.ToInt32(txtToolR.Text), Convert.ToInt32(txtToolG.Text), Convert.ToInt32(txtToolB.Text));
                return;
            }
            userTool.Color = Color.Black;
            cstpicToolBlack.AddBorder(Color.Gray);
            selectedColorPic = cstpicToolBlack;
        }

        private void ToggleCustomColor(bool isCustom)
        {
            foreach (Control c in cstpnlToolColor.Controls)
            {
                if (c is CustomPictureBox)
                {
                    c.Enabled = !isCustom;
                }
                else if (c is TrackBar)
                {
                    c.Enabled = isCustom;
                }
                else if (c is TextBox)
                {
                    c.Enabled = isCustom;
                }
            }
        }

        private void versionHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f3 = new Form3();
            f3.ShowDialog();
        }

        private void penThicknessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var help = new HelpForm("PenThickness");
            help.ShowDialog();
        }

        private void usingTheTriangleToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var help = new HelpForm("UsingTriangle");
            help.ShowDialog();
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            cstpnlInfo.Visible = false;
        }

        private void chkFilledShape_CheckedChanged(object sender, EventArgs e)
        {
            userTool.IsFilled = chkFilledShape.Checked;
        }

        private bool isCustomColor(Color c)
        {
            if (c == Color.Black || c == Color.Red || c == Color.Blue || c == Color.Green || c == Color.Yellow || c == Color.Purple)
            {
                return false;
            }
            return true;
        }        
    }
}