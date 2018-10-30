using Moq;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
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
    [TestFixture]
    internal abstract class VerifiableWrappers_Returns_Tests_Base<TWrapped, TWrappedReturn, TVerifiable> :VerifiableWrappers_Tests_Base<TWrapped, TVerifiable> where TVerifiable:Verifiable<TWrapped> where TWrapped:class where TWrappedReturn:class{
        protected TWrappedReturn wrappedReturn;
        protected TWrappedReturn mockedVerifiableWrapperReturn;
        protected TWrappedReturn mockedWrappedReturn;

        [SetUp]
        public void SetupReturns()
        {
            mockedVerifiableWrapperReturn = new Mock<TWrappedReturn>().Object;
            mockedWrappedReturn = new Mock<TWrappedReturn>().Object;
        }
        [TearDown]
        public void VerifyReturn()
        {
            Assert.That(wrappedReturn, Is.EqualTo(mockedVerifiableWrapperReturn));
        }

    }



}
