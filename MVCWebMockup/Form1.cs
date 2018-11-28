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
    public partial class Form1 : Form, IElementView
    {
        private ElementModel model;
        private FormHelper mFormHelper = new FormHelper();
        public Form1()
        {
            InitializeComponent();
            mFormHelper.setViews(radioText, radioButton, radioLine, radioImage, txtPosX, txtPosY, txtWidth, txtHeight, txtText, colorDialog, btnColor, checkBoxImage, comboBoxLine);
        }

        public Form1 setModel(ElementModel model)
        {
            this.model = model;
            return this;
        }

        public void RefreshView()
        {
            listBox.Items.Clear();
            foreach (AnyElement e in model.ElementList)
            {
                listBox.Items.Add(e);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ElementType type = mFormHelper.getType();
            if (mFormHelper.checkInput(type))
            {
                model.AddElement(mFormHelper.createNewElement());
                mFormHelper.clearInput();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                if (mFormHelper.updateElement(model.getElement(listBox.SelectedIndex)))
                {
                    mFormHelper.clearInput();
                    model.UpdateViews();
                }
            } else
            {
                MessageBox.Show("Should select an item first.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            model.DeleteShape(listBox.SelectedIndex);
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            mFormHelper.choseColor();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                mFormHelper.selectElement(model.getElement(listBox.SelectedIndex));
            }
        }

        private void radioText_CheckedChanged(object sender, EventArgs e)
        {
            mFormHelper.onItemTypeChange();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            mFormHelper.onItemTypeChange();
        }

        private void radioLine_CheckedChanged(object sender, EventArgs e)
        {
            mFormHelper.onItemTypeChange();
        }

        private void radioImage_CheckedChanged(object sender, EventArgs e)
        {
            mFormHelper.onItemTypeChange();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            mFormHelper.clearInput();
        }
        /// <summary>
        /// save element
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Elements";
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog.FileName != "")
            {
                Util.SerializeElements(model.ElementList, saveFileDialog.FileName);
            }
        }
        /// <summary>
        /// open element
        /// </summary>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Load Elements";
            fileDialog.Filter = "XML Files (*.xml)|*.xml";
            fileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (fileDialog.FileName != "")
            {
                ArrayList list = Util.DeSerializeElements(fileDialog.FileName);
                if (list != null)
                {
                    model.ElementList.Clear();
                
                    foreach (AnyElement ele in list)
                    {
                        model.ElementList.Add(ele);
                    }
                    model.UpdateViews();
                }
            }
        }
        /// <summary>
        /// exit form
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.ElementList.Clear();
            model.UpdateViews();
        }
    }
}
