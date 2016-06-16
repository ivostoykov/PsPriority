PsPriority is a small standalone console utility that allows change process 
priority and affinity. No installation required.

Help is available when executable is run without parameters. For more
info see [Usage] section.

Should be run under admin privileges. Otherwhise error will be raised.


Download
========

Project as a source is located in 
https://github.com/ivostoykov/PsPriority

Standalone executable is available into the [ececutable_only] folder under the root.
Inside is the last stable compiled executable, ready to use.
Download, place it where suitable and run it.


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


Q&A
===

  * Why another console tool managing processes?

Well, for I failed to find (perhaps didn't search eanough) easy to 
automate tool. This is the idea behind this project - easy to use 
automated process management too.

  * Why to use it?

If you monitor the processes runing on your Windows box will probably 
notice that there are some of them using resourcies more aggresively. 
Use PsPriority to tame their behaviour.

Having politics applied on your box forcing a process to start or increase
resource consumtion is another reason to use PsPriority. Espessialy when
applied on a time base.


License
=======
This project is licensed under the terms of the GNU General Public License, version 2.
For more info please read gpl-2.0.txt available in the project


Changes in PsPriority 0.15.190
======================================
  * Initial stable version.


Please use Issues for any problems found. (https://github.com/ivostoykov/PsPriority/issues)

Ivo Stoykov
