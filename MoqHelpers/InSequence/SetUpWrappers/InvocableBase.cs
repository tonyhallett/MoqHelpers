using Moq;
using Moq.Language;
using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal abstract class InvocableBase<TWrapped>:  IInvocableInternal<TWrapped>,ICallbackInvokedHandler where TWrapped:IVerifies,IThrows
    {
        private Mock mock;
        private int consecutiveInvocations;
        private SequenceInvocationIndices sequenceInvocationIndices;
        private ISequence sequence;
        protected readonly ICallbackWrapper callbackWrapper;
        protected readonly IVerifiableWrapper verifiableWrapper;
        
        protected readonly TWrapped wrapped;
        
        private InvocableBase(TWrapped wrapped, Mock mock, ISequence sequence,IVerifiableWrapper verifiableWrapper,ICallbackWrapper callbackWrapper)
        {
            this.wrapped = wrapped;
            this.mock = mock;
            this.sequence = sequence;
            if (verifiableWrapper == null)
            {
                verifiableWrapper = new VerifiableWrapper();
            }
            this.verifiableWrapper = verifiableWrapper;
            if (callbackWrapper == null)
            {
                callbackWrapper = new CallbackWrapper();
            }
            callbackWrapper.InvokedHandler = this;
            this.callbackWrapper = callbackWrapper;
            RegisterForCallback(wrapped,this.Invoked);
        }
        public InvocableBase(TWrapped wrapped,Mock mock,ISequence sequence,int consecutiveInvocations,IVerifiableWrapper verifiableWrapper=null,ICallbackWrapper callbackWrapper=null):this(wrapped,mock,sequence,verifiableWrapper,callbackWrapper)
        {
            this.consecutiveInvocations = consecutiveInvocations;
        }
        public InvocableBase(TWrapped wrapped, Mock mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices,IVerifiableWrapper verifiableWrapper=null, ICallbackWrapper callbackWrapper = null) :this(wrapped,mock,sequence,verifiableWrapper,callbackWrapper)
        {
            this.sequenceInvocationIndices = sequenceInvocationIndices;
        }
        
        public Mock Mock => mock;
        

        int IInvocable.ConsecutiveInvocations => consecutiveInvocations;
        SequenceInvocationIndices IInvocable.SequenceInvocationIndices { get => sequenceInvocationIndices; set => sequenceInvocationIndices = value; }

        TWrapped IInvocableInternal<TWrapped>.Wrapped => wrapped;

        ISequence IInvocableInternal<TWrapped>.Sequence => sequence;

        public bool Verified
        {
            get
            {
                return verifiableWrapper.Verified;
            }
        }

        protected abstract void RegisterForCallback(TWrapped wrapped,Action invoked);
        private void Invoked() => sequence.Invoked(this);

        #region common for the 'Setup' interfaces that derived implement
        
        public void Verifiable()
        {
            verifiableWrapper.Verifiable();
        }

        public void Verifiable(string failMessage)
        {
            verifiableWrapper.Verifiable(failMessage);
        }
         
        public IThrowsResult Throws(Exception exception)
        {
            return verifiableWrapper.WrapThrowsForVerification(wrapped.Throws(exception));
        }

        public IThrowsResult Throws<TException>() where TException : Exception, new()
        {
            return verifiableWrapper.WrapThrowsForVerification(wrapped.Throws<TException>());
        }

        void ICallbackInvokedHandler.Invoked()
        {
            this.Invoked();
        }

        #endregion

    }
}
