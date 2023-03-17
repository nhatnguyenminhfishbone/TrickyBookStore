using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickyBookStore.Services.Datetime
{
    internal class DatetimeService : IDatetimeService
    {
        public DateTimeOffset getEndDate(int year, int month)
        {
            DateTimeOffset startDate = new DateTime(year, month, 1);
            DateTimeOffset endDate = startDate.AddMonths(1).AddDays(-1);
            Console.WriteLine("- End date: " + endDate.ToString());
            return endDate;
        }

        public DateTimeOffset getStartDate(int year, int month)
        {
            DateTimeOffset startDate = new DateTime(year, month, 1);
            Console.WriteLine("- Start date: " + startDate.ToString());
            return startDate;
        }
    }
}
