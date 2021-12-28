﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Helpers;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.API.Payment.Model;
using TANGOCCONG.ANUIShop.API.Payment.VNPay;
using TANGOCCONG.ANUIShop.Data.EF;
using TANGOCCONG.ANUIShop.Data.Entities;
using TANGOCCONG.ANUIShop.Data.Enums;

namespace TANGOCCONG.ANUIShop.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly ANUIShopDbContext _context;
        private readonly ConnectionStrings _conn;
        public OrderService(ANUIShopDbContext context, IOptions<ConnectionStrings> conn)
        {
            _context = context;
            _conn = conn.Value;
        }


        public async Task<ResponseData<string>> Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null) return new ErrorResponseData<string>("Đơn hàng không tồn tại");
            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return new SuccessResponseData<string>("Xóa đơn hàng thành công");
            }
            catch (Exception ex)
            {

                return new ErrorResponseData<string>(ex.Message);
            }
        }

        public ResponseData<OrderModel> Detail(int id)
        {
            var o = (from od in _context.Orders
                     join user in _context.Users on od.UserId equals user.Id
                     where od.Id == id
                     select new OrderModel
                     {
                         Id = od.Id,
                         OrderDate = od.OrderDate,
                         IsDelete = od.IsDelete,
                         ShipAddress = od.ShipAddress,
                         ShipEmail = od.ShipEmail,
                         ShipName = od.ShipName,
                         ShipPhoneNumber = od.ShipPhoneNumber,
                         Status = od.Status,
                         UserId = od.UserId,
                         TotalAmount = od.TotalAmount
                     }).FirstOrDefault();
            o.OrderDetails = (from odt in _context.OrderDetails
                              join pd in _context.Products on odt.ProductId equals pd.Id
                              where odt.OrderId == id
                              select new OrderDetail
                              {
                                  OrderId = odt.OrderId,
                                  Price = odt.Price,
                                  ProductId = odt.ProductId,
                                  ProductName = odt.ProductName,
                                  Quantity = odt.Quantity
                              }
                                         ).ToList();

            return new SuccessResponseData<OrderModel>("", o);
        }

        public ResponseData<OrderDetail> GetOrderDetail(int orderID, int productID)
        {
            var kq = (from odt in _context.OrderDetails
                      join pd in _context.Products on odt.ProductId equals pd.Id
                      where odt.OrderId == orderID && odt.ProductId == productID
                      select new OrderDetail
                      {
                          OrderId = odt.OrderId,
                          Price = odt.Price,
                          ProductId = odt.ProductId,
                          ProductName = odt.ProductName,
                          Quantity = odt.Quantity
                      }).FirstOrDefault();

            return new SuccessResponseData<OrderDetail>("", kq);
        }

        public async Task<List<OrderModel>> GetList(int userID)
        {
            var data = (from od in _context.Orders
                        join user in _context.Users on od.UserId equals user.Id
                        where (od.UserId == userID)
                        select new OrderModel
                        {
                            Id = od.Id,
                            OrderDate = od.OrderDate,
                            IsDelete = od.IsDelete,
                            ShipAddress = od.ShipAddress,
                            ShipEmail = od.ShipEmail,
                            ShipName = od.ShipName,
                            ShipPhoneNumber = od.ShipPhoneNumber,
                            Status = od.Status,
                            UserId = od.UserId
                        }).ToList();
            return data;
        }

        public async Task<int> ChangeStatus(int id, int status)
        {
            var data = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (data == null)
                return 0;
            data.Status = (OrderStatus)status;

            _context.Orders.Update(data);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<PaginationResult<OrderModel>> GetPaging(int limit, int page, string sort, int? userID = null, string keyword = null)
        {
            var data = (from od in _context.Orders
                        join user in _context.Users on od.UserId equals user.Id
                        where (userID == null || userID == 0 || od.UserId == userID)
                        && (keyword == null || keyword == "" || od.ShipName.Contains(keyword)
                        || od.ShipEmail.Contains(keyword)
                        || od.ShipAddress.Contains(keyword)
                        || od.ShipPhoneNumber.Contains(keyword)
                        )
                        select new OrderModel
                        {
                            Id = od.Id,
                            OrderDate = od.OrderDate,
                            IsDelete = od.IsDelete,
                            ShipAddress = od.ShipAddress,
                            ShipEmail = od.ShipEmail,
                            ShipName = od.ShipName,
                            ShipPhoneNumber = od.ShipPhoneNumber,
                            Status = od.Status,
                            UserId = od.UserId
                        });

            if (sort == "ShipName_desc")
                data = data.OrderByDescending(x => x.ShipName);
            if (sort == "ShipPhoneNumber_desc")
                data = data.OrderByDescending(x => x.ShipPhoneNumber);

            var totalCount = data.Count();
            var listData = data.Skip((page - 1) * limit).Take(limit).ToList();

            return new PaginationResult<OrderModel>(page, totalCount, limit, listData);
        }

        public async Task<ResponseData<OrderIURequest>> Insert(OrderIURequest request)
        {
            try
            {

                var findUser = _context.Users.Find(request.UserId);
                if (findUser == null) return new ErrorResponseData<OrderIURequest>("Không tồn tại user trong hệ thống");

                Order data = new Order()
                {
                    IsDelete = false,
                    OrderDate = DateTime.Now,
                    ShipName = request.ShipName,
                    ShipAddress = request.ShipAddress,
                    ShipEmail = request.ShipEmail,
                    ShipPhoneNumber = request.ShipPhoneNumber,
                    Status = Data.Enums.OrderStatus.InProgress,
                    UserId = request.UserId,
                    OrderDetails = request.OrderDetails,
                    TotalAmount = request.OrderDetails.Sum(x => x.Price * x.Quantity)
                };
                _context.Orders.Add(data);
                var kq = await _context.SaveChangesAsync();
                if (data.Id > 0)
                {
                    request.Id = data.Id;
                    request.OrderDetails = null;
                    return new SuccessResponseData<OrderIURequest>("Tạo đơn hàng thành công", request);
                }
                else return new ErrorResponseData<OrderIURequest>("Tạo đơn hàng thất bại");
            }
            catch (Exception ex)
            {
                return new ErrorResponseData<OrderIURequest>(ex.Message);
            }
        }

        public async Task<ResponseData<OrderIURequest>> Update(OrderIURequest request)
        {
            try
            {
                var findUser = _context.Users.Find(request.UserId);
                if (findUser == null) return new ErrorResponseData<OrderIURequest>("Không tồn tại user trong hệ thống");

                var findOrder = _context.Orders.Where(x => x.Id == request.Id).FirstOrDefault();
                if (findOrder != null)
                {
                    _context.OrderDetails.RemoveRange(_context.OrderDetails.Where(x => x.OrderId == request.Id));
                    findOrder.ShipName = request.ShipName;
                    findOrder.ShipAddress = request.ShipAddress;
                    findOrder.ShipEmail = request.ShipEmail;
                    findOrder.ShipPhoneNumber = request.ShipPhoneNumber;
                    findOrder.Status = Data.Enums.OrderStatus.InProgress;
                    findOrder.UserId = request.UserId;
                    findOrder.OrderDetails = request.OrderDetails;
                    findOrder.TotalAmount = request.OrderDetails.Sum(x => x.Price * x.Quantity);
                    var kq = await _context.SaveChangesAsync();
                    request.OrderDetails = null;
                    return new SuccessResponseData<OrderIURequest>("Cập nhật đơn hàng thành công", request);
                }
                else return new ErrorResponseData<OrderIURequest>("Cập nhật đơn hàng thất bại");
            }
            catch (Exception ex)
            {
                return new ErrorResponseData<OrderIURequest>(ex.Message);
            }
        }

        public string CreateOrderVNPay(MerchantAccount merchantAccount, int orderID, string domainName)
        {
            Order order = _context.Orders.Where(x => x.Id == orderID).FirstOrDefault();
            InfoRedirect pharma = new InfoRedirect
            {
                Title = "Thanh toán đơn hàng",
                AgainLink = domainName + "api/Order/checkout",
                ReturnURL = domainName + "api/Order/receiptPaymentVNPay"
            };
            return new VNPay().Payment(order, merchantAccount, pharma);
        }

        public ResponseData<Transaction> UpdateOrderAfterPayment(int orderID, int transactionID, string transactionCode, int paymentMothod,
            decimal vnp_Amount, string vnp_BankCode, string vnp_BankTranNo, string vnp_CardType, string vnp_TmnCode)
        {
            try
            {
                Transaction payments = new Transaction();
                payments.OrderId = orderID;
                payments.TransactionID = transactionID;
                payments.TransactionCode = transactionCode;
                payments.PaymentMethod = paymentMothod;
                payments.Amount = vnp_Amount / 100;
                payments.BankCode = vnp_BankCode;
                payments.BankTranNo = vnp_BankTranNo;
                payments.CardType = vnp_CardType;
                payments.TransactionDate = DateTime.Now;
                payments.TmnCode = vnp_TmnCode;

                _context.Transactions.Add(payments);
                _context.SaveChanges();
                if (payments.Id > 0)
                {
                    return new SuccessResponseData<Transaction>("Tạo đơn hàng thành công", payments);
                }
                else
                {
                    return new ErrorResponseData<Transaction>("Cập nhật đơn hàng thất bại");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
