using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using program1;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinForm
{
    public partial class FormMain : Form
    {
        OrderService os = new OrderService();
        List<Order> temp = new List<Order>();
        string idPattern = "^20[0-9]{2}[0-1][0-9][0-3][0-9][0-9]{3}$";
        string phonePattern = "^1[0-9]{10}$";
        Regex idRx, phoneRx;
        public FormMain()
        {            
            InitializeComponent();
            init();
        }
        public void init()
        {
            idRx = new Regex(idPattern);
            phoneRx = new Regex(phonePattern);
            Order o1 = new Order(20170101000, "wang", "13033333333");
            OrderDetails od1 = new OrderDetails("a", 1, 2);
            OrderDetails od2 = new OrderDetails("b", 2, 2);
            OrderDetails od3 = new OrderDetails("c", 5, 3);
            o1.addDetail(od1);
            o1.addDetail(od2);
            o1.addDetail(od3);
            os.addOrder(o1);
            Order o2 = new Order(20170101001, "li", "13502226555");
            OrderDetails od4 = new OrderDetails("a", 1, 2);
            OrderDetails od5 = new OrderDetails("c", 5, 3);
            o2.addDetail(od4);
            o2.addDetail(od5);
            os.addOrder(o2);
            Order o3 = new Order(20170101002, "zhang", "15023252222");
            OrderDetails od6 = new OrderDetails("b", 2, 10);
            o3.addDetail(od6);
            os.addOrder(o3);
            comboBox1.SelectedIndex = 4;
            orderBS.DataSource = os.orders;
        }
        public bool isOk(Order order)
        {
            if (!(idRx.IsMatch(order.ID.ToString()) && phoneRx.IsMatch(order.Phone)))
            {
                return false;
            }else
            {
                for(int i = 0; i< os.orders.Count; i++)
                {
                    if (order.ID == os.orders[i].ID)
                        return false;
                }
            }
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormEdit formEdit = new FormEdit(new Order(),this);
            formEdit.ShowDialog();
            Order newOrder = formEdit.getResult();
            if(newOrder != null)
            {
                os.addOrder(newOrder);
                orderBS.DataSource = temp;
                orderBS.DataSource = os.orders;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            FormEdit formEdit = new FormEdit(os.orders[i],this,1);
            formEdit.ShowDialog();
            orderBS.DataSource = temp;
            orderBS.DataSource = os.orders;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            os.orders.RemoveAt(i);
            orderBS.DataSource = temp;
            orderBS.DataSource = os.orders;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String fileName = saveFileDialog1.FileName;
                os.Export(fileName);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String fileName = openFileDialog1.FileName;
                os.orders=os.Import(fileName);
                orderBS.DataSource = os.orders;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 4:
                    orderBS.DataSource = temp;
                    orderBS.DataSource = os.orders;
                    break;
                case 0:
                    try
                    {
                        long tar = long.Parse(textBox1.Text);
                        int n = 0;
                        for (int i = 0; i < os.orders.Count; i++)
                        {
                            if (os.orders[i].ID == tar) n++;
                        }
                        if (n > 0)
                            orderBS.DataSource = os.orders.Where(o => o.ID == tar);
                        else
                            orderBS.DataSource = temp;
                    }
                    catch
                    {
                        return;
                    }
                    break;
                case 3:
                    try
                    {
                        string username = textBox1.Text;
                        int n = 0;                       
                        for (int i = 0; i < os.orders.Count; i++)
                        {
                            if (os.orders[i].Username == username) n++;
                        }
                        if (n > 0)
                            orderBS.DataSource = os.orders.Where(o => o.Username == username);
                        else
                            orderBS.DataSource = temp;
                    }
                    catch
                    {
                        return;
                    }
                    break;
                case 1:
                    try
                    {
                        string detailname = textBox1.Text;
                        int n = 0;
                        for (int i = 0; i < os.orders.Count; i++)
                        {
                            if (os.orders[i].hasDetail(detailname)) n++;
                        }
                        if (n > 0)
                            orderBS.DataSource = os.orders.Where(o => o.hasDetail(detailname));
                        else
                            orderBS.DataSource = temp;
                    }
                    catch
                    {
                        return;
                    }
                    break;
                case 2:
                    try
                    {
                        int account = int.Parse(textBox1.Text);
                        int n = 0;
                        for (int i = 0; i < os.orders.Count; i++)
                        {
                            if (os.orders[i].addUp()> account) n++;
                        }
                        if (n > 0)
                            orderBS.DataSource = os.orders.Where(o => o.addUp() > account);
                        else
                            orderBS.DataSource = temp;
                    }
                    catch
                    {
                        return;
                    }
                    break;
            }
        }

        private void btnExHtml_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog2.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String htmlFileName = saveFileDialog2.FileName;
                String fileName = htmlFileName.Replace(".html", ".xml");
                os.Export(fileName);
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);

                XPathNavigator nav = doc.CreateNavigator();
                nav.MoveToRoot();

                XslCompiledTransform xt = new XslCompiledTransform();
                xt.Load(@"..\..\b.xslt");

                FileStream outFileStream = File.OpenWrite(htmlFileName);
                XmlTextWriter writer =
                    new XmlTextWriter(outFileStream, System.Text.Encoding.UTF8);
                xt.Transform(nav, null, writer);
            }
        }
        private List<Order> getRepeat(Order o)
        {
            List<Order> ret = new List<Order>();
            for (int i = 0; i < os.orders.Count; i++)
            {
                for(int j = 0; j < os.orders.Count;j++)
                {
                    if (i == j) break;
                    else if(os.orders[i].ID == os.orders[j].ID)
                    {
                        Order o1 = os.orders[i].copy();
                        Order o2 = os.orders[j].copy();
                        ret.Add(o1);
                        ret.Add(o2);
                    }
                }
            }
            return ret;
        }
    }
}
