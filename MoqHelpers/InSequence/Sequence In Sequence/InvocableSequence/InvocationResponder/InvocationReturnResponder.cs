using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    //internal abstract class InvocationReturnResponder<TInvocation, TResponse> : InvocationResponder<TInvocation,TResponse>
    //{
    //    public InvocationReturnResponder(TInvocation invocation, InvocationResponses<TResponse> responses,int loops) : base(invocation, responses,loops)
    //    {
    //    }
    //    protected TResponse GetDefault()
    //    {
    //        return GetDefault<TResponse>();
    //    }
    //    protected T GetDefault<T>()
    //    {
    //        var returnType = typeof(T);
    //        T returnValue = default(T);
    //        if (returnType.GetType().IsValueType)
    //        {
    //            returnValue = (T)Activator.CreateInstance(returnType);
    //        }
    //        return returnValue;
    //    }

    //}
    internal abstract class InvocationReturnResponder< TMock,TReturn,TResponse> : InvocationResponder<ISetup<TMock, TReturn>, TResponse> where TMock:class
    {
        public InvocationReturnResponder(ISetup<TMock, TReturn> invocation, IInvocationResponses<TResponse> responses, int loops) : base(invocation, responses, loops)
        {
        }
        protected TResponse GetDefault()
        {
            return GetDefault<TResponse>();
        }
        protected T GetDefault<T>()
        {
            var returnType = typeof(T);
            T returnValue = default(T);
            if (returnType.GetType().IsValueType)
            {
                returnValue = (T)Activator.CreateInstance(returnType);
            }
            return returnValue;
        }

    }
    
}
