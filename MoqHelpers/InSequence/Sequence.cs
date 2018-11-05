using Moq;
using MoqHelpers.Additional;
using MoqHelpers.InSequence.Invocables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoqHelpers.InSequence
{
    public class Sequence : ISequence
    {
        private static ISequenceFactory factory = new SequenceFactory();
        internal static ISequenceFactory Factory {
            get
            {
                return factory;
            }
            set
            {
                factory = value;
            }
        }

        internal static void SingleSequenceVerify(Mock mock)
        {
            mock.GetAdditional<Sequence>().Verify();
        }
        internal static void SingleSequenceVerify(Mock mock,SequenceVerifyDelegate verifier)
        {
            mock.GetAdditional<Sequence>().Verify(verifier);
        }
        

        private int loops;
        private int setupTimesCount = 0;
        private bool isStrict = true;
        
        private SortedDictionary<int, IInvocable> expectedInvocations = new SortedDictionary<int, IInvocable>();
        private List<IInvocable> Invocations = new List<IInvocable>();
        private int verifyCurrentInvocationLoop = 0;
        private int verifyCurrentInvocationExpectedInvocationIndex = 0;

        private bool hasSetSequenceType = false;
        private bool isInvocationIndices = false;

        private bool verifyUponInvocationSet = false;
        private VerifyUponInvocation verifyUponInvocation;
        public VerifyUponInvocation VerifyUponInvocation
        {
            private get
            {
                return verifyUponInvocationSet ? verifyUponInvocation : Sequences.VerifyUponInvocation;
            }
            set
            {
                verifyUponInvocationSet = true;
                verifyUponInvocation = value;
            }
        }

        public Sequence(int loops)
        {
            this.loops = loops;
        }

        private IInvocable GetInvocation(int loop, int expectedInvocation)
        {
            var expectedNumberOfInvocations = expectedInvocations.Count;
            var invocationNumber = loop * expectedNumberOfInvocations + expectedInvocation;
            try
            {
                return Invocations[invocationNumber];
            }
            catch (ArgumentException)
            {
                throw SequenceVerifyException.InsufficientCallsException(loop + 1, expectedInvocation);
            }
        }
        private void VerifyInvocation(int loop, int expectedInvocationIndex)
        {
            CheckTooManyInvocations();
            if (GetInvocation(loop, expectedInvocationIndex) != expectedInvocations[expectedInvocationIndex])
            {
                throw SequenceVerifyException.OutOfSequenceException(loop + 1, expectedInvocationIndex);
            }
        }
        private int ExpectedInvocationCount
        {
            get { return loops * expectedInvocations.Count; }
        }
        private bool ReceivedExpectedNumberOfInvocations
        {
            get { return ExpectedInvocationCount == Invocations.Count; }
        }

        int ISequence.Loops => loops;

        private void CheckTooManyInvocations()
        {
            if (ExpectedInvocationCount < Invocations.Count)
            {
                throw SequenceVerifyException.TooManyCallsException(ExpectedInvocationCount, Invocations.Count);
            }
        }
        private void VerifyAllInvocationIndices()
        {
            CheckTooManyInvocations();
            var expectedNumberOfInvocations = expectedInvocations.Count;
            for (var i = 0; i < loops; i++)
            {
                for (var j = 0; j < expectedNumberOfInvocations; j++)
                {
                    VerifyInvocation(i, j);
                }
            }
        }
        private bool VerifiesUponInvocation()
        {
            return VerifyUponInvocation == VerifyUponInvocation.Yes || VerifyUponInvocation == VerifyUponInvocation.Strict && isStrict;
        }
        private void VerifyCurrentInvocation()
        {
            VerifyInvocation(verifyCurrentInvocationLoop, verifyCurrentInvocationExpectedInvocationIndex);
            if (verifyCurrentInvocationExpectedInvocationIndex++ == expectedInvocations.Count - 1)
            {
                verifyCurrentInvocationLoop++;
                verifyCurrentInvocationExpectedInvocationIndex = 0;
            }
        }

        private void AddExpectedInvocations(IInvocable setUp)
        {
            foreach (var sequenceInvocationIndex in setUp.SequenceInvocationIndices)
            {
                try
                {
                    expectedInvocations.Add(sequenceInvocationIndex, setUp);
                }
                catch (ArgumentException)
                {
                    throw SequenceSetupException.RepeatedCallIndexAcrossSetupsException(sequenceInvocationIndex);
                }
            }
        }
        private void SetBehavior(Mock mock)
        {
            if (isStrict)
            {
                isStrict = mock.Behavior == MockBehavior.Strict;
            }
        }
        private void SetIsInvocationIndices(bool isInvocationIndices)
        {
            if (hasSetSequenceType)
            {
                if (this.isInvocationIndices != isInvocationIndices)
                {
                    throw SequenceSetupException.MixedTimesAndCallIndicesException();
                }
            }
            else
            {
                this.isInvocationIndices = isInvocationIndices;
                hasSetSequenceType = true;
            }
        }

        public void Verify()
        {
            if (VerifiesUponInvocation())
            {
                if (!ReceivedExpectedNumberOfInvocations)
                {
                    CheckTooManyInvocations();
                    throw SequenceVerifyException.InsufficientCallsException(verifyCurrentInvocationLoop+1, verifyCurrentInvocationExpectedInvocationIndex);
                }
            }
            else
            {
                VerifyAllInvocationIndices();
            }
            
        }
        
        public void Verify(SequenceVerifyDelegate verifier)
        {
            var expected = expectedInvocations.Values.AsEnumerable<IInvocable>().ToList();
            List<IInvocable> expectedLoopedInvocations = new List<IInvocable>();
            for(var i = 0; i < loops; i++)
            {
                expectedLoopedInvocations.AddRange(expected);
            }
            verifier(expectedLoopedInvocations.AsReadOnly(), Invocations.AsReadOnly());
        }

        void ISequence.RegisterInvocable(IInvocable setUp)
        {
            SetIsInvocationIndices(setUp.SequenceInvocationIndices != null);
            if (!isInvocationIndices)
            {
                setUp.SequenceInvocationIndices = new SequenceInvocationIndices(Enumerable.Range(setupTimesCount, setUp.ConsecutiveInvocations).ToArray());
                setupTimesCount = setupTimesCount + setUp.ConsecutiveInvocations;
            }
            AddExpectedInvocations(setUp);
            SetBehavior(setUp.Mock);
        }
        
        void ISequence.Invoked(IInvocable setUp)
        {
            Invocations.Add(setUp);
            if (VerifiesUponInvocation())
            {
                VerifyCurrentInvocation();
            }
        }
        
    }
}
