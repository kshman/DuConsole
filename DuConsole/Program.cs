global using System;
global using System.Text;
global using System.Windows.Forms;

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
		string? filename = args.Length > 1 ? args[1] : null;
		ConsoleScript? cs = null;

#if DEBUG && true
		// D:\APPL\ksh\duc_test_cmd.duconsole
		filename = @"D:\APPL\ksh\duc_test_cmd.duconsole";
#endif

		if (!string.IsNullOrEmpty(filename))
		{
			cs = ConsoleScript.FromFile(filename);
			if (cs != null)
			{
				// 여기서 RUNAS 검사
				if (cs.RunAs && !Du.Platform.TestEnv.IsAdministrator)
				{
					ConsoleForm.RunAs(filename);
					return;
				}
			}
		}

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new ConsoleForm(cs));
	}
}

