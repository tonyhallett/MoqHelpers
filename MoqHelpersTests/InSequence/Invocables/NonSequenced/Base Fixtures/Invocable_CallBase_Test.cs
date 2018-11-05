using Moq;
using Moq.Language;
using MoqHelpers.InSequence.Invocables;
using MoqHelpers.InSequence.Invocables.NonSequenced.VerifiableWrappers;
using NUnit.Framework;

namespace MoqHelpersTests.InSequence.SetUpWrappers
{
    [TestFixture]
    internal abstract class Invocable_CallBase_Test<TWrapped, TWrappedReturn, TInvocable>: Invocable_Wraps_Test_Base<TWrapped, TWrappedReturn, TInvocable> where TWrappedReturn : class where TWrapped : class, IVerifies, IThrows where TInvocable : InvocableBase<TWrapped>
    {
        protected abstract void SetupCallbase(Mock<TWrapped> mockWrapped, TWrappedReturn wrappedReturn);
        protected abstract void SetupVerifiableWrapper(Mock<IVerifiableWrapper> mockVerifiableWrapper, TWrappedReturn wrappedReturn, TWrappedReturn verifiableWrapperReturn);
        [Test]
        public void CallBase_Should_Wrap_The_Return_From_Wrapped()
        {
            SetupCallbase(mockWrapped, mockedWrappedReturn);
            SetupVerifiableWrapper(mockVerifiableWrapper, mockedWrappedReturn, mockedVerifiableWrapperReturn);

            wrappedReturn = ((dynamic)invocable).CallBase();
        }
    }

}
