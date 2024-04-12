using DataLayer;
using System.Collections.Generic;
using System;

namespace LogicLayer
{
    public abstract class LogicAbstractAPI
    {
        public abstract void createBalls(int amount);
        public abstract void updateBalls();

        public abstract void moveBall();
        public abstract List<PoolBall> ballsList { get; }
        public abstract Board board { get; }

        public abstract Board createBoard(int width, int lenght);

        public LogicAbstractAPI createLogicAPI() {
            return new LogicAPI(width, lenght, /*Timer*/);
        }


    }

    public class LogicLayer : LogicAbstractAPI {

        public override List<PoolBall> ballsList { get; }
        public override Board board { get; }

        private DataAbstractAPI dataLayer;

        public LogicAPI(int width, int lenght /*, Timer timer*/) {
            dataLayer = DataAbstractAPI.createDataAPI();
            ballsList = new List<PoolBall>();
            board = createBoard(width, lenght);
            /* timer */
        }
        
        public override void createBalls(int amount)
        {   
            Random rnd = new Random();
            int x = 0;
            int y = 0;
            for (int i = 0; i < amount; i++) {
                x = rnd.Next(5, board.Width - 25);
                x = rnd.Next(5, board.Lenght - 25);
                ballsList.Add(new PoolBall(x, y));
            }
        }

        public override void moveBall(PoolBall ball)
        {
            if (ball.X )
        }

    }

}
