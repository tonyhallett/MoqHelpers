using Moq;
using Moq.Language;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MoqHelpers
{
    public static class SetupSequenceExtensions
    {
        public static ISetupSequentialResult<U> SetupSequenceEnumerable<T, U>(this Mock<T> mock, Expression<Func<T, U>> setUp, IEnumerable<U> returns) where T : class
        {
            var sequence = mock.SetupSequence(setUp);
            foreach (var ret in returns)
            {
                sequence.Returns(ret);
            }
            return sequence;
        }
        public static ISetupSequentialResult<U> SetupSequenceEnumerableThrowsOrReturns<T, U>(this Mock<T> mock, Expression<Func<T, U>> setUp, IEnumerable returnsOrExceptions) where T : class
        {
            var sequence = mock.SetupSequence(setUp);
            foreach (var ret in returnsOrExceptions)
            {
                if (ret!=null&&ret.GetType().IsSubclassOf(typeof(Exception)))
                {
                    sequence.Throws(ret as Exception);
                }
                else
                {
                    sequence.Returns((U)ret);
                }

            }
            return sequence;
        }
        public static ISetupSequentialResult<U> SetupSequenceIndex<T, U>(this Mock<T> mock, Expression<Func<T, U>> setUp, int count, Func<int, U> returns) where T : class
        {
            var sequence = mock.SetupSequence(setUp);
            for (var i = 0; i < count; i++)
            {
                var j = i;
                sequence.Returns(() => returns(j));
            }
            return sequence;
        }

        public static ISetupSequentialAction SetupSequenceEnumerableThrowsOrPasses<T>(this Mock<T> mock, Expression<Action<T>> setUp, IEnumerable passesAndExceptions) where T : class
        {
            var sequence = mock.SetupSequence(setUp);
            foreach (var passOrException in passesAndExceptions)
            {
                if (passOrException != null && passOrException.GetType().IsSubclassOf(typeof(Exception)))
                {
                    sequence.Throws(passOrException as Exception);
                }
                else
                {
                    sequence.Pass();
                }

            }
            return sequence;
        }
    }
    
}
