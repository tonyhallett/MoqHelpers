using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    internal class ReturnsInvocationResponder<TMock, TReturn> : InvocationResponder<ISetup<TMock, TReturn>, TReturn> where TMock : class
    {
        public ReturnsInvocationResponder( ISetup<TMock, TReturn> invocation, IInvocationResponses<TReturn> returns, int loops) : base(invocation, returns,loops) { }

        protected override void Respond(TReturn response)
        {
            invocation.Returns(response);
        }
        
        protected override void RespondExhausted()
        {
            invocation.Returns(default(TReturn));
        }
    }
}
