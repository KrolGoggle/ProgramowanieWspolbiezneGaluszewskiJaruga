using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModelLayer
{
    internal class ModelPoolBall :IModelPoolBall, INotifyPropertyChanged
    {
       
    public event PropertyChangedEventHandler? PropertyChanged;

        private float pos_X;
        private float pos_Y;
        private int radius;

        public override float Pos_X { get => pos_X; set { pos_X = value; NotifyPropertyChanged(); } }
        public override float Pos_Y { get => pos_Y; set { pos_Y = value; NotifyPropertyChanged(); } }
        public override int Radius { get => radius; }

        public ModelPoolBall(float X, float Y, int r)
        {
            this.pos_X = X;
            this.pos_Y = Y;
            this.radius = r;

        }
       

        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
