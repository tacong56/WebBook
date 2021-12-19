using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    public class CartsController : MyController
    {
        private readonly ICartsService _cartsService;
        public CartsController(ICartsService cartService)
        {
            _cartsService = cartService;
        }

        [HttpGet("get-paging")]
        public async Task<IActionResult> Get(int limit, int page, string sort,int? userID, string keyword)
        {
            var result = await _cartsService.GetPaging(limit, page, sort,userID, keyword);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Insert(CartsIURequest request)
        {
            var result = await _cartsService.Insert(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CartsIURequest request)
        {
            var result = await _cartsService.Update(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList(int userID)
        {
            var result = await _cartsService.GetList(userID);
            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var result = _cartsService.Detail(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cartsService.Delete(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }
    }
}
