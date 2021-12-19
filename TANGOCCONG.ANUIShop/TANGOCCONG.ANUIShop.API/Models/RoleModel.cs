using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.Data.Entities;
using TANGOCCONG.ANUIShop.Data.Enums;

namespace TANGOCCONG.ANUIShop.API.Models
{
    #region Request
    //public class CategoryPagingRequest : BaseRequest
    //{
    //    public int? Level { get; set; }
    //}
    //public class CategoryInsertRequest
    //{
    //    public int? Id { get; set; }
    //    [Required]
    //    [MaxLength(250, ErrorMessage = "Tên danh mục không được vượt quá 250 ký tự")]
    //    public string Name { get; set; }
    //    [Required]
    //    public int SortOrder { get; set; }
    //    public bool? IsShowOnHome { get; set; }
    //    public int? ParentId { get; set; }
    //    public int Level { get; set; }
    //    public bool? IsDelete { get; set; }
    //    public Status? Status { get; set; }
    //}
    //public class CategoryDetailRequest
    //{
    //    public int Id { get; set; }
    //}
    public class RoleGetListRequest
    {
        public int? Level { get; set; }
    }
    #endregion

    #region Response
    public class RoleDataReponse : AppRole
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
    #endregion
}
