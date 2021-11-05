﻿using DuLib.Data;
using DuLib.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuConsole
{
	public class ConsoleScript : IDisposable
	{
		public string FileName { get; private set; } = null;
		public string TempFileName { get; private set; } = null;

		public string Name { get; private set; } = string.Empty;
		public ConsoleType Type { get; private set; } = ConsoleType.Unknown;
		public bool StartOnLoad { get; private set; } = false;
		public bool RunAs { get; private set; } = false;
		public bool AutoExit { get; private set; } = false;

		public string[] Lines { get; private set; } = null;
		public string Context { get; private set; } = null;

		private ConsoleScript()
		{
		}

		public void Dispose()
		{
			Close();
		}

		public void Close()
		{
			UnlinkTempContext();
		}

		public static ConsoleScript FromFile(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return null;

			ConsoleScript s;

			try
			{
				FileInfo fi = new FileInfo(filename);

				if (!fi.Exists)
					return null;

				s = new ConsoleScript();

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
			}
			catch
			{
				s = null;
			}

			return s;
		}

		private bool ReadFromFile()
		{
			Lines = File.ReadAllLines(FileName, ConsoleTypeToEncoding(Type));

			StringBuilder sb = new StringBuilder();

			foreach (var l in Lines)
				sb.AppendLine(l);

			Context = sb.ToString();

			return true;
		}

		private bool ReadConsoleFromFile()
		{
			Lines = File.ReadAllLines(FileName);

			StringBuilder ctx = new StringBuilder();
			StringBuilder scp = new StringBuilder();
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
			if (!string.IsNullOrEmpty(Context))
			{
				var ext = ConsoleTypeToExtension(Type);
				TempFileName = $"{Path.GetTempPath()}\\DuConsole_{TimeSupp.UnixTimeNow}.{ext}";
				File.WriteAllText(TempFileName, Context, ConsoleTypeToEncoding(Type));
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
			switch (fileinfo.Extension.ToLower())
			{
				case ".duc":
				case ".duconsole": return ConsoleType.Console;
				case ".cmd": return ConsoleType.Cmd;
				case ".ps1":
				case ".pwsh": return ConsoleType.PowerShell;
				default: return ConsoleType.Unknown;
			}
		}

		private static ConsoleType DetectConsoleType(string filename)
		{
			return DetectConsoleType(new FileInfo(filename));
		}

		private static ConsoleType StringToConsoleType(string value)
		{
			switch (value.ToLower())
			{
				case "cmd": return ConsoleType.Cmd;
				case "ps":
				case "ps1":
				case "powershell": return ConsoleType.PowerShell;
				default: return ConsoleType.Unknown;
			}
		}

		private static string ConsoleTypeToExtension(ConsoleType type)
		{
			switch (type)
			{
				case ConsoleType.Cmd: return "cmd";
				case ConsoleType.PowerShell: return "ps1";
				default: return string.Empty;
			}
		}

		private static Encoding ConsoleTypeToEncoding(ConsoleType type)
		{
			switch (type)
			{
				case ConsoleType.Cmd: return Encoding.Default;
				case ConsoleType.PowerShell: return Encoding.UTF8;
				default: return Encoding.Unicode;
			}
		}

		public static bool ConsoleTypeToRuntime(ConsoleType type, out string runtime, out string argument)
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
}
