using SampleService.Settings;
using System;
using System.Collections.Specialized;

namespace SampleService
{
    /// <summary>
    /// Box for common entities
    /// </summary>
    public class CommonObjects
	{
		///<summary> Constructor </summary>
		public CommonObjects(NameValueCollection keysCollection)
		{
			if (keysCollection == null)
				throw new ArgumentNullException(nameof(keysCollection));

			this.Settings = new SettingsBox(keysCollection);
		}

		///<summary> Service settings </summary>
		public SettingsBox Settings { get; private set; }
	}
}
