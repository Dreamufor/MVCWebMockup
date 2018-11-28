using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCWebMockup
{
    public partial class Form3 : Form, IElementView
    {
        private ElementModel model;
        private Graphics g;
        public Form3()
        {
            InitializeComponent();
            comboBox.Items.Insert(0, "Show all elements");
            comboBox.Items.Insert(1, "Show Text only");
            comboBox.Items.Insert(2, "Show Button only");
            comboBox.Items.Insert(3, "Show Line only");
            comboBox.Items.Insert(4, "Show Image only");
            comboBox.SelectedIndex = 0;
            g = this.panel.CreateGraphics();
        }

        public Form3 setModel(ElementModel model)
        {
            this.model = model;
            return this;
        }

        public void RefreshView()
        {
            clearPanel();
            ElementType allowType = getAllowedType(comboBox.SelectedIndex);
            foreach (AnyElement sh in model.ElementList)
            {
                if (allowType == ElementType.ALL || allowType == sh.getType())
                {
                    sh.Display(g);
                }
            }
        }

        private ElementType getAllowedType(int index)
        {
            switch(index)
            {
                case 1:
                    return ElementType.Text;
                case 2:
                    return ElementType.Button;
                case 3:
                    return ElementType.Line;
                case 4:
                    return ElementType.Image;
                default:
                    return ElementType.ALL;
            }
        }

        private void clearPanel()
        {
            g.Clear(panel.BackColor);
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (model != null)
            {
                model.UpdateViews();
            }
        }
    }
}
