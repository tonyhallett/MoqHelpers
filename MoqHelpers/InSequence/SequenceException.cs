using System;

namespace MoqHelpers.InSequence
{
    public class SequenceException : Exception
    {
        internal SequenceException(string message) : base(message) { }
    }
}
