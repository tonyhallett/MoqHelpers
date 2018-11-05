using Moq;
using Moq.Language;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;
using System.Reflection;

namespace MoqHelpersTests.InSequence.SetUpWrappers
{
    [TestFixture]
    internal abstract class Invocable_RegistersForCallback_Test<TWrapped,TInvocable> where TWrapped:class,IVerifies,IThrows where TInvocable:InvocableBase<TWrapped>
    {
        [Test]
        public void RegistersForCallback()
        {
            Action action = () => { };
            var mockWrapped = new Mock<TWrapped>();
            SetWrappedForCallback(mockWrapped,action);
            var wrapped = mockWrapped.Object;
            //there is a call to RegisterForCallBack in the ctor - not verifying that one
            var invocable = CreateInvocable(new Mock<TWrapped>().Object);
            var registerForCallbackMethod=typeof(InvocableBase<TWrapped>).GetMethod("RegisterForCallback",BindingFlags.NonPublic|BindingFlags.Instance);
            registerForCallbackMethod.Invoke(invocable, new object[] { wrapped, action });

            mockWrapped.VerifyAll();
        }
        protected abstract void SetWrappedForCallback(Mock<TWrapped> mockWrapped,Action action);
        protected abstract TInvocable CreateInvocable(TWrapped wrapped);
    }
}
