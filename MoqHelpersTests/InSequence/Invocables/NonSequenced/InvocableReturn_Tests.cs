using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using MoqHelpersTests.InSequence.Invocables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MoqHelpersTests.InSequence.SetUpWrappers
{
    internal class InvocableReturn_Callback_Tests : Invocable_Reflection_Tests_Base<ISetup<IToMock, int>, IReturnsThrows<IToMock, int>, InvocableReturn<IToMock, int>>
    {
        protected override InvocableReturn<IToMock, int> CreateInvocable(ISetup<IToMock, int> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableReturn<IToMock, int>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, IReturnsThrows<IToMock, int> wrappedReturn, IReturnsThrows<IToMock, int> verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapReturnsThrowsForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
        [TestCaseSource("Source")]
        public void CallbackWrapper_Wraps_Before_Calling_Wrapped_And_Wrapping_The_Return_For_Verification(MethodInfo method, Func<Mock<ISetup<IToMock, int>>, IReturnsThrows<IToMock, int>,Mock<ICallbackWrapper>, object[]> doSetupAndReturnArgs)
        {
            base.Reflection_Wraps_Return_From_Wrapped(method, doSetupAndReturnArgs);
        }
        public static IEnumerable<TestCaseData> Source()
        {
            var source= ReflectionHelper.CallbackSource<ISetup<IToMock, int>, IReturnsThrows<IToMock, int>>();
            return source;
        }

    }
    //issue accomplishing with reflection
    internal class InvocableReturn_Delegate_Callback_Test : Invocable_Delegate_Callback_Test<ISetup<IToMock, int>, IReturnsThrows<IToMock, int>, InvocableReturn<IToMock, int>>
    {
        protected override InvocableReturn<IToMock, int> CreateInvocable(ISetup<IToMock, int> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableReturn<IToMock, int>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, IReturnsThrows<IToMock, int> wrappedReturn, IReturnsThrows<IToMock, int> verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapReturnsThrowsForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }

        protected override void SetupWrappedCallback(Mock<ISetup<IToMock, int>> mockWrapped, Delegate del, IReturnsThrows<IToMock, int> wrappedReturn)
        {
            mockWrapped.Setup(m => m.Callback(del)).Returns(wrappedReturn);
        }
    }

    internal class InvocableReturn_Returns_Tests : Invocable_Reflection_Tests_Base<ISetup<IToMock, int>, IReturnsResult<IToMock>, InvocableReturn<IToMock, int>>
    {
        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, IReturnsResult<IToMock> wrappedReturn, IReturnsResult<IToMock> verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
        [TestCaseSource("Source")]
        public void Returns_Wraps_Return_From_Wrapped(MethodInfo method, Func<Mock<ISetup<IToMock, int>>, IReturnsResult<IToMock>,Mock<ICallbackWrapper>, object[]> doSetupAndReturnArgs)
        {
            base.Reflection_Wraps_Return_From_Wrapped(method, doSetupAndReturnArgs);
        }
        public static IEnumerable<TestCaseData> Source()
        {
            return ReflectionHelper.InvocableReturnsSource<ISetup<IToMock, int>>();
        }
        
        protected override InvocableReturn<IToMock, int> CreateInvocable(ISetup<IToMock, int> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableReturn<IToMock, int>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }
    }
    internal class InvocableReturn_CallBase_Test : Invocable_CallBase_Test<ISetup<IToMock, string>, IReturnsResult<IToMock>, InvocableReturn<IToMock, string>>
    {
        protected override InvocableReturn<IToMock, string> CreateInvocable(ISetup<IToMock, string> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableReturn<IToMock, string>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }

        protected override void SetupCallbase(Mock<ISetup<IToMock, string>> mockWrapped, IReturnsResult<IToMock> wrappedReturn)
        {
            mockWrapped.Setup(m => m.CallBase()).Returns(mockedWrappedReturn);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, IReturnsResult<IToMock> wrappedReturn, IReturnsResult<IToMock> verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);
        }
    }
    internal class InvocableReturn_Registers_For_Callback_Test : Invocable_RegistersForCallback_Test<ISetup<IToMock, int>, InvocableReturn<IToMock, int>>
    {
        protected override InvocableReturn<IToMock, int> CreateInvocable(ISetup<IToMock, int> wrapped)
        {
            return new InvocableReturn<IToMock, int>(wrapped, null, null, 0);
        }

        protected override void SetWrappedForCallback(Mock<ISetup<IToMock, int>> mockWrapped, Action action)
        {
            mockWrapped.Setup(m => m.Callback(action));
        }
    }
    [TestFixture]//todo do I need TestFixture
    internal class InvocableReturn_Ctor_Args_Tests : Invocable_Ctor_Args_Tests<ISetup<IToMock, string>>
    {
        protected override InvocableBase<ISetup<IToMock, string>> CreateInvocable(ISetup<IToMock, string> wrapped, Mock<IToMock> mock, ISequence sequence, int consecutiveInvocations, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableReturn<IToMock, string>(wrapped, mock, sequence, consecutiveInvocations, verifiableWrapper, callbackWrapper);
        }

        protected override InvocableBase<ISetup<IToMock, string>> CreateInvocableSequenceInvocationIndices(ISetup<IToMock, string> wrapped, Mock<IToMock> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableReturn<IToMock, string>(wrapped, mock, sequence, sequenceInvocationIndices, verifiableWrapper, callbackWrapper);
        }
    }
}
