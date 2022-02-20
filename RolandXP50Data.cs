using System.Runtime.InteropServices;


namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    //========================================================================================================
    //************************************  Performance Common  ********************************************//
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PERFORMANCE_COMMON
    {
        public byte PerformanceName1;
        public byte PerformanceName2;
        public byte PerformanceName3;
        public byte PerformanceName4;
        public byte PerformanceName5;
        public byte PerformanceName6;
        public byte PerformanceName7;
        public byte PerformanceName8;
        public byte PerformanceName9;
        public byte PerformanceName10;
        public byte PerformanceName11;
        public byte PerformanceName12;

        public EFXSource EFXSource;
        public byte EFXType;
        public byte EFXParameter1;
        public byte EFXParameter2;
        public byte EFXParameter3;
        public byte EFXParameter4;
        public byte EFXParameter5;
        public byte EFXParameter6;
        public byte EFXParameter7;
        public byte EFXParameter8;
        public byte EFXParameter9;
        public byte EFXParameter10;
        public byte EFXParameter11;
        public byte EFXParameter12;
        public EFXOutputAssign EFXOutputAssign;
        public byte EFXMixOutSendLevel;
        public byte EFXChorusSendLevel;
        public byte EFXReverbSendLevel;
        public EFXControlSource EFXControlSource1;
        public byte EFXControlDepth1;
        public EFXControlSource EFXControlSource2;
        public byte EFXControlDepth2;

        public byte ChorusLevel;
        public byte ChorusRate;
        public byte ChorusDepth;
        public byte ChorusPreDelay;
        public byte ChorusFeedback;
        public ChorusOutput ChorusOutput;

        public ReverbType ReverbType;
        public byte ReverbLevel;
        public byte ReverbTime;
        public ReverbHPDump ReverbHPDump;
        public byte DelayFeedback;

        public byte PerformanceTempoMSB;
        public byte PerformanceTempoLSB;
        public KeyboardRangeSwitch KeyboardRangeSwitch;

        public byte VoiceReserve1;
        public byte VoiceReserve2;
        public byte VoiceReserve3;
        public byte VoiceReserve4;
        public byte VoiceReserve5;
        public byte VoiceReserve6;
        public byte VoiceReserve7;
        public byte VoiceReserve8;
        public byte VoiceReserve9;
        public byte VoiceReserve10;
        public byte VoiceReserve11;
        public byte VoiceReserve12;
        public byte VoiceReserve13;
        public byte VoiceReserve14;
        public byte VoiceReserve15;
        public byte VoiceReserve16;

        public KeyboardMode KeyboardMode;
        public ClockSource ClockSource;

        //-------------------------------------  SECOND BUFFER (Fields Manager)  --------------------------------------------

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

    //========================================================================================================
    //************************************  Performance Part  **********************************************//
    [StructLayout(LayoutKind.Sequential, Pack = 1)]

    public struct PERFORMANCE_PART
    {
        public RecieveSwitch RecieveSwitch;                             
        public byte MIDIChannel;

        public PathGroupType PathGroupType;
        public byte PatchGroupID;
        public byte PatchNumberMSB;
        public byte PatchNumberLSB;

        public byte PartLevel;
        public byte PartPan;
        public byte PartCoarseTune;
        public byte PartFineTune;
        public OutputAssign OutputAssign;
        public byte MIXEFXSendLevel;
        public byte ChorusSendLevel;
        public byte ReverbSendLevel;

        public RecieveProgramChangeSwitch RecieveProgramChangeSwitch;
        public RecieveVolumeSwitch RecieveVolumeSwitch;
        public RecieveHold1Switch RecieveHold1Switch;                   // + Variant
        public byte KeyboardRangeLower;                                 // + Variant
        public byte KeyboardRangeUpper;                                 // + Variant

        public byte OctaveShift;                                        // + Variant
        public LocalSwitch LocalSwitch;                                 // + Variant
        public TransmitSwitch TransmitSwitch;                           
        public TransmitBankSelectGroup TransmitBankSelectGroup;
        public byte TransmitVolumeMSB;
        public byte TransmitVolumeLSB;
    }

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
