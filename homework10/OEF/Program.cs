using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEF
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("hahah");
            OrderService orderService = new OrderService();
            List<OrderDetail> details = new List<OrderDetail>() {
                new OrderDetail("3", "salad", 10.0, 10),
                new OrderDetail("4", "egg", 2.0, 100)
            };
            Order order = new Order("002", "zouxin2", DateTime.Now, details);

            orderService.Add(order);
            
            //Order order2 = new Order("001", "zouxin1", DateTime.Now, details);
            //orderService.Update(order2);

            //List<Order> o1 = orderService.GetAllOrders();

            //List<Order> orders = orderService.QueryByCustormer("zouxin1");
            //orders.ForEach(
             // o => Console.WriteLine($"{o.Id},{o.Customer}"));
        }
    }
}
