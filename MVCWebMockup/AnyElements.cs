using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVCWebMockup
{
    public enum ElementType { Button, Text, Line, Image, ALL };

    [Serializable]
    public abstract class AnyElement
    {
        protected string name = "unknown";
        protected int x = -1;
        protected int y = -1;
        protected int width = -1;
        protected int height = -1;

        public AnyElement() {
        }
        public AnyElement(string name, int x_at, int y_at, int shapeWidth,
           int shapeHeight)
        {
            this.name = name;
            x = x_at;
            y = y_at;
            width = shapeWidth;
            height = shapeHeight;
        }

        public AnyElement(string name, int x_at, int y_at)
        {
            this.name = name;
            x = x_at;
            y = y_at;
        }

        public abstract void Display(Graphics g); // abstract method

        public abstract ElementType getType();

        public string PositionStr()  //non abstract method
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }

        public string SizeStr()
        {
            return "[width: " + width.ToString() + ", height: " + height.ToString() + "]";
        }

        public String nameStr()
        {
            return "[" + name + "]";
        }

        public virtual bool hitTest(int inputX, int inputY)
        {
            return x <= inputX && inputX <= x + elWidth && y <= inputY && inputY <= y + elHeight;
        }

        public virtual void drawOutline(Graphics g)
        {
            drawOutline(g, x - 5, y - 5, elWidth + 10, elHeight + 10);
        }

        protected void drawOutline(Graphics g, int x, int y, int width, int height)
        {
            Brush br = new SolidBrush(Color.FromArgb(40, 00, 00, 00));
            Pen pen = new Pen(br);
            g.FillRectangle(br, x, y, width, height);
        }

        public virtual int elPosX //abstract property
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public virtual int elPosY//abstract property
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public virtual int elWidth //abstract property
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public virtual int elHeight //abstract property
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public virtual string elName //abstract property
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

    }
}
