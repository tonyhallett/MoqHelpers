using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal interface IInvocableGet<TMock, TProperty> : ISetupGetter<TMock, TProperty>, IInvocableInternal<ISetupGetter<TMock, TProperty>> where TMock : class { }
}
