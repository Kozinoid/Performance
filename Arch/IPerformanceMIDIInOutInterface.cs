using SynthLiveMidiController.MIDIEvents;

namespace SynthLiveMidiController.MIDIMessages
{
    interface IPerformanceMIDIInOutInterface
    {
        event SysExHandler OnSysExRquestedDataEvent;
        event SysExHandler OnSysExEditDataEvent;
        event ChannelHandler OnChannelEvent;

        void SendData(uint addr, byte[] data);
        void RequestData(uint addr, uint len);
    }
}
