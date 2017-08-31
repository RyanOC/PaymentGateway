using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaymentGateway.Core.Interfaces;

namespace PaymentGateway.Providers
{
    public class AmazonPay<T1, T2> : IPaymentGateway<T1, T2> where T1 : IPaymentRequest where T2 : IPaymentResponse
    {
        public async Task<T2> AuthorizeAsync(HttpClient httpClient, T1 paymentRequest)
        {
            if (paymentRequest.AccountNumber.Length != 16)
            {
                throw new Exception("account number invalid");
            }

            string result;

            using (HttpResponseMessage res = await httpClient.GetAsync("http://www.mocky.io/v2/59836865100000030ba850e0"))
            using (HttpContent content = res.Content)
            {
                result = await content.ReadAsStringAsync();
            }

            var resObj = JsonConvert.DeserializeObject(result, typeof(T2));
            return (T2)(IPaymentResponse)resObj;
        }

        public async Task<T2> CaptureAsync(HttpClient httpClient, T1 paymentRequest)
        {
            if (paymentRequest.AccountNumber.Length != 16)
            {
                throw new Exception("account number invalid");
            }

            string result;

            using (HttpResponseMessage res = await httpClient.GetAsync("http://www.mocky.io/v2/59836865100000030ba850e0"))
            using (HttpContent content = res.Content)
            {
                result = await content.ReadAsStringAsync();
            }

            var resObj = JsonConvert.DeserializeObject(result, typeof(T2));
            return (T2)(IPaymentResponse)resObj;
        }
    }
}