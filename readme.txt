PsPriority is a small utility that allows change process priority and 
affinity.

Help is available when executable is run without parameters. For more
info see [Usage] section.

This tool manipulates priority and affinity of any of the processes listed in Windows Task Manager (Name column). 


Usage
=====
     PsPriority process OR filename.exe [-a:Affinity] [-p:Priority] [-w] [-q]
             process is the name listed in Task Manager.
             filename.exe is the executable (without path) running the process.


             -a  -  Affinity defined which processor(s) to be used
                    (represented as a bit):

                    acceptable values must be in the range between:
                    1       using CPU 1
                    255     (default)   for all CPUs


             -p  -  Priority might be one of the following predefined values
                      AboveNormal
                      BelowNormal
                      High
                      Idle
                      Normal (default)
                      RealTime

             -w  -  wait for key press before exit

             -q  -  suppress all screen messages.


When necessary parameter is missing, default value is used.


Changes in PsPriority 0.15.190
======================================
  * Initial stable version.


Please use Issues for any problems found. (https://github.com/ivostoykov/PsPriority/issues)

Ivo Stoykov
