using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Whiteboard_Assignment
{
    public partial class HelpForm : Form
    {
        private string topic;
        public HelpForm(string topic)
        {
            InitializeComponent();
            this.topic = topic;
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            lblPenThicknessHelp.Dock = DockStyle.Fill;
            lblTriangleHelp.Dock = DockStyle.Fill;
            if (topic == "PenThickness")
            {
                lblPenThicknessHelp.Visible = true;
            }
            else if (topic == "UsingTriangle")
            {
                lblTriangleHelp.Visible = true;
            }
        }

        private void HelpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lblPenThicknessHelp.Visible = false;
            lblTriangleHelp.Visible = false;
        }
    }
}
