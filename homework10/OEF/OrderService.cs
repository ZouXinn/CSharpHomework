using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEF
{
    public class OrderService
    {
        public void Add(Order order)
        {
            using (var db = new OrderDB())
            {
                db.Order.Add(order);
                //db.Order.Attach(order);
                //db.Entry(order).State = EntityState.Added;
                db.SaveChanges();
            }
        }
        public void Delete(String orderId)
        {
            using (var db = new OrderDB())
            {
                var order = db.Order.Include("Details").SingleOrDefault(o => o.Id == orderId);
                db.OrderDetail.RemoveRange(order.Details);
                db.Order.Remove(order);
                db.SaveChanges();
            }
        }
        public void Update(Order order)//修改
        {
            using (var db = new OrderDB())
            {
                db.Order.Attach(order);
                db.Entry(order).State = EntityState.Modified;
                order.Details.ForEach(
                    item => db.Entry(item).State = EntityState.Modified);
                db.SaveChanges();
            }
        }
        public Order GetOrder(String Id)
        {
            using (var db = new OrderDB())
            {
                return db.Order.Include("Details").
                  SingleOrDefault(o => o.Id == Id);
            }
        }
        public List<Order> GetAllOrders()
        {
            using (var db = new OrderDB())
            {
                return db.Order.Include("Details").ToList<Order>();
            }
        }
        public List<Order> QueryByCustormer(String custormer)
        {
            using (var db = new OrderDB())
            {
                return db.Order.Include("Details")
                  .Where(o => o.Customer.Equals(custormer)).ToList<Order>();
            }
        }
        public List<Order> QueryByGoods(String product)
        {
            using (var db = new OrderDB())
            {
                var query = db.Order.Include("items")
                  .Where(o => o.Details.Where(
                    item => item.Product.Equals(product)).Count() > 0);
                return query.ToList<Order>();
            }
        }
    }
}