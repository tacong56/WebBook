using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TANGOCCONG.ANUIShop.Data.Entities
{
    public class OrderDetail
    {

        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        [NotMapped]
        public Order Order { get; set; }
        [NotMapped]
        public Product Product { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
    }
}
