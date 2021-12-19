using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    public class ProductController : MyController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Insert(ProductInsertRequest request)
        {
            var result = await _productService.Create(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> update(ProductInsertRequest request)
        {
            var result = await _productService.Update(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var request = new GetDetailByIntRequest()
            {
                Id = id
            };
            var result = await _productService.Detail(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("get-paging")]
        public async Task<IActionResult> GetPaging(int limit, int page, int? categoryId, string keyword)
        {
            var request = new ProductPagingRequest()
            {
                CategoryId = categoryId,
                Keyword = keyword,
                Limit = limit,
                Page = page
            };
            var result = await _productService.GetPaging(request);
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
            var result = await _productService.Delete(request);
            if (result == 1) return Ok(result);
            return NotFound(result);
        }
    }
}
