using System;

namespace SeasonChanger
{
    internal class RealDate
    { 
        public static DateTime Now { get
            {
                return DateTime.UtcNow + TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            } 
        }
    }
}

