using Moq;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    internal class InvocableSequenceExceptionsOrReturns<TMock, TReturn> : InvocableSequenceReturnBase<TMock, TReturn, ExceptionOrReturn<TReturn>>, IInvocableSequenceReturn<TMock, TReturn, ExceptionOrReturn<TReturn>> where TMock:class
    {
        public InvocableSequenceExceptionsOrReturns(ISetup<TMock, TReturn> wrapped, Mock mock, ISequence sequence,
            IInvocationResponder<ISetup<TMock, TReturn>, ExceptionOrReturn<TReturn>> invocationResponder) : base(wrapped, mock, sequence, invocationResponder)
        {
        }
        public InvocableSequenceExceptionsOrReturns(ISetup<TMock, TReturn> wrapped, Mock mock, ISequence sequence,
            IInvocationResponder<ISetup<TMock, TReturn>, ExceptionOrReturn<TReturn>> invocationResponder, SequenceInvocationIndices sequenceInvocationIndices) : base(wrapped, mock, sequence, invocationResponder, sequenceInvocationIndices)
        {
        }
    }
}
