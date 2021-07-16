using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace VeeamTaskKiller
{
    class TaskKiller
    {
        public string ProcessName { get; set; }
        public int KillTime { get; set; }
        public int CheckTime { get; set; }
        Timer killTimer;
        public void StartKiller(string process, string killT, string checkT)
        {
            ProcessName = process.Replace(".exe", "");
            KillTime = int.Parse(killT) * 60000;
            CheckTime = int.Parse(checkT) * 60000;
            killTimer = new(KillProcess, null, 0, CheckTime);
            HoldOn();
        }
        public void KillProcess(object state)
        {
            Process[] curProcess = Process.GetProcessesByName(ProcessName);
            if (curProcess.Length != 0)
            {
                foreach (var i in curProcess)
                {
                    Console.WriteLine($"Process: {i.ProcessName}({i.Id})\nStartup: {i.StartTime}\nLifetime: {DateTime.Now.Subtract(i.StartTime).ToString(@"hh\:mm\:ss")}\n");
                    if ((DateTime.Now.Subtract(i.StartTime)).TotalMilliseconds > KillTime)
                    {
                        i.Kill();
                        Console.WriteLine("Process sucsesful killed");
                    }
                    else
                    {
                        Console.WriteLine($"{ProcessName} lifetime less than {KillTime / 60000} minutes");
                    }
                }
            }
            else
                Console.WriteLine($"{ProcessName} not found");
        }
        public void HoldOn()
        {
            Console.ReadLine();
        }
    }
}