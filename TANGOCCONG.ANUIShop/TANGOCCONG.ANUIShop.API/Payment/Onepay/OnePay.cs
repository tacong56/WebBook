using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Payment.Commons;
using TANGOCCONG.ANUIShop.API.Payment.Model;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.API.Payment.Onepay
{
    public class OnePay
    {
        public string Payment(Order order, MerchantAccount merchantAccount, InfoRedirect infoRedirect, string vpc_Url)
        {
            VPCRequest vpc = new VPCRequest(vpc_Url);
            vpc.SetSecureSecret(merchantAccount.HashCode);
            vpc.AddDigitalOrderField("Title", infoRedirect.Title);
            vpc.AddDigitalOrderField("vpc_Locale", Constants.GATEWAY_LOCALE);
            vpc.AddDigitalOrderField("vpc_Version", Constants.GATEWAY_VERSION);
            vpc.AddDigitalOrderField("vpc_Command", Constants.GATEWAY_COMMAND);
            vpc.AddDigitalOrderField("vpc_Merchant", merchantAccount.MerchantID);
            vpc.AddDigitalOrderField("vpc_AccessCode", merchantAccount.AccessCode);
            vpc.AddDigitalOrderField("vpc_MerchTxnRef", order.Id + DateTime.Now.ToString("ddMMyyyyHHmmss"));
            vpc.AddDigitalOrderField("vpc_OrderInfo", order.Id.ToString());
            vpc.AddDigitalOrderField("vpc_Amount", (order.TotalAmount * 100).ToString());
            vpc.AddDigitalOrderField("vpc_Currency", Constants.GATEWAY_CURRENCY);
            vpc.AddDigitalOrderField("vpc_ReturnURL", infoRedirect.ReturnURL);
            //vpc.AddDigitalOrderField("vpc_SHIP_Street01", "194 Tran Quang Khai");
            //vpc.AddDigitalOrderField("vpc_SHIP_Provice", "Hanoi");
            //vpc.AddDigitalOrderField("vpc_SHIP_City", "Hanoi");
            //vpc.AddDigitalOrderField("vpc_SHIP_Country", "Vietnam");
            //vpc.AddDigitalOrderField("vpc_Customer_Phone", order.PhoneNumber);
            vpc.AddDigitalOrderField("vpc_Customer_Email", "");
            //vpc.AddDigitalOrderField("vpc_Customer_Id", order.FullName);
            vpc.AddDigitalOrderField("vpc_TicketNo", "");
            string url = vpc.Create3PartyQueryString();
            return url;
        }
    }
}
