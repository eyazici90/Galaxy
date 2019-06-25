using System;
using System.Collections.Generic;
using System.Text;

namespace EventStoreSample.Domain.Exceptions
{
    public class DailyAmountExceedException : Exception
    {

        public DailyAmountExceedException(string id)
            : base($"Max daily amount exceed for this transaction {id}")
        { }

    }
}
