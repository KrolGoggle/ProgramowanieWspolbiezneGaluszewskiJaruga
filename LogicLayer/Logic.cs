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

        private DataAbstractAPI dataLayer;

        public override System.Timers.Timer timer { get; }

        public LogicLayer()
        {
            dataLayer = DataAbstractAPI.createDataAPI();
            ballsList = new List<PoolBall>();
            board = new Board();
            timer = new System.Timers.Timer(100);
        }

        public override void createBalls(int amount)
        {
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            for (int i = 0; i < amount; i++)
            {
                x = rnd.Next(5, board.BoardWidth - 100);
                y = rnd.Next(5, board.BoardLenght - 100);
                ballsList.Add(new PoolBall(x, y));

            }
        }

        public override void updateBalls()
        {
            foreach (PoolBall pball in ballsList)
            {
                pball.move(500);
            }
        }

        public override void deleteBalls(int amount)
        {
            if(Math.Abs(amount) >= ballsList.Count)
            {
                ballsList.Clear();
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    ballsList.RemoveAt(ballsList.Count() - 1);
                }
                }
        }

        public override void startSimulation(int amount)
        {
            timer.Start();
            createBalls(amount);
        }

        public override void stopSimulation(int amount)
        {
            timer.Stop();
            deleteBalls(amount);
        }

        public override List<Vector2> getPosition()
        {
            List<Vector2> positions = new List<Vector2>();

            if (ballsList != null)
            {
                foreach (PoolBall ball in ballsList)
                {
                    positions.Add(new Vector2(ball.X, ball.Y));
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
                    positions.Add(new Vector2(ball.VelocityX, ball.VelocityY));
                }
            }

            return positions;
        }

        public override int getRadius()
        {
           return 3 ;
        }

        public override int getBoardWidth()
        {
            return board.BoardWidth;
        }

        public override int getBoardLength()
        {
            return board.BoardLenght;        }
    }
}
