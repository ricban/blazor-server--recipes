using System;

namespace Library.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Tomorrow(this DateTime datetime)
        {
            return datetime.AddDays(1);
        }

        public static DateTime Yesterday(this DateTime datetime)
        {
            return datetime.AddDays(-1);
        }
    }
}