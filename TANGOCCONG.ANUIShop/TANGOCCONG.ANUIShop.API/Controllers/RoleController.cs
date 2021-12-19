using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    public class RoleController : MyController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            var result = await _roleService.GetList();
            return Ok(result);
        }
    }
}
