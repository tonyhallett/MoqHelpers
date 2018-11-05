using System;
using Moq.Language;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal class VerifiableThrowsResult :Verifiable<IThrowsResult>, IThrowsResult
    {
        public VerifiableThrowsResult(IThrowsResult wrapped, IVerifiableWrapper verifiableWrapper) : base(wrapped, verifiableWrapper)
        { }

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
