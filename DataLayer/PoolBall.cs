using System.ComponentModel;
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

        }

        public void randomVelocity() {
            Random rnd = new Random();
            velocity.X = rnd.Next(-5, 5);
            velocity.Y = rnd.Next(-5, 5);
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
