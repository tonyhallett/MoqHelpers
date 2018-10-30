using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    
    internal class PassOrThrowResponder<T> : InvocationResponder<ISetup<T>,Exception> where T:class
    {
        public PassOrThrowResponder(ISetup<T> invocation, IInvocationResponses<Exception> passOrThrows,int loops) : base(invocation, passOrThrows,loops) {

        }

        protected override void Respond(Exception response)
        {
            if (response == null)
            {
                invocation.Throws(null);
            }
            else
            {
                invocation.Throws(response);
            }
        }

        protected override void RespondExhausted()
        {
            invocation.Throws(null);
        }
    }
}
