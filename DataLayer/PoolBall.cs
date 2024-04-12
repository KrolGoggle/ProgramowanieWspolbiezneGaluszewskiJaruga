namespace DataLayer
{
    public class PoolBall
    {
        private int x;
        private int y;
        static int r = 1;
        static double density = 7.874;

        public PoolBall(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public int X { get { return x; }
            set { x = value; }
        }
        public int Y { get { return y; }
            set { y = value; }
        }

    }
}
