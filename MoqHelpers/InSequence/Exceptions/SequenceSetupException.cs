namespace MoqHelpers.InSequence
{
    public class SequenceSetupException : SequenceException
    {
        internal SequenceSetupException(int incorrectCallIndex,string message) : base(message)
        {
            IncorrectCallIndex = incorrectCallIndex;
        }
        internal SequenceSetupException(string message) : base(message) { }

        public bool RepeatedCallIndexAcrossSetups { get; private set; }
        public bool NegativeCallIndex { get; private set; }
        public bool RepeatedCallIndexInSetup { get; private set; }
        public int IncorrectCallIndex { get; private set; }
        public bool MixedTimesAndCallIndices { get; private set; }

        internal static SequenceSetupException RepeatedCallIndexAcrossSetupsException(int callIndex)
        {
            var exception=new  SequenceSetupException(callIndex,"Repeated call index across setups");
            exception.RepeatedCallIndexAcrossSetups = true;
            return exception;
        }
        internal static SequenceSetupException RepeatedCallIndexInSetupException(int callIndex)
        {
            var exception = new SequenceSetupException(callIndex, "Repeated call index in setup");
            exception.RepeatedCallIndexInSetup = true;
            return exception;
        }
        internal static SequenceSetupException NegativeCallIndexException(int callIndex)
        {
            var exception = new SequenceSetupException(callIndex, "Negative call index");
            exception.NegativeCallIndex = true;
            return exception;
        }
        internal static SequenceSetupException MixedTimesAndCallIndicesException()
        {
            var exception = new SequenceSetupException("Mixed times and CallIndices");
            exception.MixedTimesAndCallIndices = true;
            return exception;
        }
    }
}
