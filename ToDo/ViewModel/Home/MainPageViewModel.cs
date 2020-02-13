using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using ToDo.Model;
using ToDo.Service.Data;
using ToDo.Helpers;
using System.Net;
using System.Windows.Input;
using Xamarin.Forms;

namespace ToDo.ViewModel.Home
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IDataService dataService;

        public MainPageViewModel()
        {
            dataService = new DataService();
            ReloadCommand = new Command(async () => await OnExecuteReloadCommand());
        }

        public override async Task OnAppeared()
        {
            IsBusy = true;
            await GetToDos();
            IsBusy = false;
        }

        private async Task GetToDos()
        {
            ServiceResponse<List<ToDoItem>> todoResponse = await dataService.GetToDo();
            if(!todoResponse.IsSuccess)
            {
                ErrorMessage = ErrorMessages.NetworkErrorMessage;
                HasData = false;
            }
            else if(todoResponse.Exception != null)
            {
                ErrorMessage = string.IsNullOrEmpty(todoResponse.Exception.Message) ? ErrorMessages.GenericErrorMessage : todoResponse.Exception.Message;
                HasData = false;
            }
            else if (todoResponse.ResponseCode != HttpStatusCode.OK || todoResponse.Result == null)
            {
                ErrorMessage = ErrorMessages.GenericErrorMessage;
                HasData = false;
            }
            else
            {
                List<ToDoItem> todos = todoResponse.Result;
                if(todos.Count == 0)
                {
                    ErrorMessage = ErrorMessages.DataNotAvailable;
                    HasData = false;
                }
                else
                {
                    ErrorMessage = string.Empty;
                    ToDoItems = new ObservableCollection<ToDoItem>(todos);
                    HasData = true;
                }
            }
        }

        private async Task OnExecuteReloadCommand()
        {
            await OnAppeared();
        }

        #region Commands and Properties
        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                }
            }
        }

        private bool _hasData;
        public bool HasData
        {
            get
            {
                return _hasData;
            }
            set
            {
                if (_hasData != value)
                {
                    _hasData = value;
                    OnPropertyChanged(nameof(HasData));
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        ObservableCollection<ToDoItem> _toDoItems;
        public ObservableCollection<ToDoItem> ToDoItems
        {
            get
            {
                return _toDoItems;
            }
            set
            {
                _toDoItems = value;
                OnPropertyChanged(nameof(ToDoItems));
            }
        }

        public ICommand ReloadCommand { private set; get; }
        #endregion
    }
}
