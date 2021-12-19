using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface IUserService
    {
        Task<ResponseData<AuthenticateResponse>> Authenticate(AuthenticateRequest request);
        Task<IEnumerable<AppUser>> GetAll();
        Task<ResponseData<AuthenticateResponse>> GetById(GetDetailRequest request);
        Task<ResponseData<AccountInfoResponseData>> AccountInfo();
        Task<ResponseData<string>> Register(RegisterRequest request);
        Task<ResponseData<string>> Update(AccountUpdateRequest request);
    }
}
