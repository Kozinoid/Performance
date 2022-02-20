/*
    This interface is intended to align the application with different MIDI libraries.
 */

//using MIDIEvents;

namespace SynthLiveMidiController.MIDIDevice
{
    interface IMidiInOutInterface
    {
        //-----------------------------------------------------------------------------------------------------
        //|                                              EVENTS                                               |
        //-----------------------------------------------------------------------------------------------------
        event MIDIEvents.ChannelHandler ChannelEvent;
        event MIDIEvents.SysExHandler SysExEvent;

        //-----------------------------------------------------------------------------------------------------
        //|                               INPUT/OUTPUT DEVICES INIT/CLOSE                                     |
        //-----------------------------------------------------------------------------------------------------
        bool InitDevices(int inIndex, int outIndex);
        void CloseDevices();

        //----------------------------------------------------------------------------------------------------
        //|                                          OUTPUT COMMANDS                                         |
        //----------------------------------------------------------------------------------------------------
        void SendSysExMessage(byte[] buf);
        void SendChannelMessage(MIDIEvents.ChannelCommand command, int midiChannel, int data1);
        void SendChannelMessage(MIDIEvents.ChannelCommand command, int midiChannel, int data1, int data2);
    }
}
