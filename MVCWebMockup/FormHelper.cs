using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCWebMockup
{
    class FormHelper
    {
        RadioButton radioText;
        RadioButton radioButton;
        RadioButton radioLine;
        RadioButton radioImage;
        TextBox txtPosX;
        TextBox txtPosY;
        TextBox txtWidth;
        TextBox txtHeight;
        TextBox txtText;
        ColorDialog colorDialog;
        Button btnColor;
        CheckBox checkBoxImage;
        ComboBox comboBoxLine;

        public void setViews(RadioButton radioText, RadioButton radioButton,
            RadioButton radioLine,
            RadioButton radioImage,
            TextBox txtPosX,
            TextBox txtPosY,
            TextBox txtWidth,
            TextBox txtHeight,
            TextBox txtText,
            ColorDialog colorDialog,
            Button btnColor,
            CheckBox checkBoxImage,
            ComboBox comboBoxLine
            )
        {
            this.radioText = radioText;
            this.radioButton = radioButton;
            this.radioLine = radioLine;
            this.radioImage = radioImage;
            this.txtPosX = txtPosX;
            this.txtPosY = txtPosY;
            this.txtWidth = txtWidth;
            this.txtHeight = txtHeight;
            this.txtText = txtText;
            this.colorDialog = colorDialog;
            this.btnColor = btnColor;
            this.checkBoxImage = checkBoxImage;
            this.comboBoxLine = comboBoxLine;
            checkBoxImage.Checked = false;
            radioText.Checked = true;
            initComboBoxLine();
            onItemTypeChange();
        }


        private void initComboBoxLine()
        {
            comboBoxLine.Items.Insert(0, "Solid");
            comboBoxLine.Items.Insert(1, "Dashed");
            comboBoxLine.SelectedIndex = 0;
        }

        public void choseColor()
        {
            colorDialog.AllowFullOpen = false;
            colorDialog.ShowHelp = true;
            colorDialog.Color = btnColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = colorDialog.Color;
            }
        }
        /// <summary>
        /// Get type
        /// </summary>
        public ElementType getType()
        {
            if (radioText.Checked)
            {
                return ElementType.Text;
            } else if (radioButton.Checked)
            {
                return ElementType.Button;
            } else if (radioLine.Checked)
            {
                return ElementType.Line;
            } else if (radioImage.Checked)
            {
                return ElementType.Image;
            }

            // none is checked, so we check text here
            radioText.Checked = true;
            return ElementType.Text;
        }

        public Color getColor()
        {
            return btnColor.BackColor;
        }

        public int getPosX()
        {
            return Util.parseInt(txtPosX.Text, -1);
        }

        public int getPosY()
        {
            return Util.parseInt(txtPosY.Text, -1);
        }

        public int getWidth()
        {
            return Util.parseInt(txtWidth.Text, -1);
        }

        public int getHeight()
        {
            return Util.parseInt(txtHeight.Text, -1);
        }

        public string getText()
        {
            return txtText.Text;
        }

        public bool isLineSolid()
        {
            return comboBoxLine.SelectedIndex <= 0;
        }

        public bool isImageSquare()
        {
            return checkBoxImage.Checked;
        }
        /// <summary>
        /// Check input and display hint
        /// </summary>
        public bool checkInput(ElementType type)
        {
            if (getPosX() < 0)
            {
                MessageBox.Show("Input of posX is not supported.");
                return false;
            }

            if (getPosY() < 0)
            {
                MessageBox.Show("Input of posY is not supported.");
                return false;
            }


            if (getWidth() < 0)
            {
                if (type == ElementType.Button || type == ElementType.Image || type == ElementType.Line)
                {
                    MessageBox.Show("Input of width is not supported.");
                    return false;
                }
            }

            if (getHeight() < 0)
            {
                if (type == ElementType.Button || type == ElementType.Image)
                {
                    MessageBox.Show("Input of height is not supported.");
                    return false;
                }
            }

            if (type == ElementType.Text)
            {
                string text = getText();
                if (String.IsNullOrEmpty(text))
                {
                    MessageBox.Show("Input of text can not be empty.");
                    return false;
                }
                else if (text.Length > 10)
                {
                    MessageBox.Show("The max length of text is 10.");
                    return false;
                }
            }

            if (type == ElementType.Button || type == ElementType.Image)
            {
                if (getWidth() < 40 || getHeight() < 40)
                {
                    MessageBox.Show("Width and height can not be smaller than 40.");
                    return false;
                }
            }

            if (type == ElementType.Line)
            {
                if (getWidth() < 40)
                {
                    MessageBox.Show("Width can not be smaller than 40.");
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Create element
        /// </summary>
        public AnyElement createNewElement()
        {
            switch (getType())
            {
                case ElementType.Button:
                        return new AnyButton(getPosX(), getPosY(), getWidth(), getHeight(), getColor());
                case ElementType.Text:
                        return new AnyText(getPosX(), getPosY(), getText());
                case ElementType.Line:
                    return new AnyLine(getPosX(), getPosY(), getWidth(), isLineSolid());
                case ElementType.Image:
                    return new AnyImage(getPosX(), getPosY(), getWidth(), getHeight(), isImageSquare());
                default:
                    throw new System.NotSupportedException("Type " + getType().ToString() + " not supported");
            }
        }
        /// <summary>
        /// Clear input
        /// </summary>
        public void clearInput()
        {
            txtPosX.Text = "";
            txtPosY.Text = "";
            txtHeight.Text = "";
            txtWidth.Text = "";
            txtText.Text = "";
            btnColor.BackColor = Color.Black;
            comboBoxLine.SelectedIndex = 0;
            checkBoxImage.Checked = false;
        }
        /// <summary>
        /// Select element
        /// </summary>
        public void selectElement(AnyElement ele)
        {
            clearInput();

            txtPosX.Text = ele.elPosX.ToString();
            txtPosY.Text = ele.elPosY.ToString();
            if (ele.elWidth > 0)
            {
                txtWidth.Text = ele.elWidth.ToString();
            }
            if (ele.elHeight > 0)
            {
                txtHeight.Text = ele.elHeight.ToString();
            }
            
            switch(ele.getType())
            {
                case ElementType.Text:
                    {
                        radioText.Checked = true;
                        txtText.Text = ((AnyText)ele).elText;
                        break;
                    }
                case ElementType.Button:
                    {
                        radioButton.Checked = true;
                        btnColor.BackColor = ((AnyButton)ele).elColor;
                        break;
                    }
                case ElementType.Line:
                    {
                        radioLine.Checked = true;
                        comboBoxLine.SelectedIndex = ((AnyLine)ele).elIsSolid ? 0 : 1;
                        break;
                    }
                case ElementType.Image:
                    {
                        radioImage.Checked = true;
                        checkBoxImage.Checked = ((AnyImage)ele).elIsSquare;
                        break;
                    }
            }
        }
        /// <summary>
        /// Update element
        /// </summary>
        public bool updateElement(AnyElement ele)
        {
            if (!checkInput(ele.getType()))
            {
                return false;
            }

            ele.elPosX = getPosX();
            ele.elPosY = getPosY();
            ele.elWidth = getWidth();
            ele.elHeight = getHeight();

            switch (ele.getType())
            {
                case ElementType.Text:
                    {
                        ((AnyText)ele).elText = txtText.Text;
                        break;
                    }
                case ElementType.Button:
                    {
                        ((AnyButton)ele).elColor = btnColor.BackColor;
                        break;
                    }
                case ElementType.Line:
                    {
                        ((AnyLine)ele).elIsSolid = isLineSolid();
                        break;
                    }
                case ElementType.Image:
                    {
                        ((AnyImage)ele).elIsSquare = isImageSquare();
                        break;
                    }
            }
            return true;
        }
        /// <summary>
        /// Diable fields for specific element
        /// </summary>
        public void onItemTypeChange()
        {
            txtWidth.Enabled = true;
            txtHeight.Enabled = true;

            txtText.Enabled = false;
            btnColor.Enabled = false;
            checkBoxImage.Enabled = false;
            comboBoxLine.Enabled = false;

            switch (getType())
            {
                case ElementType.Text:
                    txtWidth.Enabled = false;
                    txtHeight.Enabled = false;
                    txtText.Enabled = true;
                    break;
                case ElementType.Button:
                    btnColor.Enabled = true;
                    break;
                case ElementType.Image:
                    checkBoxImage.Enabled = true;
                    break;
                case ElementType.Line:
                    comboBoxLine.Enabled = true;
                    break;
            }
        }
    }
}
