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

        public abstract event EventHandler LogicEvent;
        public abstract List<Vector2> getPosition();
        public abstract List<Vector2> getVelocity();

        public abstract int getRadius();

        public abstract int getBoardWidth();
        public abstract int getBoardLength();

        public static LogicAbstractAPI createLogicAPI() {
            return new LogicLayer();
        }

    }

}
