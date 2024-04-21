using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;

namespace ModelLayer
{
    public abstract class ModelAbstractAPI
    {
        public static ModelAbstractAPI createModelAPI()
        {
            return new ModelLayer();
        }

        public abstract void createPoolBalls(int a);

        public abstract void startSimulation(int a);
        public abstract void stopSimulation(int a);

        public abstract ObservableCollection<IModelPoolBall> createVisibleBalls();

    }
        
   
}