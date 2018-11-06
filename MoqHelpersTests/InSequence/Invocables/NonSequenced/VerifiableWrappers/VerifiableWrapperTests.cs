using Moq;
using Moq.Language;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MoqHelpersTests.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    [TestFixture]
    public class VerifiableWrapperTests
    {
        [Test]
        public void Should_Not_Be_Verfied_If_Verifiable_Has_Not_Been_Called()
        {
            var verifiableWrapper = new VerifiableWrapper();
            Assert.That(verifiableWrapper.Verified, Is.False);
        }
        [Test]
        public void Should_Be_Verified_When_Verfiable_Called()
        {
            var verifiableWrapper = new VerifiableWrapper();
            
            verifiableWrapper.Verifiable();
            Assert.That(verifiableWrapper.Verified, Is.True);
        }
        [Test]
        public void Should_Be_Verified_With_FailMessage_When_Verfiable_Called_With_Fail_Message()
        {
            var verifiableWrapper = new VerifiableWrapper();
            var failMessage = "fail message";
            verifiableWrapper.Verifiable(failMessage);
            Assert.That(verifiableWrapper.Verified, Is.True);
            Assert.That(verifiableWrapper.VerifiableFailMessage, Is.EqualTo(failMessage));
        }
        [Test]
        public void WrapReturnsForVerification_Should_Wrap_Passing_Self()
        {
            var verifiableWrapper = new VerifiableWrapper();
            var returnsResult = new Mock<IReturnsResult<string>>().Object;
            var wrapped = verifiableWrapper.WrapReturnsForVerification(returnsResult);
            Assert.That(wrapped, Is.TypeOf<VerifiableReturnsResult<string>>());
            var wrappedResult = wrapped as VerifiableReturnsResult<string>;
            Assert.That(wrappedResult.Wrapped, Is.EqualTo(returnsResult));
            Assert.That(wrappedResult.VerifiableWrapper, Is.EqualTo(verifiableWrapper));
        }
        [Test]
        public void WrapThrowsForVerification_Should_Wrap_Passing_Self()
        {
            var verifiableWrapper = new VerifiableWrapper();
            var throwsResult = new Mock<IThrowsResult>().Object;
            var wrapped = verifiableWrapper.WrapThrowsForVerification(throwsResult);
            Assert.That(wrapped, Is.TypeOf<VerifiableThrowsResult>());
            var wrappedResult = wrapped as VerifiableThrowsResult;
            Assert.That(wrappedResult.Wrapped, Is.EqualTo(throwsResult));
            Assert.That(wrappedResult.VerifiableWrapper, Is.EqualTo(verifiableWrapper));
        }
        [Test]
        public void WrapReturnsThrowsGetterForVerification_Should_Wrap_Passing_Self()
        {
            var verifiableWrapper = new VerifiableWrapper();
            var returnsThrowsGetter = new Mock<IReturnsThrowsGetter<IToMock,int>>().Object;
            var wrapped = verifiableWrapper.WrapReturnsThrowsGetterForVerification(returnsThrowsGetter);
            Assert.That(wrapped, Is.TypeOf<VerifiableReturnsThrowsGetter<IToMock,int>>());
            var wrappedResult = wrapped as VerifiableReturnsThrowsGetter<IToMock, int>;
            Assert.That(wrappedResult.Wrapped, Is.EqualTo(returnsThrowsGetter));
            Assert.That(wrappedResult.VerifiableWrapper, Is.EqualTo(verifiableWrapper));
        }
        [Test]
        public void WrapCallbackResultForVerification_Should_Wrap_Passing_Self()
        {
            var verifiableWrapper = new VerifiableWrapper();
            var callbackResult = new Mock<ICallbackResult>().Object;
            var wrapped = verifiableWrapper.WrapCallbackResultForVerification(callbackResult);
            Assert.That(wrapped, Is.TypeOf<VerifiableCallbackResult>());
            var wrappedResult = wrapped as VerifiableCallbackResult;
            Assert.That(wrappedResult.Wrapped, Is.EqualTo(callbackResult));
            Assert.That(wrappedResult.VerifiableWrapper, Is.EqualTo(verifiableWrapper));
        }
        [Test]
        public void WrapVerifiesForVerification_Should_Wrap_Passing_Self()
        {
            var verifiableWrapper = new VerifiableWrapper();
            var verifies = new Mock<IVerifies>().Object;
            var wrapped = verifiableWrapper.WrapVerifiesForVerification(verifies);
            Assert.That(wrapped, Is.TypeOf<VerifiableVerifies>());
            var wrappedResult = wrapped as VerifiableVerifies;
            Assert.That(wrappedResult.Wrapped, Is.EqualTo(verifies));
            Assert.That(wrappedResult.VerifiableWrapper, Is.EqualTo(verifiableWrapper));
        }
        [Test]
        public void WrapCallBaseResultForVerification_Should_Wrap_Passing_Self()
        {
            var verifiableWrapper = new VerifiableWrapper();
            var callbaseResult = new Mock<ICallBaseResult>().Object;
            var wrapped = verifiableWrapper.WrapCallBaseResultForVerification(callbaseResult);
            Assert.That(wrapped, Is.TypeOf<VerifiableCallBaseResult>());
            var wrappedResult = wrapped as VerifiableCallBaseResult;
            Assert.That(wrappedResult.Wrapped, Is.EqualTo(callbaseResult));
            Assert.That(wrappedResult.VerifiableWrapper, Is.EqualTo(verifiableWrapper));
        }
        [Test]
        public void WrapReturnsThrowsForVerification_Should_Wrap_Passing_Self()
        {
            var verifiableWrapper = new VerifiableWrapper();
            var returnsThrows = new Mock<IReturnsThrows<IToMock, int>>().Object;
            var wrapped = verifiableWrapper.WrapReturnsThrowsForVerification(returnsThrows);
            Assert.That(wrapped, Is.TypeOf<VerifiableReturnsThrows<IToMock,int>>());
            var wrappedResult = wrapped as VerifiableReturnsThrows<IToMock, int>;
            Assert.That(wrappedResult.Wrapped, Is.EqualTo(returnsThrows));
            Assert.That(wrappedResult.VerifiableWrapper, Is.EqualTo(verifiableWrapper));
        }
        //
    }
}
