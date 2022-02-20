using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SynthLiveMidiController.MIDIMessages
{
    //------------------------------ Structure for UINT to byte[] conversion ----------------------------------
    [StructLayout(LayoutKind.Explicit)]
    public struct UINTUnion
    {
        [FieldOffset(0)]
        public uint i;

        [FieldOffset(0)]
        public byte b1;

        [FieldOffset(1)]
        public byte b2;

        [FieldOffset(2)]
        public byte b3;

        [FieldOffset(3)]
        public byte b4;
    }
    //===========================================  CLASSES  ===================================================
    // Base message class
    public abstract class MIDIMessageBase
    {

    }

    // Channel Message Class
    public class ChannelMessageClass : MIDIMessageBase
    {
        readonly MIDIEvents.ChannelCommand command;
        readonly int channal;
        readonly int data1;
        readonly int data2;

        public MIDIEvents.ChannelCommand Command { get { return command; } }
        public int Channel { get { return channal; } }
        public int Data1 { get { return data1; } }
        public int Data2 { get { return data2; } }

        public ChannelMessageClass(MIDIEvents.ChannelCommand cmd, int chan, int d1, int d2)
        {
            command = cmd;
            channal = chan;
            data1 = d1;
            data2 = d2;
        }
    }

    // SysEx Base Class
    public class SystemExclusiveBaseClass : MIDIMessageBase
    {
        protected List<byte> bufferList;        // Buffer byte list
        protected int headerLength = 3;         // Default header length

        // Create SystemExclusiveBaseClass
        public SystemExclusiveBaseClass()
        {
            bufferList = new List<byte> 
            {
                0xF0,   // Exclusive status
                0xF7    // End of Exclusive
            };
        }

        // Create SystemExclusiveDT1Class from received message
        public SystemExclusiveBaseClass(byte[] messageArgs)
        {
            bufferList = new List<byte>();
            foreach (byte bt in messageArgs)
            {
                bufferList.Add(bt);
            }
        }

        // ADD UINT to List Tail before 0xF7
        internal void InsertUINT(uint number)
        {
            UINTUnion uu = new UINTUnion 
            {
                i = number
            };

            bufferList.Insert(bufferList.Count - 1, uu.b4); // addr - byte 1
            bufferList.Insert(bufferList.Count - 1, uu.b3); // addr - byte 2
            bufferList.Insert(bufferList.Count - 1, uu.b2); // addr - byte 3
            bufferList.Insert(bufferList.Count - 1, uu.b1); // addr - byte 4
        }

        // ADD byte[] to List Tail before 0xF7
        internal void InsertArray(byte[] data)
        {
            if (data.Length > 0)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    InsertByte(data[i]);
                }
            }
        }

        // ADD byte to List Tail before 0xF7
        internal void InsertByte(byte bt)
        {
            bufferList.Insert(bufferList.Count - 1, bt);
        }

        // ADD CheckSum to List Tail before 0xF7
        public void InsertCheckSum(byte[] data)
        {
            uint sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            byte result = (byte)(128 - sum % 128);
            if (result == 128) result = 0;
            InsertByte(result);                            // checksum
        }

        // Get byte[] from buffer
        public byte[] GetBuffer()
        {
            return bufferList.ToArray();
        }

        // Get Hedaer Lenght
        public int GetHeaderLenght()
        {
            return headerLength;
        }

        // Get address from SysEx Messag
        public uint GetAddressFromArray()
        {
            UINTUnion uu = new UINTUnion
            {
                b4 = bufferList[headerLength + 2],
                b3 = bufferList[headerLength + 3],
                b2 = bufferList[headerLength + 4],
                b1 = bufferList[headerLength + 5]
            };
            return uu.i;
        }

        // Get data from SysEx Message 
        public byte[] GetDataFromArray()
        {
            int count = bufferList.Count - headerLength - 8;
            int offset = headerLength + 6;
            byte[] result = new byte[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = bufferList[offset + i];
            }
            return result;
        }
    }

    // SysEx DT1 type message class
    public class SystemExclusiveDT1Class : SystemExclusiveBaseClass
    {
        // Standart constructor
        public SystemExclusiveDT1Class(byte[] header, byte command, uint addr, byte[] data)
        {
            InsertArray(header);   // Вставить заголовок SysEx, зависит от типа синтезатора
            headerLength = header.Length;   // put header length

            InsertByte(command);    // Вставить команду DT1

            InsertUINT(addr);      // Встаить адрес

            InsertArray(data);      // Вставить данные

            InsertCheckSum(bufferList.GetRange(headerLength + 2, bufferList.Count - 6).ToArray());     // Контрольная сумма
        }
    }

    //SysEx Data Request message class
    public class SystemExclusiveRequestClass : SystemExclusiveBaseClass
    {
        public SystemExclusiveRequestClass(byte[] header, byte command, uint addr, uint len)
        {
            InsertArray(header);   // Вставить заголовок SysEx, зависит от типа синтезатора
            headerLength = header.Length;   // put header length

            InsertByte(command);    // Вставить команду Request

            InsertUINT(addr);      // Встаить адрес

            InsertUINT(len);       // Встаить длину

            InsertCheckSum(bufferList.GetRange(headerLength + 2, bufferList.Count - 6).ToArray());     // Контрольная сумма
        }
    }
}
