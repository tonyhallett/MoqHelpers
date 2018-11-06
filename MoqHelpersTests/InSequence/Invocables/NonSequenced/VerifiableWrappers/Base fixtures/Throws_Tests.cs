using Moq.Language;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    [TestFixture]
    internal abstract class Throws_Tests<TWrapped, TVerifiable> : VerifiableWrappers_Returns_Tests_Base<TWrapped, IThrowsResult, TVerifiable> where TVerifiable : Verifiable<TWrapped>,IThrows where TWrapped : class,IThrows
    {
        [Test]
        public void Throws_Should_Wrap_Return_From_Wrapped()
        {
            var exception = new Exception();
            mockWrapped.Setup(m => m.Throws(exception)).Returns(wrappedReturn);
            mockVerifiableWrapper.Setup(m => m.WrapThrowsForVerification(wrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = verifiable.Throws(exception);
        }
        [Test]
        public void Throws_Generic_Should_Wrap_Return_From_Wrapped()
        {
            mockWrapped.Setup(m => m.Throws<ArgumentException>()).Returns(wrappedReturn);
            mockVerifiableWrapper.Setup(m => m.WrapThrowsForVerification(wrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = verifiable.Throws<ArgumentException>();
        }
    }
    
}
