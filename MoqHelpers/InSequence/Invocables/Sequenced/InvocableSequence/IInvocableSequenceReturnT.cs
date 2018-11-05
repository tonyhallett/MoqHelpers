using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    interface IInvocableSequenceReturn<TMock,TReturn,TResponse> :  IInvocableSequenceInternal<ISetup<TMock,TReturn>, TResponse>, IInvocableSequenceReturn where TMock : class
    {

    }
}
