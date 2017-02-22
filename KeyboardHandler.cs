using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

namespace MHP3rdController
{
    public static class KeyboardHandler
    {
        private static KeyboardHookListener KeyboardHook;

        public static void Run()
        {
            KeyboardHook = new KeyboardHookListener(new GlobalHooker());
            KeyboardHook.KeyUp += KeyboardHook_KeyUp;
            KeyboardHook.Enabled = true;
        }

        private static void KeyboardHook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F6)
                MouseMoveHandler.IsMouseEnabled = !MouseMoveHandler.IsMouseEnabled;
        }
    }
}
