using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard_Assignment.Classes
{
    class DrawingTool
    {
        private Pen tool;
        private string name;
        private bool isFilled;

        public Pen Tool
        {
            get { return tool; }
            set { tool = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsFilled
        {
            get { return isFilled; }
            set { isFilled = value; }
        }

        public Color Color
        {
            get { return tool.Color; }
            set { tool.Color = value; }
        }

        public DrawingTool()
        {
            tool = null;
            name = null;
            isFilled = false;
        }

        public DrawingTool(string name, Pen tool, bool isFilled)
        {
            this.tool = tool;
            this.name = name;
            this.isFilled = isFilled;
        }

        public void UpdateTool(string name, Pen tool, bool isFilled)
        {
            this.tool = tool;
            this.name = name;
            this.isFilled = isFilled;
        }
    }
}