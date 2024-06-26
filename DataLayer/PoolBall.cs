﻿using Data;
using System.Diagnostics;
using System.Numerics;


namespace DataLayer
{

    public interface IPoolBall
    {
        public void StopThread();
        public abstract Vector2 Position { get; }

        public abstract Vector2 Velocity { get; set; }
    }

    public class PoolBall : IPoolBall
    {
        private Vector2 position;
        private Vector2 velocity;

         public  int ID { get; }

        private Thread? thread;
        private int period = 1;
        public static int mass = 3;
        public static int radius = 12;
        private object move_lock = new object();
        private object velocity_lock = new object();



        public PoolBall(int id, int x, int y)
        {

            ID = id;
            position.X = x;
            position.Y = y;
            randomVelocity();
            createThread();

        }

        public Vector2 Position { get { lock (move_lock) { return position; } } private set { lock (move_lock) { position = value; } } }
        public Vector2 Velocity { get { lock (velocity_lock) { return velocity; } } set { lock (velocity_lock) { velocity = value; } } }

        private void move(long time)
        {

            Vector2 temp = new Vector2((Velocity.X * time) + Position.X, (Velocity.Y * time) + Position.Y);
            Position = temp;

            OnPositionChange();

        }

        public void randomVelocity()
        {
            Random rnd = new Random();
            float tempX = rnd.Next(-1, 1);
            float tempY = rnd.Next(-1, 1);

            while (tempX == 0)
            {
                tempX = rnd.Next(-1, 1);
            }
            while (tempY == 0)
            {
                tempY = rnd.Next(-1, 1);
            }

            Vector2 temp = new Vector2(tempX, tempY);
            Velocity = temp;

        }


        private bool shouldStop = false;

        private void createThread()
        {
            Stopwatch sw = new Stopwatch();

            thread = new Thread(() =>
            {
                int waiting = 0;
                sw.Start();
                long lastElapsed = sw.ElapsedMilliseconds;

                while (!shouldStop)
                {
                    long currentElapsed = sw.ElapsedMilliseconds;
                    long deltaTime = currentElapsed - lastElapsed;
                    lastElapsed = currentElapsed;

                    move(deltaTime);
                    Logger.GetInstance().LogBallPosition(ID, new Vector2(Position.X, Position.Y), DateTime.UtcNow);

                    long nextMoveTime = currentElapsed + period;
                    if (sw.ElapsedMilliseconds < nextMoveTime)
                    {
                        waiting = period;
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

        public void StopThread()
        {
            shouldStop = true;
            thread.Join();
        }

        public void CalculateCollision(PoolBall ball, PoolBall other_ball)
        {
            float speedx1, speedx2, speedy1, speedy2;

            bool nearWall = ball.Position.X - radius <= 0 || ball.Position.X + radius >= 400 ||
                                       ball.Position.Y - radius <= 0 || ball.Position.Y + radius >= 250;

            if (!nearWall)
            {
                speedx1 = (mass * ball.Velocity.X + mass * other_ball.Velocity.X - mass * (ball.Velocity.X - other_ball.Velocity.X)) / (mass + mass);
                speedy1 = (mass * ball.Velocity.Y + mass * other_ball.Velocity.Y - mass * (ball.Velocity.Y - other_ball.Velocity.Y)) / (mass + mass);
                speedx2 = (mass * ball.Velocity.X + mass * other_ball.Velocity.X - mass * (other_ball.Velocity.X - ball.Velocity.Y)) / (mass + mass);
                speedy2 = (mass * ball.Velocity.Y + mass * other_ball.Velocity.Y - mass * (other_ball.Velocity.Y - ball.Velocity.Y)) / (mass + mass);

                Vector2 temp = new Vector2(speedx1, speedy1);
                Vector2 temp2 = new Vector2(speedx2, speedy2);
                ball.Velocity = temp;
                other_ball.Velocity = temp2;
            }
        }

        public void BounceOffWall(int wall, bool top)
        {
            if (top == true)
            {
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
