using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface ICategoryService
    {
        Task<ResponseData<string>> Insert(CategoryInsertRequest request);
        Task<ResponseData<string>> Update(CategoryInsertRequest request);
        Task<ResponseData<string>> Delete(CategoryDetailRequest request);
        ResponseData<CategoryDataReponse> Detail(CategoryDetailRequest request);
        Task<PaginationResult<CategoryDataReponse>> GetPaging(CategoryPagingRequest request);
        Task<List<CategoryDataReponse>> GetList(CategoryGetListRequest request);
    }
}
