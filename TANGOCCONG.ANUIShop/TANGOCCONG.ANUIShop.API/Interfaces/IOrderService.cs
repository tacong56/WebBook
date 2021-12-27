using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.API.Payment.Model;
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
        Task<PaginationResult<OrderModel>> GetPaging(int limit, int page, string sort, int? userID = null, string keyword = null);
        Task<List<OrderModel>> GetList(int userID);
        Task<int> ChangeStatus(int id, int status);
        string CreateOrderVNPay(MerchantAccount merchantAccount, int orderID, string domainName);
        ResponseData<Transaction> UpdateOrderAfterPayment(int orderID, int transactionID, string transactionCode, int paymentMothod,
            decimal vnp_Amount, string vnp_BankCode, string vnp_BankTranNo, string vnp_CardType, string vnp_TmnCode);
    }
}
