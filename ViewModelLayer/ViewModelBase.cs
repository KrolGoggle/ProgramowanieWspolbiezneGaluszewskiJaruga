using ModelLayer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace ViewModelLayer
{
    public class ViewModelBase : INotifyPropertyChanged 
    {

        private ModelAbstractAPI modelAPI;
        private int ballsToAdd { get; set; }

        private int currentBalls;

        public ICommand CommandStart { get; set; }
        public ICommand CommandStop { get; set; }

        private bool isRunning = true;

        public ObservableCollection<IModelPoolBall> PoolBalls => modelAPI.createVisibleBalls();

        public ViewModelBase()
        {
            modelAPI = ModelAbstractAPI.createModelAPI();
             
              CommandStop = new RelayCommand(stopSimulation,isAbleToStop);
              CommandAdd = new RelayCommand(Add);    
        }

   
        private void stopSimulation(object parameter) {
            
            modelAPI.destroyEveryPoolBall();
            RaisePropertyChanged(nameof(PoolBalls));
            IsRunning = true;           
            
          
            ((RelayCommand)CommandStop).RaiseCanExecuteChanged();
            ; }

        private void Add(object parameter) {
         
            
            modelAPI.createPoolBalls(ballsToAdd);
            modelAPI.createVisibleBalls();
            isRunning = false;
            currentBalls = modelAPI.getCurrentVisibleBalls();
            RaisePropertyChanged(nameof(PoolBalls));
            if (currentBalls > 0) {((RelayCommand)CommandStop).RaiseCanExecuteChanged(); }


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
                   RaisePropertyChanged(nameof(BallsToAdd));
                }
            }

        // Method to reset the simulation
        private void stopSimulation(object parameter)
        {
            // Remove all balls from the model layer and update the view
            modelAPI.deleteBalls(BallsToAdd);
            RaisePropertyChanged(nameof(PoolBalls));
            IsRunning = true;           // Set running flag to true
            // Notify commands to re-evaluate if they can be executed
            ((RelayCommand)CommandStart).RaiseCanExecuteChanged();
            ((RelayCommand)CommandStop).RaiseCanExecuteChanged();
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
            
                if (isRunning != value)
                {
                    isRunning = value;
                    RaisePropertyChanged(nameof(IsRunning));
                }
            }
        }

        private bool isAbleToStop(object parameter)
        {
            return !IsRunning;
        }


        protected virtual void RaisePropertyChanged( [CallerMemberName] string propertyName = null) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    }
}
