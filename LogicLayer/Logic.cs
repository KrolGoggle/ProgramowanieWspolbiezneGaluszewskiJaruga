using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class LogicLayer : LogicAbstractAPI
    {

        public override List<PoolBall> ballsList { get; }
        public override Board board { get; }

        private readonly Random random;

        private DataAbstractAPI dataLayer;

        public override System.Timers.Timer timer { get; }

        public LogicLayer(Random random)
        {
            this.random = random;
            dataLayer = DataAbstractAPI.createDataAPI();
            ballsList = new List<PoolBall>();
            board = new Board();
            timer = new System.Timers.Timer(100);
        }

        public override void createBalls(int amount)
        {
            
            int x = 0;
            int y = 0;
            for (int i = 0; i < amount; i++)
            {
                x = random.Next(5, board.BoardWidth - 10);
                y = random.Next(5, board.BoardHeight - 10);
                ballsList.Add(new PoolBall(x, y));
                ballsList[i].PositionChange += HandlePositionChange;
            }

        }

        public override void deleteBalls()
        {
            for (int i = 0;i < ballsList.Count;i++) { ballsList[i].stopThread(); }
                ballsList.Clear();

        }

        public override event EventHandler LogicEvent;


        object moveLock = new object();

        private void HandlePositionChange(Object sender, EventArgs e)
        {
            try
            {
                lock (moveLock) { 
                if (sender != null)
                {
                    PoolBall ball = (PoolBall)sender;


                    checkIfOnBoard(ball);
                    foreach (PoolBall other_ball in ballsList)
                    {
                            if (ball.Equals(other_ball)) continue;

                            if (DetectCollision(ball, other_ball))
                            {
                                CalculateCollision(ball, other_ball);
                            }

                    }

                    LogicEvent?.Invoke(sender, EventArgs.Empty);
                }
            }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception");
            }
        }


        public override List<Vector2> getPosition()
        {
            List<Vector2> positions = new List<Vector2>();

            if (ballsList != null)
            {
                foreach (PoolBall ball in ballsList)
                {
                    positions.Add(new Vector2(ball.Position_x, ball.Position_y));
                }
            }

            return positions;
        }

        public override List<Vector2> getVelocity()
        {
            List<Vector2> positions = new List<Vector2>();

            if (ballsList != null)
            {
                foreach (PoolBall ball in ballsList)
                {
                    positions.Add(new Vector2(ball.Velocity_x, ball.Velocity_y));
                }
            }

            return positions;
        }

        private bool DetectCollision(PoolBall ball, PoolBall other_ball)
        {

            double dx = other_ball.Position_x - ball.Position_x;
            double dy = other_ball.Position_y - ball.Position_y;

            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < getRadius()) return true;
            else return false;

        }
        private void CalculateCollision(PoolBall ball, PoolBall other_ball)
        {
            float speedx1, speedx2, speedy1, speedy2;

            bool nearWall = ball.Position_x - getRadius() <= 0 || ball.Position_x + getRadius() >= 400 ||
                                       ball.Position_y - getRadius() <= 0 || ball.Position_y + getRadius() >= 250;

            if (!nearWall)
            {
                speedx1 = (ball.Mass * ball.Velocity_x + other_ball.Mass * other_ball.Velocity_x - other_ball.Mass * (ball.Velocity_x - other_ball.Velocity_x)) / (ball.Mass + other_ball.Mass);
                speedy1 = (ball.Mass * ball.Velocity_y + other_ball.Mass * other_ball.Velocity_y - other_ball.Mass * (ball.Velocity_y - other_ball.Velocity_y)) / (ball.Mass + other_ball.Mass);
                speedx2 = (ball.Mass * ball.Velocity_x + other_ball.Mass * other_ball.Velocity_x - ball.Mass * (other_ball.Velocity_x - ball.Velocity_y)) / (ball.Mass + other_ball.Mass);
                speedy2 = (ball.Mass * ball.Velocity_y + other_ball.Mass * other_ball.Velocity_y - other_ball.Mass * (other_ball.Velocity_y - ball.Velocity_y)) / (ball.Mass + other_ball.Mass);

                ball.Velocity_x = speedx1;
                ball.Velocity_y = speedy1;

                other_ball.Velocity_x = speedx2;
                other_ball.Velocity_y = speedy2;
            }
        

         }






        private void checkIfOnBoard(PoolBall poolBall)
        {
            if (poolBall.Position_x > getBoardWidth() + 50)
            {
                poolBall.Position_x = Board.width + 50;
                poolBall.Velocity_x *= -1;
            }
            else if (poolBall.Position_x < 0)
            {
                poolBall.Position_x = 0;
                poolBall.Velocity_x *= -1;
            }

            if (poolBall.Position_y > getBoardLength() - 35)
            {
                poolBall.Position_y = getBoardLength() - 35;
                poolBall.Velocity_y *= -1;
            }
            else if (poolBall.Position_y < 0)
            {
                poolBall.Position_y = 0;
                poolBall.Velocity_y *= -1;
            }

        }






        public override int getRadius()
        {
           return 12;
        }

        public override int getBoardWidth()
        {
            return board.BoardWidth;
        }

        public override int getBoardLength()
        {
            return board.BoardHeight;
        }
    }
}
