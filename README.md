# Retention
Service that runs on a windows machine to only allow 30 days file retention in a directory

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

This will make a fairly usefull service to monitor log directorys and the like and prevent perpetual retention of files.<br>

*************************************************************************************************************************<br>

The paramiters are now hard coded but can be overridden by registry entrys <br>
----> HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Retention<br>
      ---->DaysToKeep - Integer 32 bit - (30)<br>
           ExtensionToMonitor - NOT IMPLAMENTED YET !<br>
           PathToMonitor - String - (C:\Whatever\)<br>
           TimeOfDayToDeleteFiles - String in - (HH:mm) format<br>
 I will be writing a GUI to create and manage this Registry Entry and thus manage the Retention Service<br>
 
