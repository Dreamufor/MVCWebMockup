using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWebMockup
{
    [Serializable]
    public class AnyLine : AnyElement
    {
        private bool isSolid = true;
        public AnyLine() : base()
        {
        }
        public AnyLine(int x, int y, int width, bool solid) : base("Line", x, y, width, -1)
        {
            this.isSolid = solid;
        }
        /// <summary>
        /// display horizontal line
        /// </summary>
        /// <param name="g"></param>
        public override void Display(Graphics g)
        {
            if (g != null)
            {
                Brush br = new SolidBrush(Color.Black);
                Pen pen = new Pen(br);
                if (!isSolid)
                {
                    float[] dashValues = { 4, 2, 10, 2 };
                    pen.DashPattern = dashValues;
                }
                g.DrawLine(pen, x, y, x + width, y);
            }
        }
        /// <summary>
        /// Style limitation
        /// </summary>
        public override bool hitTest(int inputX, int inputY)
        {
            return x <= inputX && inputX <= x + width && y - 5 <= inputY && inputY <= y + 5;
        }

        public override void drawOutline(Graphics g)
        {
            drawOutline(g, x - 5, y - 5, width + 10, 10);
        }

        public override ElementType getType()
        {
            return ElementType.Line;
        }
        /// <summary>
        /// display line details
        /// </summary>
        public override string ToString()
        {
            return "Add [" + name + "] at " + PositionStr() + ", width = " + width + ", type = " + (isSolid ? "solid" : "dashed");
        }

        public bool elIsSolid 
        {
            get
            {
                return isSolid;
            }
            set
            {
                isSolid = value;
            }
        }
    }
}
