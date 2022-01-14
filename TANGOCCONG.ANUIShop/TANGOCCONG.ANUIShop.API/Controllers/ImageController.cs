using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    public class ImageController : MyController
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [AllowAnonymous]
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            var result = await _imageService.UploadImage(request);

            if (result > 0) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("upload-image-product")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request, [FromForm] bool IsMain)
        {
            var result = await _imageService.UploadImage(request);

            if (result > 0) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("uploads")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImages([FromForm] UploadImagesRequest request)
        {
            var result = await _imageService.UploadImages(request);

            if (result != null) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteImage(int[] imageIds, bool isProduct)
        {
            var result = await _imageService.DeleteImage(imageIds, isProduct);

            if (result > 0) Ok(result);

            return NotFound(result);
        }
    }
}
