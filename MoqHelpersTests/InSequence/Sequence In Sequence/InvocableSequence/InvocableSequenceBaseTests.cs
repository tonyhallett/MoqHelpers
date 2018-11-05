using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetUpWrappers;
using NUnit.Framework;
using System;
using System.Reflection;

namespace MoqHelpersTests.InSequence.Sequence
{
    [TestFixture]
    public class InvocationSequenceBase_RegisterForCallback_Test
    {
        [Test]
        public void Should_Call_ApplyCallback_With_Wrapped_Action_That_Invokes_Original_And_The_Responder()
        {
            var mockSetupArgument = new Mock<ISetup<IToMock>>();
            var setupArgument = mockSetupArgument.Object;

            bool originalActionInvoked = false;
            var actionArgument = new Action(() => { originalActionInvoked = true; });
            
            
            var mockInvocationResponder = new Mock<IInvocationResponder<ISetup<IToMock>, string>>();

            var sequenceBaseDerivation = new InvocableSequenceBaseDerivation(new Mock<ISetup<IToMock>>().Object, null, null, mockInvocationResponder.Object);

            var registerForCallbackMethod=sequenceBaseDerivation.GetType().GetMethod("RegisterForCallback", BindingFlags.Instance | BindingFlags.NonPublic);
            registerForCallbackMethod.Invoke(sequenceBaseDerivation, new object[] { setupArgument, actionArgument });

            sequenceBaseDerivation.ApplyCallbackAction();

            Assert.That(sequenceBaseDerivation.ApplyCallbackWrapped, Is.EqualTo(setupArgument));
            Assert.That(originalActionInvoked);
            mockInvocationResponder.Verify(m => m.Respond());
        }
    }

    internal class InvocableSequenceBaseDerivation : InvocableSequenceBase<ISetup<IToMock>, string>
    {
        public InvocableSequenceBaseDerivation(ISetup<IToMock> wrapped, Mock mock, ISequence sequence, IInvocationResponder<ISetup<IToMock>, string> invocationResponder) : base(wrapped, mock, sequence, invocationResponder)
        {
        }

        public ISetup<IToMock> ApplyCallbackWrapped { get; private set; }
        public Action ApplyCallbackAction { get; private set; }
        protected override void ApplyCallback(ISetup<IToMock> wrapped, Action callback)
        {
            ApplyCallbackWrapped = wrapped;
            ApplyCallbackAction = callback;
        }
    }

    

}
