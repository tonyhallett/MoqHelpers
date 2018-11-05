using MoqHelpers.InSequence.Invocables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqHelpers.InSequence
{
    public delegate void SequenceVerifyDelegate(IReadOnlyList<IInvocable> expectedSetupCalls, IReadOnlyList<IInvocable> setupCalls);
}
