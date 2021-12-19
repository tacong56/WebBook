using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TANGOCCONG.ANUIShop.Data.Entities
{
    public class Cart
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }

        public int UserId { get; set; }

        [NotMapped]
        public Product Product { get; set; }
        [NotMapped]
        public DateTime DateCreated { get; set; }
        [NotMapped]
        public AppUser AppUser { get; set; }
    }
}
