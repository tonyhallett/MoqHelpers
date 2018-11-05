using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetUpWrappers;
using MoqHelpersTests;
using NUnit.Framework;

namespace MoqHelpersTests.InSequence.Sequence
{
    internal class InvocableSequenceReturns_Sets_Ctor_Args_To_Properties : InvocableSequence_Sets_Ctor_Args_To_Properties<InvocableSequenceReturns<IToMock, string>, ISetup<IToMock, string>, string>
    {
        protected override InvocableSequenceReturns<IToMock, string> CreateInvocableSequence(ISetup<IToMock, string> wrapped, Mock mock, ISequence sequence, IInvocationResponder<ISetup<IToMock, string>, string> responder)
        {
            return new InvocableSequenceReturns<IToMock, string>(wrapped, mock, sequence, responder);
        }

        protected override InvocableSequenceReturns<IToMock, string> CreateInvocableSequenceSequenceInvocationIndices(ISetup<IToMock, string> wrapped, Mock mock, ISequence sequence, IInvocationResponder<ISetup<IToMock, string>, string> responder, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return new InvocableSequenceReturns<IToMock, string>(wrapped, mock, sequence, responder,sequenceInvocationIndices);
        }
    }
}
