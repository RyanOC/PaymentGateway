using PaymentGateway.Core.Interfaces;

namespace PaymentGateway.Core.Models
{
    public class PaymentRequest : IPaymentRequest
    {
        public string AccountNumber { get; set; }
    }
}
