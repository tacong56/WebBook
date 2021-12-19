using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Helpers;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.Data.EF;
using TANGOCCONG.ANUIShop.Data.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TANGOCCONG.ANUIShop.API.Services
{
    public class CartsService : ICartsService
    {
        private readonly ANUIShopDbContext _context;
        private readonly ConnectionStrings _conn;
        public CartsService(ANUIShopDbContext context, IOptions<ConnectionStrings> conn)
        {
            _context = context;
            _conn = conn.Value;
        }

        public async Task<ResponseData<string>> Delete(int id)
        {
            var cart = _context.Carts.FirstOrDefault(x => x.Id == id);
            if (cart == null) return new ErrorResponseData<string>("Giỏ hàng không tồn tại");
            try
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return new SuccessResponseData<string>("Xóa giỏ hàng thành công");
            }
            catch (Exception ex)
            {

                return new ErrorResponseData<string>(ex.Message);
            }
        }

        public ResponseData<CartsModel> Detail(int id)
        {
            var kq = (from c in _context.Carts
                      join product in _context.Products on c.ProductId equals product.Id
                      join user in _context.Users on c.UserId equals user.Id
                      where (c.Id == id)
                      select new CartsModel
                      {
                          ProductName = product.Name,
                          Price = c.Price,
                          Quantity = c.Quantity,
                          Id = c.Id,
                          ProductId = c.ProductId,
                          UserId = c.UserId,
                          UserName = user.UserName
                      }).FirstOrDefault();
            if (kq != null)
                return new SuccessResponseData<CartsModel>("Thành công", kq);
            else return new ErrorResponseData<CartsModel>("Không tìm thấy giỏ hàng trong hệ thống");
        }


        public async Task<List<CartsModel>> GetList(int userID)
        {
            return (from c in _context.Carts
                    join product in _context.Products on c.ProductId equals product.Id
                    join user in _context.Users on c.UserId equals user.Id
                    where (c.UserId == userID)
                    select new CartsModel
                    {
                        ProductName = product.Name,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        Id = c.Id,
                        ProductId = c.ProductId,
                        UserId = c.UserId,
                        UserName = user.UserName
                    }).ToList();
        }

        public async Task<PaginationResult<CartsModel>> GetPaging(int limit, int page, string sort, int? userID, string keyword = null)
        {
            var data = (from c in _context.Carts
                        join product in _context.Products on c.ProductId equals product.Id
                        join user in _context.Users on c.UserId equals user.Id
                        where (keyword == null || keyword == "" || product.Name.Contains(keyword))
                        && (userID == null || userID == 0 || c.UserId == userID)
                        select new CartsModel
                        {
                            ProductName = product.Name,
                            Price = c.Price,
                            Quantity = c.Quantity,
                            Id = c.Id,
                            ProductId = c.ProductId,
                            UserId = c.UserId,
                            UserName = user.UserName
                        });

            if (sort == "productName_desc")
                data = data.OrderByDescending(x => x.ProductName);
            if (sort == "productID_desc")
                data = data.OrderByDescending(x => x.ProductId);

            var totalCount = data.Count();
            var listData = data.Skip(page == 0 ? 0 : page + 1).Take(limit).ToList();

            return new PaginationResult<CartsModel>(page, totalCount, limit, listData);
        }

        public async Task<ResponseData<CartsIURequest>> Insert(CartsIURequest request)
        {
            try
            {

                var findProduct = _context.Products.Find(request.ProductId);
                if (findProduct == null) return new ErrorResponseData<CartsIURequest>("Không tồn tại sản phẩm trong hệ thống");

                var findUser = _context.Users.Find(request.UserId);
                if (findUser == null) return new ErrorResponseData<CartsIURequest>("Không tồn tại user trong hệ thống");

                Cart data = new Cart()
                {
                    ProductId = request.ProductId,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    UserId = request.UserId,
                    DateCreated = DateTime.Now
                };
                _context.Carts.Add(data);
                var kq = await _context.SaveChangesAsync();
                if (data.Id > 0)
                {
                    request.Id = data.Id;
                    return new SuccessResponseData<CartsIURequest>("Tạo giỏ hàng thành công", request);
                }
                else return new ErrorResponseData<CartsIURequest>("Tạo giỏ hàng thất bại");
            }
            catch (Exception ex)
            {
                return new ErrorResponseData<CartsIURequest>(ex.Message);
            }
        }

        public async Task<ResponseData<CartsIURequest>> Update(CartsIURequest request)
        {
            try
            {
                var findProduct = _context.Products.Find(request.ProductId);
                if (findProduct == null) return new ErrorResponseData<CartsIURequest>("Không tồn tại sản phẩm trong hệ thống");

                var findUser = _context.Users.Find(request.UserId);
                if (findUser == null) return new ErrorResponseData<CartsIURequest>("Không tồn tại user trong hệ thống");

                var findCard = _context.Carts.Where(x => x.Id == request.Id).FirstOrDefault();
                if (findCard != null)
                {
                    findCard.Price = request.Price;
                    findCard.ProductId = request.ProductId;
                    findCard.Quantity = request.Quantity;
                    findCard.UserId = request.UserId;
                    var kq = await _context.SaveChangesAsync();
                    return new SuccessResponseData<CartsIURequest>("Cập nhật giỏ hàng thành công", request);
                }
                else
                {
                    return new ErrorResponseData<CartsIURequest>("Không tìm thấy giỏ hàng trong hệ thống.");
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponseData<CartsIURequest>(ex.Message);
            }
        }
    }
}
