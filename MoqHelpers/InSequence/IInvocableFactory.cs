using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.SetupWrappers;
using MoqHelpers.InSequence.SetUpWrappers;
using System;

namespace MoqHelpers.InSequence
{
    internal interface IInvocableFactory
    {
        IInvocableGet<T, TProperty> CreateGet<T, TProperty>(ISetupGetter<T, TProperty> setUpGetter, Mock<T> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class;
        IInvocableGet<T, TProperty> CreateGet<T, TProperty>(ISetupGetter<T, TProperty> setUpGetter, Mock<T> mock, ISequence sequence, int consecutiveInvocations) where T : class;

        IInvocableNoReturn<T> CreateNoReturn<T>(ISetup<T> setUp, Mock<T> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class;
        IInvocableNoReturn<T> CreateNoReturn<T>(ISetup<T> setUp, Mock<T> mock, ISequence sequence, int consecutiveInvocations) where T : class;

        IInvocableReturn<T, TResult> CreateReturn<T, TResult>(ISetup<T, TResult> setUpResult, Mock<T> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class;
        IInvocableReturn<T, TResult> CreateReturn<T, TResult>(ISetup<T, TResult> setUpResult, Mock<T> mock, ISequence sequence, int consecutiveInvocations) where T : class;

        IInvocableSequenceNoReturn<T> CreateSequenceNoReturn<T>(ISetup<T> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<Exception> passOrThrows) where T : class;
        IInvocableSequenceNoReturn<T> CreateSequenceNoReturn<T>(ISetup<T> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<Exception> passOrThrows, SequenceInvocationIndices sequenceInvocationIndices) where T : class;


        IInvocableSequenceReturn<T, TReturn, TReturn> CreateSequenceReturn<T, TReturn>(ISetup<T, TReturn> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<TReturn> returns) where T : class;
        IInvocableSequenceReturn<T, TReturn, TReturn> CreateSequenceReturn<T, TReturn>(ISetup<T, TReturn> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<TReturn> returns, SequenceInvocationIndices sequenceInvocationIndices) where T : class;
        IInvocableSequenceReturn<T, TReturn, ExceptionOrReturn<TReturn>> CreateSequenceReturn<T, TReturn>(ISetup<T, TReturn> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<ExceptionOrReturn<TReturn>> exceptionsOrReturns) where T : class;
        IInvocableSequenceReturn<T, TReturn, ExceptionOrReturn<TReturn>> CreateSequenceReturn<T, TReturn>(ISetup<T, TReturn> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<ExceptionOrReturn<TReturn>> exceptionsOrReturns, SequenceInvocationIndices sequenceInvocationIndices) where T : class;
    }
}
