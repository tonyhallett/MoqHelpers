using Moq.Language;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    internal interface IVerifiableWrapper
    {
        IReturnsResult<TResult> WrapReturnsForVerification<TResult>(IReturnsResult<TResult> wrap);

        IThrowsResult WrapThrowsForVerification(IThrowsResult wrap);

        IReturnsThrowsGetter<TMock, TProperty> WrapReturnsThrowsGetterForVerification<TMock, TProperty>(IReturnsThrowsGetter<TMock, TProperty> wrap) where TMock : class;

        ICallbackResult WrapCallbackResultForVerification(ICallbackResult wrap);

        IVerifies WrapVerifiesForVerification(IVerifies wrap);

        ICallBaseResult WrapCallBaseResultForVerification(ICallBaseResult wrap);

        IReturnsThrows<TMock, TResult> WrapReturnsThrowsForVerification<TMock, TResult>(IReturnsThrows<TMock, TResult> wrap) where TMock : class;

        void Verifiable();

        void Verifiable(string failMessage);

        bool Verified { get; }
        string VerifiableFailMessage { get; }
    }
}
