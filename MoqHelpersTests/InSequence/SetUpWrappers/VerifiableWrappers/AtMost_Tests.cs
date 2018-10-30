﻿using Moq.Language;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal class AtMost_Tests<TWrapped,TVerifiable>: VerifiableWrappers_Returns_Tests_Base<TWrapped, IVerifies, TVerifiable>  where TWrapped :class, IOccurrence where TVerifiable : Verifiable<TWrapped>, IOccurrence
    {
        [Test]
        public void AtMost_Should_Wrap_The_Return_From_Wrapped()
        {
            var atMost = 5;

            #pragma warning disable 0618
            mockWrapped.Setup(m => m.AtMost(atMost)).Returns(mockedWrappedReturn);
            #pragma warning restore 0618

            mockVerifiableWrapper.Setup(m => m.WrapVerifiesForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            #pragma warning disable 0618
            wrappedReturn = verifiable.AtMost(atMost);
            #pragma warning restore 0618

        }
        [Test]
        public void AtMostOnce_Should_Wrap_The_Return_From_Wrapped()
        {
            #pragma warning disable 0618
            mockWrapped.Setup(m => m.AtMostOnce()).Returns(mockedWrappedReturn);
            #pragma warning restore 0618
            mockVerifiableWrapper.Setup(m => m.WrapVerifiesForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            #pragma warning disable 0618
            wrappedReturn = verifiable.AtMostOnce();
            #pragma warning restore 0618
        }

    }
}