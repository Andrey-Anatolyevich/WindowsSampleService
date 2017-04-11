using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace SampleService
{
	class Program
	{
		///<summary> Entrance </summary>
		public static void Main(String[] args)
		{
			// List of services to run
			ServiceBase[] ervicesToRun =
			{
				new MainService()
			};

			string firstArg = string.Empty;
			if(args.Length > 0)
				firstArg = args[0].ToLower();

			// if .Exe file is started with param "install" or "uninstall", install or uninstall it
			if(Environment.UserInteractive && (firstArg == "install" || firstArg == "uninstall"))
			{
				switch(firstArg)
				{
					case "install":
						ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
						break;
					case "uninstall":
						ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
						break;
				}
				return;
			}

			// if launched with single "console" argument, then just launch console app.
			// this way the app is testable as console application 
			if((Environment.UserInteractive && System.Diagnostics.Debugger.IsAttached)
				|| (firstArg == "console"))
				RunInteractiveServices(ervicesToRun);
			else
				ServiceBase.Run(ervicesToRun);
		}

		///<summary> Run services from console Application </summary>
		private static void RunInteractiveServices(ServiceBase[] servicesToRun)
		{
			MethodInfo onStartMethod = typeof(ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);

			foreach(ServiceBase service in servicesToRun)
			{
				Console.WriteLine(string.Format("Service {0}:", service.ServiceName));
				Console.Write("Starting ... ");
				onStartMethod.Invoke(service, new object[] { new string[] { } });
				Console.Write("Started");
			}

			// Waiting the end
			Console.WriteLine();
			Console.WriteLine("Press a key to call OnStop of all services ...");
			Console.ReadKey();
			Console.WriteLine();

			// Get the method to invoke on each service to stop it
			MethodInfo onStopMethod = typeof(ServiceBase).GetMethod("OnStop", BindingFlags.Instance | BindingFlags.NonPublic);

			// Stop loop
			foreach(ServiceBase service in servicesToRun)
			{
				Console.WriteLine(string.Format("Service {0}:", service.ServiceName));
				Console.Write("Stopping ... ");
				onStopMethod.Invoke(service, null);
				Console.WriteLine("Stopped");
			}

			Console.WriteLine();
			Console.WriteLine("All services are stopped.");

			if(System.Diagnostics.Debugger.IsAttached)
				Console.ReadKey();
		}
	}
}
