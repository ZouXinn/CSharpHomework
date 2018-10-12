using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace program2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Graphics graphics;
        double th1 = 30 * Math.PI / 180;//30du   right
        double th2 = 20 * Math.PI / 180;//20du   left
        double per1 = 0.6;//right
        double per2 = 0.7;//left
        double k = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            if (graphics == null) graphics = this.CreateGraphics();
            string sth1 = textBox1.Text;
            string sth2 = textBox2.Text;
            string sk = textBox3.Text;
            string sper2 = textBox4.Text;
            string sper1 = textBox5.Text;
            try
            {
                th1 = double.Parse(sth1) * Math.PI / 180; ;
                th2 = double.Parse(sth2) * Math.PI / 180; ;
                k = double.Parse(sk);
                per2 = double.Parse(sper2);
                per1 = double.Parse(sper1);
            }
            catch
            {
                Console.WriteLine("输入格式错误，应该输出浮点数或整数！");
                Application.Exit();
            }
            drawCayleyTree(10, 200, 310, 100, -Math.PI / 2);
        }
        void drawCayleyTree(int n ,double x0,double y0,double leng,double th)
        {
            if (n == 0) return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);
            double x2 = x0 + leng * k * Math.Cos(th);
            double y2 = y0 + leng * k * Math.Sin(th);
            drawLine(x0, y0, x1, y1);
            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);//right
            drawCayleyTree(n - 1, x2, y2, per2 * leng, th - th2);//left
        }
        void drawLine(double x0,double y0,double x1,double y1)
        {
            graphics.DrawLine(Pens.Blue, (int)x0, (int)y0, (int)x1, (int)y1);
        }     
    }
}
