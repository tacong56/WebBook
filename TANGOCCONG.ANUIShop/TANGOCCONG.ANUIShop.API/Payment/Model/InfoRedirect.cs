using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TANGOCCONG.ANUIShop.API.Payment.Model
{
    public class InfoRedirect
    {
        public string AgainLink { get; set; }
        public string Title { get; set; }
        public string ReturnURL { get; set; }
        public string SecureHash { get; set; }
    }
}
