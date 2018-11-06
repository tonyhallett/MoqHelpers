using Moq;
using Moq.Language;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.Invocables.NonSequenced.VerifiableWrappers
{
    [TestFixture]
    internal abstract class VerifiableWrappers_Tests_Base<TWrapped,TVerifiable> where TVerifiable:Verifiable<TWrapped> where TWrapped:class
    {
        private MockRepository mockRepository;
        internal Mock<IVerifiableWrapper> mockVerifiableWrapper;
        internal Mock<TWrapped> mockWrapped;
        internal TVerifiable verifiable;

        

        [SetUp]
        public void Setup()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            mockVerifiableWrapper = mockRepository.Create<IVerifiableWrapper>();
            mockWrapped = mockRepository.Create<TWrapped>();
            verifiable = (TVerifiable)Activator.CreateInstance(typeof(TVerifiable), mockWrapped.Object, mockVerifiableWrapper.Object);
        }

        [TearDown]
        public void Verify()
        {
            mockRepository.VerifyAll();
        }
    }



}
