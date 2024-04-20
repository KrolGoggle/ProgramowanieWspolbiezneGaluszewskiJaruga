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

}
