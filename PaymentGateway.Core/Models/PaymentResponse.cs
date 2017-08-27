using PaymentProcessor.Core.Interfaces;

namespace PaymentProcessor.Core.Models
{
    public class PaymentResponse : IPaymentResponse
    {
        public string Token { get; set; }
    }
}
