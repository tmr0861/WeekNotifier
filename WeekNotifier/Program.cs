using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WeekNotifier
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );

			// Create the main form, but don't show it.
			using ( MainForm mainForm = new MainForm() )
			{
				Application.Run();
			}
		}
	}
}
