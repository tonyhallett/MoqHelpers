using System;
using Moq.Language;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    internal class VerifiableCallbackResult :Verifiable<ICallbackResult>, ICallbackResult
    {
        public VerifiableCallbackResult(ICallbackResult wrapped, IVerifiableWrapper verifiableWrapper):base(wrapped,verifiableWrapper)
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
        
        public ICallBaseResult CallBase()
        {
            return VerifiableWrapper.WrapCallBaseResultForVerification(Wrapped.CallBase());
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
