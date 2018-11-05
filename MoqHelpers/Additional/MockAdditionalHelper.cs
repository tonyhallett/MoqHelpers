using Moq;
using System.Collections.Generic;
using System.Linq;

namespace MoqHelpers.Additional
{
    public static class MockAdditionalHelper {
        public static T GetAdditional<T>(this Mock mock)
        {
            try
            {
                var mockSequenced = mock.As<IAdditional<T>>();
                return mockSequenced.Object.___Additional;
            }
            catch (MockException)
            {
            }
            return default(T);
        }
        public static void AddAdditional<T>(this Mock mock,T additional)
        {
            var additionalMock = mock.As<IAdditional<T>>();
            additionalMock.SetupGet(s => s.___Additional).Returns(additional);
            var _ = additionalMock.Object.___Additional;//necessary if VerifyAll is called
            additionalMock.VerifyGet(s => s.___Additional);//necessary for VerifyNoOtherCalls
        }
        public static IEnumerable<IInvocation> WithoutAdditional(this IInvocationList invocationList)
        {
            //there is no setting to be concerned with
            return invocationList.Where(i => i.Method.Name != "get____Additional");
        }
    }
}
