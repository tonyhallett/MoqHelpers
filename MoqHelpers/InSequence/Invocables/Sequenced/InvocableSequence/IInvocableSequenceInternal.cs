namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    interface IInvocableSequenceInternal<TInvocation, TResponse>: IInvocableInternal<TInvocation>
    {
        IInvocationResponder<TInvocation,TResponse> InvocationResponder { get; }
    }
}
