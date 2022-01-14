using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Payment.Commons;
using TANGOCCONG.ANUIShop.API.Payment.Model;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    public class ExportStorageController : MyController
    {
        private readonly IExportStorage _exportStorage;
        public ExportStorageController(IExportStorage exportStorage)
        {
            _exportStorage = exportStorage;
        }

        [HttpGet("get-paging")]
        public async Task<IActionResult> Get(int limit, int page, DateTime? ngaytu, DateTime? ngayden, string sort)
        {
            var result = await _exportStorage.GetPaging(limit, page, ngaytu, ngayden, sort);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Insert(InsertUpdateStorage request)
        {
            var result = await _exportStorage.Insert(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(InsertUpdateStorage request)
        {
            var result = await _exportStorage.Update(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var result = _exportStorage.Detail(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _exportStorage.Delete(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }
    }
}
