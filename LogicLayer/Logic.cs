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
           
                ballsList.Clear();
           
        }

        public override event EventHandler LogicEvent;

        private void HandlePositionChange(object sender, EventArgs e)
        {
            if (sender != null)
            {
                LogicEvent?.Invoke(sender, EventArgs.Empty);
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

        public override int getRadius()
        {
           return 5 ;
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
