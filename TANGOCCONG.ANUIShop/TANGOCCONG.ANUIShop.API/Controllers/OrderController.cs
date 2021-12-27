using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    public class OrderController : MyController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-paging")]
        public async Task<IActionResult> Get(int limit, int page, string sort, int? userID = null, string keyword = null)
        {
            var result = await _orderService.GetPaging(limit, page, sort, userID, keyword);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Insert(OrderIURequest request)
        {
            var result = await _orderService.Insert(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(OrderIURequest request)
        {
            var result = await _orderService.Update(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList(int userID)
        {
            var result = await _orderService.GetList(userID);
            return Ok(result);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> changeStatus(int id, int status)
        {
            var result = await _orderService.ChangeStatus(id, status);
            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var result = _orderService.Detail(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("getorderdetail")]
        public IActionResult GetOrderDetail(int orderID, int productID)
        {
            var result = _orderService.GetOrderDetail(orderID, productID);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.Delete(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }
    }
}
