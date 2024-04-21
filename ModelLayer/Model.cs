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

        private int num;
    
        public override event EventHandler ModelEvent;

        public ModelLayer()
        {
            logicLayer = LogicAbstractAPI.createLogicAPI();
           
        }



        public override void createPoolBalls(int amount)
        {   
            this.num = amount;
            logicLayer.createBalls(amount);
        }

        public override ObservableCollection<IModelPoolBall> createVisibleBalls()
        {
            visiblePoolBals.Clear();

            foreach (Vector2 position in logicLayer.getPosition())
            {
                IModelPoolBall PoolBall = IModelPoolBall.createBall(position.X, position.Y, logicLayer.getRadius());
                visiblePoolBals.Add(PoolBall);
            }
            return visiblePoolBals;
        }

        public override void createBalls(int a)
        {
            logicLayer.createBalls(a);
        }


        public override void deleteBalls(int a)
        {
            logicLayer.deleteBalls(a);
        }

        private void ChangeModelBallsPositions(object sender, EventArgs e)
        {
            int i = 0;

            // Update the positions of ball models in the collection based on the positions from LogicAPI
            foreach (Vector2 ball in logicLayer.getPosition())
            {
                if (num == visiblePoolBals.Count)        // Check if the number of ball models matches the expected count
                {
                    visiblePoolBals[i].Pos_X = ball.X;  // Update positions
                    visiblePoolBals[i].Pos_Y = ball.Y;
                    i++;
                }
            }

        }

    }


}
