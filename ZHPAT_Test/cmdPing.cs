using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ZHPAT_Test
{
    class cmdPing
    {
        public string cmdIPPing(string IP) 
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            string pingRst;
            process.Start();
            process.StandardInput.WriteLine("ping -n 1 " + IP);
            process.StandardInput.WriteLine("exit");

            pingRst = process.StandardOutput.ReadToEnd();
            Console.WriteLine(pingRst);
            process.Close();
            return pingRst;



        }



    }
}
