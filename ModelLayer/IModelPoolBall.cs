using System.Numerics;

namespace ModelLayer
{
    public abstract class IModelPoolBall
    {

        public abstract Vector2 Position { get; set; }
        public abstract int Radius { get; }

        public static IModelPoolBall createBall(Vector2 pos, int r)
        {
            return new ModelPoolBall(pos, r);
        }

    }
}
