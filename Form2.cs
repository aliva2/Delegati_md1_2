using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Delegati_md1_2; // kopīgi Form 1 un Form 2,public delegate double DY(double x, double a, double b, double c); 

namespace Delegati_md1_2
{
    public partial class Form2 : Form
    {

        private DY selectedFunction; // delegāts - izvēlātā funkcija
        private double x_Begin, x_End, step, a, b, c; // koeficienti

        // konstruktors no delegāta
        public Form2(DY func, double x_Begin, double x_End, double step, double a, double b, double c)
        {
            InitializeComponent();
            this.selectedFunction = func;
            this.x_Begin = x_Begin;
            this.x_End = x_End;
            this.step = step;
            this.a = a;
            this.b = b;
            this.c = c;
            // kas par funkc
            //Console.WriteLine($"Selected Function: {selectedFunction != null}");

            this.pictureBox1.Paint += PictureBox1_Paint; // zīmēt grafiku
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // parāda grafiku, mērogošana nelieliem koeficientiem
            ShowFu(selectedFunction, 25, e.Graphics); 
        }

        public delegate double fu(double x, double a, double b, double c);
        // grafika zīmēšana
        public void ShowFu(DY F, int m, Graphics g)
        {
            // veido grafiks objektu g un zīmēs to iekšā pictureBox
            //Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White); //notīra visu, viss paliek balts
            Pen axisPen = new Pen(Color.Silver); //asis būs melnā krāsā

            // centrs pa x un y asīm ir pa vidu pictureBox
            int xc = pictureBox1.Width / 2, yc = pictureBox1.Height / 2;
            // uzzīmē x un y asis
            g.DrawLine(axisPen, 0, yc, pictureBox1.Width, yc); // izstiepj asis pa visu platumu
            g.DrawLine(axisPen, xc, 0, xc, pictureBox1.Height); // izstiepj asis pa visu augstumu

            Pen plotPen = new Pen(Color.Black); // grafiks melnā krāsa
            double x = x_Begin; //sākuma koordināta x
            int scale = m; // mērogošana

            while (x <= x_End) // no sākuma līdz beigām zīmēs visus punktus lai uzzīmētu funkc, ja mērogs par lielu dažus neredz
            {
                try
                {
                    double y = F(x, a, b, c);
                    //ko tad atgriež funkcija
                    //Console.WriteLine($"x: {x}, y: {y}");

                    if (double.IsNaN(y) || double.IsInfinity(y))
                        continue; // nederigos punktus nedara

                    int xe = (int)(xc + scale * x);
                    int ye = (int)(yc - scale * y);

                    // pārbauda, vai punkts atrodas pictureBox robežās
                    if (xe >= 0 && xe < pictureBox1.Width && ye >= 0 && ye < pictureBox1.Height)
                    {
                        g.FillRectangle(Brushes.MediumPurple, xe, ye, 2, 2); // uzzīmē mazu punktu grafikam
                    }
                }
                catch (Exception ex)
                {
                    // ja kļūdas tad konsolē ieraksta ziņu
                    Console.WriteLine($"Error processing x = {x}: {ex.Message}");
                }
                x += step; // nākamais solis
            }
        }
    }
}
