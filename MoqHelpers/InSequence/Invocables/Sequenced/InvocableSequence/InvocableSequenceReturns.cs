﻿using Moq;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    internal class InvocableSequenceReturns<TMock, TReturn> : InvocableSequenceReturnBase<TMock, TReturn, TReturn>, IInvocableSequenceReturn<TMock, TReturn, TReturn> where TMock : class
    {
        public InvocableSequenceReturns(ISetup<TMock, TReturn> wrapped, Mock mock, ISequence sequence,
            IInvocationResponder<ISetup<TMock, TReturn>, TReturn> invocationResponder) : base(wrapped, mock, sequence, invocationResponder)
        {
        }
        public InvocableSequenceReturns(ISetup<TMock, TReturn> wrapped, Mock mock, ISequence sequence,
            IInvocationResponder<ISetup<TMock, TReturn>, TReturn> invocationResponder, SequenceInvocationIndices sequenceInvocationIndices) : base(wrapped, mock, sequence, invocationResponder, sequenceInvocationIndices)
        {
        }
    }

}
