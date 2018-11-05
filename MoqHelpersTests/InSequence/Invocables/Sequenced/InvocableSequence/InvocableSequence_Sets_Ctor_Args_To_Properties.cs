using Moq;
using Moq.Language;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.Sequenced;
using NUnit.Framework;

namespace MoqHelpersTests.InSequence.Sequence
{
    [TestFixture]
    internal abstract class InvocableSequence_Sets_Ctor_Args_To_Properties<TInvocable,TWrapped,TResponse> where TInvocable:InvocableSequenceBase<TWrapped,TResponse> where TWrapped:class,IVerifies,IThrows
    {
        private TWrapped wrapped = new Mock<TWrapped>().Object;
        private Mock mock = new Mock<IToMock>();
        private ISequence sequence = new Mock<ISequence>().Object;
        private Mock<IInvocationResponder<TWrapped, TResponse>> mockResponder = new Mock<IInvocationResponder<TWrapped, TResponse>>();
        private IInvocationResponder<TWrapped, TResponse> responder;
        
        protected abstract TInvocable CreateInvocableSequence(TWrapped wrapped, Mock mock, ISequence sequence, IInvocationResponder<TWrapped, TResponse> responder);
        protected abstract TInvocable CreateInvocableSequenceSequenceInvocationIndices(TWrapped wrapped, Mock mock, ISequence sequence, IInvocationResponder<TWrapped, TResponse> responder,SequenceInvocationIndices sequenceInvocationIndices);

        [SetUp]
        public void Setup()
        {
            responder = mockResponder.Object;
        }
        [Test]
        public void Without_SequenceInvocationIndices()
        {
            var configuredResponses = 5;
            mockResponder.SetupGet(m => m.ConfiguredResponses).Returns(configuredResponses);
            var invocableSequence = CreateInvocableSequence(wrapped, mock, sequence, responder);

            Assert.That(invocableSequence.ConsecutiveInvocations, Is.EqualTo(configuredResponses));
            AssertProperties(invocableSequence);
        }
            
        [Test]
        public void With_SequenceInvocationIndices()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices();
            var invocableSequence = CreateInvocableSequenceSequenceInvocationIndices(wrapped, mock, sequence, responder,sequenceInvocationIndices);
            Assert.That(invocableSequence.SequenceInvocationIndices, Is.EqualTo(sequenceInvocationIndices));
            AssertProperties(invocableSequence);
        }
        private void AssertProperties(TInvocable invocableSequence)
        {
            Assert.That(invocableSequence.Mock, Is.EqualTo(mock));
            IInvocableInternal<TWrapped> invocableInternal = (IInvocableInternal<TWrapped>)invocableSequence;
            Assert.That(invocableInternal.Wrapped, Is.EqualTo(wrapped));
            Assert.That(invocableInternal.Sequence, Is.EqualTo(sequence));
            
        }
    }

}
