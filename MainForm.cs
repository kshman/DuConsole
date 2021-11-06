using DuLib.Data;
using DuLib.System;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace DuConsole
{
	public partial class MainForm : Form
	{
		private readonly string _title = "DuConsole";

		private string _filename;
		private string _lastdir;

		private Process _ps;
		private ConsoleScript _cs;

		public MainForm(ConsoleScript cs)
		{
			InitializeComponent();

			//
			_cs = cs;

			//
			if (TestSystem.IsFontInstalled("Bitstream Vera Sans Mono"))
				txtOutput.Font = new Font("Bitstream Vera Sans Mono", 9.0F, FontStyle.Regular, GraphicsUnit.Point);
			else if (TestSystem.IsFontInstalled("Consolas"))
				txtOutput.Font = new Font("Consolas", 9.0F, FontStyle.Regular, GraphicsUnit.Point);
			else if (TestSystem.IsFontInstalled("Tahoma"))
				txtOutput.Font = new Font("Consolas", 9.0F, FontStyle.Regular, GraphicsUnit.Point);
			else
			{
				// 오우 어떤 글꼴을 써야한단 말인가
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			KeyPreview = true;

			lblAdmin.Text = TestSystem.IsAdministrator ? "RUNAS" : "";

			using (RegKey rk = new RegKey("PuruLive\\DuConsole"))
			{
				if (rk.IsOpen)
				{
					string value;

					value = rk.GetString("Window");
					if (!string.IsNullOrEmpty(value))
					{
						string[] ss = value.Split(',');
						if (ss.Length == 4)
						{
							Location = new Point(Converter.ToInt(ss[0], Location.X), Converter.ToInt(ss[1], Location.Y));
							Size = new Size(Converter.ToInt(ss[2], Size.Width), Converter.ToInt(ss[3], Size.Height));
						}
					}

					if (string.IsNullOrEmpty(_lastdir))
					{
						value = rk.GetString("LastDir");
						if (!string.IsNullOrEmpty(value))
						{
							value = Converter.Base64Decoding(value);
							if (Directory.Exists(value))
								_lastdir = value;
						}
					}
				}
			}

			//
			PrepareScript();

			if (_cs == null)
				LogLine(Color.SeaGreen, "Open script first");
			else
			{
				_cs.MakeTempContext();

				LogLine(Color.Blue, $"{_filename}{Environment.NewLine}");
				foreach (var l in _cs.Lines)
					LogLine(Color.Teal, l);
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_ps != null)
				_ps.Kill();

			if (_cs != null)
				_cs.Close();

			using (RegKey rk = new RegKey("PuruLive\\DuConsole", true))
			{
				rk.SetString("Window", $"{Location.X},{Location.Y},{Size.Width},{Size.Height}");

				if (!string.IsNullOrEmpty(_lastdir))
					rk.SetString("LastDir", Converter.Base64Encoding(_lastdir));
			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				WaitProcess();
				Close();
			}
		}

		private void BtnDoit_Click(object sender, EventArgs e)
		{
			RunIt();
		}

		private void MiRun_Click(object sender, EventArgs e)
		{
			RunIt();
		}

		private void MiClose_Click(object sender, EventArgs e)
		{
			CloseIt();
		}

		private void MiExit_Click(object sender, EventArgs e)
		{
			if (btnDoit.Enabled)
				Close();
		}

		private void miRegisterExtension_Click(object sender, EventArgs e)
		{
			var r = MessageBox.Show(this, "Register extensions for DuConsole. Continue?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (r != DialogResult.Yes)
				return;

			RegKey.RegisterExtension(".duc", "DuConsole.duc", "DuConsole script", Application.ExecutablePath);
			RegKey.RegisterExtension(".duconsole", "DuConsole.duconsole", "DuConsole script", Application.ExecutablePath);
		}

		private void InvokeLog(string text)
		{
			Invoke(new Action(() =>
			{
				txtOutput.AppendText(text);
			}));
		}

		private void InvokeLog(Color color, string text)
		{
			Invoke(new Action(() =>
			{
				txtOutput.SelectionColor = color;
				txtOutput.SelectionStart = txtOutput.TextLength;
				txtOutput.SelectionLength = 0;
				txtOutput.AppendText(text);
				txtOutput.SelectionColor = txtOutput.ForeColor;
			}));
		}

		public void LogWrite(Color color, string text)
		{
			if (txtOutput.IsDisposed)
				return;

			InvokeLog(color, text);
		}

		public void LogLine(Color color, string text)
		{
			if (txtOutput.IsDisposed)
				return;

			InvokeLog(color, text + Environment.NewLine);
		}

		public void LogWrite(string text)
		{
			if (txtOutput.IsDisposed)
				return;

			InvokeLog(text);
		}

		public void LogLine(string text)
		{
			if (txtOutput.IsDisposed)
				return;

			InvokeLog(text + Environment.NewLine);
		}

		//
		private void CloseIt()
		{
			if (_cs != null)
			{
				WaitProcess();

				_cs.Close();
				_cs = null;

				PrepareScript();

				txtOutput.Clear();
				LogWrite(Color.DarkKhaki, "Script closed");
			}
		}

		// 진짜 두잇
		private void RunIt()
		{
			if (_cs == null)
			{
				var dlg = new OpenFileDialog()
				{
					Title = "Select console script",
					Filter = "Console script|*.duc;*.duconsole;*.cmd;*.ps1|All files|*.*",
					CheckFileExists = true,
					CheckPathExists = true,
					Multiselect = false,
					InitialDirectory = _lastdir,
				};

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					FileInfo fi = new FileInfo(dlg.FileName);
					_lastdir = fi.Directory.FullName;

					txtOutput.Clear();

					_cs = ReadScript(dlg.FileName);
					PrepareScript();
				}
			}
			else
			{
				ThreadIt();
			}
		}

		private void WaitProcess()
		{
			if (_ps != null)
				_ps.WaitForExit();
		}

		private ConsoleScript ReadScript(string filename)
		{
			ConsoleScript cs = ConsoleScript.FromFile(filename);

			if (cs == null)
			{
				LogWrite(Color.Red, "Script read failed: ");
				LogLine(filename);
			}
			else
			{
				if (cs.RunAs && !DuLib.System.TestSystem.IsAdministrator)
				{
					// RUNAS!!!
					RunAs(filename);
					Close();
					return null;
				}

				cs.MakeTempContext();

				LogLine(Color.Blue, $"{filename}{Environment.NewLine}");
				foreach (var l in cs.Lines)
					LogLine(Color.Teal, l);
			}

			return cs;
		}

		private void PrepareScript()
		{
			if (_cs == null)
			{
				Text = _title;
				btnDoit.Text = "Open";
				miClose.Enabled = false;

				_filename = null;
			}
			else
			{
				Text = $"{_title} - {_cs.Name}";
				btnDoit.Text = "Run";
				miClose.Enabled = true;

				_filename = _cs.FileName;
				_lastdir = (new FileInfo(_cs.FileName)).DirectoryName;

				// start 처리 여기서 하면 됨
				if (_cs.StartOnLoad)
					ThreadIt();
			}
		}

		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		private void ThreadIt()
		{
			btnDoit.Enabled = false;

			txtOutput.Clear();

			Thread thd = new Thread(RunAsAsync);
			thd.Start();
		}

		private void RunAsAsync()
		{
			if (_cs == null)
			{
				LogWrite(Color.Red, "No script. Open first");
				return;
			}

			if (_ps != null)
			{
				// 헐랭 실행중
				return;
			}

			if (!ConsoleScript.ConsoleTypeToRuntime(_cs.Type, out var runtime, out var argument))
			{
				LogWrite(Color.Red, $"Invalid console runtime: {_cs.Type}");
				return;
			}

			_ps = new Process();

			_ps.StartInfo.FileName = runtime;
			_ps.StartInfo.Arguments = $"{argument} {_cs.TempFileName}";
			_ps.StartInfo.UseShellExecute = false;
			_ps.StartInfo.CreateNoWindow = true;
			_ps.StartInfo.RedirectStandardOutput = true;
			_ps.StartInfo.RedirectStandardError = true;

			if (_cs.RunAs)
				_ps.StartInfo.Verb = "runas";

			_ps.EnableRaisingEvents = true;
			_ps.OutputDataReceived += (s, e) => LogLine(e.Data);
			_ps.ErrorDataReceived += (s, e) => LogLine(Color.Red, e.Data);
			_ps.Exited += (s, e) => ProcessExited();

			_ps.Start();
			_ps.BeginOutputReadLine();
			_ps.BeginErrorReadLine();
		}

		private void ProcessExited()
		{
			int exitcode = _ps.ExitCode;

			_ps.Close();
			_ps = null;

			// 당연하지만 종료가 메시지 보다 빨리 올 가능성 99.999%
			// TxtOutputAppendText($"{Environment.NewLine}프로그램 종료: {exitcode}");
			Debug.WriteLine($"Exit code: {exitcode}");

			//
			if (!btnDoit.InvokeRequired)
				btnDoit.Enabled = true;
			else
				btnDoit.Invoke(new Action(() => btnDoit.Enabled = true));

			AutoExitIt();
		}

		private void AutoExitIt()
		{
			if (_cs == null || !_cs.AutoExit)
				return;

			Thread thd = new Thread(() =>
			{
				Thread.Sleep(1000);
				Invoke(new Action(() => Close()));
			});
			thd.Start();
		}

		//
		public static void RunAs(string filename)
		{
			Process ps = new Process();
			ps.StartInfo.FileName = Application.ExecutablePath;
			ps.StartInfo.Arguments = filename;
			ps.StartInfo.WorkingDirectory = Application.StartupPath;
			ps.StartInfo.UseShellExecute = true;
			ps.StartInfo.Verb = "runas";
			ps.Start();
		}
	}
}
