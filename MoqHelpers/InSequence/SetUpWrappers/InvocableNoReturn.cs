using Moq;
using Moq.Language;
using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal class InvocableNoReturn<TMock> : InvocableBase<ISetup<TMock>>, IInvocableNoReturn<TMock> where TMock : class
    {
        public InvocableNoReturn(ISetup<TMock> wrapped,Mock mock, ISequence sequence,int consecutiveInvocations) : base(wrapped,mock,sequence,consecutiveInvocations)
        {
            
        }
        public InvocableNoReturn(ISetup<TMock> wrapped,Mock mock, ISequence sequence, SequenceInvocationIndices callIndices) : base(wrapped,mock, sequence, callIndices)
        {

        }


        public IVerifies AtMost(int callCount)
        {
            #pragma warning disable 0618
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.AtMost(callCount));
            #pragma warning restore 0618
        }

        public IVerifies AtMostOnce()
        {
            #pragma warning disable 0618
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.AtMostOnce());
            #pragma warning restore 0618
        }

        #region callbacks
        public ICallbackResult Callback(Delegate callback)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(callback)));
        }

        public ICallbackResult Callback(Action action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T>(Action<T> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2>(Action<T1, T2> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        {
            return verifiableWrapper.WrapCallbackResultForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }
        #endregion
        public ICallBaseResult CallBase()
        {
            return verifiableWrapper.WrapCallBaseResultForVerification( wrapped.CallBase());
        }
        #region wrapped raises
        public IVerifies Raises(Action<TMock> eventExpression, EventArgs args)
        {
            return verifiableWrapper.WrapVerifiesForVerification( wrapped.Raises(eventExpression, args));
        }

        public IVerifies Raises(Action<TMock> eventExpression, Func<EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification( wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises(Action<TMock> eventExpression, params object[] args)
        {
            return verifiableWrapper.WrapVerifiesForVerification( wrapped.Raises(eventExpression, args));
        }

        public IVerifies Raises<T1>(Action<TMock> eventExpression, Func<T1, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification( wrapped.Raises(eventExpression,func));
        }

        public IVerifies Raises<T1, T2>(Action<TMock> eventExpression, Func<T1, T2, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3>(Action<TMock> eventExpression, Func<T1, T2, T3, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<TMock> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, EventArgs> func)
        {
            return verifiableWrapper.WrapVerifiesForVerification(wrapped.Raises(eventExpression, func));
        }
        #endregion
        
        protected override void RegisterForCallback(ISetup<TMock> wrapped, Action invokedAction)
        {
            wrapped.Callback(invokedAction);
        }
    }
}
