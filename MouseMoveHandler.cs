using System;
using System.Threading;

namespace MHP3rdController
{
    static class MouseMoveHandler
    {

        public static bool IsMouseEnabled = false;
            
        private static Timer MouseHandleTimer;

        public static void Run()
        {
            MouseHandleTimer = new Timer(TimerCallback, null, 0, 10);
        }
        
        private static double deltaY = 0.0;
        private static double deltaX = 0.0;

        private static bool IsScopeActive = false;
        
        private static void TimerCallback(object sender)
        {
            //return;

            if (!Emulator.IsInFocus || !IsMouseEnabled)
            {
                deltaX = 0.0;
                deltaY = 0.0;
                return;
            }

            Mouse.Point midpoint = Emulator.MidPoint;
            //Mouse.Point midpoint = new Mouse.Point(600, 300);
            Mouse.Point delta = midpoint - Mouse.Position;
            Mouse.Position = midpoint;

            deltaX += delta.X;
            deltaY += delta.Y;

            if (GameMemory.IsScopeActive != IsScopeActive)
            {
                IsScopeActive = GameMemory.IsScopeActive;
                deltaX = 0.0;
                deltaY = 0.0;
                return;
            }

            if (IsScopeActive)
            {
                int deltaYPossible = (int)Math.Floor(deltaY / Setting.ScopeYSens);
                if (deltaYPossible != 0)
                {
                    deltaY -= deltaYPossible * Setting.ScopeYSens;
                    GameMemory.ScopeVertical += deltaYPossible;
                }

                ushort deltaXPossible = (ushort)Math.Floor(deltaX / Setting.ScopeXSens);
                if (deltaXPossible != 0)
                {
                    deltaX -= deltaXPossible * Setting.ScopeXSens;
                    GameMemory.ScopeHorisontal = (ushort)(GameMemory.ScopeHorisontal + deltaXPossible);
                }
            }
            else
            {
                deltaY = 0.0;

                ushort deltaXPossible = (ushort)Math.Floor(deltaX / Setting.CameraXSens);
                if (deltaXPossible != 0)
                {
                    deltaX -= deltaXPossible * Setting.CameraXSens;
                    GameMemory.LookHorisontal = (ushort)(GameMemory.LookHorisontal + deltaXPossible);
                }
            }
        }
    }
}
