using Moq;
using Moq.Language;
using Moq.Language.Flow;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MoqHelpersTests.InSequence.SetUpWrappers.VerifiableWrappers
{
    
    [TestFixture]
    internal abstract class Returns_Tests_Base<TWrapped,TVerifiable>: VerifiableWrappers_Returns_Tests_Base<TWrapped, IReturnsResult<IToMock>, TVerifiable> where TVerifiable : Verifiable<TWrapped> where TWrapped : class
    {
        [TestCaseSource("ReturnsSource")]
        public void Returns_Should_Wrap_Return_From_Wrapped(MethodInfo method, Func<Mock<TWrapped>, IReturnsResult<IToMock>, object> doSetupAndReturnArg)
        {
            var arg = doSetupAndReturnArg(mockWrapped, mockedWrappedReturn);
            mockVerifiableWrapper.Setup(m => m.WrapReturnsForVerification(mockedWrappedReturn)).Returns(mockedVerifiableWrapperReturn);

            wrappedReturn = (IReturnsResult<IToMock>)method.Invoke(verifiable, new object[] { arg });
        }

        public static List<object[]> ReturnsSource()
        {
            object GetArgument(Type parameterType)
            {
                object parameter = null;
                if (parameterType == typeof(string))
                {
                    parameter = "Some return";
                }
                else if (parameterType == typeof(Delegate))
                {
                    parameter = new Action(() => { });
                }
                else
                {
                    var genericArguments = parameterType.GetGenericArguments();
                    var parameterExpressions = genericArguments.Take(genericArguments.Length - 1).Select(t => Expression.Parameter(t)).ToArray();

                    parameter = Expression.Lambda(parameterType, Expression.Block(Expression.Constant("Hello!")), parameterExpressions).Compile();
                }
                return parameter;
            }

            Expression<Func<TWrapped, IReturnsResult<IToMock>>> GetSetup(MethodInfo method, object parameter)
            {
                ParameterExpression[] parameters = new ParameterExpression[1];
                var wrappedParameter = Expression.Parameter(typeof(TWrapped), "wrapped");
                parameters[0] = wrappedParameter;
                var invocation = Expression.Call(wrappedParameter, method, Expression.Constant(parameter));
                return Expression.Lambda<Func<TWrapped, IReturnsResult<IToMock>>>(invocation, parameters);
            }

            var returnsMethods = typeof(TWrapped).GetMethods().Where(method => method.Name == "Returns").Select(m =>
            {
                if (m.IsGenericMethodDefinition)
                {
                    var genericArguments = m.GetGenericArguments();
                    return m.MakeGenericMethod(genericArguments.Select(a =>
                    {
                        return typeof(int);
                    }).ToArray());
                }
                return m;
            });

            var source = returnsMethods.Select(method =>
            {

                var parameter = GetArgument(method.GetParameters()[0].ParameterType);

                Func<Mock<TWrapped>, IReturnsResult<IToMock>, object> func = (mock, result) =>
                {
                    mock.Setup(GetSetup(method, parameter)).Returns(result);
                    return parameter;
                };
                return new object[] { method, func };
            }).ToList();

            return source;
        }
    }
    
}
