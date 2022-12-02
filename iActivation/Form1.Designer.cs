
namespace iActivation
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.CscTypeBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BypassWorker = new System.ComponentModel.BackgroundWorker();
            this.ChangeWorker = new System.ComponentModel.BackgroundWorker();
            this.DeviceProg = new System.Windows.Forms.ProgressBar();
            this.USBDevices = new System.Windows.Forms.ComboBox();
            this.ReadUSB = new System.ComponentModel.BackgroundWorker();
            this.BypassLock = new System.Windows.Forms.Button();
            this.ChangeCsc = new System.Windows.Forms.Button();
            this.RichText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(364, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "Change Csc:";
            // 
            // CscTypeBox
            // 
            this.CscTypeBox.Location = new System.Drawing.Point(441, 26);
            this.CscTypeBox.Multiline = true;
            this.CscTypeBox.Name = "CscTypeBox";
            this.CscTypeBox.Size = new System.Drawing.Size(277, 21);
            this.CscTypeBox.TabIndex = 10;
            this.CscTypeBox.Text = "XAA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "USB Port:";
            // 
            // BypassWorker
            // 
            this.BypassWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BypassWorker_DoWork);
            // 
            // ChangeWorker
            // 
            this.ChangeWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ChangeWorker_DoWork);
            // 
            // DeviceProg
            // 
            this.DeviceProg.Location = new System.Drawing.Point(-9, 264);
            this.DeviceProg.Name = "DeviceProg";
            this.DeviceProg.Size = new System.Drawing.Size(799, 21);
            this.DeviceProg.TabIndex = 16;
            // 
            // USBDevices
            // 
            this.USBDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.USBDevices.FormattingEnabled = true;
            this.USBDevices.Location = new System.Drawing.Point(70, 26);
            this.USBDevices.Name = "USBDevices";
            this.USBDevices.Size = new System.Drawing.Size(277, 21);
            this.USBDevices.TabIndex = 17;
            // 
            // ReadUSB
            // 
            this.ReadUSB.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReadDevice);
            // 
            // BypassLock
            // 
            this.BypassLock.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BypassLock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BypassLock.Location = new System.Drawing.Point(70, 203);
            this.BypassLock.Name = "BypassLock";
            this.BypassLock.Size = new System.Drawing.Size(277, 43);
            this.BypassLock.TabIndex = 18;
            this.BypassLock.Text = "BYPASS FRP";
            this.BypassLock.UseVisualStyleBackColor = true;
            this.BypassLock.Click += new System.EventHandler(this.BypassLock_Click);
            // 
            // ChangeCsc
            // 
            this.ChangeCsc.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeCsc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ChangeCsc.Location = new System.Drawing.Point(441, 203);
            this.ChangeCsc.Name = "ChangeCsc";
            this.ChangeCsc.Size = new System.Drawing.Size(277, 43);
            this.ChangeCsc.TabIndex = 19;
            this.ChangeCsc.Text = "CHANGE CSC";
            this.ChangeCsc.UseVisualStyleBackColor = true;
            this.ChangeCsc.Click += new System.EventHandler(this.ChangeCsc_Click);
            // 
            // RichText
            // 
            this.RichText.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.RichText.ForeColor = System.Drawing.Color.Black;
            this.RichText.Location = new System.Drawing.Point(72, 68);
            this.RichText.Name = "RichText";
            this.RichText.Size = new System.Drawing.Size(646, 116);
            this.RichText.TabIndex = 20;
            this.RichText.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 281);
            this.Controls.Add(this.RichText);
            this.Controls.Add(this.ChangeCsc);
            this.Controls.Add(this.BypassLock);
            this.Controls.Add(this.USBDevices);
            this.Controls.Add(this.DeviceProg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CscTypeBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAMSUNG FRP V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormClosed);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CscTypeBox;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker BypassWorker;
        private System.ComponentModel.BackgroundWorker ChangeWorker;
        private System.Windows.Forms.ProgressBar DeviceProg;
        private System.Windows.Forms.ComboBox USBDevices;
        private System.ComponentModel.BackgroundWorker ReadUSB;
        private System.Windows.Forms.Button BypassLock;
        private System.Windows.Forms.Button ChangeCsc;
        private System.Windows.Forms.RichTextBox RichText;
    }
}

