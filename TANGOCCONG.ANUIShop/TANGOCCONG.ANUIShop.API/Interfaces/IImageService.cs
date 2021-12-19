using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface IImageService
    {
        Task<int> UploadImage(UploadImageRequest request);
        Task<int> UploadImageProduct(UploadImageRequest request, bool isMain);
        Task<List<int>> UploadImages(UploadImagesRequest request);
        Task<int> DeleteImage(int[] imageId, bool isProduct);
    }
}
