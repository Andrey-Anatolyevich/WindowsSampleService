# Sample Windows Service
Sample Windows service with NLog.<br/>
This version is debuggable from within Visual studio and launchable as console window.

This version perform required operations once the Timer is elapsed.<br/>
On exception, it waits for N time and launches the same worker again until the service is stopped <br/>
or the worker thread exits without exceptions.

Steps to launch as console app after compilation:<br/>
1) Create shortcut to compiled EXE file.<br/>
2) In properties of the shortcut add " console" after executable file path.<br/>
3) Save shortcut changes.<br/>
3) Run the shortcut.<br/>
