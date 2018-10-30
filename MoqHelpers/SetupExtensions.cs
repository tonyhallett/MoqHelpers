using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoqHelpers
{
    public static class SetupExtensions
    {
        public static void SetupMultipleExact<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> methodSelector, IEnumerable<ITuple> tuples,bool verifiable=false) where T : class
        {
            var method = (methodSelector.Body as MethodCallExpression).Method;

            foreach (var tuple in tuples)
            {
                var tupleLength = tuple.Length;
                var parameters = new List<ConstantExpression> { };
                for (var i = 0; i < tuple.Length - 1; i++)
                {
                    parameters.Add(Expression.Constant(tuple[i]));
                }
                var call = Expression.Call(methodSelector.Parameters[0], method, parameters.ToArray());
                var newLambda = Expression.Lambda<Func<T, TResult>>(call, methodSelector.Parameters[0]);
                var returns=mock.Setup(newLambda).Returns((TResult)tuple[tuple.Length - 1]);
                if (verifiable)
                {
                    returns.Verifiable();
                }
            }
        }
        
    }
}
