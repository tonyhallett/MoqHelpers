using Moq;
using Moq.Language;
using Moq.Language.Flow;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal class VerifiableThrowsResult_Verifies_Tests : VerifiableTests<IThrowsResult, VerifiableThrowsResult>
    {

    }
    
    [TestFixture]
    internal class VerifiableThrowsResult_AtMost_Tests : VerifiableWrappers_Returns_Tests_Base<IThrowsResult, IVerifies, VerifiableThrowsResult>
    {
        [Test]
        public void AtMost_Should_Wrap_The_Return_From_Wrapped()
        {
            var atMost = 5;

            #pragma warning disable 0618
            mockWrapped.Setup(m => m.AtMost(atMost)).Returns(mockedWrappedReturn);
            #pragma warning restore 0618
            
            mockVerifiableWrapper.Setup(m => m.WrapVerifiesForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = verifiable.AtMost(atMost);

        }
        [Test]
        public void AtMostOnce_Should_Wrap_The_Return_From_Wrapped()
        {
            #pragma warning disable 0618
            mockWrapped.Setup(m => m.AtMostOnce()).Returns(mockedWrappedReturn);
            #pragma warning restore 0618
            mockVerifiableWrapper.Setup(m => m.WrapVerifiesForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = verifiable.AtMostOnce();
        }
    }
}
