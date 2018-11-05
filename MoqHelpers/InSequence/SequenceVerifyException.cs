namespace MoqHelpers.InSequence
{
    public class SequenceVerifyException : SequenceException
    {
        internal SequenceVerifyException(string message) : base(message) { }
        internal SequenceVerifyException(string message,int loop,int call) : base(message)
        {
            Loop = loop;
            Call = call;
        }
        public bool OutOfSequence
        {
            get; private set;
        }
        public int Loop
        {
            get;private set;
        }
        public int Call
        {
            get; private set;
        }
        public bool InsufficientCalls
        {
            get;private set;
        }
        public bool TooManyCalls { get; private set; }
        public int ExpectedCount { get; private set; }
        public int ActualCount { get; private set; }

        internal static SequenceVerifyException InsufficientCallsException(int loop,int call)
        {
            var exception = new SequenceVerifyException("Insufficient calls",loop,call);
            exception.InsufficientCalls = true;
            return exception;
        }
        internal static SequenceVerifyException OutOfSequenceException(int loop,int call)
        {
            var exception = new SequenceVerifyException("Out of sequence",loop,call);
            exception.OutOfSequence = true;
            return exception;
        }

        internal static SequenceVerifyException TooManyCallsException(int expectedCount,int actualCount)
        {
            var exception = new SequenceVerifyException("Too many calls");
            exception.TooManyCalls = true;
            exception.ExpectedCount = expectedCount;
            exception.ActualCount = actualCount;
            return exception;
        }
    }
}
