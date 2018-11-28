using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

//TODO use regex to restrict input
namespace MVCWebMockup
{
    public partial class Form2 : Form, IElementView
    {
        private ElementModel model;
        private FormHelper mFormHelper = new FormHelper();
        private bool mStopUpdate = false;
        private int mBgIndex = 1;
        private Graphics g;
        public Form2()
        {
            InitializeComponent();
            mFormHelper.setViews(radioText, radioButton, radioLine, radioImage, txtPosX, txtPosY, txtWidth, txtHeight, txtText, colorDialog, btnColor, checkBoxImage, comboBoxLine);
            g = this.canvasPanel.CreateGraphics();
            Thread thread = new Thread(updateBackground);
            thread.IsBackground = true;
            thread.Start();
        }
        /// <summary>
        /// change background 
        /// </summary>
        private void updateBackground()
        {
            while (!mStopUpdate)
            {
                if (IsHandleCreated)
                {
                    Invoke(new MethodInvoker(RefreshView));
                }
                Thread.Sleep(5000);
                mBgIndex++;
                if (mBgIndex == 4)
                {
                    mBgIndex = 1;
                }
            }
        }

        public Form2 setModel(ElementModel model) {
            this.model = model;
            return this;
        }
        /// <summary>
        /// set background
        /// </summary>
        public void RefreshView()
        {
            //clearPanel();

            Image imag = Image.FromFile(@"..\\..\\images\\background" + mBgIndex + ".jpg");
            g.DrawImage(imag, new Point(0, 0));

            if (model != null)
            {
                foreach (AnyElement sh in model.ElementList)
                {
                    if (mMoveElement == sh)
                    {
                        mMoveElement.drawOutline(g);
                    }
                    sh.Display(g);
                }
            }
            Invalidate();
        }

        private void clearPanel()
        {
            g.Clear(canvasPanel.BackColor);
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

        private AnyElement mMoveElement;
        private AnyElement mUpdateElement;
        private int[] mMouseDownLoc = new int[2];
        private int[] mEleOriginLoc = new int[2];

        private void canvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mMoveElement = null;
            mUpdateElement = null;
            mMouseDownLoc[0] = e.Location.X;
            mMouseDownLoc[1] = e.Location.Y;

            foreach (AnyElement ele in model.ElementList) {
                if (ele.hitTest(e.Location.X, e.Location.Y))
                {
                    mEleOriginLoc[0] = ele.elPosX;
                    mEleOriginLoc[1] = ele.elPosY;
                    mMoveElement = ele;
                    model.UpdateViews();
                    break;
                }
            }
        }
        private void canvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (mMoveElement != null)
            {
                mUpdateElement = mMoveElement;
                mFormHelper.selectElement(mMoveElement);
            }
            mMoveElement = null;
            model.UpdateViews();
        }
        /// <summary>
        /// drag element
        /// </summary>
        private void canvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mMoveElement != null)
            {
                mMoveElement.elPosY = mEleOriginLoc[1] + (e.Location.Y - mMouseDownLoc[1]);
                mMoveElement.elPosX = mEleOriginLoc[0] + (e.Location.X - mMouseDownLoc[0]);
                model.UpdateViews();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (mUpdateElement != null)
            {
                if (mFormHelper.updateElement(mUpdateElement))
                {
                    mFormHelper.clearInput();
                    model.UpdateViews();
                }
            }
            else
            {
                MessageBox.Show("Please select an item first.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (mUpdateElement != null)
            {
                model.DeleteShape(mUpdateElement);
                mUpdateElement = null;
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            mFormHelper.choseColor();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            mFormHelper.clearInput();
        }
    }
}
