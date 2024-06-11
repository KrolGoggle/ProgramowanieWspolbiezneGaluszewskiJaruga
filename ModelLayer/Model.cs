using LogicLayer;
using System.Collections.ObjectModel;

namespace ModelLayer
{
    public class ModelLayer : ModelAbstractAPI
    {

        private LogicAbstractAPI logicLayer;

        private ObservableCollection<IModelPoolBall> visiblePoolBalls = new ObservableCollection<IModelPoolBall>();


        private int createdVisibleBalls = 0;

        public override event EventHandler ModelEvent;

        private int current_balls;
        private Random random = new Random();


        public ModelLayer()
        {
            logicLayer = LogicAbstractAPI.createLogicAPI(random);
            logicLayer.LogicEvent += moveVisibleBall;

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
            visiblePoolBalls.Clear();

            var positions = logicLayer.getPosition();
            for (int i = 0; i < positions.Count; i++)
            {
                var position = positions[i];
                IModelPoolBall poolBall = IModelPoolBall.createBall(position, logicLayer.getRadius());
                visiblePoolBalls.Add(poolBall);
            }

            createdVisibleBalls = visiblePoolBalls.Count();
            return visiblePoolBalls;
        }






        private void moveVisibleBall(object sender, BallEventArgs e)
        {

            visiblePoolBalls[e.ballID].Position = e.Position;

        }






        public override int getCurrentVisibleBalls()
        {
            return createdVisibleBalls;
        }


    }


}
