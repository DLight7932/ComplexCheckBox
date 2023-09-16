using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ComplexCheckBox : UserControl
    {
        List<CheckBox> CheckBoxes = new List<CheckBox>();

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public List<CheckBox> CheckBoxItems => CheckBoxes;

        public ComplexCheckBox()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in CheckBoxes)
                checkBox.CheckedChanged -= CheckCheckBoxes;

            if (checkBox1.CheckState == CheckState.Checked || checkBox1.CheckState == CheckState.Indeterminate)
                foreach (CheckBox checkBox in CheckBoxes)
                    checkBox.Checked = true;

            else
                foreach (CheckBox checkBox in CheckBoxes)
                    checkBox.Checked = false;

            foreach (CheckBox checkBox in CheckBoxes)
                checkBox.CheckedChanged += CheckCheckBoxes;
        }

        private void ComplexCheckBox_Load(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            foreach (CheckBox checkBox in CheckBoxes)
            {
                checkBox.Dock = DockStyle.Top;
                checkBox.CheckedChanged += CheckCheckBoxes;
                groupBox1.Controls.Add(checkBox);
            }
            Invalidate();
            groupBox1.Invalidate();
            foreach (CheckBox checkBox in CheckBoxes)
                checkBox.Invalidate();
        }

        void CheckCheckBoxes(object sender, EventArgs e)
        {
            bool allTrue = true;
            bool allFalse = true;

            foreach (CheckBox checkBox in CheckBoxes)
                if (!checkBox.Checked)
                    allTrue = false;
                else
                    allFalse = false;

            checkBox1.CheckedChanged -= checkBox1_CheckedChanged;

            if (allTrue)
                checkBox1.CheckState = CheckState.Checked;
            else if (allFalse)
                checkBox1.CheckState = CheckState.Unchecked;
            else
                checkBox1.CheckState = CheckState.Indeterminate;

            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
        }
    }
}
