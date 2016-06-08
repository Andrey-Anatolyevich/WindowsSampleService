# WindowsSampleService
Sample Windows service with NLog.
This version is debuggable from within Visual studio and launchable as console window.

This version perform required operations once the Timer is elapsed.
On exception, it waits for N time and launches the same worker again until the service is stopped or the worker thread exits without exceptions.

Steps to launch as console app after compilation:
1) Create shortcut to compiled EXE file.
2) In properties of the shortcut add " console" after executable file path. 
3) Save shortcut changes.
3) Run the shortcut
