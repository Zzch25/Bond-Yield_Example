/*
 *Zachary Job
 *main.cs
 *2/1/17
 *
 *Provide definitions
 */

using System;
using System.Runtime.InteropServices;

namespace fiyield
{
    public class shared
    {
        public const int
            shared_double_precision = 7;
        public int
            shared_double_precision_gui
        {get;} = 7;

		public const int
			shared_sampling_precision = 1;

        //General help messages
        public const string
            shared_view_help = "Try \"<name.exe> --help\" for commands...",
            shared_help_message = @"To run...\n
			For price queries using coupon percentage, years intended, the face value, and the rate or yield. The query and result can be logged with the
optional switc\n
			<name.exe> --price
				Required additional fields
					-coupon=<Percentage, Double type>
					-years=<Value, Int type>
					-face=<Value, Double type>
					-rate=<Value, Double type>
				Optional
					-log-query\n
			For yield queries using coupon percentage, years intended, the face value, and the price. The query and result can be logged with the
optional switch\n
			<name.exe> --yield
				Required additional fields
					-coupon=<Percentage, Double type>
					-years=<Value, Int type>
					-face=<Value, Double type>
					-price=<Value, Double type>
				Optional
					-log-query\n
		Additional log information...
			fiyield
				--log-wipe
				--log-print",
            shared_continue_dialog = "Press Enter To Continue...\n";
        public string
            shared_gui_help
        { get; } = "Please input appropriate values into Price<Double>, Rate<Double>, Coupon<Double>, Face<Double>, and Years<Int>";
        public string
            shared_gui_log_wipe
        { get; } = "Log wiped";
 

        //Logging rationales
        public const string
            shared_error_too_few_parameters = "Parameters are missing",
            shared_error_mode_unspecified = "Mode not specified",
            shared_error_bad_price = "Bad price value",
            shared_error_bad_rate = "Bad yield value",
            shared_error_bad_coupon = "Bad coupon value",
            shared_error_bad_years = "Bad years value",
            shared_error_bad_face = "Bad face value";
        public string
            shared_error_log_get_failed
        { get; } = "Could not get log";

		//Setup definitions for switches
		public const string
			shared_price_switch = "--price",
			shared_yield_switch = "--yield",
			shared_sub_coupon_switch = "-coupon=",
			shared_sub_years_switch = "-years=",
			shared_sub_face_switch = "-face=",
			shared_sub_price_switch = "-price=",
			shared_sub_rate_switch = "-rate=",
			shared_log_switch = "-log-query",
			shared_log_wipe_switch = "--log-wipe",
			shared_log_print_switch = "--log-print",
			shared_help_switch = "--help";

		//Make switches readable
		public enum
			shared_switch_types
			{
				Integer = 0,
				Double,
				String,
				Empty
			};
	
		//Additional function definitions if bypass
		public enum
			shared_bypass_function_types
			{
				LogWipe = 0,
				LogPrint
			};

		//For map structure, I don't expect collisions
		//at such a low number
		/*[StructLayout(LayoutKind.Explicit)]
		public struct shared_switch_union
		{
			 [FieldOffset(0)]
			 public int uInteger;

			 [FieldOffset(0)]
			 public double uDouble;

			 [FieldOffset(0)]
			 public string uString;
		}*/

		//Logging file information
		public const string
			shared_descriptor_name = "./_log.txt";

        internal static class EnterConsole
        {
            internal const int RETURN = 0x0D;
            internal const int KEYDOWN = 0x100;

            [DllImport("kernel32.dll")]
            internal static extern bool FreeConsole();
            [DllImport("kernel32.dll")]
            internal static extern bool AttachConsole(int id);
            [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
            internal static extern bool PostMessage(IntPtr ptr, uint msg, int p0, int p1);
            [DllImport("user32.dll")]
            internal static extern IntPtr GetForegroundWindow();
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern uint GetWindowThreadProcessId(IntPtr ptr, out int id);
        }
    }
}
