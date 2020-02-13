using ToDo.ViewModel.Home;

namespace ToDo.View.Home
{
    public partial class MainPage : ViewPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}
