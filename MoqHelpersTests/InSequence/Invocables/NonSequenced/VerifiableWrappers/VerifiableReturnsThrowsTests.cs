using Moq;
using Moq.Language;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MoqHelpersTests.InSequence.Invocables.NonSequenced.VerifiableWrappers
{

    [TestFixture]
    internal class VerifiableReturnsThrows_Throws_Tests : Throws_Tests<IReturnsThrows<IToMock, string>, VerifiableReturnsThrows<IToMock, string>>
    {
    }

    [TestFixture(Description = "VerifiableReturnsThrowsReturns")]
    internal class VerifiableReturnsThrows_Returns_Tests : Returns_Tests_Base<IReturnsThrows<IToMock, string>, VerifiableReturnsThrows<IToMock, string>>
    {

    }

    [TestFixture]
    internal class VerifiableReturnsThrows_CallBase_Test : CallBase_Test_Base<IReturnsThrows<IToMock, string>, IReturnsResult<IToMock>, VerifiableReturnsThrows<IToMock, string>>
    {
        protected override void SetupCallbase(Mock<IReturnsThrows<IToMock, string>> mockWrapped, IReturnsResult<IToMock> returns)
        {
            mockWrapped.Setup(m => m.CallBase()).Returns(returns);
        }

        protected override void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, IReturnsResult<IToMock> wrappedReturn, IReturnsResult<IToMock> verifiableWrapperReturn)
        {
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(wrappedReturn)).Returns(verifiableWrapperReturn);
        }
    }

}
