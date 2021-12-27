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
    public class CategoryController : MyController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-paging")]
        public async Task<IActionResult> Get(int limit, int page, string keyword, int? level)
        {
            CategoryPagingRequest request = new CategoryPagingRequest()
            {
                Keyword = keyword,
                Limit = limit,
                Page = page,
                Level = level
            };
            var result = await _categoryService.GetPaging(request);
            return Ok(result);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList(int? level)
        {
            CategoryGetListRequest request = new CategoryGetListRequest()
            {
                Level = level
            };
            var result = await _categoryService.GetList(request);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getlist-no-auth")]
        public async Task<IActionResult> GetListNoAuth(int? level)
        {
            CategoryGetListRequest request = new CategoryGetListRequest()
            {
                Level = level
            };
            var result = await _categoryService.GetList(request);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Insert(CategoryInsertRequest request)
        {
            var result = await _categoryService.Insert(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(CategoryInsertRequest request)
        {
            var result = await _categoryService.Update(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var request = new CategoryDetailRequest()
            {
                Id = id
            };
            var result = _categoryService.Detail(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new CategoryDetailRequest()
            {
                Id = id
            };
            var result = await _categoryService.Delete(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }
    }
}
