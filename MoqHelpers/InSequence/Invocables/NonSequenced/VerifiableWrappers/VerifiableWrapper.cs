using Moq.Language;
using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    internal class VerifiableWrapper: IVerifiableWrapper
    {
        #region wrapping for verification
        public IReturnsResult<TResult> WrapReturnsForVerification<TResult>(IReturnsResult<TResult> wrap)
        {
            return new VerifiableReturnsResult<TResult>(wrap, this);
        }
        public IThrowsResult WrapThrowsForVerification(IThrowsResult wrap)
        {
            return new VerifiableThrowsResult(wrap, this);
        }
        public IReturnsThrowsGetter<TMock, TProperty> WrapReturnsThrowsGetterForVerification<TMock, TProperty>(IReturnsThrowsGetter<TMock, TProperty> wrap) where TMock : class
        {
            return new VerifiableReturnsThrowsGetter<TMock, TProperty>(wrap, this);
        }
        public ICallbackResult WrapCallbackResultForVerification(ICallbackResult wrap)
        {
            return new VerifiableCallbackResult(wrap, this);
        }
        public IVerifies WrapVerifiesForVerification(IVerifies wrap)
        {
            return new VerifiableVerifies(wrap, this);
        }
        public ICallBaseResult WrapCallBaseResultForVerification(ICallBaseResult wrap)
        {
            return new VerifiableCallBaseResult(wrap, this);
        }
        public IReturnsThrows<TMock, TResult> WrapReturnsThrowsForVerification<TMock, TResult>(IReturnsThrows<TMock, TResult> wrap) where TMock : class
        {
            return new VerifiableReturnsThrows<TMock, TResult>(wrap, this);
        }
        
        
        #endregion
        #region verification
        
        public void Verifiable()
        {
            this.Verified = true;
        }
        public void Verifiable(string failMessage)
        {
            VerifiableFailMessage = failMessage;
            this.Verified = true;
        }


        public bool Verified
        {
            get;
            private set;
        }
        public string VerifiableFailMessage
        {
            get;
            private set;
        }
        #endregion
    }
}
