using System;
using System.Collections.Generic;
using SynthLiveMidiController.MIDIMessages;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    // *******************************************************************************************************************************************************
    // **********************************************************  ALL PERFORMANCE DATA CLASS  ***************************************************************
    class RolandXP50Performance 
    {
        //----------------------------------------  DATA  -----------------------------------------------------
        private readonly PerformanceCommonClass performanceCommon;          // Performance common structure
        private readonly List<PerformancePartClass> performancePartList;    // Performance part list [16]
        private readonly uint performanceAddress;                           // Main performance address
        private readonly IPerformanceMIDIInOutInterface commander;          // Command Collection

        // Constructor
        public RolandXP50Performance(uint perfAddr, IPerformanceMIDIInOutInterface cmd)                         // Constructor
        {
            performanceAddress = perfAddr;
            commander = cmd;

            performanceCommon = new PerformanceCommonClass(performanceAddress);
            performancePartList = new List<PerformancePartClass>();
            for (int i = 0; i < RolandXP50Constants.MIDIChannelCount; i++)
            {
                performancePartList.Add(new PerformancePartClass(performanceAddress + 0x1000u + 0x100u * (uint)i, i));
            }
        }

        // ===================================================  PARAMETERS SECTION  =============================================================
        // Request Parameter from Performance Common Segment
        public void RequestParameters(
            EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS>> eHandler, 
            PERFORMANCE_COMMON_PARAMETERS[] prs)
        {
            performanceCommon.RequestCallback(eHandler, prs);
        }

        // Request Parameter from Performance Part Segment
        public void RequestParameters(
            EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS>> eHandler,
            PERFORMANCE_PART_PARAMETERS[] prs)
        {
            EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS>> id;
            for (int i = 0; i < RolandXP50Constants.MIDIChannelCount; i++)
            {
                performancePartList[i].RequestCallback(eHandler, prs);
            }
        }

        // Set Parameter into Performance Common Segment
        public void SetParameter(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS>> id, PERFORMANCE_COMMON_PARAMETERS parameter, byte[] value)
        {
            performanceCommon.SetParameter(id, parameter, value, commander);
        }

        // Set Parameter into Performance Part Segment
        public void SetParameter(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS>> id, PERFORMANCE_PART_PARAMETERS parameter, int channel, byte[] value)
        {
            performancePartList[channel].SetParameter(id, parameter, value, commander);
        }

        // Detect target of message AND Copy data to structure
        public int DetectTarget(SystemExclusiveBaseClass msg)
        {
            int result = -1;

            uint addr = msg.GetAddressFromArray();
            if ((addr >= performanceCommon.SegmentAddress) && (addr < performanceCommon.SegmentAddress + performanceCommon.Length))
            {
                byte[] buf = performanceCommon.ToByteArray();
                uint off = msg.GetAddressFromArray() - performanceCommon.SegmentAddress;

                SetDataInBuffer(buf, off, msg.GetDataFromArray());

                performanceCommon.CopyDataToStructure(buf);

                result = 16;
            }
            else
            {
                for (int i = 0; i < performancePartList.Count; i++)
                {
                    if ((addr >= performancePartList[i].SegmentAddress) && (addr < performancePartList[i].SegmentAddress + performancePartList[i].Length))
                    {
                        byte[] buf = performancePartList[i].ToByteArray();
                        uint off = msg.GetAddressFromArray() - performancePartList[i].SegmentAddress;

                        SetDataInBuffer(buf, off, msg.GetDataFromArray());

                        performancePartList[i].CopyDataToStructure(buf);

                        result = i;
                        break;
                    }
                }
            }

            return result;  // 16 - Performance Common, 0-15 - Performance Part (1-16), -1 - no result;
        }

        // Set Data in Buffer
        private void SetDataInBuffer(byte[] buffer, uint offset, byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                buffer[offset + i] = data[i];
            }
        }

        //|                                       *********  PERFORMANCE STRUCTURE <-> ROLAND  *********                                        |
        //======================================================================================================================================|
     
        //-----------------------------------------------------------  BY SEGMENTS  ------------------------------------------------------------|
        // MIDI Commander: Send Performance Common -> Roland
        public void SendPerformanceCommon()
        {
            performanceCommon.SendData(commander);
        }

        // MIDI Commander: Send Performance Part #cannel -> Roland
        public void SendPerformancePart(int channel)
        {
            performancePartList[channel].SendData(commander);
        }

        // MIDI Commander: Request Performance Common (Roland -> Performance Common Structure)
        public void RequestPerformanceCommon()
        {
            performanceCommon.RequestData(commander);
        }

        // MIDI Commander: Request Performance Part (Roland -> Performance Part #channel Structure)
        public void RequestPerformancePart(int channel)
        {
            performancePartList[channel].RequestData(commander);
        }

        //------------------------------------------------------  ALL PERFORMANCE DATA  --------------------------------------------------------|
        // MIDI Commander: Send All Performace Data (Performance Structure -> Roland)
        public void SendPerformance()
        {
            SendPerformanceCommon();
            for (int i = 0; i < RolandXP50Constants.MIDIChannelCount; i++)
            {
                SendPerformancePart(i);
            }
        }

        // MIDI Commander: Request All Performance Data (Roland -> Performance Structure)
        public void RequestPerformance()
        {
            RequestPerformanceCommon();
            for (int i = 0; i < RolandXP50Constants.MIDIChannelCount; i++)
            {
                RequestPerformancePart(i);
            }
        }

        //----------------------------------------------------------  SONG SECTION  ------------------------------------------------------------|
        // MIDI Commander: Send Song Data (Performance Structure -> Roland)
        public void SendSongData()
        {
            SendPerformanceCommon();
            for (int i = 0; i < RolandXP50Constants.SongChannelCount; i++)
            {
                SendPerformancePart(i);
            }
        }

        // MIDI Commander: Request Song Data (Roland -> Performance Structure)
        public void RequestSongData()
        {
            RequestPerformanceCommon();
            for (int i = 0; i < RolandXP50Constants.SongChannelCount; i++)
            {
                RequestPerformancePart(i);
            }
        }

        //----------------------------------------------------------  FAST SECTION  ------------------------------------------------------------|
        // MIDI Commander: Send Fast Data (Performance Structure -> Roland)
        public void SendFastData()
        {
            for (int i = RolandXP50Constants.SongChannelCount; i < RolandXP50Constants.FastChannelCount; i++)
            {
                SendPerformancePart(i);
            }
        }

        // MIDI Commander: Request Fast Data (Roland -> Performance Structure)
        public void RequestFastData()
        {
            for (int i = RolandXP50Constants.SongChannelCount; i < RolandXP50Constants.FastChannelCount; i++)
            {
                RequestPerformancePart(i);
            }
        }

        //|                                    *********  PERFORMANCE STRUCTURE <-> STORAGE MODULE  *********                                   |
        //======================================================================================================================================|

        //-----------------------------------------------------------  BY SEGMENTS  ------------------------------------------------------------|
        // Set Performance Common
        private void SetPerformanceCommon(byte[] data)
        {
            performanceCommon.CopyDataToStructure(data);
        }

        // Set Performance Part #channel
        private void SetPerformancePart(byte[] data, int channel)
        {
            performancePartList[channel].CopyDataToStructure(data);
        }

        // Get Performance Common
        private byte[] GetPerformanceCommon()
        {
            return performanceCommon.ToByteArray();
        }

        // Get Performance Part #channel
        private byte[] GetPerformancePart(int channel)
        {
            //while (performanceCommon.Requested) { };
            return performancePartList[channel].ToByteArray();
        }

        //----------------------------------------------------------  SONG SECTION  ------------------------------------------------------------|
        // Set Song Segment Data
        public void SetSongData(byte[] data)
        {
            int index = 0;

            byte[] perfData = new byte[performanceCommon.Length];
            Array.Copy(data, index, perfData, 0, performanceCommon.Length);
            SetPerformanceCommon(perfData);

            index += (int)performanceCommon.Length;

            for (int i = 0; i < RolandXP50Constants.SongChannelCount; i++)
            {
                byte[] partData = new byte[performancePartList[i].Length];
                Array.Copy(data, index, partData, 0, performancePartList[i].Length);
                SetPerformancePart(partData, i);

                index += (int)performancePartList[i].Length;
            }
        }

        // Get Song Segment Data
        public byte[] GetSongData()
        {
            byte[] buffer = new byte[performanceCommon.Length + performancePartList[0].Length * RolandXP50Constants.SongChannelCount];
            int index = 0;

            Array.Copy(GetPerformanceCommon(), 0, buffer, index, performanceCommon.Length);
            index += (int)performanceCommon.Length;

            for (int i = 0; i < RolandXP50Constants.SongChannelCount; i++)
            {
                Array.Copy(GetPerformancePart(i), 0, buffer, index, performancePartList[i].Length);
                index += (int)performancePartList[i].Length;
            }

            return buffer;
        }

        //----------------------------------------------------------  FAST SECTION  ------------------------------------------------------------|
        // Set Fast Segment Data
        public void SetFastData(byte[] data)
        {
            int index = 0;

            for (int i = RolandXP50Constants.SongChannelCount; i < RolandXP50Constants.MIDIChannelCount; i++)
            {
                byte[] partData = new byte[performancePartList[i].Length];
                Array.Copy(data, index, partData, 0, performancePartList[i].Length);
                SetPerformancePart(partData, i);

                index += (int)performancePartList[i].Length;
            }
        }

        // Get Fast Segment Data
        public byte[] GetFastData()
        {
            byte[] buffer = new byte[performancePartList[0].Length * (RolandXP50Constants.MIDIChannelCount - RolandXP50Constants.SongChannelCount)];
            int index = 0;

            for (int i = RolandXP50Constants.SongChannelCount; i < RolandXP50Constants.MIDIChannelCount; i++)
            {
                Array.Copy(GetPerformancePart(i), 0, buffer, index, performancePartList[i].Length);
                index += (int)performancePartList[i].Length;
            }

            return buffer;
        }
    }

    //==========================================================  SEGMENT DATA EVENT  =====================================================+====|
    public class SegmentDataReceivedEvendArgs : EventArgs
    {
        public int segment = 0;

        public SegmentDataReceivedEvendArgs(int num)
        {
            segment = num;
        }
    }
    public delegate void SegmentDataReceivedHandler(object sender, SegmentDataReceivedEvendArgs ea);
}
