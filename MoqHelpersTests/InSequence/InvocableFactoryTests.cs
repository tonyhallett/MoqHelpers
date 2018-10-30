using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetupWrappers;
using MoqHelpers.InSequence.SetUpWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MoqHelpersTests.InSequence
{
    [TestFixture]
    public abstract class InvocableFactory_Tests_Base<TWrapped>
    {
        protected TWrapped wrapped;
        internal IInvocableInternal<TWrapped> invocable;

        protected Mock<IToMock> mock;
        protected ISequence sequence;
        internal InvocableFactory invocableFactory = new InvocableFactory();
        [SetUp]
        public void Setup()
        {
            mock = new Mock<IToMock>();

            var mockSequence = new Mock<ISequence>();
            SetupSequence(mockSequence);
            sequence =mockSequence.Object;

            wrapped = CreateWrapped();
        }
        protected abstract TWrapped CreateWrapped();
        protected virtual void SetupSequence(Mock<ISequence> mockSequence) { }
        
        [TearDown]
        public void AssertCommonProperties()
        {
            Assert.That(invocable.Mock, Is.EqualTo(mock));
            Assert.That(invocable.Sequence, Is.EqualTo(sequence));
            Assert.That(invocable.Wrapped, Is.EqualTo(wrapped));
        }
    }
    [TestFixture]
    public abstract class InvocableFactory_Sequence_Base_Tests<TWrapped,TResponse>: InvocableFactory_Tests_Base<TWrapped>
    {
        internal abstract IInvocableSequenceInternal<TWrapped, TResponse> CreateInvocable(TWrapped wrapped, Mock<IToMock> mock, ISequence sequence, IInvocationResponses<TResponse> responses);
        internal abstract IInvocableSequenceInternal<TWrapped, TResponse> CreateInvocable(TWrapped wrapped, Mock<IToMock> mock, ISequence sequence, IInvocationResponses<TResponse> responses, SequenceInvocationIndices sequenceInvocationIndices);

        internal abstract IInvocationResponses<TResponse> CreateInvocationResponses();
        private IInvocableSequenceInternal<TWrapped, TResponse> invocableSequence;
        private IInvocationResponses<TResponse> invocationResponses;

        private int loops = 3;
        protected override void SetupSequence(Mock<ISequence> mockSequence)
        {
            mockSequence.SetupGet(m => m.Loops).Returns(loops);
        }

        [SetUp]
        public void SequenceSetup()
        {
            invocationResponses=CreateInvocationResponses();
        }
        [Test]
        public void Without_SequenceInvocationIndices()
        {
            invocableSequence = CreateInvocable(wrapped, mock, sequence, invocationResponses);
            invocable = invocableSequence;
            
            Assert.That(invocable.ConsecutiveInvocations, Is.EqualTo(invocationResponses.ConfiguredResponses));
        }
        [Test]
        public void With_Sequence_Invocation_Indices()
        {
            SequenceInvocationIndices sequenceInvocationIndices = new SequenceInvocationIndices { 1, 3 };
            invocableSequence = CreateInvocable(wrapped, mock, sequence, invocationResponses,sequenceInvocationIndices);
            invocable = invocableSequence;

            Assert.That(invocable.SequenceInvocationIndices, Is.EqualTo(sequenceInvocationIndices));
        }
        [TearDown]
        public void AssertSequenceInvocable()
        {
            var invocationResponder = invocableSequence.InvocationResponder;
            Assert.That(invocationResponder, Is.Not.Null);
            Assert.That(invocationResponder.Loops, Is.EqualTo(loops));
            Assert.That(invocationResponder.Responses, Is.EqualTo(invocationResponses));
            Assert.That(invocationResponder.Invocation, Is.EqualTo(wrapped));
        }
    }
    public class InvocableFactory_Sequence_PassOrThrows : InvocableFactory_Sequence_Base_Tests<ISetup<IToMock>, Exception>
    {
        internal override IInvocationResponses<Exception> CreateInvocationResponses()
        {
            return new PassOrThrows { null, new Exception() };
        }

        protected override ISetup<IToMock> CreateWrapped()
        {
            return new Mock<ISetup<IToMock>>().Object;
        }

        internal override IInvocableSequenceInternal<ISetup<IToMock>, Exception> CreateInvocable(ISetup<IToMock> wrapped, Mock<IToMock> mock, ISequence sequence, IInvocationResponses<Exception> responses)
        {
            return invocableFactory.CreateSequenceNoReturn(wrapped, mock, sequence, responses);
        }

        internal override IInvocableSequenceInternal<ISetup<IToMock>, Exception> CreateInvocable(ISetup<IToMock> wrapped, Mock<IToMock> mock, ISequence sequence, IInvocationResponses<Exception> responses, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return invocableFactory.CreateSequenceNoReturn(wrapped, mock, sequence, responses,sequenceInvocationIndices);
        }
    }
    public class InvocableFactory_Sequence_Returns : InvocableFactory_Sequence_Base_Tests<ISetup<IToMock, string>, string>
    {
        protected override ISetup<IToMock, string> CreateWrapped()
        {
            return new Mock<ISetup<IToMock, string>>().Object;
        }

        internal override IInvocableSequenceInternal<ISetup<IToMock, string>, string> CreateInvocable(ISetup<IToMock, string> wrapped, Mock<IToMock> mock, ISequence sequence, IInvocationResponses<string> responses)
        {
            return invocableFactory.CreateSequenceReturn(wrapped, mock, sequence, responses);
        }

        internal override IInvocableSequenceInternal<ISetup<IToMock, string>, string> CreateInvocable(ISetup<IToMock, string> wrapped, Mock<IToMock> mock, ISequence sequence, IInvocationResponses<string> responses, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return invocableFactory.CreateSequenceReturn(wrapped, mock, sequence, responses,sequenceInvocationIndices);
        }

        internal override IInvocationResponses<string> CreateInvocationResponses()
        {
            return new Returns<string> { "1", "2", "3" };
        }
    }
    public class InvocableFactory_Sequence_ExceptionsOrReturns : InvocableFactory_Sequence_Base_Tests<ISetup<IToMock, string>, ExceptionOrReturn<string>>
    {
        protected override ISetup<IToMock, string> CreateWrapped()
        {
            return new Mock<ISetup<IToMock, string>>().Object;
        }

        internal override IInvocableSequenceInternal<ISetup<IToMock, string>, ExceptionOrReturn<string>> CreateInvocable(ISetup<IToMock, string> wrapped, Mock<IToMock> mock, ISequence sequence, IInvocationResponses<ExceptionOrReturn<string>> responses)
        {
            return invocableFactory.CreateSequenceReturn(wrapped, mock, sequence, responses);
        }

        internal override IInvocableSequenceInternal<ISetup<IToMock, string>, ExceptionOrReturn<string>> CreateInvocable(ISetup<IToMock, string> wrapped, Mock<IToMock> mock, ISequence sequence, IInvocationResponses<ExceptionOrReturn<string>> responses, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return invocableFactory.CreateSequenceReturn(wrapped, mock, sequence, responses,sequenceInvocationIndices);
        }

        internal override IInvocationResponses<ExceptionOrReturn<string>> CreateInvocationResponses()
        {
            return new ExceptionsOrReturns<string> { ExceptionOrReturnFactory.Return("1"), ExceptionOrReturnFactory.Exception<string>(new Exception())};
        }
    }
    

    [TestFixture]
    public abstract class InvocableFactory_Non_Sequence_Tests_Base<TWrapped>:InvocableFactory_Tests_Base<TWrapped>
    {
        
        internal abstract IInvocableInternal<TWrapped> CreateInvocable(TWrapped wrapped,Mock<IToMock> mock,ISequence sequence,int consecutiveInvocations);
        internal abstract IInvocableInternal<TWrapped> CreateInvocable(TWrapped wrapped, Mock<IToMock> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices);

        [Test]
        public void ConsecutiveInvocations_Test()
        {
            var consecutiveInvocations = 2;

            invocable = CreateInvocable(wrapped, mock, sequence, consecutiveInvocations);

            Assert.That(invocable.ConsecutiveInvocations, Is.EqualTo(consecutiveInvocations));
        }
        [Test]
        public void SequenceInvocationIndices_Test()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 1, 2 };

            invocable = CreateInvocable(wrapped, mock, sequence, sequenceInvocationIndices);

            Assert.That(invocable.SequenceInvocationIndices, Is.EqualTo(sequenceInvocationIndices));
        }
        
    }
    
    public class InvocableFactory_CreateGet : InvocableFactory_Non_Sequence_Tests_Base<ISetupGetter<IToMock, int>>
    {
        protected override ISetupGetter<IToMock, int> CreateWrapped()
        {
            return new Mock<ISetupGetter<IToMock, int>>().Object;
        }

        internal override IInvocableInternal<ISetupGetter<IToMock, int>> CreateInvocable(ISetupGetter<IToMock, int> wrapped, Mock<IToMock> mock, ISequence sequence, int consecutiveInvocations)
        {
            return invocableFactory.CreateGet(wrapped, mock, sequence, consecutiveInvocations);
        }

        internal override IInvocableInternal<ISetupGetter<IToMock, int>> CreateInvocable(ISetupGetter<IToMock, int> wrapped, Mock<IToMock> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return invocableFactory.CreateGet(wrapped, mock, sequence, sequenceInvocationIndices);
        }
    }
    public class InvocableFactory_CreateNoReturn : InvocableFactory_Non_Sequence_Tests_Base<ISetup<IToMock>>
    {
        protected override ISetup<IToMock> CreateWrapped()
        {
            return new Mock<ISetup<IToMock>>().Object;
        }

        internal override IInvocableInternal<ISetup<IToMock>> CreateInvocable(ISetup<IToMock> wrapped, Mock<IToMock> mock, ISequence sequence, int consecutiveInvocations)
        {
            return invocableFactory.CreateNoReturn(wrapped, mock, sequence, consecutiveInvocations);
        }

        internal override IInvocableInternal<ISetup<IToMock>> CreateInvocable(ISetup<IToMock> wrapped, Mock<IToMock> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return invocableFactory.CreateNoReturn(wrapped, mock, sequence, sequenceInvocationIndices);
        }
    }
    public class InvocableFactory_CreateReturn : InvocableFactory_Non_Sequence_Tests_Base<ISetup<IToMock, string>>
    {
        protected override ISetup<IToMock, string> CreateWrapped()
        {
            return new Mock<ISetup<IToMock, string>>().Object;
        }

        internal override IInvocableInternal<ISetup<IToMock, string>> CreateInvocable(ISetup<IToMock, string> wrapped, Mock<IToMock> mock, ISequence sequence, int consecutiveInvocations)
        {
            return invocableFactory.CreateReturn(wrapped, mock, sequence, consecutiveInvocations);
        }

        internal override IInvocableInternal<ISetup<IToMock, string>> CreateInvocable(ISetup<IToMock, string> wrapped, Mock<IToMock> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return invocableFactory.CreateReturn(wrapped, mock, sequence, sequenceInvocationIndices);
        }
    }

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
    internal class DummyInvocable : InvocableBase<ISetup<IToMock>>
    {
        public ISetup<IToMock> RegisterForCallbackWrapped;
        public DummyInvocable(ISetup<IToMock> wrapped, Mock<IToMock> mock, ISequence sequence, int consecutiveInvocations,IVerifiableWrapper verifiableWrapper,ICallbackWrapper callbackWrapper=null) : base(wrapped, mock, sequence, consecutiveInvocations,verifiableWrapper,callbackWrapper) { }
        public DummyInvocable(ISetup<IToMock> wrapped, Mock<IToMock> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper = null) : base(wrapped, mock, sequence, sequenceInvocationIndices, verifiableWrapper, callbackWrapper) { }
        public bool WrappedCallbackInvoked = false;
        private Action invokedAction;
        protected override void RegisterForCallback(ISetup<IToMock> wrapped, Action invokedAction)
        {
            RegisterForCallbackWrapped = wrapped;
            this.invokedAction = invokedAction;
        }
        public void InvokeBase()
        {
            invokedAction();
        }
        public IVerifiableWrapper VerifiableWrapper
        {
            get => this.verifiableWrapper;
        }
        public ICallbackWrapper CallbackWrapper
        {
            get => this.callbackWrapper;
        }
    }
}
