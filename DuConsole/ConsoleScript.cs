using Du.Globalization;

namespace DuConsole;

public class ConsoleScript : IDisposable
{
	public string FileName { get; private set; } = string.Empty;
	public string TempFileName { get; private set; } = string.Empty;

	public string Name { get; private set; } = string.Empty;
	public ConsoleType Type { get; private set; } = ConsoleType.Unknown;
	public bool StartOnLoad { get; private set; }
	public bool RunAs { get; private set; }
	public bool AutoExit { get; private set; }

	public string[]? Lines { get; private set; }
	public string? Context { get; private set; }

	private ConsoleScript()
	{
	}

	public void Dispose()
	{
		Close();
		GC.SuppressFinalize(this);
	}

	public void Close()
	{
		UnlinkTempContext();
	}

	public static ConsoleScript? FromFile(string filename)
	{
		if (string.IsNullOrEmpty(filename))
			return null;

		try
		{
			var fi = new FileInfo(filename);

			if (!fi.Exists)
				return null;

			var s = new ConsoleScript();

			var detect = DetectConsoleType(fi);
			if (detect == ConsoleType.Unknown)
				return null;

			s.FileName = fi.FullName;
			s.Type = detect;

			switch (detect)
			{
				case ConsoleType.Console:
					if (!s.ReadConsoleFromFile())
						return null;
					break;

				case ConsoleType.Cmd or ConsoleType.PowerShell:
					if (!s.ReadFromFile())
						return null;
					break;
				
				default:
					return null;
			}

			return s;
		}
		catch
		{
			return null;
		}
	}

	private bool ReadFromFile()
	{
		if (string.IsNullOrEmpty(FileName))
			return false;

		Lines = File.ReadAllLines(FileName, ConsoleTypeToEncoding(Type));

		var sb = new StringBuilder();

		foreach (var l in Lines)
			sb.AppendLine(l);

		Context = sb.ToString();

		return true;
	}

	private bool ReadConsoleFromFile()
	{
		if (string.IsNullOrEmpty(FileName))
			return false;

		Lines = File.ReadAllLines(FileName);

		var ctx = new StringBuilder();
		var scp = new StringBuilder();
		var isdb = true;

		foreach (var l in Lines)
		{
			if (!isdb)
				scp.AppendLine(l);
			else if (l.Length == 0)
				isdb = false;
			else
				ctx.AppendLine(l);
		}

		if (scp.Length == 0)
			return false;

		Context = scp.ToString();

		if (ctx.Length <= 0) 
			return false;
		
		var db = LineDb.FromContext(ctx.ToString());

		Name = db.Get("name") ?? FileName;
		Type = StringToConsoleType(db.Get("type"));
		StartOnLoad = Converter.ToBool(db.Get("start"), StartOnLoad);
		RunAs = Converter.ToBool(db.Get("runas"), RunAs);
		AutoExit = Converter.ToBool(db.Get("autoexit"), AutoExit);

		return true;
	}

	public void MakeTempContext()
	{
		if (string.IsNullOrEmpty(Context) || !string.IsNullOrEmpty(TempFileName)) 
			return;
		
		var ext = ConsoleTypeToExtension(Type);
		TempFileName = $"{Path.GetTempPath()}\\DuConsole_{UnixTime.Tick}.{ext}";
		var encoding = ConsoleTypeToEncoding(Type);
		File.WriteAllText(TempFileName, Context, encoding);
	}

	private void UnlinkTempContext()
	{
		if (string.IsNullOrEmpty(TempFileName) || !File.Exists(TempFileName)) 
			return;
		
		File.Delete(TempFileName);
		TempFileName = string.Empty;
	}

	private static ConsoleType DetectConsoleType(FileInfo fileinfo)
	{
		return fileinfo.Extension.ToLower() switch
		{
			".duc" or ".duconsole" => ConsoleType.Console,
			".cmd" => ConsoleType.Cmd,
			".ps1" or ".pwsh" => ConsoleType.PowerShell,
			_ => ConsoleType.Unknown,
		};
	}

	private static ConsoleType StringToConsoleType(string? value)
	{
		if (string.IsNullOrEmpty(value))
			return ConsoleType.Unknown;

		return value.ToLower() switch
		{
			"cmd" => ConsoleType.Cmd,
			"ps" or "ps1" or "powershell" => ConsoleType.PowerShell,
			_ => ConsoleType.Unknown,
		};
	}

	private static string ConsoleTypeToExtension(ConsoleType type)
	{
		return type switch
		{
			ConsoleType.Cmd => "cmd",
			ConsoleType.PowerShell => "ps1",
			_ => string.Empty,
		};
	}

	private static Encoding ConsoleTypeToEncoding(ConsoleType type)
	{
		switch (type)
		{
			case ConsoleType.Cmd:
#if false
				return Encoding.Default;
#else
				var enc = CodePagesEncodingProvider.Instance.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage);
				return enc ?? Encoding.Default;
#endif
			
			case ConsoleType.PowerShell:
				return Encoding.UTF8;
			
			case ConsoleType.Unknown:
			case ConsoleType.Console:
			default:
				return Encoding.Default;
		}
	}

	public static bool ConsoleTypeToRuntime(ConsoleType type, out string? runtime, out string? argument)
	{
		switch (type)
		{
			case ConsoleType.Cmd:
				runtime = "cmd.exe";
				argument = "/c";
				break;

			case ConsoleType.PowerShell:
				runtime = "powershell.exe";
				argument = "-File";
				break;

			case ConsoleType.Unknown:
			case ConsoleType.Console:
			default:
				runtime = null;
				argument = null;
				return false;
		}

		return true;
	}
}

public enum ConsoleType
{
	Unknown,
	Console,
	Cmd,
	PowerShell,
}

