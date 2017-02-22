using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace MHP3rdController
{
    class Program
    {
        static ConsoleEventDelegate handler;

        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        
        public static bool Running = false;
        
        static void Main(string[] args)
        {
            Setting.LoadIni();
            Emulator.Run();

            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);
            
            GameMemory.LoadAddresses();
            MouseMoveHandler.Run();
            MouseClickHandler.Run();
            KeyboardHandler.Run();

            Timer timer = new Timer(TimerCallback, null, 0, 10);
            Running = true;
            
            System.Windows.Forms.Application.Run();
        }

        private static void TimerCallback(object sender)
        {
            Console.Clear();
            Console.WriteLine("Base address:     " + GameMemory.Address.Base.ToString("X"));

            Console.WriteLine("");

            Console.WriteLine("IsInFocus:        " + Emulator.IsInFocus.ToString());
            Console.WriteLine("IsMouseEnabled:   " + MouseMoveHandler.IsMouseEnabled.ToString());
            Console.WriteLine("IsScopeActive:    " + GameMemory.IsScopeActive.ToString());

            Console.WriteLine("");

            Console.WriteLine("Camera X:         " + GameMemory.LookHorisontal.ToString());
            Console.WriteLine("Scope X:          " + GameMemory.ScopeHorisontal.ToString());
            Console.WriteLine("Scope Y:          " + GameMemory.ScopeVertical.ToString());

            Console.WriteLine("");

            Console.WriteLine("Middle point:     " + Emulator.MidPoint.X.ToString() + " : " + Emulator.MidPoint.Y.ToString());

            Console.WriteLine("");

            Console.WriteLine("IsShooting:       " + Shooter.IsShooting.ToString());
            Console.WriteLine("IsWeaponEquipped: " + GameMemory.IsWeaponEquipped.ToString());
            Console.WriteLine("Bullet count:     " + GameMemory.AmmoCurAmount.ToString());

        }

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                Emulator.EmulatorProcess.Kill();
            }
            return false;
        }
    }
}
