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
        int _a, _b;
        private void button1_Click(object sender, EventArgs e)
        {
            //当textBox1和textBox2的文本内容不是数字的时候怎么办？
            _a = Int32.Parse(textBox1.Text);
            _b = Int32.Parse(textBox2.Text);
            label2.Text = (_a * _b).ToString();
        }
        public Form1()
        {
            InitializeComponent();
        }
    }
}
