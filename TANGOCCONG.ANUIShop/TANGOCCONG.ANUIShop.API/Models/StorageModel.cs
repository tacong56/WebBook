using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.API.Models
{
    public class StorageModel
    {
    }

    #region Request
    public class InsertUpdateStorage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    #endregion

    #region Response
    public class ImportStorageDataResponse : ImportStorage
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }

    public class ExportStorageDataResponse : ExportStorage
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }
    #endregion
}
