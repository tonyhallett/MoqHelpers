using Moq;
using MoqHelpers;
using MoqHelpers.InSequence;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MoqHelpersTests
{
    [TestFixture]
    public class ThrewHelperTests
    {
        [TestCase(true,ExpectedResult =true)]
        [TestCase(false, ExpectedResult = false)]
        public bool Should_Return_True_When_Action_Throws(bool throws)
        {
            Action action = null;
            if (throws)
            {
                action = () => throw new Exception();
            }
            else
            {
                action = () => { };
            }
            return ThrewHelper.Threw(action);
        }

        [Test]
        public void With_Generic_Should_Return_True_If_Throws_Instance_Of()
        {
            Assert.That(()=>ThrewHelper.Threw<SystemException>(() => throw new ArgumentException()), Is.True);
        }
        [Test]
        public void With_Generic_Should_Return_False_If_Does_Not_Throws_Instance_Of()
        {
            Assert.That(() => ThrewHelper.Threw<SystemException>(() => throw new Exception()), Is.False);
        }
        [Test]
        public void With_Generic_Should_Return_False_If_Does_Not_Throw()
        {
            Assert.That(() => ThrewHelper.Threw<SystemException>(() => { }), Is.False);
        }
        [Test]
        public void Threw_With_Predicate_Should_Throw_Thrown_If_Does_Not_Catch()
        {
            var thrown = false;
            var exception = new Exception();
            try
            {
                ThrewHelper.Threw<SystemException>(() => throw exception, (s => true));
            }catch(Exception exc)
            {
                thrown = true;
                Assert.That(exc, Is.EqualTo(exception));
            }
            Assert.That(thrown);

        }
        [Test]
        public void Threw_With_Predicate_Should_Return_False_If_Predicate_False()
        {
            var threw=ThrewHelper.Threw<Exception>(() => throw new Exception(), (s => false));
            
            Assert.That(threw,Is.False);

        }
        [Test]
        public void Threw_With_Predicate_Should_Return_True_If_Predicate_True()
        {
            var threw = ThrewHelper.Threw<Exception>(() => throw new Exception(), (s => true));

            Assert.That(threw, Is.True);
        }
        [Test]
        public void Threw_With_Predicate_Should_Return_False_If_No_Exception()
        {
            var threw = ThrewHelper.Threw<Exception>(() => { }, (s => true));

            Assert.That(threw, Is.False);
        }
    }
}
