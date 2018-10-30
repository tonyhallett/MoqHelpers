using System;
using Moq.Language;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal class VerifiableCallBaseResult :Verifiable<ICallBaseResult>, ICallBaseResult
    {
        public VerifiableCallBaseResult(ICallBaseResult wrapped, IVerifiableWrapper verifiableWrapper):base(wrapped,verifiableWrapper)
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

        public IThrowsResult Throws(Exception exception)
        {
            return VerifiableWrapper.WrapThrowsForVerification(Wrapped.Throws(exception));
        }

        public IThrowsResult Throws<TException>() where TException : Exception, new()
        {
            return VerifiableWrapper.WrapThrowsForVerification(Wrapped.Throws<TException>());
        }

        public void Verifiable()
        {
            VerifiableWrapper.Verifiable();
        }

        public void Verifiable(string failMessage)
        {
            VerifiableWrapper.Verifiable(failMessage);
        }
    }
    
}
