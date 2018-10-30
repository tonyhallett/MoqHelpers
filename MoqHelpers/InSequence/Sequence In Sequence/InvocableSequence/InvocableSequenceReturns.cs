using Moq;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    internal class InvocableSequenceReturns<TMock, TReturn> : InvocableSequenceReturnBase<TMock, TReturn, TReturn>, IInvocableSequenceReturn<TMock, TReturn, TReturn> where TMock : class
    {
        public InvocableSequenceReturns(ISetup<TMock, TReturn> wrapped, Mock mock, ISequence sequence,
            ReturnsInvocationResponder<TMock, TReturn> invocationResponder) : base(wrapped, mock, sequence, invocationResponder)
        {
        }
        public InvocableSequenceReturns(ISetup<TMock, TReturn> wrapped, Mock mock, ISequence sequence,
            ReturnsInvocationResponder<TMock, TReturn> invocationResponder, SequenceInvocationIndices sequenceInvocationIndices) : base(wrapped, mock, sequence, invocationResponder,sequenceInvocationIndices)
        {
        }
        
    }

}
