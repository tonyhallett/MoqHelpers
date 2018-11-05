using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables
{
    internal interface IInvocableGet<TMock, TProperty> : ISetupGetter<TMock, TProperty>, IInvocableInternal<ISetupGetter<TMock, TProperty>> where TMock : class { }
}
