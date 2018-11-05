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
using System.Linq.Expressions;
using System.Reflection;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal abstract class Returns_Tests_Base<TWrapped, TVerifiable>: VerifiableReflectionBase<TWrapped, IReturnsResult<IToMock>, TVerifiable> where TVerifiable : Verifiable<TWrapped>  where TWrapped:class
    {
        protected override void SetupVerifiableWrapper(IReturnsResult<IToMock> wrappedReturn, IReturnsResult<IToMock> verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);
        }
        [TestCaseSource("Source")]
        public void Returns_Wraps_Return_From_Wrapped(MethodInfo method, Func<Mock<TWrapped>, IReturnsResult<IToMock>, object[]> doSetupAndReturnArgs)
        {
            base.Reflection_Wraps_Return_From_Wrapped(method, doSetupAndReturnArgs);
        }
        public static IEnumerable<TestCaseData> Source()
        {
            return ReflectionHelper.ReturnsSource<TWrapped>();
        }
    }
}
