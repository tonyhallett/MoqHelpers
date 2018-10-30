using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqHelpers.InSequence
{
    public class SequenceInvocationIndices : IEnumerable<int>
    {
        private List<int> callIndices = new List<int>();

        public SequenceInvocationIndices() { }
        public static SequenceInvocationIndices Singular(int callIndex)
        {
            return new SequenceInvocationIndices { callIndex };
        }
        internal SequenceInvocationIndices(IEnumerable<int> indices)
        {
            callIndices = indices.ToList();
        }
        internal int Count => callIndices.Count;
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return callIndices.GetEnumerator();
        }
        public void Add(int callIndex)
        {
            if (callIndex < 0)
            {
                throw SequenceSetupException.NegativeCallIndexException(callIndex);
            }
            if (callIndices.IndexOf(callIndex) != -1)
            {
                throw SequenceSetupException.RepeatedCallIndexInSetupException(callIndex);
            }
            callIndices.Add(callIndex);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return ((IEnumerable<int>)callIndices).GetEnumerator();
        }
    }
}
