using System.ComponentModel;
using System.Numerics;

namespace ModelLayer
{
    public interface IModelPoolBall : INotifyPropertyChanged
    {

        abstract Vector2 Position { get; set; }
        abstract int Radius { get; }

        static IModelPoolBall createBall(Vector2 pos, int r)
        {
            return new ModelPoolBall(pos, r);
        }

    }


    public class BallChaneEventArgs : EventArgs
    {
        public IModelPoolBall Ball { get; internal set; }
    }

}
