using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MHP3rdController
{
    static class KeyActions
    {
        private static Actions Action = new Actions();

        private static Thread thread;

        public static void Shoot()
        {
            thread = new Thread(Action.CircleClick);
            thread.Start();
        }

        public static void Reload()
        {
            thread = new Thread(Action.TriangleClick);
            thread.Start();
        }
    }
}
