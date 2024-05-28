using System.Drawing;
using System.Numerics;
using DataLayer;

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

        public LogicLayer(DataAbstractAPI d)
        {
            this.random = random;
            dataLayer = d;
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
                ballsList.Add(new PoolBall(i, x, y));
                ballsList[i].PositionChange += HandlePositionChange;
            }

        }

        public override void deleteBalls()
        {
            for (int i = 0;i < ballsList.Count;i++) { ballsList[i].StopThread(); }
                ballsList.Clear();

        }

        public override event EventHandler<BallEventArgs> LogicEvent;


        private object moveLock = new object();

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
                                ball.CalculateCollision(ball, other_ball);
                            }

                    }

                    LogicEvent?.Invoke(this, new BallEventArgs(ball.Position,ball.ID));
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
                    positions.Add(new Vector2(ball.Position.X, ball.Position.Y));
                }
            }

            return positions;
        }

  
        private bool DetectCollision(PoolBall ball, PoolBall other_ball)
        {

            double dx = other_ball.Position.X - ball.Position.X;
            double dy = other_ball.Position.Y - ball.Position.Y;

            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < getRadius()) return true;
            else return false;

        }


        private void checkIfOnBoard(PoolBall poolBall)
        {
            if (poolBall.Position.X > getBoardWidth() + 50)
            {
                poolBall.BounceOffWall(getBoardWidth() + 50, false);
            }
            else if (poolBall.Position.X < 0)
            {
                poolBall.BounceOffWall(0, false);
            }

            if (poolBall.Position.Y > getBoardLength() - 35)
            {
                poolBall.BounceOffWall(getBoardLength() - 35, true);
            }
            else if (poolBall.Position.Y < 0)
            {   
                poolBall.BounceOffWall(0, true);
            }

        }


        public override int getRadius()
        {
           return dataLayer.GetRadius();
        }

        public override int getBoardWidth()
        {
            return dataLayer.GetBoardWidth();
        }

        public override int getBoardLength()
        {   
            return dataLayer.GetBoardLength();
        }
    }




    public class BallEventArgs : EventArgs
    {
       public Vector2 Position { get; }
       public int ballID { get; }

        public BallEventArgs(Vector2 position, int ballid)
        {
            Position = position;
            ballID = ballid;
            
        }
    }









}
