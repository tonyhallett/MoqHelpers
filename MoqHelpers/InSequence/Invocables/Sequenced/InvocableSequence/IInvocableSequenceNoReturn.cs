using Moq.Language.Flow;
using System;

namespace MoqHelpers.InSequence.Invocables.Sequenced
{
    interface IInvocableSequenceNoReturn<TMock>: IInvocableSequenceInternal<ISetup<TMock>, Exception> ,IInvocableSequence where TMock:class
    {
        
    }
}
