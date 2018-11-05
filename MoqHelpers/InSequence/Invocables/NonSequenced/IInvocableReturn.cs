using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.NonSequenced
{
    internal interface IInvocableReturn<TMock, TReturn> : ISetup<TMock, TReturn>, IInvocableInternal<ISetup<TMock,TReturn>> where TMock : class { }
}
