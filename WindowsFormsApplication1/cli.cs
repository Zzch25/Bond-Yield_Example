/*
 *Zachary Job
 *cli.cs
 *2/1/17
 *
 *A generic CLI object for ease of CLI operations
 */

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace fiyield
{
	internal class cli : shared 
	{
		private bool
			cli_is_price_mode,
			cli_log_opened,
			cli_logging_enabled,
			cli_function_bypass,
			cli_help_toggle;

		private int
			cli_years;

		private double
			cli_coupon,
			cli_face,
			cli_price,
			cli_rate;

		private shared_bypass_function_types
			cli_bypass_function_mode;

		private file
			cli_logger;

		private calculator
			cli_calculator;

		/*
		 *A basic initialization to setup the CLI
		 *with the user specified data
		 *
		 *@PARAM: The user argument list
		 *@PARAM: Whether or not the execution suceeded
		 */
		internal cli(string[] args, ref bool status)
		{
			string
				cli_strip;

			cli_strip = "";
			status = true;
			cli_is_price_mode = false;
			cli_logging_enabled = true;
			cli_function_bypass = false;
			cli_log_opened = false;
			cli_help_toggle = false;

			cli_calculator = new calculator();

			if(-1 < Array.FindIndex(args, s => s.Contains(shared_log_switch)))
				cli_logging_enabled = true;

			cli_logger = new file(ref cli_log_opened, cli_logging_enabled);
			if(cli_logging_enabled && !cli_log_opened)
				Console.WriteLine("<!>Error Opening Log File<!>");
			cli_logging_enabled &= cli_log_opened;
			cli_logger.toggleSTDOUT();

			if(-1 < Array.FindIndex(args, s => s.Contains(shared_log_wipe_switch)))
			{
				cli_bypass_function_mode = shared_bypass_function_types.LogWipe;
				cli_function_bypass = true;

				cli_logger.toggleLog();
			}
			if(-1 < Array.FindIndex(args, s => s.Contains(shared_log_print_switch)))
			{
				cli_bypass_function_mode = shared_bypass_function_types.LogPrint;
				cli_function_bypass = true;
				
				cli_logger.toggleLog();
			}

			if(-1 < Array.FindIndex(args, s => s.Contains(shared_help_switch)))
			{
				cli_help_toggle = true;
				status = false;
			}

			if(status && !cli_function_bypass)
			{
				//Check primary modes
				if(args.Length < 5)
				{
					status = false;
					cli_logger.writeLine(shared_error_too_few_parameters);	
				}

				if(status && -1 < Array.FindIndex(args, s => s.Contains(shared_price_switch)))
					cli_is_price_mode = true;
			}

			//<!><!><!><!><!><!><!><!><!><!><!><!><!><!><!>
			//Could be adapted to decltype & template loop
			//Could be adapted to decltype & template loop
			//Could be adapted to decltype & template loop
			//<!><!><!><!><!><!><!><!><!><!><!><!><!><!><!>
			
			//Verify a primary mode was valid
			if	(
					status &&
					!cli_is_price_mode &&
					!cli_function_bypass &&
					0 > Array.FindIndex(args, s => s.Contains(shared_yield_switch))
				)
			{
				status = false;
				cli_logger.writeLine(shared_error_mode_unspecified);
			}

			//Get further mode paramters or bypass if not needed
			if(status && !cli_function_bypass)
			{
                if (cli_is_price_mode && status && !attemptParse<double>(ref status, ref cli_strip, ref args, ref cli_rate, shared_sub_rate_switch))
                    cli_logger.writeLine(shared_error_bad_rate);
                if (!cli_is_price_mode && status && !attemptParse<double>(ref status, ref cli_strip, ref args, ref cli_price, shared_sub_price_switch))
                    cli_logger.writeLine(shared_error_bad_price);
                if (status && !attemptParse<double>(ref status, ref cli_strip, ref args, ref cli_coupon, shared_sub_coupon_switch))
                    cli_logger.writeLine(shared_error_bad_coupon);
                if (status && !attemptParse<int>(ref status, ref cli_strip, ref args, ref cli_years, shared_sub_years_switch))
                    cli_logger.writeLine(shared_error_bad_years);
                if (status && !attemptParse<double>(ref status, ref cli_strip, ref args, ref cli_face, shared_sub_face_switch))
                    cli_logger.writeLine(shared_error_bad_face);

                //Optionals - can fail outright and be ignored
                if (status && getIfValid
					(
						args, 
						shared_log_switch, 
						shared_log_switch, 
						ref cli_strip, 
						shared_switch_types.Empty
					))
					cli_logger.toggleLog();
			}
		}

        /*
         *A quick template to shrink the code size, attempt tryparse as
         *additional measure on inputs
         * 
         *@PARAM: The return status
         *@PARAM: The text to get
         *@PARAM: The system arguments
         */
        internal bool attemptParse<T>(ref bool status, ref string cli_strip, ref string[] args, ref T var, string type)
        {
            shared_switch_types
                attemptParse_mode;

            attemptParse_mode = 0;

            if (typeof(T) == typeof(double))
                attemptParse_mode = shared_switch_types.Double;
            else if (typeof(T) == typeof(int))
                attemptParse_mode = shared_switch_types.Integer;

            if (getIfValid
            (
                args,
                type,
                type,
                ref cli_strip,
                attemptParse_mode
            ))
            {

                if (typeof(T) == typeof(double))
                {
                    double temp_get;
                    if (!(status = double.TryParse(cli_strip, out temp_get)))
                    {
                        cli_logger.writeLine(shared_error_bad_years);
                        status = false;
                    }
                    else
                        var = (T)(object)temp_get;
                }
                else if (typeof(T) == typeof(int))
                {
                    int temp_get;
                    if (!(status = int.TryParse(cli_strip, out temp_get)))
                    {
                        cli_logger.writeLine(shared_error_bad_years);
                        status = false;
                    }
                    else
                        var = (T)(object)temp_get;
                }

                if (status)
                {
                    try
                    {
                        if (typeof(T) == typeof(double))
                            var = (T)(object)Math.Abs((double)(object)var);
                        else if (typeof(T) == typeof(int))
                            var = (T)(object)Math.Abs((int)(object)var);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                }
            }
            else
                status = false;

            return status;
        }

		/*
		 *Verify if a user value is correct
		 *
		 *@PARAM: The user arguments 
		 *@PARAM: The parameter head to seek
		 *@PARAM: The parameter to match, same as key typically
		 *@PARAM: A return space for the string after the head
		 *@PARAM: What type is expected of the parameter
		 *@RETURN: Whether the user parameters were correct
		 */
		internal bool getIfValid(string[] args, string key, string to_match, ref string ending, shared_switch_types type)
		{
			bool
				isValid_result;

			int
				isValid_search_index;

			isValid_result = false;
			
			if(-1 < (isValid_search_index = Array.FindIndex(args, s => s.Contains(key))))
			{
				if(type == shared_switch_types.Empty)
					isValid_result = true;
				else
				{
					ending = args[isValid_search_index].Substring(args[isValid_search_index].IndexOf(to_match) + to_match.Length);
                    
                    //Again pardon the variation, I just wanted to see C# regex syntax
					if(!string.IsNullOrEmpty(ending))
					{
						if(shared_switch_types.Integer == type)
							isValid_result = Regex.IsMatch(ending, @"^\d+$");
						else if(shared_switch_types.Double == type)
							isValid_result = Regex.IsMatch(ending, @"[\d]+([.,][\d]*)?");
						else if(shared_switch_types.String == type) //for clarity
							isValid_result = true;
					}
				}
			}
			
			return isValid_result;
		}

		internal Double cliExec()
		{
			double
				cliExec_result;

			string
				cliExec_format_log,
                cliExec_get_log;

			cliExec_result = -1;
			cliExec_format_log = "";
            cliExec_get_log = "";

            if (cli_function_bypass)
            {
                if (cli_bypass_function_mode == shared_bypass_function_types.LogWipe)
                    cli_logger.wipe();
                if (cli_bypass_function_mode == shared_bypass_function_types.LogPrint)
                {
                    if (cli_logger.getFile(ref cliExec_get_log))
                        cliExec_format_log = cliExec_get_log;
                    else
                        cliExec_format_log = shared_error_log_get_failed;

                    Console.WriteLine(cliExec_format_log + "\n");
                }
			}
			else
			{
				if(cli_is_price_mode)
				{
					cliExec_result = cli_calculator.CalcPrice(cli_coupon, cli_years, cli_face, cli_rate);
					cliExec_format_log = String.Format("Price: Coupon={0}, Years={1}, Face={2}, Rate={3} :: Result({4})\n", cli_coupon, cli_years, cli_face, cli_rate, cliExec_result);
				}
				else
				{
					cliExec_result = cli_calculator.CalcYield(cli_coupon, cli_years, cli_face, cli_price);
					cliExec_format_log = String.Format("Yield: Coupon={0}, Years={1}, Face={2}, Price={3} :: Result({4})\n", cli_coupon, cli_years, cli_face, cli_price, cliExec_result);
				}

				cli_logger.writeLine(cliExec_format_log);
			}

			return cliExec_result;
		}

		internal void cliHelp()
		{
			if(cli_help_toggle)
				Console.WriteLine(shared_help_message);
			else
				Console.WriteLine(shared_view_help);
		}
	}
}
