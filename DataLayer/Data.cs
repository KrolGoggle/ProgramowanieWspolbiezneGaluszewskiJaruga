using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    internal class DataLayer : DataAbstractAPI
    {
        public override int BoardLength()
        {
            return Board.height;
        }

        public override int BoardWidth()
        {
            return Board.width;
        }


    }
}
