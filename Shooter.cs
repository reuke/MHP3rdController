using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MHP3rdController
{
    static class Shooter
    {
        public static bool IsShooting = false;
        
        private static Timer timer = new Timer(TimerCallback, null, 0, 100);

        private static void TimerCallback(object sender)
        {
            if (!GameMemory.IsWeaponEquipped || !IsShooting)
                return;
            
            if (GameMemory.AmmoCurAmount != 0)
                KeyActions.Shoot();
            else
                KeyActions.Reload();
        }

    }
}
