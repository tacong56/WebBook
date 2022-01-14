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
        Task<PaginationResult<ProductDataResponse>> GetPaging2(int page, int limit, int? categoryid, string keyword, string sortprice, string sortname, string where);
        Task<List<ProductDataResponse>> GetList(int top, string sort, string keyword, int? priceFrom, int? priceTo);
        Task<List<ProductDataResponse>> GetAll(string keyword);
        Task<PaginationResult<ProductDataResponse>> GetByParentCategory(int page, int limit, int? categoryid);
    }
}
