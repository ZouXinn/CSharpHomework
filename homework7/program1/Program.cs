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
        public List<OrderDetails> Details { get; set; }
        public long ID { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public Order()
        {
            ID = 0;
            Username = "";
            Phone = "0000000000000";
            Details = new List<OrderDetails>();
        }
        public Order(long ID, string username,string phone)
        {
            this.ID = ID;
            this.Username = username;
            this.Phone = phone;
            Details = new List<OrderDetails>();
        }
        public Order copy()
        {
            Order ret = new Order();
            ret.ID = ID;
            ret.Username = Username;
            ret.Phone = Phone;
            for(int i = 0; i < Details.Count; i++)
            {
                OrderDetails od = new OrderDetails();
                od.Name = Details[i].Name;
                od.Price = Details[i].Price;
                od.Account = Details[i].Account;
                ret.addDetail(od);
            }
            return ret;
        }
        public int addUp()
        {
            int ret = 0;
            for (int i = 0; i < Details.Count; i++)
            {
                ret += Details[i].Price * Details[i].Account;
            }
            return ret;
        }
        public void addDetail(OrderDetails od)
        {
            Details.Add(od);
        }
        public bool hasDetail(string detailname)
        {
            bool ret = false;
            for(int i = 0; i < Details.Count; i++)
            {
                if(Details[i].Name == detailname)
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }
        public void delDetail(string name)
        {
            try
            {
                bool done = false;
                for (int i = Details.Count - 1; i >= 0; i--)
                {
                    if (name == Details[i].Name)
                    {
                        Details.RemoveAt(i);
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
    }
    public class OrderDetails
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Account { get; set; }
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
    }
    //[Serializable]
    public class OrderService
    {
        //public List<Order> orders;
        public List<Order> orders { get; set; }
        public OrderService()
        {
            orders = new List<Order>();
        }
        public void addOrder(Order order)
        {
            orders.Add(order);
        }
        public void delOrder(long ID)
        {
            try
            {
                bool done = false;
                for (int i = orders.Count - 1; i >= 0; i--)
                {
                    if (orders[i].ID == ID)
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
        public void changeOrder(long ID, string name1, string name2, int account, int price)
        {
            try
            {
                bool done = false;
                for (int i = orders.Count - 1; i >= 0; i--)
                {
                    if (orders[i].ID == ID)
                    {
                        for (int j = 0; j < orders[i].Details.Count; j++)
                        {
                            if (orders[i].Details[j].Name == name1)
                            {
                                orders[i].Details[j].Name=name2;
                                orders[i].Details[j].Price=price;
                                orders[i].Details[j].Account=account;
                                done = true;
                            }
                        }
                    }
                }
                if (!done)
                {
                    throw new MyException("没有ID为" + ID + "的有" + name1 + "的订单");
                }
            }
            catch (MyException e)
            {
                Console.Write(e.Id);
            }
        }
        public void checkOrder(long id)
        {
            bool done = false;
            var m = from o in orders where o.ID == id select o;
            foreach (Order o in m)
            {
                done = true;
                Console.WriteLine("订单号:" + o.ID);
                Console.WriteLine("客户名:" + o.Username);
                for (int i = 0; i < o.Details.Count; i++)
                {
                    Console.WriteLine("商品:" + o.Details[i].Name + " 单价:" + o.Details[i].Price
                        + " 数量:" + o.Details[i].Account);
                }
            }
            try
            {
                if (!done)
                    throw new MyException("没有ID为" + id + "的订单");
            }
            catch (MyException e)
            {
                Console.WriteLine(e.Id);
            }
        }
        public void checkOrder(string username)
        {
            bool done = false;
            var m = from o in orders where o.Username== username select o;
            foreach (Order o in m)
            {
                done = true;
                Console.WriteLine("订单号:" + o.ID);
                Console.WriteLine("客户名:" + o.Username);
                for (int i = 0; i < o.Details.Count; i++)
                {
                    Console.WriteLine("商品:" + o.Details[i].Name + " 单价:" + o.Details[i].Price
                        + " 数量:" + o.Details[i].Account);
                }
            }
            try
            {
                if (!done)
                    throw new MyException("没有客户为" + username + "的订单");
            }
            catch (MyException e)
            {
                Console.WriteLine(e.Id);
            }
        }
        public bool Check(Order o, string s)
        {
            bool ok = false;
            for (int i = 0; i < o.Details.Count; i++)
            {
                if (o.Details[i].Name == s)
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }
        public void checkOrder(string name, int k)
        {
            bool done = false;
            //var m = orders.Where(o => Check(o, name));
            var m = from o in orders where Check(o, name) select o;
            foreach (Order o in m)
            {
                done = true;
                Console.WriteLine("订单号:" + o.ID);
                Console.WriteLine("客户名:" + o.Username);
                for (int i = 0; i < o.Details.Count; i++)
                {
                    Console.WriteLine("商品:" + o.Details[i].Name + " 单价:" + o.Details[i].Price
                        + " 数量:" + o.Details[i].Account);
                }
            }
            try
            {
                if (!done)
                    throw new MyException("没有购买了" + name + "的订单");
            }
            catch (MyException e)
            {
                Console.WriteLine(e.Id);
            }
        }
        public void Export(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            //XmlSerializer xmlser = new XmlSerializer(typeof(OrderService));
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
                Order order = new Order(i, "A" + i.ToString(),i+"123");
                for (int j = 0; j < 3; j++)
                {
                    order.Details.Add(new OrderDetails("B" + j.ToString(), 3 * j, j));
                }
                os.orders.Add(order);
            }
            Order order1 = new Order(1222, "王有钱","666");
            order1.Details.Add(new OrderDetails("ex", 5000, 3));
            os.orders.Add(order1);
            var expensive = from o in os.orders where o.addUp() > 10000 select o;
            foreach (Order o in expensive)
            {
                Console.WriteLine("价格超过一万元的订单如下:");
                Console.WriteLine("订单号:" + o.ID);
                Console.WriteLine("客户名:" + o.Username);
                for (int i = 0; i < o.Details.Count; i++)
                {
                    Console.WriteLine("商品:" + o.Details[i].Name + " 单价:" + o.Details[i].Price
                        + " 数量:" + o.Details[i].Account);
                }
            }
            os.checkOrder(50);
            os.checkOrder("张三");
            os.checkOrder("水", 0);
            os.checkOrder(2);
            os.checkOrder("A1");
            os.checkOrder("b1", 0);
            os.Export("aaa.xml");
        }
    }
}
