using System;
namespace MoqHelpers.InSequence
{
    public static class Sequences {
        public static void Verify(params ISequence[] sequences)
        {
            foreach (var sequence in sequences)
            {
                sequence.Verify();
            }
        }
        public static void Verify(SequenceVerifyDelegate verifier, params ISequence[] sequences)
        {
            foreach (var sequence in sequences)
            {
                sequence.Verify(verifier);
            }
        }
        [ThreadStatic]
        public static VerifyUponInvocation VerifyUponInvocation;
    }
}
