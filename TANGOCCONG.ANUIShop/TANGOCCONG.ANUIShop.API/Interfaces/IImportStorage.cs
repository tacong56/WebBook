using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface IImportStorage
    {
        Task<ResponseData<InsertUpdateStorage>> Insert(InsertUpdateStorage request);
        Task<ResponseData<InsertUpdateStorage>> Update(InsertUpdateStorage request);
        Task<ResponseData<string>> Delete(int id);
        Task<PaginationResult<ImportStorageDataResponse>> GetPaging(int limit, int page, DateTime? ngaytu, DateTime? ngayden, string sort, string keyword = null);
        ResponseData<ImportStorageDataResponse> Detail(int id);
    }
}
