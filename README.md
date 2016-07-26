# Sample Windows Service
Sample Windows service with NLog.<br/>
This version is debuggable from within Visual studio and launchable as console application.

General functionality:
The service perform required operations once the Timer is elapsed.<br/>
On exception, it waits for N time and launches the same worker again until the service is stopped <br/>
or the worker thread exits without exceptions.

Steps to launch as console app after compilation:<br/>
1) Create shortcut to compiled EXE file.<br/>
2) In properties of the shortcut add " console" after executable file path.<br/>
3) Save shortcut changes.<br />
3) Run the shortcut.<br />
<br/><br/>
To install service:<br />
1) Run compiled .exe file with attribute "install"
<br/><br/>
To uninstall service:<br/>
1) Run compiled .exe file with attribute "uninstall"
