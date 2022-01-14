using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TANGOCCONG.ANUIShop.Data.Enums;

namespace TANGOCCONG.ANUIShop.Data.Entities
{
    public class ImportStorage
    {
        public int Id { set; get; }
        public int ProductId { set; get; }
        public decimal Price { set; get; }
        public int Quantity { set; get; }
        public bool IsDelete { set; get; }
        public DateTime DateCreated { get; set; }
    }
}
