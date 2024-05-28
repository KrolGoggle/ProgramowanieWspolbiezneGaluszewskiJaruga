using System.Collections.ObjectModel;
using System.Numerics;
using LogicLayer;

namespace ModelLayer
{
    public class ModelLayer : ModelAbstractAPI
    {

        private LogicAbstractAPI logicLayer;

        private ObservableCollection<IModelPoolBall> visiblePoolBals = new ObservableCollection<IModelPoolBall>();

        private int createdVisibleBalls = 0;

        public override event EventHandler ModelEvent;

        private int current_balls;
        private Random random = new Random();


        public ModelLayer()
        {
            logicLayer = LogicAbstractAPI.createLogicAPI(random);
            logicLayer.LogicEvent += moveVisibleBalls;

        }

        public override void createPoolBalls(int amount)
        {
            if (amount >= 0)
            { 
            
             this.current_balls += amount;


             logicLayer.createBalls(amount); 
            }
        }

        public override void destroyEveryPoolBall()
        {
            this.current_balls = 0;
            logicLayer.deleteBalls();
            createdVisibleBalls = 0;
        }


      

        public override ObservableCollection<IModelPoolBall> createVisibleBalls()
        {
            visiblePoolBals.Clear();
            foreach (Vector2 position in logicLayer.getPosition())
            {
                IModelPoolBall PoolBall = IModelPoolBall.createBall(position, logicLayer.getRadius());
                visiblePoolBals.Add(PoolBall);
            }
            createdVisibleBalls = visiblePoolBals.Count();
            return visiblePoolBals;
        }



        public void moveVisibleBalls(object sender, EventArgs e)
        {

            int i = 0;

            foreach (Vector2 ball in logicLayer.getPosition())
            {
                if (current_balls == visiblePoolBals.Count)
                {
                    visiblePoolBals[i].Position = ball; 
                    i++;
                }
            }

        }

        public override int getCurrentVisibleBalls()
        {
            return createdVisibleBalls;
        }

    }


}
