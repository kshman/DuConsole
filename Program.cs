using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuConsole
{
	internal static class Program
	{
		/// <summary>
		/// 해당 애플리케이션의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main(string[] Args)
		{
			string filename = Args.Length > 0 ? Args[0] : null;
			ConsoleScript cs = null;

#if DEBUG && true
			// D:\APPL\ksh\zcmd.duconsole
			filename = @"D:\APPL\ksh\zcmd.duconsole";
#endif

			if (!string.IsNullOrEmpty(filename))
			{
				cs = ConsoleScript.FromFile(filename);
				if (cs != null)
				{
					// 여기서 RUNAS 검사
					if (cs.RunAs && !DuLib.System.TestSystem.IsAdministrator)
					{
						// 여기서 안해도 되나
					}
				}
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(cs));
		}
	}
}
