using System;
using PaymentProcessor.Core.Interfaces;
using PaymentProcessor.Providers;

namespace PaymentProcessor.Infrastructure.Factories
{
    public class PaymentProcessorFactory<T1, T2> where T1 : IPaymentRequest where T2 : IPaymentResponse
    {
        private readonly string _processor;

        public PaymentProcessorFactory(string processor)
        {
            _processor = processor;
        }

        public IPaymentProcessor<T1, T2> GetPaymentProcessor()
        {
            switch (_processor)
            {
                case "mc":
                    return new Payflow<T1, T2>();
                case "vs":
                    return new AmazonPay<T1, T2>();
                default:
                    throw new Exception("Unknown PaymentProcessor type requested");
            }  
        }
    }
}
