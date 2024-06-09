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
        private int ballsToAdd { get; set; }

        private int currentBalls;
        private bool addEnabled { get; set; }
        public ICommand CommandStart { get; set; }
        public ICommand CommandStop { get; set; }
        public ICommand CommandAdd { get; set; }

        private bool isRunning = true;

        public ObservableCollection<IModelPoolBall> PoolBalls => modelAPI.createVisibleBalls();

        public ViewModelBase()
        {
            modelAPI = ModelAbstractAPI.createModelAPI();

            CommandStop = new RelayCommand(stopSimulation, isAbleToStop);
            CommandAdd = new RelayCommand(Add);

            addEnabled = true;
        }

        private void stopSimulation(object parameter)
        {
            IsAddEnabled = true;
            modelAPI.destroyEveryPoolBall();
            RaisePropertyChanged(nameof(PoolBalls));
            IsRunning = true;


            ((RelayCommand)CommandStop).RaiseCanExecuteChanged();
            ;
        }

        private void Add(object parameter)
        {

            if (ballsToAdd > 0)
            {
                IsAddEnabled = false;
                modelAPI.createPoolBalls(ballsToAdd);
                modelAPI.createVisibleBalls();
                isRunning = false;
                currentBalls = modelAPI.getCurrentVisibleBalls();
                RaisePropertyChanged(nameof(PoolBalls));
                if (currentBalls > 0) { ((RelayCommand)CommandStop).RaiseCanExecuteChanged(); }
            }
            ;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int BallsToAdd
        {
            get { return ballsToAdd; }
            set
            {
                if (ballsToAdd != value)
                {
                    ballsToAdd = value;
                    RaisePropertyChanged(nameof(BallsToAdd));
                }
            }

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

        public bool IsAddEnabled
        {
            get { return addEnabled; }
            set
            {

                if (addEnabled != value)
                {
                    addEnabled = value;
                    RaisePropertyChanged(nameof(IsAddEnabled));
                }
            }
        }

        private bool isAbleToStop(object parameter)
        {
            return !IsRunning;
        }


        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
