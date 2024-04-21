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
        static int r = 3;
        static double mass = 1;
        private Task? task;
        private Stopwatch sw = new Stopwatch();
        private int period = 4;

        public PoolBall(int x, int y) {
            position.X = x;
            position.Y = y;
            randomVelocity();
        }

        public float X { 
            
            get { return position.X; }
            set { position.X = value; }
        }
        public float Y { get { return position.Y; }
            set { position.Y = value; }
        }

        public float VelocityX { get { return velocity.X; }
            set { velocity.X = value;  }
        }
        public float VelocityY
        {
            get { return velocity.Y; }
            set { velocity.Y = value; }
        }

        public void move(int speed) {
            position += new Vector2(velocity.X * speed, velocity.Y * speed);
            if (position.X < 0 || position.X > Board.width - 10) {
                velocity *= -Vector2.UnitX;
            }

            if (position.Y < 0 || position.Y > Board.width - 10)
            {
                velocity *= -Vector2.UnitY;
            }
            OnPositionChange();
        }

        public void randomVelocity() {
            Random rnd = new Random();
            velocity.X = rnd.Next(-5, 5);
            velocity.Y = rnd.Next(-5, 5);
        }

        private void CreateTask()
        {
            task = Task.Run(async () =>
            {
                int waiting = 0;            // This variable will store the waiting time before the next ball movement is made.

                while (true)
                {
                    sw.Restart();    // Restart the stopwatch to measure time
                    sw.Start();      // Start the stopwatch
                    move(30);
                    sw.Stop();       // Stop the stopwatch

                    // This conditional instruction calculates the waiting time before the next ball movement is made.
                    // It subtracts the duration of the ball movement from the expected period between movements.
                    // If the result is greater than 0, the ball will wait for this time.
                    // Otherwise,if the duration of the ball's movement is longer than the expected period, the waiting variable will be equal to 0,
                    // meaning that the ball will continue moving immediately.
                    if (period - sw.ElapsedMilliseconds > 0)
                    {
                        waiting = period - (int)sw.ElapsedMilliseconds;
                    }
                    else
                    {
                        waiting = 0;
                    }

                    // The task waits for a specified amount of time(wait) before the next ball move is executed.
                    // await causes the task to be suspended without blocking the thread, allowing other operations to use the processor.
                    // When the wait time expires, the task resumes and the cycle starts again, with the next ball move executed.
                    await Task.Delay(waiting);
                }
            });
        }

        // Method to dispose the task when needed
        public void KillTask()
        {
            task.Dispose();
        }

        public event EventHandler PositionChange;

        // This is a method that is responsible for calling the PositionChange event. 
        // It calls the event, passing itself(this) as the event sender and EventArgs.Empty as the event argument (as it does not require additional information).
        internal void OnPositionChange()
        {
            PositionChange?.Invoke(this, EventArgs.Empty);
        }

    }
}
