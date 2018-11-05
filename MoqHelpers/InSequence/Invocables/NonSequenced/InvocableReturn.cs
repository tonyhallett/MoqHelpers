using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using System;

namespace MoqHelpers.InSequence.Invocables.NonSequenced
{
    internal class InvocableReturn<TMock, TReturn> : InvocableBase<ISetup<TMock, TReturn>>, IInvocableReturn<TMock, TReturn> where TMock : class
    {
        public InvocableReturn(ISetup<TMock, TReturn> wrapped,Mock mock,ISequence sequence,int consecutiveInvocations,IVerifiableWrapper verifiableWapper=null,ICallbackWrapper callbackWrapper=null):base(wrapped,mock,sequence,consecutiveInvocations,verifiableWapper,callbackWrapper)
        {
            
        }

        public InvocableReturn(ISetup<TMock, TReturn> wrapped,Mock mock, ISequence sequence, SequenceInvocationIndices callIndices, IVerifiableWrapper verifiableWapper=null, ICallbackWrapper callbackWrapper=null) :base(wrapped,mock,sequence,callIndices,verifiableWapper,callbackWrapper)
        {
        }

        protected sealed override void RegisterForCallback(ISetup<TMock, TReturn> wrapped,Action invokedAction)
        {
            wrapped.Callback(invokedAction);
        }
        #region callbacks
        public IReturnsThrows<TMock, TReturn> Callback(Delegate callback)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(callback)));
        }

        public IReturnsThrows<TMock, TReturn> Callback(Action action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification( 
                wrapped.Callback(callbackWrapper.WrapCallback(action)));

        }

        public IReturnsThrows<TMock, TReturn> Callback<T>(Action<T> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2>(Action<T1, T2> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public IReturnsThrows<TMock, TReturn> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        {
            return verifiableWrapper.WrapReturnsThrowsForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }
        #endregion
        public IReturnsResult<TMock> CallBase()
        {
            return verifiableWrapper.WrapReturnsForVerification( wrapped.CallBase());
        }
        #region wrapped returns
        public IReturnsResult<TMock> Returns(TReturn value)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(value));
        }

        public IReturnsResult<TMock> Returns(Delegate valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns(Func<TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T>(Func<T, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2>(Func<T1, T2, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3>(Func<T1, T2, T3, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }
        #endregion
        
    }
}
