using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TANGOCCONG.ANUIShop.API.Payment.Model
{
    public class MerchantAccount
    {
        public string AccessCode { get; set; }
        public string MerchantID { get; set; }
        public string HashCode { get; set; }
        public string TmnCode { get; set; }
        public string HashSecret { get; set; }
        public string GateWay_VNPAY { get; set; }
    }
}
