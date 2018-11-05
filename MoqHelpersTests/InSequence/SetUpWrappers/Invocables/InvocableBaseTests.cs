using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence
{
    [TestFixture]
    public class InvocableBaseTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Should_Call_Derived_Register_Callback_On_Wrapped_Upon_Constructor(bool consecutiveInvocations)
        {
            var wrapped = new Mock<ISetup<IToMock>>().Object;
            var dummyInvocable = consecutiveInvocations ? new DummyInvocable(wrapped, null, null, 0, null) : new DummyInvocable(wrapped, null, null, null, null, null);
                
            Assert.That(dummyInvocable.RegisterForCallbackWrapped, Is.EqualTo(wrapped));
        }
        
        [TestCase(true)]
        [TestCase(false)]
        public void Should_Set_InvokedHandler_On_The_CallbackWrapper_To_Self_In_Constructor(bool consecutiveInvocations)
        {
            var mockCallbackWrapper = new Mock<ICallbackWrapper>();
            var mockedCallbackWrapper = mockCallbackWrapper.Object;
            var dummyInvocable = consecutiveInvocations? new DummyInvocable(null, null, null, 0, null,mockedCallbackWrapper): new DummyInvocable(null, null, null, null, null, mockedCallbackWrapper);

            mockCallbackWrapper.VerifySet(m => m.InvokedHandler = dummyInvocable);
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Should_Default_The_VerifiableWrapper_If_Null_In_Constructor(bool consecutiveInvocations)
        {
            var dummyInvocable =  consecutiveInvocations? new DummyInvocable(null, null, null, 0,null): new DummyInvocable(null, null, null, null, null);
            Assert.That(dummyInvocable.VerifiableWrapper, Is.InstanceOf<VerifiableWrapper>());
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Should_Default_The_CallbackWrapper_If_Null(bool consecutiveInvocations)
        {
            var dummyInvocable = consecutiveInvocations? new DummyInvocable(null, null, null, 0, null): new DummyInvocable(null, null, null, null, null);
            Assert.That(dummyInvocable.CallbackWrapper, Is.InstanceOf<CallbackWrapper>());
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Should_Set_The_Mock(bool consecutiveInvocations)
        {
            var mock = new Mock<IToMock>();
            var dummyInvocable = consecutiveInvocations ? new DummyInvocable(null, mock, null, 0, null) : new DummyInvocable(null, mock, null, null, null);
            Assert.That(dummyInvocable.Mock, Is.EqualTo(mock));
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Should_Set_Wrapped(bool consecutiveInvocations)
        {
            var wrapped = new Mock<ISetup<IToMock>>().Object;
            var dummyInvocable = consecutiveInvocations ? new DummyInvocable(wrapped, null, null, 0, null) : new DummyInvocable(wrapped, null, null, null, null);
            Assert.That(((IInvocableInternal<ISetup<IToMock>>)dummyInvocable).Wrapped, Is.EqualTo(wrapped));
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Should_Set_Sequence(bool consecutiveInvocations)
        {
            var sequence = new Mock<ISequence>().Object;
            var dummyInvocable = consecutiveInvocations ? new DummyInvocable(null, null,sequence, 0, null) : new DummyInvocable(null, null, sequence, null, null);
            Assert.That(((IInvocableInternal<ISetup<IToMock>>)dummyInvocable).Sequence, Is.EqualTo(sequence));
        }
        [Test]
        public void Should_Set_ConsecutiveInvocations()
        {
            var dummyInvocable = new DummyInvocable(null, null, null, 10, null);
            Assert.That(((IInvocable)dummyInvocable).ConsecutiveInvocations, Is.EqualTo(10));
        }
        [Test]
        public void Should_Set_SequenceInvocationIndices()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1, 2, 3 };
            var dummyInvocable = new DummyInvocable(null, null, null, sequenceInvocationIndices, null);
            Assert.That(((IInvocable)dummyInvocable).SequenceInvocationIndices, Is.EqualTo(sequenceInvocationIndices));
        }

        [Test]
        public void Should_Invoke_Sequence_When_Invoked_After_Registration()
        {
            var mockSequence = new Mock<ISequence>();
            
            var dummyInvocable = new DummyInvocable(null, null, mockSequence.Object, 0,null);
            dummyInvocable.InvokeBase();

            mockSequence.Verify(s => s.Invoked(dummyInvocable));
        }
        [Test]
        public void Should_Invoke_Sequence_When_Invoked_Through_Callback()
        {
            var mockCallbackWrapper = new Mock<ICallbackWrapper>();
            var mockedCallbackWrapper = mockCallbackWrapper.Object;
            mockCallbackWrapper.SetupAllProperties();
            var mockSequence = new Mock<ISequence>();

            var dummyInvocable = new DummyInvocable(null, null,mockSequence.Object, 0,null,mockedCallbackWrapper);
            ((ICallbackInvokedHandler)dummyInvocable).Invoked();

            mockSequence.Verify(s => s.Invoked(dummyInvocable));
        }
        
        [Test]
        public void Verifiable_Should_Call_The_VerifiableWrapper()
        {
            var mockSetUp = new Mock<ISetup<IToMock>>();
            var mockVerifiableWrapper = new Mock<IVerifiableWrapper>();
            var dummyInvocable = new DummyInvocable(mockSetUp.Object, null, null,0, mockVerifiableWrapper.Object);

            dummyInvocable.Verifiable();
            mockVerifiableWrapper.Verify(m => m.Verifiable());
        }
        [Test]
        public void Verifiable_Msg_Should_Call_Wrapped_And_Be_Verified()
        {
            var mockSetUp = new Mock<ISetup<IToMock>>();
            var mockVerifiableWrapper = new Mock<IVerifiableWrapper>();
            var dummyInvocable = new DummyInvocable(mockSetUp.Object, null, null, 0, mockVerifiableWrapper.Object);
            var msg = "msg";
            dummyInvocable.Verifiable(msg);

            mockVerifiableWrapper.Verify(m => m.Verifiable(msg));
        }
        [TestCase(true)]
        [TestCase(false)]
        public void Verified_Should_Get_From_VerifiableWrapper(bool wrapperVerified)
        {
            var mockSetUp = new Mock<ISetup<IToMock>>();
            var mockVerifiableWrapper = new Mock<IVerifiableWrapper>();
            mockVerifiableWrapper.SetupGet(m => m.Verified).Returns(wrapperVerified);
            var dummyInvocable = new DummyInvocable(mockSetUp.Object, null, null, 0, mockVerifiableWrapper.Object);

            var verified = dummyInvocable.Verified;
            Assert.That(verified, Is.EqualTo(wrapperVerified));
        }

        [Test]
        public void Throws_Specific_Exception_Should_Call_Wrapped_And_Wrap_For_Verification()
        {
            var mockRepository = new MockRepository(MockBehavior.Loose);
            var exception = new Exception();
            var mockSetUp = mockRepository.Create<ISetup<IToMock>>();
            var mockedThrowsResult = new Mock<IThrowsResult>().Object;
            mockSetUp.Setup(m => m.Throws(exception)).Returns(mockedThrowsResult);

            var mockVerifiableWrapper = mockRepository.Create<IVerifiableWrapper>();
            var mockedWrappedThrowsResult = new Mock<IThrowsResult>().Object;
            mockVerifiableWrapper.Setup(m => m.WrapThrowsForVerification(mockedThrowsResult)).Returns(mockedWrappedThrowsResult);

            var dummyInvocable = new DummyInvocable(mockSetUp.Object, null, null, 0, mockVerifiableWrapper.Object);
            var throwsResult = dummyInvocable.Throws(exception);

            mockRepository.VerifyAll();
            Assert.That(throwsResult, Is.EqualTo(mockedWrappedThrowsResult));
        }
        [Test]
        public void Throws_Should_Call_Wrapped_And_Wrap_For_Verification()
        {
            var mockRepository = new MockRepository(MockBehavior.Loose);
            var mockSetUp = mockRepository.Create<ISetup<IToMock>>();
            var mockedThrowsResult = new Mock<IThrowsResult>().Object;
            mockSetUp.Setup(m => m.Throws<ArgumentException>()).Returns(mockedThrowsResult);

            var mockVerifiableWrapper = mockRepository.Create<IVerifiableWrapper>();
            var mockedWrappedThrowsResult = new Mock<IThrowsResult>().Object;
            mockVerifiableWrapper.Setup(m => m.WrapThrowsForVerification(mockedThrowsResult)).Returns(mockedWrappedThrowsResult);

            var dummyInvocable = new DummyInvocable(mockSetUp.Object, null, null, 0, mockVerifiableWrapper.Object);
            var throwsResult = dummyInvocable.Throws<ArgumentException>();

            mockRepository.VerifyAll();
            Assert.That(throwsResult, Is.EqualTo(mockedWrappedThrowsResult));
        }
    }
}
