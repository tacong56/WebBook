using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    public class UsersController : MyController
    {
        private readonly IUserService _userService;
        private readonly ILoggerManager _logger;

        public UsersController(IUserService userService, ILoggerManager logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("info")]
        public async Task<IActionResult> AccountInfo()
        {
            try
            {
                var result = await _userService.AccountInfo();

                if (result != null && result.Error == 0 && result.Data != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                _logger.LogErr(string.Format("Lỗi: {0}", e));
                return BadRequest(e);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var result = await _userService.Register(request);
                    if (result != null && result.Error == 0) return Ok(result);
                    return BadRequest(result);
                }
                catch (Exception ex)
                {
                    _logger.LogErr("Đang ký tài khoản: Lỗi không xác định");
                    throw ex;
                }
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogErr(string.Format("Đăng nhập không thành công với tài khoản {0}", request.UserName));
                return BadRequest(ModelState);
            }

            var result = await _userService.Authenticate(request);

            if (result != null && result.Error == 0 && !string.IsNullOrEmpty(result.Data.Token))
            {
                _logger.LogInfo(string.Format("Đăng nhập thành công với tài khoản {0}", result.Data.UserName));
                return Ok(result);
            }

            _logger.LogErr(string.Format("Đăng nhập không thành công: {0}", result.Msg));
            return BadRequest(result);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update(AccountUpdateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _userService.Update(request);
            if (result != null && result.Error == 0)
            {
                _logger.LogInfo(string.Format("Tài khoản: {0} đã thay đổi thông tin thành công.", request.UserName));
                return Ok(result);
            }

            _logger.LogErr(string.Format("Tài khoản: {0} thay đổi thông tin không thành công.", request.UserName));
            return NotFound(result);
        }
    }
}
