using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private double binom(int n, int k, double p)
        {
            double b = 1;
            for (int i = 1; i <= n; i++)
                b = b * i;
                        
            for (int i = 1; i <= n - k; i++)
                b = b / i * (1 - p);
            
            for (int i = 1; i <= k; i++)
                b = b / i * p;
            
            return b;
        }

        private double max_binom(int n, double p)
        {
            double m = 0;

            for (int i = 0; i <= n; i++)
            {
                if (binom(n, i, p) > m)
                {
                    m = binom(n, i, p);
                }
            }

            return m;
        }

        private int max_i(int n, double p)
        {
            double m = 0;
            int k = 0;

            for (int i = 0; i <= n; i++)
            {
                if (binom(n, i, p) > m)
                {
                    m = binom(n, i, p);
                    k = i;
                }
            }

            return k;
        }

        private double Norm(double s, double m, double x)
        {
            double Norm = Math.Exp( - (x - m) * (x - m) / 2 / s / s) / s / 2.5066282763;
                       
            return Norm;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].CursorX.IsUserEnabled = false;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
            chart1.ChartAreas[0].CursorX.LineColor = Color.Transparent;
            chart1.ChartAreas[0].CursorY.LineColor = Color.Transparent;
            chart1.Legends.Clear();
            chart1.Series[0].BorderWidth = 3;

            int n = 2;
            double p = 0.3;

            for (int z = 0; z <= 48; z++)
            {
                
                chart1.Series[1].Points.Clear();
                chart1.Series[0].Points.Clear();
                n = n + 2;
                chart1.ChartAreas[0].Axes[0].Title = "n = " + n + " p = " + p;
                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(2, n-2);
                if (n > 20)
                {
                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(max_i(n, p) - 9, max_i(n, p) + 10);
                }
                double max = max_binom(n, p) * 1.05;
                chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, max);

                for (int i = 0; i <= n; i++)
                    chart1.Series[1].Points.AddXY(i, binom(n, i, p));
                
                for (int i = 0; i <= n; i++)
                    chart1.Series[0].Points.AddXY(i, Norm(Math.Sqrt(n * p * (1 - p)), n * p, i));
                
                this.chart1.SaveImage(@"d:\\gr" + n + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }


        }

        
    }
}
