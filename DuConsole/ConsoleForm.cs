using Du;
using Du.Platform;
using Du.WinForms;
using System.Diagnostics;

namespace DuConsole;

public partial class ConsoleForm : Form
{
	private readonly string _title = "DuConsole";

	private string? _filename;
	private string? _last_folder;

	private Process? _ps;
	private ConsoleScript? _cs;
	
	private readonly BadakFormWorker _bfw;

	public ConsoleForm(ConsoleScript? cs)
	{
		InitializeComponent();

		Opacity = 0;

		SystemButton.Form = this;
		_bfw = new BadakFormWorker(this, SystemButton);

		//
		_cs = cs;

		//
		var fonts = new[]
		{
				"Bitstream Vera Sans Mono",
				"Consolas",
				"Tahoma",
			};
		var n = TestFont.IsInstalled(fonts);
		if (n >= 0)
			txtOutput.Font = new Font(fonts[n], 9.0F, FontStyle.Regular, GraphicsUnit.Point);
		else
		{
			// 오우 어떤 글꼴을 써야한단 말인가
		}
	}

	private void ConsoleForm_Load(object sender, EventArgs e)
	{
		KeyPreview = true;

		if (!TestEnv.IsAdministrator)
			lblAdmin.Visible = false;
		else
		{
			lblAdmin.Text = "RUNAS";
			lblAdmin.BackColor = Color.Red;
		}

		using (var rk = new RegKey("PuruLive\\DuConsole"))
		{
			if (rk.IsOpen)
			{
				string? value;

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

				if (string.IsNullOrEmpty(_last_folder))
				{
					value = rk.GetDecodingString("LastFolder");
					if (!string.IsNullOrEmpty(value))
					{
						if (Directory.Exists(value))
							_last_folder = value;
					}
				}
			}
		}

		//
		if (!PrepareScript())
		{
			// 이게 참이 었다면, prepare에서 실행한것을 의미함
			if (_cs == null)
				LogLine(Color.SeaGreen, "스크립트부터 열어 두시지?");
			else
			{
				_cs.MakeTempContext();

				LogLine(Color.Blue, $"{_filename}{Environment.NewLine}");
				if (_cs.Lines != null)
					foreach (string l in _cs.Lines)
						LogLine(Color.Teal, l);
			}
		}
	}

	private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
	{

	}

	private void ConsoleForm_FormClosed(object sender, FormClosedEventArgs e)
	{
		if (_ps != null)
			_ps.Kill();

		if (_cs != null)
			_cs.Close();

		using var rk = new RegKey("PuruLive\\DuConsole", true);

		rk.SetString("Window", $"{Location.X},{Location.Y},{Size.Width},{Size.Height}");

		if (!string.IsNullOrEmpty(_last_folder))
			rk.SetEncodingString("LastFolder", _last_folder);
	}

	private void ConsoleForm_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			WaitProcess();
			Close();
		}
	}

	private void SystemButton_CloseOrder(object sender, EventArgs e)
	{
		// 시스템 버튼에서 알아서 닫아줌
		// 왜 함수 만들엇지...
	}

	private void TopPanel_MouseDown(object sender, MouseEventArgs e)
	{
		_bfw.DragOnDown(e);
	}

	private void TopPanel_MouseUp(object sender, MouseEventArgs e)
	{
		_bfw.DragOnUp(e);
	}

	private void TopPanel_MouseMove(object sender, MouseEventArgs e)
	{
		_bfw.DragOnMove(e);
	}

	protected override void WndProc(ref Message m)
	{
		if (!_bfw.WndProc(ref m))
		{
			FormDu.MagneticDockForm(ref m, this, 15);
			base.WndProc(ref m);
		}
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		FormDu.EffectAppear(this);
	}

	public override string Text
	{
		get => base.Text;
		set
		{
			base.Text = value;
			TitleLabel.Text = value;
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

	private void MiRegisterExtension_Click(object sender, EventArgs e)
	{
		var r = MessageBox.Show(this, "DuConsole용 확장자를 등록할깝숑?", "알려주시와요", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

		if (r != DialogResult.Yes)
			return;

		RegKey.RegisterExtension(".duc", "DuConsole.duc", "DuConsole script", Application.ExecutablePath);
		RegKey.RegisterExtension(".duconsole", "DuConsole.duconsole", "DuConsole script", Application.ExecutablePath);
	}

	private void InvokeLog(string? text)
	{
		Invoke(new Action(() =>
		{
			txtOutput.AppendText(text);
		}));
	}

	private void InvokeLog(Color color, string? text)
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

	public void LogWrite(Color color, string? text)
	{
		if (txtOutput.IsDisposed)
			return;

		InvokeLog(color, text);
	}

	public void LogLine(Color color, string? text)
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

	public void LogLine(string? text)
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
				Title = "콘솔 스크립트를 고르세요",
				Filter = "콘솔 스크립트|*.duc;*.duconsole;*.cmd;*.ps1|모든 파일|*.*",
				CheckFileExists = true,
				CheckPathExists = true,
				Multiselect = false,
				InitialDirectory = _last_folder,
			};

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				var fi = new FileInfo(dlg.FileName);
				_last_folder = fi.Directory?.FullName;

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

	private ConsoleScript? ReadScript(string filename)
	{
		var cs = ConsoleScript.FromFile(filename);

		if (cs == null)
		{
			LogWrite(Color.Red, "스크립트 읽기 실패: ");
			LogLine(filename);
		}
		else
		{
			if (cs.RunAs && !TestEnv.IsAdministrator)
			{
				// RUNAS!!!
				RunAs(filename);
				Close();
				return null;
			}

			cs.MakeTempContext();

			LogLine(Color.Blue, $"{filename}{Environment.NewLine}");
			if (cs.Lines != null)
				foreach (var l in cs.Lines)
					LogLine(Color.Teal, l);
		}

		return cs;
	}

	private bool PrepareScript()
	{
		if (_cs == null)
		{
			Text = _title;
			btnDoit.Text = "열기";
			miClose.Enabled = false;

			_filename = null;

			return false;
		}
		else
		{
			Text = _cs.Name;
			btnDoit.Text = "실행";
			miClose.Enabled = true;

			_filename = _cs.FileName;
			_last_folder = _cs.FileName != null ? new FileInfo(_cs.FileName).DirectoryName : null;

			// start 처리 여기서 하면 됨
			if (!_cs.StartOnLoad)
				return false;
			else
			{
				_cs.MakeTempContext();
				ThreadIt();
				return true;
			}
		}
	}

	private void ThreadIt()
	{
		btnDoit.Enabled = false;

		txtOutput.Clear();

		var thd = new Thread(RunAsAsync);
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
		int exitcode;

		if (_ps == null)
			exitcode = 0;
		else
		{
			exitcode = _ps.ExitCode;

			_ps.Close();
			_ps = null;
		}

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

		var thd = new Thread(() =>
		{
			Thread.Sleep(1000);
			Invoke(new Action(() => Close()));
		});
		thd.Start();
	}

	//
	public static void RunAs(string filename)
	{
		var ps = new Process();
		ps.StartInfo.FileName = Application.ExecutablePath;
		ps.StartInfo.Arguments = filename;
		ps.StartInfo.WorkingDirectory = Application.StartupPath;
		ps.StartInfo.UseShellExecute = true;
		ps.StartInfo.Verb = "runas";
		ps.Start();
	}
}

