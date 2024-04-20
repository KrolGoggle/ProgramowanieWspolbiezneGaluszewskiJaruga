using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Board
    {
        public static int width = 1000;
        public static int lenght = 750;

        public Board() {
        }

        public int BoardWidth { get { return width; } }
        public int BoardLenght { get { return lenght; } }

    }
}
