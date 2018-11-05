using System;
using Moq.Language;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    internal class VerifiableReturnsResult<TResult> :Verifiable<IReturnsResult<TResult>>, IReturnsResult<TResult>
    {
        public VerifiableReturnsResult(IReturnsResult<TResult> wrapped,IVerifiableWrapper verifiableWrapper):base(wrapped,verifiableWrapper)
        {
        }
        public IVerifies AtMost(int callCount)
        {
            #pragma warning disable 0618
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.AtMost(callCount));
            #pragma warning restore 0618
        }

        public IVerifies AtMostOnce()
        {
            #pragma warning disable 0618
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.AtMostOnce());
            #pragma warning restore 0618
        }

        #region callbacks
        public ICallbackResult Callback(Delegate callback)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(callback));
        }

        public ICallbackResult Callback(Action action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T>(Action<T> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2>(Action<T1, T2> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }

        public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        {
            return VerifiableWrapper.WrapCallbackResultForVerification(Wrapped.Callback(action));
        }
        #endregion
        #region raises
        public IVerifies Raises(Action<TResult> eventExpression, EventArgs args)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, args));
        }

        public IVerifies Raises(Action<TResult> eventExpression, Func<EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises(Action<TResult> eventExpression, params object[] args)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, args));
        }

        public IVerifies Raises<T1>(Action<TResult> eventExpression, Func<T1, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2>(Action<TResult> eventExpression, Func<T1, T2, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3>(Action<TResult> eventExpression, Func<T1, T2, T3, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }

        public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<TResult> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, EventArgs> func)
        {
            return VerifiableWrapper.WrapVerifiesForVerification(Wrapped.Raises(eventExpression, func));
        }
        #endregion

        public void Verifiable()
        {
            Wrapped.Verifiable();
            VerifiableWrapper.Verifiable();
        }
        public void Verifiable(string failMessage)
        {
            Wrapped.Verifiable(failMessage);
            VerifiableWrapper.Verifiable(failMessage);
        }
    }
    
}
