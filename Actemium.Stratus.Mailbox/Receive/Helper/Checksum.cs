﻿namespace Actemium.Stratus.MailboxPlugin.Receive.Helper
{
    public static class Checksum
    {
        public static uint CalculateResultChecksum(string data, int size)
        {
            int x = 0;
            uint csum = 0xFFFFFFFF;
            uint[] table = new uint[16]
                {	0x00000000, 0x1DB71064, 0x3B6E20C8, 0x26D930AC,
                    0x76DC4190, 0x6B6B51F4, 0x4DB26158, 0x5005713C,
                    0xEDB88320, 0xF00F9344, 0xD6D6A3E8, 0xCB61B38C,
                    0x9B64C2B0, 0x86D3D2D4, 0xA00AE278, 0xBDBDF21C
                };

            while ((size--) > 0)
            {
                csum ^= (byte)data[x++];
                csum = (csum >> 4) ^ table[csum & 15];
                csum = (csum >> 4) ^ table[csum & 15];
            }
            return csum;
        }
    }
}
