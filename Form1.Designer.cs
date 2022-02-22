
namespace Performance
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.gbB = new System.Windows.Forms.GroupBox();
            this.lbRevB = new System.Windows.Forms.Label();
            this.lbTempoB = new System.Windows.Forms.Label();
            this.gbA = new System.Windows.Forms.GroupBox();
            this.lbRevA = new System.Windows.Forms.Label();
            this.lbTempoA = new System.Windows.Forms.Label();
            this.gbC = new System.Windows.Forms.GroupBox();
            this.lbRevC = new System.Windows.Forms.Label();
            this.lbTempoC = new System.Windows.Forms.Label();
            this.gbD = new System.Windows.Forms.GroupBox();
            this.lbRevD = new System.Windows.Forms.Label();
            this.lbTempoD = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tbA = new System.Windows.Forms.TextBox();
            this.tbB = new System.Windows.Forms.TextBox();
            this.tbC = new System.Windows.Forms.TextBox();
            this.tbD = new System.Windows.Forms.TextBox();
            this.revA = new System.Windows.Forms.TextBox();
            this.revC = new System.Windows.Forms.TextBox();
            this.revD = new System.Windows.Forms.TextBox();
            this.revB = new System.Windows.Forms.TextBox();
            this.gbB.SuspendLayout();
            this.gbA.SuspendLayout();
            this.gbC.SuspendLayout();
            this.gbD.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(674, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Performance";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbB
            // 
            this.gbB.Controls.Add(this.revB);
            this.gbB.Controls.Add(this.tbB);
            this.gbB.Controls.Add(this.lbRevB);
            this.gbB.Controls.Add(this.lbTempoB);
            this.gbB.Location = new System.Drawing.Point(12, 207);
            this.gbB.Name = "gbB";
            this.gbB.Size = new System.Drawing.Size(252, 179);
            this.gbB.TabIndex = 1;
            this.gbB.TabStop = false;
            this.gbB.Text = "Editor B";
            // 
            // lbRevB
            // 
            this.lbRevB.AutoSize = true;
            this.lbRevB.Location = new System.Drawing.Point(36, 113);
            this.lbRevB.Name = "lbRevB";
            this.lbRevB.Size = new System.Drawing.Size(48, 13);
            this.lbRevB.TabIndex = 3;
            this.lbRevB.Text = "Rev ch1";
            // 
            // lbTempoB
            // 
            this.lbTempoB.AutoSize = true;
            this.lbTempoB.Location = new System.Drawing.Point(31, 48);
            this.lbTempoB.Name = "lbTempoB";
            this.lbTempoB.Size = new System.Drawing.Size(40, 13);
            this.lbTempoB.TabIndex = 1;
            this.lbTempoB.Text = "Tempo";
            // 
            // gbA
            // 
            this.gbA.Controls.Add(this.revA);
            this.gbA.Controls.Add(this.tbA);
            this.gbA.Controls.Add(this.lbRevA);
            this.gbA.Controls.Add(this.lbTempoA);
            this.gbA.Location = new System.Drawing.Point(12, 12);
            this.gbA.Name = "gbA";
            this.gbA.Size = new System.Drawing.Size(252, 179);
            this.gbA.TabIndex = 1;
            this.gbA.TabStop = false;
            this.gbA.Text = "Editor A";
            // 
            // lbRevA
            // 
            this.lbRevA.AutoSize = true;
            this.lbRevA.Location = new System.Drawing.Point(36, 118);
            this.lbRevA.Name = "lbRevA";
            this.lbRevA.Size = new System.Drawing.Size(48, 13);
            this.lbRevA.TabIndex = 3;
            this.lbRevA.Text = "Rev ch1";
            // 
            // lbTempoA
            // 
            this.lbTempoA.AutoSize = true;
            this.lbTempoA.Location = new System.Drawing.Point(31, 53);
            this.lbTempoA.Name = "lbTempoA";
            this.lbTempoA.Size = new System.Drawing.Size(40, 13);
            this.lbTempoA.TabIndex = 1;
            this.lbTempoA.Text = "Tempo";
            // 
            // gbC
            // 
            this.gbC.Controls.Add(this.revC);
            this.gbC.Controls.Add(this.tbC);
            this.gbC.Controls.Add(this.lbRevC);
            this.gbC.Controls.Add(this.lbTempoC);
            this.gbC.Location = new System.Drawing.Point(291, 12);
            this.gbC.Name = "gbC";
            this.gbC.Size = new System.Drawing.Size(252, 179);
            this.gbC.TabIndex = 1;
            this.gbC.TabStop = false;
            this.gbC.Text = "Editor C";
            // 
            // lbRevC
            // 
            this.lbRevC.AutoSize = true;
            this.lbRevC.Location = new System.Drawing.Point(41, 118);
            this.lbRevC.Name = "lbRevC";
            this.lbRevC.Size = new System.Drawing.Size(48, 13);
            this.lbRevC.TabIndex = 3;
            this.lbRevC.Text = "Rev ch1";
            // 
            // lbTempoC
            // 
            this.lbTempoC.AutoSize = true;
            this.lbTempoC.Location = new System.Drawing.Point(36, 53);
            this.lbTempoC.Name = "lbTempoC";
            this.lbTempoC.Size = new System.Drawing.Size(40, 13);
            this.lbTempoC.TabIndex = 1;
            this.lbTempoC.Text = "Tempo";
            // 
            // gbD
            // 
            this.gbD.Controls.Add(this.revD);
            this.gbD.Controls.Add(this.tbD);
            this.gbD.Controls.Add(this.lbRevD);
            this.gbD.Controls.Add(this.lbTempoD);
            this.gbD.Location = new System.Drawing.Point(291, 207);
            this.gbD.Name = "gbD";
            this.gbD.Size = new System.Drawing.Size(252, 179);
            this.gbD.TabIndex = 1;
            this.gbD.TabStop = false;
            this.gbD.Text = "Editor D";
            // 
            // lbRevD
            // 
            this.lbRevD.AutoSize = true;
            this.lbRevD.Location = new System.Drawing.Point(41, 113);
            this.lbRevD.Name = "lbRevD";
            this.lbRevD.Size = new System.Drawing.Size(48, 13);
            this.lbRevD.TabIndex = 3;
            this.lbRevD.Text = "Rev ch1";
            // 
            // lbTempoD
            // 
            this.lbTempoD.AutoSize = true;
            this.lbTempoD.Location = new System.Drawing.Point(36, 48);
            this.lbTempoD.Name = "lbTempoD";
            this.lbTempoD.Size = new System.Drawing.Size(40, 13);
            this.lbTempoD.TabIndex = 1;
            this.lbTempoD.Text = "Tempo";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(674, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Set Tempo";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbA
            // 
            this.tbA.Location = new System.Drawing.Point(95, 50);
            this.tbA.Name = "tbA";
            this.tbA.Size = new System.Drawing.Size(120, 20);
            this.tbA.TabIndex = 4;
            // 
            // tbB
            // 
            this.tbB.Location = new System.Drawing.Point(95, 45);
            this.tbB.Name = "tbB";
            this.tbB.Size = new System.Drawing.Size(120, 20);
            this.tbB.TabIndex = 4;
            // 
            // tbC
            // 
            this.tbC.Location = new System.Drawing.Point(100, 50);
            this.tbC.Name = "tbC";
            this.tbC.Size = new System.Drawing.Size(120, 20);
            this.tbC.TabIndex = 4;
            // 
            // tbD
            // 
            this.tbD.Location = new System.Drawing.Point(100, 45);
            this.tbD.Name = "tbD";
            this.tbD.Size = new System.Drawing.Size(120, 20);
            this.tbD.TabIndex = 4;
            // 
            // revA
            // 
            this.revA.Location = new System.Drawing.Point(95, 115);
            this.revA.Name = "revA";
            this.revA.Size = new System.Drawing.Size(120, 20);
            this.revA.TabIndex = 5;
            // 
            // revC
            // 
            this.revC.Location = new System.Drawing.Point(100, 115);
            this.revC.Name = "revC";
            this.revC.Size = new System.Drawing.Size(120, 20);
            this.revC.TabIndex = 5;
            // 
            // revD
            // 
            this.revD.Location = new System.Drawing.Point(100, 110);
            this.revD.Name = "revD";
            this.revD.Size = new System.Drawing.Size(120, 20);
            this.revD.TabIndex = 5;
            // 
            // revB
            // 
            this.revB.Location = new System.Drawing.Point(95, 110);
            this.revB.Name = "revB";
            this.revB.Size = new System.Drawing.Size(120, 20);
            this.revB.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.gbA);
            this.Controls.Add(this.gbD);
            this.Controls.Add(this.gbC);
            this.Controls.Add(this.gbB);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbB.ResumeLayout(false);
            this.gbB.PerformLayout();
            this.gbA.ResumeLayout(false);
            this.gbA.PerformLayout();
            this.gbC.ResumeLayout(false);
            this.gbC.PerformLayout();
            this.gbD.ResumeLayout(false);
            this.gbD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbB;
        private System.Windows.Forms.GroupBox gbA;
        private System.Windows.Forms.GroupBox gbC;
        private System.Windows.Forms.GroupBox gbD;
        private System.Windows.Forms.Label lbRevB;
        private System.Windows.Forms.Label lbTempoB;
        private System.Windows.Forms.Label lbRevA;
        private System.Windows.Forms.Label lbTempoA;
        private System.Windows.Forms.Label lbRevC;
        private System.Windows.Forms.Label lbTempoC;
        private System.Windows.Forms.Label lbRevD;
        private System.Windows.Forms.Label lbTempoD;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbB;
        private System.Windows.Forms.TextBox tbA;
        private System.Windows.Forms.TextBox tbC;
        private System.Windows.Forms.TextBox tbD;
        private System.Windows.Forms.TextBox revB;
        private System.Windows.Forms.TextBox revA;
        private System.Windows.Forms.TextBox revC;
        private System.Windows.Forms.TextBox revD;
    }
}

