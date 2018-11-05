using Moq;
using Moq.Language;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables.NonSequenced;
using MoqHelpers.InSequence.Invocables.Sequenced;
using System;


namespace MoqHelpers.InSequence.Invocables
{
    internal class InvocableFactory : IInvocableFactory
    {
        public IInvocableGet<T, TProperty> CreateGet<T, TProperty>(ISetupGetter<T, TProperty> setUpGetter, Mock<T> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            return new InvocableGet<T, TProperty>(setUpGetter, mock, sequence, sequenceInvocationIndices);
        }

        public IInvocableGet<T, TProperty> CreateGet<T, TProperty>(ISetupGetter<T, TProperty> setUpGetter, Mock<T> mock, ISequence sequence, int consecutiveInvocations) where T : class
        {
            return new InvocableGet<T, TProperty>(setUpGetter, mock, sequence, consecutiveInvocations);
        }

        public IInvocableNoReturn<T> CreateNoReturn<T>(ISetup<T> setUp, Mock<T> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            return new InvocableNoReturn<T>(setUp, mock, sequence, sequenceInvocationIndices);
        }

        public IInvocableNoReturn<T> CreateNoReturn<T>(ISetup<T> setUp, Mock<T> mock, ISequence sequence, int consecutiveInvocations) where T : class
        {
            return new InvocableNoReturn<T>(setUp, mock, sequence, consecutiveInvocations);
        }

        public IInvocableReturn<T, TResult> CreateReturn<T, TResult>(ISetup<T, TResult> setUpResult, Mock<T> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            return new InvocableReturn<T, TResult>(setUpResult, mock, sequence, sequenceInvocationIndices);
        }

        public IInvocableReturn<T, TResult> CreateReturn<T, TResult>(ISetup<T, TResult> setUpResult, Mock<T> mock, ISequence sequence, int consecutiveInvocations) where T : class
        {
            return new InvocableReturn<T, TResult>(setUpResult, mock, sequence, consecutiveInvocations);
            
        }
        
        public IInvocableSequenceNoReturn<T> CreateSequenceNoReturn<T>(ISetup<T> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<Exception> passOrThrows) where T : class
        {
            return new InvocableSequenceNoReturn<T>(setUp, mock, sequence, new PassOrThrowResponder<T>(setUp, passOrThrows, sequence.Loops));
        }

        public IInvocableSequenceNoReturn<T> CreateSequenceNoReturn<T>(ISetup<T> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<Exception> passOrThrows, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            return new InvocableSequenceNoReturn<T>(setUp, mock, sequence, new PassOrThrowResponder<T>(setUp, passOrThrows, sequence.Loops), sequenceInvocationIndices);
        }

        public IInvocableSequenceReturn<T,TReturn,TReturn> CreateSequenceReturn<T, TReturn>(ISetup<T, TReturn> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<TReturn> returns) where T : class
        {
            return new InvocableSequenceReturns<T, TReturn>(setUp, mock, sequence, new ReturnsInvocationResponder<T,TReturn>(setUp,returns,sequence.Loops));
        }

        public IInvocableSequenceReturn<T, TReturn, TReturn> CreateSequenceReturn<T, TReturn>(ISetup<T, TReturn> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<TReturn> returns, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            return new InvocableSequenceReturns<T, TReturn>(setUp, mock, sequence, new ReturnsInvocationResponder<T, TReturn>( setUp,returns, sequence.Loops),sequenceInvocationIndices);
        }
        public IInvocableSequenceReturn<T, TReturn, ExceptionOrReturn<TReturn>> CreateSequenceReturn<T, TReturn>(ISetup<T, TReturn> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<ExceptionOrReturn<TReturn>> exceptionsOrReturns) where T : class
        {
            return new InvocableSequenceExceptionsOrReturns<T, TReturn>(setUp, mock, sequence, new ExceptionsOrReturnsInvocationResponder<T, TReturn>(setUp, exceptionsOrReturns, sequence.Loops));
        }

        public IInvocableSequenceReturn<T, TReturn, ExceptionOrReturn<TReturn>> CreateSequenceReturn<T, TReturn>(ISetup<T, TReturn> setUp, Mock<T> mock, ISequence sequence, IInvocationResponses<ExceptionOrReturn<TReturn>> exceptionsOrReturns, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            return new InvocableSequenceExceptionsOrReturns<T, TReturn>(setUp, mock, sequence, new ExceptionsOrReturnsInvocationResponder<T, TReturn>( setUp, exceptionsOrReturns, sequence.Loops),sequenceInvocationIndices);
        }
    }
}
