using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHP3rdController
{
    class Addresses
    {
        /// <summary>
        /// Address of console memory
        /// </summary>
        public IntPtr Base;

        /// <summary>
        /// Camera angle horisontal       2 bytes
        /// </summary>
        public IntPtr LookHor;

        /// <summary>
        /// Scope angle horisontal        2 bytes
        /// </summary>
        public IntPtr ScopeHor;

        /// <summary>
        /// Scope angle vertical          1 byte
        /// </summary>
        public IntPtr ScopeVer;

        /// <summary>
        /// Bullets curr amount           1 byte
        /// </summary>
        public IntPtr AmmoCurAmount;

        /// <summary>
        /// If weapon equipped            1 byte
        /// </summary>
        public IntPtr IsWeaponEquipped;

        /// <summary>
        /// If scope active               1 byte
        /// </summary>
        public IntPtr IsScopeActive;

        /// <summary>
        /// Current ammo slot             1 byte
        /// </summary>
        public IntPtr AmmoCurSlotIndex;

        /// <summary>
        /// Inventory                     96 bytes for standart inventory
        /// </summary>
        public IntPtr Inventory;

        public Addresses(IntPtr BaseAddress)
        {
            Base = BaseAddress;

            // Адреса работают для синглплеера и хоста
            //
            LookHor = Base + 0x9F4E3A0;
            ScopeVer = Base + 0x964A4F2;
            ScopeHor = Base + 0x9649A58;
            AmmoCurAmount = Base + 0x964A4F0;
            IsWeaponEquipped = Base + 0x964AD4C;
            IsScopeActive = Base + 0x8ABB0E9;
            AmmoCurSlotIndex = Base + 0x964A4FA;
            Inventory = Base + 0x9FAF7FE;
        }
    }
}
