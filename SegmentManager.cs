using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynthLiveMidiController.MIDIMessages;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    //--------------------------------  All Fields MAnager  -----------------------------------
    class SegmentManager
    {
        public event SegmentDataReceivedHandler SongDataReceived;
        public event SegmentDataReceivedHandler FastDataReceived;

        //----------------------------------------  DATA  -----------------------------------------------------
        RolandXP50Performance performance;
        RolandXP50Commands commands;
        private readonly IPerformanceMIDIInOutInterface commander;          // Command Collection

        // Constructor
        public SegmentManager(IPerformanceMIDIInOutInterface cmd)
        {
            commander = cmd;

            performance = new RolandXP50Performance(RolandXP50Constants.TemporaryPerformanceAddress, commander);
            commands = new RolandXP50Commands(commander);

            commander.OnChannelEvent += Commander_OnChannelEvent;
            commander.OnSysExEditDataEvent += Commander_OnSysExEditDataEvent;
            commander.OnSysExRquestedDataEvent += Commander_OnSysExRquestedDataEvent;
        }

        //|                                                        CALLBACK SECTION                                                             |
        //=======================================================================================================================================

        // ----  Variables for last received data storing  ----
        int msb = -1;       // Most significant byte
        int lsb = -1;       // Least significant byte
        int patch = -1;     // Patch number

        // Channel message received
        private void Commander_OnChannelEvent(object sender, MIDIEvents.ChannelEventArgs e)
        {
            switch (e.Command)
            {
                case MIDIEvents.ChannelCommand.Controller:
                    if (e.Data1 == 0x00)
                    {
                        msb = e.Data2;
                    }
                    else if (e.Data1 == 0x20)
                    {
                        lsb = e.Data2;
                    }
                    else
                    {
                        // _____________________________  Other Control change  _________________________________
                    }
                    break;

                case MIDIEvents.ChannelCommand.ProgramChange:
                    patch = e.Data1;
                    //string name = BankNameConvertor.GetPatchName(BankNameConvertor.ChannelCommandToBuffer(msb, lsb, patch));
                    //Console.WriteLine(name);
                    break;

                case MIDIEvents.ChannelCommand.NoteOn:
                    break;

                case MIDIEvents.ChannelCommand.NoteOff:
                    break;

                case MIDIEvents.ChannelCommand.PitchWheel:
                    break;

                case MIDIEvents.ChannelCommand.PolyPressure:
                    break;

                case MIDIEvents.ChannelCommand.ChannelPressure:
                    break;
            }
        }

        // System exclusive: Requested data received
        private void Commander_OnSysExRquestedDataEvent(object sender, MIDIEvents.SysExEventArgs e)
        {
            SysExProcessing(sender, e);
        }

        // System exclusive: Data for edit received
        private void Commander_OnSysExEditDataEvent(object sender, MIDIEvents.SysExEventArgs e)
        {
            SysExProcessing(sender, e);
        }

        // SysExProcessing
        private void SysExProcessing(object sender, MIDIEvents.SysExEventArgs e)
        {
            SystemExclusiveBaseClass msg = new SystemExclusiveBaseClass(e.Buffer);
            int target = performance.DetectTarget(msg);
            if ((target >= 0) && (target < RolandXP50Constants.SongChannelCount) || (target == 16)) SongDataReceived?.Invoke(sender, new SegmentDataReceivedEvendArgs(target));
            else if ((target >= RolandXP50Constants.SongChannelCount) && (target < RolandXP50Constants.SongChannelCount + RolandXP50Constants.FastChannelCount)) FastDataReceived?.Invoke(sender, new SegmentDataReceivedEvendArgs(target));
            else
            {
                // Other Segment
                //Console.WriteLine("Other segment");
            }
        }

    }
}
