using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface IAccountService
    {
        Task<int> Delete(int id);
        Task<ResponseData<AccountDataResponse>> Detail(int id);
        Task<ResponseData<string>> Create(RegisterRequest request);
        string ChangePassword(ChangePasswordRequest request);
        string LockAccount(int id);
        Task<ResponseData<int>> Update(AccountUpdateRequest request);
        Task<PaginationResult<AccountDataResponse>> GetPaging(AccountSearchRequest request);
    }
}
