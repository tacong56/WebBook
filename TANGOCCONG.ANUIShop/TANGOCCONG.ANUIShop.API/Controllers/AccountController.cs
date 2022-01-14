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
    public class AccountController : MyController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Insert(RegisterRequest request)
        {
            var result = await _accountService.Create(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> update(AccountUpdateRequest request)
        {
            var result = await _accountService.Update(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [AllowAnonymous]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var request = new GetDetailByIntRequest()
            {
                Id = id
            };
            var result = await _accountService.Detail(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("get-paging")]
        public async Task<IActionResult> GetPaging(int limit, int page, string keyword, int? roleId)
        {
            var request = new AccountSearchRequest()
            {
                Keyword = keyword,
                Limit = limit,
                Page = page,
                RoleId = roleId
            };
            var result = await _accountService.GetPaging(request);
            if (result != null) return Ok(result);
            return NotFound(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new GetDetailByIntRequest()
            {
                Id = id
            };
            var result = await _accountService.Delete(id);
            if (result == 1) return Ok(result);
            return NotFound(result);
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword(ChangePasswordRequest request)
        {
            var result = _accountService.ChangePassword(request);
            if (!string.IsNullOrEmpty(result)) return Ok(result);
            return NotFound(result);
        }

        [HttpPost("lock/{id}")]
        public IActionResult LockAccount(int id)
        {
            var result = _accountService.LockAccount(id);
            if (!string.IsNullOrEmpty(result)) return Ok(result);
            return NotFound(result);
        }
    }
}
