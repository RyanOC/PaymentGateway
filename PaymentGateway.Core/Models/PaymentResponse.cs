using PaymentGateway.Core.Interfaces;

namespace PaymentGateway.Core.Models
{
    public class PaymentResponse : IPaymentResponse
    {
        public string Token { get; set; }
    }
}
