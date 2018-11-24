using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MySql.Data;
using System.ComponentModel.DataAnnotations;
namespace OEF
{
    public class Order
    {
        [Key]
        public String Id { get; set; }
        public String Customer { get; set; }
        public DateTime CreateTime { get; set; }
        public List<OrderDetail> Details { get; set; }

        public Order()
        {
            Details = new List<OrderDetail>();
        }

        public Order(string id, string customer, DateTime createTime, List<OrderDetail> details)
        {
            Id = id;
            Customer = customer;
            CreateTime = createTime;
            Details = details;
        }
    }
}
