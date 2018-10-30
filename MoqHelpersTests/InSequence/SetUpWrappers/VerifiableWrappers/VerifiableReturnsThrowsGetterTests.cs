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


}
