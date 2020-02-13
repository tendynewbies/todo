using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Model;

namespace ToDo.Service.Data
{
    public interface IDataService
    {
        Task<ServiceResponse<List<ToDoItem>>> GetToDo();
    }
}
