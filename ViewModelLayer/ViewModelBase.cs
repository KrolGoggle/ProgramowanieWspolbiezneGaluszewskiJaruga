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

        private int BallsToAdd;

        public ICommand CommandStart { get; set; }
        public ICommand CommandStop { get; set; }

        private bool isRunning = true;

        public ObservableCollection<IModelPoolBall> PoolBalls => modelAPI.createVisibleBalls();

        public ViewModelBase()
        {
            modelAPI = ModelAbstractAPI.createModelAPI();
            CommandStart = new RelayCommand(startSimulation, canStart);
            CommandStop = new RelayCommand(stopSimulation, canStop); 
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public int ballsToAdd
        { 
            get { return BallsToAdd; }
            set
            {
                if(BallsToAdd != value)
                {
                    BallsToAdd = value;
                    RaisePropertyChanged(nameof(BallsToAdd));
                }
            }

        }

        // Property indicating if the simulation is running
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                // Update the running flag and notify property change
                if (isRunning != value)
                {
                    isRunning = value;
                    RaisePropertyChanged(nameof(IsRunning));
                }
            }
        }

        // Method to start the simulation
        private void startSimulation(object parameter)
        {
            if (BallsToAdd > 0)      // If there are balls to create
            {
                // Create balls in the model layer and update the view
                modelAPI.createBalls(BallsToAdd);
                RaisePropertyChanged(nameof(PoolBalls));
                IsRunning = false;      // Set running flag to false
                // Notify commands to re-evaluate if they can be executed
                ((RelayCommand)CommandStart).RaiseCanExecuteChanged();
                ((RelayCommand)CommandStop).RaiseCanExecuteChanged();
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

        // Method to check if the start command can be executed
        private bool canStart(object parameter)
        {
            return IsRunning;
        }

        // Method to check if the reset command can be executed
        private bool canStop(object parameter)
        {
            return !IsRunning;
        }
        protected virtual void RaisePropertyChanged( [CallerMemberName] string propertyName = null) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}
