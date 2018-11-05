using System.Collections;
using System.Collections.Generic;

namespace MoqHelpers.InSequence
{
    public class ExceptionsOrReturns<TReturn> :InvocationResponses<ExceptionOrReturn<TReturn>>
    {
    }
}