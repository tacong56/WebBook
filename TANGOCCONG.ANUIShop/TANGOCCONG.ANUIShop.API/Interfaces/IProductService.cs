using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface IProductService
    {
        Task<int> Delete(GetDetailByIntRequest request);
        Task<ResponseData<ProductDataResponse>> Detail(GetDetailByIntRequest request);
        Task<ResponseData<int>> Create(ProductInsertRequest request);
        Task<ResponseData<int>> Update(ProductInsertRequest request);
        Task<PaginationResult<ProductDataResponse>> GetPaging(ProductPagingRequest request);
    }
}
