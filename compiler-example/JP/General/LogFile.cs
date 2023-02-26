#define __TRACE

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace General
{
    public enum LogLevel { All, Warning, AnyError, FatalError } ;

    public class LogFile
    {
        private class WorkLoad
        {
            private static int count = 0;

            private int processID;
            private string template;
            private string process;
            private string command;
            private long startTime;
            private long endTime;
            private double duration;

            public WorkLoad(string Template, string Process, string Command)
            {
                count++;
                if (Template.Length > 20)
                    Template = Template.Substring(0, 19) + "~";
                template = Template;
                if (Process.Length > 20)
                    Process = Process.Substring(0, 19) + "~";
                process = Process;
                command = Command.Replace("\r\n", "\n");
                startTime = DateTime.Now.Ticks;
            }
            public void ProcessStart()
            {
                startTime = DateTime.Now.Ticks;
            }
            public double ProcessEnd()
            {
                endTime = DateTime.Now.Ticks;
                duration = (double)(endTime - startTime);
                duration /= 10000000D; // in miliseconds
                count--;
                return duration;
            }
            public int ProcessID
            {
                set
                {
                    processID = value;
                }
                get
                {
                    return processID;
                }
            }
            public string Template
            {
                get
                {
                    return template;
                }
            }
            public string Process
            {
                get
                {
                    return process;
                }
            }
            public string Command
            {
                get
                {
                    return command;
                }
            }
            public long StartTime
            {
                get
                {
                    return startTime;
                }
            }
            public long EndTime
            {
                get
                {
                    return endTime;
                }
            }
            public double Duration
            {
                get
                {
                    return duration;
                }
            }
        }

        private ArrayList workLoads = new ArrayList();

        private const int columnWidth = 20;
        private string filePath;
        private bool fileDefined;
        private LogLevel logLevel = LogLevel.All;
        private bool dateInName;

        // Constructors
        public LogFile()
        {
            fileDefined = false;
        }
        public LogFile(string Path, bool DateInName)
        {
            filePath = Path;
            dateInName = DateInName;
            fileDefined = true;
        }
        // Properties
        public string FilePath
        {
            set
            {
                filePath = value;
            }
            get
            {
                return filePath;
            }
        }
        public bool FileDefined
        {
            get
            {
                return fileDefined;
            }
        }
        // Write method
        public void Write(LogLevel level, string template, string fileName, string source, string message)
        {
            lock (this)
            {
                string filePathTmp;
                if (dateInName)
                {
                    string day, month, year;
                    day = DateTime.Now.Day.ToString();
                    month = DateTime.Now.Month.ToString();
                    year = DateTime.Now.Year.ToString();
                    filePathTmp = filePath.Substring(0, filePath.Length - 4);
                    filePathTmp = filePathTmp + '_' + year + month + day + ".log";
                }
                else
                {
                    filePathTmp = filePath;
                }
                if (fileDefined && level <= logLevel)
                {
                    System.IO.FileInfo file = null;
                    StreamWriter sw = null;
                    try
                    {
                        
                        file = new FileInfo(filePathTmp);
                        
                        sw = file.AppendText();
                        if (template.Length > columnWidth)
                            template = template.Substring(0, columnWidth - 3) + "...";
                        if (fileName.Length > columnWidth)
                            fileName = fileName.Substring(0, 5) + "~" + fileName.Substring(fileName.Length - (columnWidth - 6), columnWidth - 6);


                        //sw.WriteLine("[{0}][{1, -20}][{2, -20}][{3, -20}]", DateTime.Now, template, fileName, source);
                        sw.WriteLine(message);
                        sw.Flush();
                    }
                    catch (System.IO.DirectoryNotFoundException)
                    {
                        fileDefined = false;
                    }
                    catch (Exception)
                    {
                        fileDefined = false;
                    }
                    finally
                    {
                        if (sw != null)
                        {
                            sw.Close();
                            sw = null;
                        }
                    }
                }
            }
        }
        public void Write(LogLevel level, string source, string message)
        {
            Write(level, "no template", "no file", source, message);
        }
        public void Write(LogLevel level, string message)
        {
            Write(level, "no template", "no file", "unknown", message);
        }

        public int WorkStart(string Template, string Process, string Command)
        {
            int pid;
            WorkLoad wLoad = new WorkLoad(Template, Process, Command);
            pid = workLoads.Add(wLoad);
            wLoad.ProcessID = pid;
            return pid;
        }
        public double WorkEnd(int PID)
        {
            double duration = 0D;

            WorkLoad wLoad = (WorkLoad)workLoads[PID];
            duration = wLoad.ProcessEnd();
            if (fileDefined)
            {
                System.IO.FileInfo file = null;
                StreamWriter sw = null;
                try
                {
                    file = new FileInfo(filePath);
                    sw = file.AppendText();

                    sw.WriteLine("{0};'{1}';'{2}';'{3}';{4};{5};{6};{7}", wLoad.ProcessID, DateTime.Now, wLoad.Template, wLoad.Process, wLoad.Command, wLoad.StartTime, wLoad.EndTime, wLoad.Duration);
                    sw.Flush();
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    TRACE(e.Message);
                    fileDefined = false;
                }
                catch (Exception e)
                {
                    TRACE(e.Message);
                    fileDefined = false;
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw = null;
                    }
                }
            }
            return duration;
        }
        [Conditional("__TRACE")]
        public void TRACE(string Message)
        {
            Console.WriteLine("TRACE:" + Message);
        }
    }
}
