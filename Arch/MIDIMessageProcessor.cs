using System;
using System.Collections.Generic;
using System.Threading;
using SynthLiveMidiController.MIDIDevice;
using SynthLiveMidiController.MIDIEvents;

namespace SynthLiveMidiController.MIDIMessages
{
    class MIDIMessageProcessor
    {
        private readonly Queue<MIDIMessageBase> messageQueue = new Queue<MIDIMessageBase>();    // Message Queue
        private readonly Thread backgroundProcess;                                              // Message processing thread
        private bool stopBackground = false;                                                    // Stop message processing
        private readonly IMidiInOutInterface extDevice;                                         // MIDI in/out device (Sanford)
        private bool requestSending = false;                                                    // Request data have sent
        private uint lastAddress = uint.MaxValue;                                               // Address of requested data

        public event SysExHandler OnSysExRquestedDataEvent;
        public event SysExHandler OnSysExEditDataEvent;
        public event ChannelHandler OnChannelEvent;

        // Create process
        public MIDIMessageProcessor(IMidiInOutInterface device)
        {
            extDevice = device;
            extDevice.SysExEvent += OnDevice_SysExEvent;
            extDevice.ChannelEvent += OnDevice_ChannelEvent;
            backgroundProcess = new Thread(new ThreadStart(MessagesProcessing));
            backgroundProcess.Start();
        }

        // Stop process
        public void StopProcess()
        {
            stopBackground = true;
            backgroundProcess.Join(1000);
            if (backgroundProcess.ThreadState != ThreadState.Stopped)
            {
                backgroundProcess.Abort();
                do
                {

                } while (backgroundProcess.ThreadState != ThreadState.Aborted);
            }
        }

        // =================================================  CALLBACKS  ===========================================================

        // OnChannelEvent
        private void OnDevice_ChannelEvent(object sender, ChannelEventArgs e)
        {
            // ...............................  Channel message received  ..............................
            OnChannelEvent?.Invoke(sender, e);
        }

        // OnSysExEvent 
        private void OnDevice_SysExEvent(object sender, SysExEventArgs e)
        {
            SystemExclusiveBaseClass msg = new SystemExclusiveBaseClass(e.Buffer);
            if (lastAddress == msg.GetAddressFromArray())
            {
                // ......................................  Requested data recieved  .....................................
                requestSending = false;
                lastAddress = uint.MaxValue;

                OnSysExRquestedDataEvent?.Invoke(sender, e);
            }
            else
            {
                // .....................................  Edit SusEx Message  ...........................................
                OnSysExEditDataEvent?.Invoke(sender, e);
            }
        }

        // ===========================================  Message Queue processing  ===================================================

        // Put Message to Queue
        public void SendMessage(MIDIMessageBase message)
        {
            // Put message parameters queue
            messageQueue.Enqueue(message);
        }

        // Message LOOP
        private void MessagesProcessing()
        {
            do
            {
                if ((messageQueue.Count > 0) && (!requestSending))
                {
                    MIDIMessageBase message = messageQueue.Dequeue();
                    if (message.GetType() == typeof(ChannelMessageClass))
                    {
                        ChannelMessageClass msg = (ChannelMessageClass)message;
                        if ((msg.Command == ChannelCommand.ChannelPressure) ||(msg.Command == ChannelCommand.ProgramChange))
                        {
                            SendShortChannelMessage(msg);
                        }
                        else
                        {
                            SendChannelMessage(msg);
                        }
                    }
                    else if (message.GetType() == typeof(SystemExclusiveDT1Class))
                    {
                        SystemExclusiveDT1Class msg = (SystemExclusiveDT1Class)message;
                        SendSysExDT1Message(msg);
                    }
                    else if (message.GetType() == typeof(SystemExclusiveRequestClass))
                    {
                        SystemExclusiveRequestClass msg = (SystemExclusiveRequestClass)message;
                        SendSysExRequestMessage(msg);
                    }
                    else
                    {
                        // Unknown
                    }
                }
            }
            while (!stopBackground);
        }

        //=================================  Pause depends of message length plus 20 ms  =================================================
        private void Pause(int count)
        {
            int res = count * 320 / 1000 + 20;
            Thread.Sleep(res);
        }

        //================================================  SEND MESSAGES FROM QUEUE =====================================================

        // SysEx Data Tranmit
        private void SendSysExDT1Message(SystemExclusiveDT1Class msg)
        {
            extDevice.SendSysExMessage(msg.GetBuffer());
            Pause(msg.GetBuffer().Length);  // Pause depends of message length
        }

        // SysEx Data Request
        private void SendSysExRequestMessage(SystemExclusiveRequestClass msg)
        {
            requestSending = true;
            lastAddress = msg.GetAddressFromArray();
            extDevice.SendSysExMessage(msg.GetBuffer());
            Pause(msg.GetBuffer().Length);  // Pause depends of message length
        }

        // Shot Channel Message
        private void SendShortChannelMessage(ChannelMessageClass msg)
        {
            extDevice.SendChannelMessage(msg.Command, msg.Channel, msg.Data1);
        }

        // ChannelMessage
        private void SendChannelMessage(ChannelMessageClass msg)
        {
            extDevice.SendChannelMessage(msg.Command, msg.Channel, msg.Data1, msg.Data2);
        }
    }
}

