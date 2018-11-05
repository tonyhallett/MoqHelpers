namespace MoqHelpers.InSequence.Invocables
{
    internal interface IInvocableInternal<TWrapped> : IInvocable
    {
        TWrapped Wrapped { get; }
        ISequence Sequence { get; }
    }
}
