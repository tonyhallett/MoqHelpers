using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetUpWrappers;
using NUnit.Framework;
using System;
using System.Reflection;

namespace MoqHelpersTests.InSequence.Sequence.Responders
{
    internal class ReturnsInvocationResponder_Should_Pass_To_Base_Ctor_Arguments : Responder_Should_Pass_To_Base_Ctor_Arguments<ReturnsInvocationResponder<IToMock, string>, ISetup<IToMock, string>, string>
    {

    }
    [TestFixture]
    public class ReturnsInvocationResponderTests
    {
        [TestCase("Response")]
        [TestCase(null)]
        public void Respond_Should_Pass_Response_To_Setup(string response)
        {
            var mockSetup = new Mock<ISetup<IToMock, string>>();
            mockSetup.Setup(m => m.Returns(response));
            IInvocationResponses<string> returns = new Mock<IInvocationResponses<string>>().Object;
            var responder = new ReturnsInvocationResponder<IToMock, string>( mockSetup.Object, returns, 0);

            var respondMethod = typeof(ReturnsInvocationResponder<IToMock, string>).GetMethod("Respond", BindingFlags.Instance | BindingFlags.NonPublic);
            respondMethod.Invoke(responder, new object[] { response });

            mockSetup.VerifyAll();
        }
    }

    [TestFixture(0)]
    [TestFixture(null,TypeArgs =new Type[] { typeof(string)})]
    public class ReturnsInvocationResponder_RespondExhausted_Test<TReturn>
    {
        private TReturn expectedDefault;

        public ReturnsInvocationResponder_RespondExhausted_Test(TReturn expectedDefault)
        {
            this.expectedDefault = expectedDefault;
        }
        private IInvocationResponses<TReturn> CreateResponses()
        {
            return new Mock<IInvocationResponses<TReturn>>().Object;
        }
        [Test]
        public void RespondExhausted_Should_Return_Default_Return()
        {
            var mockSetup = new Mock<ISetup<IToMock, TReturn>>();

            var responses = CreateResponses();
            var responder = new ReturnsInvocationResponder<IToMock, TReturn> ( mockSetup.Object, responses, 0);
            var respondMethod = typeof(ReturnsInvocationResponder<IToMock, TReturn>).GetMethod("RespondExhausted", BindingFlags.Instance | BindingFlags.NonPublic);
            respondMethod.Invoke(responder, new object[] { });

            mockSetup.Verify(m => m.Returns(expectedDefault));

        }
    }
}
