using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Comons;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.Data.EF;
using TANGOCCONG.ANUIShop.Data.Entities;
using static TANGOCCONG.ANUIShop.API.Comons.Utilities;

namespace TANGOCCONG.ANUIShop.API.Services
{
    public class ImageService : IImageService
    {
        private readonly ANUIShopDbContext _context;
        private readonly IFileStorageService _storageService;

        public ImageService(ANUIShopDbContext context, IFileStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> UploadImage(UploadImageRequest request)
        {
            if (request != null)
            {
                try
                {
                    Image image = new Image()
                    {
                        Name = "Ảnh đại diện",
                        Caption = "Ảnh đại diện",
                        TimeCreated = DateTime.Now,
                        Size = request.Image.Length,
                        IsMain = true,
                        IsProduct = false,
                        UrlPath = await this.SaveFile(request.Image),
                    };
                    var result = _context.Images.Add(image);
                    await _context.SaveChangesAsync();

                    return result.Entity.Id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            return -1;
        }

        public async Task<int> UploadImageProduct(UploadImageRequest request, bool isMain)
        {
            if (request != null)
            {
                try
                {
                    Image image = new Image()
                    {
                        Name = "Ảnh đại diện",
                        Caption = "Ảnh đại diện",
                        TimeCreated = DateTime.Now,
                        Size = request.Image.Length,
                        IsMain = isMain,
                        IsProduct = true,
                        UrlPath = await this.SaveFile(request.Image),
                    };
                    var result = _context.Images.Add(image);
                    await _context.SaveChangesAsync();

                    return result.Entity.Id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }

            return -1;
        }

        public async Task<List<int>> UploadImages(UploadImagesRequest request)
        {
            var Ids = new List<int>();
            if (request != null)
            {
                for (var i = 0; i < request.Images.Count; i++)
                {
                    var item = request.Images[i];
                    try
                    {
                        Image image = new Image()
                        {
                            Name = "Ảnh sản phẩm",
                            Caption = "Ảnh sản phẩm",
                            TimeCreated = DateTime.Now,
                            Size = item.Image.Length,
                            IsProduct = item.IsMain != null ? item.IsMain.Value : false,
                            UrlPath = await this.SaveFile(item.Image),
                        };
                        var result = _context.Images.Add(image);
                        Ids.Add(result.Entity.Id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        throw;
                    }
                }
                await _context.SaveChangesAsync();
                return Ids;
            }

            return Ids;
        }

        public async Task<int> DeleteImage(int[] imageIds, bool isProduct)
        {
            if (imageIds.Length > 0)
            {
                foreach (var item in imageIds)
                {
                    try
                    {
                        var image = _context.Images.FirstOrDefault(x => x.Id == item && x.IsProduct == isProduct);
                        if (image != null)
                        {
                            await _storageService.DeleteFileAsync(image.UrlPath);
                            _context.Images.Remove(image);
                            await _context.SaveChangesAsync();
                        }

                        return 0;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return -1;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"Thumbnail_{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);

            return SystemContant.USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}
