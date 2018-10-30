using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    internal abstract class InvocableSequenceReturnBase<TMock, TReturn,TResponse> : InvocableSequenceBase<ISetup<TMock, TReturn>, TResponse>, IInvocableSequenceReturn where TMock : class
    {

        public InvocableSequenceReturnBase(ISetup<TMock, TReturn> wrapped, Mock mock, ISequence sequence, InvocationReturnResponder<TMock, TReturn, TResponse> invocationResponder)
           : base(wrapped, mock, sequence, invocationResponder) { }

        public InvocableSequenceReturnBase(ISetup<TMock, TReturn> wrapped, Mock mock, ISequence sequence, InvocationReturnResponder<TMock, TReturn, TResponse> invocationResponder,SequenceInvocationIndices sequenceInvocationIndices)
           : base(wrapped, mock, sequence, invocationResponder,sequenceInvocationIndices) { }

        public void CallBase()
        {
            wrapped.CallBase();
        }

        protected override void ApplyCallback(ISetup<TMock, TReturn> wrapped, Action callback)
        {
            wrapped.Callback(callback);
        }
    }

}
