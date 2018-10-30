using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal interface IInvocableNoReturn<TMock> : ISetup<TMock>, IInvocableInternal<ISetup<TMock>> where TMock : class { }
}
