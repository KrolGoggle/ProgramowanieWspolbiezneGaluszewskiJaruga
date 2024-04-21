using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
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


        public PoolBall(int x, int y)
        {
            position.X = x;
            position.Y = y;
            randomVelocity();
            createTask();

        }

        public float Position_x { get => position.X; private set => position.X = value; }
        public float Position_y { get => position.Y; private set => position.Y = value; }
        public float Velocity_x { get => velocity.X; set => velocity.X = value; }
        public float Velocity_y { get => velocity.Y; set => velocity.Y = value; }


        public void move(int speed)
        {

            Position_x += speed * velocity.X;
            Position_y += speed * velocity.Y;

            checkIfOnBoard();

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

        private void createTask()
        {
            task = Task.Run(async () =>
            {
                int waiting = 0;           

                while (true)
                {
                    sw.Restart();    
                    sw.Start();     
                    move(1);
          
                    sw.Stop();       

                     if (period - sw.ElapsedMilliseconds > 0)
                    {
                        waiting = period - (int)sw.ElapsedMilliseconds;
                     }
                     else
                     {
                        waiting = 0;
                     }

                    await Task.Delay(waiting);
                }
            });
        }
      
        public event EventHandler PositionChange;

        internal void OnPositionChange()
        {
            PositionChange?.Invoke(this, EventArgs.Empty);
        }

        private void checkIfOnBoard()
        {
            
            if (position.X > Board.width+50)
            {
                position.X = Board.width+50; 
                velocity.X *= -1;       
            }
            else if (position.X < 0)
            {
                position.X = 0; 
                velocity.X *= -1;               
            }

            if (position.Y > Board.height-30)
            {
                position.Y = Board.height-30; 
                velocity.Y *= -1;    
            }
            else if (position.Y < 0)
            {
                position.Y = 0; 
                velocity.Y *= -1;          
            }

        }
    }

    
}
