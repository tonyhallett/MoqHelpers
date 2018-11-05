namespace MoqHelpers.InSequence
{
    internal interface IInvocableInternal<TWrapped> : IInvocable
    {
        TWrapped Wrapped { get; }
        ISequence Sequence { get; }
    }
}
