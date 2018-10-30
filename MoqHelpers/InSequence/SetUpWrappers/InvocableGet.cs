using Moq;
using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetupWrappers
{
    
    internal class InvocableGet<TMock, TProperty> :InvocableBase<ISetupGetter<TMock, TProperty>>, IInvocableGet<TMock, TProperty> where TMock : class
    {
        public InvocableGet(ISetupGetter<TMock, TProperty> wrapped,Mock mock,ISequence sequence,int consecutiveInvocations):base(wrapped,mock,sequence,consecutiveInvocations)
        {
            
        }
        public InvocableGet(ISetupGetter<TMock, TProperty> wrapped,Mock mock, ISequence sequence, SequenceInvocationIndices callIndices) : base(wrapped,mock, sequence, callIndices)
        {

        }
        protected override void RegisterForCallback(ISetupGetter<TMock, TProperty> wrapped, Action invokedAction)
        {
            wrapped.Callback(invokedAction);
        }
        public IReturnsThrowsGetter<TMock, TProperty> Callback(Action action)
        {
            return verifiableWrapper.WrapReturnsThrowsGetterForVerification(
                wrapped.Callback(callbackWrapper.WrapCallback(action)));
        }
        #region pass throughs
        public IReturnsResult<TMock> CallBase()
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.CallBase());
        }

        public IReturnsResult<TMock> Returns(TProperty value)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(value));
        }

        public IReturnsResult<TMock> Returns(Func<TProperty> valueFunction)
        {
            return verifiableWrapper.WrapReturnsForVerification(wrapped.Returns(valueFunction));
        }
        
        #endregion
    }
}
