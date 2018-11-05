using Moq;
using MoqHelpers;
using MoqHelpers.InSequence;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoqHelpersTests.InSequence
{
    [TestFixture]
    public class SequenceInvocationIndices_Tests
    {
        [Test]
        public void Does_Not_Throw_With_Non_Repeated_Non_Negative()
        {
            void MethodWithCallIndicesArg(SequenceInvocationIndices callIndices)
            {
                callIndices.ToArray();
            }
            Assert.That(() => MethodWithCallIndicesArg(new SequenceInvocationIndices { 1, 3 }), Throws.Nothing);
        }
        [Test]
        public void Adding_Negative_Will_Throw()
        {
            Assert.That(
                ThrewHelper.Threw<SequenceSetupException>(() =>
                {
                    new SequenceInvocationIndices { 1, -1 };
                }, (e => e.NegativeCallIndex))
            );
        }
        [Test]
        public void Adding_Repeat_Will_Throw()
        {
            Assert.That(
                ThrewHelper.Threw<SequenceSetupException>(() =>
                {
                    new SequenceInvocationIndices { 1, 1 };
                }, (e => e.RepeatedCallIndexInSetup))
            );
        }

        [TestCaseSource("InitializerSource")]
        public int Initializer_Count_Should_Be_Num_Items(SequenceInvocationIndices sequenceInvocationIndices)
        {
            return sequenceInvocationIndices.Count;
        }
        public static List<TestCaseData> InitializerSource()
        {
            return new List<TestCaseData>
            {
                new TestCaseData(new SequenceInvocationIndices{ }){ExpectedResult=0},
                new TestCaseData(new SequenceInvocationIndices{ 1 }){ExpectedResult=1},
                new TestCaseData(new SequenceInvocationIndices{ 1,2 }){ExpectedResult=2},
            };
        }

        [Test]
        public void Initializer_IEnumerator_Should_Be_Added_Items()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 0, 1, 4, 9 };
            var enumerator=((IEnumerable)sequenceInvocationIndices).GetEnumerator();
            var enumerated = new List<object>();
            while (enumerator.MoveNext())
            {
                enumerated.Add(enumerator.Current);
            }
            Assert.That(enumerated, Is.EquivalentTo(new int[] { 0, 1, 4, 9 }));
        }
        [Test]
        public void Initializer_IEnumeratorT_Should_Be_Added_Items()
        {
            var sequenceInvocationIndices = new SequenceInvocationIndices { 0, 1, 4, 9 };
            var enumerator = sequenceInvocationIndices.GetEnumerator();
            var enumerated = new List<int>();
            while (enumerator.MoveNext())
            {
                enumerated.Add(enumerator.Current);
            }
            Assert.That(enumerated, Is.EquivalentTo(new int[] { 0, 1, 4, 9 }));
        }
        [Test]
        public void Singular_Should_Contain_A_Single_Item()
        {
            var singularIndex = 2;
            var singularSequence = SequenceInvocationIndices.Singular(singularIndex);
            Assert.That(singularSequence.Count, Is.EqualTo(1));
            var sequenceAsArray = singularSequence.ToArray();
            Assert.That(sequenceAsArray.Length, Is.EqualTo(1));
            Assert.That(sequenceAsArray[0], Is.EqualTo(singularIndex));
        }
        [Test]
        public void CallIndices_Can_Be_Provided_In_Ctor()
        {
            var indices = new int[] { 8, 6, 4 };
            var singularSequence = new SequenceInvocationIndices(indices);
            Assert.That(singularSequence.Count, Is.EqualTo(indices.Length));
            Assert.That(singularSequence, Is.EquivalentTo(indices));
        }
    }
}
