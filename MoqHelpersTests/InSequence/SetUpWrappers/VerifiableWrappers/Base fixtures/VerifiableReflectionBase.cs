using Moq;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;
using System.Reflection;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal abstract class VerifiableReflectionBase<TWrapped, TWrappedReturn, TVerifiable> : VerifiableWrappers_Returns_Tests_Base<TWrapped, TWrappedReturn, TVerifiable> where TVerifiable : Verifiable<TWrapped> where TWrapped : class where TWrappedReturn : class
    {
        protected abstract void SetupVerifiableWrapper(TWrappedReturn wrappedReturn, TWrappedReturn verifiableWrapperReturn);
        
        public virtual void Reflection_Wraps_Return_From_Wrapped(MethodInfo method, Func<Mock<TWrapped>, TWrappedReturn, object[]> doSetupAndReturnArgs)
        {
            var args = doSetupAndReturnArgs(mockWrapped, mockedWrappedReturn);
            SetupVerifiableWrapper(mockedWrappedReturn, mockedVerifiableWrapperReturn);

            wrappedReturn = (TWrappedReturn)method.Invoke(verifiable, args);
        }
    }
}
