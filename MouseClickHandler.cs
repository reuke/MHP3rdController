using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHP3rdController
{
    static class MouseClickHandler
    {
        private static MouseHookListener MouseHook;

        public static void Run()
        {
            MouseHook = new MouseHookListener(new GlobalHooker());
            MouseHook.MouseDownExt += MouseHook_MouseDownExt;
            MouseHook.MouseUp += MouseHook_MouseUp;
            MouseHook.Enabled = true;
        }

        private static void MouseHook_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
                Shooter.IsShooting = true;
        }

        private static void MouseHook_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                Shooter.IsShooting = false;
        }

    }
}
