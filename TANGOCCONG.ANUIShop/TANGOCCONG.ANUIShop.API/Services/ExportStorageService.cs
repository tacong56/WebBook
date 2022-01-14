using Microsoft.Extensions.Options;
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
    public class ExportStorageService : IExportStorage
    {
        private readonly ANUIShopDbContext _context;
        private readonly ConnectionStrings _conn;
        public ExportStorageService(ANUIShopDbContext context, IOptions<ConnectionStrings> conn)
        {
            _context = context;
            _conn = conn.Value;
        }


        public async Task<ResponseData<string>> Delete(int id)
        {
            var order = _context.ExportStorages.FirstOrDefault(x => x.Id == id);
            if (order == null) return new ErrorResponseData<string>("Phiếu xuất không tồn tại");
            try
            {
                _context.ExportStorages.Remove(order);
                await _context.SaveChangesAsync();
                return new SuccessResponseData<string>("Xóa xuất nhập thành công");
            }
            catch (Exception ex)
            {

                return new ErrorResponseData<string>(ex.Message);
            }
        }

        public ResponseData<ExportStorageDataResponse> Detail(int id)
        {
            try
            {
                var o = (from i in _context.ExportStorages
                         join p in _context.Products on i.ProductId equals p.Id into ip
                         from p in ip.DefaultIfEmpty()
                         where i.Id == id
                         select new ExportStorageDataResponse
                         {
                             Id = i.Id,
                             DateCreated = i.DateCreated,
                             IsDelete = i.IsDelete,
                             Price = i.Price,
                             ProductId = p.Id,
                             Quantity = i.Quantity,
                             ProductCode = p.Code,
                             ProductName = p.Name
                         }).FirstOrDefault();

                return new SuccessResponseData<ExportStorageDataResponse>("", o);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<PaginationResult<ExportStorageDataResponse>> GetPaging(int limit, int page, DateTime? ngaytu, DateTime? ngayden, string sort, string keyword = null)
        {
            var data = (from i in _context.ExportStorages
                        join p in _context.Products on i.ProductId equals p.Id into ip
                        from p in ip.DefaultIfEmpty()
                        where (ngaytu == null || i.DateCreated >= ngaytu)
                        && (ngayden == null || i.DateCreated <= ngayden)
                        select new ExportStorageDataResponse
                        {
                            Id = i.Id,
                            Price = Convert.ToInt32(i.Price),
                            Quantity = i.Quantity,
                            DateCreated = i.DateCreated,
                            IsDelete = i.IsDelete,
                            ProductId = p.Id,
                            ProductCode = p.Code,
                            ProductName = p.Name,

                        });

            if (sort == "DATE_DESC")
                data = data.OrderByDescending(x => x.DateCreated);
            if (sort == "DATE_ASC")
                data = data.OrderBy(x => x.DateCreated);

            var totalCount = data.Count();
            var listData = data.Skip((page - 1) * limit).Take(limit).ToList();

            return new PaginationResult<ExportStorageDataResponse>(page, totalCount, limit, listData);
        }

        public async Task<ResponseData<InsertUpdateStorage>> Insert(InsertUpdateStorage request)
        {
            try
            {

                //var findUser = _context.Users.Find(request.UserId);
                //if (findUser == null) return new ErrorResponseData<OrderIURequest>("Không tồn tại user trong hệ thống");

                ExportStorage data = new ExportStorage()
                {
                    IsDelete = false,
                    DateCreated = DateTime.Now,
                    Price = request.Price,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                _context.ExportStorages.Add(data);
                var kq = await _context.SaveChangesAsync();
                if (data.Id > 0)
                {
                    request.Id = data.Id;
                    return new SuccessResponseData<InsertUpdateStorage>("Tạo xuất nhập thành công", request);
                }
                else return new ErrorResponseData<InsertUpdateStorage>("Tạo xuất nhập thất bại");
            }
            catch (Exception ex)
            {
                return new ErrorResponseData<InsertUpdateStorage>(ex.Message);
            }
        }

        public async Task<ResponseData<InsertUpdateStorage>> Update(InsertUpdateStorage request)
        {
            try
            {
                var findOrder = _context.ExportStorages.Where(x => x.Id == request.Id).FirstOrDefault();
                if (findOrder != null)
                {
                    findOrder.Price = request.Price;
                    findOrder.Quantity = request.Quantity;
                    findOrder.ProductId = request.ProductId;
                    var kq = await _context.SaveChangesAsync();

                    return new SuccessResponseData<InsertUpdateStorage>("Cập nhật Tạo phiếu nhập thành công", request);
                }
                else return new ErrorResponseData<InsertUpdateStorage>("Cập nhật Tạo phiếu nhập thất bại");
            }
            catch (Exception ex)
            {
                return new ErrorResponseData<InsertUpdateStorage>(ex.Message);
            }
        }
    }
}
