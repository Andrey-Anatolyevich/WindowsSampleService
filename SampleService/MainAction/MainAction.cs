using NLog;
using System;
using System.Timers;

namespace SampleService
{
	/// <summary>
	/// This class does actual work for MainService
	/// </summary>
	internal class MainAction
	{
		///<summary> Constructor </summary>
		public MainAction(CommonObjects commons)
		{
			if(commons == null)
				throw new ArgumentNullException(nameof(commons));


			this.Commons = commons;
			this.Log = LogManager.GetLogger(nameof(MainAction));
		}


		///<summary> Logger </summary>
		private Logger Log;

		///<summary> Instance of commmon objects </summary>
		private CommonObjects Commons;

		///<summary> Timer for delay after operation is complete </summary>
		private Timer Ticker
		{
			get
			{
				if(!this._Ticker_init)
				{
					this._Ticker = new Timer(this.Commons.Settings.TimerDelay.TotalMilliseconds);

					this._Ticker_init = true;
				}
				return this._Ticker;
			}
		}
		private Timer _Ticker;
		private bool _Ticker_init = false;


		///<summary> Actual worker method </summary>
		internal void Act()
		{
			this.Log.Info("Initial job launch.");
			// Do operations on initial start
			this.DoJob();
			this.Log.Info("Initial job is done.");

			// Start timer and do work once it's elapsed
			this.StartTicker();
		}

		///<summary> Start Timer and do work once elapsed </summary>
		private void StartTicker()
		{
			this.Ticker.AutoReset = false;
			this.Ticker.Elapsed += Ticker_Elapsed;

			this.Ticker.Start();

			string delayString = this.Commons.Settings.TimerDelay.ToString(@"hh\:mm\:ss");
			this.Log.Info(string.Format("Timer is set for {0} and started ...", delayString));
		}

		///<summary> Timer is elapsed handler </summary>
		private void Ticker_Elapsed(object sender, ElapsedEventArgs e)
		{
			this.Log.Info("Timer is elapsed. Work is STARTED ...");
			// Stop timer
			this.Ticker.Stop();

			this.DoJob();
			this.Log.Info("Work is DONE.");

			// Launch timer again
			this.Ticker.Start();
		}

		///<summary> Do work </summary>
		private void DoJob()
		{
			try
			{
				/*

				Here you place all the business logic.

				*/
			}
			catch(Exception ex)
			{
				this.Log.Error($"Exception encountered in {nameof(DoJob)}:", ex);
			}
		}
	}
}
