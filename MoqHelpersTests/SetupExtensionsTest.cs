using Moq;
using MoqHelpers;
using MoqHelpers.InSequence;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MoqHelpersTests
{
    [TestFixture]
    public class SetupExtensionsTest
    {
        [Test]
        public void SetupMultipleExact()
        {
            var mock = new Mock<IToMock>(MockBehavior.Strict);
            mock.SetupMultipleExact(m => m.MethodWithArgument(""), new List<ITuple> { ("1", "One"), ("2", "Two") });
            var mocked = mock.Object;
            var return1 = mocked.MethodWithArgument("1");
            var return2 = mocked.MethodWithArgument("2");
            Assert.That(return1, Is.EqualTo("One"));
            Assert.That(return2, Is.EqualTo("Two"));
        }
        [TestCase(true,ExpectedResult =false)]
        [TestCase(false, ExpectedResult = true)]
        public bool SetupMultipleExactVerifiable(bool invokeAll)
        {
            var mock = new Mock<IToMock>();
            mock.SetupMultipleExact(m => m.MethodWithArgument(""), new List<ITuple> { ("1", "One"), ("2", "Two") },true);
            var mocked = mock.Object;
            mocked.MethodWithArgument("1");
            if (invokeAll)
            {
                mocked.MethodWithArgument("2");
            }
            return ThrewHelper.Threw(() => mock.Verify());
        }
    }
}
