using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using LogicLayer;
using Moq;
using DataLayer;
using System.Numerics;

namespace LogicLayerTest
{
    [TestClass]
    public class LogicAPITests
    {
        private Mock<LogicAbstractAPI> mockLogicAPI;

        [TestInitialize]
        public void Setup()
        {
            mockLogicAPI = new Mock<LogicAbstractAPI>();
        }

        [TestMethod]
        public void createApiTest()
        {
            LogicAbstractAPI l = mockLogicAPI.Object;

            Assert.IsNotNull(l);

        }

        [TestMethod]
        public void deleteBallsTest()
        {
            LogicAbstractAPI l = mockLogicAPI.Object;

            l.createBalls(5);
            l.deleteBalls();

            Assert.AreEqual(0, l.ballsList.Count);
        }


    }
}