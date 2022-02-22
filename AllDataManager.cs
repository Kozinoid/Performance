using System;
using System.Collections.Generic;
using SynthLiveMidiController.MIDIMessages;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    //--------------------------------------  All Fields MAnager  ---------------------------------------------
    class AllDataManager : IParametersManager
    {
        // Modified Event
        //public event ParametersModifiedEventHandler OnParameterModified = null;

        //----------------------------------------  DATA  -----------------------------------------------------
        private RolandXP50Performance performance;
        //RolandXP50Commands commands;
        private readonly IPerformanceMIDIInOutInterface commander;          // Command Collection

        // Constructor
        public AllDataManager(IPerformanceMIDIInOutInterface cmd)
        {
            commander = cmd;

            performance = new RolandXP50Performance(RolandXP50Constants.TemporaryPerformanceAddress, commander);
            //commands = new RolandXP50Commands(commander);

            commander.OnChannelEvent += Commander_OnChannelEvent;
            commander.OnSysExEditDataEvent += Commander_OnSysExEditDataEvent;
            commander.OnSysExRquestedDataEvent += Commander_OnSysExRquestedDataEvent;
        }

        //=======================================================================================================================================
        public void RequestAllPerformanceData()
        {
            performance.RequestPerformance();
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
        }

        // ====================================================  INTERFACE SECTION  =============================================================
        public void RequestParameters(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS>> eHandler, PERFORMANCE_COMMON_PARAMETERS[] parameters)
        {
            performance.RequestParameters(eHandler, parameters);
        }

        public void RequestParameters(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS>> eHandler, PERFORMANCE_PART_PARAMETERS[] parameters)
        {
            performance.RequestParameters(eHandler, parameters);
        }

        public void SetParameter(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS>> id, PERFORMANCE_COMMON_PARAMETERS parameter, byte[] value)
        {
            performance.SetParameter(id, parameter, value);
        }

        public void SetParameter(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS>> id, PERFORMANCE_PART_PARAMETERS parameter, int channel, byte[] value)
        {
            performance.SetParameter(id, parameter, channel, value);
        }
    }

    //======================================================================================================================================|
    interface IParametersManager
    {
        void RequestParameters(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS>> eHandler, PERFORMANCE_COMMON_PARAMETERS[] parameters);
        void RequestParameters(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS>> eHandler, PERFORMANCE_PART_PARAMETERS[] parameters);
        void SetParameter(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS>> id, PERFORMANCE_COMMON_PARAMETERS parameter, byte[] value);
        void SetParameter(EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS>> id, PERFORMANCE_PART_PARAMETERS parameter, int channel, byte[] value);
    }
}
