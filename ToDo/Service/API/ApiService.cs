using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Helpers;
using ToDo.Model;
using ToDo.Service.Request;

namespace ToDo.Service.API
{
    public class ApiService : IApiService
    {
        private readonly IRequestService requestService;

        public ApiService()
        {
            requestService = new RequestService();
        }

        public async Task<ServiceResponse<List<ToDoItem>>> GetToDo()
        {
            string requestUrl = $"{UriHelper.Server}{UriHelper.RelativeUrlToDos}";
            return await requestService.Get<dynamic, List<ToDoItem>>(requestUrl, null);
        }
    }
}
