using System;

namespace MoqHelpers
{
    public static class ThrewHelper
    {
        private static bool Threw(Action action,Func<Exception,bool> exceptionPredicate)
        {
            var threw = false;
            try
            {
                action();
            }
            catch(Exception exc)
            {
                threw = exceptionPredicate(exc);
            }
            return threw;
        }
        public static bool Threw(Action action)
        {
            return Threw(action, e => true);
        }
        public static bool Threw<T>(Action action) where T:Exception
        {
            return Threw(action, (exc) =>
            {
                return typeof(T).IsInstanceOfType(exc);
            });
        }

        public static bool Threw<T>(Action action,Func<T,bool> exceptionPredicate) where T:Exception
        {
            var threw = false;
            try
            {
                action();
            }
            catch (T exc)
            {
                threw = exceptionPredicate(exc);
            }
            return threw;
        }
    }
}
