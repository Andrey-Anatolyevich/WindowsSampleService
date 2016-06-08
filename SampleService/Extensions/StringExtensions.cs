using System;
using System.Linq;

namespace SampleService
{
    ///<summary> Extensions for string </summary>
    public static class StringExtensions
    {
        ///<summary> Get timespan from formatted string. Expected format: [HH:mm:ss] </summary>
        public static TimeSpan ToTimeSpan(this string rawString, char blocksSeparator)
        {
            if (string.IsNullOrEmpty(rawString))
                throw new ArgumentException("rawString is null or empty");


            string[] timerSplitString = rawString.Split(blocksSeparator);

            if (timerSplitString.Count() != 3
                || timerSplitString.All(x => x.All(c => char.IsDigit(c))) == false)
                throw new Exception(string.Format("Provided string has wrong format. Expected format: [HH:mm:ss]. Provided string: {0}"
                    , rawString));

            int hours = int.Parse(timerSplitString[0]);
            int minutes = int.Parse(timerSplitString[1]);
            int seconds = int.Parse(timerSplitString[2]);
            TimeSpan result = new TimeSpan(hours, minutes, seconds);

            return result;

        }
    }
}
