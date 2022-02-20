using SynthLiveMidiController.MIDIMessages;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    public class RolandXP50Class : IInstrumentCommandInterface
    {
        //-----------------------------------------  HEADER  --------------------------------------------------
        private readonly byte[] sysExHeader = {
                                                0x41,  // ID number (Roland)
                                                0x10,  // device ID (Optional in device)
                                                0x6A,  // model ID (XP-50)
                                            };

        private const int DT1CommandByte = 0x12;        // command ID (Data Set DT1)
        private const int RequestCommandByte = 0x11;    // command ID (Data Set Request)
        public byte[] Header => sysExHeader;            // Header

        public int HeaderLength => sysExHeader.Length;  // Header Length
        public byte Dt1Command => DT1CommandByte;           // DT1 command const
        public byte RequestCommand => RequestCommandByte;   // Request command const
    }
}
