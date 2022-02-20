using SynthLiveMidiController.MIDIEvents;

namespace SynthLiveMidiController.MIDIMessages
{
    class PreprocessorCommandsClass : IPerformanceMIDIInOutInterface
    {
        private readonly MIDIMessageProcessor processor = null;                              // Message queue processor
        private readonly InstrumentMIDIMessages messages;

        public event SysExHandler OnSysExRquestedDataEvent;
        public event SysExHandler OnSysExEditDataEvent;
        public event ChannelHandler OnChannelEvent;

        public PreprocessorCommandsClass(MIDIDevice.IMidiInOutInterface dev, InstrumentMIDIMessages mes)
        {
            processor = new MIDIMessageProcessor(dev);           // Create message queue processor
            messages = mes;

            processor.OnChannelEvent += Processor_OnChannelEvent;
            processor.OnSysExEditDataEvent += Processor_OnSysExEditDataEvent;
            processor.OnSysExRquestedDataEvent += Processor_OnSysExRquestedDataEvent;
        }

        public void Stop()
        {
            processor.StopProcess();                                        // Stop message queue processor
        }

        //======================================================  CALLBACKS  ==============================================================
        private void Processor_OnSysExRquestedDataEvent(object sender, SysExEventArgs e)
        {
            OnSysExRquestedDataEvent?.Invoke(sender, e);
        }

        private void Processor_OnSysExEditDataEvent(object sender, SysExEventArgs e)
        {
            OnSysExEditDataEvent?.Invoke(sender, e);
        }

        private void Processor_OnChannelEvent(object sender, ChannelEventArgs e)
        {
            OnChannelEvent?.Invoke(sender, e);
        }

        // ==============================================  INTERFACE IMPLEMENTATION  ======================================================
        // Send data to Roland XP50 memory
        public void SendData(uint addr, byte[] data)
        {
            SystemExclusiveDT1Class mes = messages.GetSystemExclusiveDT1Message(addr, data);
            processor.SendMessage(mes);
        }

        // Request data from Roland XP50 memory
        public void RequestData(uint addr, uint len)
        {
            SystemExclusiveRequestClass mes = messages.GetSystemExclusiveRequestMessage(addr, len);
            processor.SendMessage(mes);
        }
    }
}
