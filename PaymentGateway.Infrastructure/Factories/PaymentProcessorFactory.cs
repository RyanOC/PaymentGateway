﻿using System;
using PaymentGateway.Core.Interfaces;
using PaymentGateway.Providers;

namespace PaymentGateway.Infrastructure.Factories
{
    public class PaymentProcessorFactory<T1, T2> where T1 : IPaymentRequest where T2 : IPaymentResponse
    {
        private readonly string _processor;

        public PaymentProcessorFactory(string processor)
        {
            _processor = processor;
        }

        public IPaymentGateway<T1, T2> GetPaymentProcessor()
        {
            switch (_processor)
            {
                case "mc":
                    return new Payflow<T1, T2>();
                case "vs":
                    return new AmazonPay<T1, T2>();
                default:
                    throw new Exception("Unknown PaymentGateway type requested");
            }  
        }
    }
}
