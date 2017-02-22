using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MHP3rdController
{
    class Actions
    {
        [DllImport("user32.dll")]
        static extern bool keybd_event(byte bVk, byte bScan, uint dwFlags,
           int dwExtraInfo);

        const byte TRIANGLE = 0x49;
        const byte CROSS = 0x4B;
        const byte CIRCLE = 0x4C;
        const byte RIGHTK = 0x60;

        public void CircleClick()
        {
            keybd_event(CIRCLE, 0, 0, 0);
            Thread.Sleep(150);
            keybd_event(CIRCLE, 0, 2, 0);
        }

        public void TriangleClick()
        {
            keybd_event(TRIANGLE, 0, 0, 0);
            Thread.Sleep(150);
            keybd_event(TRIANGLE, 0, 2, 0);
        }
    }
}
