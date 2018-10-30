using Moq;
using MoqHelpers.InSequence.SetupWrappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;


namespace MoqHelpersTests.InSequence.SetUpWrappers
{
    [TestFixture]
    public class CallbackWrapperTests
    {
        private Assembly CoreAssembly = Assembly.GetAssembly(typeof(Action<,,,,,,,,,>));
        private List<int> args;
        private bool called;

        private LambdaExpression CreateActionExpression(int numTypes,Type actionType)
        {
            var listIntType = typeof(List<int>);
            var addMethod = listIntType.GetMethod("Add");
            Expression createArgs = Expression.New(listIntType.GetConstructor(new Type[] { }));

            ParameterExpression listVar = Expression.Variable(typeof(List<int>),"delegateArgs");
            Expression assignArgsToVariable = Expression.Assign(listVar, createArgs);


            List<Expression> block = new List<Expression> {assignArgsToVariable };
            ParameterExpression[] parameters = new ParameterExpression[numTypes];
            
            for(var i = 0; i < numTypes; i++)
            {
                var parameterExpression = Expression.Parameter(typeof(int),"param"+(i+1).ToString());
                block.Add(Expression.Call(listVar, addMethod, parameterExpression));
                parameters[i] = parameterExpression;
            }


            Expression setArgs = Expression.Assign(Expression.Field(Expression.Constant(this), "args"), listVar);
            block.Add(setArgs);
            Expression setCalled = Expression.Assign(Expression.Field(Expression.Constant(this), "called"), Expression.Constant(true));
            block.Add(setCalled);
            var expressionBlock = Expression.Block(new ParameterExpression[] { listVar }, block);

            return Expression.Lambda(actionType,expressionBlock, parameters);
            
        }
        private object[] CreateArgs(int numTypes)
        {
            var args = new object[numTypes];
            var counter = 0;
            for(var i = 0; i < numTypes; i++)
            {
                args[i] = counter;
                counter++;
            }
            return args;
        }
        private Type[] GetIntTypes(int numTypes)
        {
            return Enumerable.Range(0, numTypes).Select(_ => typeof(int)).ToArray();
        }
        private Type GetActionType(int numTypes)
        {
            if (numTypes == 0)
            {
                return typeof(Action);
            }
            else
            {
                Type genericActionType = null;
                var actionTypeName = "System.Action`" + numTypes;
                if (numTypes < 9)
                {
                    genericActionType = Type.GetType(actionTypeName);
                }
                else
                {
                    genericActionType = CoreAssembly.GetType(actionTypeName);
                }
                
                return genericActionType.MakeGenericType(GetIntTypes(numTypes));
            }
        }
        [Test]
        public void WrapCallback_Should_Return_Action_That_Invokes_Handler_And_Provided()
        {
            var callbackWrapper = new CallbackWrapper();
            var mockCallbackInvokedHandler = new Mock<ICallbackInvokedHandler>();
            callbackWrapper.InvokedHandler = mockCallbackInvokedHandler.Object;

            var wrapCallbackMethods = typeof(CallbackWrapper).GetMethods().Where(m=>m.Name=="WrapCallback");
            var actionWrapCallbackMethods = wrapCallbackMethods.Where(m => m.GetParameters()[0].Name.StartsWith("action")).Select(m =>
            {
                if (m.IsGenericMethodDefinition)
                {
                    var numGenerics = m.GetGenericArguments().Length;
                    return m.MakeGenericMethod(GetIntTypes(numGenerics));
                }
                return m;
            }).ToList();

            void Reset()
            {
                args = null;
                called = false;
            }

            var numWrapMethods = 17;
            var numTypes = Enumerable.Range(0, numWrapMethods);
                
            foreach (var num in numTypes)
            {
                Reset();

                var callArgs = CreateArgs(num);
                var actionType = GetActionType(num);

                var expression = CreateActionExpression(num,actionType);
                var action = expression.Compile();

                MethodInfo wrapCallbackMethod = actionWrapCallbackMethods[num];
                var wrappedAction = (Delegate)wrapCallbackMethod.Invoke(callbackWrapper, new object[] { action });
                wrappedAction.DynamicInvoke(callArgs);

                Assert.That(called, Is.True);
                Assert.That(args, Is.EquivalentTo(callArgs));
            }
            mockCallbackInvokedHandler.Verify(m => m.Invoked(), Times.Exactly(numWrapMethods));
        }
    }
}
