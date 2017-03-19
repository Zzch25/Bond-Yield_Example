/*
 *Zachary Job
 *file.cs
 *2/1/17
 *
 *A simple output wrapper for ease of printing
 *and logging
 */
 
using System;
using System.IO;

namespace fiyield
{
    internal class file : shared
    {
        bool
            file_toggle_out,
            file_toggle_stdout,
            file_dual_to_stdout;

        /*
		 *Basic initializer setting up a file for use
		 *or setting up for the specified mode
		 *
		 *@PARAM: The return reference for whether or not there was success
		 *@PARAM: If STDOUT has been targeted for printing
		 */
        internal file(ref bool status, bool dualToSTDOUT)
        {
            file_toggle_out = false;
            file_toggle_stdout = false;
            file_dual_to_stdout = dualToSTDOUT;
            status = true; //For future updates, eg forward checking for a file
                           //location's availability
        }

		/*
		 *Toggle logging to a file
		 */
		internal void toggleLog()
		{
			file_toggle_out = !file_toggle_out;
		}

		/*
		 *Toggle logging to the terminal
		 */
		internal void toggleSTDOUT()
		{
			file_toggle_stdout = !file_toggle_stdout;
		}

		/*
		 *Write a line and newline to a file
		 *
		 *@PARAM: The string to write
		 *@RETURN: If the line wrote
		 */
		internal bool writeLine(string to_write)
		{
			bool
				writeLine_result;

            writeLine_result = true;

            if (file_toggle_out)
            {
                try { File.AppendAllText(shared_descriptor_name, to_write); }
                catch(Exception ex) { Console.WriteLine(ex.ToString()); writeLine_result = false; }
            }
            else
                writeLine_result = false;

            if (file_toggle_stdout)	
				Console.WriteLine(to_write);

			return writeLine_result;
		}
	
		/*
		 *Clean the open file descriptor
		 *
		 *@RETURN: If the file was cleaned
		 */	
		internal bool wipe()
		{
			bool
                wipe_except,
				wipe_result;

            FileStream
                wipe_stream;

            wipe_except = false;
			wipe_result = false;
            wipe_stream = null;

            try { wipe_stream = File.Open(shared_descriptor_name, FileMode.Open); }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); wipe_except = true; }

            if (wipe_stream != null && wipe_except == false)
			{
				if(file_toggle_stdout)
					Console.WriteLine("Wiping Log...");

				wipe_stream.SetLength(0);
				wipe_result = true;
                wipe_stream.Close();
			}
			else
			{
				if(file_toggle_stdout)
					Console.WriteLine("Wipe Failed...");
			}

			return wipe_result;
		}

		/*
		 *Retrieve a file's contents wholly
		 */
		internal bool getFile(ref string file)
		{
            bool
                getFile_result;

            file = "";

            try { file = File.ReadAllText(shared_descriptor_name); }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }

            getFile_result = !String.IsNullOrEmpty(file);

            return getFile_result;
		}
	}
}
