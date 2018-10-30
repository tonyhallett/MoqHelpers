using MoqHelpers.InSequence.SetupWrappers;
using System.Collections.Generic;

namespace MoqHelpers.InSequence
{
    public interface ISequence
    {
        void RegisterInvocable(IInvocable invocable);
        void Invoked(IInvocable invocable);
        int Loops { get; }
        VerifyUponInvocation VerifyUponInvocation { set; }

        void Verify(SequenceVerifyDelegate verifier);
        void Verify();
    }
}
