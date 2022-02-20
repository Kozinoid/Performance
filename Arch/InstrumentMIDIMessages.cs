namespace SynthLiveMidiController.MIDIMessages
{
    class InstrumentMIDIMessages
    {
        private readonly IInstrumentCommandInterface instrumentModel;

        public InstrumentMIDIMessages(IInstrumentCommandInterface inst)
        {
            instrumentModel = inst;
        }

        public ChannelMessageClass GetChannelMessage(MIDIEvents.ChannelCommand cmd, int chan, int d1, int d2)
        {
            return new ChannelMessageClass(cmd,  chan, d1, d2);
        }

        public SystemExclusiveDT1Class GetSystemExclusiveDT1Message(uint addr, byte[] data)
        {
            return new SystemExclusiveDT1Class(instrumentModel.Header, instrumentModel.Dt1Command, addr, data);
        }

        public SystemExclusiveRequestClass GetSystemExclusiveRequestMessage(uint addr, uint len)
        {
            return new SystemExclusiveRequestClass(instrumentModel.Header, instrumentModel.RequestCommand, addr, len);
        }
    }
}
