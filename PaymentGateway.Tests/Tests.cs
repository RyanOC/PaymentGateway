using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;

using PaymentProcessor.Core.Models;
using PaymentProcessor.Infrastructure.Factories;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public async Task TestMethod01()
        {
            var ret = await TestHttpMock01();
            Assert.AreEqual(ret, "{'token' : '33300000-0000-0000-0000-000000000000'}");
        }

        [TestMethod]
        public async Task TestMethod02()
        {
            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("http://www.mocky.io/v2/59836865100000030ba850e0")
                .Respond("application/json", "{'Token' : '33300000-0000-0000-0000-000000000000'}");

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            var factory = new PaymentProcessorFactory<PaymentRequest, PaymentResponse>("mc");
            var ccProcessor = factory.GetPaymentProcessor();

            var paymentRequest = new PaymentRequest
            {
                AccountNumber = "1111222233334444"
            };

            var response = await ccProcessor.AuthorizeAsync(mockHttp.ToHttpClient(), paymentRequest);

            Assert.AreEqual(response.Token, "33300000-0000-0000-0000-000000000000");
        }

        [TestMethod]
        public async Task IntegrationTest01()
        {
            // processor can change depending on provider fees per cart type, or processor outages.
            // This can make it easy to switch providers easily with simple configurations.
            var factory = new PaymentProcessorFactory<PaymentRequest, PaymentResponse>("mc");
            var ccProcessor = factory.GetPaymentProcessor();

            var paymentRequest = new PaymentRequest
            {
                AccountNumber = "1111222233334444"
            };

            var authResponse = await ccProcessor.AuthorizeAsync(new System.Net.Http.HttpClient(), paymentRequest);
            var captResponse = await ccProcessor.CaptureAsync(new System.Net.Http.HttpClient(), paymentRequest);

            // Console.WriteLine($"Auth Token: {authResponse.Token}");
            // Console.WriteLine($"Capt Token: {captResponse.Token}");
        }

        private async Task<string> TestHttpMock01()
        {
            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("http://localhost/api/gettoken/*").Respond("application/json", "{'token' : '33300000-0000-0000-0000-000000000000'}");

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            var response = await client.GetAsync("http://localhost/api/gettoken/1234");

            var json = await response.Content.ReadAsStringAsync();

            // No network connection required
            return json;
        }

        private async Task<string> TestHttpMock02()
        {
            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("http://www.mocky.io/v2/59836865100000030ba850e0")
                .Respond("application/json", "{'Token' : '33300000-0000-0000-0000-000000000000'}");

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            var factory = new PaymentProcessorFactory<PaymentRequest, PaymentResponse>("mc");
            var ccProcessor = factory.GetPaymentProcessor();

            var paymentRequest = new PaymentRequest
            {
                AccountNumber = "1111222233334444"
            };

            var response = await ccProcessor.AuthorizeAsync(mockHttp.ToHttpClient(), paymentRequest);

            return response.Token;
        }
    }
}
