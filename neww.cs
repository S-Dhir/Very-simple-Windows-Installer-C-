using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.IO.Enumeration;
using System.Net;
using System.Net.WebSockets;
using System.Net.Http;
using System.Windows;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

using System.Net.Sockets;

namespace notify
{
    class Neww
    {
        public static string URL = "https://raw.githubusercontent.com/S-Dhir/blog/main/webmap.txt";
        public static long inbytes = 216;
        public static Thread? le;
        public static bool runsThread = true;
        //        
        public static void Main(string?[]? args)
        {
            Console.CursorVisible = false;
            if (!File.Exists("kge.t")) File.WriteAllText("kge.t", "", Encoding.Unicode);
            le = new Thread(() =>
            {

                while (true)
                {
                    Thread.Sleep(500);
                    if (runsThread == false)
                    {
                        return;
                    }

                    Console.SetCursorPosition(0, 1);
                    Console.Write(Math.Round(((double)new FileInfo("kge.t").Length/double.Parse(inbytes.ToString()) * 100D), 2) + "%");
                }
            });
            le.SetApartmentState(ApartmentState.MTA);
            le.Start();
            new Thread(() => 
            {
                bool[] bits = { false, false };
                while(runsThread)
                {
                    if(!runsThread)
                    {
                        Application.ExitThread();
                        return;
                    }
                    Thread.Sleep(200);
                    Console.SetCursorPosition(0, 0);
                    Console.Write(new string(' ', Console.BufferWidth));
                    Console.SetCursorPosition(0, 0);
                    if(!bits[0] && !bits[1])
                    {
                        Console.Write("Downloading");
                        bits[0] = true;
                        bits[1] = false;
                        Thread.Sleep(300);
                    } else if(bits[0] && !bits[1])
                    {
                        bits[0] = false;
                        bits[1] = true;
                        Console.Write("Downloading.");
                    } else if(!bits[0] && bits[1])
                    {
                        bits[0] = true;
                        bits[1] = true;
                        Console.Write("Downloading..");
                    } else if(bits[0] && bits[1])
                    { 
                        bits[0] = false;
                        bits[1] = false;
                        Console.Write("Downloading...");
                    }
                }
            }).Start();
            
            main();
            Console.ReadKey();
        }
        public static void main()
        {

            WebClient webClient = new();
            //webClient.DownloadFileCompleted += FileDONE;
            //new Task(() =>
            //{
            webClient.DownloadFileCompleted += (e, eir) =>
              {

              };
              webClient.DownloadFile(new Uri(URL), "kge.t");
            runsThread = false;
            Thread.Sleep(300);

            Console.Clear();
            Console.WriteLine("Done!!!");
            Console.CursorVisible = false;

            //webClient.DownloadFileCompleted += FileDONE;
            //}).Start();
            //webClient.BaseAddress = "https://raw.githubusercontent.com/S-Dhir/blog/main/webmap.txt";
        }
    }
}
