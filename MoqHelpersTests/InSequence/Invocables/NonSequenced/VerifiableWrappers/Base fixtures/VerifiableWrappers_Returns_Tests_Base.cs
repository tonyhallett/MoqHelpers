using Moq;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;

namespace MoqHelpersTests.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    [TestFixture]
    internal abstract class VerifiableWrappers_Returns_Tests_Base<TWrapped, TWrappedReturn, TVerifiable> :VerifiableWrappers_Tests_Base<TWrapped, TVerifiable> where TVerifiable:Verifiable<TWrapped> where TWrapped:class where TWrappedReturn:class{
        protected TWrappedReturn wrappedReturn;
        protected TWrappedReturn mockedVerifiableWrapperReturn;
        protected TWrappedReturn mockedWrappedReturn;

        [SetUp]
        public void SetupReturns()
        {
            mockedVerifiableWrapperReturn = new Mock<TWrappedReturn>().Object;
            mockedWrappedReturn = new Mock<TWrappedReturn>().Object;
        }
        [TearDown]
        public void VerifyReturn()
        {
            Assert.That(wrappedReturn, Is.EqualTo(mockedVerifiableWrapperReturn));
        }

    }



}
