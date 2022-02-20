/*
    Classes for working with MIDI events
 */

namespace SynthLiveMidiController.MIDIEvents
{
    delegate void ChannelHandler(object sender, ChannelEventArgs e);
    delegate void SysExHandler(object sender, SysExEventArgs e);

    // Channel Commands according to Sanford.ChannelCommand enum
    public enum ChannelCommand
    {
        ChannelPressure = 208,
        Controller = 176,
        NoteOff = 128,
        NoteOn = 144,
        PitchWheel = 224,
        PolyPressure = 160,
        ProgramChange = 192,
    }

    // System Exclusive Message Event Arguments
    public class SysExEventArgs
    {
        private readonly byte[] buffer;

        public byte[] Buffer { get { return buffer; } }

        public SysExEventArgs(byte[] buf)
        {
            buffer = buf;
        }
    }

    // Channel Message Event Arguments
    public class ChannelEventArgs
    {
        private readonly ChannelCommand command;
        private readonly int midiChannel;
        private readonly int data1;
        private readonly int data2;

        public ChannelCommand Command { get { return command; } }
        public int MidiChannel { get { return midiChannel; } }
        public int Data1 { get { return data1; } }
        public int Data2 { get { return data2; } }

        public ChannelEventArgs(ChannelCommand com, int chan, int d1, int d2)
        {
            command = com;
            midiChannel = chan;
            data1 = d1;
            data2 = d2;
        }
    }
}
