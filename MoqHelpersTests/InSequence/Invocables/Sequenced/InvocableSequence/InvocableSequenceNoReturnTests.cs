using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.Invocables.Sequenced;
using NUnit.Framework;
using System;
using System.Reflection;

namespace MoqHelpersTests.InSequence.Sequence
{
    internal class InvocableSequenceNoReturn_Sets_Ctor_Args_To_Properties : InvocableSequence_Sets_Ctor_Args_To_Properties<InvocableSequenceNoReturn<IToMock>, ISetup<IToMock>, Exception>
    {
        protected override InvocableSequenceNoReturn<IToMock> CreateInvocableSequence(ISetup<IToMock> wrapped, Mock mock, ISequence sequence, IInvocationResponder<ISetup<IToMock>, Exception> responder)
        {
            return new InvocableSequenceNoReturn<IToMock>(wrapped, mock, sequence, responder);
        }

        protected override InvocableSequenceNoReturn<IToMock> CreateInvocableSequenceSequenceInvocationIndices(ISetup<IToMock> wrapped, Mock mock, ISequence sequence, IInvocationResponder<ISetup<IToMock>, Exception> responder, SequenceInvocationIndices sequenceInvocationIndices)
        {
            return new InvocableSequenceNoReturn<IToMock>(wrapped, mock, sequence, responder, sequenceInvocationIndices);
        }
    }
    
    [TestFixture]
    public class InvocableSequenceNoReturn_Applies_Callback_Test
    {
        [Test]
        public void Works()
        {
            var invocableSequenceNoReturn= new InvocableSequenceNoReturn<IToMock>(new Mock<ISetup<IToMock>>().Object, null, null,new Mock<IInvocationResponder<ISetup<IToMock>,Exception>>().Object);
            var applyCallbackMethod=invocableSequenceNoReturn.GetType().GetMethod("ApplyCallback", BindingFlags.Instance | BindingFlags.NonPublic);

            var callback = new Action(() => { });
            var mockWrapped = new Mock<ISetup<IToMock>>();
            applyCallbackMethod.Invoke(invocableSequenceNoReturn, new object[] { mockWrapped.Object,callback  });

            mockWrapped.Verify(m => m.Callback(callback));
        }
    }
    
}
