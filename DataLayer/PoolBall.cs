using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataLayer
{
    public class PoolBall
    {
        private Vector2 position;
        private Vector2 velocity;
        private Thread? thread;
        private Stopwatch sw;
        private int period = 4;
        public int mass = 3;
        private object lockObject = new object();

        public PoolBall(int x, int y)
        {
            position.X = x;
            position.Y = y;
            randomVelocity();
            createTask();

        }

        public float Position_x
        {
            get { lock (lockObject) { return position.X; } }
            set { lock (lockObject) { position.X = value; } }
        }

        public float Position_y
        {
            get { lock (lockObject) { return position.Y; } }
            set { lock (lockObject) { position.Y = value; } }
        }

        public float Velocity_x
        {
            get { lock (lockObject) { return velocity.X; } }
            set { lock (lockObject) { velocity.X = value; } }
        }

        public float Velocity_y   
        {
            get { lock (lockObject) { return velocity.Y; } }
            set { lock (lockObject) { velocity.Y = value; } }
        }

        public int Mass { get { return mass; } set { mass = value; } }
        private void move()
        {

            Position_x +=   velocity.X;
            Position_y +=   velocity.Y;

           

            OnPositionChange();

        }

        public void randomVelocity()
        {
            Random rnd = new Random();
            velocity.X = rnd.Next(-3, 4);
            velocity.Y = rnd.Next(-3, 4);

          
            while (velocity.X == 0)
            {
                velocity.X = rnd.Next(-3, 4);
            }
            while (velocity.Y == 0)
            {
                velocity.Y = rnd.Next(-3, 4);
            }

        }

        object moveLock = new object();
        private bool shouldStop = false;

        private void createTask()
        {
           sw = new Stopwatch();

            thread = new Thread(() =>
            {
                int waiting = 0;           

                while (!shouldStop)
                {
                    sw.Restart();    
                    sw.Start();

                    lock (moveLock)
                    {

                        move();

                    }


                    sw.Stop();       

                     if (period - sw.ElapsedMilliseconds > 0)
                    {
                        waiting = period - (int)sw.ElapsedMilliseconds;
                     }
                     else
                     {
                        waiting = 0;
                     }

                   Thread.Sleep(waiting);
                }
            });

            thread.Start();

        }

        public void stopThread() {
            shouldStop = true;
            thread.Join();
        }



        public event EventHandler PositionChange;

        internal void OnPositionChange()
        {
            PositionChange?.Invoke(this, EventArgs.Empty);
        }

    }

    
}
