using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ToDo.ViewModel
{
    public class ViewModel : ILoadable, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isDisposed;

        protected void OnPropertyChanged(string propertyName)
        {
            if (!isDisposed)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual async Task OnAppeared(){}

        public virtual async Task OnDisappearing(){}

        public void Dispose()
        {
            isDisposed = true;
        }
    }
}
