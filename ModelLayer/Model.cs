using LogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class ModelLayer : ModelAbstractAPI
    {

        private LogicAbstractAPI logicLayer;

        private ObservableCollection<IModelPoolBall> visiblePoolBals = new ObservableCollection<IModelPoolBall>();

        public ModelLayer()
        {
            logicLayer = LogicAbstractAPI.createLogicAPI();
        }

        public override void createPoolBalls(int amount)
        {
            logicLayer.createBalls(amount);
        }

        public override void startSimulation()
        {
            logicLayer.startSimulation();
        }

        public override void stopSimulation()
        {
            logicLayer.stopSimulation();
        }

        public override ObservableCollection<IModelPoolBall> createVisibleBalls()
        {
            foreach (Vector2 position in logicLayer.getPosition())
            {
                IModelPoolBall PoolBall = IModelPoolBall.createBall(position.X, position.Y, logicLayer.getRadius());
                visiblePoolBals.Add(PoolBall);
            }
            return visiblePoolBals;
        }

        




    }


}
