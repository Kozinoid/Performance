using System;
using SynthLiveMidiController.MIDIMessages;
using System.Collections.Generic;

namespace SynthLiveMidiController.InstrumentList.Roland.XP50
{
    //--------------------------------  One Field Manager  -------------------------------------
    public class OneParameterFieldManager<T> where T : Enum
    {
        readonly string name = "";
        readonly int offset = 0;
        readonly int length = 1;
        readonly T parameter;
        readonly byte[] parameterValue;

        public string Name { get { return name; } }
        public int Offset { get { return offset; } }
        public int Length { get { return length; } }
        public T Parameter { get { return parameter; } }
        public byte[] ParameterValue
        {
            //get { return parameterValue; }
            get
            {
                byte[] res = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    res[i] = parameterValue[i];
                }
                return res;
            }
            set
            {
                for (int i = 0; i < length; i++)
                {
                    parameterValue[i] = value[i];
                }
            }
        }

        public OneParameterFieldManager(T par, int os, int len)
        {
            name = Enum.GetName(typeof(T), par);
            length = len;
            offset = os;
            parameter = par;
            parameterValue = new byte[len];
        }
    }

    //                                                      SEGMENT_BUFFER                                                                      |
    //==========================================================================================================================================|
    class SEGMENT_BUFFER
    {
        private readonly int length;
        // buffer
        private byte[] buffer;
        // Buffer
        public byte[] Buffer
        {
            get { return buffer; }
        }
        // Length
        public int Length
        {
            get { return length; }
        }
        // Indexator
        public byte this[int index]
        {
            get { return buffer[index]; }
        }
        // Constructor
        public SEGMENT_BUFFER(int len)
        {
            length = len;
            buffer = new byte[length];
        }
    }

    //                                                  DataSegment base class                                                                  |
    //==========================================================================================================================================|
    abstract class DataSegment<T> where T : Enum 
    {
        //public event EventHandler<SegmentChangedEventArgs> OnBufferChangedHandler = null;

        //------------------------------- DATA -------------------------------
        // id
        protected readonly int segmentId = -1;
        // ID
        public int SegmentID { get { return segmentId; } }
        // Main Segment Address
        protected readonly uint segmentAddress;
        // Main Segment Address getter
        public uint SegmentAddress { get { return segmentAddress; } }
        // Structure
        protected SEGMENT_BUFFER segmentData;
        // Structure Length
        public uint Length
        {
            get { return (uint)segmentData.Length; }
        }

        //-----------------------  Event List  -------------------------------
        // Event List
        EventList<T> eventList;

        //----------------------- Parameter Manager --------------------------
        // Parameters Manager
        protected Dictionary<T, OneParameterFieldManager<T>> parametersManager;
        // Modified Fields
        protected List<OneParameterFieldManager<T>> modified = new List<OneParameterFieldManager<T>>();
        public List<OneParameterFieldManager<T>> Modified
        {
            get { return modified; }
        }

        //--------------------------------------------------------------------
        // Constructor
        public DataSegment(uint addr, int len, int identifyNumber)
        {
            segmentId = identifyNumber;
            segmentAddress = addr;
            segmentData = new SEGMENT_BUFFER(len);
            ParametersManagerInit();
            eventList = new EventList<T>();
        }

        // Parameters Manager Init
        protected abstract void ParametersManagerInit();
        
        //----------------------- Data operations ----------------------------
        // Data Array -> Structure
        public void CopyDataToStructure(byte[] data)
        {
            CopyDataToStructure(data, 0);
        }

        // Data Array -> Structure at Address
        bool scanAll = false;
        public void CopyDataToStructure(byte[] data, int offset)
        {
            Array.Copy(data, 0, segmentData.Buffer, offset, data.Length);
            SendParametersToSubScribers(scanAll);
            scanAll = false;
        }

        // Structure -> Data Array
        public byte[] ToByteArray()
        {
            int length = segmentData.Length;
            byte[] data = new byte[length];
            Array.Copy(segmentData.Buffer, data, length);

            return data;
        }

        //-------------------------------  Roland  ------------------------------------
        // Send
        public void SendData(IPerformanceMIDIInOutInterface commander)
        {
            commander.SendData(segmentAddress, ToByteArray());
        }

        // Request
        public void RequestData(IPerformanceMIDIInOutInterface commander, bool all)
        {
            scanAll = all;
            commander.RequestData(segmentAddress, Length);
        }

        //---------------------------  Parameter section  -----------------------------
        // Set Parameter in Parameter Field
        public void SetParameterInField(T param, byte[] value)
        {
            OneParameterFieldManager<T> field = parametersManager[param];
            int len = field.Length;
            field.ParameterValue = value;
        }

        // Copy Parameter in Structure
        public void CopyParameterIntoStructure(T param) 
        {
            OneParameterFieldManager<T> field = parametersManager[param];
            CopyDataToStructure(field.ParameterValue, field.Offset);
        }

        // Send Parameter to Roland
        public void SendParameter(T param, IPerformanceMIDIInOutInterface commander)
        {
            uint addr = segmentAddress + (uint)parametersManager[param].Offset;
            commander.SendData(addr, parametersManager[param].ParameterValue);
        }

        //------------------------------  From Roland  --------------------------------
        // Reset Modified
        private void ResetModified()
        {
            modified.Clear();
        }

        // Scan Structure. Find non equal parameters. Copy parameters.
        private void ScanModifiedParameters()
        {
            ResetModified();

            foreach (T param in Enum.GetValues(typeof(T))) 
            {
                OneParameterFieldManager<T> field = parametersManager[param];
                int len = field.Length;
                int off = field.Offset;
                bool equal = true;
                for (int i = 0; i < len; i++)
                {
                    if (segmentData[off + i] != field.ParameterValue[i])
                    {
                        equal = false;
                    }
                }
                if (!equal)
                {
                    byte[] buf = new byte[len];
                    Array.Copy(segmentData.Buffer, off, buf, 0, len);
                    field.ParameterValue = buf;
                    modified.Add(field);
                }
            }
        }

        // Scan All Parameters
        private void ScanAllParameters()
        {
            ResetModified();

            foreach (T param in Enum.GetValues(typeof(T)))
            {
                OneParameterFieldManager<T> field = parametersManager[param];
                int len = field.Length;
                int off = field.Offset;

                byte[] buf = new byte[len];
                Array.Copy(segmentData.Buffer, off, buf, 0, len);
                field.ParameterValue = buf;
                modified.Add(field);
            }
        }

        // Find All Subscribers
        private void FindSubscribers()
        {
            foreach(EventHandler<ModifiedParameterFieldsEventArgs<T>> eHandler in eventList.GetKeys())
            {
                foreach(T prm in eventList[eHandler])
                {
                    foreach(OneParameterFieldManager<T> prmManager in modified)
                    {
                        //if (prm == prmManager.Parameter)
                        if (Enum.GetName(typeof(T), prm) == Enum.GetName(typeof(T), prmManager.Parameter))
                        {
                            eHandler?.Invoke(this, new ModifiedParameterFieldsEventArgs<T>(segmentId, prm, prmManager.ParameterValue));
                        }
                    }
                }
            }
        }

        // Send Parameters to Subscribers
        private void SendParametersToSubScribers(bool all)
        {
            if (all) ScanAllParameters(); else ScanModifiedParameters();
            FindSubscribers();
        }

        // Refresh All Parameters
        public void RefreshAllParameters()
        {
            ScanAllParameters();
            FindSubscribers();
        }

        //-----------------------------  From Subscriber  -----------------------------
        // Receive Parameter
        private void StoreParameter(T param, byte[] value, IPerformanceMIDIInOutInterface commander)
        {
            SetParameterInField(param, value);
            CopyParameterIntoStructure(param);
            SendParameter(param, commander);
        }
        
        // Request Parameters Callback
        public void RequestCallback(EventHandler<ModifiedParameterFieldsEventArgs<T>> eHandler, T[] prs)
        {
            eventList.AddCallback(eHandler, prs);
        }

        // Set Parameter
        public void SetParameter(EventHandler<ModifiedParameterFieldsEventArgs<T>> id, T parameter, byte[] value, IPerformanceMIDIInOutInterface commander)
        {
            StoreParameter(parameter, value, commander);
            foreach (EventHandler<ModifiedParameterFieldsEventArgs<T>> eHandler in eventList.GetKeys())
            {
                foreach (T prm in eventList[eHandler])
                {
                    if (Enum.GetName(typeof(T), prm) == Enum.GetName(typeof(T), parameter))
                    {
                        if (eHandler != id)
                        {
                            eHandler?.Invoke(this, new ModifiedParameterFieldsEventArgs<T>(segmentId, parameter, value));
                        }
                    }

                }
            }
        }
    }

    // *********************************************  Performance Common Data Class  ************************************************************
    class PerformanceCommonClass : DataSegment<PERFORMANCE_COMMON_PARAMETERS>
    {
        // Structure Length
        const int PerformanceCommonStructureLength = 0x42;

        // delegate
        public delegate void SegmentParametersModifiedEventHandler(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS> e);

        // Constructor
        public PerformanceCommonClass(uint segAddr) : base(segAddr, PerformanceCommonStructureLength, 16) {}

        // Parameters Manager Init
        protected override void ParametersManagerInit()
        {
            //parametersManager = new Dictionary<PERFORMANCE_COMMON_PARAMETERS, OneParameterFieldManager<PERFORMANCE_COMMON_PARAMETERS>>();
            //foreach (PERFORMANCE_COMMON_PARAMETERS par in Enum.GetValues(typeof(PERFORMANCE_COMMON_PARAMETERS)))
            //{
            //    int length = 1;
            //    if (par == PERFORMANCE_COMMON_PARAMETERS.PerformanceName) length = 12;
            //    if (par == PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo) length = 2;
            //    int offset = (int)par;

            //    OneParameterFieldManager<PERFORMANCE_COMMON_PARAMETERS> item = new OneParameterFieldManager<PERFORMANCE_COMMON_PARAMETERS>(par, offset, length);
            //    parametersManager.Add(par, item);
            //}

            parametersManager = new Dictionary<PERFORMANCE_COMMON_PARAMETERS, OneParameterFieldManager<PERFORMANCE_COMMON_PARAMETERS>>();
            int parametersCount = Enum.GetValues(typeof(PERFORMANCE_COMMON_PARAMETERS)).Length;
            Array addresses = Enum.GetValues(typeof(PERFORMANCE_COMMON_PARAMETERS));
            //int[] lengthes = new int[parametersCount];
            for (int i = 0; i < parametersCount; i++)
            {
                int length = (i == (parametersCount - 1)) ? PerformanceCommonStructureLength - (int)addresses.GetValue(i) : (int)addresses.GetValue(i + 1) - (int)addresses.GetValue(i);
                int offset = (int)addresses.GetValue(i);
                PERFORMANCE_COMMON_PARAMETERS par = (PERFORMANCE_COMMON_PARAMETERS)addresses.GetValue(i);

                OneParameterFieldManager<PERFORMANCE_COMMON_PARAMETERS> item = new OneParameterFieldManager<PERFORMANCE_COMMON_PARAMETERS>(par, offset, length);
                parametersManager.Add(par, item);
            }
        }
    }

    // *************************************************  Performance Part Data Class  **********************************************************
    class PerformancePartClass : DataSegment<PERFORMANCE_PART_PARAMETERS>
    {
        // Structure Length
        const int PerformancePartStructureLength = 0x19;

        // delegate
        public delegate void SegmentParametersModifiedEventHandler(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS> e);

        // Constructor
        public PerformancePartClass(uint segAddr, int identifyNumber) : base(segAddr, PerformancePartStructureLength, identifyNumber) { }

        // Parameters Manager Init
        protected override void ParametersManagerInit()
        {
            //parametersManager = new Dictionary<PERFORMANCE_PART_PARAMETERS, OneParameterFieldManager<PERFORMANCE_PART_PARAMETERS>>();
            //foreach (PERFORMANCE_PART_PARAMETERS par in Enum.GetValues(typeof(PERFORMANCE_PART_PARAMETERS)))
            //{
            //    int length = 1;
            //    if (par == PERFORMANCE_PART_PARAMETERS.PatchNumber) length = 4;
            //    if (par == PERFORMANCE_PART_PARAMETERS.TransmitVolume) length = 2;
            //    int offset = (int)par;
            //    OneParameterFieldManager<PERFORMANCE_PART_PARAMETERS> item = new OneParameterFieldManager<PERFORMANCE_PART_PARAMETERS>(par, offset, length);
            //    parametersManager.Add(par, item);
            //}

            parametersManager = new Dictionary<PERFORMANCE_PART_PARAMETERS, OneParameterFieldManager<PERFORMANCE_PART_PARAMETERS>>();
            int parametersCount = Enum.GetValues(typeof(PERFORMANCE_PART_PARAMETERS)).Length;
            Array addresses = Enum.GetValues(typeof(PERFORMANCE_PART_PARAMETERS));
            //int[] lengthes = new int[parametersCount];
            for (int i = 0; i < parametersCount; i++)
            {
                int length = (i == (parametersCount - 1)) ? PerformancePartStructureLength - (int)addresses.GetValue(i) : (int)addresses.GetValue(i + 1) - (int)addresses.GetValue(i);
                int offset = (int)addresses.GetValue(i);
                PERFORMANCE_PART_PARAMETERS par = (PERFORMANCE_PART_PARAMETERS)addresses.GetValue(i);

                OneParameterFieldManager<PERFORMANCE_PART_PARAMETERS> item = new OneParameterFieldManager<PERFORMANCE_PART_PARAMETERS>(par, offset, length);
                parametersManager.Add(par, item);
            }
        }
    }

    //                                                            Event List                                                                    |
    //===========================================================================================================================================
    class EventList<T> where T : Enum
    {
        Dictionary<EventHandler<ModifiedParameterFieldsEventArgs<T>>, T[]> eventList = new Dictionary<EventHandler<ModifiedParameterFieldsEventArgs<T>>, T[]>();

        public Dictionary<EventHandler<ModifiedParameterFieldsEventArgs<T>>, T[]>.KeyCollection GetKeys()
        {
            return eventList.Keys;
        }

        public T[] this[EventHandler<ModifiedParameterFieldsEventArgs<T>> evnt]
        {
            get { return eventList[evnt]; }
        }

        public EventHandler<ModifiedParameterFieldsEventArgs<T>> AddCallback(EventHandler<ModifiedParameterFieldsEventArgs<T>> eHandler, T[] prs)
        {
            eventList.Add(eHandler, prs);
            return eHandler;
        }
    }
}
