using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;

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
}
