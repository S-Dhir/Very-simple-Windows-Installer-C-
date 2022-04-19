//I had also installed the Microsoft.Windows.Comaptibility nuget package
//https://www.nuget.org/packages/Microsoft.Windows.Compatibility/7.0.0-preview.3.22175.4
//
// PLEASE DO THIS:
//
//It may work w/out it but I highly recommend installing it
//The code installs the File in a random location- kge.t
//You also have to set the values of URL and inbytes to the URL and the size of the file respectively
// Please read the entire thing. It's just an example of one simple way to this stuff
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
        public static string URL = ""; // You must enter the url here
        public static long inbytes = ; // and the webpage size
        public static Thread? le;//A thread that continously displays the amount of data downnloaded
        public static bool runsThread = true;//Whether the "Downloading..." thing should be displayed or not. Once the file is downloaded this is set to false in line 100
      
        public static void Main(string?[]? args)// You might want to change this
        {
            Console.CursorVisible = false; // Make the cursor invisible so it's not visible
            if (!File.Exists("kge.t")) File.WriteAllText("kge.t", "", Encoding.Unicode); //Creating the file
            le = new Thread(() =>
            {

                while (true)
                {
                    Thread.Sleep(500);
                    if (runsThread == false)
                    {
                        return;// one way to exit the thread
                    }

                    Console.SetCursorPosition(0, 1);
                    int decimalPlaces = 2; //Increase this value if you are making it download a very very large file
                    Console.Write(Math.Round(((double)new FileInfo("kge.t").Length/double.Parse(inbytes.ToString()) * 100D), decimalPlaces) + "%");
                }
            });
            le.SetApartmentState(ApartmentState.MTA);
            le.Start();
            new Thread(() => 
            {
                //This thread just makes a cool display of the fact that it's downloading data.
                
                bool[] bits = { false, false };//This trick saves a ton of memory. This is the closes representation in C# to 00
                //Which is a binary digit
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
                    if(!bits[0] && !bits[1])//00
                    {
                        Console.Write("Downloading");
                        bits[0] = true;
                        bits[1] = false;
                        Thread.Sleep(300);
                    } else if(bits[0] && !bits[1])//10
                    {
                        bits[0] = false;
                        bits[1] = true;
                        Console.Write("Downloading.");
                    } else if(!bits[0] && bits[1])//01
                    {
                        bits[0] = true;
                        bits[1] = true;
                        Console.Write("Downloading..");
                    } else if(bits[0] && bits[1])//00
                    { 
                        bits[0] = false;
                        bits[1] = false;
                        Console.Write("Downloading...");
                    }
                }
            }).Start();
            
            main();
            Console.ReadKey();//After which the program exits
        }
        public static void main()
        {

            WebClient webClient = new();//I know it's a outdate library but it still works awesome
              webClient.DownloadFile(new Uri(URL), "kge.t"); // By the way, this works synchronously
            runsThread = false;// So this will only happen once the file has downloaded
            Thread.Sleep(300);// To give the thread some time to stop

            Console.Clear();
            Console.WriteLine("Done!!!");
            Console.CursorVisible = false;
        }
    }
}
