using Moq;
using Moq.Language;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;
using System.Reflection;

namespace MoqHelpersTests.InSequence.SetUpWrappers
{
    [TestFixture]
    internal abstract class Invocable_Reflection_Tests_Base<TWrapped, TWrappedReturn, TInvocable>: Invocable_Wraps_Test_Base<TWrapped, TWrappedReturn, TInvocable> where TWrappedReturn : class where TWrapped : class, IVerifies, IThrows where TInvocable : InvocableBase<TWrapped>
    {
        protected abstract void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, TWrappedReturn wrappedReturn, TWrappedReturn verifiableWrapperReturn);

        protected void Reflection_Wraps_Return_From_Wrapped(MethodInfo method, Func<Mock<TWrapped>, TWrappedReturn,Mock<ICallbackWrapper>, object[]> doSetupAndReturnArgs)
        {
            var args = doSetupAndReturnArgs(mockWrapped, mockedWrappedReturn, mockCallbackWrapper);
            SetupVerifiableWrapper(mockVerifiableWrapper, mockedWrappedReturn, mockedVerifiableWrapperReturn);

            wrappedReturn = (TWrappedReturn)method.Invoke(invocable, args);
        }
    }
}
