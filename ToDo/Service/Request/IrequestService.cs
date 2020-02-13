using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Model;

namespace ToDo.Service.Request
{
    public interface IRequestService
    {
        Task<ServiceResponse<TResult>> Get<TRequest, TResult>(string url, TRequest body, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null);

        Task<ServiceResponse<TResult>> Put<TRequest, TResult>(string url, TRequest body, Dictionary<string, string> headers = null);

        Task<ServiceResponse<TResult>> Post<TRequest, TResult>(string url, TRequest body, Dictionary<string, string> headers = null);

        Task<ServiceResponse<TResult>> Delete<TRequest, TResult>(string url, TRequest body, Dictionary<string, string> headers = null);
    }
}
