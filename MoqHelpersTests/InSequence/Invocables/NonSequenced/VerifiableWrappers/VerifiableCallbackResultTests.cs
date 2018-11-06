using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    [TestFixture]
    internal class VerifiableCallbackResult_Throws_Tests : Throws_Tests<ICallbackResult, VerifiableCallbackResult>
    {
    }

    [TestFixture]
    internal class VerifiableCallbackResult_Verifies_Tests : VerifiableTests<ICallbackResult, VerifiableCallbackResult>
    {

    }
    [TestFixture]
    internal class VerifiableCallBackResult_AtMost_Tests : AtMost_Tests<ICallbackResult, VerifiableCallbackResult>
    {

    }
    [TestFixture]
    internal class VerifiableCallbackResult_CallBase_Test : CallBase_Test_Base<ICallbackResult, ICallBaseResult, VerifiableCallbackResult>
    {
        protected override void SetupCallbase(Mock<ICallbackResult> mockWrapped, ICallBaseResult returns)
        {
            mockWrapped.Setup(m => m.CallBase()).Returns(returns);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, ICallBaseResult wrappedReturn, ICallBaseResult verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapCallBaseResultForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
    }

}
