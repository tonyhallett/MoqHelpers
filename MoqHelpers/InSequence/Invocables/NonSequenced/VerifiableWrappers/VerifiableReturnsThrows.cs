using System;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    internal class VerifiableReturnsThrows<TMock, TReturn> : Verifiable<IReturnsThrows<TMock, TReturn>>,IReturnsThrows<TMock, TReturn> where TMock : class
    {
        public VerifiableReturnsThrows(IReturnsThrows<TMock, TReturn> wrapped, IVerifiableWrapper verifiableWrapper) : base(wrapped, verifiableWrapper) { }
        public IReturnsResult<TMock> CallBase()
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.CallBase());
        }
        #region returns
        public IReturnsResult<TMock> Returns(TReturn value)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(value));
        }

        public IReturnsResult<TMock> Returns(Delegate valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns(Func<TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T>(Func<T, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2>(Func<T1, T2, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3>(Func<T1, T2, T3, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

        public IReturnsResult<TMock> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TReturn> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }
        #endregion
        public IThrowsResult Throws(Exception exception)
        {
            return VerifiableWrapper.WrapThrowsForVerification(Wrapped.Throws(exception));
        }

        public IThrowsResult Throws<TException>() where TException : Exception, new()
        {
            return VerifiableWrapper.WrapThrowsForVerification(Wrapped.Throws<TException>());
        }
    }
}
