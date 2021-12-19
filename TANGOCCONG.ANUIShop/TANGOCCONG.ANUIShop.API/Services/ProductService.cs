using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Helpers;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.Data.EF;

namespace TANGOCCONG.ANUIShop.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ANUIShopDbContext _context;
        private readonly ConnectionStrings _conn;

        public ProductService(ANUIShopDbContext context, IOptions<ConnectionStrings> conn)
        {
            _context = context;
            _conn = conn.Value;
        }

        public async Task<ResponseData<int>> Create(ProductInsertRequest request)
        {
            var productExist = await CheckProductExist(request.Code);
            if (productExist) return new ErrorResponseData<int>("Mã sản phẩm đã tồn tại");

            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            string query = @"INSERT INTO products (ID, Name, Code, Title, ImageID, Price, TimeCreated, Description, IsActive, View)
                             VALUES(@ID, @Name, @Code, @Title, @ImageID, @Price, @TimeCreated, @Description, @IsActive, @View);
                             SELECT last_insert_id();";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", request.ID);
            command.Parameters.AddWithValue("@Name", request.Name.Trim());
            command.Parameters.AddWithValue("@Code", request.Code);
            command.Parameters.AddWithValue("@Title", request.Title.Trim());
            command.Parameters.AddWithValue("@Price", request.Price);
            command.Parameters.AddWithValue("@ImageID", request.ImageID);
            command.Parameters.AddWithValue("@Description", request.Description.Trim());
            command.Parameters.AddWithValue("@TimeCreated", DateTime.Now);
            command.Parameters.AddWithValue("@IsActive", request.IsActive != null ? request.IsActive.Value : true);
            command.Parameters.AddWithValue("@View", 0);
            try
            {
                var Id = Convert.ToInt32(command.ExecuteScalar());
                //int Id = Convert.ToInt32(command.Parameters["@Id"].Value);
                await CreateProductCategory(Id, request.CategoryID);
                await connection.CloseAsync();

                return new SuccessResponseData<int>("Thêm mới sản phẩm thành công", Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<int> Delete(GetDetailByIntRequest request)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == request.Id);
            if (product == null) return -1;
            product.IsDeleted = true;

            try
            {
                var result = _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResponseData<ProductDataResponse>> Detail(GetDetailByIntRequest request)
        {
            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            string query = @"SELECT * FROM (
	                            SELECT 
		                            p.Id AS ProductId,
                                    p.Code AS ProductCode,
		                            p.Name AS ProductName,
                                    p.Description,
                                    p.IsActive,
                                    p.Price,
                                    p.TimeCreated,
                                    p.TimeUpdated,
                                    p.Title,
                                    p.View,
                                    c.Id AS CategoryId,
                                    c.Name AS CategoryName,
                                    i.Id AS ImageId,
                                    i.UrlPath
	                            FROM products p
                                LEFT JOIN productincategories pic ON pic.ProductId = p.Id
                                LEFT JOIN categories c ON c.Id = pic.CategoryId 
                                LEFT JOIN images i ON i.Id = p.ImageId"
                                + " WHERE p.Id = " + request.Id
                            + " ) AS temp;";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            try
            {
                ProductDataResponse data = new ProductDataResponse();
                if (await reader.ReadAsync())
                {
                    data = new ProductDataResponse()
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        ProductCode = reader["ProductCode"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString(),
                        TimeCreated = Convert.ToDateTime(reader["TimeCreated"]),
                        TimeUpdated = Convert.ToDateTime(reader["TimeUpdated"]),
                        Title = reader["Title"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"]),
                        Price = Convert.ToDecimal(reader["Price"]),
                        View = Convert.ToInt32(reader["View"]),
                        ImageMain = reader["UrlPath"].ToString(),
                        ImageId = Convert.ToInt32(reader["ImageId"])
                    };

                    await connection.CloseAsync();
                }
                return new SuccessResponseData<ProductDataResponse>("", data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationResult<ProductDataResponse>> GetPaging(ProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join i in _context.Images on p.ImageId equals i.Id into pi
                        from i in pi.DefaultIfEmpty()
                        where p.IsActive == true && p.IsDeleted == false
                        select new { p, pic, i, c };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword) || x.p.Code.Contains(request.Keyword));

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            try
            {
                var data = await query.Skip((request.Page - 1) * request.Limit)
                            .Take(request.Limit)
                            .Select(x => new ProductDataResponse()
                            {
                                ProductId = x.p.Id,
                                ProductName = x.p.Name,
                                ProductCode = x.p.Code,
                                TimeCreated = x.p.TimeCreated,
                                Title = x.p.Title,
                                Price = x.p.Price,
                                CategoryId = x.pic.CategoryId,
                                CategoryName = x.c.Name,
                                ImageMain = x.i.UrlPath,
                                Description = x.p.Description
                            }).ToListAsync();

                //4. Select and projection
                var pagedResult = new PaginationResult<ProductDataResponse>(request.Page, totalRow, request.Limit, data);

                return pagedResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<ResponseData<int>> Update(ProductInsertRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ID);
            if (product == null) return new ErrorResponseData<int>("Sản phẩm không tồn tại trong hệ thống.");

            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            string query = @"UPDATE products
                             SET Name=@Name, Code=@Code, Title=@Title, ImageID=@ImageID, Price=@Price, Description=@Description, TimeUpdated=@TimeUpdated
                             WHERE Id=@Id";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", request.ID);
            command.Parameters.AddWithValue("@Name", request.Name.Trim());
            command.Parameters.AddWithValue("@Code", request.Code);
            command.Parameters.AddWithValue("@Title", request.Title.Trim());
            command.Parameters.AddWithValue("@Price", request.Price);
            command.Parameters.AddWithValue("@ImageID", request.ImageID);
            command.Parameters.AddWithValue("@Description", request.Description.Trim());
            command.Parameters.AddWithValue("@TimeUpdated", DateTime.Now);
            command.Parameters.AddWithValue("@View", 0);
            try
            {
                command.ExecuteScalar();
                //int Id = Convert.ToInt32(command.Parameters["@Id"].Value);
                await CreateProductCategory(request.ID, request.CategoryID);
                await connection.CloseAsync();

                return new SuccessResponseData<int>("Cập nhật sản phẩm thành công", request.ID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<bool> CheckProductExist(string code)
        {
            bool check;
            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            string query = "SELECT * FROM products p WHERE Code LIKE N'" + code + "'";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            check = reader.HasRows;

            await connection.CloseAsync();

            return check;
        }

        private async Task<bool> CreateProductCategory(int productId, int categoryId)
        {
            var productInCategory = await _context.ProductInCategories.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (productInCategory != null)
            {
                if (productInCategory.CategoryId != categoryId)
                {
                    _context.ProductInCategories.Remove(productInCategory);
                    await _context.SaveChangesAsync();
                }
                else return false;
            }

            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            string query = @"INSERT INTO productincategories (ProductId, CategoryId)
                             VALUES(@ProductId, @CategoryId)";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductId", productId);
            command.Parameters.AddWithValue("@CategoryId", categoryId);
            try
            {
                var reader = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();

                return reader == 1 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
