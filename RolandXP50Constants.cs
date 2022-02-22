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
        public static int PERFORMANCE_COMMON_LENTGH = 0x42;
        public static int PERFORMANCE_PART_LENTGH = 0x19;
    }

    //*************************  Performance Common parameters  *****************************
    public enum EFXSource : byte
    {
        PERFORM, Part1, Part2, Part3, Part4, Part5, Part6, Part7, Part8, Part9,
        Part11, Part12, Part13, Part14, Part15, Part16
    };
    public enum EFXOutputAssign : byte { MIX, OUTPUT1, OUTPUT2 };
    public enum EFXControlSource : byte { OFF, SYS_CTRL1, SYS_CTRL2, MODULATION, BREATH, FOOT, VOLUME, PAN, EXPRESSION, BENDER, AFTERTOUCH };
    public enum ChorusOutput : byte { MIX, REV, MIX_REV };
    public enum ReverbType : byte { ROOM1, ROOM2, STAGE1, STAGE2, HALL1, HALL2, DELAY, PAN_DELAY };
    public enum ReverbHPDump : byte
    {
        HPDump200, HPDump250, HPDump315, HPDump400, HPDump500, HPDump630, HPDump800,
        HPDump1000, HPDump1250, HPDump1600, HPDump2000, HPDump2500, HPDump3150, HPDump4000, HPDump5000, HPDump6300,
        HPDump8000, HPDumpBYPASS
    };
    public enum KeyboardRangeSwitch : byte { OFF, ON };
    public enum KeyboardMode : byte { LAYER, SINGLE };
    public enum ClockSource : byte { PERFORMANCE, SEQUENCER };
    //========================================================================================================

    //*************************  Performance Part parameters  *****************************
    public enum RecieveSwitch : byte { OFF, ON };
    public enum PathGroupType : byte { USER_PRESET, PCM, EXP };
    public enum OutputAssign : byte { MIX, EFX, OUTPUT1, OUTPUT2, PATCH };
    public enum RecieveProgramChangeSwitch : byte { OFF, ON };
    public enum RecieveVolumeSwitch : byte { OFF, ON };
    public enum RecieveHold1Switch : byte { OFF, ON };
    public enum LocalSwitch : byte { OFF, ON };
    public enum TransmitSwitch : byte { OFF, ON };
    public enum TransmitBankSelectGroup : byte { PATCH, GROUP1, GROUP2, GROUP3, GROUP4, GROUP5, GROUP6, GROUP7 };
    //========================================================================================================
}
