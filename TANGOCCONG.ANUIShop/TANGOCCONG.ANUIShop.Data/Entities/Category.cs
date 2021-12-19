using System.Collections.Generic;
using TANGOCCONG.ANUIShop.Data.Enums;

namespace TANGOCCONG.ANUIShop.Data.Entities
{
    public class Category
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public int SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public int ParentId { set; get; }
        public int Level { set; get; }
        public bool IsDelete { set; get; }
        public Status Status { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }

    }
}
