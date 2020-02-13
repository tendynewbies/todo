using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Model;
using ToDo.Service.API;

namespace ToDo.Service.Data
{
    public class DataService : IDataService
    {
        private readonly IApiService apiService;

        public DataService()
        {
            apiService = new ApiService();
        }

        public async Task<ServiceResponse<List<ToDoItem>>> GetToDo()
        {
            return await apiService.GetToDo();
        }
    }
}
