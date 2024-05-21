using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class DataLayer : DataAbstractAPI
    {
        public override int GetBoardLength()
        {
            return Board.height;
        }

        public override int GetBoardWidth()
        {
            return Board.width;
        }

        public override int GetRadius()
        {
            return PoolBall.radius;
        }

    }
}
