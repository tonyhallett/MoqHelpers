using Moq;
using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetUpWrappers
{

    internal class InvocableSequenceNoReturn<TMock> : InvocableSequenceBase<ISetup<TMock>,Exception>, IInvocableSequenceNoReturn<TMock> where TMock : class
    {
        public InvocableSequenceNoReturn(ISetup<TMock> wrapped, Mock mock, ISequence sequence, PassOrThrowResponder<TMock> responder) : base(wrapped, mock, sequence, responder) { }
        public InvocableSequenceNoReturn(ISetup<TMock> wrapped, Mock mock, ISequence sequence, PassOrThrowResponder<TMock> responder,SequenceInvocationIndices sequenceInvocationIndices) : base(wrapped, mock, sequence, responder,sequenceInvocationIndices) { }

        protected override void ApplyCallback(ISetup<TMock> wrapped, Action callback)
        {
            wrapped.Callback(callback);
        }
    }
}
