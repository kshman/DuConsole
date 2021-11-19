global using System.Text;

namespace DuConsole;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		var args = Environment.GetCommandLineArgs();
		string? filename = args.Length > 0 ? args[0] : null;
		ConsoleScript? cs = null;

#if DEBUG && false
		// D:\APPL\ksh\zcmd.duconsole
		filename = @"D:\APPL\ksh\zcmd.duconsole";
#endif

		if (!string.IsNullOrEmpty(filename))
		{
			cs = ConsoleScript.FromFile(filename);
			if (cs != null)
			{
				// 여기서 RUNAS 검사
				if (cs.RunAs && !Du.Platform.TestPlatform.IsAdministrator)
				{
					ConsoleForm.RunAs(filename);
					return;
				}
			}
		}

		ApplicationConfiguration.Initialize();
		Application.Run(new ConsoleForm(cs));
	}
}
