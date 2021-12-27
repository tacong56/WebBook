using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TANGOCCONG.ANUIShop.API.Payment.Commons;
using TANGOCCONG.ANUIShop.API.Payment.Model;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.API.Payment.VNPay
{
    public class VNPay
    {
        public string Payment(Order order, MerchantAccount merchantAccount,InfoRedirect infoRedirect)
        {
            string vnp_Returnurl = infoRedirect.ReturnURL;
            string vnp_TmnCode = merchantAccount.TmnCode;
            string vnp_HashSecret = merchantAccount.HashSecret;

            VNPayLibrary vnpay = new VNPayLibrary();
            vnpay.AddRequestData("vnp_Version", Constants.GATEWAY_VERSION);
            vnpay.AddRequestData("vnp_Command", Constants.GATEWAY_COMMAND);
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Locale", Constants.GATEWAY_LOCALE);
            vnpay.AddRequestData("vnp_CurrCode", Constants.GATEWAY_CURRENCY);
            vnpay.AddRequestData("vnp_TxnRef", order.Id + DateTime.Now.ToString("ddMMyyyyHHmmss"));
            vnpay.AddRequestData("vnp_OrderInfo", order.Id.ToString());
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_Amount", (order.TotalAmount * 100).ToString());
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            //vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            //vnpay.AddRequestData("vnp_BankCode", "NCB");

            string paymentUrl = vnpay.CreateRequestUrl(merchantAccount.GateWay_VNPAY, vnp_HashSecret);
            return paymentUrl;
        }
    }
}
