using System;
using System.Collections.Generic;
using SynthLiveMidiController.MIDIMessages;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    // -----------------------------------------------------  One Channel Command  --------------------------------------------------------------
    public class RolandXP50SongFastCommandClass
    {
        // Channel Local Switch field
        public LocalSwitch localSwitch = LocalSwitch.OFF;

        // Constructor
        public RolandXP50SongFastCommandClass(LocalSwitch sw)
        {
            localSwitch = sw;
        }

        // LocalSwitch -> byte[]
        public byte[] GetLocalBuffer()
        {
            byte[] buffer = new byte[1];
            buffer[0] = (byte)localSwitch;
            return buffer;
        }
    }

    // -----------------------------------------------------  One Song/Fast Command  ------------------------------------------------------------
    public class RolandXP50PerformanceCommand
    {
        // Command Name
        public string CommandName { get; set; }

        // Performance EFX Source field
        public EFXSource efxSource = EFXSource.PERFORM;

        // List of channels` Local Switch 
        public List<RolandXP50SongFastCommandClass> channels;

        // Length of serialized data buffer
        public int BufferLength
        {
            get { return channels.Count + 1; }
        }

        // Indexator
        public RolandXP50SongFastCommandClass this[int chIndex]
        {
            get { return channels[chIndex]; }
        }

        // Constructor
        public RolandXP50PerformanceCommand(int chCount, EFXSource efx)
        {
            efxSource = efx;
            channels = new List<RolandXP50SongFastCommandClass>();
            for (int i = 0; i < chCount; i++)
            {
                channels.Add(new RolandXP50SongFastCommandClass(LocalSwitch.OFF));
            }
        }

        // EFXSource -> byte[]
        public byte[] GetEFXBuffer()
        {
            byte[] buffer = new byte[1];
            buffer[0] = (byte)efxSource;
            return buffer;
        }

        // Song/Fast Command -> byte[]{LocalSwitch1, LocalSwitch2 ... LocalSwitchChanCount, EFXSource}
        public byte[] ToByteArray()
        {
            byte[] buffer = new byte[BufferLength];

            for (int i = 0; i < channels.Count; i++)
            {
                buffer[i] = (byte)channels[i].localSwitch;
            }
            buffer[channels.Count] = (byte)efxSource;

            return buffer;
        }

        // byte[]{LocalSwitch1, LocalSwitch2 ... LocalSwitchChanCount, EFXSource} => Song/Fast Command
        public void FromByteArray(byte[] data)
        {
            for (int i = 0; i < channels.Count; i++)
            {
                channels[i].localSwitch = (LocalSwitch)data[i];
            }
            efxSource = (EFXSource)data[channels.Count];
        }
    }

    // -----------------------------------------------------  Command Set Base Class  -----------------------------------------------------------
    abstract class RolandXP50CommandSet
    {
        // ----------------------------------------------  Static fields  -------------------------------------------
        public static int SongCommandCount = 10;
        public static int FastCommandCount = 9;
        public static uint TemporaryEFXSourceAddress = RolandXP50Constants.TemporaryPerformanceAddress + 0x0C;
        public static uint[] TemporaryLocalSwitchAddresses = {
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1014u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1114u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1214u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1314u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1414u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1514u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1614u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1714u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1814u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1914u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1A14u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1B14u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1C14u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1D14u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1E14u,
                RolandXP50Constants.TemporaryPerformanceAddress + 0x1F14u,
            };
        protected IPerformanceMIDIInOutInterface commander;

        // Song/Fast Preset Name
        public string PresetName { get; set; }

        // Song or Fast commands List
        protected List<RolandXP50PerformanceCommand> commandList;

        // Length of serialized data buffer
        protected abstract int Lenght{ get; }

        // Indexator
        public RolandXP50PerformanceCommand this[int comIndex]
        {
            get { return commandList[comIndex]; }
        }

        // Constructor
        internal RolandXP50CommandSet(IPerformanceMIDIInOutInterface cmd, int commandCount, int channelCount)
        {
            commander = cmd;
            commandList = new List<RolandXP50PerformanceCommand>(commandCount);
            for (int i = 0; i < commandCount; i++)
            {
                commandList.Add(new RolandXP50PerformanceCommand(channelCount, EFXSource.PERFORM));
            }
        }

        //Send Command
        public abstract void SendCommand(int comNumber);

        // To Byte Array
        public byte[] ToByteArray()
        {
            byte[] buffer = new byte[Lenght];

            int index = 0;

            for (int i = 0; i < commandList.Count; i++)
            {
                byte[] sourceBuf = commandList[i].ToByteArray();
                Array.Copy(sourceBuf, 0, buffer, index, sourceBuf.Length);
                index += sourceBuf.Length;
            }

            return buffer;
        }

        // From Byte Array
        public void FromByteArray(byte[] data)
        {
            int index = 0;

            for (int i = 0; i < commandList.Count; i++)
            {
                byte[] buf = new byte[commandList[i].channels.Count + 1];
                Array.Copy(data, index, buf, 0, commandList[i].BufferLength);
                commandList[i].FromByteArray(buf);
                index += commandList[i].BufferLength;
            }
        }

        // Get Command Name
        public string GetCommandName(int comNumber)
        {
            return commandList[comNumber].CommandName;
        }

        // Set Command Name
        public void SetCommandName(int comNumber, string name)
        {
            commandList[comNumber].CommandName = name;
        }

        // Get Command EFX Source
        public EFXSource GetCommandEFXSource(int comNumber)
        {
            return commandList[comNumber].efxSource;
        }

        // Set Command EFX Source
        public void SetCommandEFXSource(int comNumber, EFXSource src)
        {
            commandList[comNumber].efxSource = src;
        }

        // Get Command Local Switch
        public LocalSwitch GetCommandLocalSwitch(int comNumber, int channel)
        {
            return commandList[comNumber][channel].localSwitch;
        }

        // Set Command Local Switch
        public void SetCommandLocalSwitch(int comNumber, int channel, LocalSwitch lcSw)
        {
            commandList[comNumber][channel].localSwitch = lcSw; ;
        }
    }

    // --------------------------------------------------------  Song Command Set  --------------------------------------------------------------
    class RolandXP50SongCommandSet : RolandXP50CommandSet
    {
        // Singer
        public string Singer { get; set; }

        // Key
        public string Key { get; set; }

        // Serialized data buffer length
        protected override int Lenght
        {
            get { return SongCommandCount * (RolandXP50Constants.SongChannelCount + 1); }
        }

        // Constructor
        public RolandXP50SongCommandSet(IPerformanceMIDIInOutInterface cmd) : base(cmd, SongCommandCount, RolandXP50Constants.SongChannelCount) 
        {
            // Initialization
            PresetName = "New Song Name";
            Singer = "New Singer";
            Key = "New Key";

            for (int i = 0; i < SongCommandCount; i++)
            {
                commandList[i].CommandName = string.Format("SongCommand {0}", i);
            }
        }

        // Send Song Command
        public override void SendCommand(int comNumber)
        {
            if ((comNumber >= 0) && (comNumber < commandList.Count))
            {
                // Send Command to commander
                uint efxAddr = TemporaryEFXSourceAddress;
                commander.SendData(efxAddr, commandList[comNumber].GetEFXBuffer());
                for (int i = 0; i < RolandXP50Constants.MIDIChannelCount; i++)
                {
                    uint addr = TemporaryLocalSwitchAddresses[i];
                    if (i < RolandXP50Constants.SongChannelCount)
                    {
                        commander.SendData(addr, commandList[comNumber].channels[i].GetLocalBuffer());
                    }
                    else
                    {
                        commander.SendData(addr, new byte[] {(byte)LocalSwitch.OFF});
                    }
                }

            }
        }
    }

    // ---------------------------------------------------------  Fast Command Set  -------------------------------------------------------------
    class RolandXP50FastCommandSet : RolandXP50CommandSet
    {
        // Serialized data buffer length
        protected override int Lenght
        {
            get { return FastCommandCount * (RolandXP50Constants.FastChannelCount + 1); }
        }

        // Constructor
        public RolandXP50FastCommandSet(IPerformanceMIDIInOutInterface cmd) : base(cmd, FastCommandCount, RolandXP50Constants.FastChannelCount) 
        {
            // Initialization
            PresetName = "New Fast Preset";

            for (int i = 0; i < FastCommandCount; i++)
            {
                commandList[i].CommandName = string.Format("FastCommand {0}", i);
            }
        }

        // Send Fast Command
        public override void SendCommand(int comNumber)
        {
            if ((comNumber >= 0) && (comNumber < commandList.Count))
            {
                // Send Command to commander
                uint efxAddr = TemporaryEFXSourceAddress;
                commander.SendData(efxAddr, commandList[comNumber].GetEFXBuffer());
                for (int i = 0; i < RolandXP50Constants.MIDIChannelCount; i++)
                {
                    uint addr = TemporaryLocalSwitchAddresses[i];
                    if ((i >= RolandXP50Constants.SongChannelCount) && (i < RolandXP50Constants.MIDIChannelCount))
                    {
                        commander.SendData(addr, commandList[comNumber].channels[i - RolandXP50Constants.SongChannelCount].GetLocalBuffer());
                    }
                    else
                    {
                        commander.SendData(addr, new byte[] { (byte)LocalSwitch.OFF });
                    }
                }
            }
        }
    }
}
