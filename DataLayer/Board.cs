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

        public int Width { get { return width; } }
        public int Lenght { get { return lenght; } }

    }
}
