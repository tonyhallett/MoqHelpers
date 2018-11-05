namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    internal interface IInvocationResponses<TResponse>
    {
        int ConfiguredResponses { get; }
        TResponse this[int invocationNumber]
        {
            get;
        }
    }
}