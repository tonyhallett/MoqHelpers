namespace MoqHelpers.InSequence
{
    internal static class Invocable
    {
        private static IInvocableFactory invocableFactory = new InvocableFactory();
        public static IInvocableFactory Factory
        {
            get
            {
                return invocableFactory;
            }
            set
            {
                invocableFactory = value;
            }
        }
    }
}
