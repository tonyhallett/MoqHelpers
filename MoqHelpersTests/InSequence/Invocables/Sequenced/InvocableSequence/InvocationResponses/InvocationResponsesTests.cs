using Moq;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.Invocables.Sequenced;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MoqHelpersTests.InSequence
{
	[TestFixture]
    public class InvocationResponsesTests
    {
        private (InvocationResponses<string>,string[]) CreateInvocationResponses(int numResponses)
        {
            var invocationResponses = new InvocationResponses<string>();
            string[] responses = new string[numResponses];
            for(var i = 0; i < numResponses; i++)
            {
                var response = i.ToString();
                responses[i] = response;
                invocationResponses.Add(response);
            }
            return (invocationResponses, responses);
        }
        [Test]
        public void Enumerator_Should_Return_Responses_From_Initializer()
        {
            var (invocationResponses, responses) = CreateInvocationResponses(100);
            var enumerator = ((IEnumerable)invocationResponses).GetEnumerator();
            List<object> enumerated = new List<object>();
            while (enumerator.MoveNext())
            {
                enumerated.Add(enumerator.Current);
            }
            Assert.That(enumerated, Is.EquivalentTo(responses));
        }
        [Test]
        public void EnumeratorT_Should_Return_Responses_From_Initializer()
        {
            var (invocationResponses,responses) = CreateInvocationResponses(10);
            var enumerator = invocationResponses.GetEnumerator();
            List<string> enumerated = new List<string>();
            while (enumerator.MoveNext())
            {
                enumerated.Add(enumerator.Current);
            }
            Assert.That(enumerated, Is.EquivalentTo(responses));
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void ConfiguredResponses_Should_Return_Number_Of_Responses_From_Initializer(int num)
        {
            var (invocationResponses, responses) = CreateInvocationResponses(num);
            Assert.That(((IInvocationResponses<string>)invocationResponses).ConfiguredResponses, Is.EqualTo(num));

        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Indexer_Should_Index_Into_Responses_From_Initializer(int index)
        {
            var (invocationResponses, responses) = CreateInvocationResponses(5);
            Assert.That(((IInvocationResponses<string>)invocationResponses)[index], Is.EqualTo(responses[index]));
        }
    }
}
