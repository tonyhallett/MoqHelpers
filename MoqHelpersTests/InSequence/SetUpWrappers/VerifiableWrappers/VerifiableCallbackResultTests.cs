using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    [TestFixture]
    internal class VerifiableCallbackResult_Throws_Tests : Throws_Tests<ICallbackResult, VerifiableCallbackResult>
    {
    }
}
