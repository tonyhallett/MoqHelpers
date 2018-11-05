using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqHelpers.InSequence
{
    public interface IInvocable
    {
        bool Verified { get; }
        Mock Mock { get; }
        int ConsecutiveInvocations { get; }
        SequenceInvocationIndices SequenceInvocationIndices { get; set; }
    }
}
