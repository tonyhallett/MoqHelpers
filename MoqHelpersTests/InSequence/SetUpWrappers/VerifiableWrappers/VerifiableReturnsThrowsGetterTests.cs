using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal class VerifiableReturnsThrowsGetter_Throws_Tests : Throws_Tests<IReturnsThrowsGetter<IToMock, string>, VerifiableReturnsThrowsGetter<IToMock, string>>
    {
    }

    [TestFixture]
    internal class VerifiableReturnsThrowsGetter_Returns_Tests : Returns_Tests_Base<IReturnsThrowsGetter<IToMock, string>, VerifiableReturnsThrowsGetter<IToMock, string>>
    {

    }
    [TestFixture]
    internal class VerifiableReturnsThrowsGetter_CallBase_Test : CallBase_Test_Base<IReturnsThrowsGetter<IToMock, int>, IReturnsResult<IToMock>, VerifiableReturnsThrowsGetter<IToMock, int>>
    {
        protected override void SetupCallbase(Mock<IReturnsThrowsGetter<IToMock, int>> mockWrapped, IReturnsResult<IToMock> returns)
        {
            mockWrapped.Setup(m => m.CallBase()).Returns(returns);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, IReturnsResult<IToMock> wrappedReturn, IReturnsResult<IToMock> verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
    }



}
