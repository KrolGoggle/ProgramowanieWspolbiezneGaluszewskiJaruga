namespace DataLayer
{
    public class PoolBall
    {
        private int x;
        private int y;
        private int[] motionVector;
        static int r = 1;
        static double density = 7.874;

        public PoolBall(int x, int y) {
            this.x = x;
            this.y = y;
            this.motionVector = new int[2];
            Random rnd = new Random();
            this.motionVector[0] = rnd.Next(-10, 10);
            this.motionVector[1]= rnd.Next(-10, 10);
        }

        public void motion(int T) {
            this.x += motionVector[0] * T;
            this.y += motionVector[1] * T;
        } 

    }
}
