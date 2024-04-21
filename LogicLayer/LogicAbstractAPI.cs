using DataLayer;
using System.Collections.Generic;
using System;
using System.Numerics;

namespace LogicLayer
{
    public abstract class LogicAbstractAPI
    {
        public abstract void createBalls(int amount);
        public abstract void updateBalls();
        public abstract void deleteBalls(int amount);
        public abstract List<PoolBall> ballsList { get; }
        public abstract Board board { get; }
        public abstract System.Timers.Timer timer { get; }
        public abstract List<Vector2> getPosition();
        public abstract List<Vector2> getVelocity();

        public abstract int getRadius();

        public abstract int getBoardWidth();
        public abstract int getBoardLength();


        public abstract void startSimulation(int amount);

        public abstract void stopSimulation(int amount);

        public static LogicAbstractAPI createLogicAPI() {
            return new LogicLayer();
        }

    }

}
