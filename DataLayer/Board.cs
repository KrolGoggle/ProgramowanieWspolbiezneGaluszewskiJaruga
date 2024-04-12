using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Board
    {
        private int width;
        private int lenght;

        public Board(int width, int lenght) {
            this.width = width;
            this.lenght = lenght;
        }

        public int Width { get { return width; } }
        public int Lenght { get { return lenght; } }

    }
}
