using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.Core.Interfaces
{
    public interface IPaymentGateway<in T1, T2>
    {
        Task<T2> AuthorizeAsync(HttpClient httpClient, T1 paymentRequest);
        Task<T2> CaptureAsync(HttpClient httpClient, T1 paymentRequest);
    }
}
