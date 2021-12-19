using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Objects;

namespace TANGOCCONG.ANUIShop.API.Models
{
    #region Request
    public class ProductInsertRequest
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Tên danh mục không được vượt quá 250 ký tự")]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageIDs { get; set; }
        public int ImageID { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ProductPagingRequest : BaseRequest
    {
        public int? CategoryId { get; set; }
    }
    #endregion

    #region Response
    public class ProductDataResponse
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ImageId { get; set; }
        public string ImageMain { get; set; }
        public string Images { get; set; }
        public bool IsActive { get; set; }
        public int View { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public int UserUpdate { get; set; }
    }
    #endregion
}
