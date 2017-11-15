using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using WindowsInput;

namespace SaaW
{

    class Program
    {
        static void Main(string[] args)
        {
            var t = new System.Timers.Timer(TimeSpan.FromMinutes(5).TotalMilliseconds);
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(runCheck);
            t.Start();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void runCheck(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool time = false;
            DateTime dtCurrent = DateTime.Now;
            var buttonPush = new InputSimulator();

            if ((dtCurrent.Hour >= 7 && dtCurrent.Hour < 11) || (dtCurrent.Hour >= 13 && dtCurrent.Hour < 16))
                time = true;

            if ((dtCurrent.Hour == 11 || dtCurrent.Hour == 16) && dtCurrent.Minute <= 45)
                time = true;

            if ((dtCurrent.Hour >= 15 && dtCurrent.Minute > 45) && dtCurrent.DayOfWeek.ToString() == "Friday")
                time = false;

            if (time)
            {
                buttonPush.Mouse.MoveMouseBy(1, 1);
                buttonPush.Mouse.MoveMouseBy(-1, -1);
            }

            else
            {
                UserMethods.SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
                buttonPush.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.SPACE);
            }
        }

        internal static class UserMethods
        {
            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);
        }
    }
}
