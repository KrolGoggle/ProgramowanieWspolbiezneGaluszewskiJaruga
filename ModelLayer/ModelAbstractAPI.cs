using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;

namespace ModelLayer
{
    public abstract class ModelAbstractAPI
    {
        public ModelAbstractAPI createModelAPI()
        {
            return new ModelLayer();
        }

        public abstract void startSimulation();
        public abstract void stopSimulation();


    }

    public class ModelLayer : ModelAbstractAPI
    {

        private LogicAbstractAPI logicLayer;

        public ModelLayer()
        {
            logicLayer = LogicAbstractAPI.createLogicAPI();
        }


        public override void startSimulation()
        {
            logicLayer.startSimulation();
        }

        public override void stopSimulation()
        {
            logicLayer.stopSimulation();
        }

    }
}