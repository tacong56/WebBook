using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

namespace TANGOCCONG.ANUIShop.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ANUIShopDbContext _context;
        private readonly ConnectionStrings _conn;

        public AccountService(ANUIShopDbContext context, IOptions<ConnectionStrings> conn, UserManager<AppUser> userManager)
        {
            _context = context;
            _conn = conn.Value;
            _userManager = userManager;
        }

        public async Task<ResponseData<string>> Create(RegisterRequest request)
        {
            var existUserByName = await _context.AppUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            var existUserByEmail = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existUserByName != null) return new ErrorResponseData<string>("Tài khoản đã tồn tại");
            if (existUserByEmail != null) return new ErrorResponseData<string>("Email đã được sử dụng");
            if (request.Password != request.Password_Repeat) return new ErrorResponseData<string>("Mật khẩu không trùng khớp");

            var user = new AppUser()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = Utilities.HassPass(request.Password),
                PhoneNumber = request.PhoneNumber,
                Dob = request.Dob != null ? request.Dob.Value : DateTime.MinValue,
                ImageId = request.ImageID != null ? request.ImageID.Value : 0,
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                if (request.RoleId != null)
                {
                    await createUserRole(request.RoleId, user.Id);
                }
                return new SuccessResponseData<string>("Đăng ký thành công");
            }
            else return new ErrorResponseData<string>("Đăng ký thất bại");
        }

        public async Task<int> Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
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

        public async Task<ResponseData<AccountDataResponse>> Detail(int id)
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
                                + " WHERE p.Id = " + id
                            + " ) AS temp;";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            try
            {
                AccountDataResponse data = new AccountDataResponse();
                if (await reader.ReadAsync())
                {
                    data = new AccountDataResponse()
                    {
                        //ProductId = Convert.ToInt32(reader["ProductId"]),
                        //ProductName = reader["ProductName"].ToString(),
                        //ProductCode = reader["ProductCode"].ToString(),
                        //CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        //CategoryName = reader["CategoryName"].ToString(),
                        //Description = reader["Description"].ToString(),
                        //TimeCreated = Convert.ToDateTime(reader["TimeCreated"]),
                        //TimeUpdated = Convert.ToDateTime(reader["TimeUpdated"]),
                        //Title = reader["Title"].ToString(),
                        //IsActive = Convert.ToBoolean(reader["IsActive"]),
                        //Price = Convert.ToDecimal(reader["Price"]),
                        //View = Convert.ToInt32(reader["View"]),
                        //ImageMain = reader["UrlPath"].ToString(),
                        ImageId = Convert.ToInt32(reader["ImageId"])
                    };

                    await connection.CloseAsync();
                }
                return new SuccessResponseData<AccountDataResponse>("", data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationResult<AccountDataResponse>> GetPaging(AccountSearchRequest request)
        {
            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            var where = " where au.IsDelete = 0 and ar.Name not like N'Admin' ";
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                where += " and (au.UserName like N'%" + request.Keyword + "%') ";
            }
            if (request.RoleId != null)
            {
                where += " and (ar.Id = " + request.RoleId + ") ";
            }
            //1. Select join
            var query = @"select au.Id UserId,
	                           au.UserName UserName,	
	                           au.FirstName,
                               au.LastName,
                               au.dob,
                               au.UserName,
                               au.Email,
                               au.PhoneNumber,
                               au.ImageId,
                               ar.Id RoleId,
                               ar.Name RoleName,
                               i.UrlPath
                        from appusers au
                        left join appuserroles aur on aur.UserId = au.Id
                        left join approles ar on ar.Id = aur.RoleId
                        left join images i on i.Id = au.ImageId"
                        + where
                        + " order by au.Id desc ";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            var entries = new List<AccountDataResponse>();
            try
            {
                while (await reader.ReadAsync())
                {
                    AccountDataResponse data = new AccountDataResponse()
                    {
                        //ProductId = Convert.ToInt32(reader["ProductId"]),
                        //ProductName = reader["ProductName"].ToString(),
                        //ProductCode = reader["ProductCode"].ToString(),
                        //CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        //CategoryName = reader["CategoryName"].ToString(),
                        //Description = reader["Description"].ToString(),
                        //TimeCreated = Convert.ToDateTime(reader["TimeCreated"]),
                        //TimeUpdated = Convert.ToDateTime(reader["TimeUpdated"]),
                        //Title = reader["Title"].ToString(),
                        //IsActive = Convert.ToBoolean(reader["IsActive"]),
                        //Price = Convert.ToDecimal(reader["Price"]),
                        //View = Convert.ToInt32(reader["View"]),
                        //ImageMain = reader["UrlPath"].ToString(),
                        ImageId = Convert.ToInt32(reader["ImageId"])
                    };
                    entries.Add(data);
                }
                await connection.CloseAsync();
                return new PaginationResult<AccountDataResponse>(request.Page, 0, request.Limit, entries);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<ResponseData<int>> Update(AccountUpdateRequest request)
        {
            var product = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (product == null) return new ErrorResponseData<int>("Sản phẩm không tồn tại trong hệ thống.");

            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            string query = @"UPDATE products
                             SET Name=@Name, Code=@Code, Title=@Title, ImageID=@ImageID, Price=@Price, Description=@Description, TimeUpdated=@TimeUpdated
                             WHERE Id=@Id";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            //command.Parameters.AddWithValue("@ID", request.ID);
            //command.Parameters.AddWithValue("@Name", request.Name.Trim());
            //command.Parameters.AddWithValue("@Code", request.Code);
            //command.Parameters.AddWithValue("@Title", request.Title.Trim());
            //command.Parameters.AddWithValue("@Price", request.Price);
            //command.Parameters.AddWithValue("@ImageID", request.ImageID);
            //command.Parameters.AddWithValue("@Description", request.Description.Trim());
            command.Parameters.AddWithValue("@TimeUpdated", DateTime.Now);
            command.Parameters.AddWithValue("@View", 0);
            try
            {
                command.ExecuteScalar();
                //int Id = Convert.ToInt32(command.Parameters["@Id"].Value);
                //await CreateProductCategory(request.ID, request.CategoryID);
                await connection.CloseAsync();

                return new SuccessResponseData<int>("Cập nhật sản phẩm thành công");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<int> createUserRole(int roleId, int userId)
        {
            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            string query = @"INSERT INTO appuserroles (UserId, RoleId)
                             VALUES(@UserId, @RoleId)";

            // Tạo đối tượng SqlCommand
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@RoleId", roleId);
            try
            {
                command.ExecuteScalar();
                await connection.CloseAsync();

                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
