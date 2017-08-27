using PaymentProcessor.Core.Interfaces;

namespace PaymentProcessor.Core.Models
{
    public class PaymentRequest : IPaymentRequest
    {
        public string AccountNumber { get; set; }
    }
}
