using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TANGOCCONG.ANUIShop.API.Payment.Model
{
    public class OnePayRequestData
    {
        public string vpc_Version = "2";
        public string vpc_Currency = "VND";
        public string vpc_Command = "pay";
        public string vpc_Locale = "vn";
        public string vpc_AccessCode { get; set; }
        public string vpc_Merchant { get; set; }

        public string vpc_ReturnURL { get; set; }

        public string vpc_MerchTxnRef { get; set; }
        public string vpc_OrderInfo { get; set; }
        public string vpc_Amount { get; set; }
        public string vpc_TicketNo { get; set; }
        public string AgainLink { get; set; }
        public string Title { get; set; }
        public string vpc_SecureHash { get; set; }
        public string vpc_Customer_Phone { get; set; }
        public string vpc_Customer_Email { get; set; }
        public string vpc_Customer_Id { get; set; }
    }
}
