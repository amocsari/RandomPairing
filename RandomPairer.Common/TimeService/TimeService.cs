using System;

namespace RandomPairer.Common.TimeService
{
    public class TimeService : ITimeService
    {
        public DateTime Now { get { return DateTime.Now; } }
        public DateTime Today { get { return DateTime.Today; } }
        public DateTime Yesterday { get { return DateTime.Today.AddDays(-1).Date; } }
        public DateTime Tomorrow { get { return DateTime.Today.AddDays(1).Date; } }
        public DateTime UtcNow { get { return DateTime.UtcNow; } }
    }
}
