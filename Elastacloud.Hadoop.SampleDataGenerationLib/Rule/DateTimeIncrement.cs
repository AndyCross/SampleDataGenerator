using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elastacloud.Hadoop.SampleDataGenerationLib.Rule
{
    public class DateTimeIncrement : DataGenerationRule
    {
        private readonly DateTime _startDate;
        private readonly int _numberOfMillisecTillEnd;

        public DateTimeIncrement(DateTime startDate, int numberOfDays)
        {
            _startDate = startDate;
            _numberOfMillisecTillEnd = numberOfDays * 24 * 60 * 60 * 1000;
        }
         
        public override object GenerateValue(int seed)
        {
            double d = new Random((int)DateTime.Now.Ticks).Next(seed, _numberOfMillisecTillEnd);

            return _startDate.AddMilliseconds(d);
        }
    }
}
