using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWebMockup
{
    [Serializable]
    public class AnyText : AnyElement
    {
        //TODO use width and height
        private SizeF mSize;
        private string text;

        public AnyText() : base()
        {
        }
        public AnyText(int x, int y, string text) : base("Text", x, y, -1, -1)
        {
            this.text = text;
        }
        /// <summary>
        /// display text
        /// </summary>
        /// <param name="g"></param>
        public override void Display(Graphics g)
        {
            if (g != null && text != null)
            {
                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                mSize = g.MeasureString(text, drawFont);
                g.DrawString(text, new System.Drawing.Font("Arial", 16), drawBrush, x, y, new StringFormat());
            }
        }
        /// <summary>
        /// text position
        /// </summary>
        public override bool hitTest(int inputX, int inputY)
        {
            if (mSize != null)
            {
                return x <= inputX && inputX <= x + mSize.Width && y <= inputY && inputY <= y + mSize.Height;
            }
            else
            {
                return false;
            }
        }

        public override void drawOutline(Graphics g)
        {
            if (mSize != null)
            {
                drawOutline(g, x - 5, y - 5, Convert.ToInt32(mSize.Width) + 10, Convert.ToInt32(mSize.Height) + 10);
            }
        }

        public override ElementType getType()
        {
            return ElementType.Text;
        }
        /// <summary>
        /// Display text details
        /// </summary>
        public override string ToString()
        {
            return "Add [" + name + "] at " + PositionStr() + " , text = " + text;
        }

        public string elText
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }
    }
}
