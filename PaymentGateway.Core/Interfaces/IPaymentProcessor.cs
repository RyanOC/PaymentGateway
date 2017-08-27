using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentProcessor.Core.Interfaces
{
    public interface IPaymentProcessor<in T1, T2>
    {
        Task<T2> AuthorizeAsync(HttpClient httpClient, T1 paymentRequest);
        Task<T2> CaptureAsync(HttpClient httpClient, T1 paymentRequest);
    }
}
