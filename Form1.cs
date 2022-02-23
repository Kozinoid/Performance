using System;
using System.Windows.Forms;
using SynthLiveMidiController;
using SynthLiveMidiController.InstrumentList.Roland.XP50;
using SynthLiveMidiController.MIDIDevice;
using SynthLiveMidiController.MIDIMessages;

namespace Performance
{
    public partial class Form1 : Form
    {
        // Fields
        private IMidiInOutInterface mainMidiDevice = null;       // Main MIDI Device
        private readonly MidiInOutDialog dlg = new MidiInOutDialog();       // Select Midi In/Out Device Dialog
        private int midiInDevice = -1;                                      // Midi In Device Index
        private int midiOutDevice = -1;                                     // Midi Out Device Index
        private PreprocessorCommandsClass perfCommander = null;             // Command module
        private InstrumentMIDIMessages messages = null;                     // Message options
        private RolandXP50Class roland = null;                              // Roland XP50 
        private AllDataManager manager = null;                              // Main Manager

        //------------  TEST  ---------------
        EditorA edA;
        EditorB edB;
        EditorC edC;
        EditorD edD;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupDevices();
        }

        // Setup Devices
        private void SetupDevices()
        {
            ShowSelectMidiDeviceDialog();                                   // Show Device In/Out Dialog
            mainMidiDevice = new SanfordMidiDevice();                       // Create MIDI Device
            mainMidiDevice.InitDevices(midiInDevice, midiOutDevice);        // Init In/Out devices

            roland = new RolandXP50Class();                                 // Use Roland XP50 
            messages = new InstrumentMIDIMessages(roland);                  // Use Roland XP50 message options

            perfCommander = new PreprocessorCommandsClass(mainMidiDevice, messages);                                        // Command module

            manager = new AllDataManager(perfCommander);

            SetupControl();
        }

        private void SetupControl()
        {
            edA = new EditorA(manager);
            edB = new EditorB(manager);
            edC = new EditorC(manager);
            edD = new EditorD(manager);

            edA.PerfCommonEventHandler += EdA_PerfCommonEventHandler;
            edA.PerfPartEventHandler += EdA_PerfPartEventHandler;
            edB.PerfCommonEventHandler += EdB_PerfCommonEventHandler;
            edB.PerfPartEventHandler += EdB_PerfPartEventHandler;
            edC.PerfCommonEventHandler += EdC_PerfCommonEventHandler;
            edC.PerfPartEventHandler += EdC_PerfPartEventHandler;
            edD.PerfCommonEventHandler += EdD_PerfCommonEventHandler;
            edD.PerfPartEventHandler += EdD_PerfPartEventHandler;

            tbA.MouseWheel += TbA_MouseWheel;
            tbB.MouseWheel += TbB_MouseWheel;
            tbC.MouseWheel += TbC_MouseWheel;
            tbD.MouseWheel += TbD_MouseWheel;

            revA.MouseWheel += RevA_MouseWheel;
            revB.MouseWheel += RevB_MouseWheel;
            revC.MouseWheel += RevC_MouseWheel;
            revD.MouseWheel += RevD_MouseWheel;
        }

        // Form Closing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            perfCommander.Stop();
            mainMidiDevice?.CloseDevices();                                 // close MIDI Device
        }

        // Select MIDI devices or exit
        private void ShowSelectMidiDeviceDialog()
        {
            do
            {
                DialogResult dr = dlg.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    midiInDevice = dlg.SelectedInIndex;
                    midiOutDevice = dlg.SelectedOutIndex;
                }
                else if (dr == DialogResult.Abort)
                {
                    Environment.Exit(0);
                }
            }
            while ((midiInDevice < 0) || (midiOutDevice < 0));
        }

        //-----------------------------------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            manager.RequestAllPerformanceData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            edB.SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, 100);
        }

        //-----------------------------  Callback  -----------------------------
        private void EdD_PerfPartEventHandler(object sender, EventArgs e)
        {
            revD.Text = edD.Reverb.ToString();
        }

        private void EdD_PerfCommonEventHandler(object sender, EventArgs e)
        {
            tbD.Text = edD.Tempo.ToString();
        }

        private void EdC_PerfPartEventHandler(object sender, EventArgs e)
        {
            revC.Text = edC.Reverb.ToString();
        }

        private void EdC_PerfCommonEventHandler(object sender, EventArgs e)
        {
            tbC.Text = edC.Tempo.ToString();
        }

        private void EdB_PerfPartEventHandler(object sender, EventArgs e)
        {
            revB.Text = edB.Reverb.ToString();
        }

        private void EdB_PerfCommonEventHandler(object sender, EventArgs e)
        {
            tbB.Text = edB.Tempo.ToString();
        }

        private void EdA_PerfPartEventHandler(object sender, EventArgs e)
        {
            revA.Text = edA.Reverb.ToString();
        }

        private void EdA_PerfCommonEventHandler(object sender, EventArgs e)
        {
            tbA.Text = edA.Tempo.ToString();
        }

        //------------------------  Tempo Wheel  -----------------------------
        private void TbD_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = 0;
            if (e.Delta > 0) delta = 1;
            if (e.Delta < 0) delta = -1;
            edD.SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, CorrectTempo(edD.Tempo + delta));
        }

        private void TbC_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = 0;
            if (e.Delta > 0) delta = 1;
            if (e.Delta < 0) delta = -1;
            edC.SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, CorrectTempo(edC.Tempo + delta));
        }

        private void TbB_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = 0;
            if (e.Delta > 0) delta = 1;
            if (e.Delta < 0) delta = -1;
            edB.SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, CorrectTempo(edB.Tempo + delta));
        }

        private void TbA_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = 0;
            if (e.Delta > 0) delta = 1;
            if (e.Delta < 0) delta = -1;
            edA.SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, CorrectTempo(edA.Tempo + delta));
        }

        //---------------------------  Reverb Wheel  --------------------------------
        private void RevD_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = 0;
            if (e.Delta > 0) delta = 1;
            if (e.Delta < 0) delta = -1;
            edD.SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS.ReverbSendLevel, 0, CorrectByte(edD.Reverb + delta));
        }

        private void RevC_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = 0;
            if (e.Delta > 0) delta = 1;
            if (e.Delta < 0) delta = -1;
            edC.SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS.ReverbSendLevel, 0, CorrectByte(edC.Reverb + delta));
        }

        private void RevB_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = 0;
            if (e.Delta > 0) delta = 1;
            if (e.Delta < 0) delta = -1;
            edB.SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS.ReverbSendLevel, 0, CorrectByte(edB.Reverb + delta));
        }

        private void RevA_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = 0;
            if (e.Delta > 0) delta = 1;
            if (e.Delta < 0) delta = -1;
            edA.SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS.ReverbSendLevel, 0, CorrectByte(edA.Reverb + delta));
        }

        // ------------------------------- Corrections  -------------------------------------
        private int CorrectTempo(int val)
        {
            int res = val;
            if (res < 20) res = 20;
            if (res > 250) res = 250;
            return res;
        }

        private int CorrectByte(int val)
        {
            int res = val;
            if (res < 0) res = 0;
            if (res > 127) res = 127;
            return res;
        }
    }

    abstract class Editor
    {
        public event EventHandler PerfCommonEventHandler = null;
        public event EventHandler PerfPartEventHandler = null;

        protected IParametersManager performance;
        protected EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS>> commID;
        protected EventHandler<ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS>> partID;
        protected int tempo;
        protected int reverb;
        public int Tempo
        {
            get { return tempo; }
            set 
            { 
                tempo = value;
                PerfCommonEventHandler?.Invoke(this, new EventArgs());
            }
        }
        public int Reverb
        {
            get { return reverb; }
            set 
            { 
                reverb = value;
                PerfPartEventHandler?.Invoke(this, new EventArgs());
            }
        }

        public Editor(IParametersManager perf)
        {
            performance = perf;
            commID = PerfCommonParametersChanged;
            partID = PerfPartParametersChanged;
        }

        protected virtual void PerfCommonParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS> e)
        {
            
        }

        protected virtual void PerfPartParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS> e)
        {
            
        }

        public virtual void SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS prm, int value)
        {
            
        }

        public virtual void SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS prm, int channel, int value)
        {
            
        }
    }

    class EditorA : Editor
    {
        public EditorA(IParametersManager perf) : base(perf) 
        {
            performance.RequestParameters(PerfCommonParametersChanged, new PERFORMANCE_COMMON_PARAMETERS[] { PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo });
            performance.RequestParameters(PerfPartParametersChanged, new PERFORMANCE_PART_PARAMETERS[] { PERFORMANCE_PART_PARAMETERS.ReverbSendLevel });
        }

        public override void SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS prm, int value)
        {
            Tempo = value;
            byte[] valBuffer = new byte[2];
            valBuffer[1] = (byte)(value & 0x0F);
            valBuffer[0] = (byte)((value & 0xF0) >> 4);
            performance.SetParameter(commID, PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, valBuffer);
        }

        public override void SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS prm, int channel, int value)
        {
            Reverb = value;
            performance.SetParameter(partID, PERFORMANCE_PART_PARAMETERS.ReverbSendLevel, 0, new byte[] { (byte)value });
        }

        protected override void PerfCommonParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS> e)
        {
            if (e.Parameter == PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo)
            {
                Tempo = (e.Value[0] << 4) + e.Value[1];
            }
        }

        protected override void PerfPartParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS> e)
        {
            if (e.Parameter == PERFORMANCE_PART_PARAMETERS.ReverbSendLevel)
            {
                if (e.Segment == 0)
                {
                    Reverb = e.Value[0];
                }
            }
        }
    }

    class EditorB : Editor
    {
        public EditorB(IParametersManager perf) : base(perf) 
        {
            performance.RequestParameters(PerfCommonParametersChanged, new PERFORMANCE_COMMON_PARAMETERS[] { PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo });
            performance.RequestParameters(PerfPartParametersChanged, new PERFORMANCE_PART_PARAMETERS[] { PERFORMANCE_PART_PARAMETERS.ReverbSendLevel });
        }

        public override void SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS prm, int value)
        {
            Tempo = value;
            byte[] valBuffer = new byte[2];
            valBuffer[1] = (byte)(value & 0x0F);
            valBuffer[0] = (byte)((value & 0xF0) >> 4);
            performance.SetParameter(commID, PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, valBuffer);
        }

        public override void SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS prm, int channel, int value)
        {
            Reverb = value;
            performance.SetParameter(partID, PERFORMANCE_PART_PARAMETERS.ReverbSendLevel, 0, new byte[] { (byte)value });
        }

        protected override void PerfCommonParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS> e)
        {
            if (e.Parameter == PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo)
            {
                Tempo = (e.Value[0] << 4) + e.Value[1];
            }
        }

        protected override void PerfPartParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS> e)
        {
            if (e.Parameter == PERFORMANCE_PART_PARAMETERS.ReverbSendLevel)
            {
                if (e.Segment == 0)
                {
                    Reverb = e.Value[0];
                }
            }
        }
    }

    class EditorC : Editor
    {
        public EditorC(IParametersManager perf) : base(perf) 
        {
            performance.RequestParameters(PerfCommonParametersChanged, new PERFORMANCE_COMMON_PARAMETERS[] { PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo });
            performance.RequestParameters(PerfPartParametersChanged, new PERFORMANCE_PART_PARAMETERS[] { PERFORMANCE_PART_PARAMETERS.ReverbSendLevel });
        }

        public override void SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS prm, int value)
        {
            Tempo = value;
            byte[] valBuffer = new byte[2];
            valBuffer[1] = (byte)(value & 0x0F);
            valBuffer[0] = (byte)((value & 0xF0) >> 4);
            performance.SetParameter(commID, PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, valBuffer);
        }

        public override void SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS prm, int channel, int value)
        {
            Reverb = value;
            performance.SetParameter(partID, PERFORMANCE_PART_PARAMETERS.ReverbSendLevel, 1, new byte[] { (byte)value });
        }

        protected override void PerfCommonParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS> e)
        {
            if (e.Parameter == PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo)
            {
                Tempo = (e.Value[0] << 4) + e.Value[1];
            }
        }

        protected override void PerfPartParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS> e)
        {
            if (e.Parameter == PERFORMANCE_PART_PARAMETERS.ReverbSendLevel)
            {
                if (e.Segment == 1)
                {
                    Reverb = e.Value[0];
                }
            }
        }
    }

    class EditorD : Editor
    {
        public EditorD(IParametersManager perf) : base(perf) 
        {
            performance.RequestParameters(PerfCommonParametersChanged, new PERFORMANCE_COMMON_PARAMETERS[] { PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo });
            performance.RequestParameters(PerfPartParametersChanged, new PERFORMANCE_PART_PARAMETERS[] { PERFORMANCE_PART_PARAMETERS.ReverbSendLevel });
        }

        public override void SetPerfCommonParameter(PERFORMANCE_COMMON_PARAMETERS prm, int value)
        {
            Tempo = value;
            byte[] valBuffer = new byte[2];
            valBuffer[1] = (byte)(value & 0x0F);
            valBuffer[0] = (byte)((value & 0xF0) >> 4);
            performance.SetParameter(commID, PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo, valBuffer);
        }

        public override void SetPerfPartParameter(PERFORMANCE_PART_PARAMETERS prm, int channel, int value)
        {
            Reverb = value;
            performance.SetParameter(partID, PERFORMANCE_PART_PARAMETERS.ReverbSendLevel, 1, new byte[] { (byte)value });
        }

        protected override void PerfCommonParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_COMMON_PARAMETERS> e)
        {
            if (e.Parameter == PERFORMANCE_COMMON_PARAMETERS.PerformanceTempo)
            {
                Tempo = (e.Value[0] << 4) + e.Value[1];
            }
        }

        protected override void PerfPartParametersChanged(object sender, ModifiedParameterFieldsEventArgs<PERFORMANCE_PART_PARAMETERS> e)
        {
            if (e.Parameter == PERFORMANCE_PART_PARAMETERS.ReverbSendLevel)
            {
                if (e.Segment == 1)
                {
                    Reverb = e.Value[0];
                }
            }
        }
    }
}
