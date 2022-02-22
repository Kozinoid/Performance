using System;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    //                                                                   EVENTS                                                                 |
    //===========================================================================================================================================
    // Modified Parameters Event Args
    public class ModifiedParameterFieldsEventArgs<T> : EventArgs where T : Enum
    {
        //private OneParameterFieldManager<T>[] modifiedParameters;
        private int segment;
        private T parameter;
        private byte[] value;

        //public OneParameterFieldManager<T>[] ModifiedParameters { get { return modifiedParameters; } }
        public int Segment { get { return segment; } }
        public T Parameter { get { return parameter; } }
        public byte[] Value { get { return value; } }

        public ModifiedParameterFieldsEventArgs(int target, T par, byte[] val)
        {
            segment = target;
            parameter = par;
            value = val;
        }
    }

    public class SegmentChangedEventArgs : EventArgs
    {
        private int segment;
        public int Segment { get { return segment; } }

        public SegmentChangedEventArgs(int target)
        {
            segment = target;
        }
    }

    // Performance Common Parameters
    public enum PERFORMANCE_COMMON_PARAMETERS
    {
        PerformanceName = 0x00,     // 00

        EFXSource = 0x0C,           // 0C
        EFXType,                    // 0D
        EFXParameter1,              // 0E
        EFXParameter2,              // 0F
        EFXParameter3,              // 10
        EFXParameter4,              // 11
        EFXParameter5,              // 12
        EFXParameter6,              // 13
        EFXParameter7,              // 14
        EFXParameter8,              // 15
        EFXParameter9,              // 16
        EFXParameter10,             // 17
        EFXParameter11,             // 18
        EFXParameter12,             // 19
        EFXOutputAssign,            // 1A
        EFXMixOutSendLevel,         // 1B
        EFXChorusSendLevel,         // 1C
        EFXReverbSendLevel,         // 1D
        EFXControlSource1,          // 1E
        EFXControlDepth1,           // 1F
        EFXControlSource2,          // 20
        EFXControlDepth2,           // 21
        ChorusLevel,                // 22
        ChorusRate,                 // 23
        ChorusDepth,                // 24
        ChorusPreDelay,             // 25
        ChorusFeedback,             // 26
        ChorusOutput,               // 27
        ReverbType,                 // 28
        ReverbLevel,                // 29
        ReverbTime,                 // 2A
        ReverbHPDump,               // 2B
        DelayFeedback,              // 2C

        PerformanceTempo = 0x2D,    // 2D

        KeyboardRangeSwitch = 0x2F, // 2F

        KeyboardMode = 0x40,        // 40
        ClockSource                 // 41
    }

    // Performance Part Parameters
    public enum PERFORMANCE_PART_PARAMETERS
    {
        RecieveSwitch = 0x00,                   // 00                      
        MIDIChannel,                            // 01

        //PathGroupType,                        
        //PatchGroupID,                         
        PatchNumber,                            // 02

        PartLevel = 0x06,                       // 06
        PartPan,                                // 07
        PartCoarseTune,                         // 08
        PartFineTune,                           // 09
        OutputAssign,                           // 0A
        MIXEFXSendLevel,                        // 0B
        ChorusSendLevel,                        // 0C
        ReverbSendLevel,                        // 0D

        RecieveProgramChangeSwitch,             // 0E
        RecieveVolumeSwitch,                    // 0F
        RecieveHold1Switch,                     // 10
        KeyboardRangeLower,                     // 11
        KeyboardRangeUpper,                     // 12

        OctaveShift,                            // 13
        LocalSwitch,                            // 14
        TransmitSwitch,                         // 15
        TransmitBankSelectGroup,                // 16
        TransmitVolume                          // 17
    }
}
