using System;

namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    public static class ExceptionOrReturnFactory
    {
        public static ExceptionOrReturn<TReturn> Return<TReturn>(TReturn result)
        {
            return new ExceptionOrReturn<TReturn> { Return = result };
        }
        public static ExceptionOrReturn<TReturn> Exception<TReturn>(Exception exc)
        {
            return new ExceptionOrReturn<TReturn> { Exception = exc };
        }
    }
}