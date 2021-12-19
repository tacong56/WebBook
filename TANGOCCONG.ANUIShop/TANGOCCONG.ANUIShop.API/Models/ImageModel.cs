using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TANGOCCONG.ANUIShop.API.Models
{
    public class ImageModel
    {
    }

    #region Request
    public class UploadImageRequest
    {
        public IFormFile Image { get; set; }
    }

    public class UploadImagesRequest
    {
        public List<ImageRequest> Images { get; set; }
    }

    public class ImageRequest
    {
        public IFormFile Image { get; set; }
        public bool? IsMain { get; set; }
    }
    #endregion

    #region Response
    public class ImageDataResponse
    {

    }
    #endregion
}
