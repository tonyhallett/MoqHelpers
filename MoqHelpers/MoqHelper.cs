using Moq;

namespace MoqHelpers
{
    public static class MoqHelper{

        public static bool IsMocked<T>(T mocked) where T:class
        {
            return !ThrewHelper.Threw(() => Mock.Get<T>(mocked));
        }
    }
}
