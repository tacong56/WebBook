using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
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
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ANUIShopDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageService _imageService;
        public UserService(IOptions<AppSettings> appSettings, UserManager<AppUser> userManager, ANUIShopDbContext context,
             SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IHttpContextAccessor httpContextAccessor,
             IImageService imageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _imageService = imageService;
        }

        public async Task<ResponseData<AccountInfoResponseData>> AccountInfo()
        {
            Claim claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == Utilities.USER_ID);

            var userId = claim.Value;
            //Get user by id
            var user = await _userManager.FindByIdAsync(userId);
            //Get role
            var roleName = await _userManager.GetRolesAsync(user);
            var role = _context.AppRoles.FirstOrDefault(x => x.Name == roleName[0]);
            var image = _context.Images.FirstOrDefault(x => x.Id == user.ImageId);

            AccountInfoResponseData acc = new AccountInfoResponseData()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Dob = user.Dob,
                Address = user.Address,
                RoleId = role.Id,
                RoleName = role.Name,
                UrlImage = image?.UrlPath,
            };

            return new SuccessResponseData<AccountInfoResponseData>("", acc);
        }

        public async Task<ResponseData<AuthenticateResponse>> Authenticate(AuthenticateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ErrorResponseData<AuthenticateResponse>("Tài khoản không tồn tại.");

            var login = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!login.Succeeded)
                return new ErrorResponseData<AuthenticateResponse>("Tài khoản hoặc mật khẩu không chính xác.");

            var roles = await _userManager.GetRolesAsync(user);
            // authentication successful so generate jwt token
            string token = generateJwtToken(user, roles);
            var resData = new AuthenticateResponse(user, token);

            return new SuccessResponseData<AuthenticateResponse>("", resData);
        }

        public Task<IEnumerable<AppUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<AuthenticateResponse>> GetById(GetDetailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
                return new ErrorResponseData<AuthenticateResponse>("Tài khoản không tồn tại.");

            var resData = new AuthenticateResponse(user);

            return new SuccessResponseData<AuthenticateResponse>("", resData);
        }

        public async Task<ResponseData<string>> Register(RegisterRequest request)
        {
            var existUserByName = await _userManager.FindByNameAsync(request.UserName);
            var existUserByEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existUserByName != null) return new ErrorResponseData<string>("Tài khoản đã tồn tại");
            if (existUserByEmail != null) return new ErrorResponseData<string>("Email đã được sử dụng");
            if (request.Password == request.Password_Repeat) return new ErrorResponseData<string>("Mật khẩu không trùng khớp");

            var user = new AppUser()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = Utilities.HassPass(request.Password),
                PhoneNumber = request.PhoneNumber,
                Dob = request.Dob.Value
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return new SuccessResponseData<string>("Đăng ký thành công");
            }
            else return new ErrorResponseData<string>("Đăng ký thất bại");
        }

        public async Task<ResponseData<string>> Update(AccountUpdateRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return new ErrorResponseData<string>("Tài khoản không tồn tại");
            if (request.ImageId != null && request.ImageId != user.ImageId)
            {
                await _imageService.DeleteImage(new int[] { user.ImageId }, false);
                user.ImageId = request.ImageId.Value;
            }

            user.Email = request.Email;
            user.LastName = request.LastName;
            user.FirstName = request.FirstName;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;
            user.Dob = request.Dob;
            user.Email = request.Email;

            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded) return new SuccessResponseData<string>("Đổi thông tin tài khoản thành công");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new ErrorResponseData<string>("Đổi thông tin tài khoản không thành công");
        }

        private string generateJwtToken(AppUser user, dynamic roles)
        {
            // generate token that is valid for 1 hours
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new[]
            {
                new Claim(Utilities.USER_ID, user.Id.ToString()),
                new Claim(Utilities.USER_EMAIL, user.Email),
                new Claim(Utilities.FIRST_NAME, user.FirstName),
                new Claim(Utilities.LAST_NAME, user.LastName),
                new Claim(Utilities.USER_ROLE, string.Join(";",roles)),
                new Claim(Utilities.USER_NAME, user.UserName)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
