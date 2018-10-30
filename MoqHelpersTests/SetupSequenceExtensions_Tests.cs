using Moq;
using MoqHelpers;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoqHelpersTests
{
    [TestFixture]
    public class SetupSequenceExtensions_Tests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void SetUpSequenceEnumerable_Should_Act_Like_SetupSequence_With_Returns_From_The_Enumerable(int numReturns)
        {
            var mock = new Mock<IToMock>();
            var returns = Enumerable.Range(0, numReturns).Select(n => n.ToString());
            mock.SetupSequenceEnumerable(m => m.MethodWithArgument(It.IsAny<string>()), returns);
            var mocked = mock.Object;
            foreach (var r in returns)
            {
                Assert.That(mocked.MethodWithArgument(""), Is.EqualTo(r));
            }
            Assert.That(mocked.MethodWithArgument(""), Is.Null);
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void SetUpSequenceIndex_Should_Supply_The_Returns_From_Delegate_Return_That_It_Passes_Index(int numReturns)
        {
            var mock = new Mock<IToMock>();
            Func<int, string> provider = (i => i.ToString());
            var returns = Enumerable.Range(0, numReturns).Select((r, i) => provider(i));
            mock.SetupSequenceIndex(m => m.MethodWithArgument(It.IsAny<string>()),numReturns,provider);
            var mocked = mock.Object;
            foreach (var r in returns)
            {
                Assert.That(mocked.MethodWithArgument(""), Is.EqualTo(r));
            }
            Assert.That(mocked.MethodWithArgument(""), Is.Null);
        }

        [TestCaseSource("ThrowsOrReturnsSource")]
        public void SetUpSequenceEnumerableThrowsOrReturns_Should_Throw_For_Exceptions_Or_Return_Otherwise(List<object> returnsOrExceptions)
        {
            var mock = new Mock<IToMock>();
            mock.SetupSequenceEnumerableThrowsOrReturns(m => m.MethodWithArgument(""), returnsOrExceptions);
            var mocked = mock.Object;
            List<Exception> thrownExceptions = new List<Exception>();
            foreach (var returnOrThrow in returnsOrExceptions)
            {
                Exception thrownException = null;
                object returned = null;
                try
                {
                    returned=mocked.MethodWithArgument("");
                }
                catch (Exception exc)
                {
                    thrownException = exc;
                }
                if (IsException(returnOrThrow))
                {
                    Assert.That(thrownException, Is.EqualTo(returnOrThrow));
                }
                else
                {
                    Assert.That(returned, Is.EqualTo(returnOrThrow));
                }
            }
            Assert.That(mocked.MethodWithArgument(""), Is.Null);
        }
        private bool IsException(object obj)
        {
            return obj != null && obj.GetType().IsSubclassOf(typeof(Exception));
            
        }
        [TestCaseSource("ThrowsOrPassesSource")]
        public void SetUpSequenceEnumerableThrowsOrPasses_Should_Throw_For_Exceptions(List<Exception> enumerable)
        {
            var mock = new Mock<IToMock>();
            mock.SetupSequenceEnumerableThrowsOrPasses(m => m.Method1(), enumerable);
            var mocked = mock.Object;
            List<Exception> thrownExceptions = new List<Exception>();
            foreach(var exception in enumerable) {
                Exception thrownException = null;
                try
                {
                    mocked.Method1();
                }catch(Exception exc)
                {
                    thrownException = exc;
                }
                if (IsException(exception)){
                    Assert.That(thrownException, Is.EqualTo(exception));
                }
                else
                {
                    Assert.That(thrownException, Is.Null);
                }
            }
            Assert.That(mocked.MethodWithArgument(""), Is.Null);
        }
        public static List<TestCaseData> ThrowsOrPassesSource()
        {
            var exception1 = new ArgumentException();
            var exception2 = new OutOfMemoryException();
            var exception3 = new FieldAccessException();
            var exception4 = new NotImplementedException();
            return new List<TestCaseData>
            {
                new TestCaseData(new List<Exception>{ exception1,exception2,exception3,exception4}),
                new TestCaseData(new List<Exception>{ null,null,null,null}),
                new TestCaseData(new List<Exception>{ null, exception1, null, exception2 }),
                new TestCaseData(new List<Exception>{ exception1, null, exception2, null })
            };
        }
        public static List<List<object>> ThrowsOrReturnsSource()
        {
            var exception1 = new ArgumentException();
            var exception2 = new OutOfMemoryException();
            var exception3 = new FieldAccessException();
            var exception4 = new NotImplementedException();

            return new List<List<object>>() {
                new List<object>{ exception1,exception2,exception3,exception4},
                new List<object>{ "1","2","3","4"},
                new List<object>{ null,null,null,null},
                new List<object>{ exception1,"1",null,null,"2",exception2}
            };

        }
    }
}
