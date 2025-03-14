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

            this.pictureBox1.Invalidate(); // ielādē formu 2 un uzzīmē grafiku

        }

        public delegate double fu(double x, double a, double b, double c);
        // grafika zīmēšana
        public void ShowFu(fu F, int m)
        {
            // veido grafiks objektu g un zīmēs to iekšā pictureBox
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White); //notīra visu, viss paliek balts
            Pen axisPen = new Pen(Color.Black); //asis būs melnā krāsā
            
            // centrs pa x un y asīm ir pa vidu pictureBox
            int xc = pictureBox1.Width / 2, yc = pictureBox1.Height / 2;
            // uzzīmē x un y asis
            g.DrawLine(axisPen, 10, yc, 2 * xc - 10, yc); 
            g.DrawLine(axisPen, xc, 10, xc, 2 * yc - 10);

            Pen plotPen = new Pen(Color.Black); // grafiks melnā krāsa
            double x = x_Begin; //sākuma koordināta x
            int scale = 30; // mērogošana?
            while (x <= x_End) // no sākuma līdz beigām zīmēs visus punktus
            {
                try
                {
                    double y = F(x, a, b, c);
                    int xe = (int)(xc + m * x);
                    int ye = (int)(yc - m * y);
                    g.DrawEllipse(plotPen, xe, ye, 1, 1);
                }
                catch { }
                x += step;
            }
        }


    }
}
