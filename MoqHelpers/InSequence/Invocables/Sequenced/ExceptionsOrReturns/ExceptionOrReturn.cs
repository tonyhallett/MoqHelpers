using System;

namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    public class ExceptionOrReturn<TReturn>
    {
        public Exception Exception { get; set; }
        public TReturn Return { get; set; }
    }
}