using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using program1;

namespace WinForm
{
    public partial class Form1 : Form
    {
        OrderService os = new OrderService();
        List<Order> ol = new List<Order>();
        Order on = new Order(0, null);
        Order o1 = new Order(1311, "aaaa");
        Order o2 = new Order(1111, "asdas");
        OrderDetails od1 = new OrderDetails("haha", 21, 2);
        OrderDetails od2 = new OrderDetails("haha2", 21, 2);
        OrderDetails od3 = new OrderDetails("haha3", 21, 2);
        OrderDetails od4 = new OrderDetails("haha4", 21, 2);
        bool checking = false;
        public Form1()
        {
            o1.addDetail(od1);
            o1.addDetail(od2);
            o1.addDetail(od3);
            o2.addDetail(od4);
            os.addOrder(o1);
            os.addOrder(o2);
            InitializeComponent();
            button6.Visible = false;
            bindingSource1.DataSource = os.orders;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Order to = new Order(int.Parse(textBox1.Text), textBox2.Text);
                os.addOrder(to);
                textBox1.Text = null;
                textBox2.Text = null;
                bindingSource1.DataSource = ol;
                bindingSource1.DataSource = os.orders;
            }catch
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            os.orders.RemoveAt(i);
            bindingSource1.DataSource = ol;
            bindingSource1.DataSource = os.orders;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OrderDetails tod = new OrderDetails(textBox3.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text));
                int i = dataGridView1.CurrentRow.Index;
                os.orders[i].addDetail(tod);
                textBox3.Text = null;
                textBox4.Text = null;
                textBox5.Text = null;
                bindingSource1.DataSource = ol;
                bindingSource1.DataSource = os.orders;
            }
            catch
            {
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            int j = dataGridView2.CurrentRow.Index;
            os.orders[i].Details.RemoveAt(j);
            bindingSource1.DataSource = ol;
            bindingSource1.DataSource = os.orders;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int n = 0;
                int tar = int.Parse(textBox6.Text);
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                checking = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button6.Visible = true;
                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
                bindingSource1.DataSource = os.orders;
                for(int i = 0; i < os.orders.Count;i++)
                {
                    if (os.orders[i].ID == tar) n++;
                }
                if (n > 0)
                    bindingSource1.DataSource = os.orders.Where(o => o.ID == tar);
                else
                    bindingSource1.DataSource = on;
            }            
            catch
            {
               return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!checking) return;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            checking = false;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button6.Visible = false;
            dataGridView1.ReadOnly = false;
            dataGridView2.ReadOnly = false;
            textBox6.Text = null;
            dataGridView2.Update();
            bindingSource1.DataSource = os.orders;
        }
    }
}
