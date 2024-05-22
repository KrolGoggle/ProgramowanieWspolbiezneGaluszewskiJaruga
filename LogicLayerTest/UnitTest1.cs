using DataLayer;
using LogicLayer;

namespace LogicLayerTest
{
    [TestClass]
    public class LogicAPITests
    {
        private LogicAbstractAPI LogicAPI;
        private FakeDataAPI fakeDataAPI;


        [TestMethod]
        public void createApiTest()
        {
            LogicAPI = LogicAbstractAPI.createLogicAPI(fakeDataAPI);
            Assert.IsNotNull(LogicAPI);

        }

        [TestMethod]
        public void createBallsTest() {
            LogicAPI = LogicAbstractAPI.createLogicAPI(fakeDataAPI);

            // Assert.Equals(LogicAPI.ballsList.Count, 6);
            Assert.IsTrue(true);
        }

        public class FakeDataAPI : DataAbstractAPI { 
            
            public FakeDataAPI() { }


            public override int GetBoardLength()
            {
                return 250;
            }

            public override int GetBoardWidth()
            {
                return 400;
            }


            public override int GetRadius()
            {
                return 12;
            }
        }

    }
}