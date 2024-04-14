using DataLayer;
using System.Collections.Generic;
using System;

namespace LogicLayer
{
    public abstract class LogicAbstractAPI
    {
        public abstract void createBalls(int amount);
        public abstract void updateBalls();
        public abstract List<PoolBall> ballsList { get; }
        public abstract Board board { get; }
        public abstract System.Timers.Timer timer { get; }

        public abstract void startSimulation();

        public abstract void stopSimulation();

        public static LogicAbstractAPI createLogicAPI() {
            return new LogicLayer();
        }


    }

    public class LogicLayer : LogicAbstractAPI {

        public override List<PoolBall> ballsList { get; }
        public override Board board { get; }

        private DataAbstractAPI dataLayer;

        public override System.Timers.Timer timer { get; }

        public LogicLayer() {
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
            for (int i = 0; i < amount; i++) {
                x = rnd.Next(5, board.Width - 100);
                y = rnd.Next(5, board.Lenght - 100);
                ballsList.Add(new PoolBall(x, y));

            }
        }

        public override void updateBalls()
        {
            foreach (PoolBall pball in ballsList) {
                pball.move(500);
            }
        }

        public override void startSimulation() {
            timer.Start();
        }

        public override void stopSimulation()
        {
            timer.Stop();
        }

    }

}
