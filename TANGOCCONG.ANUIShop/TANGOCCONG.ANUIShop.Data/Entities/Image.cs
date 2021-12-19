using System;
using System.Collections.Generic;
using System.Text;

namespace TANGOCCONG.ANUIShop.Data.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string UrlPath { get; set; }
        public double Size { get; set; }
        public bool IsMain { get; set; }
        public bool IsProduct { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}
