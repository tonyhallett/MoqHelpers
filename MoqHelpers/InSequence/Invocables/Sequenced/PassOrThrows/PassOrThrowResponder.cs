using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    internal class PassOrThrowResponder<T> : InvocationResponder<ISetup<T>,Exception> where T:class
    {
        public PassOrThrowResponder(ISetup<T> invocation, IInvocationResponses<Exception> passOrThrows,int loops) : base(invocation, passOrThrows,loops) {

        }

        protected override void Respond(Exception response)
        {
            invocation.Throws(response);
        }

        protected override void RespondExhausted()
        {
            invocation.Throws(null);
        }
    }
}
