using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delegati_md1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Piešķiram Tag vērtības radiopogām
            radioButton1.Tag = 0; // y = x
            radioButton2.Tag = 1; // y = x^2
            radioButton3.Tag = 2; // y = x^3
            radioButton4.Tag = 3; // y = sqrt(x)

            // Visām radiopogām piesaistām vienu CheckedChanged apstrādātāju
            radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton_CheckedChanged;
            radioButton4.CheckedChanged += RadioButton_CheckedChanged;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                int functionType = (int)rb.Tag;
                MessageBox.Show($"Izvēlēta funkcija: {functionType}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Poga 1 tika nospiesta!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Poga 2 tika nospiesta!");
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
}
