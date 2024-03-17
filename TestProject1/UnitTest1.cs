using System;
using NUnit.Framework.Constraints;
using projekt1;

namespace TestProject1
{
    public class Tests
    {

        [Test]
        public void HelloTest()
        {
            Prog prog1 = new Prog();
            Assert.AreEqual("Hello World!", prog1.hello());
        }
    }
}