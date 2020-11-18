using System;

namespace RandomPairer.Common.TimeService
{
    public interface ITimeService
    {
        DateTime Now { get; }
        DateTime Today { get; }
        DateTime Tomorrow { get; }
        DateTime Yesterday { get; }
        DateTime UtcNow { get; }
    }
}
