using Microsoft.VisualStudio.TestTools.UnitTesting;
using program1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace program1.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void OrderServiceTest()
        {
            OrderService os = new OrderService();
            Assert.IsNotNull(os.orders);
        }

        [TestMethod()]
        public void addOrderTest()
        {
            OrderService os = new OrderService();
            Order o = new Order();
            os.addOrder(o);
            Assert.IsTrue(os.orders.Count == 1);
        }

        [TestMethod()]
        public void delOrderTest()
        {
            OrderService os = new OrderService();
            Order o = new Order();
            os.addOrder(o);
            int a1 = os.orders.Count;
            os.delOrder(0);
            int a2 = os.orders.Count;
            Assert.IsTrue(a1 == 1 && a2 == 0);
        }       
        [TestMethod()]
        public void checkOrderTest()
        {
            OrderService os = new OrderService();
            Order o = new Order(10, "aaa");
            os.addOrder(o);
            List<Order> ors = os.checkOrder(10);
            Assert.IsTrue(ors.Count==1&&ors[0].getID()==10&& ors[0].getUsername()=="aaa");
        }

        [TestMethod()]
        public void checkOrderTest1()
        {
            OrderService os = new OrderService();
            Order o = new Order(10, "aaa");
            os.addOrder(o);
            List<Order> ors = os.checkOrder("aaa");
            Assert.IsTrue(ors.Count == 1 && ors[0].getID() == 10 && ors[0].getUsername() == "aaa");
        }

        [TestMethod()]
        public void CheckTest()
        {
            OrderService os = new OrderService();
            Order o = new Order(10, "aaa");
            OrderDetails od = new OrderDetails("a1", 0, 0);
            o.addDetail(od);
            bool b1 = os.Check(o, "a1");
            bool b2 = os.Check(o, "b1");
            Assert.IsTrue(b1 && !b2);
        }

        [TestMethod()]
        public void checkOrderTest2()
        {
            OrderService os = new OrderService();
            Order o1 = new Order(10, "aaa");
            Order o2 = new Order(11, "abc");
            OrderDetails od1 = new OrderDetails("a1", 2, 2);
            OrderDetails od2 = new OrderDetails("a3", 2, 2);
            o2.addDetail(od2);
            o1.addDetail(od1);
            os.addOrder(o1);
            os.addOrder(o2);
            List<Order> ors = os.checkOrder("a1", 0) ;
            Assert.IsTrue(ors.Count==1&&ors[0].details[0].getName()=="a1");
        }

        [TestMethod()]
        public void ExportTest()
        {
            OrderService os = new OrderService();
            Order o = new Order(10, "aaa");
            OrderDetails od = new OrderDetails("a1", 2, 3);
            o.addDetail(od);
            os.addOrder(o);
            os.Export("test1.xml");
            string s1, s2;
            using (FileStream fr = new FileStream("test1.xml", FileMode.Open,FileAccess.Read))
            {
                s1 = File.ReadAllText("test1.xml", Encoding.UTF8);
                s2 = "<?xml version=\"1.0\"?>\r\n"+
                    "<ArrayOfOrder xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  "+
                    "<Order>\r\n    <details>\r\n      <OrderDetails>\r\n        <Name>a1</Name>\r\n        "+
                    "<Price>2</Price>\r\n        <Account>3</Account>\r\n      </OrderDetails>\r\n    "+
                    "</details>\r\n    <ID>10</ID>\r\n    <Username>aaa</Username>\r\n  </Order>\r\n</ArrayOfOrder>";
            }
            Assert.IsTrue(s1==s2);
        }

        [TestMethod()]
        public void ImportTest()
        {
            OrderService os = new OrderService();
            Order o = new Order(10, "aaa");
            OrderDetails od = new OrderDetails("a1",2,3);
            o.addDetail(od);
            os.addOrder(o);
            os.Export("test2.xml");
            List<Order> aim = os.Import("test2.xml");
            bool b1 = (aim.Count == 1 && aim[0].details.Count == 1);
            bool b2 = false;
            if (b1)
            {
                if (aim[0].getID() == 10 && aim[0].getUsername() == "aaa")
                {
                    if (aim[0].details[0].getName() == "a1" && aim[0].details[0].getPrice() == 2
                        && aim[0].details[0].getAccount() == 3) b2 = true;
                }
            }
            Assert.IsTrue(b1&&b2);
        }
    }
}