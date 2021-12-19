using System;
using System.Collections.Generic;
using System.Text;

namespace TANGOCCONG.ANUIShop.Data.Entities
{
    public class SystemParameter
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int ParameterId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
