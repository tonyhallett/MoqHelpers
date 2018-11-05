using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using MoqHelpers;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetUpWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoqHelpersTests.InSequence.Sequence.Responders
{
    [TestFixture]
    internal class InvocationResponder_Ctor_Tests {
        private string invocation;
        private InvocationResponder<string, string> responder;
        private Mock<IInvocationResponses<string>> mockResponses;
        private int numConfiguredResponses;
        private IInvocationResponses<string> responses;
        private int loops = 3;
        [SetUp]
        public void Setup()
        {
            mockResponses = new Mock<IInvocationResponses<string>>();
            numConfiguredResponses = 2;
            mockResponses.SetupGet(m => m.ConfiguredResponses).Returns(numConfiguredResponses);

            responses = mockResponses.Object;
            invocation = "";
            responder = new Mock<InvocationResponder<string, string>>(invocation, responses, loops).Object;
        }
        [Test]
        public void Ctor_Should_Set_Invocation()
        {
            Assert.That(responder.Invocation, Is.EqualTo(invocation));
        }
        [Test]//todo did this really need to be exposed ?
        public void Ctor_Should_Set_Responses()
        {
            Assert.That(responder.Responses, Is.EqualTo(responses));
        }
        [Test]
        public void Ctor_Should_Set_ConfiguredResponses_From_Responder()
        {
            mockResponses.VerifyGet(m => m.ConfiguredResponses);
            Assert.That(responder.ConfiguredResponses, Is.EqualTo(numConfiguredResponses));
        }
        [Test]
        public void Ctor_Should_Set_Loops()
        {
            Assert.That(responder.Loops, Is.EqualTo(loops));
        }
    }

    [TestFixture]
    internal class InvocationResponder_Response_Tests
    { 
        private (IInvocationResponses<string>,List<string>) GetInvocationResponses(int numResponses)
        {
            var mockInvocationResponses = new Mock<IInvocationResponses<string>>();
            var responses = Enumerable.Range(0, numResponses).Select(i => i.ToString()).ToList();
            mockInvocationResponses.Setup(m => m[It.IsAny<int>()]).Returns<int>(i => responses[i]);
            mockInvocationResponses.SetupGet(m => m.ConfiguredResponses).Returns(numResponses);

            return (mockInvocationResponses.Object, responses);
        }
        private (List<string>,IInvocationList) Respond(int numResponses,int loops,int numResponds)
        {
            var (invocationResponses,responses) = GetInvocationResponses(numResponses);
            var mockInvocationResponder = new Mock<InvocationResponder<string, string>>("", invocationResponses, loops);
            var invocationResponder = mockInvocationResponder.Object;

            for (var i = 0; i < numResponds; i++)
            {
                invocationResponder.Respond();
            }
            return (responses, mockInvocationResponder.Invocations);
        }

        private MethodInfo GetInvocationResponderMethod(string methodName)
        {
            var invocationResponderType = typeof(InvocationResponder<string, string>);
            var protectedBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            return invocationResponderType.GetMethod(methodName, protectedBindingFlags);
        }
        [TestCaseSource("RespondSource")]
        public void Should_Call_Respond_From_Responses_When_Not_Exhausted(int numResponds,int loops,int numResponses,List<int> expectedResponseIndices)
        {
            var (responses, invocations) = Respond(numResponses, loops, numResponds);

            var respondMethod = GetInvocationResponderMethod("Respond");

            Assert.That(invocations.Count, Is.EqualTo(expectedResponseIndices.Count));
            for(var i = 0; i < invocations.Count;i++)
            {
                var invocation = invocations[i];
                Assert.That(invocation.Method, Is.EqualTo(respondMethod));
                Assert.That(invocation.Arguments[0], Is.EqualTo(responses[expectedResponseIndices[i]]));
            }
        }

        [TestCaseSource("RespondExhaustedSource")]
        public void Should_Call_RespondExhausted_When_Exhausted(int numResponds,int loops,int numResponses,int expectedfirstExhaustIndex)
        {
            var (_, invocations) = Respond(numResponses, loops, numResponds);

            var respondExhaustedMethod = GetInvocationResponderMethod("RespondExhausted");

            var firstExhaustIndex = -1;
            for (var i = 0; i < invocations.Count; i++)
            {
                var invocation = invocations[i];
                if (firstExhaustIndex == -1)
                {
                    if (invocation.Method == respondExhaustedMethod)
                    {
                        firstExhaustIndex = i;
                    }
                }
                else
                {
                    Assert.That(invocation.Method, Is.EqualTo(respondExhaustedMethod));
                }
            }
            Assert.That(firstExhaustIndex, Is.EqualTo(expectedfirstExhaustIndex));
        }
        public static List<TestCaseData> RespondExhaustedSource()
        {
            return new List<TestCaseData>
            {
                new TestCaseData(2,1,0,0),
                new TestCaseData(11,2,5,10)
            };
        }
        public static List<TestCaseData> RespondSource()
        {
            return new List<TestCaseData>
            {
                //int numResponds,int loops,int numResponses,List<int> expectedResponseIndices
                new TestCaseData(10,2,5,new List<int>{ 0,1,2,3,4,0,1,2,3,4})
            };
        }
    }
}
