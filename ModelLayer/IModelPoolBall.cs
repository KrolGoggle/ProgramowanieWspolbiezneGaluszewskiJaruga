using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public abstract class IModelPoolBall
    {

        public abstract float Pos_X { get; set; }
        public abstract float Pos_Y { get; set; }
        public abstract int Radius { get; }

        public static IModelPoolBall createBall(float x, float y, int r)
        {
            return new ModelPoolBall(x, y, r);
        }

    }
}
