using Moq;
using MoqHelpers.InSequence.Invocables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;


namespace MoqHelpersTests.InSequence.Invocables.NonSequenced
{
    [TestFixture]
    public class CallbackWrapperTests
    {
        public class CallDetails {
            public List<int> args;
            public bool called;
            public void AssertCalled(object[] callArgs)
            {
                Assert.That(called, Is.True);
                Assert.That(args, Is.EquivalentTo(callArgs));
            }
        }

        private static (Delegate,object[],CallDetails) CreateActionDelegateArgsCallDetails(MethodInfo wrapCallbackMethod)
        {
            CallDetails callDetails = new CallDetails();
            var actionType=wrapCallbackMethod.GetParameters()[0].ParameterType;
            var genericArguments = actionType.GetGenericArguments();

            object[] args = new object[genericArguments.Length];

            var listIntType = typeof(List<int>);
            var addMethod = listIntType.GetMethod("Add");
            Expression createArgs = Expression.New(listIntType.GetConstructor(new Type[] { }));

            ParameterExpression listVar = Expression.Variable(typeof(List<int>), "delegateArgs");
            Expression assignArgsToVariable = Expression.Assign(listVar, createArgs);


            List<Expression> block = new List<Expression> { assignArgsToVariable };
            ParameterExpression[] parameters = new ParameterExpression[genericArguments.Length];
            
            for (var i = 0; i < genericArguments.Length; i++)
            {
                var parameterExpression = Expression.Parameter(typeof(int), "param" + (i + 1).ToString());
                block.Add(Expression.Call(listVar, addMethod, parameterExpression));
                parameters[i] = parameterExpression;
                args[i] = i;
            }


            Expression setArgs = Expression.Assign(Expression.Field(Expression.Constant(callDetails), "args"), listVar);
            block.Add(setArgs);
            Expression setCalled = Expression.Assign(Expression.Field(Expression.Constant(callDetails), "called"), Expression.Constant(true));
            block.Add(setCalled);
            var expressionBlock = Expression.Block(new ParameterExpression[] { listVar }, block);

            return (Expression.Lambda(actionType, expressionBlock, parameters).Compile(),args,callDetails);

        }
        private static Type[] GetIntTypes(int numTypes)
        {
            return Enumerable.Range(0, numTypes).Select(_ => typeof(int)).ToArray();
        }
        
        private static List<MethodInfo> GetClosedWrapCallbackActionMethods()
        {
            var wrapCallbackMethods = typeof(CallbackWrapper).GetMethods().Where(m => m.Name == "WrapCallback");
            return wrapCallbackMethods.Where(m => m.GetParameters()[0].ParameterType!=typeof(Delegate)).Select(m =>
            {
                if (m.IsGenericMethodDefinition)
                {
                    var numGenerics = m.GetGenericArguments().Length;
                    return m.MakeGenericMethod(GetIntTypes(numGenerics));
                }
                return m;
            }).ToList();
        }

        [TestCaseSource("Source")]
        public void WrapCallback_Should_Return_Action_That_Invokes_Handler_And_Provided2(MethodInfo wrapCallbackMethod,Delegate action,object[] args,CallDetails callDetails)
        {
            var callbackWrapper = new CallbackWrapper();
            var mockCallbackInvokedHandler = new Mock<ICallbackInvokedHandler>();
            callbackWrapper.InvokedHandler = mockCallbackInvokedHandler.Object;

            var wrappedAction = (Delegate)wrapCallbackMethod.Invoke(callbackWrapper, new object[] { action });

            wrappedAction.DynamicInvoke(args);

            callDetails.AssertCalled(args);
            mockCallbackInvokedHandler.Verify(m => m.Invoked());
        }
        public static IEnumerable<TestCaseData> Source()
        {
            var actionWrapCallbackMethods = GetClosedWrapCallbackActionMethods();
            return actionWrapCallbackMethods.Select(m =>
            {
                var (del,args,callDetails)=CreateActionDelegateArgsCallDetails(m);
                return new TestCaseData(m,del, args, callDetails);
            });
        }
        
        [Test]
        public void Wrap_Delegate()
        {
            Assert.Fail("Not implemented");
        }
    }
}
