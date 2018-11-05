using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetUpWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MoqHelpersTests.InSequence.Sequence.Responders
{
    internal class ExceptionsOrReturnsInvocationResponder_Should_Pass_To_Base_Ctor_Arguments : Responder_Should_Pass_To_Base_Ctor_Arguments<ExceptionsOrReturnsInvocationResponder<IToMock, string>, ISetup<IToMock, string>, ExceptionOrReturn<string>>
    {

    }
    [TestFixture]
    public class ExceptionsOrReturnsInvocationResponder_Respond_Tests
    {
        
        [TestCaseSource("ResponsesSource")]
        public void Respond_Should_Protect_Against_Throw_And_Throws_Or_Returns(ExceptionOrReturn<string> response)
        {
            var mockSetup = new Mock<ISetup<IToMock, string>>();
            var callOrder = new List<int>();
            mockSetup.Setup(m => m.Throws(null)).Callback(() =>
            {
                callOrder.Add(1);
            });
            if (response.Return != null)
            {
                mockSetup.Setup(m => m.Returns(response.Return)).Callback(() =>
                {
                    callOrder.Add(2);
                });
            }
            else
            {
                mockSetup.Setup(m => m.Throws(response.Exception)).Callback(() =>
                {
                    callOrder.Add(2);
                });
            }

            var responses = new Mock<IInvocationResponses<ExceptionOrReturn<string>>>().Object;
            var passOrThrowResponder = new ExceptionsOrReturnsInvocationResponder<IToMock,string>(mockSetup.Object,responses, 0);
            var respondMethod = typeof(ExceptionsOrReturnsInvocationResponder<IToMock, string>).GetMethod("Respond", BindingFlags.Instance | BindingFlags.NonPublic);
            respondMethod.Invoke(passOrThrowResponder, new object[] { response });

            Assert.That(callOrder, Is.EquivalentTo(new int[] { 1, 2 }));
            
        }
        public static List<ExceptionOrReturn<string>> ResponsesSource()
        {
            return new List<ExceptionOrReturn<string>> {
                ExceptionOrReturnFactory.Exception<string>(new ArgumentException()),
                ExceptionOrReturnFactory.Return("Some return")
            };
        }
        
    }
    [TestFixture(0)]
    [TestFixture(null,TypeArgs =new Type[] { typeof(string)})]
    public class ExceptionsOrReturnsInvocationResponder_RespondExhausted_Test<TReturn>
    {
        private TReturn expectedDefault;

        public ExceptionsOrReturnsInvocationResponder_RespondExhausted_Test(TReturn expectedDefault)
        {
            this.expectedDefault = expectedDefault;
        }
        private IInvocationResponses<ExceptionOrReturn<TReturn>> CreateResponses()
        {
            return new Mock<IInvocationResponses<ExceptionOrReturn<TReturn>>>().Object;
        }
        [Test]
        public void RespondExhausted_Should_Protect_Against_Throw_And_Return_Default_Return()
        {
            var mockSetup = new Mock<ISetup<IToMock, TReturn>>();
            var callOrder = new List<int>();
            mockSetup.Setup(m => m.Throws(null)).Callback(() =>
            {
                callOrder.Add(1);
            });
            mockSetup.Setup(m => m.Returns(expectedDefault)).Callback(() =>
            {
                callOrder.Add(2);
            });


            var responses = CreateResponses();
            var passOrThrowResponder = new ExceptionsOrReturnsInvocationResponder<IToMock, TReturn>( mockSetup.Object, responses, 0);
            var respondMethod = typeof(ExceptionsOrReturnsInvocationResponder<IToMock, TReturn>).GetMethod("RespondExhausted", BindingFlags.Instance | BindingFlags.NonPublic);
            respondMethod.Invoke(passOrThrowResponder, new object[] { });

            Assert.That(callOrder, Is.EquivalentTo(new int[] { 1, 2 }));

        }
    }
}
