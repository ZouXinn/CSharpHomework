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
    public partial class FormEdit : Form
    {
        Order result = null;
        Order get = null;
        long ID;
        string Username;
        string Phone;
        List<OrderDetails> orderdetails = new List<OrderDetails>();
        FormMain fmain;
        public FormEdit()
        {
            InitializeComponent();
        }
        public FormEdit(Order order,FormMain f) : this()
        {
            get = order;
            orderBS.DataSource = get;
            fmain = f;
        }
        public FormEdit(Order order,FormMain f,int x) : this()
        {
            get = order;
            orderBS.DataSource = get;
            ID = order.ID;
            Username = order.Username;
            Phone = order.Phone;
            for(int i = 0; i < order.Details.Count; i++)
            {
                OrderDetails od = new OrderDetails();
                od.Name = order.Details[i].Name;
                od.Price = order.Details[i].Price;
                od.Account = order.Details[i].Account;
                orderdetails.Add(od);
            }
            fmain = f;
            textBox1.Text = order.ID.ToString();
            textBox2.Text = order.Username;
            textBox3.Text = order.Phone;
        }
        public Order getResult()
        {
            return result;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                result = get;
                result.ID = long.Parse(textBox1.Text);
                result.Username = textBox2.Text;
                result.Phone = textBox3.Text;
                if (!(fmain.isOk(result)))
                {
                    FormWarn warn = new FormWarn();
                    warn.ShowDialog();
                    result.ID = ID;
                    result.Username = Username;
                    result.Phone = Phone;
                    result.Details = orderdetails;
                    result = null;
                }
            }
            catch
            {
                result = null;
            }
            finally
            {

                this.Close();
            }
        }
    }
}
