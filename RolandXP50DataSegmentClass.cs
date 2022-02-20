using System;
using System.Text;
using System.Runtime.InteropServices;
using SynthLiveMidiController.MIDIMessages;
using System.Collections.Generic;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    //Dictionary<int, OneParameterFieldManager> parameters;
    //--------------------------------  One Fiel Manager  -------------------------------------
    class OneParameterFieldManager
    {
        readonly int offset = 0;
        readonly int length = 1;
        readonly byte[] parameterValue;

        public OneParameterFieldManager(int os, int len)
        {
            length = len;
            offset = os;
            parameterValue = new byte[len];
        }
    }

    // -----------------------------------------------  Data Segment Base Class  ---------------------------------------------------------------------
    abstract class DataSegmentClass
    {
        // Main Segment Address
        protected readonly uint segmentAddress;

        // Parameters Collection
        protected Dictionary<int, OneParameterFieldManager> parameters;

        // Main segment address getter
        public uint SegmentAddress { get { return segmentAddress; } }   

        // Segment data length
        public abstract uint Length { get; }

        // Segment data to byte array
        public abstract byte[] ToByteArray();

        // Base Constructor
        protected DataSegmentClass(uint addr)
        {
            segmentAddress = addr;
        }

        // Send segment data to device
        public abstract void SendData(IPerformanceMIDIInOutInterface commander);

        // Request segment data from device via Callback events
        public abstract void RequestData(IPerformanceMIDIInOutInterface commander);
    }

    // ---------------------------------------------  Performance Common Data Class  -----------------------------------------------------------------
    class PerformanceCommonClass : DataSegmentClass
    {
        // Performance Common Parameters
        public enum PerformanceCommonParameters
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

        // Parameters Manager
        private Dictionary<PerformanceCommonParameters, OneParameterFieldManager> parametersManager;

        // Structure
        PERFORMANCE_COMMON performanceCommonData;

        // Structure Length
        public override uint Length
        {
            get { return (uint)Marshal.SizeOf(performanceCommonData); }
        }

        // Constructor
        public PerformanceCommonClass(uint segAddr) : base(segAddr)
        {
            performanceCommonData = new PERFORMANCE_COMMON();
            ParametersManagerInit();
        }

        // Parameters Manager Init
        private void ParametersManagerInit()
        {
            foreach(PerformanceCommonParameters par in Enum.GetValues(typeof(PerformanceCommonParameters)))
            {
                int length = 1;
                if (par == PerformanceCommonParameters.PerformanceName) length = 12;
                if (par == PerformanceCommonParameters.PerformanceTempo) length = 2;
                int offset = (int)par;
                OneParameterFieldManager item = new OneParameterFieldManager(offset, length);
                parametersManager.Add(par, item);
            }
        }

        // Data Array -> Structure
        public void CopyDataToStructure(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            PERFORMANCE_COMMON temp = (PERFORMANCE_COMMON)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PERFORMANCE_COMMON));
            handle.Free();
            performanceCommonData = temp;
        }

        // Structure -> Data Array
        public override byte[] ToByteArray()
        {
            int rawsize = Marshal.SizeOf(performanceCommonData);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(performanceCommonData, buffer, false);
            byte[] rawdata = new byte[rawsize];
            Marshal.Copy(buffer, rawdata, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdata;
        }

        // Send
        public override void SendData(IPerformanceMIDIInOutInterface commander)
        {
            commander.SendData(segmentAddress, ToByteArray());
        }

        // Request
        public override void RequestData(IPerformanceMIDIInOutInterface commander)
        {
            //requested = true;
            commander.RequestData(segmentAddress, Length);
        }
    }

    // -------------------------------------------------  Performance Part Data Class  ---------------------------------------------------------------
    class PerformancePartClass : DataSegmentClass
    {
        // Structure
        PERFORMANCE_PART performancePartData;

        // Structure Length
        public override uint Length
        {
            get { return (uint)Marshal.SizeOf(performancePartData); }
        }

        // Constructor
        public PerformancePartClass(uint segAddr) : base(segAddr)
        {
            performancePartData = new PERFORMANCE_PART();
        }

        // Data Array -> Structure
        public void CopyDataToStructure(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            PERFORMANCE_PART temp = (PERFORMANCE_PART)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PERFORMANCE_PART));
            handle.Free();
            performancePartData = temp;
        }

        // Structure -> Data Array
        public override byte[] ToByteArray()
        {
            int rawsize = Marshal.SizeOf(performancePartData);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(performancePartData, buffer, false);
            byte[] rawdata = new byte[rawsize];
            Marshal.Copy(buffer, rawdata, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdata;
        }

        // Send
        public override void SendData(IPerformanceMIDIInOutInterface commander)
        {
            commander.SendData(segmentAddress, ToByteArray());
        }

        // Request
        public override void RequestData(IPerformanceMIDIInOutInterface commander)
        {
            //requested = true;
            commander.RequestData(segmentAddress, Length);
        }
    }
}
