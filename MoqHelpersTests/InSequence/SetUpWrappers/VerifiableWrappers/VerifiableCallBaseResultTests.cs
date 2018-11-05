using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal class VerifiableCallbaseResult_Throws_Tests : Throws_Tests<ICallBaseResult, VerifiableCallBaseResult>
    {
    }

    [TestFixture]
    internal class VerifiableCallBaseResult_Verifies_Tests : VerifiableTests<ICallBaseResult, VerifiableCallBaseResult>
    {

    }
    [TestFixture]
    internal class VerifiableCallBaseResult_AtMost_Tests : AtMost_Tests<ICallBaseResult, VerifiableCallBaseResult>
    {

    }
}
