namespace MoqHelpers.InSequence.SetupWrappers
{
    internal abstract class Verifiable<T> 
    {
        private readonly T wrapped;
        
        private readonly IVerifiableWrapper verifiableWrapper;

        public T Wrapped => wrapped;
        public IVerifiableWrapper VerifiableWrapper => verifiableWrapper;
        public Verifiable(T wrapped,IVerifiableWrapper verifiableWrapper)
        {
            this.wrapped = wrapped;
            this.verifiableWrapper = verifiableWrapper;
        }
    }
    
}
