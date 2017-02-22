using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace MHP3rdController
{
    static class Emulator
    {
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Process EmulatorProcess;

        public static bool IsInFocus
        {
            get
            {
                return EmulatorProcess != null && IsWindowVisible(EmulatorProcess.MainWindowHandle) && GetForegroundWindow() == EmulatorProcess.MainWindowHandle;
            }
        }

        public static Mouse.Point MidPoint
        {
            get
            {
                Rect size = new Rect();
                GetWindowRect(EmulatorProcess.MainWindowHandle, ref size);
                return new Mouse.Point(size.Left + (size.Right - size.Left) / 2, size.Top + (size.Bottom - size.Top) / 2);
            }
        }

        public static void Run()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = Setting.PpssppExePath;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            if (File.Exists(Setting.PpssppLogPath))
                try
                {
                    File.Delete(Setting.PpssppLogPath);
                }
                catch
                {
                    Environment.Exit(1);
                }
            //startInfo.Arguments = $"--log=\"{Setting.PpssppLogPath}\"  \"{Setting.MH3rdpIsoPath}\"";
            startInfo.Arguments = $"--state=\"{Setting.StatePath}\" --log=\"{Setting.PpssppLogPath}\"  \"{Setting.MH3rdpIsoPath}\"";

            EmulatorProcess = Process.Start(startInfo);
        }


    }
}
