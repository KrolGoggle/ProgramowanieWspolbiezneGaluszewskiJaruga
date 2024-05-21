using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public abstract class DataAbstractAPI

    {
        public static DataAbstractAPI createDataAPI()
        {
            return new DataLayer();
        }

        public abstract int GetBoardWidth();
        public abstract int GetBoardLength();

        public abstract int GetRadius();

    }

}
