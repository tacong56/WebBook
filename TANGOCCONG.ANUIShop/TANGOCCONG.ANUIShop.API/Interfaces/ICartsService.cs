using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface ICartsService
    {
        Task<ResponseData<CartsIURequest>> Insert(CartsIURequest request);
        Task<ResponseData<CartsIURequest>> Update(CartsIURequest request);
        Task<ResponseData<string>> Delete(int id);
        ResponseData<CartsModel> Detail(int id);
        Task<PaginationResult<CartsModel>> GetPaging(int limit, int page, string sort, int? userID, string keyword = null);
        Task<List<CartsModel>> GetList(int userID);
    }
}
