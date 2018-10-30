using System;
using System.Collections;
using System.Collections.Generic;

namespace MoqHelpersTests
{
    public interface IToMock {
        void Method1();
        void Method2();
        string MethodWithArgument(string arg);
        string DefaultReferenceType();
        int DefaultInt();
        Nullable<int> DefaultNullable();
        IEnumerable<int> DefaultCollection();
        IOtherToMock DefaultMock();
        IOtherToMock AutoMock { get; }
        int Prop { get; set; }
        string ForMatcher(IEnumerable enumerable);
    }
}
