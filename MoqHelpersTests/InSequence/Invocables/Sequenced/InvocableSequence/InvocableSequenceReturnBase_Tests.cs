using Moq;
using Moq.Language.Flow;
using MoqHelpers.InSequence;
using MoqHelpers.InSequence.Invocables.Sequenced;
using NUnit.Framework;
using System;

namespace MoqHelpersTests.InSequence.Sequence
{
    [TestFixture]
    public class InvocableSequenceReturnBase_Tests
    {
        class InvocableSequenceReturnBaseDerivation : InvocableSequenceReturnBase<IToMock, string, int>
        {
            public InvocableSequenceReturnBaseDerivation(ISetup<IToMock, string> wrapped, Mock mock, ISequence sequence, IInvocationResponder<ISetup<IToMock, string>, int> invocationResponder) : base(wrapped, mock, sequence, invocationResponder)
            {
            }
            public void DoApplyCallback(ISetup<IToMock, string> wrapped, Action callback)
            {
                ApplyCallback(wrapped, callback);
            }

        }
        [Test]
        public void Should_Apply_Callback()
        {
            var invocableSequenceBase = new InvocableSequenceReturnBaseDerivation(new Mock<ISetup<IToMock,string>>().Object, null, null,new Mock<IInvocationResponder<ISetup<IToMock,string>,int>>().Object);

            var callback = new Action(() => { });
            var mockWrapped = new Mock<ISetup<IToMock,string>>();

            invocableSequenceBase.DoApplyCallback(mockWrapped.Object,callback);
            mockWrapped.Verify(m => m.Callback(callback));

        }
        [Test]
        public void CallBase_Should_Call_On_Wrapped()
        {
            var mockWrapped = new Mock<ISetup<IToMock, string>>();
            var invocableSequenceBase = new InvocableSequenceReturnBaseDerivation(mockWrapped.Object, null, null, new Mock<IInvocationResponder<ISetup<IToMock, string>, int>>().Object);
            invocableSequenceBase.CallBase();
            mockWrapped.Verify(m => m.CallBase());
        }

    }
    
}
