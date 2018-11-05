using Moq;
using Moq.Language;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqHelpersTests.InSequence.SetUpWrappers
{
    [TestFixture]
    internal abstract class Invocable_Wraps_Test_Base<TWrapped, TWrappedReturn,TInvocable> where TWrappedReturn:class where TWrapped:class, IVerifies, IThrows where TInvocable:InvocableBase<TWrapped>
    {
        protected TWrappedReturn wrappedReturn;//to be set in test
        protected TWrappedReturn mockedVerifiableWrapperReturn;
        protected TWrappedReturn mockedWrappedReturn;

        private MockRepository mockRepository;

        protected Mock<ISequence> mockSequence;
        protected Mock<IVerifiableWrapper> mockVerifiableWrapper;
        protected Mock<ICallbackWrapper> mockCallbackWrapper;

        protected TInvocable invocable;//clause ?
        protected Mock<TWrapped> mockWrapped;
        [SetUp]
        public void Setup()
        {
            mockedVerifiableWrapperReturn = new Mock<TWrappedReturn>().Object;
            mockedWrappedReturn = new Mock<TWrappedReturn>().Object;

            mockRepository = new MockRepository(MockBehavior.Loose);
            mockSequence = mockRepository.Create<ISequence>();
            mockVerifiableWrapper = mockRepository.Create<IVerifiableWrapper>();
            mockCallbackWrapper = mockRepository.Create<ICallbackWrapper>();

            mockWrapped = new Mock<TWrapped>();
            invocable = CreateInvocable(mockWrapped.Object, mockSequence.Object, mockVerifiableWrapper.Object, mockCallbackWrapper.Object);
        }
        protected abstract TInvocable CreateInvocable(TWrapped wrapped, ISequence sequence, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper);


        [TearDown]
        public void Verify()
        {
            mockRepository.VerifyAll();
            Assert.That(wrappedReturn, Is.EqualTo(mockedVerifiableWrapperReturn));
        }
    }

}
