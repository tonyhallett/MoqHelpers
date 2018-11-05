using System;
using Moq.Language;

namespace MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    internal class VerifiableVerifies : Verifiable<IVerifies>,IVerifies
    {
        public VerifiableVerifies(IVerifies wrapped, IVerifiableWrapper verifiableWrapper):base(wrapped,verifiableWrapper)
        {
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
