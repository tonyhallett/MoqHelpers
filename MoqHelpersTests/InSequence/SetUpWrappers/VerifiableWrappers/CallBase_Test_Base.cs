using Moq;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal abstract class CallBase_Test_Base<TWrapped, TWrappedReturn, TVerifiable> : VerifiableWrappers_Returns_Tests_Base<TWrapped, TWrappedReturn, TVerifiable> where TVerifiable : Verifiable<TWrapped> where TWrapped : class where TWrappedReturn : class
    {
        protected abstract void SetupCallbase(Mock<TWrapped> mockWrapped, TWrappedReturn returns);
        protected abstract void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, TWrappedReturn wrappedReturn, TWrappedReturn verifiableWrapperReturn);
        [Test]
        public void CallBase_Should_Wrap_Return_From_Wrapped()
        {
            SetupCallbase(mockWrapped, mockedWrappedReturn);
            SetupVerifiableWrapper(mockVerifiableWrapper, mockedWrappedReturn, mockedVerifiableWrapperReturn);

            wrappedReturn = ((dynamic)verifiable).CallBase();
        }
    }
    
}
