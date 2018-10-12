using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class Order
    {
        private int ID;
        private string username;
        public List<OrderDetails> details;
        public Order(int ID, string username)
        {
            this.ID = ID;
            this.username = username;
            details = new List<OrderDetails>();
        }
        public int addUp()
        {
            int ret = 0;
            for(int i = 0;i< details.Count;i++)
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
            return username;
        }
    }
    public class OrderDetails
    {
        private string name;
        private int price;
        private int account;
        public OrderDetails(string name, int price, int account)
        {
            this.name = name;
            this.price = price;
            this.account = account;
        }
        public int getAccount()
        {
            return account;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public void setAccount(int account)
        {
            this.account = account;
        }
        public void setPrice(int price)
        {
            this.price = price;
        }
        public int getPrice()
        {
            return price;
        }
        public string getName()
        {
            return name;
        }

    }
    public class OrderService
    {
        public List<Order> orders;
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
        public void changeOrder(int ID, string name1, string name2, int account, int price)
        {
            try
            {
                bool done = false;
                for (int i = orders.Count - 1; i >= 0; i--)
                {
                    if (orders[i].getID() == ID)
                    {
                        for (int j = 0; j < orders[i].details.Count ; j++)
                        {
                            if (orders[i].details[j].getName() == name1)
                            {
                                orders[i].details[j].setName(name2);
                                orders[i].details[j].setPrice(price);
                                orders[i].details[j].setAccount(account);
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
        public void checkOrder(int id)
        {
            bool done = false;
            var m = from o in orders where o.getID() == id select o;
            foreach(Order o in m)
            {
                done = true;
                Console.WriteLine("订单号:" + o.getID());
                Console.WriteLine("客户名:" + o.getUsername());
                for (int i = 0; i < o.details.Count; i++)
                {
                    Console.WriteLine("商品:" + o.details[i].getName() + " 单价:" + o.details[i].getPrice()
                        + " 数量:" + o.details[i].getAccount());
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
            var m = from o in orders where o.getUsername() == username select o;
            foreach(Order o in m)
            {
                done = true;
                Console.WriteLine("订单号:" + o.getID());
                Console.WriteLine("客户名:" + o.getUsername());
                for (int i = 0; i < o.details.Count;i++)
                {
                    Console.WriteLine("商品:"+o.details[i].getName()+" 单价:"+ o.details[i].getPrice()
                        +" 数量:"+ o.details[i].getAccount());
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
        public bool Check(Order o,string s)
        {
            bool ok = false;
            for(int i = 0; i < o.details.Count;i++)
            {
                if(o.details[i].getName()==s)
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
            foreach(Order o in m)
            {
                done = true;
                Console.WriteLine("订单号:" + o.getID());
                Console.WriteLine("客户名:" + o.getUsername());
                for (int i = 0; i < o.details.Count; i++)
                {
                    Console.WriteLine("商品:" + o.details[i].getName() + " 单价:" + o.details[i].getPrice()
                        + " 数量:" + o.details[i].getAccount());
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
            foreach(Order o in expensive)
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
            os.checkOrder(50);
            os.checkOrder("张三");
            os.checkOrder("水", 0);
            os.checkOrder(2);
            os.checkOrder("A1");
            os.checkOrder("b1", 0);
        }
    }
}
