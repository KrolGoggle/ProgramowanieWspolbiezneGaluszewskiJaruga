using ModelLayer;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using VierModelLayer;

namespace ViewModelLayer
{
    public class ViewModelBase : INotifyPropertyChanged 
    {

        private ModelAbstractAPI modelAPI;

        public ICommand CommandStart { get; set; }
        public ICommand CommandStop { get; set; }
        public ICommand CommandAdd { get; set; }

        public ViewModelBase()
        {
            modelAPI = ModelAbstractAPI.createModelAPI();
            //  CommandStart = new RelayCommand();
            //  CommandStop = new RelayCommand();
            //  CommandAdd = new RelayCommand();
        }




        private void Start(object parameter) {; }

        private void Stop(object parameter) {; }

        private void Add(object parameter) {; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void RaisePropertyChangerd( [CallerMemberName] string propertyName = null) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
