using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.NonSequenced
{
    internal interface IInvocableNoReturn<TMock> : ISetup<TMock>, IInvocableInternal<ISetup<TMock>> where TMock : class { }
}
