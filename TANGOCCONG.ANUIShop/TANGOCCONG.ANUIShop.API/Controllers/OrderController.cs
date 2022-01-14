using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TANGOCCONG.ANUIShop.API.Interfaces;
using TANGOCCONG.ANUIShop.API.Models;
using TANGOCCONG.ANUIShop.API.Payment.Commons;
using TANGOCCONG.ANUIShop.API.Payment.Model;

namespace TANGOCCONG.ANUIShop.API.Controllers
{
    public class OrderController : MyController
    {
        private readonly IOrderService _orderService;
        public MerchantAccount _merchantAccount;
        public OrderController(IOrderService orderService, IOptions<MerchantAccount> merchantAccount)
        {
            _orderService = orderService;
            _merchantAccount = merchantAccount.Value;
        }

        [AllowAnonymous]
        [HttpGet("get-paging")]
        public async Task<IActionResult> Get(int limit, int page, int? status, DateTime? ngaytu, DateTime? ngayden, string sort, int? userID = null, string keyword = null)
        {
            var result = await _orderService.GetPaging(limit, page, status, ngaytu, ngayden, sort, userID, keyword);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Insert(OrderIURequest request)
        {
            var result = await _orderService.Insert(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [AllowAnonymous]
        [HttpPut("update")]
        public async Task<IActionResult> Update(OrderIURequest request)
        {
            var result = await _orderService.Update(request);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList(int userID)
        {
            var result = await _orderService.GetList(userID);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("update-status/{id}/{status}")]
        public async Task<IActionResult> changeStatus(int id, int status)
        {
            var result = await _orderService.ChangeStatus(id, status);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("detail/{id}")]
        public IActionResult Detail(int id)
        {
            var result = _orderService.Detail(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [AllowAnonymous]
        [HttpGet("getorderdetail")]
        public IActionResult GetOrderDetail(int orderID, int productID)
        {
            var result = _orderService.GetOrderDetail(orderID, productID);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [AllowAnonymous]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.Delete(id);
            if (result != null && result.Error != 1) return Ok(result);
            return NotFound(result);
        }

        [AllowAnonymous]
        [HttpGet("CreateOrderVNPay")]
        public IActionResult CreateOrderVNPay(int orderID, string domainName)
        {
            var result = _orderService.CreateOrderVNPay(_merchantAccount, orderID, domainName);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("receiptPaymentVNPay")]
        public ActionResult receiptPaymentVNPay(string vnp_Amount, string vnp_BankCode, string vnp_BankTranNo, string vnp_CardType,
          string vnp_OrderInfo, string vnp_PayDate, string vnp_ResponseCode, string vnp_TmnCode, string vnp_TransactionNo,
          string vnp_TxnRef, string vnp_SecureHashType, string vnp_SecureHash)
        {
            var urlRedirect = _merchantAccount.UrlReturnClient + "/notify-payment";
            int orderID = 0;
            int TransactionID = 0;
            try
            {
                var stringMessage = "";
                bool checkStatus = false;
                switch (vnp_ResponseCode)
                {
                    case "00":
                        var kq = _orderService.UpdateOrderAfterPayment(int.Parse(vnp_OrderInfo), int.Parse(vnp_TransactionNo), vnp_ResponseCode, (int)PaymentMethod.CardATM,
                            decimal.Parse(vnp_Amount), vnp_BankCode, vnp_BankTranNo, vnp_CardType, vnp_TmnCode);
                        if (kq.Error == 0)
                        {
                            orderID = kq.Data.OrderId;
                            TransactionID = kq.Data.TransactionID;
                            stringMessage = "Thanh toánh đơn hàng thành công";
                        }
                        else stringMessage = "Thanh toán đơn hàng thất bại";
                        break;
                    case "01":
                        stringMessage = "Giao dịch đã tồn tại";
                        break;
                    case "02":
                        stringMessage = "Merchant không hợp lệ (kiểm tra lại vnp_TmnCode)";
                        break;
                    case "03":
                        stringMessage = "Dữ liệu gửi sang không đúng định dạng";
                        break;
                    case "04":
                        stringMessage = "Khởi tạo GD không thành công do Website đang bị tạm khóa";
                        break;
                    case "05":
                        stringMessage = "Giao dịch không thành công do: Quý khách nhập sai mật khẩu quá số lần quy định. Xin quý khách vui lòng thực hiện lại giao dịch";
                        break;
                    case "13":
                        stringMessage = "Giao dịch không thành công do Quý khách nhập sai mật khẩu xác thực giao dịch (OTP). Xin quý khách vui lòng thực hiện lại giao dịch.";
                        break;
                    case "07":
                        stringMessage = "Giao dịch bị nghi ngờ là giao dịch gian lận";
                        break;
                    case "09":
                        stringMessage = "Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng.";
                        break;
                    case "10":
                        stringMessage = "Giao dịch không thành công do: Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần";
                        break;
                    case "11":
                        stringMessage = "Giao dịch không thành công do: Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch.";
                        break;
                    case "12":
                        stringMessage = "Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng bị khóa.";
                        break;
                    case "51":
                        stringMessage = "Giao dịch không thành công do: Tài khoản của quý khách không đủ số dư để thực hiện giao dịch.";
                        break;
                    case "65":
                        stringMessage = "Giao dịch không thành công do: Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày.";
                        break;
                    case "08":
                        stringMessage = "Giao dịch không thành công do: Hệ thống Ngân hàng đang bảo trì. Xin quý khách tạm thời không thực hiện giao dịch bằng thẻ/tài khoản của Ngân hàng này.";
                        break;
                    case "99":
                        stringMessage = "Các lỗi khác (lỗi còn lại, không có trong danh sách mã lỗi đã liệt kê)";
                        break;
                    default:
                        break;
                }
                // Check vnp_TmnCode nếu 00 mới được update, ngược lại thì k https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                return RedirectPermanent(urlRedirect + "?orderID=" + orderID + "&transactionID=" + TransactionID + "&msg=" + HttpUtility.UrlEncode(stringMessage));
            }
            catch (Exception ex)
            {
                return RedirectPermanent(urlRedirect + "?orderID=0&transactionID=0&msg=Lỗi:" + HttpUtility.UrlEncode(ex.Message));
            }
        }

        //dashboard
        [HttpGet("get-d-order")]
        public IActionResult DOrder()
        {
            var result = _orderService.DOrder();
            return Ok(result);
        }

        [HttpGet("get-pie-order")]
        public IActionResult DPieOrder(DateTime? ngaytu, DateTime? ngayden)
        {
            var result = _orderService.DPieOrder(ngaytu, ngayden);
            return Ok(result);
        }

        [HttpGet("top-product")]
        public IActionResult TopProduct(DateTime? ngaytu, DateTime? ngayden)
        {
            var result = _orderService.TopProduct(ngaytu, ngayden);
            return Ok(result);
        }

        //Transaction
        [HttpGet("get-paging-tran")]
        public async Task<IActionResult> GetTransaction(int limit, int page, DateTime? ngaytu, DateTime? ngayden, string sort)
        {
            var result = await _orderService.GetPagingTran(limit, page, ngaytu, ngayden, sort);
            return Ok(result);
        }

    }
}
