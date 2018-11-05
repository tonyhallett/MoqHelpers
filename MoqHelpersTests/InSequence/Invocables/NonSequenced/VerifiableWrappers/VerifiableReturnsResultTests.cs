using Moq;
using Moq.Language;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using MoqHelpersTests.InSequence.Invocables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal class VerifiableReturnsResult_Verifies_Tests : VerifiableTests<IReturnsResult<IToMock>, VerifiableReturnsResult<IToMock>>
    {

    }
    [TestFixture]
    internal class VerifiableReturnsResult_AtMost_Tests : AtMost_Tests<IReturnsResult<IToMock>, VerifiableReturnsResult<IToMock>>
    {

    }

    [TestFixture(Description = "VerifiableReturnsResult_Raises_Reflection")]
    internal class VerifiableReturnsResult_Raises_Reflection : VerifiableReflectionBase<IReturnsResult<IToMock>, IVerifies, VerifiableReturnsResult<IToMock>>
    {
        protected override void SetupVerifiableWrapper(IVerifies wrappedReturn, IVerifies verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapVerifiesForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
        [TestCaseSource("Source")]
        public void Raises_Wraps_Return_From_Wrapped(MethodInfo method, Func<Mock<IReturnsResult<IToMock>>, IVerifies, object[]> doSetupAndReturnArgs)
        {
            base.Reflection_Wraps_Return_From_Wrapped(method, doSetupAndReturnArgs);
        }

        public static IEnumerable<TestCaseData> Source()
        {
            return ReflectionHelper.RaisesSource<IReturnsResult<IToMock>>();
        }
    }
    [TestFixture(Description = "VerifiableReturnsResult_Callback_Reflection")]
    internal class VerifiableReturnsResult_Callback_Reflection : VerifiableReflectionBase<IReturnsResult<IToMock>, ICallbackResult, VerifiableReturnsResult<IToMock>>
    {
        protected override void SetupVerifiableWrapper(ICallbackResult wrappedReturn, ICallbackResult verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapCallbackResultForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
        [TestCaseSource("Source")]
        public void Callback_Wraps_Return_From_Wrapped(MethodInfo method, Func<Mock<IReturnsResult<IToMock>>, ICallbackResult, object[]> doSetupAndReturnArgs)
        {
            base.Reflection_Wraps_Return_From_Wrapped(method, doSetupAndReturnArgs);
        }
        public static IEnumerable<TestCaseData> Source()
        {
            return ReflectionHelper.Source<IReturnsResult<IToMock>, ICallbackResult>(
                (types) =>
                {
                    return new object[] { ReflectionHelper.GetActionOrDelegate(types.First()) };
                },
                (m) =>
                    {
                        return m.Name == "Callback";
                    }
                );
        }
    }
}
