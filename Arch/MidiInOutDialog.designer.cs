namespace SynthLiveMidiController
{
    partial class MidiInOutDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_InputDevice = new System.Windows.Forms.ListBox();
            this.lb_OutputDevice = new System.Windows.Forms.ListBox();
            this.bt_Ok = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_InputDevice
            // 
            this.lb_InputDevice.FormattingEnabled = true;
            this.lb_InputDevice.ItemHeight = 15;
            this.lb_InputDevice.Location = new System.Drawing.Point(14, 35);
            this.lb_InputDevice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lb_InputDevice.Name = "lb_InputDevice";
            this.lb_InputDevice.Size = new System.Drawing.Size(188, 274);
            this.lb_InputDevice.TabIndex = 0;
            this.lb_InputDevice.SelectedIndexChanged += new System.EventHandler(this.LB_SelectedIndexChanged);
            // 
            // lb_OutputDevice
            // 
            this.lb_OutputDevice.FormattingEnabled = true;
            this.lb_OutputDevice.ItemHeight = 15;
            this.lb_OutputDevice.Location = new System.Drawing.Point(223, 35);
            this.lb_OutputDevice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lb_OutputDevice.Name = "lb_OutputDevice";
            this.lb_OutputDevice.Size = new System.Drawing.Size(188, 274);
            this.lb_OutputDevice.TabIndex = 1;
            this.lb_OutputDevice.SelectedIndexChanged += new System.EventHandler(this.LB_SelectedIndexChanged);
            // 
            // bt_Ok
            // 
            this.bt_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_Ok.Enabled = false;
            this.bt_Ok.Location = new System.Drawing.Point(66, 315);
            this.bt_Ok.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bt_Ok.Name = "bt_Ok";
            this.bt_Ok.Size = new System.Drawing.Size(88, 27);
            this.bt_Ok.TabIndex = 2;
            this.bt_Ok.Text = "Ok";
            this.bt_Ok.UseVisualStyleBackColor = true;
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Cancel.Location = new System.Drawing.Point(168, 315);
            this.bt_Cancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(88, 27);
            this.bt_Cancel.TabIndex = 3;
            this.bt_Cancel.Text = "Cancel";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "MIDI Input Device:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "MIDI Output Device:";
            // 
            // bt_Exit
            // 
            this.bt_Exit.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.bt_Exit.Location = new System.Drawing.Point(270, 315);
            this.bt_Exit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(88, 27);
            this.bt_Exit.TabIndex = 3;
            this.bt_Exit.Text = "Exit";
            this.bt_Exit.UseVisualStyleBackColor = true;
            // 
            // MidiInOutDialog
            // 
            this.AcceptButton = this.bt_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bt_Cancel;
            this.ClientSize = new System.Drawing.Size(424, 342);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.bt_Ok);
            this.Controls.Add(this.lb_OutputDevice);
            this.Controls.Add(this.lb_InputDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MidiInOutDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MidiInOutDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_InputDevice;
        private System.Windows.Forms.ListBox lb_OutputDevice;
        private System.Windows.Forms.Button bt_Ok;
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_Exit;
    }
}