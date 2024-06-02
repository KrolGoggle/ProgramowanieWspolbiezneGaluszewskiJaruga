using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ModelLayer
{
    internal class ModelPoolBall : IModelPoolBall, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private int radius;
        Vector2 position;
        private object pos_lock = new object();

        public Vector2 Position
        {
            get { lock (pos_lock) { return position; } }
            set { lock (pos_lock) { position = value; NotifyPropertyChanged(nameof(X)); NotifyPropertyChanged(nameof(Y)); } }
        }

        public double X
        {
            get => Position.X;
        }

        public double Y
        {
            get => Position.Y;
        }

        public int Radius { get => radius; }

        public ModelPoolBall(Vector2 pos, int r)
        {
            this.position = pos;
            this.radius = r;

        }


        private void NotifyPropertyChanged([CallerMemberName] string? propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
