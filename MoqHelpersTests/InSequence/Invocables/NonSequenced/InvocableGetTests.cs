using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MoqHelpersTests.InSequence.Invocables.NonSequenced
{
    [TestFixture]
    internal class InvocableGet_CallBase_Test : Invocable_CallBase_Test<ISetupGetter<IToMock, int>, IReturnsResult<IToMock>, InvocableGet<IToMock, int>>
    {
        protected override InvocableGet<IToMock, int> CreateInvocable(ISetupGetter<IToMock, int> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableGet<IToMock, int>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }

        protected override void SetupCallbase(Mock<ISetupGetter<IToMock, int>> mockWrapped, IReturnsResult<IToMock> wrappedReturn)
        {
            mockWrapped.Setup(m => m.CallBase()).Returns(wrappedReturn);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, IReturnsResult<IToMock> wrappedReturn, IReturnsResult<IToMock> verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
    }
    [TestFixture]
    internal class InvocableGet_Registers_For_Callback : Invocable_RegistersForCallback_Test<ISetupGetter<IToMock, int>, InvocableGet<IToMock, int>>
    {
        protected override InvocableGet<IToMock, int> CreateInvocable(ISetupGetter<IToMock, int> wrapped)
        {
            return new InvocableGet<IToMock, int>(wrapped, null, null, 0, null, null);
        }

        protected override void SetWrappedForCallback(Mock<ISetupGetter<IToMock, int>> mockWrapped,Action action)
        {
            mockWrapped.Setup(m => m.Callback(action));
        }
    }

    [TestFixture]
    internal class InvocableGet_Returns_Tests : Invocable_Wraps_Test_Base<ISetupGetter<IToMock, int>, IReturnsResult<IToMock>, InvocableGet<IToMock, int>>
    {
        protected override InvocableGet<IToMock, int> CreateInvocable(ISetupGetter<IToMock, int> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableGet<IToMock, int>(wrapped, null, sequence, 0, verifiableWrapper, null);
        }
        [Test]
        public void Returns_Should_Wrap_The_Return_From_Wrapped()
        {
            int returns = 7;
            mockWrapped.Setup(m => m.Returns(returns)).Returns(wrappedReturn);
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(wrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = invocable.Returns(returns);
        }
        [Test]
        public void Returns_Func_Should_Wrap_The_Return_From_Wrapped()
        {
            Func<int> returns = () => 7;
            mockWrapped.Setup(m => m.Returns(returns)).Returns(wrappedReturn);
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(wrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = invocable.Returns(returns);
        }
    }
    [TestFixture]
    internal class InvocableGet_Callback_Test : Invocable_Wraps_Test_Base<ISetupGetter<IToMock, int>, IReturnsThrowsGetter<IToMock, int>, InvocableGet<IToMock, int>>
    {
        protected override InvocableGet<IToMock, int> CreateInvocable(ISetupGetter<IToMock, int> wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableGet<IToMock, int>(wrapped, null, sequence, 0, verifiableWrapper, callbackWrapper);
        }
        [Test]
        public void Callback_Should_Wrap_The_Callback_Passing_To_Wrapped_And_Wrap_That()
        {
            Action action = () => { };
            Action wrappedAction = () => { };
            mockCallbackWrapper.Setup(m => m.WrapCallback(action)).Returns(wrappedAction);

            mockWrapped.Setup(m => m.Callback(wrappedAction)).Returns(wrappedReturn);
            mockVerifiableWrapper.Setup(m => m.WrapReturnsThrowsGetterForVerification(wrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = invocable.Callback(action);
        }
    }
    [TestFixture]
    internal class InvocableGet_Ctor_Args_Tests : Invocable_Ctor_Args_Tests<ISetupGetter<IToMock, int>>
    {
        protected override InvocableBase<ISetupGetter<IToMock, int>> CreateInvocable(ISetupGetter<IToMock, int> wrapped, Mock<IToMock> mock, ISequence sequence, int consecutiveInvocations, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableGet<IToMock, int>(wrapped, mock,sequence, consecutiveInvocations,verifiableWrapper,callbackWrapper);
        }

        protected override InvocableBase<ISetupGetter<IToMock, int>> CreateInvocableSequenceInvocationIndices(ISetupGetter<IToMock, int> wrapped, Mock<IToMock> mock, ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper)
        {
            return new InvocableGet<IToMock, int>(wrapped, mock, sequence, sequenceInvocationIndices,verifiableWrapper,callbackWrapper);
        }
    }
}
