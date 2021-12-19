using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseData<OrderIURequest>> Insert(OrderIURequest request);
        Task<ResponseData<OrderIURequest>> Update(OrderIURequest request);
        Task<ResponseData<string>> Delete(int id);
        ResponseData<OrderModel> Detail(int id);
        ResponseData<OrderDetail> GetOrderDetail(int orderID, int productID);
        Task<PaginationResult<OrderModel>> GetPaging(int limit, int page, string sort, int userID, string keyword = null);
        Task<List<OrderModel>> GetList(int userID);
    }
}
