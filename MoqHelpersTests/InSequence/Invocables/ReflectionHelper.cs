using Moq;
using Moq.Language;
using Moq.Language.Flow;
using MoqHelpers.InSequence.Invocables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MoqHelpersTests.InSequence.Invocables
{
    internal static class ReflectionHelper
    {
        #region ICallbackWrapper
        private static Type ICallbackWrapperType = typeof(ICallbackWrapper);
        private static MethodInfo GetMockICallbackSetupMethod(object actionOrDelegate)
        {
            //not sure if need T - ICallbackWrapper
            var setupMethod = typeof(Mock<ICallbackWrapper>).GetMethods().First(m => m.Name == "Setup" && m.IsGenericMethod);
            return setupMethod.MakeGenericMethod(actionOrDelegate.GetType());
        }
        private static void SetupCallbackWrapper(Mock<ICallbackWrapper> mockCallbackWrapper, MethodInfo callbackMethod, object actionOrDelegateArg, object returnValue)
        {
            var setupMethod = GetMockICallbackSetupMethod(actionOrDelegateArg);
            var returns = setupMethod.Invoke(mockCallbackWrapper, new object[] { SetupCallbackWrapperExpression(callbackMethod, actionOrDelegateArg) });

            SetupCallbackWrapperReturns(returns, returnValue);
        }
        private static void SetupCallbackWrapperReturns(object returns, object returnValue)
        {
            var actionOrDelegateType = returnValue.GetType();
            var returnsActionOrDelegateType = typeof(IReturns<,>).MakeGenericType(ICallbackWrapperType, actionOrDelegateType);
            var returnsValueMethod = returnsActionOrDelegateType.GetMethods().First(m => m.Name == "Returns" && m.GetParameters()[0].ParameterType == actionOrDelegateType);

            returnsValueMethod.Invoke(returns, new object[] { returnValue });
        }
        private static LambdaExpression SetupCallbackWrapperExpression(MethodInfo method, object actionOrDelegateArg)
        {
            ParameterExpression[] parameterExpressions = new ParameterExpression[1];
            var wrappedParameter = Expression.Parameter(ICallbackWrapperType, "callbackWrapper");
            parameterExpressions[0] = wrappedParameter;
            var invocation = Expression.Call(wrappedParameter, method, Expression.Constant(actionOrDelegateArg));
            var actionOrDelegateType = actionOrDelegateArg.GetType();
            var funcType = typeof(Func<,>).MakeGenericType(ICallbackWrapperType, actionOrDelegateType);

            return Expression.Lambda(funcType, invocation, parameterExpressions);
        }
        public static IEnumerable<TestCaseData> CallbackSource<TWrapped, TResult>() where TWrapped : class
        {
            Func<MethodInfo, bool> exceptDelegate = (m => m.GetParameters()[0].ParameterType != typeof(Delegate));
            var methods = GetInterfaceMethods(typeof(TWrapped)).Where(m => m.Name == "Callback").Where(exceptDelegate).Select(CloseGeneric);
            var callbackMethods = typeof(ICallbackWrapper).GetMethods().Where(exceptDelegate).Select(CloseGeneric).ToList();

            return methods.Select(m =>
            {
                var parameterType = m.GetParameters().Select(p => p.ParameterType).First();
                var invocationArgument = CreateEmptyAction(parameterType);
                var callbackWrapperReturn = CreateEmptyAction(parameterType);

                Func<Mock<TWrapped>, TResult, Mock<ICallbackWrapper>, object[]> func = (mock, result, mockCallbackWrapper) =>
                {
                    var callbackMethod = callbackMethods.First(meth => meth.GetParameters()[0].ParameterType == parameterType);
                    SetupCallbackWrapper(mockCallbackWrapper, callbackMethod, invocationArgument, callbackWrapperReturn);
                    mock.Setup(GetMockSetupExpression<TWrapped, TResult>(m, new object[] { callbackWrapperReturn })).Returns(result);

                    return new object[] { invocationArgument };
                };
                return new TestCaseData(m, func);
            }).ToList();
        }
        #endregion

        #region common

        private static Expression<Func<TWrapped, TResult>> GetMockSetupExpression<TWrapped, TResult>(MethodInfo method, object[] parameters)
        {
            ParameterExpression[] parameterExpressions = new ParameterExpression[1];
            var wrappedParameter = Expression.Parameter(typeof(TWrapped), "wrapped");
            parameterExpressions[0] = wrappedParameter;
            var invocation = Expression.Call(wrappedParameter, method, parameters.Select(p => Expression.Constant(p)).ToArray());
            return Expression.Lambda<Func<TWrapped, TResult>>(invocation, parameterExpressions);
        }

        private static Func<MethodInfo, MethodInfo> CloseGeneric = (m =>
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
        private static List<MethodInfo> GetInterfaceMethods(this Type type)
        {
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            foreach (Type interf in type.GetInterfaces())
            {
                foreach (MethodInfo method in interf.GetMethods())
                    if (!methodInfos.Contains(method))
                        methodInfos.Add(method);
            }
            return methodInfos;
        }
        private static Delegate CreateEmptyAction(Type actionType)
        {
            var genericArguments = actionType.GetGenericArguments();
            var parameterExpressions = genericArguments.Select(t => Expression.Parameter(t)).ToArray();

            var func = Expression.Lambda(actionType, Expression.Block(Expression.Empty()), parameterExpressions).Compile();
            return func;
        }
        public static object GetActionOrDelegate(Type actionOrDelegateType)
        {
            object actionOrDelegate = null;
            if (actionOrDelegateType == typeof(Delegate))
            {
                actionOrDelegate = new Action(() => { });
            }
            else
            {
                actionOrDelegate = ReflectionHelper.CreateEmptyAction(actionOrDelegateType);
            }
            return actionOrDelegate;
        }


        #endregion

        #region Non ICallbackWrapper

        private static Func<IEnumerable<Type>, object[]> RaisesArgumentProvider = (types) =>
        {
            Action<IToMock> eventExpression = (_) => { };
            object secondArg = eventExpression;
            var argsOrFuncType = types.ElementAt(1);
            if (argsOrFuncType == typeof(object[]))
            {
                secondArg = new object[] { "1", 2 };
            }
            else if (argsOrFuncType == typeof(EventArgs))
            {
                secondArg = new EventArgs();
            }
            else
            {
                secondArg = ReflectionHelper.CreateEmptyFunc(argsOrFuncType);
            }

            return new object[] { eventExpression, secondArg };
        };
        private static Func<IEnumerable<Type>, object[]> ReturnsArgumentProvider = (types) =>
        {
            var parameterType = types.First();
            object parameter = null;
            if (parameterType == typeof(Delegate))
            {
                parameter = new Action(() => { });
            }
            else if (typeof(Delegate).IsAssignableFrom(parameterType))
            {
                parameter = ReflectionHelper.CreateEmptyFunc(parameterType);
            }
            else
            {
                if (parameterType == typeof(string))
                {
                    parameter = "!";
                }
                else
                {
                    parameter = Activator.CreateInstance(parameterType);
                }
            }

            return new object[] { parameter };
        };

        private static Delegate CreateEmptyFunc(Type funcType)
        {
            var genericArguments = funcType.GetGenericArguments();
            var parameterExpressions = genericArguments.Take(genericArguments.Length - 1).Select(t => Expression.Parameter(t)).ToArray();
            var returnType = genericArguments.Last();
            DefaultExpression returnExpression = Expression.Default(returnType);

            var func = Expression.Lambda(funcType, Expression.Block(returnExpression), parameterExpressions).Compile();
            return func;
        }

        public static IEnumerable<TestCaseData> InvocableRaisesSource<TWrapped>() where TWrapped : class
        {
            return ReflectionHelper.InvocableSource<TWrapped, IVerifies>(RaisesArgumentProvider, (m) =>
            {
                return m.Name == "Raises";
            });
        }
        public static IEnumerable<TestCaseData> InvocableReturnsSource<TWrapped>() where TWrapped : class
        {
            return ReflectionHelper.InvocableSource<TWrapped, IReturnsResult<IToMock>>(ReturnsArgumentProvider, m => m.Name == "Returns");
        }
        private static IEnumerable<TestCaseData> InvocableSource<TWrapped, TResult>(Func<IEnumerable<Type>, object[]> argumentProvider, Func<MethodInfo, bool> methodSelector) where TWrapped : class
        {
            return SourceBase<TWrapped, TResult, Func<Mock<TWrapped>, TResult, Mock<ICallbackWrapper>, object[]>>(argumentProvider, methodSelector, (f) => {

                return new Func<Mock<TWrapped>, TResult, Mock<ICallbackWrapper>, object[]>((mockWrapped, result, mockCallbackWrapper) => {
                    return f(mockWrapped, result);
                });
            });
        }

        public static IEnumerable<TestCaseData> RaisesSource<TWrapped>() where TWrapped : class
        {
            return ReflectionHelper.Source<TWrapped, IVerifies>(RaisesArgumentProvider, (m) =>
            {
                return m.Name == "Raises";
            });
        }
        public static IEnumerable<TestCaseData> ReturnsSource<TWrapped>() where TWrapped : class
        {
            return ReflectionHelper.Source<TWrapped, IReturnsResult<IToMock>>(ReturnsArgumentProvider, m => m.Name == "Returns");
        }

        public static IEnumerable<TestCaseData> Source<TWrapped, TResult>(Func<IEnumerable<Type>, object[]> argumentProvider, Func<MethodInfo, bool> methodSelector) where TWrapped : class
        {
            return SourceBase<TWrapped, TResult, Func<Mock<TWrapped>, TResult, object[]>>(argumentProvider, methodSelector, (f) => f);
        }
        private static IEnumerable<TestCaseData> SourceBase<TWrapped, TResult, TFunc>(Func<IEnumerable<Type>, object[]> argumentProvider, Func<MethodInfo, bool> methodSelector, Func<Func<Mock<TWrapped>, TResult, object[]>, TFunc> funcProvider) where TWrapped : class
        {
            var methods = GetInterfaceMethods(typeof(TWrapped)).Where(m => methodSelector(m)).Select(CloseGeneric);

            return methods.Select(m =>
            {
                var arguments = argumentProvider(m.GetParameters().Select(p => p.ParameterType));

                Func<Mock<TWrapped>, TResult, object[]> func = (mock, result) =>
                {
                    mock.Setup(GetMockSetupExpression<TWrapped, TResult>(m, arguments)).Returns(result);
                    return arguments;
                };
                var testCaseData = new TestCaseData(m, funcProvider(func));
                //todo - would like to build TestName - but is causing the adapter to behave strangely
                return testCaseData;
            }).ToList();
        }
        #endregion

    }
}


