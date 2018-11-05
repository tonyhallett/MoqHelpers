using System;

namespace MoqHelpers.InSequence.Invocables
{
    internal interface ICallbackWrapper
    {
        ICallbackInvokedHandler InvokedHandler { set; }
        Delegate WrapCallback(Delegate callback);
        Action WrapCallback(Action action);
        Action<T> WrapCallback<T>(Action<T> action);
        Action<T1,T2> WrapCallback<T1,T2>(Action<T1,T2> action);
        Action<T1, T2, T3> WrapCallback<T1, T2, T3>(Action<T1, T2, T3> action);
        Action<T1, T2, T3, T4> WrapCallback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action);
        Action<T1, T2, T3, T4, T5> WrapCallback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action);
        Action<T1, T2, T3, T4, T5, T6> WrapCallback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action);
        Action<T1, T2, T3, T4, T5, T6, T7> WrapCallback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action);
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> WrapCallback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action);
    }
}
