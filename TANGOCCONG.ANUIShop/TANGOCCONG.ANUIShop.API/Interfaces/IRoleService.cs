using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Objects;

namespace TANGOCCONG.ANUIShop.API.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDataReponse>> GetList();
    }
}
