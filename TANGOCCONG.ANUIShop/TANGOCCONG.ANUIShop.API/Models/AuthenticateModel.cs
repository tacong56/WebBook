using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.API.Models
{
    public class AuthenticateModel
    {
    }

    public class AccountInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string UrlImage { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public DateTime Dob { get; set; }
    }

    #region: Request
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
        [MaxLength(50, ErrorMessage = "Được được quá 50 ký tự.")]
        [MinLength(4, ErrorMessage = "Tài khoản phải có ít nhất 4 ký tự.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MaxLength(50, ErrorMessage = "Được được quá 50 ký tự.")]
        [MinLength(4, ErrorMessage = "Mật khẩu phải có ít nhất 4 ký tự.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterRequest
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Được được quá 50 ký tự.")]
        public string FirstName { get; set; }
        public string Email { get; set; }
        [MaxLength(50, ErrorMessage = "Được được quá 50 ký tự.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
        [MaxLength(50, ErrorMessage = "Được được quá 50 ký tự.")]
        [MinLength(4, ErrorMessage = "Tài khoản phải có ít nhất 4 ký tự.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
        [MaxLength(50, ErrorMessage = "Được được quá 50 ký tự.")]
        [MinLength(4, ErrorMessage = "Tài khoản phải có ít nhất 4 ký tự.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
        [MaxLength(50, ErrorMessage = "Được được quá 50 ký tự.")]
        [MinLength(4, ErrorMessage = "Tài khoản phải có ít nhất 4 ký tự.")]
        public string Password_Repeat { get; set; }
        public string PhoneNumber { get; set; }
        public string UrlImage { get; set; }
        public int? ImageID { get; set; }
        public int RoleId { get; set; }
        public DateTime? Dob { get; set; }
    }

    public class AccountSearchRequest : BaseRequest
    {
        public int? RoleId { get; set; }
    }

    public class AccountUpdateRequest
    {
        public int Id { get; set; }
        public int? ImageId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Dob { get; set; }
    }
    #endregion

    #region: Response
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string Token { get; set; }
        public string Address { get; set; }

        public AuthenticateResponse() { }

        public AuthenticateResponse(AppUser user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Email = user.Email;
            Dob = user.Dob;
            PhoneNumber = user.PhoneNumber;
            Token = token;
            Address = user.Address;
        }

        public AuthenticateResponse(AppUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            Email = user.Email;
            Dob = user.Dob;
            PhoneNumber = user.PhoneNumber;
        }
    }

    public class AccountInfoResponseData : AccountInfo
    {

    }

    public class AccountDataResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ImageId { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public bool IsDelete { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    #endregion
}
