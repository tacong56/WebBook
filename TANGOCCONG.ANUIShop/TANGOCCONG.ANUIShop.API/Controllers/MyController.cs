using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {

    }
}
