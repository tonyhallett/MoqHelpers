using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal class VerifiableReturnsThrowsGetter<TMock, TProperty> :Verifiable<IReturnsThrowsGetter<TMock, TProperty>>, IReturnsThrowsGetter<TMock, TProperty> where TMock:class
    {
        public VerifiableReturnsThrowsGetter(IReturnsThrowsGetter<TMock, TProperty> wrapped, IVerifiableWrapper verifiableWrapper):base(wrapped,verifiableWrapper)
        {
        }

        public IReturnsResult<TMock> CallBase()
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.CallBase());
        }

        public IReturnsResult<TMock> Returns(TProperty value)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(value));
        }

        public IReturnsResult<TMock> Returns(Func<TProperty> valueFunction)
        {
            return VerifiableWrapper.WrapReturnsForVerification(Wrapped.Returns(valueFunction));
        }

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
