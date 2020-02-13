using System.Threading.Tasks;

namespace ToDo.ViewModel
{
    public interface ILoadable
    {
        /// <summary>
        /// Do heavy operations here, e.g. Loading of data from API
        /// </summary>
        /// <returns></returns>
        Task OnAppeared();

        /// <summary>
        /// Clear memory here
        /// </summary>
        /// <returns></returns>
        Task OnDisappearing();
    }
}
