using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace SampleService.WinService
{
	///<summary> Service installer </summary>
	[RunInstaller(true)]
	public class ProjectInstaller:Installer
	{
		///<summary> Constructor </summary>
		public ProjectInstaller()
		{
			ServiceProcessInstaller processInst;
			processInst = new ServiceProcessInstaller();
			processInst.Account = ServiceAccount.LocalSystem;

			ServiceInstaller serviceInst;
			serviceInst = new ServiceInstaller();
			serviceInst.ServiceName = Consts.Program.ServiceName;

			base.Installers.Add(processInst);
			base.Installers.Add(serviceInst);
		}
	}
}
