using Moq.Language;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal abstract class VerifiableTests<TWrapped, TVerifiable> : VerifiableWrappers_Tests_Base<TWrapped, TVerifiable> 
        where TVerifiable : Verifiable<TWrapped>,IVerifies where TWrapped :class, IVerifies
    {
        [Test]
        public void Verifiable_Should_Call_Wrapped_And_The_VerifiableWrapper()
        {

            mockWrapped.Setup(m => m.Verifiable());

            mockVerifiableWrapper.Setup(m => m.Verifiable());

            verifiable.Verifiable();
        }
        [Test]
        public void Verifiable_FailMessage_Should_Call_Wrapped_And_The_VerifiableWrapper()
        {
            var failMessage = "Fail message";

            mockWrapped.Setup(m => m.Verifiable(failMessage));

            mockVerifiableWrapper.Setup(m => m.Verifiable(failMessage));

            verifiable.Verifiable(failMessage);

        }
    }
    
}
