using SynthLiveMidiController.MIDIMessages;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    class RolandXP50Commands
    {
        //----------------------------------------  DATA  -----------------------------------------------------
        private readonly IPerformanceMIDIInOutInterface commander;          // Command Collection
        private readonly RolandXP50SongCommandSet songCommandSet;           // Song command set
        private readonly RolandXP50FastCommandSet fastCommandSet;           // FAst command set

        // Constructor
        public RolandXP50Commands(IPerformanceMIDIInOutInterface cmd)
        {
            commander = cmd;
            songCommandSet = new RolandXP50SongCommandSet(commander);
            fastCommandSet = new RolandXP50FastCommandSet(commander);
        }

        //|                                      *********  SONG/FAST COMMAND SECTION <-> ROLAND  *********                                     |
        //======================================================================================================================================|

        //------------------------------------------------------  SEND COMMANDS TO ROLAND  -----------------------------------------------------|
        // MIDI Commander: Send Song Command
        public void SendSongCommand(int commandIndex)
        {
            songCommandSet.SendCommand(commandIndex);
        }

        // MIDI Commander: Send Fast Command
        public void SendFastCommand(int commandIndex)
        {
            fastCommandSet.SendCommand(commandIndex);
        }

        //|                                      *********  COMMAND SECTION <-> STORAGE MODULE  *********                                       |
        //======================================================================================================================================|

        //----------------------------------------------------------  SONG SECTION  ------------------------------------------------------------|
        // Set Song Command Data
        public void SetSongCommandSection(byte[] data)
        {
            songCommandSet.FromByteArray(data);
        }

        // Get Song Command Data
        public byte[] GetSongCommandSection()
        {
            return songCommandSet.ToByteArray();
        }

        //----------------------------------------------------------  FAST SECTION  ------------------------------------------------------------|
        // Set Fast Command Data
        public void SetFastCommandSection(byte[] data)
        {
            fastCommandSet.FromByteArray(data);
        }

        // Get Fast Command Data
        public byte[] GetFastCommandSection()
        {
            return fastCommandSet.ToByteArray();
        }
    }
}
