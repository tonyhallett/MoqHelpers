using Moq;
using MoqHelpers.Additional;
using NUnit.Framework;
using System.Linq;

namespace MoqHelpersTests.Additional
{
    [TestFixture]
    public class MockAdditionalHelperTests
    {
        [Test]
        public void AddAdditional_Works()
        {
            var mock = new Mock<IToMock>();
            var additional = "Additional";
            mock.AddAdditional(additional);

            Assert.That(mock.GetAdditional<string>(), Is.EqualTo(additional));
        }
        [Test]
        public void GetAdditional_Returns_Default_If_No_Additional()
        {
            var mock = new Mock<IToMock>();
            Assert.That(mock.GetAdditional<string>(), Is.Null);
        }
        [Test]
        public void GetAdditional_Returns_Default_If_No_Additional_And_Exception()
        {
            var mock = new Mock<IToMock>(MockBehavior.Strict);
            Assert.That(mock.GetAdditional<string>(), Is.Null);
        }
        [Test]
        public void Without_Additional_Removes_Get___Additional_Invocations()
        {
            var mock = new Mock<IToMock>();
            var _ = mock.GetAdditional<string>();
            var invocations = mock.Invocations;
            Assert.That(invocations.WithoutAdditional().Count(), Is.EqualTo(invocations.Count - 1));
        }
    }
}
