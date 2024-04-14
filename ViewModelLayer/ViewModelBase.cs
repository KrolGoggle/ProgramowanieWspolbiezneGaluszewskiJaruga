using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModelLayer
{
    public class ViewModelBase : INotifyPropertyChanged 
    {   


        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void RaisePropertyChangerd( [CallerMemberName] string propertyName = null) { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
