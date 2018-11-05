using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetUpWrappers;
using NUnit.Framework;

namespace MoqHelpersTests.InSequence.Sequence
{
    internal class InvocableSequenceExceptionsOrReturns_Sets_Ctor_Args_To_Properties : InvocableSequence_Sets_Ctor_Args_To_Properties<InvocableSequenceExceptionsOrReturns<IToMock, string>, ISetup<IToMock, string>, ExceptionOrReturn<string>>
    {
        protected override InvocableSequenceExceptionsOrReturns<IToMock, string> CreateInvocableSequence(ISetup<IToMock, string> wrapped, Mock mock, ISequence sequence, IInvocationResponder<ISetup<IToMock, string>, ExceptionOrReturn<string>> responder)
        {
            return new InvocableSequenceExceptionsOrReturns<IToMock, string>(wrapped, mock, sequence, responder);
        }

        protected override InvocableSequenceExceptionsOrReturns<IToMock, string> CreateInvocableSequenceSequenceInvocationIndices(ISetup<IToMock, string> wrapped, Mock mock, ISequence sequence, IInvocationResponder<ISetup<IToMock, string>, ExceptionOrReturn<string>> responder, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return new InvocableSequenceExceptionsOrReturns<IToMock, string>(wrapped, mock, sequence, responder,sequenceInvocationIndices);
        }
    }
}
