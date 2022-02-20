using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    class RolandXP50Constants
    {
        // -------------------------------------------  Static fields  ----------------------------------------
        public static int MIDIChannelCount = 16;
        public static int SongChannelCount = 10;
        public static int FastChannelCount = MIDIChannelCount - SongChannelCount;
        public static uint TemporaryPerformanceAddress = 0x01000000;
        public static uint[] USERPerformanceAddresses = {
                0x10000000, 0x10010000, 0x10020000, 0x10030000, 0x10040000, 0x10050000, 0x10060000, 0x10070000,
                0x10080000, 0x10090000, 0x100A0000, 0x100B0000, 0x100C0000, 0x100D0000, 0x100E0000, 0x100F0000,
                0x10100000, 0x10110000, 0x10120000, 0x10130000, 0x10140000, 0x10150000, 0x10160000, 0x10170000,
                0x10180000, 0x10190000, 0x101A0000, 0x101B0000, 0x101C0000, 0x101D0000, 0x101E0000, 0x101F0000
            };
    }
}
