using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace program1
{
    class MyException : Exception
    {
        private string id;
        public MyException(string ex)
        {
            id = ex;
        }
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
    }
    [Serializable]
    public class Order
    {
        //private int ID;
        //private string username;
        public List<OrderDetails> details;
        public int ID { get; set; }
        public string Username { get; set; }
        public Order()
        {
            this.ID = 0;
            this.Username = "";
            details = new List<OrderDetails>();
        }
        public Order(int ID, string username)
        {
            this.ID = ID;
            this.Username = username;
            details = new List<OrderDetails>();
        }
        //public override string ToString()
        //{
        //    string ret = "";
        //    ret = ret+("订单号:" + getID()+"/n");
        //    ret = ret + ("客户名:" + getUsername() + "/n");
        //    for(int i = 0; i<details.Count-1;i++)
        //    {
        //        ret = ret + ("商品名:" + details[i].getName() + " ");
        //        ret = ret + ("单价:" + details[i].getPrice() + " ");
        //        ret = ret + ("数量:" + details[i].getAccount() + "/n");
        //    }
        //    return ret;
        //}
        public int addUp()
        {
            int ret = 0;
            for (int i = 0; i < details.Count; i++)
            {
                ret += details[i].getPrice() * details[i].getAccount();
            }
            return ret;
        }
        public void addDetail(OrderDetails od)
        {
            details.Add(od);
        }
        public void delDetail(string name)
        {
            try
            {
                bool done = false;
                for (int i = details.Count - 1; i >= 0; i--)
                {
                    if (name == details[i].getName())
                    {
                        details.RemoveAt(i);
                        done = true;
                    }
                }
                if (!done)
                {
                    throw new MyException("没有名为" + name + "的商品");
                }
            }
            catch (MyException e)
            {
                Console.Write(e.Id);
            }
        }
        public int getID()
        {
            return ID;
        }
        public string getUsername()
        {
            return Username;
        }
    }
    public class OrderDetails
    {
        //private string name;
        //private int price;
        //private int account;
        public string Name { set; get; }
        public int Price { set; get; }
        public int Account { set; get; }
        public OrderDetails(string name, int price, int account)
        {
            this.Name = name;
            this.Price = price;
            this.Account = account;
        }
        public OrderDetails()
        {
            this.Name = "";
            this.Price = 0;
            this.Account = 0;
        }
        public int getAccount()
        {
            return Account;
        }
        public void setName(string name)
        {
            this.Name = name;
        }
        public void setAccount(int account)
        {
            this.Account = account;
        }
        public void setPrice(int price)
        {
            this.Price = price;
        }
        public int getPrice()
        {
            return Price;
        }
        public string getName()
        {
            return Name;
        }

    }
    public class OrderService
    {
        public List<Order> orders = null;
        public OrderService()
        {
            orders = new List<Order>();
        }
        public void addOrder(Order order)
        {
            orders.Add(order);
        }
        public void delOrder(int ID)
        {
            try
            {
                bool done = false;
                for (int i = orders.Count - 1; i >= 0; i--)
                {
                    if (orders[i].getID() == ID)
                    {
                        orders.RemoveAt(i);
                        done = true;
                        break;
                    }
                }
                if (!done)
                {
                    throw new MyException("没有ID为" + ID + "的订单");
                }
            }
            catch (MyException e)
            {
                Console.Write(e.Id);
            }
        }
        public List<Order> checkOrder(int id)
        {
            var m = from o in orders where o.getID() == id select o;
            return m.ToList();
        }
        public List<Order> checkOrder(string username)
        {
            var m = from o in orders where o.getUsername() == username select o;
            return m.ToList();
        }
        public bool Check(Order o, string s)
        {
            bool ok = false;
            for (int i = 0; i < o.details.Count; i++)
            {
                if (o.details[i].getName() == s)
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }
        public List<Order> checkOrder(string name, int k)
        {
            var m = from o in orders where Check(o, name) select o;
            return m.ToList();
        }
        public void Export(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(xmlFileName, FileMode.Create))
            {
                xmlser.Serialize(fs, orders);
            }
        }

        public List<Order> Import(string xmlFileName)
        {
            List<Order> ret = null;
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(xmlFileName, FileMode.Open))
            {
                ret = (List<Order>)xmlser.Deserialize(fs);
            }
            return ret;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            OrderService os = new OrderService();
            for (int i = 0; i < 10; i++)
            {
                Order order = new Order(i, "A" + i.ToString());
                for (int j = 0; j < 3; j++)
                {
                    order.details.Add(new OrderDetails("B" + j.ToString(), 3 * j, j));
                }
                os.orders.Add(order);
            }
            Order order1 = new Order(1222, "王有钱");
            order1.details.Add(new OrderDetails("ex", 5000, 3));
            os.orders.Add(order1);
            var expensive = from o in os.orders where o.addUp() > 10000 select o;
            foreach (Order o in expensive)
            {
                Console.WriteLine("价格超过一万元的订单如下:");
                Console.WriteLine("订单号:" + o.getID());
                Console.WriteLine("客户名:" + o.getUsername());
                for (int i = 0; i < o.details.Count; i++)
                {
                    Console.WriteLine("商品:" + o.details[i].getName() + " 单价:" + o.details[i].getPrice()
                        + " 数量:" + o.details[i].getAccount());
                }
            }
            os.Export("orders.xml");
        }
    }
}
