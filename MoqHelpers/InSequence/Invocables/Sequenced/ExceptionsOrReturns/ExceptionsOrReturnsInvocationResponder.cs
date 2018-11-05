using Moq.Language.Flow;

namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    internal class ExceptionsOrReturnsInvocationResponder<TMock, TReturn> : InvocationResponder<ISetup<TMock, TReturn>, ExceptionOrReturn<TReturn>>   where TMock : class
    {
        public ExceptionsOrReturnsInvocationResponder(ISetup<TMock, TReturn> invocation, IInvocationResponses<ExceptionOrReturn<TReturn>> exceptionsOrReturns, int loops) : base(invocation, exceptionsOrReturns, loops)
        {
        }
        private void ProtectAgainstThrow()
        {
            invocation.Throws(null);
        }
        protected override void RespondExhausted()
        {
            ProtectAgainstThrow();
            invocation.Returns(default(TReturn));
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
