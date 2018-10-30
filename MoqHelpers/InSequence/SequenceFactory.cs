using Moq;

namespace MoqHelpers.InSequence
{
    //will want to look at ISequence again
    internal class SequenceFactory : ISequenceFactory
    {
        public ISequence CreateShared(int loops)
        {
            var sequence = new Sequence(loops);
            return sequence;
        }

        public ISequence CreateSingle<T>(Mock<T> mock, int loops) where T : class
        {
            var sequence = mock.GetAdditional<Sequence>();
            if (sequence == null)
            {
                sequence = new Sequence(loops);
                mock.AddAdditional(sequence);
            }
            return sequence;
        }
    }
}
