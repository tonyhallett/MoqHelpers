using Moq;
using Moq.Language;
using MoqHelpers.InSequence.SetupWrappers;
using System;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    internal abstract class InvocableSequenceBase<TWrapped,TResponse> : InvocableBase<TWrapped>, IInvocableSequenceInternal<TWrapped, TResponse> where TWrapped : IVerifies, IThrows
    {
        private IInvocationResponder<TWrapped, TResponse> invocationResponder;
        public IInvocationResponder<TWrapped, TResponse> InvocationResponder => invocationResponder;
        public InvocableSequenceBase(TWrapped wrapped, Mock mock, ISequence sequence, IInvocationResponder<TWrapped, TResponse> invocationResponder):base(wrapped,mock,sequence,invocationResponder.ConfiguredResponses)
        {
            this.invocationResponder = invocationResponder;
        }
        public InvocableSequenceBase(TWrapped wrapped, Mock mock, ISequence sequence, IInvocationResponder<TWrapped, TResponse> invocationResponder, SequenceInvocationIndices sequenceInvocationIndices) : base(wrapped, mock, sequence,sequenceInvocationIndices)
        {
            this.invocationResponder = invocationResponder;
        }
        protected override void RegisterForCallback(TWrapped wrapped, Action invokedAction)
        {
            ApplyCallback(wrapped,() =>
            {
                invocationResponder.Respond();
                invokedAction();
            });
        }
        protected abstract void ApplyCallback(TWrapped wrapped, Action callback);
    }
}
