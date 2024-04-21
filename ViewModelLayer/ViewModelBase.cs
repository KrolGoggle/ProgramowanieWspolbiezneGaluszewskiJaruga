using ModelLayer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace ViewModelLayer
{
    public class ViewModelBase : INotifyPropertyChanged 
    {

        private ModelAbstractAPI modelAPI;
        int ballsToAdd { get; set; }

        public ICommand CommandStart { get; set; }
        public ICommand CommandStop { get; set; }
        public ICommand CommandAdd { get; set; }

        public ObservableCollection<IModelPoolBall> PoolBalls => modelAPI.createVisibleBalls();

        public ViewModelBase()
        {
            modelAPI = ModelAbstractAPI.createModelAPI();
             // CommandStart = new RelayCommand();
            //  CommandStop = new RelayCommand();
             // CommandAdd = new RelayCommand();    
        }

    

        private void startSimulation(object parameter) {
            
            
            
            
            ; }

        private void stopSimulation(object parameter) {
            
            
            
            ; }

        private void Add(object parameter) {

            modelAPI.createPoolBalls(ballsToAdd);
            modelAPI.createVisibleBalls();
            
            
            ; }

        public event PropertyChangedEventHandler PropertyChanged;


        public int BallsToAdd
        { 
            get { return ballsToAdd; }
            set
            {
                if(ballsToAdd != value)
                {
                    ballsToAdd = value;
                    RaisePropertyChangerd(nameof(BallsToAdd));
                }
            }

        }


        protected virtual void RaisePropertyChangerd( [CallerMemberName] string propertyName = null) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
