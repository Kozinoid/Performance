using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SanfordMIDI = Sanford.Multimedia.Midi;

namespace SynthLiveMidiController
{
    public partial class MidiInOutDialog : Form
    {
        private int selectedInIndex = -1;
        private int selectedOutIndex = -1;
        public int SelectedInIndex { get { return selectedInIndex; } }
        public int SelectedOutIndex { get { return selectedOutIndex; } }

        public MidiInOutDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            lb_InputDevice.Items.Clear();
            for (int i = 0; i < SanfordMIDI.InputDevice.DeviceCount; i++)
            {
                lb_InputDevice.Items.Add(SanfordMIDI.InputDevice.GetDeviceCapabilities(i).name);
            }
            lb_InputDevice.SelectedIndex = selectedInIndex;

            lb_OutputDevice.Items.Clear();
            for (int i = 0; i < SanfordMIDI.OutputDevice.DeviceCount; i++)
            {
                lb_OutputDevice.Items.Add(SanfordMIDI.OutputDevice.GetDeviceCapabilities(i).name);
            }
            lb_OutputDevice.SelectedIndex = selectedOutIndex;

            base.OnLoad(e);
        }

        private void LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedInIndex = lb_InputDevice.SelectedIndex;
            selectedOutIndex = lb_OutputDevice.SelectedIndex;

            if ((selectedInIndex >= 0) && (selectedOutIndex >= 0)) bt_Ok.Enabled = true; else bt_Ok.Enabled = false;
        }
    }
}
