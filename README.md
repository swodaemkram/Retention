# Retention
Service that runs on a widows machine t only allow 30 days file retention in a directory

This service was created for a spacific purpose.
It runs as a windows service and monitors the "C:\ProgramData\Gallagher\Backup\" Directory. 
It looks at the file creation dates and compares that to the current date if the file is older then 30 days it deletes it.

to install download the Retention.exe file. copy it to let say C:\Retention\

then run a cmd prompt as Administrator and run SC create Retention binpath= C:\Retention\Retention.exe start= auto
this will install Retention as a service.
Use service plugin to start it by right clicking Retention and chooseing start or reboot the computer.

to Uninstall Stop the Retention Service.
Start a cmd prompt as Administrator and run SC delete Retention.

As of right now all paramiters are hard coded this will change in future versions to allow Retention to remove only certen file extensions 
and change the retention time for a fixed 30 days to what ever you want.
Also I will add a way to change the hard coded Directory that Retention watches.

This will make a fairly usefull service to monitor log directorys and the like and prevent perpetual retention of files.

