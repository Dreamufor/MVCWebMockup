using System;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ElementController controller = new ElementController();
            ElementModel model = new ElementModel(controller);
            ((Form)controller.AddView(new Form3().setModel(model))).Show();
            ((Form)controller.AddView(new Form2().setModel(model))).Show();
            ((Form)controller.AddView(new Form1().setModel(model))).Show();
        }
    }
}
