using System;

namespace SynthLiveMidiController
{
    class TestClass
    {
        // PRINT SYSEX
        public static void PrintBuffer(byte[] buffer, string tittle = "Array:")
        {
            //=================================== TEST ===============================
            Console.WriteLine(tittle);
            Console.WriteLine("Length: {0}", buffer.Length);

            foreach (byte bt in buffer) 
            {
                Console.Write("{0:X2} ", bt);
            }
            Console.WriteLine();
            //========================================================================
        }

        public static void PrintArray(byte[] buffer)
        {
            foreach (byte bt in buffer)
            {
                Console.Write("{0:X2} ", bt);
            }
            Console.WriteLine();
        }
    }
}

//// Имя Performance
//public string PerformanceName
//{
//    get
//    {
//        string str = "";

//        byte[] buf = PerformanceNameToByteArray();
//        str = Encoding.Default.GetString(buf);

//        return str;

//    }
//    set
//    {
//        int length = value.Length;
//        byte[] buf = new byte[length];
//        buf = Encoding.Default.GetBytes(value);
//        perfomanceCommonData.PerformanceName1 = buf[0];
//        perfomanceCommonData.PerformanceName2 = buf[1];
//        perfomanceCommonData.PerformanceName3 = buf[2];
//        perfomanceCommonData.PerformanceName4 = buf[3];
//        perfomanceCommonData.PerformanceName5 = buf[4];
//        perfomanceCommonData.PerformanceName6 = buf[5];
//        perfomanceCommonData.PerformanceName7 = buf[6];
//        perfomanceCommonData.PerformanceName8 = buf[7];
//        perfomanceCommonData.PerformanceName9 = buf[8];
//        perfomanceCommonData.PerformanceName10 = buf[9];
//        perfomanceCommonData.PerformanceName11 = buf[10];
//        perfomanceCommonData.PerformanceName12 = buf[11];
//    }
//}

//public byte[] PerformanceNameToByteArray()
//{
//    byte[] buf = new byte[12];
//    buf[0] = perfomanceCommonData.PerformanceName1;
//    buf[1] = perfomanceCommonData.PerformanceName2;
//    buf[2] = perfomanceCommonData.PerformanceName3;
//    buf[3] = perfomanceCommonData.PerformanceName4;
//    buf[4] = perfomanceCommonData.PerformanceName5;
//    buf[5] = perfomanceCommonData.PerformanceName6;
//    buf[6] = perfomanceCommonData.PerformanceName7;
//    buf[7] = perfomanceCommonData.PerformanceName8;
//    buf[8] = perfomanceCommonData.PerformanceName9;
//    buf[9] = perfomanceCommonData.PerformanceName10;
//    buf[10] = perfomanceCommonData.PerformanceName11;
//    buf[11] = perfomanceCommonData.PerformanceName12;
//    return buf;
//}