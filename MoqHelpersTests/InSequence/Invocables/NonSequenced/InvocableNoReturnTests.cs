using Moq;
using Moq.Language;
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
    [TestFixture]
    internal class InvocableNoReturn_AtMost_Tests : Invocable_Wraps_Test_Base<ISetup<IToMock>, IVerifies, InvocableNoReturn<IToMock>>
    {
        protected override InvocableNoReturn<IToMock> CreateInvocable(ISetup<IToMock> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableNoReturn<IToMock>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }
        [Test]
        public void AtMost_Should_wrap_The_Return_From_Wrapped()
        {
            var atMost = 5;
            #pragma warning disable 0618
            mockWrapped.Setup(m => m.AtMost(atMost)).Returns(mockedWrappedReturn);
            #pragma warning restore 0618
            mockVerifiableWrapper.Setup(m => m.WrapVerifiesForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn= invocable.AtMost(atMost);
        }
        [Test]
        public void AtMostOnce_Should_wrap_The_Return_From_Wrapped()
        {
            #pragma warning disable 0618
            mockWrapped.Setup(m => m.AtMostOnce()).Returns(mockedWrappedReturn);
            #pragma warning restore 0618
            mockVerifiableWrapper.Setup(m => m.WrapVerifiesForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = invocable.AtMostOnce();
        }
    }
    [TestFixture]
    internal class InvocableNoReturn_CallBase_Test : Invocable_CallBase_Test<ISetup<IToMock>, ICallBaseResult, InvocableNoReturn<IToMock>>
    {
        protected override InvocableNoReturn<IToMock> CreateInvocable(ISetup<IToMock> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableNoReturn<IToMock>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }

        protected override void SetupCallbase(Mock<ISetup<IToMock>> mockWrapped, ICallBaseResult wrappedReturn)
        {
            mockWrapped.Setup(m => m.CallBase()).Returns(wrappedReturn);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, ICallBaseResult wrappedReturn, ICallBaseResult verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapCallBaseResultForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
    }
    [TestFixture]
    internal class InvocableNoReturn_Registers_For_Callback_Test : Invocable_RegistersForCallback_Test<ISetup<IToMock>, InvocableNoReturn<IToMock>>
    {
        protected override InvocableNoReturn<IToMock> CreateInvocable(ISetup<IToMock> wrapped)
        {
            return new InvocableNoReturn<IToMock>(wrapped, null, null, 0);
        }

        protected override void SetWrappedForCallback(Mock<ISetup<IToMock>> mockWrapped, Action action)
        {
            mockWrapped.Setup(m => m.Callback(action));
        }
    }
    [TestFixture]
    internal class InvocableNoReturn_Callback_Tests : Invocable_Reflection_Tests_Base<ISetup<IToMock>, ICallbackResult, InvocableNoReturn<IToMock>>
    {
        protected override InvocableNoReturn<IToMock> CreateInvocable(ISetup<IToMock> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableNoReturn<IToMock>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, ICallbackResult wrappedReturn, ICallbackResult verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapCallbackResultForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
        [TestCaseSource("Source")]
        public void CallbackWrapper_Wraps_Before_Calling_Wrapped_And_Wrapping_The_Return_For_Verification(MethodInfo method, Func<Mock<ISetup<IToMock>>, ICallbackResult,Mock<ICallbackWrapper>, object[]> doSetupAndReturnArgs)
        {
            base.Reflection_Wraps_Return_From_Wrapped(method, doSetupAndReturnArgs);
        }
        public static IEnumerable<TestCaseData> Source()
        {
            return ReflectionHelper.CallbackSource<ISetup<IToMock>, ICallbackResult>();
        }
    }
    internal abstract class Invocable_Delegate_Callback_Test<TWrapped, TWrappedReturn, TInvocable> : Invocable_Wraps_Test_Base<TWrapped, TWrappedReturn, TInvocable> where TWrappedReturn : class where TWrapped : class, IVerifies, IThrows where TInvocable : InvocableBase<TWrapped>
    {
        protected abstract void SetupWrappedCallback(Mock<TWrapped> mockWrapped, Delegate del, TWrappedReturn wrappedReturn);
        protected abstract void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, TWrappedReturn wrappedReturn, TWrappedReturn verifiableWrapperReturn);

        [Test]
        public void Should_Wrap_The_Callback_For_Wrapped_And_Wrap_The_Return()
        {
            Delegate del = new Action(() => { });
            Delegate wrappedDelegate = new Action(() => { });
           
            mockCallbackWrapper.Setup(m => m.WrapCallback(del)).Returns(wrappedDelegate);
            SetupWrappedCallback(mockWrapped, wrappedDelegate, mockedWrappedReturn);
            SetupVerifiableWrapper(mockVerifiableWrapper, mockedWrappedReturn, mockedVerifiableWrapperReturn);

            wrappedReturn = ((dynamic)invocable).Callback(del);

        }
    }
    //having issue with accomplishing with reflection
    internal class InvocableNoReturn_Delegate_Callback_Test : Invocable_Delegate_Callback_Test<ISetup<IToMock>, ICallbackResult, InvocableNoReturn<IToMock>>
    {
        protected override InvocableNoReturn<IToMock> CreateInvocable(ISetup<IToMock> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableNoReturn<IToMock>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, ICallbackResult wrappedReturn, ICallbackResult verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapCallbackResultForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }

        protected override void SetupWrappedCallback(Mock<ISetup<IToMock>> mockWrapped, Delegate del, ICallbackResult wrappedReturn)
        {
            mockWrapped.Setup(m => m.Callback(del)).Returns(wrappedReturn);
        }
    }

    [TestFixture]
    internal class InvocableNoReturn_Raises_Tests : Invocable_Reflection_Tests_Base<ISetup<IToMock>, IVerifies, InvocableNoReturn<IToMock>>
    {
        protected override InvocableNoReturn<IToMock> CreateInvocable(ISetup<IToMock> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableNoReturn<IToMock>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, IVerifies wrappedReturn, IVerifies verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapVerifiesForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
        [TestCaseSource("Source")]
        public void Raises_Wraps_Wrapped_Return(MethodInfo method, Func<Mock<ISetup<IToMock>>, IVerifies, Mock<ICallbackWrapper>,object[]> doSetupAndReturnArgs)
        {
            base.Reflection_Wraps_Return_From_Wrapped(method, doSetupAndReturnArgs);
        }
        public static IEnumerable<TestCaseData> Source()
        {
            var source = ReflectionHelper.InvocableRaisesSource<ISetup<IToMock>>();
            return source;
        }
    }
    [TestFixture]
    internal class InvocableNoReturn_Ctor_Args_Tests : Invocable_Ctor_Args_Tests<ISetup<IToMock>>
    {
        protected override InvocableBase<ISetup<IToMock>> CreateInvocable(ISetup<IToMock> wrapped, Mock<IToMock> mock, ISequence sequence, int consecutiveInvocations, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableNoReturn<IToMock>(wrapped, mock, sequence, consecutiveInvocations, verifiableWrapper, callbackWrapper);
        }

        protected override InvocableBase<ISetup<IToMock>> CreateInvocableSequenceInvocationIndices(ISetup<IToMock> wrapped, Mock<IToMock> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableNoReturn<IToMock>(wrapped, mock, sequence, sequenceInvocationIndices, verifiableWrapper, callbackWrapper);
        }
    }
}
