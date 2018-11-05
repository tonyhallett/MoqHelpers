using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.Sequenced;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MoqHelpers.InSequence
{
    public static class InSequenceExtensions
    {
        #region confirmation
        internal static readonly string ConsecutiveInvocationsLessThanOneMessage = "Consecutive invocations should be one or more";
        internal static readonly string LoopsLessThanOneMessage = "Loops should be one or more";
        internal static readonly string SequenceInvocationIndicesEmptyMessage = "SequenceInvocationIndices cannot be empty";
        internal static readonly string IncorrectNumberOfConfiguredResponses = "Number of responses not equal to number of SequenceInvocationIndices";
        internal static readonly string InvocationResponsesEmptyMessage = "Invocation responses cannot be empty";

        private static void ConfirmResponses<TResponse>(IInvocationResponses<TResponse> responses,string responsesParameterName)
        {
            if (responses == null)
            {
                throw new ArgumentNullException(responsesParameterName);
            }
            else if(responses.ConfiguredResponses==0)
            {
                throw new ArgumentException(InvocationResponsesEmptyMessage);
            }
        }
        private static void ConfirmResponsesAndSequenceInvocationIndices<TResponse>(IInvocationResponses<TResponse> responses,string responsesParameterName, SequenceInvocationIndices sequenceInvocationIndices)
        {
            ConfirmResponses(responses,responsesParameterName);
            ConfirmSequenceInvocationIndices(sequenceInvocationIndices);
            if (responses.ConfiguredResponses != sequenceInvocationIndices.Count)
            {
                throw new ArgumentException(IncorrectNumberOfConfiguredResponses);
            }
        }
        private static void ConfirmLoops(int loops)
        {
            if (loops < 1)
            {
                throw new ArgumentException(LoopsLessThanOneMessage);
            }
        }
        private static void ConfirmConsecutiveInvocations(int consecutiveInvocations)
        {
            if (consecutiveInvocations < 1)
            {
                throw new ArgumentException(ConsecutiveInvocationsLessThanOneMessage);
            }
        }
        private static void ConfirmSequenceInvocationIndices(SequenceInvocationIndices sequenceInvocationIndices)
        {
            if (sequenceInvocationIndices == null)
            {
                throw new ArgumentNullException("sequenceInvocationIndices");
            }
            if (sequenceInvocationIndices.Count == 0)
            {
                throw new ArgumentException(SequenceInvocationIndicesEmptyMessage);
            }
        }
        #endregion

        #region Setup No Result
        //helpers
        private static ISetup<T> SetupInSequence<T>(ISequence sequence, Mock<T> mock, Expression<Action<T>> setUp, int consecutiveInvocations = 1) where T : class
        {
            ConfirmConsecutiveInvocations(consecutiveInvocations);
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateNoReturn<T>(setUpResult,mock, sequence, consecutiveInvocations);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        private static ISetup<T> SetupInSequence<T>(ISequence sequence, Mock<T> mock, Expression<Action<T>> setUp, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            ConfirmSequenceInvocationIndices(sequenceInvocationIndices);
            var setUpResult = mock.Setup(setUp);
            var invocable = Invocable.Factory.CreateNoReturn<T>(setUpResult, mock, sequence, sequenceInvocationIndices);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        //without SequentialInvocationIndices
        public static ISetup<T> SetupInSequenceSingle<T>(this Mock<T> mock, Expression<Action<T>> setUp, int consecutiveInvocations = 1, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupInSequence(Sequence.Factory.CreateSingle(mock, loops), mock, setUp, consecutiveInvocations);
        }
        public static ISetup<T> SetupInSequenceShared<T>(this Mock<T> mock, Expression<Action<T>> setUp, ISequence sequence, int consecutiveInvocations = 1) where T : class
        {
            sequence=sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequence(sequence, mock, setUp, consecutiveInvocations);
        }
        public static ISetup<T> SetupInSequenceCreateShared<T>(this Mock<T> mock, Expression<Action<T>> setUp, out ISequence sequence, int consecutiveInvocations = 1, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequence(sequence, mock, setUp, consecutiveInvocations);
        }
        //with SequentialInvocationIndices
        public static ISetup<T> SetupInSequenceSingle<T>(this Mock<T> mock, Expression<Action<T>> setUp, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupInSequence(Sequence.Factory.CreateSingle(mock, loops), mock, setUp, sequenceInvocationIndices);
        }
        public static ISetup<T> SetupInSequenceShared<T>(this Mock<T> mock, Expression<Action<T>> setUp, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequence(sequence, mock, setUp, sequenceInvocationIndices);
        }
        public static ISetup<T> SetupInSequenceCreateShared<T>(this Mock<T> mock, Expression<Action<T>> setUp, out ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequence(sequence, mock, setUp, sequenceInvocationIndices);
        }

        #region sequence in sequence
        //helpers
        private static IInvocableSequence SetupInSequence<T>(ISequence sequence, Mock<T> mock, Expression<Action<T>> setUp, PassOrThrows passOrThrows) where T : class
        {
            ConfirmResponses(passOrThrows,"passOrThrows");
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateSequenceNoReturn<T>(setUpResult, mock, sequence,passOrThrows);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        private static IInvocableSequence SetupInSequence<T>(ISequence sequence, Mock<T> mock, Expression<Action<T>> setUp, PassOrThrows passOrThrows,SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            ConfirmResponsesAndSequenceInvocationIndices(passOrThrows,"passOrThrows", sequenceInvocationIndices);
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateSequenceNoReturn<T>(setUpResult, mock, sequence, passOrThrows,sequenceInvocationIndices);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        //without indices
        public static IInvocableSequence SetupInSequenceSingle<T>(this Mock<T> mock, Expression<Action<T>> setUp, PassOrThrows passOrThrows, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupInSequence(Sequence.Factory.CreateSingle(mock, loops), mock, setUp,passOrThrows);
        }
        public static IInvocableSequence SetupInSequenceCreateShared<T>(this Mock<T> mock, Expression<Action<T>> setUp,out ISequence sequence, PassOrThrows passOrThrows, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequence(sequence, mock, setUp, passOrThrows);
        }
        public static IInvocableSequence SetupInSequenceShared<T>(this Mock<T> mock, Expression<Action<T>> setUp, ISequence sequence, PassOrThrows passOrThrows) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequence(sequence, mock, setUp, passOrThrows);
        }

        //with indices
        public static IInvocableSequence SetupInSequenceSingle<T>(this Mock<T> mock, Expression<Action<T>> setUp, PassOrThrows passOrThrows,SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T:class
        {
            ConfirmLoops(loops);
            return SetupInSequence<T>(Sequence.Factory.CreateSingle(mock, loops), mock, setUp, passOrThrows, sequenceInvocationIndices);
        }
        public static IInvocableSequence SetupInSequenceCreateShared<T>(this Mock<T> mock, Expression<Action<T>> setUp,out ISequence sequence, PassOrThrows passOrThrows, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequence<T>(sequence, mock, setUp, passOrThrows, sequenceInvocationIndices);

        }
        public static IInvocableSequence SetupInSequenceShared<T>(this Mock<T> mock, Expression<Action<T>> setUp, ISequence sequence, PassOrThrows passOrThrows, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequence<T>(sequence, mock, setUp, passOrThrows, sequenceInvocationIndices);
        }

        #endregion
        #endregion

        #region SetUpSet
        //Note that other overloads have been deprecated
        private static ISetup<T> SetupSetInSequence<T>(ISequence sequence, Mock<T> mock, Action<T> setUp, int consecutiveInvocations = 1) where T : class
        {
            ConfirmConsecutiveInvocations(consecutiveInvocations);
            var setUpResult = mock.SetupSet(setUp);
            var invocable=Invocable.Factory.CreateNoReturn<T>(setUpResult,mock, sequence, consecutiveInvocations);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        private static ISetup<T> SetupSetInSequence<T>(ISequence sequence, Mock<T> mock, Action<T> setUp, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            ConfirmSequenceInvocationIndices(sequenceInvocationIndices);
            var setUpResult = mock.SetupSet(setUp);
            var invocable=Invocable.Factory.CreateNoReturn<T>(setUpResult,mock, sequence, sequenceInvocationIndices);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        public static ISetup<T> SetupSetInSequenceSingle<T>(this Mock<T> mock, Action<T> setUp, int consecutiveInvocations = 1, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupSetInSequence(Sequence.Factory.CreateSingle(mock, loops), mock, setUp, consecutiveInvocations);
        }
        public static ISetup<T> SetupSetInSequenceSingle<T>(this Mock<T> mock, Action<T> setUp, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupSetInSequence(Sequence.Factory.CreateSingle(mock, loops), mock, setUp, sequenceInvocationIndices);
        }
        public static ISetup<T> SetupSetInSequenceShared<T>(this Mock<T> mock, Action<T> setUp, ISequence sequence, int consecutiveInvocations = 1) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupSetInSequence(sequence, mock, setUp, consecutiveInvocations);
        }
        public static ISetup<T> SetupSetInSequenceShared<T>(this Mock<T> mock, Action<T> setUp, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupSetInSequence(sequence, mock, setUp, sequenceInvocationIndices);
        }
        public static ISetup<T> SetupSetInSequenceCreateShared<T>(this Mock<T> mock, Action<T> setUp, out ISequence sequence, int consecutiveInvocations = 1, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupSetInSequence(sequence, mock, setUp, consecutiveInvocations);
        }
        public static ISetup<T> SetupSetInSequenceCreateShared<T>(this Mock<T> mock, Action<T> setUp, out ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupSetInSequence(sequence, mock, setUp, sequenceInvocationIndices);
        }
        #endregion

        #region SetUpGet
        private static ISetupGetter<T, TProperty> SetupGetInSequence<T, TProperty>(ISequence sequence, Mock<T> mock, Expression<Func<T, TProperty>> setupGet, int consecutiveInvocations) where T : class
        {
            ConfirmConsecutiveInvocations(consecutiveInvocations);
            var setUpGetter = mock.SetupGet(setupGet);
            var invocable = Invocable.Factory.CreateGet<T, TProperty>(setUpGetter, mock, sequence, consecutiveInvocations);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        private static ISetupGetter<T, TProperty> SetupGetInSequence<T, TProperty>(ISequence sequence, Mock<T> mock, Expression<Func<T, TProperty>> setupGet, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            ConfirmSequenceInvocationIndices(sequenceInvocationIndices);
            var setUpGetter = mock.SetupGet(setupGet);
            var invocable=Invocable.Factory.CreateGet<T, TProperty>(setUpGetter,mock, sequence, sequenceInvocationIndices);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        public static ISetupGetter<T, TProperty> SetupGetInSequenceSingle<T, TProperty>(this Mock<T> mock, Expression<Func<T, TProperty>> setupGet, int consecutiveInvocations = 1,int loops=1) where T : class
        {
            ConfirmLoops(loops);
            return SetupGetInSequence(Sequence.Factory.CreateSingle(mock,loops), mock, setupGet, consecutiveInvocations);
        }
        public static ISetupGetter<T, TProperty> SetupGetInSequenceSingle<T, TProperty>(this Mock<T> mock, Expression<Func<T, TProperty>> setupGet, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupGetInSequence(Sequence.Factory.CreateSingle(mock, loops), mock, setupGet, sequenceInvocationIndices);
        }
        public static ISetupGetter<T, TProperty> SetupGetInSequenceShared<T, TProperty>(this Mock<T> mock, Expression<Func<T, TProperty>> setupGet, ISequence sequence, int consecutiveInvocations = 1) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            ConfirmConsecutiveInvocations(consecutiveInvocations);
            return SetupGetInSequence(sequence, mock, setupGet, consecutiveInvocations);
        }
        public static ISetupGetter<T, TProperty> SetupGetInSequenceShared<T, TProperty>(this Mock<T> mock, Expression<Func<T, TProperty>> setupGet, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            ConfirmSequenceInvocationIndices(sequenceInvocationIndices);
            return SetupGetInSequence(sequence, mock, setupGet, sequenceInvocationIndices);
        }
        public static ISetupGetter<T, TProperty> SetupGetInSequenceCreateShared<T, TProperty>(this Mock<T> mock, Expression<Func<T, TProperty>> setupGet, out ISequence sequence, int consecutiveInvocations = 1,int loops=1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupGetInSequence(sequence, mock, setupGet, consecutiveInvocations);
        }
        public static ISetupGetter<T, TProperty> SetupGetInSequenceCreateShared<T, TProperty>(this Mock<T> mock, Expression<Func<T, TProperty>> setupGet, out ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupGetInSequence(sequence, mock, setupGet, sequenceInvocationIndices);
        }
        #endregion

        #region SetUp Result
        private static ISetup<T, TResult> SetupInSequenceResult<T, TResult>(ISequence sequence,Mock<T> mock, Expression<Func<T, TResult>> setUp, int consecutiveInvocations = 1) where T : class
        {
            ConfirmConsecutiveInvocations(consecutiveInvocations);
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateReturn<T, TResult>(setUpResult,mock, sequence, consecutiveInvocations);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        private static ISetup<T, TResult> SetupInSequenceResult<T, TResult>(ISequence sequence, Mock<T> mock, Expression<Func<T, TResult>> setUp, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            ConfirmSequenceInvocationIndices(sequenceInvocationIndices);
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateReturn<T, TResult>(setUpResult,mock, sequence, sequenceInvocationIndices);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        public static ISetup<T, TResult> SetupInSequenceSingle<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> setUp, int consecutiveInvocations = 1,int loops=1) where T : class
        {
            ConfirmLoops(loops);
            return SetupInSequenceResult<T,TResult>(Sequence.Factory.CreateSingle(mock,loops),mock, setUp, consecutiveInvocations);
        }
        public static ISetup<T, TResult> SetupInSequenceSingle<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> setUp, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupInSequenceResult<T, TResult>(Sequence.Factory.CreateSingle(mock, loops), mock, setUp, sequenceInvocationIndices);
        }
        public static ISetup<T, TResult> SetupInSequenceShared<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> setUp, ISequence sequence,int consecutiveInvocations = 1) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequenceResult<T, TResult>(sequence, mock, setUp, consecutiveInvocations);
        }
        public static ISetup<T, TResult> SetupInSequenceShared<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> setUp, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequenceResult<T, TResult>(sequence, mock, setUp, sequenceInvocationIndices);
        }
        public static ISetup<T, TResult> SetupInSequenceCreateShared<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> setUp, out ISequence sequence, int consecutiveInvocations = 1,int loops=1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequenceResult(sequence,mock,setUp, consecutiveInvocations);
        }
        public static ISetup<T, TResult> SetupInSequenceCreateShared<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> setUp, out ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequenceResult(sequence, mock, setUp, sequenceInvocationIndices);
        }
        
        #region sequence in sequence
        private static IInvocableSequenceReturn SetupInSequenceResult<T, TReturn>(ISequence sequence, Mock<T> mock, Expression<Func<T, TReturn>> setUp, Returns<TReturn> returns) where T : class
        {
            ConfirmResponses(returns,"returns");
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateSequenceReturn<T, TReturn>(setUpResult, mock, sequence,returns);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        private static IInvocableSequenceReturn SetupInSequenceResult<T, TReturn>(ISequence sequence, Mock<T> mock, Expression<Func<T, TReturn>> setUp, Returns<TReturn> returns, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            ConfirmResponsesAndSequenceInvocationIndices(returns,"returns", sequenceInvocationIndices);
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateSequenceReturn<T, TReturn>(setUpResult, mock, sequence, returns,sequenceInvocationIndices);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        private static IInvocableSequenceReturn SetupInSequenceResult<T, TReturn>(ISequence sequence, Mock<T> mock, Expression<Func<T, TReturn>> setUp, ExceptionsOrReturns<TReturn> exceptionsOrReturns) where T:class
        {
            ConfirmResponses(exceptionsOrReturns,"exceptionsOrReturns");
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateSequenceReturn<T, TReturn>(setUpResult, mock, sequence,exceptionsOrReturns);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }
        private static IInvocableSequenceReturn SetupInSequenceResult<T, TReturn>(ISequence sequence, Mock<T> mock, Expression<Func<T, TReturn>> setUp, ExceptionsOrReturns<TReturn> exceptionsOrReturns, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            ConfirmResponsesAndSequenceInvocationIndices(exceptionsOrReturns,"exceptionsOrReturns", sequenceInvocationIndices);
            var setUpResult = mock.Setup(setUp);
            var invocable=Invocable.Factory.CreateSequenceReturn<T, TReturn>(setUpResult, mock, sequence, exceptionsOrReturns,sequenceInvocationIndices);
            sequence.RegisterInvocable(invocable);
            return invocable;
        }

        public static IInvocableSequenceReturn SetupInSequenceSingle<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp, Returns<TReturn> returns, int loops = 1) where T:class
        {
            ConfirmLoops(loops);
            return SetupInSequenceResult(Sequence.Factory.CreateSingle(mock, loops), mock, setUp, returns);
        }
        public static IInvocableSequenceReturn SetupInSequenceSingle<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp, Returns<TReturn> returns, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupInSequenceResult(Sequence.Factory.CreateSingle(mock, loops), mock, setUp, returns, sequenceInvocationIndices);
        }
        public static IInvocableSequenceReturn SetupInSequenceSingle<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp, ExceptionsOrReturns<TReturn> exceptionsOrReturns, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupInSequenceResult(Sequence.Factory.CreateSingle(mock,loops), mock, setUp, exceptionsOrReturns);
        }
        public static IInvocableSequenceReturn SetupInSequenceSingle<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp, ExceptionsOrReturns<TReturn> exceptionsOrReturns,SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            return SetupInSequenceResult(Sequence.Factory.CreateSingle(mock, loops),mock,setUp, exceptionsOrReturns,sequenceInvocationIndices);
        }

        public static IInvocableSequenceReturn SetupInSequenceCreateShared<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp,out ISequence sequence, Returns<TReturn> returns, int loops = 1) where T:class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequenceResult(sequence, mock, setUp, returns);
        }
        public static IInvocableSequenceReturn SetupInSequenceShared<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp, ISequence sequence, Returns<TReturn> returns) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequenceResult(sequence, mock, setUp, returns);
        }
        public static IInvocableSequenceReturn SetupInSequenceCreateShared<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp,out ISequence sequence, Returns<TReturn> returns, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T:class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequenceResult(sequence, mock, setUp, returns, sequenceInvocationIndices);
        }
        public static IInvocableSequenceReturn SetupInSequenceShared<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp, ISequence sequence, Returns<TReturn> returns, SequenceInvocationIndices sequenceInvocationIndices) where T:class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequenceResult(sequence, mock, setUp, returns, sequenceInvocationIndices);
        }

        public static IInvocableSequenceReturn SetupInSequenceCreateShared<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp,out ISequence sequence, ExceptionsOrReturns<TReturn> exceptionsOrReturns, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequenceResult(sequence, mock, setUp, exceptionsOrReturns);
        }
        public static IInvocableSequenceReturn SetupInSequenceShared<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp,ISequence sequence, ExceptionsOrReturns<TReturn> exceptionsOrReturns) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequenceResult(sequence, mock, setUp, exceptionsOrReturns);
        }
        public static IInvocableSequenceReturn SetupInSequenceCreateShared<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp,out ISequence sequence, ExceptionsOrReturns<TReturn> exceptionsOrReturns, SequenceInvocationIndices sequenceInvocationIndices, int loops = 1) where T : class
        {
            ConfirmLoops(loops);
            sequence = Sequence.Factory.CreateShared(loops);
            return SetupInSequenceResult(sequence, mock, setUp, exceptionsOrReturns, sequenceInvocationIndices);
        }
        public static IInvocableSequenceReturn SetupInSequenceShared<T, TReturn>(this Mock<T> mock, Expression<Func<T, TReturn>> setUp, ISequence sequence, ExceptionsOrReturns<TReturn> exceptionsOrReturns, SequenceInvocationIndices sequenceInvocationIndices) where T : class
        {
            sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
            return SetupInSequenceResult(sequence, mock, setUp, exceptionsOrReturns, sequenceInvocationIndices);
        }
        #endregion

        #endregion

        #region Single Verification
        public static void VerifySequence<T>(this Mock<T> mock) where T : class
        {
            Sequence.SingleSequenceVerify(mock);
        }
        public static void VerifySequence<T>(this Mock<T> mock,SequenceVerifyDelegate verifier) where T : class
        {
            Sequence.SingleSequenceVerify(mock,verifier);
        }
        #endregion
    }
}
