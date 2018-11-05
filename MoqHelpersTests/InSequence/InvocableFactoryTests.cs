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
