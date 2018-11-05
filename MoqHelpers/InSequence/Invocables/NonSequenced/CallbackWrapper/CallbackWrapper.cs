using System;

namespace MoqHelpers.InSequence.SetupWrappers
{
    internal class CallbackWrapper : ICallbackWrapper
    {
        private Action Invoked;
        public ICallbackInvokedHandler InvokedHandler { set { Invoked = new Action(()=>value.Invoked()); } }


        public Delegate WrapCallback(Delegate callback)
        {
            throw new NotImplementedException();

        }
        public Action WrapCallback(Action action)
        {
            return () =>
            {
                Invoked();
                action();
            };

        }
        public Action<T> WrapCallback<T>(Action<T> action)
        {
            return (t) =>
            {
                Invoked();
                action(t);
            };
        }
        public Action<T1, T2> WrapCallback<T1, T2>(Action<T1, T2> action)
        {
            return (t1, t2) =>
            {
                Invoked();
                action(t1, t2);
            };
        }
        public Action<T1, T2, T3> WrapCallback<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            return (t1, t2, t3) =>
            {
                Invoked();
                action(t1, t2, t3);
            };
        }
        public Action<T1, T2, T3, T4> WrapCallback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            return (t1, t2, t3, t4) =>
            {
                Invoked();
                action(t1, t2, t3, t4);
            };
        }
        public Action<T1, T2, T3, T4, T5> WrapCallback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
        {
            return (t1, t2, t3, t4, t5) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6> WrapCallback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
        {
            return (t1, t2, t3, t4, t5, t6) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7> WrapCallback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8, t9) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
            };
        }
        public Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        {
            return (t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16) =>
            {
                Invoked();
                action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
            };
        }

    }
}
