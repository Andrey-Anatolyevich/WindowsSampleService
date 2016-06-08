using System;
using System.Collections.Specialized;

namespace SampleService.Settings
{
    ///<summary> Holder of all settings data </summary>
    public class SettingsBox
    {
        ///<summary> Constructor </summary>
        public SettingsBox(NameValueCollection keysCollection)
        {
            if (keysCollection == null)
                throw new ArgumentNullException(nameof(keysCollection));


            try
            {
                // Timer delay processor
                string timerString = keysCollection[Consts.Config.TimerDelay];
                this.TimerDelay = timerString.ToTimeSpan(Consts.Config.DelayStringSeparator);

                // Delay after exception
                string excDelayString = keysCollection[Consts.Config.OnExceptionDelay];
                this.OnExceptionDelay = excDelayString.ToTimeSpan(Consts.Config.DelayStringSeparator);

                // Other settings variables
                /*
                 * Place here any other used keys
                 */
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Exception while restoring settings from NameValueCollection: {0}{1}"
                    , Environment.NewLine, ex));
            }
        }

        ///<summary> Timer delay </summary>
        public readonly TimeSpan TimerDelay;

        ///<summary> Delay after Exception </summary>
        public readonly TimeSpan OnExceptionDelay;
    }
}
