using Moq;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetUpWrappers;
using NUnit.Framework;
using System;
using System.Reflection;

namespace MoqHelpersTests.InSequence.Sequence.Responders
{
    [TestFixture]
    internal class Responder_Should_Pass_To_Base_Ctor_Arguments<TResponder,TInvocation, TResponse> where TInvocation:class where TResponder: InvocationResponder<TInvocation, TResponse>
    {
        private TInvocation invocation;
        private IInvocationResponses<TResponse> responses;
        private TResponder responder;
        private int loops = 4;

        [SetUp]
        public void Setup()
        {
            invocation = new Mock<TInvocation>().Object;
            responses = new Mock<IInvocationResponses<TResponse>>().Object;
            responder = (TResponder)Activator.CreateInstance(typeof(TResponder),BindingFlags.Public|BindingFlags.Instance,null,new object[] { invocation, responses, loops },null);
        }
        [Test]
        public void Should_Pass_Setup()
        {
            Assert.That(responder.Invocation, Is.EqualTo(invocation));
        }
        [Test]
        public void Should_Pass_Responses()
        {
            Assert.That(responder.Responses, Is.EqualTo(responses));
        }
        [Test]
        public void Should_Pass_Loops()
        {
            Assert.That(responder.Loops, Is.EqualTo(loops));
        }
    }
}
