using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Comons;
using TANGOCCONG.ANUIShop.API.Helpers;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.Data.EF;
using TANGOCCONG.ANUIShop.Data.Entities;
using TANGOCCONG.ANUIShop.Data.Enums;

namespace TANGOCCONG.ANUIShop.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ANUIShopDbContext _context;
        private readonly ConnectionStrings _conn;
        public CategoryService(ANUIShopDbContext context, IOptions<ConnectionStrings> conn)
        {
            _context = context;
            _conn = conn.Value;
        }

        public async Task<ResponseData<string>> Delete(CategoryDetailRequest request)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == request.Id);
            if (category == null) return new ErrorResponseData<string>("Danh mục không tồn tại");
            category.IsDelete = true;
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return new SuccessResponseData<string>("Xóa danh mục thành công");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResponseData<CategoryDataReponse> Detail(CategoryDetailRequest request)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == request.Id);
            if (category == null) return new ErrorResponseData<CategoryDataReponse>("Danh mục không tồn tại");

            CategoryDataReponse data = new CategoryDataReponse()
            {
                Id = category.Id,
                Name = category.Name,
                SortOrder = category.SortOrder,
                Status = category.Status,
                IsDelete = category.IsDelete,
                IsShowOnHome = category.IsShowOnHome,
                ParentId = category.ParentId,
            };

            return new SuccessResponseData<CategoryDataReponse>("", data);
        }

        public async Task<PaginationResult<CategoryDataReponse>> GetPaging(CategoryPagingRequest request)
        {
            var where = "WHERE c.IsDelete = 0";
            if (request.Page == 0) request.Page = 1;
            if (request.Limit == 0) request.Limit = 10;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                where += " AND c.Name LIKE N'%" + request.Keyword + "%'";
            }
            if (request.Level != null)
            {
                where += " AND c.Level = " + request.Level.Value;
            }

            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();
            string query = @"SELECT c.*, p.Name ParentName FROM categories c 
                             LEFT JOIN categories p ON p.Id = c.ParentId " + where + " ORDER BY ID DESC";
            using var command = new MySqlCommand(query, connection);
            try
            {
                using var reader = await command.ExecuteReaderAsync();
                List<CategoryDataReponse> categories = new List<CategoryDataReponse>();
                while (await reader.ReadAsync())
                {
                    CategoryDataReponse category = new CategoryDataReponse()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ParentId = Convert.ToInt32(reader["ParentId"]),
                        Name = reader["Name"].ToString(),
                        IsDelete = (bool)reader["IsDelete"] /*as bool? ?? false*/,
                        IsShowOnHome = (bool)reader["IsShowOnHome"],
                        Level = Convert.ToInt32(reader["Level"]),
                        SortOrder = Convert.ToInt32(reader["SortOrder"]),
                        Status = reader["Status"] as Status? ?? Status.Active,
                        ParentName = reader["ParentName"].ToString(),
                    };
                    categories.Add(category);
                    // do something with 'value'
                }

                var total = categories.Count();
                var records = categories.Skip((request.Page - 1) * request.Limit).Take(request.Limit).ToList();
                return new PaginationResult<CategoryDataReponse>(request.Page, total, request.Limit, records);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<CategoryDataReponse>> GetList(CategoryGetListRequest request)
        {
            var where = "WHERE IsDelete = 0";
            if (request.Level != null)
            {
                where += " AND Level = " + request.Level;
            }

            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();
            string query = "SELECT * FROM categories c " + where + " ORDER BY ID";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            List<CategoryDataReponse> categories = new List<CategoryDataReponse>();
            try
            {
                while (await reader.ReadAsync())
                {
                    CategoryDataReponse category = new CategoryDataReponse()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ParentId = Convert.ToInt32(reader["ParentId"]),
                        Name = reader["Name"].ToString(),
                        IsDelete = (bool)reader["IsDelete"] /*as bool? ?? false*/,
                        IsShowOnHome = (bool)reader["IsShowOnHome"],
                        Level = Convert.ToInt32(reader["Level"]),
                        SortOrder = Convert.ToInt32(reader["SortOrder"]),
                        Status = reader["Status"] as Status? ?? Status.Active
                    };
                    categories.Add(category);
                    // do something with 'value'
                }

                return categories;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResponseData<string>> Insert(CategoryInsertRequest request)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Name == request.Name);
            if (category != null) return new ErrorResponseData<string>("Tên danh mục đã tồn tại");
            var checkSortOrder = _context.Categories.FirstOrDefault(x => x.ParentId == request.ParentId && x.SortOrder == request.SortOrder);
            if (checkSortOrder != null) return new ErrorResponseData<string>("Vị trí danh mục đã tồn tại");
            //if ((request.ParentId != null && request.Level == (int)Enums.CategoryLevel.parent)
            //    || (request.ParentId == null && request.Level == (int)Enums.CategoryLevel.child))
            //    return new ErrorResponseData<string>("Lỗi thêm danh mục. Kiểm tra danh mục cha và cấp danh mục");
            if (request.SortOrder <= 0)
                return new ErrorResponseData<string>("Vị trí không được nhỏ hơn 1");
            try
            {
                CategoryModel data = new CategoryModel()
                {
                    IsShowOnHome = request.IsShowOnHome != null ? request.IsShowOnHome.Value : true,
                    Name = request.Name,
                    IsDelete = request.IsDelete != null ? request.IsDelete.Value : false,
                    Level = request.Level,
                    ParentId = request.ParentId != null ? request.ParentId.Value : 0,
                    SortOrder = request.SortOrder,
                    Status = request.Status != null ? request.Status.Value : Status.Active
                };
                _context.Categories.Add(data);
                await _context.SaveChangesAsync();

                return new SuccessResponseData<string>("Tạo danh mục thành công");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResponseData<string>> Update(CategoryInsertRequest request)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == request.Id);
            if (category == null) return new ErrorResponseData<string>("Danh mục không tồn tại");
            var checkSortOrder = _context.Categories.FirstOrDefault(x => x.ParentId == request.ParentId && x.SortOrder == request.SortOrder);
            if (checkSortOrder != null && checkSortOrder.ParentId != category.ParentId && checkSortOrder.Level != category.Level)
                return new ErrorResponseData<string>("Vị trí danh mục đã tồn tại");
            //if ((request.ParentId != null && request.Level == (int)Enums.CategoryLevel.parent)
            //   || (request.ParentId == null && request.Level == (int)Enums.CategoryLevel.child))
            //    return new ErrorResponseData<string>("Lỗi cập nhật danh mục. Kiểm tra danh mục cha và cấp danh mục");
            if (request.SortOrder <= 0)
                return new ErrorResponseData<string>("Vị trí không được nhỏ hơn 1");

            try
            {
                category.IsShowOnHome = request.IsShowOnHome != null ? request.IsShowOnHome.Value : true;
                category.Name = request.Name;
                category.Level = request.Level;
                category.ParentId = request.ParentId != null ? request.ParentId.Value : 0;
                category.SortOrder = request.SortOrder;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return new SuccessResponseData<string>("Cập nhật danh mục thành công");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
