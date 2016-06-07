/*
 * Created by SharpDevelop.
 * User: ivo.stoykov
 * Date: 14.4.2016 г.
 * Time: 16:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PsPriority
{
	class Program
	{
	  private static ProcessPriorityClass ppc = ProcessPriorityClass.Normal;
    private static IntPtr pa = IntPtr.Zero;
    private static String ServiceName = "";
    private static bool doWait = false;
    private static bool beQuiet = false;
    
		public static void Main(string[] args)
		{
		  StringBuilder sb = new StringBuilder();
		  if(!CheckArgs(args)) 
		  {
		    PreExit();
		    Environment.Exit(0);
		  }
      Process[] pr = Process.GetProcessesByName(ServiceName);
      if(pr.Length == 0)
      {
        ShowOutput(String.Format("Process {0} not found", args[0]));
        doWait = true;
        PreExit();
        Environment.Exit(0);
      }
      if(pr[0].ProcessorAffinity==pa && pr[0].PriorityClass==ppc)
      {
        ShowOutput(String.Format("Process {0} already set to: ProcessorAffinity: {1}; PriorityClass: {2}",
                                 args[0], pr[0].ProcessorAffinity, pr[0].PriorityClass));
        PreExit();
        Environment.Exit(0);
      }
      sb.AppendLine(String.Format("Process {0} is named {1} with PID {2}", args[0], pr[0].ProcessName, pr[0].Id));
      sb.AppendLine(String.Format("\tbefore: Affinity: {0}; Priority {1}", pr[0].ProcessorAffinity, pr[0].PriorityClass));
      if(pa != IntPtr.Zero) 
      {
        try {  pr[0].ProcessorAffinity = pa;  } 
        catch (Exception e) {  ShowErrors(args, e);  }
      }
      try {  pr[0].PriorityClass = ppc;  }
      catch (Exception e) {  ShowErrors(args, e);  }
      sb.AppendLine(String.Format("\tafter: Affinity: {0}; Priority {1}", pr[0].ProcessorAffinity, pr[0].PriorityClass));
      ShowOutput(sb.ToString());
      PreExit();
		}

    static bool CheckArgs(string[] args)
    {
      if(args.Length==0)
      {
        DisplayHelp();
        return false;
      }
      return GetCmdParams(args);
    }
    
    private static bool GetCmdParams(string[] args)
    {
      foreach(string s in args)
      {
        String str = s.Substring(0,2).ToLower();
        switch(str)
        {
          case "-w":
            doWait = true;
            break;
          case "-q":
            beQuiet=true;
            break;
          case "-a":
            str = s.Substring(3);
            int i = 255;
            int.TryParse(str, out i);
            pa = (IntPtr)i;
            break;
          case "-p":
            str = s.Substring(3);
            bool r = Enum.TryParse(str, true, out ppc);
            if(!r)
            {
              ShowOutput(String.Format("[{0}]: Invalid value for -p argument.{1}", str, Environment.NewLine));
              DisplayHelp();
              return false;
            }
            break;
          default:
            ServiceName = Path.GetFileNameWithoutExtension(s);
//            ServiceName = s;
            break;
        }
      }
#if DEBUG
Debug.WriteLine("ServiceName: {0}", ServiceName);
Debug.WriteLine("pa: {0}", pa.ToString());
Debug.WriteLine("ppc: {0}", ppc.ToString());
#endif
      return true;
    }
		
    private static void ShowErrors(string[] args, Exception e)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Executed command Line:");
      sb.AppendLine(String.Format("{0}\t{1}{0}", Environment.NewLine, String.Join(" ", args)));
      sb.AppendLine(String.Format("\t{0} - {1}{2}", pa.ToString(), e.Message, Environment.NewLine));
      sb.AppendLine(String.Format("Type {0} without parameters to see help . . .", AppDomain.CurrentDomain.FriendlyName));
      if(e.InnerException != null) { sb.AppendLine(e.InnerException.Message); }
      ShowOutput(sb.ToString());
      PreExit();
      Environment.Exit(e.HResult);
    }
		
		private static void PreExit()
		{
		  if(!doWait || !beQuiet) { return; }
      Console.Write("Press any key to exit ...");
      Console.WriteLine("\t{0} {1} ", AppDomain.CurrentDomain.FriendlyName, typeof(Program).Assembly.GetName().Version.ToString());
      Console.ReadKey();
		}
    
		private static void ShowOutput(String s)
		{
		  if(beQuiet) { return; }
		  Console.WriteLine(s);
		}
		
    private static void DisplayHelp()
    {
      doWait = true;
      Console.WriteLine("Usage: ...");
      Console.WriteLine("\t {0} process OR filename.exe [-a:Affinity] [-p:Priority] [-w] [-q]", Process.GetCurrentProcess().ProcessName);
      Console.WriteLine("\t\t process is the name listed in Task Manager.");
      Console.WriteLine("\t\t filename.exe is the executable (without path) running the process.");
      Console.WriteLine(Environment.NewLine);
      Console.WriteLine("\t\t-a  -  Affinity defined which processor(s) to be used (represented as a bit):");
      Console.WriteLine("\t\t\tacceptable values must be in the range between:");
      Console.WriteLine("\t\t\t1\tusing CPU 1");
      Console.WriteLine("\t\t\t255 (default)\tfor all 7 CPUs (where available)");
      Console.WriteLine(Environment.NewLine);
      Console.WriteLine("\t\t-p  -  Priority might be one of the following predefined values");
      Console.WriteLine("\t\t\tAboveNormal");
      Console.WriteLine("\t\t\tBelowNormal");
      Console.WriteLine("\t\t\tHigh");
      Console.WriteLine("\t\t\tIdle");
      Console.WriteLine("\t\t\tNormal (default)");
      Console.WriteLine("\t\t\tRealTime");
      Console.WriteLine("{0}\t\t\t-w  -  wait for key press before exit", Environment.NewLine);
      Console.WriteLine("{0}\t\t\t-q  -  suppress all screen messages.", Environment.NewLine);
    }
	}
}