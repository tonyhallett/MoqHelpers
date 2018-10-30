using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    internal class ExceptionsOrReturnsInvocationResponder<TMock, TReturn> : InvocationReturnResponder<TMock, TReturn,ExceptionOrReturn<TReturn>> where TMock : class
    {
        public ExceptionsOrReturnsInvocationResponder(IInvocationResponses<ExceptionOrReturn<TReturn>> exceptionsOrReturns, ISetup<TMock, TReturn> invocation, int loops) : base(invocation, exceptionsOrReturns, loops)
        {
        }
        private void ProtectAgainstThrow()
        {
            invocation.Throws(null);
        }
        protected override void RespondExhausted()
        {
            ProtectAgainstThrow();
            invocation.Returns(GetDefault<TReturn>());
        }

        protected override void Respond(ExceptionOrReturn<TReturn> response)
        {
            ProtectAgainstThrow();

            if (response.Exception != null)
            {
                invocation.Throws(response.Exception);
            }
            else
            {
                invocation.Returns(response.Return);
            }
        }
    }
}
