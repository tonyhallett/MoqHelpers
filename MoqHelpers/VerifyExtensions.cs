using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoqHelpers
{
    public static class VerifyExtensions
    {
        #region Mocked verify
        //due to the MockRepository having Verify/VerifyAll there probably is no need for these
        //and additional overloads have not been provided
        public static void MockedVerify<T>(params T[] allMocked) where T : class
        {
            Mock.Verify(allMocked.Select(m => Mock.Get<T>(m)).ToArray());
        }
        public static void MockedVerify<T,U>(T mockedT,U mockedU) where T:class where U : class
        {
            Mock.Verify(Mock.Get(mockedT), Mock.Get(mockedU));
        }
        public static void MockedCollectionVerify<T, U>(IEnumerable<T> mockedT, IEnumerable<U> mockedU) where T : class where U : class
        {
            MockedVerify(mockedT.ToArray());
            MockedVerify(mockedU.ToArray());
        }
        public static void MockedVerifyAll<T>(params T[] allMocked) where T : class
        {
            Mock.VerifyAll(allMocked.Select(m => Mock.Get<T>(m)).ToArray());
        }
        public static void MockedVerifyAll<T, U>(T mockedT, U mockedU) where T : class where U : class
        {
            Mock.VerifyAll(Mock.Get(mockedT), Mock.Get(mockedU));
        }
        public static void MockedCollectionVerifyAll<T, U>(IEnumerable<T> mockedT, IEnumerable<U> mockedU) where T : class where U : class
        {
            MockedVerifyAll(mockedT.ToArray());
            MockedVerifyAll(mockedU.ToArray());
        }
        #endregion
        #region VerifyAndNoOthers
        public static void VerifyAllAndNoOthers<T>(this Mock<T> mock) where T : class
        {
            mock.VerifyAll();
            mock.VerifyNoOtherCalls();
        }
        public static void VerifyAndNoOthers<T>(this Mock<T> mock) where T : class
        {
            mock.Verify();
            mock.VerifyNoOtherCalls();
        }
        public static void VerifyAllAndNoOthers(this MockRepository mockRepository)
        {
            mockRepository.VerifyAll();
            mockRepository.VerifyNoOtherCalls();
        }
        public static void VerifyAndNoOthers(this MockRepository mockRepository)
        {
            mockRepository.Verify();
            mockRepository.VerifyNoOtherCalls();
        }
        #endregion
    }
}
