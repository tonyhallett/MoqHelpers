using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetUpWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MoqHelpersTests.InSequence.Sequence.Responders
{
    internal class PassOrThrowsResponder_Should_Pass_To_Base_Ctor_Arguments: Responder_Should_Pass_To_Base_Ctor_Arguments<PassOrThrowResponder<IToMock>, ISetup<IToMock>, Exception>
    {

    }
    
    [TestFixture]
    public class PassOrThrowsResponder_Respond_Tests
    {
        [TestCaseSource("ResponsesSource")]
        public void Respond_Should_Throw_Response_On_Invocation(Exception exception)
        {
            var mockSetup = new Mock<ISetup<IToMock>>();
            var mockPassOrThrows = new Mock<IInvocationResponses<Exception>>();
            var passOrThrowResponder = new PassOrThrowResponder<IToMock>(mockSetup.Object, mockPassOrThrows.Object, 0);
            var respondMethod = typeof(PassOrThrowResponder<IToMock>).GetMethod("Respond", BindingFlags.Instance | BindingFlags.NonPublic);
            respondMethod.Invoke(passOrThrowResponder, new object[] { exception });

            mockSetup.Verify(m => m.Throws(exception));
        }
        public static List<Exception> ResponsesSource()
        {
            return new List<Exception> { null, new ArgumentException() };
        }
        [Test]
        public void RespondExhausted_Should_Throw_Null_On_Invocation()
        {
            var mockSetup = new Mock<ISetup<IToMock>>();
            var mockPassOrThrows = new Mock<IInvocationResponses<Exception>>();
            var passOrThrowResponder = new PassOrThrowResponder<IToMock>(mockSetup.Object, mockPassOrThrows.Object, 0);
            var respondMethod = typeof(PassOrThrowResponder<IToMock>).GetMethod("RespondExhausted", BindingFlags.Instance | BindingFlags.NonPublic);
            respondMethod.Invoke(passOrThrowResponder, new object[] { });

            mockSetup.Verify(m => m.Throws(null));
        }
    }
}
