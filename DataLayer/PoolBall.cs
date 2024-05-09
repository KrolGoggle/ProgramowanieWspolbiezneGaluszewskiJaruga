using System.ComponentModel;
using System.Diagnostics;
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
        private Task? task;
        private Stopwatch sw;
        private int period = 4;


        public PoolBall(int x, int y)
        {
            position.X = x;
            position.Y = y;
            randomVelocity();
            createTask();

        }

        public float Position_x { get { return position.X; } set { position.X = value; } }
        public float Position_y { get { return position.Y; } set { position.Y = value; } }
        public float Velocity_x { get { return velocity.X; } set { velocity.X = value; } }
        public float Velocity_y { get { return velocity.Y; } set { velocity.Y = value; } }




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

        private void createTask()
        {
           sw = new Stopwatch();

            thread = new Thread(() =>
            {
                int waiting = 0;           

                while (true)
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
      
        public event EventHandler PositionChange;

        internal void OnPositionChange()
        {
            PositionChange?.Invoke(this, EventArgs.Empty);
        }

    }

    
}
