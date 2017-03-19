/*
 *Zachary Job
 *2/2/2017
 * 
 *An autogen GUI modified to work with the existing
 *classes
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string
            form_output;

        fiyield.file
            form_file;

        fiyield.calculator
            form_calculator;

        fiyield.shared
            form_definitions;
        /*
         *Call the form initialization
         */
        public Form1()
        {
            InitializeComponent();
        }

        /*
         *Load and initialize class variables
         *
         *@PARAM: SYSTEM API
         *@PARAM: SYSTEM API
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            bool
                Form1_Load_check_log;

            form_output = "";
            Form1_Load_check_log = false;

            form_file = new fiyield.file(ref Form1_Load_check_log, false);
            form_calculator = new fiyield.calculator();
            form_definitions = new fiyield.shared();

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            //this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);

            //Output window
            richTextBox1.Multiline = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.ReadOnly = true;

            //Immediate result window
            textBox1.ReadOnly = true;

            //Default radio
            radioButton2.Checked = true;
        }

        /*
         *Ensure the app closes
         *
         *@PARAM: SYSTEM API
         *@PARAM: SYSTEM API
         */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }

        /*
        private void Form1_FormClosed(Object sender, FormClosedEventArgs e)
        {
        }
        */

        //UNUSED
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //UNUSED
        private void label1_Click(object sender, EventArgs e)
        {

        }

        //UNUSED
        private void label5_Click(object sender, EventArgs e)
        {

        }

        //UNUSED
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        //UNUSED
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        //UNUSED
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //UNUSED
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //UNUSED
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //UNUSED
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /*
         *Calculate has been requested. Verify the current state
         *and if valid produce the result. Else prompt the user
         *with what has happened
         */
        private void button1_Click(object sender, EventArgs e)
        {
            bool
                button1_Click_result;

            string
                button1_Click_price,
                button1_Click_rate,
                button1_Click_coupon,
                button1_Click_face,
                button1_Click_years,
                button1_Click_formater;

            double
                button1_Click_price_parsed,
                button1_Click_rate_parsed,
                button1_Click_coupon_parsed,
                button1_Click_face_parsed,
                button1_Click_calculation;

            int
                button1_Click_years_parsed;

            button1_Click_result = true;
            button1_Click_formater = "";

            button1_Click_calculation = 0;
            button1_Click_price_parsed = 0;
            button1_Click_rate_parsed = 0;
            button1_Click_coupon_parsed = 0;
            button1_Click_face_parsed = 0;
            button1_Click_calculation = 0;
            button1_Click_years_parsed = 0;

            if (radioButton2.Checked) //price
            {
                button1_Click_rate = textBox5.Text;
                button1_Click_coupon = textBox4.Text;
                button1_Click_face = textBox2.Text;
                button1_Click_years = textBox3.Text;

                if (
                        !String.IsNullOrEmpty(button1_Click_rate) &&
                        !String.IsNullOrEmpty(button1_Click_coupon) &&
                        !String.IsNullOrEmpty(button1_Click_face) &&
                        !String.IsNullOrEmpty(button1_Click_years)
                    )
                {
                    //PARDON the variation.. Earlier I used regex to see the syntax because I tend to use it a lot
                    if (!double.TryParse(button1_Click_rate, out button1_Click_rate_parsed))
                        button1_Click_result = false;
                    else if (!double.TryParse(button1_Click_coupon, out button1_Click_coupon_parsed))
                        button1_Click_result = false;
                    else if (!double.TryParse(button1_Click_face, out button1_Click_face_parsed))
                        button1_Click_result = false;
                    else if (!int.TryParse(button1_Click_years, out button1_Click_years_parsed))
                        button1_Click_result = false;
                    else
                    {
                        button1_Click_calculation = form_calculator.CalcPrice
                                                       (
                                                        button1_Click_coupon_parsed,
                                                        button1_Click_years_parsed,
                                                        button1_Click_face_parsed,
                                                        button1_Click_rate_parsed
                                                       );
                    }
                }
                else
                    button1_Click_result = false;
            }
            else //yield
            {
                button1_Click_price = textBox6.Text;
                button1_Click_coupon = textBox4.Text;
                button1_Click_face = textBox2.Text;
                button1_Click_years = textBox3.Text;

                if (
                        !String.IsNullOrEmpty(button1_Click_price) &&
                        !String.IsNullOrEmpty(button1_Click_coupon) &&
                        !String.IsNullOrEmpty(button1_Click_face) &&
                        !String.IsNullOrEmpty(button1_Click_years)
                    )
                {
                    if (!double.TryParse(button1_Click_price, out button1_Click_price_parsed))
                        button1_Click_result = false;
                    else if (!double.TryParse(button1_Click_coupon, out button1_Click_coupon_parsed))
                        button1_Click_result = false;
                    else if (!double.TryParse(button1_Click_face, out button1_Click_face_parsed))
                        button1_Click_result = false;
                    else if (!int.TryParse(button1_Click_years, out button1_Click_years_parsed))
                        button1_Click_result = false;
                    else
                        button1_Click_calculation = form_calculator.CalcYield
                                                       (
                                                        button1_Click_coupon_parsed,
                                                        button1_Click_years_parsed,
                                                        button1_Click_face_parsed,
                                                        button1_Click_price_parsed
                                                       );
                }
                else
                    button1_Click_result = false;
            }

            if(button1_Click_result)
            {
                button1_Click_calculation = Math.Round(button1_Click_calculation, form_definitions.shared_double_precision_gui);

                if (radioButton2.Checked) //price
                    button1_Click_formater = String.Format
                                        (
                                            "Price: Coupon={0}, Years={1}, Face={2}, Rate={3} :: Result({4})", 
                                            button1_Click_coupon_parsed,
                                            button1_Click_years_parsed,
                                            button1_Click_face_parsed,
                                            button1_Click_rate_parsed, 
                                            button1_Click_calculation
                                        );
                else
                    button1_Click_formater = String.Format
                                        (
                                            "Price: Coupon={0}, Years={1}, Face={2}, Price={3} :: Result({4})",
                                            button1_Click_coupon_parsed,
                                            button1_Click_years_parsed,
                                            button1_Click_face_parsed,
                                            button1_Click_price_parsed,
                                            button1_Click_calculation
                                        );

                form_output += button1_Click_formater;
                form_output += "\n";

                richTextBox1.Text = form_output;
                textBox1.Text = button1_Click_calculation.ToString();

                if (checkBox6.Checked && !form_file.writeLine(button1_Click_formater))
                {
                    form_output += form_definitions.shared_error_log_get_failed;
                    form_output += "\n";

                    richTextBox1.Text = form_output;
                }
            }
            else
            {
                form_output += form_definitions.shared_gui_help;
                form_output += "\n";

                richTextBox1.Text = form_output;

                if (checkBox6.Checked && !form_file.writeLine(form_definitions.shared_gui_help))
                {
                    form_output += form_definitions.shared_error_log_get_failed;
                    form_output += "\n";

                    richTextBox1.Text = form_output;
                }
            }
        }

        /*
         *Update fields if to favor Yield when the Yield radio is selected
         *
         *@PARAM: SYSTEM API
         *@PARAM: SYSTEM API
         */
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = false;
            textBox6.Enabled = true;
        }

        /*
         *Update fields if to favor Price when the Price radio is selected
         *
         *@PARAM: SYSTEM API
         *@PARAM: SYSTEM API
         */
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = true;
            textBox6.Enabled = false;
        }

        /*
         *Enable logging when Log Work is checked
         *
         *@PARAM: SYSTEM API
         *@PARAM: SYSTEM API
         */
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            form_file.toggleLog();
        }

        /*
         *Clear the window and window buffer when Wipe History is clicked
         *
         *@PARAM: SYSTEM API
         *@PARAM: SYSTEM API
         */
        private void button2_Click(object sender, EventArgs e)
        {
            form_output = "";
            richTextBox1.Clear();
        }
        
        /*
         *Wipe the log file when Wipe log is clicked
         *
         *@PARAM: SYSTEM API
         *@PARAM: SYSTEM API
         */
        private void button3_Click(object sender, EventArgs e)
        {
            if (form_file.wipe())
                form_output += form_definitions.shared_gui_log_wipe;
            else
                form_output += form_definitions.shared_error_log_get_failed;
            form_output += "\n";

            richTextBox1.Text = form_output;
        }

        /*
         *Load the log file when Load log is clicked
         *
         *@PARAM: SYSTEM API
         *@PARAM: SYSTEM API
         */
        private void button4_Click(object sender, EventArgs e)
        {
            string
                button4_click_get_log;

            button4_click_get_log = "";

            if (form_file.getFile(ref button4_click_get_log))
                form_output += button4_click_get_log;
            else
                form_output += form_definitions.shared_error_log_get_failed;
            form_output += "\n";

            richTextBox1.Text = form_output;
        }
    }
}
