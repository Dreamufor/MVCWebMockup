using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWebMockup
{
    [Serializable]
    public class AnyImage : AnyElement
    {
        private bool isSquare;

        public AnyImage() : base()
        {
        }
        public AnyImage(int x, int y, int width, int height, bool isSquare) : base("Image", x, y, width, height)
        {
            this.isSquare = isSquare;
        }
        /// <summary>
        /// display image
        /// </summary>
        /// <param name="g"></param>
        public override void Display(Graphics g)
        {
            if (g != null)
            {
                Brush br = new SolidBrush(Color.Red);
                Pen pen = new Pen(br);

                int l_w = width;
                int l_h = height;
                if (isSquare)
                {
                    l_h = l_w = getSize();
                }
                g.DrawLine(pen, x, y, x + l_w, y);
                g.DrawLine(pen, x + l_w, y, x + l_w, y + l_h);
                g.DrawLine(pen, x + l_w, y + l_h, x, y + l_h);
                g.DrawLine(pen, x, y + l_h, x, y);

                g.DrawLine(pen, x, y, x + l_w, y + l_h);
                g.DrawLine(pen, x, y + l_h, x + l_w, y);

                Font drawFont = new Font("Arial", 16);

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                g.DrawString("Image", new System.Drawing.Font("Arial", Math.Min(l_h / 2, 13)), br, x + l_w / 2, y + l_h / 2, format);
            }
        }

        public override ElementType getType()
        {
            return ElementType.Image;
        }
        /// <summary>
        /// display image details
        /// </summary>
        public override string ToString()
        {
            return "Add [" + name + "] at " + PositionStr() + ", size = " + SizeStr();
        }

        public bool elIsSquare
        {
            get
            {
                return isSquare;
            }
            set
            {
                isSquare = value;
            }
        }


        public override int elWidth //abstract property
        {
            get
            {
                return isSquare ? getSize() : width;
            }
            set
            {
                width = value;
            }
        }

        public override int elHeight //abstract property
        {
            get
            {
                return isSquare ? getSize() : height;
            }
            set
            {
                height = value;
            }
        }

        private int getSize()
        {
            return width > height ? width : height;
        }
    }
}
