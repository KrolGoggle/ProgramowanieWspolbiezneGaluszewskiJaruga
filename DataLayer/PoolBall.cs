using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataLayer
{

    public interface IPoolBall {
        public void StopThread();
        public Vector2 Position { get; }

        public Vector2 Velocity { get; set; }
    }

    public class PoolBall : IPoolBall
    {
        private Vector2 position;
        private Vector2 velocity;
        private Thread? thread;
        private int period = 4;
        public static int mass = 3;
        public static int radius = 12;
        private object lockObject = new object();

        public PoolBall(int x, int y)
        {
            position.X = x;
            position.Y = y;
            randomVelocity();
            createThread();

        }

        public Vector2 Position { get { return position; } private set { position = value; } }
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

        public int Mass { get { return mass; } }

        public int Radius { get { return radius; } }
        private void move()
        {
            Vector2 temp = Position + Velocity;
            Position = temp;

            OnPositionChange();

        }

        public void randomVelocity()
        {
            Random rnd = new Random();
            velocity.X = rnd.Next(-1, 1);
            velocity.Y = rnd.Next(-1, 1);

          
            while (velocity.X == 0)
            {
                velocity.X = rnd.Next(-1, 1);
            }
            while (velocity.Y == 0)
            {
                velocity.Y = rnd.Next(-1, 1);
            }

        }

        object moveLock = new object();
        private bool shouldStop = false;

        private void createThread()
        {
           Stopwatch sw = new Stopwatch();

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

        public void StopThread() {
            shouldStop = true;
            thread.Join();
        }

        public void CalculateCollision(PoolBall ball, PoolBall other_ball)
        {
            float speedx1, speedx2, speedy1, speedy2;

            bool nearWall = ball.Position.X - Radius <= 0 || ball.Position.X + Radius >= 400 ||
                                       ball.Position.Y - Radius <= 0 || ball.Position.Y + Radius >= 250;

            if (!nearWall)
            {
                speedx1 = (ball.Mass * ball.Velocity.X + other_ball.Mass * other_ball.Velocity.X - other_ball.Mass * (ball.Velocity.X - other_ball.Velocity.X)) / (ball.Mass + other_ball.Mass);
                speedy1 = (ball.Mass * ball.Velocity.Y + other_ball.Mass * other_ball.Velocity.Y - other_ball.Mass * (ball.Velocity.Y - other_ball.Velocity.Y)) / (ball.Mass + other_ball.Mass);
                speedx2 = (ball.Mass * ball.Velocity.X + other_ball.Mass * other_ball.Velocity.X - ball.Mass * (other_ball.Velocity.X - ball.Velocity.Y)) / (ball.Mass + other_ball.Mass);
                speedy2 = (ball.Mass * ball.Velocity.Y + other_ball.Mass * other_ball.Velocity.Y - other_ball.Mass * (other_ball.Velocity.Y - ball.Velocity.Y)) / (ball.Mass + other_ball.Mass);

                Vector2 temp = new Vector2(speedx1, speedy1);
                Vector2 temp2 = new Vector2(speedx2, speedy2);
                ball.Velocity = temp;
                other_ball.Velocity = temp2;
            }
        }

        public void BounceOffWall(int wall, bool top)
        {
            if (top == true) {
                Vector2 tempPos = new Vector2(Position.X, wall);
                Vector2 tempVel = new Vector2(Velocity.X, Velocity.Y * -1);

                Position = tempPos;
                Velocity = tempVel;
            }
            else
            {
                Vector2 tempPos = new Vector2(wall, Position.Y);
                Vector2 tempVel = new Vector2(Velocity.X * -1, Velocity.Y);

                Position = tempPos;
                Velocity = tempVel;
            }
        }


        public event EventHandler PositionChange;

        internal void OnPositionChange()
        {
            PositionChange?.Invoke(this, EventArgs.Empty);
        }

    }

    
}
