using Moq;
using MoqHelpers;
using NUnit.Framework;

namespace MoqHelpersTests
{
    [TestFixture]
    public class MoqHelperTests
    {
        [Test]
        public void IsMocked_Should_Return_True_For_A_Mocked_Object()
        {
            Assert.That(MoqHelper.IsMocked(new Mock<IToMock>().Object));
        }
        [Test]
        public void IsMocked_Should_Return_False_For_A_Not_Mocked_Object()
        {
            Assert.That(MoqHelper.IsMocked(new object()), Is.False);
        }
        [Test]
        public void Is_Mocked_Works_With_Auto_Mocked()
        {
            var mock = new Mock<IToMock>() { DefaultValue = DefaultValue.Mock };
            Assert.That(MoqHelper.IsMocked(mock.Object.DefaultMock())); 
        }
    }
    
}
