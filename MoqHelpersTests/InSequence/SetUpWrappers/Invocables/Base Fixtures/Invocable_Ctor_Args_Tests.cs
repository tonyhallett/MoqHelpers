using Moq;
using Moq.Language;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System.Reflection;

namespace MoqHelpersTests.InSequence.SetUpWrappers
{
    [TestFixture]
    internal abstract class Invocable_Ctor_Args_Tests<TWrapped> where TWrapped:class,IVerifies,IThrows
    {
        private Mock<IToMock> mock = new Mock<IToMock>();
        private TWrapped wrapped = new Mock<TWrapped>().Object;
        private IVerifiableWrapper verifiableWrapper = new Mock<IVerifiableWrapper>().Object;
        private ICallbackWrapper callbackWrapper = new Mock<ICallbackWrapper>().Object;
        private ISequence sequence = new Mock<ISequence>().Object;
        [Test]
        public void Should_Pass_Ctor_Args_To_Base()
        {
            var consecutiveInvocations = 5;

            var invocable = CreateInvocable(wrapped, mock, sequence,consecutiveInvocations, verifiableWrapper, callbackWrapper);
            //Assert.That(invocable.Mock, Is.EqualTo(mock));
            Assert.That(invocable.ConsecutiveInvocations, Is.EqualTo(consecutiveInvocations));
            AssertCtorArgsToProperties(invocable);
        }
        private void AssertCtorArgsToProperties(InvocableBase<TWrapped> invocable)
        {
            Assert.That(invocable.Mock, Is.EqualTo(mock));
            var invocableInternal = invocable as IInvocableInternal<TWrapped>;
            Assert.That(invocableInternal.Sequence, Is.EqualTo(sequence));
            Assert.That(invocableInternal.Wrapped, Is.EqualTo(wrapped));

            var invocableBaseType = typeof(InvocableBase<>).MakeGenericType(typeof(TWrapped));

            var callbackWrapperField = invocableBaseType.GetField("callbackWrapper", BindingFlags.Instance | BindingFlags.NonPublic);
            var verifiableWrapperField = invocableBaseType.GetField("verifiableWrapper", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(callbackWrapperField.GetValue(invocable), Is.EqualTo(callbackWrapper));
            Assert.That(verifiableWrapperField.GetValue(invocable), Is.EqualTo(verifiableWrapper));

        }
        protected abstract InvocableBase<TWrapped> CreateInvocable(TWrapped wrapped, Mock<IToMock> mock,ISequence sequence, int consecutiveInvocations, IVerifiableWrapper verifiableWrapper, ICallbackWrapper callbackWrapper);
        protected abstract InvocableBase<TWrapped> CreateInvocableSequenceInvocationIndices(TWrapped wrapped, Mock<IToMock> mock,ISequence sequence, SequenceInvocationIndices sequenceInvocationIndices,IVerifiableWrapper verifiableWrapper,ICallbackWrapper callbackWrapper);
        [Test]
        public void Should_Pass_Ctor_Args_To_Base_SequenceInvocationIndices()
        {
            
            var sequenceInvocationIndices = new SequenceInvocationIndices();
            
            var invocable=CreateInvocableSequenceInvocationIndices(wrapped, mock, sequence, sequenceInvocationIndices, verifiableWrapper, callbackWrapper);

            Assert.That(invocable.SequenceInvocationIndices, Is.EqualTo(sequenceInvocationIndices));
            AssertCtorArgsToProperties(invocable);
            //Assert.That(invocable.Mock, Is.EqualTo(mock));
            //var invocableInternal = invocable as IInvocableInternal<TWrapped>;
            //Assert.That(invocableInternal.Sequence, Is.EqualTo(sequence));
            //Assert.That(invocableInternal.Wrapped, Is.EqualTo(wrapped));

            //var invocableBaseType = typeof(InvocableBase<>).MakeGenericType(typeof(TWrapped));

            //var callbackWrapperField = invocableBaseType.GetField("callbackWrapper", BindingFlags.Instance | BindingFlags.NonPublic);
            //var verifiableWrapperField = invocableBaseType.GetField("verifiableWrapper", BindingFlags.Instance | BindingFlags.NonPublic);
            //Assert.That(callbackWrapperField.GetValue(invocable), Is.EqualTo(callbackWrapper));
            //Assert.That(verifiableWrapperField.GetValue(invocable), Is.EqualTo(verifiableWrapper));

        }
    }
}
