/*
 *Zachary Job
 *main.cs
 *2/1/17
 *
 *The stub for setting up the application
 */

using System;
using System.Diagnostics;

namespace fiyield
{
	internal class mains : shared
	{
		/*
		 *Stub for passing off execution to the determined
		 *classes
		 *
		 *@PARAM: The user argument list
		 */
		static void Main(string[] args)
		{
			bool
				mains_cli_initialized;

            int
                mains_id;

			double
				mains_result;

			cli
				mains_cli;

            draw
                mains_draw;

            Process
                mains_process;

            IntPtr
                mains_ptr;

            mains_cli_initialized = false;

			if(args.Length == 0 || args[0] == "--gui")
                mains_draw = new draw();
			else
			{
                mains_ptr = EnterConsole.GetForegroundWindow();
                EnterConsole.GetWindowThreadProcessId(mains_ptr, out mains_id);
                mains_process = Process.GetProcessById(mains_id);
                EnterConsole.AttachConsole(-1);

                Console.WriteLine();

				mains_cli = new cli(args, ref mains_cli_initialized);

				if(mains_cli_initialized)
				{
					mains_result = mains_cli.cliExec();
					//ONLY cramp precision here to keep accuracy
					mains_result = Math.Round(mains_result, shared_double_precision);
					Console.WriteLine(mains_result.ToString());
				}
				else
					mains_cli.cliHelp();

                EnterConsole.FreeConsole();
                var temp = mains_process.MainWindowHandle;
                EnterConsole.PostMessage(temp, EnterConsole.KEYDOWN, EnterConsole.RETURN, 0);
            }
		}
	}
}
