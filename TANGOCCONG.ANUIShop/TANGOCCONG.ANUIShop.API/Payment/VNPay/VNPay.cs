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
        public string Payment(Order order, MerchantAccount merchantAccount, InfoRedirect infoRedirect)
        {
            string vnp_Returnurl = infoRedirect.ReturnURL;
            string vnp_TmnCode = merchantAccount.TmnCode;
            string vnp_HashSecret = merchantAccount.HashSecret;

            VNPayLibrary vnpay = new VNPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_OrderInfo", order.Id.ToString());
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", infoRedirect.ReturnURL);
            vnpay.AddRequestData("vnp_Amount", (Convert.ToInt32(order.TotalAmount) *100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_TxnRef", order.Id.ToString());

            string paymentUrl = vnpay.CreateRequestUrl(merchantAccount.GateWay_VNPAY, vnp_HashSecret);
            return paymentUrl;
        }
    }
}
