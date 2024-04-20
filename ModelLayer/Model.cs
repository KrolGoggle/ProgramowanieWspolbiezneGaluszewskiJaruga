using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
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
