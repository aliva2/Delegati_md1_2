using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Delegati_md1_2;

namespace Delegati_md1_2
{
    public partial class Form1 : Form
    {
        private double a=2, b=3, c=4; // koeficienti
        private double x_Begin=1, x_End=10, step=1; // tabulas parametri

        public Form1()
        {
            InitializeComponent();

            // inicializē koeficientus un piešķir sākotnējās vērtības
            textBox1.Text = "2";
            textBox2.Text = "3";
            textBox3.Text = "4";

            // inicializē tabulas parametru laukus
            textBox4.Text = "1";
            textBox5.Text = "10";
            textBox6.Text = "1";

            // piešķir tag vērtības radiopogām
            radioButton1.Tag = 0; // y = ax^2 + bx + c
            radioButton2.Tag = 1; // y = a/x^2 + b/x + c
            radioButton3.Tag = 2; // y = (ax + b)/(ax + c)
            radioButton4.Tag = 3; // y = ax^2 - bx +c

            // piesaista CheckedChanged apstrādātājus visām radiopogām
            radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton_CheckedChanged;
            radioButton4.CheckedChanged += RadioButton_CheckedChanged;

            // ieslēdz pirmo radiopogu kā noklusējuma izvēli
            radioButton1.Checked = true;

            foreach (var rb in new[] { radioButton1, radioButton2, radioButton3, radioButton4 })
                rb.CheckedChanged += RadioButton_CheckedChanged;
        }


        // radiopogas pārbaude vai noklikšķināta
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // ja izvēlēta radiopoga tad...
            if ((sender as RadioButton).Checked)
            {
                // dabū radiopogas Tag (līdzīgs id)
                int selectedOption = (int)(sender as RadioButton).Tag;

                // Get the current values from textboxes (to make sure the latest values are used)
                double x_Begin = double.Parse(textBox4.Text);
                double x_End = double.Parse(textBox5.Text);
                double step = double.Parse(textBox6.Text);
                double a = double.Parse(textBox1.Text);
                double b = double.Parse(textBox2.Text);
                double c = double.Parse(textBox3.Text);

                // zīmē tabulu ar DoTable
                DoTable(Y[selectedOption], x_Begin, x_End, step, a, b, c);
            }
        }

        //  funkciju saraksts radiopogām
        public static double Y0(double x, double a, double b, double c) => a * Math.Pow(x, 2) + b * x + c;
        public static double Y1(double x, double a, double b, double c) => x != 0 ? a / Math.Pow(x, 2) + b / x + c : double.NaN;
        public static double Y2(double x, double a, double b, double c) => (a * x + b) / (a * x + c);
        public static double Y3(double x, double a, double b, double c) => a * Math.Pow(x, 2) - b * x + c;


        // mūsu funkciju masīvs - var turpināt, pielikts vēl Y3,
        // satur funkcijas double DY(double x, double a, double b, double c)
        public DY[] Y = new DY[4] { Y0, Y1, Y2, Y3 };

        // tabulas "x un y(x)" konstruēšanas metode
        private void DoTable(DY y, double x_Begin, double x_End, double step, double a, double b, double c)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText("   x         y ");
            for (double x = x_Begin; x <= x_End; x += step) {
                double yValue = y(x, a, b, c);
                richTextBox1.AppendText("\n  " + x.ToString() + "\t" + y(x, a, b, c).ToString());
            }
                
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            int k = Convert.ToInt32(((RadioButton)sender).Tag);
            DoTable(Y[k], x_Begin, x_End, step, a, b, c);
        }

        // poga "Tabula" - parāda tabulu
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Parādīt tabulu!");
            // Read the values from the textboxes
            double x_Begin = double.Parse(textBox4.Text);  // x_Begin from textbox4
            double x_End = double.Parse(textBox5.Text);    // x_End from textbox5
            double step = double.Parse(textBox6.Text);     // step from textbox6
            double a = double.Parse(textBox1.Text);        // a from textbox1
            double b = double.Parse(textBox2.Text);        // b from textbox2
            double c = double.Parse(textBox3.Text);        // c from textbox3

            // Determine which function is selected based on the radio button
            int selectedOption = radioButton1.Checked ? 0 :
                                 radioButton2.Checked ? 1 :
                                 radioButton3.Checked ? 2 : 3;

            // Call the DoTable function to update the table with the selected function and the updated parameters
            DoTable(Y[selectedOption], x_Begin, x_End, step, a, b, c);
        }

        // poga "Grafiks" - parāda grafiku
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Parādīt grafiku!");
            //make code go to form 2 and do delegate fu and showfu
            int selectedOption = radioButton1.Checked ? 0 : radioButton2.Checked ? 1 : radioButton3.Checked ? 2 : 3;

            // Parse the values from text boxes for x_Begin, x_End, step, a, b, c
            double x_Begin = double.Parse(textBox4.Text);
            double x_End = double.Parse(textBox5.Text);
            double step = double.Parse(textBox6.Text);
            double a = double.Parse(textBox1.Text);
            double b = double.Parse(textBox2.Text);
            double c = double.Parse(textBox3.Text);

            Form2 graphForm = new Form2(Y[selectedOption], x_Begin, x_End, step, a, b, c);
            graphForm.Show();
        }


 
 
    }
}
