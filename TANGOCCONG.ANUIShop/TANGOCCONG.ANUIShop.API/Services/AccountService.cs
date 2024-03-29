﻿using Microsoft.AspNetCore.Identity;
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
            if (existUserByEmail != null && !string.IsNullOrEmpty(request.Email)) return new ErrorResponseData<string>("Email đã được sử dụng");
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
            var where = " where au.IsDelete = 0 and au.Id = " + id;
            using var connection = new MySqlConnection(_conn.DefaultConnection);
            await connection.OpenAsync();

            string query = @"select au.Id UserId,
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
                        + where;

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
                        Id = Convert.ToInt32(reader["UserId"]),
                        UserName = reader["UserName"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Dob = Convert.ToDateTime(reader["Dob"]),
                        Email = reader["Email"].ToString(),
                        Avatar = reader["UrlPath"].ToString(),
                        RoleId = Convert.ToInt32(reader["RoleId"]),
                        RoleName = reader["RoleName"].ToString(),
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

            var where = " where ar.Id <> 1 ";
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                where += " and (au.UserName like N'%" + request.Keyword + "%' or au.PhoneNumber like N'%" + request.Keyword + "%') ";
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
                               au.IsDelete,
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
                        Id = Convert.ToInt32(reader["UserId"]),
                        UserName = reader["UserName"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Dob = Convert.ToDateTime(reader["Dob"]),
                        Email = reader["Email"].ToString(),
                        Avatar = reader["UrlPath"].ToString(),
                        RoleId = Convert.ToInt32(reader["RoleId"]),
                        RoleName = reader["RoleName"].ToString(),
                        ImageId = Convert.ToInt32(reader["ImageId"]),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        IsDelete = Convert.ToBoolean(reader["IsDelete"]),
                    };
                    entries.Add(data);
                }
                var count = entries.Count();
                var data1 = entries.Skip((request.Page - 1) * request.Limit).Take(request.Limit).ToList();
                await connection.CloseAsync();
                return new PaginationResult<AccountDataResponse>(request.Page, count, request.Limit, entries);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public string ChangePassword(ChangePasswordRequest request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == request.Id);
            if (user == null)
                return "Tài khoản không tồn tại";
            user.PasswordHash = Utilities.HassPass(request.Password);

            _context.Users.Update(user);
            _context.SaveChanges();

            return "Thay đổi mật khẩu thành công";
        }


        public string LockAccount(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return "Tài khoản không tồn tại";

            var result = user.IsDelete ? "Mở tài khoản thành công" : "Khóa tài khoản thành công";

            user.IsDelete = !user.IsDelete;

            _context.Users.Update(user);
            _context.SaveChanges();

            return result;
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
