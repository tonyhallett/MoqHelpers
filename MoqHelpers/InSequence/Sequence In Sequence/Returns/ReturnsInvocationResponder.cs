using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    internal class ReturnsInvocationResponder<TMock, TReturn> : InvocationReturnResponder<TMock,TReturn,TReturn> where TMock:class{
        public ReturnsInvocationResponder(IInvocationResponses<TReturn> returns, ISetup<TMock, TReturn> invocation,int loops) : base(invocation, returns,loops) { }

        protected override void Respond(TReturn response)
        {
            invocation.Returns(response);
        }
        
        protected override void RespondExhausted()
        {
            invocation.Returns(GetDefault());
        }
    }
}
