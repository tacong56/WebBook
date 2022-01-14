using System;
using System.ComponentModel.DataAnnotations.Schema;
using TANGOCCONG.ANUIShop.Data.Enums;

namespace TANGOCCONG.ANUIShop.Data.Entities
{
    public class Transaction
    {
        public int Id { set; get; }
        public DateTime TransactionDate { set; get; }
        public string ExternalTransactionId { set; get; }
        public decimal Amount { set; get; }
        public decimal Fee { set; get; }
        public string Result { set; get; }
        public string Message { set; get; }
        public TransactionStatus Status { set; get; }
        public string Provider { set; get; }
        public int OrderId { get; set; }
        public int TransactionID { get; set; }
        public string TransactionCode { get; set; }
        public int PaymentMethod { get; set; }
        public string BankCode { get; set; }
        public string BankTranNo { get; set; }
        public string CardType { get; set; }
        public string TmnCode { get; set; }
        [NotMapped]
        public int UserId { get; set; }
        [NotMapped]
        public AppUser AppUser { get; set; }

    }
}
