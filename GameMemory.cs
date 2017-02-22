using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHP3rdController
{
    static class GameMemory
    {
        public static Addresses Address;
        private static ProcessMemoryReader Reader = new ProcessMemoryReader();
        private static int tempReadSize;

        /// <summary>
        /// Camera angle horisontal
        /// </summary>
        public static ushort LookHorisontal
        {
            get
            {
                byte[] raw = Reader.ReadProcessMemory((IntPtr)Address.LookHor, (int)2, out tempReadSize);
                return BitConverter.ToUInt16(raw, 0);
            }
            set
            {
                byte[] raw = BitConverter.GetBytes(value);
                Reader.WriteProcessMemory((IntPtr)Address.LookHor, raw, out tempReadSize);
            }
        }

        /// <summary>
        /// Scope angle horisontal
        /// </summary>
        public static ushort ScopeHorisontal
        {
            get
            {
                byte[] raw = Reader.ReadProcessMemory((IntPtr)Address.ScopeHor, (int)2, out tempReadSize);
                return (ushort)(BitConverter.ToUInt16(raw, 0) - 32767);
                //return BitConverter.ToUInt16(raw, 0);
            }
            set
            {
                byte[] raw = BitConverter.GetBytes((ushort)(value + 32767));
                //byte[] raw = BitConverter.GetBytes((value));
                Reader.WriteProcessMemory((IntPtr)Address.ScopeHor, raw, out tempReadSize);
            }
        }

        /// <summary>
        /// Scope angle vertical (Y)
        /// From -100 to 100
        /// </summary>
        public static int ScopeVertical
        {
            get
            {
                byte[] raw = Reader.ReadProcessMemory((IntPtr)Address.ScopeVer, (int)1, out tempReadSize);
                int retval = (int)raw[0];
                if (retval > 128) retval -= 256;
                return retval;
            }
            set
            {
                int val = value;

                if (val < -100) val = -100;
                if (val > 100) val = 100;
                if (val < 0) val += 256;

                byte[] raw = new byte[] { (byte)val };
                Reader.WriteProcessMemory((IntPtr)Address.ScopeVer, raw, out tempReadSize);
            }
        }

        /// <summary>
        /// Bullets curr amount
        /// </summary>
        public static byte AmmoCurAmount
        {
            get
            {
                byte[] raw = Reader.ReadProcessMemory((IntPtr)Address.AmmoCurAmount, (int)1, out tempReadSize);
                return raw[0];
            }
            set
            {
                byte[] raw = new byte[] { value };
                Reader.WriteProcessMemory((IntPtr)Address.AmmoCurAmount, raw, out tempReadSize);
            }
        }

        /// <summary>
        /// If scope active
        /// </summary>
        public static bool IsScopeActive
        {
            get
            {
                byte[] raw = Reader.ReadProcessMemory((IntPtr)Address.IsScopeActive, (int)1, out tempReadSize);
                return raw[0] == 1;
            }
        }

        /// <summary>
        /// If weapon equipped
        /// </summary>
        public static bool IsWeaponEquipped
        {
            get
            {
                byte[] raw = Reader.ReadProcessMemory((IntPtr)Address.IsWeaponEquipped, (int)1, out tempReadSize);
                return raw[0] == 0;
            }
        }
        

        public static void LoadAddresses()
        {
            if(WaitForLoadAddresses())
            {
                Reader.ReadProcess = Emulator.EmulatorProcess;
                Reader.OpenProcess();
            }
            else
            {
                Environment.Exit(1);
            }

        }

        private static bool WaitForLoadAddresses()
        {
            int i = 0;
            bool result = false;

            while (!result && i < 50)
            {
                result = TryLoadAddresses();
                if (!result)
                    System.Threading.Thread.Sleep(100);
            }
            return result;
        }

        private static bool TryLoadAddresses()
        {
            try
            {
            FileStream LogFileStream = new FileStream(Setting.PpssppLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader LogFileReader = new StreamReader(LogFileStream, System.Text.Encoding.UTF8);

            string LogText = LogFileReader.ReadToEnd();
            int LogAddressShift = LogText.LastIndexOf("Memory system initialized. RAM at ");

            IntPtr BaseAddress = IntPtr.Zero;

            if (IntPtr.Size == 8)
            {
                BaseAddress = ((IntPtr)UInt64.Parse(LogText.Substring(LogAddressShift + 34, 16), System.Globalization.NumberStyles.HexNumber) - 0x8000000);
            }
            else
            {
                BaseAddress = ((IntPtr)int.Parse(LogText.Substring(LogAddressShift + 43, 7), System.Globalization.NumberStyles.HexNumber) - 0x8000000);
            }

            if (BaseAddress != IntPtr.Zero)
                Address = new Addresses(BaseAddress);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
