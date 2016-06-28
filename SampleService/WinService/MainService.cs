using NLog;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace SampleService
{
    /// <summary>
    /// Actual service
    /// </summary>
    partial class MainService : ServiceBase
	{
        // !! the service should have NO constructor. All initialisation is to be performed in OnStart method !!

		///<summary> This class logger </summary>
		private Logger Log;

		///<summary> A task in which business-code will run </summary>
		private Task WorkerTask;

		///<summary> Cancellation token for WorkerTask </summary>
		private CancellationTokenSource TokenCreator;

		///<summary> App-wide objects are stored here </summary>
		CommonObjects Commons;

		///<summary> Service start method </summary>
		protected override void OnStart(string[] args)
		{
            // initialise logger
			this.Log = LogManager.GetLogger(this.ServiceName);

			this.Log.Info(string.Format("{0}Launching service {1}...", Environment.NewLine, this.ServiceName));

			try
			{
				this.Log.Info(string.Format("Settings loading for {0}...", this.ServiceName));

				// Here we take sattings form .config file
				NameValueCollection settingsCollection = ConfigurationManager.AppSettings;
				// Create new instance of common objects
				this.Commons = new CommonObjects(settingsCollection);

				this.Log.Info("Settings are loaded.");

				this.TokenCreator = new CancellationTokenSource();
				this.WorkerTask = new Task(this.StartAction, this.TokenCreator.Token, TaskCreationOptions.LongRunning);
				this.WorkerTask.Start();
			}
			catch (Exception ex)
			{
				this.Log.Error(string.Format("Error on start of service {0}: {1}.", this.ServiceName, ex));
			}
		}

		///<summary> Stopping service method </summary>
		protected override void OnStop()
		{
			this.Log.Info(string.Format("Stopping service {0}: ...", this.ServiceName));

			bool finishedSuccessfully = false;
			try
			{
				// Вызываем завершение таска обработки писем
				this.TokenCreator.Cancel();
				var timeout = TimeSpan.FromSeconds(5);
				finishedSuccessfully = this.WorkerTask.Wait(timeout);
			}
			catch (Exception ex)
			{
				this.Log.Error(string.Format("Exception on stopping service: {0}{1}"
					, Environment.NewLine, ex));
			}
			finally
			{
				if (finishedSuccessfully == true)
					this.Log.Info(string.Format("Service {0} stopped successfully.", this.ServiceName));
			}
		}

        ///<summary> Method where you launch worker Task </summary>
        private void StartAction()
        {
            // glag indicating encountered exception
            bool exception = false;

            // in case of exception the cycle will wait for some time and then launch the Task once again
            do
            {
                try
                {
                    if (exception == true)
                        this.Log.Info("Service after exception ...");

                    exception = false;
                    
                    MainAction actor = new MainAction(Commons);
                    Task trier = new Task(actor.Act, this.TokenCreator.Token, TaskCreationOptions.LongRunning);
                    trier.Start();
                    this.Log.Info("Service launched successfully.");
                    // wait for the Task to finish
                    // if there will be exceptions, the DO will be processed again
                    trier.Wait();

                }
                catch (Exception ex)
                {
                    exception = true;
                    this.Log.Error(string.Format("Exception while processing worder task: {0}{1}", Environment.NewLine, ex));
                    this.Log.Error(string.Format("Will wait after exception for: {0} ...",
                        this.Commons.Settings.OnExceptionDelay.ToString("HH:mm:ss")));
                    Thread.Sleep(this.Commons.Settings.OnExceptionDelay);
                }
            }
            while (exception == true);
        }
    }
}
