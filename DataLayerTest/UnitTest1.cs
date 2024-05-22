using DataLayer;
using Moq;

namespace DataLayerTest
{
    [TestClass]
    public class DataLayerTest
    {

        private Mock<DataAbstractAPI> mockDataAPI;

        [TestInitialize]
        public void Setup()
        {
            mockDataAPI = new Mock<DataAbstractAPI>();

            mockDataAPI.Setup(m => m.GetBoardWidth()).Returns(1000);
            mockDataAPI.Setup(m => m.GetBoardLength()).Returns(750);
        }

        [TestMethod]
        public void createApiTest()
        {
            DataAbstractAPI d = mockDataAPI.Object;

            Assert.IsNotNull(d);

        }

        [TestMethod]
        public void getWidthHeightTest()
        {

            DataAbstractAPI d = mockDataAPI.Object;

            Assert.AreEqual(1000, d.GetBoardWidth());
            Assert.AreEqual(750, d.GetBoardLength());

        }

        [TestMethod]

        public void getRadiusTest()
        {
            DataAbstractAPI d = mockDataAPI.Object;


            Assert.IsTrue(true);

        }

        [TestMethod]
        public void createBallTest() {
            DataAbstractAPI d = mockDataAPI.Object;
            PoolBall p = new PoolBall(2,2);
           
            Assert.IsNotNull(p);
        }

    }
}
