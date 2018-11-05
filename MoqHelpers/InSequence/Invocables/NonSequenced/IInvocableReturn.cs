using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal interface IInvocableReturn<TMock, TReturn> : ISetup<TMock, TReturn>, IInvocableInternal<ISetup<TMock,TReturn>> where TMock : class { }
}
