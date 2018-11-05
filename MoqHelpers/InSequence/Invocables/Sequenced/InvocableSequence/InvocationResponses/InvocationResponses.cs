using System.Collections;
using System.Collections.Generic;

namespace MoqHelpers.InSequence
{
    public class InvocationResponses<TResponse> : IInvocationResponses<TResponse>, IEnumerable<TResponse>
    {
        private List<TResponse> responses = new List<TResponse>();

        TResponse IInvocationResponses<TResponse>.this[int invocationNumber] => responses[invocationNumber];

        int IInvocationResponses<TResponse>.ConfiguredResponses => responses.Count;

        public void Add(TResponse invocationResponse)
        {
            responses.Add(invocationResponse);
        }
        public IEnumerator<TResponse> GetEnumerator()
        {
            return responses.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)responses.GetEnumerator();
        }
    }
}