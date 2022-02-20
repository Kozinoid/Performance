/*
    lass for working with the Sanford library
 */

using System;
using Sanford.Multimedia;
using SanfordMIDI =  Sanford.Multimedia.Midi;
using System.Windows.Forms;

namespace SynthLiveMidiController.MIDIDevice
{
    class SanfordMidiDevice : IMidiInOutInterface
    {
        //-----------------------------------------------------------------------------------------------------
        //|                                          DEVICES FIELDS                                           |
        //-----------------------------------------------------------------------------------------------------

        private static SanfordMIDI.InputDevice inDevice = null;
        private static SanfordMIDI.OutputDevice opDevice = null;

        //-----------------------------------------------------------------------------------------------------
        //|                                              EVENTS                                               |
        //-----------------------------------------------------------------------------------------------------

        event MIDIEvents.ChannelHandler ChannelEvent;
        event MIDIEvents.SysExHandler SysExEvent;
        event MIDIEvents.ChannelHandler IMidiInOutInterface.ChannelEvent
        {
            add
            {
                ChannelEvent += value;
            }

            remove
            {
                ChannelEvent -= value; ;
            }
        }
        event MIDIEvents.SysExHandler IMidiInOutInterface.SysExEvent
        {
            add
            {
                SysExEvent += value; ;
            }

            remove
            {
                SysExEvent -= value; ;
            }
        }

        //-----------------------------------------------------------------------------------------------------
        //|                               INPUT/OUTPUT DEVICES INIT/CLOSE                                     |
        //-----------------------------------------------------------------------------------------------------

        // Init In/Out Devices
        public bool InitDevices(int inIndex, int outIndex)
        {
            try
            {
                inDevice = new SanfordMIDI.InputDevice(inIndex);

                inDevice.ChannelMessageReceived += HandleChannelMessageReceived;
                inDevice.SysExMessageReceived += HandleSysExMessageReceived;
                inDevice.Error += new EventHandler<ErrorEventArgs>(InDevice_Error);
                inDevice.StartRecording();
            }
            catch
            {
                MessageBox.Show(String.Format("Create input device #{0} error", inIndex));
                return false;
            }

            try
            {
                opDevice = new SanfordMIDI.OutputDevice(outIndex);
            }
            catch
            {
                MessageBox.Show(String.Format("Create output device #{0} error", outIndex));
                return false;
            }

            return true;
        }

        // Close In/Out Devices
        public void CloseDevices()
        {
            if (opDevice != null)
            {
                try
                {
                    opDevice.Close();
                }
                catch
                {
                    MessageBox.Show(String.Format("Closing output device #{0} error", opDevice.ToString()));
                }
            }

            if (inDevice != null)
            {
                try
                {
                    inDevice.StopRecording();
                    inDevice.Reset();
                    inDevice.ChannelMessageReceived -= HandleChannelMessageReceived;
                    inDevice.SysExMessageReceived -= HandleSysExMessageReceived;
                    inDevice.Error -= new EventHandler<ErrorEventArgs>(InDevice_Error);
                    inDevice.Close();
                }
                catch
                {
                    MessageBox.Show(String.Format("Closing input device #{0} error", inDevice.ToString()));
                }
                
            }
        }

        //-----------------------------------------------------------------------------------------------------
        //|                                            CALLBACKS                                              |
        //-----------------------------------------------------------------------------------------------------

        // Channel message received
        private void HandleChannelMessageReceived(object sender, SanfordMIDI.ChannelMessageEventArgs e)
        {
            MIDIEvents.ChannelEventArgs ea = new MIDIEvents.ChannelEventArgs(GetEventChannelCommand(e.Message.Command), e.Message.MidiChannel, e.Message.Data1, e.Message.Data2);
            ChannelEvent?.Invoke(this, ea);
        }

        // System exclusive message received
        private void HandleSysExMessageReceived(object sender, SanfordMIDI.SysExMessageEventArgs e)
        {
            MIDIEvents.SysExEventArgs ea = new MIDIEvents.SysExEventArgs(e.Message.GetBytes());
            SysExEvent?.Invoke(this, ea);
        }

        // Error handler
        private void InDevice_Error(object sender, ErrorEventArgs e)
        {
            // Handle error
        }

        //----------------------------------------------------------------------------------------------------
        //|                                          OUTPUT COMMANDS                                         |
        //----------------------------------------------------------------------------------------------------

        // Send SysEx Message
        public void SendSysExMessage(byte[] buf)
        {
            SanfordMIDI.SysExMessage message = new SanfordMIDI.SysExMessage(buf);
            opDevice.Send(message);
        }

        // Send Channel Message 1 
        public void SendChannelMessage(MIDIEvents.ChannelCommand command, int midiChannel, int data1)
        {
            SanfordMIDI.ChannelCommand cmd = GetChannelCommand(command);
            SanfordMIDI.ChannelMessage message = new SanfordMIDI.ChannelMessage(cmd, midiChannel, data1);
            opDevice.Send(message);
        }

        // Send Channel Message 2 
        public void SendChannelMessage(MIDIEvents.ChannelCommand command, int midiChannel, int data1, int data2)
        {
            SanfordMIDI.ChannelCommand cmd = GetChannelCommand(command);
            SanfordMIDI.ChannelMessage message = new SanfordMIDI.ChannelMessage(cmd, midiChannel, data1, data2);
            opDevice.Send(message);
        }

        // Command Converter App -> Sanford
        private SanfordMIDI.ChannelCommand GetChannelCommand(MIDIEvents.ChannelCommand cmd)
        {
            switch (cmd)
            {
                case MIDIEvents.ChannelCommand.ChannelPressure:
                    return SanfordMIDI.ChannelCommand.ChannelPressure;

                case MIDIEvents.ChannelCommand.Controller:
                    return SanfordMIDI.ChannelCommand.Controller;

                case MIDIEvents.ChannelCommand.NoteOff:
                    return SanfordMIDI.ChannelCommand.NoteOff;

                case MIDIEvents.ChannelCommand.NoteOn:
                    return SanfordMIDI.ChannelCommand.NoteOn;
;
                case MIDIEvents.ChannelCommand.PitchWheel:
                    return SanfordMIDI.ChannelCommand.PitchWheel;

                case MIDIEvents.ChannelCommand.ProgramChange:
                    return SanfordMIDI.ChannelCommand.ProgramChange;

                default:
                    return SanfordMIDI.ChannelCommand.PolyPressure;
            }
        }

        // Command Converter Sanford -> App
        private MIDIEvents.ChannelCommand GetEventChannelCommand(SanfordMIDI.ChannelCommand cmd)
        {
            switch (cmd)
            {
                case SanfordMIDI.ChannelCommand.ChannelPressure:
                    return MIDIEvents.ChannelCommand.ChannelPressure;

                case SanfordMIDI.ChannelCommand.Controller:
                    return MIDIEvents.ChannelCommand.Controller;

                case SanfordMIDI.ChannelCommand.NoteOff:
                    return MIDIEvents.ChannelCommand.NoteOff;

                case SanfordMIDI.ChannelCommand.NoteOn:
                    return MIDIEvents.ChannelCommand.NoteOn;
                    ;
                case SanfordMIDI.ChannelCommand.PitchWheel:
                    return MIDIEvents.ChannelCommand.PitchWheel;

                case SanfordMIDI.ChannelCommand.ProgramChange:
                    return MIDIEvents.ChannelCommand.ProgramChange;

                default:
                    return MIDIEvents.ChannelCommand.PolyPressure;
            }
        }
        
    }
}
