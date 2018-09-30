using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program2
{
    class MyException:Exception
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
    class Order
    {
        private int ID;
        private string username;
        public List<OrderDetails> details;
        public Order(int ID,string username)
        {
            this.ID = ID;
            this.username = username;
            details = new List<OrderDetails>();
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
            }catch(MyException e)
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
    class OrderDetails
    { 
        private string name;
        private int price;
        private int account;
        public OrderDetails(string name,int price,int account)
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
    class OrderService
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
            }catch(MyException e)
            {
                Console.Write(e.Id);
            }
        }
        public void changeOrder(int ID,string name1,string name2,int account,int price)
        {
            try
            {
                bool done = false;
                for (int i = orders.Count - 1; i >= 0; i--)
                {
                    if (orders[i].getID() == ID)
                    {
                        for (int j = 0; j < orders[i].details.Count - 1; j++)
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
            }catch(MyException e)
            {
                Console.Write(e.Id);
            }
        }
        public void checkOrder(int id)
        {
            bool done = false;            
            for (int i = 0; i < orders.Count-1;i++)
            {
                if(orders[i].getID()== id)
                {
                    for (int j = 0; j < orders[i].details.Count - 1; j++)
                        Console.WriteLine("商品:" + orders[i].details[j].getName() + " 单价：" +
                            orders[i].details[j].getPrice() + " 数量:" + orders[i].details[j].getAccount());
                    done = true;
                }
            }
            try
            {
                if (!done)
                    throw new MyException("没有ID为" + id + "的订单");
            }catch(MyException e)
            {
                Console.WriteLine(e.Id);
            }                       
        }
        public void checkOrder(string username)
        {
            bool done = false;
            for (int i = 0; i < orders.Count - 1; i++)
            {
                if (orders[i].getUsername() == username)
                {
                    for (int j = 0; j < orders[i].details.Count - 1; j++)
                        Console.WriteLine("商品:" + orders[i].details[j].getName() + " 单价：" +
                            orders[i].details[j].getPrice() + " 数量:" + orders[i].details[j].getAccount());
                    done = true;
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
        public void checkOrder(string name,int k)
        {
            bool done = false;
            for (int i = 0; i < orders.Count - 1; i++)
            {
                bool aim = false;
                for(int j = 0; j < orders[i].details.Count-1;j++)
                {
                    if(orders[i].details[j].getName() == name)
                    {
                        aim = true;
                        break;
                    }
                }
                if (aim)
                {
                    Console.WriteLine("ID:" + orders[i].getID() + "\n" +
                            "客户：" + orders[i].getUsername() + "\n");
                    for (int j = 0; j < orders[i].details.Count - 1; j++)
                        Console.WriteLine("商品:" + orders[i].details[j].getName() + " 单价：" +
                            orders[i].details[j].getPrice() + " 数量:" + orders[i].details[j].getAccount());
                    done = true;
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
            for(int i = 0; i < 10;i++)
            {
                Order order = new Order(i,"A"+i.ToString());
                for(int j = 0; j < 3;j++)
                {
                    order.details.Add(new OrderDetails("B" + j.ToString(), 3 * j, j));
                }
                os.orders.Add(order);
            }
            os.changeOrder(15, "A1", "B1", 3, 2);
            os.changeOrder(1, "A1", "B8", 3, 2);
            os.changeOrder(1, "A1", "B1", 3, 2);
            os.checkOrder(50);//
            os.checkOrder("张三");
            os.checkOrder("水", 0);
            os.checkOrder(2);
            os.checkOrder("A1");
            os.checkOrder("B1", 0);
        }
    }
}
