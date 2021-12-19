using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.API.Models
{
    public class CartsModel: Cart
    {
        public string ProductName { get; set; }
        public string UserName { get; set; }
    }

    public class CartsIURequest
    {
        public int? Id { set; get; }
        [Required]
        public int ProductId { set; get; }
        [Required]
        public int Quantity { set; get; }
        [Required]
        public decimal Price { set; get; }
        [Required]
        public int UserId { get; set; }
    }
}
