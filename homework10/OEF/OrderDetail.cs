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
    public class OrderDetail
    {
        [Key]
        public string Id { get; set; }
        public string Product { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }

        public OrderDetail()
        {
            Id = Guid.NewGuid().ToString();
        }

        public OrderDetail(string id, string product, double unitPrice, int quantity)
        {
            Id = id;
            Product = product;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
