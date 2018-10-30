namespace MoqHelpers.InSequence
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