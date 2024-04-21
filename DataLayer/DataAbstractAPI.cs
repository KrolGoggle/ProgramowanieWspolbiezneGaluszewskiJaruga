using System;
using System.Collections.Generic;
using System.Linq;
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

        public abstract int BoardWidth();
        public abstract int BoardLength();

    }

}
