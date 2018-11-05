using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    interface IInvocableSequenceReturn<TMock,TReturn,TResponse> :  IInvocableSequenceInternal<ISetup<TMock,TReturn>, TResponse>, IInvocableSequenceReturn where TMock : class
    {

    }
}
