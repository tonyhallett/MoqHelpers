using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal class VerifiableThrowsResult_Verifies_Tests : VerifiableTests<IThrowsResult, VerifiableThrowsResult>
    {

    }
    [TestFixture]
    internal class VerifiableThrowsResult_AtMost_Tests : AtMost_Tests<IThrowsResult, VerifiableThrowsResult>
    {

    }
}
