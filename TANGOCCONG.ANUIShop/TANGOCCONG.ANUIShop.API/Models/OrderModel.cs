using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.Data.Entities;
using TANGOCCONG.ANUIShop.Data.Enums;

namespace TANGOCCONG.ANUIShop.API.Models
{
    public class OrderModel : Order
    {

    }

    public class OrderIURequest
    {
        public int? Id { set; get; }
        public DateTime? OrderDate { set; get; }
        [Required]
        public int UserId { set; get; }
        [Required]
        public string ShipName { set; get; }
        [Required]
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        [Required]
        public string ShipPhoneNumber { set; get; }
        public OrderStatus? Status { set; get; }
        [Required]
        [NotMapped]
        public List<OrderDetail> OrderDetails { get; set; }
        public bool? IsDelete { set; get; }
    }
}
