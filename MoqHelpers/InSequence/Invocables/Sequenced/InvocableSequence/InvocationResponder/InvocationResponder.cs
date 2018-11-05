using Moq.Language.Flow;

namespace MoqHelpers.InSequence.SetUpWrappers
{
    internal abstract class InvocationResponder<TInvocation,TResponse>: IInvocationResponder<TInvocation,TResponse>
    {
        protected readonly TInvocation invocation;
        private readonly int configuredResponses;
        private readonly int loops;
        private readonly IInvocationResponses<TResponse> responses;
        private int currentLoop = 1;
        private int responseIndex = 0;
        private bool exhausted = false;

        public InvocationResponder(TInvocation invocation, IInvocationResponses<TResponse> responses,int loops)
        {
            this.invocation = invocation;
            this.configuredResponses = responses.ConfiguredResponses;
            this.exhausted = this.configuredResponses == 0;
            this.loops = loops;
            
            this.responses = responses;
        }
        

        public int ConfiguredResponses => configuredResponses;

        public TInvocation Invocation => invocation;

        public IInvocationResponses<TResponse> Responses => responses;

        public int Loops => loops;

        public void Respond()
        {
            if (exhausted)
            {
                RespondExhausted();
            }
            else
            {
                Respond(responses[responseIndex]);

                responseIndex++;
                if (responseIndex == ConfiguredResponses)
                {
                    responseIndex = 0;
                    currentLoop++;
                    if (currentLoop > loops)
                    {
                        exhausted = true;
                    }
                }
            }
        }
        protected abstract void Respond(TResponse response);
        protected abstract void RespondExhausted();
    
    }
}
