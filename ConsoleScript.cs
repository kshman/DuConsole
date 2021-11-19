using Du;
using Du.Data;

namespace DuConsole;

public class ConsoleScript : IDisposable
{
	public string? FileName { get; private set; } = null;
	public string? TempFileName { get; private set; } = null;

	public string? Name { get; private set; } = string.Empty;
	public ConsoleType Type { get; private set; } = ConsoleType.Unknown;
	public bool StartOnLoad { get; private set; } = false;
	public bool RunAs { get; private set; } = false;
	public bool AutoExit { get; private set; } = false;

	public string[]? Lines { get; private set; } = null;
	public string? Context { get; private set; } = null;

	private ConsoleScript()
	{
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
			Close();
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
			FileInfo fi = new(filename);

			if (!fi.Exists)
				return null;

			ConsoleScript s = new();

			var detect = DetectConsoleType(fi);
			if (detect == ConsoleType.Unknown)
				return null;

			s.FileName = fi.FullName;
			s.Type = detect;

			if (detect == ConsoleType.Unknown)
				return null;
			else if (detect == ConsoleType.Console)
			{
				if (!s.ReadConsoleFromFile())
					return null;
			}
			else if (detect == ConsoleType.Cmd || detect == ConsoleType.PowerShell)
			{
				if (!s.ReadFromFile())
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

		StringBuilder sb = new();

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

		StringBuilder ctx = new();
		StringBuilder scp = new();
		bool isdb = true;

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

		if (ctx.Length > 0)
		{
			var db = LineDb.FromContext(ctx.ToString(), false);

			Name = db.Get("name");
			Type = StringToConsoleType(db.Get("type"));
			StartOnLoad = Converter.ToBool(db.Get("start"), StartOnLoad);
			RunAs = Converter.ToBool(db.Get("runas"), RunAs);
			AutoExit = Converter.ToBool(db.Get("autoexit"), AutoExit);
		}

		return true;
	}

	public void MakeTempContext()
	{
		if (!string.IsNullOrEmpty(Context) && string.IsNullOrEmpty(TempFileName))
		{
			var ext = ConsoleTypeToExtension(Type);
			TempFileName = $"{Path.GetTempPath()}\\DuConsole_{TimeDu.UnixTimeNow}.{ext}";
			var encoding = ConsoleTypeToEncoding(Type);
			if (Type != ConsoleType.Cmd || encoding != Encoding.UTF8)
				File.WriteAllText(TempFileName, Context, encoding);
			else
			{
				var ChcpContext = "@chcp 65001" + Environment.NewLine + Context;
				File.WriteAllText(TempFileName, ChcpContext);
			}
		}
	}

	public void UnlinkTempContext()
	{
		if (!string.IsNullOrEmpty(TempFileName) && File.Exists(TempFileName))
		{
			File.Delete(TempFileName);
			TempFileName = null;
		}
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

	private static ConsoleType StringToConsoleType(string value)
	{
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
		return type switch
		{
#if false
			ConsoleType.Cmd => Encoding.Default,
#else
			ConsoleType.Cmd => Encoding.UTF8,
#endif
			ConsoleType.PowerShell => Encoding.UTF8,
			_ => Encoding.Unicode,
		};
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
