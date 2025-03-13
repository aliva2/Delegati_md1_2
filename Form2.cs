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
using Delegati_md1_2;

namespace Delegati_md1_2
{
    public partial class Form2 : Form
    {

        // Delegate to represent the function
        private DY selectedFunction;
        private double x_Begin, x_End, step, a, b, c;


        // Constructor accepting the selected function delegate
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
        }

        // Method to display the function graph
        public void ShowGraph(DY function)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Pen axisPen = new Pen(Color.Silver);
            int xc = pictureBox1.Width / 2, yc = pictureBox1.Height / 2;
            g.DrawLine(axisPen, 10, yc, 2 * xc - 10, yc); // X-axis
            g.DrawLine(axisPen, xc, 10, xc, 2 * yc - 10); // Y-axis

            Pen plotPen = new Pen(Color.Black);
            double x = x_Begin;
            int scale = 30; // Scaling factor for plotting
            while (x <= x_End)
            {
                try
                {
                    double y = function(x, a, b, c); // Get the y value using the function
                    int xe = (int)(xc + scale * x);
                    int ye = (int)(yc - scale * y);
                    g.DrawEllipse(plotPen, xe, ye, 2, 2); // Plot the point
                }
                catch { }
                x += step;
            }
        }

        // Form Load event to trigger graph drawing
        private void Form2_Load(object sender, EventArgs e)
        {
            ShowGraph(selectedFunction);
        }
    }
}
