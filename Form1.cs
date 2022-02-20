using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private SegmentManager manager = null;                              // Main Manager

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
            mainMidiDevice = new SanfordMidiDevice();            // Create MIDI Device
            mainMidiDevice.InitDevices(midiInDevice, midiOutDevice);        // Init In/Out devices

            roland = new RolandXP50Class();                                 // Use Roland XP50 
            messages = new InstrumentMIDIMessages(roland);                  // Use Roland XP50 message options

            perfCommander = new PreprocessorCommandsClass(mainMidiDevice, messages);                                        // Command module

            manager = new SegmentManager(perfCommander);

            /*
            1. Разработать протокол передачи данных в Performance
            2. Разработать класс-посредник между редактором/хранилищем (заказчики-корректоры-хранители информации) и Performance
            3. Разработать интерфейс заказа параметров и Callback при их изменении из другого редактора
            Любой новый редактор, хранилище, визуализатор параметров может сам выбирать параметры и блоки памяти для редактирования и контролировать их изменение
            */
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(int val in Enum.GetValues(typeof(PerformanceCommonClass.PerformanceCommonParameters)))
            {
                Console.WriteLine("{0} = {1:X2}", Enum.GetName(typeof(PerformanceCommonClass.PerformanceCommonParameters), val), val);
            }
        }
    }
}
