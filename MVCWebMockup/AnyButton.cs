using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVCWebMockup
{
    [Serializable]
    public class AnyButton : AnyElement
    {
        private Color color = Color.Black;

        public AnyButton() : base()
        {
        }
        public AnyButton(int x_at, int y_at, int shape_width,
            int shape_height, Color color) : base("Button", x_at, y_at, shape_width, shape_height)
        {
            this.color = color;
        }
        /// <summary>
        /// display buttons
        /// </summary>
        /// <param name="g"></param>
        public override void Display(Graphics g)
        {
            if (g != null)
            {
                Brush br = new SolidBrush(color);
                Pen pen = new Pen(br);
                g.DrawPath(pen, RoundedRect(new Rectangle(x, y, width, height), 5));

                Font drawFont = new Font("Arial", Math.Min(height / 2, 14));
                SolidBrush drawBrush = new SolidBrush(color);
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                g.DrawString(name, drawFont, drawBrush, x + width / 2, y + height / 2, format);
            }
        }

        public override ElementType getType()
        {
            return ElementType.Button;
        }
        /// <summary>
        /// Button style
        /// </summary>
        public GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        /// <summary>
        /// Display button details
        /// </summary>
        public override string ToString()
        {
            return "Add [" + name + "] at " + PositionStr() + ", size = " + SizeStr() + ", color = " + color.ToString(); 
        }


        public String ColorStr()
        {
            return "color = " + color.ToString();
        }

        [XmlIgnore]
        public virtual Color elColor //abstract property
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        [XmlElement("color")]
        public int BackColorAsArgb
        {
            get { return color.ToArgb(); }
            set { color = Color.FromArgb(value); }
        }
    }

}
