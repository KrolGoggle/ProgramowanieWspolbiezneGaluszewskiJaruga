using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Board
    {
        public static int width = 400;
        public static int height = 250;

        public Board() {
        }

        public int BoardWidth { get { return width; } }
        public int BoardHeight { get { return height; } }

    }
}
