using Moq;

namespace MoqHelpers.InSequence
{
    internal interface ISequenceFactory
    {
        ISequence CreateSingle<T>(Mock<T> mock, int loops) where T : class;
        ISequence CreateShared(int loops);
    }
}
