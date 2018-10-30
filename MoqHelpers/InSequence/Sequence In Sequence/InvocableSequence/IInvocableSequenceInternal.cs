namespace MoqHelpers.InSequence.SetUpWrappers
{
    interface IInvocableSequenceInternal<TInvocation, TResponse>: IInvocableInternal<TInvocation>
    {
        IInvocationResponder<TInvocation,TResponse> InvocationResponder { get; }
    }
}
