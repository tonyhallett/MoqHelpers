namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    internal interface IInvocationResponder<TInvocation, TResponse>
    {
        TInvocation Invocation { get; }
        IInvocationResponses<TResponse> Responses { get; }
        int Loops { get; }

        void Respond();
        int ConfiguredResponses { get; }
    }
}
