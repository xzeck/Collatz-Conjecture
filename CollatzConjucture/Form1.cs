﻿using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace CollatzConjucture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BigInteger input,result; // Input takes input , and result is used for calculations

        #region Compute Button and Benchmark Button
        /// <summary>
        /// Triggered when Compute Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Compute Button
        private void ComputeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InputNumberBox.Text) || InputNumberBox.Text.Contains("Enter an integer"))
            {
                input = 7;
                MessageBox.Show("Since you didn't enter a number , the default is set to 7", "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                input = BigInteger.Parse(InputNumberBox.Text);// converting the input from the textbox to biginteger
            }
            
            //converting input to a postive intger 
            if (input < 0)
            {
                input = ~input + 1; // converting value of input to positive if the integer is negative
                
            }
            

            CollatzConjectureCalc(input); // calling the function and passing input as a value
        }
        #endregion


        /// <summary>
        /// Triggered when Benchmark Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Benchmark Button
        private void BenchmarkButton_Click(object sender, EventArgs e)
        {
            Thread Benchmark = new Thread(new ThreadStart(CollatzConjectureBenchmark));// Thread for Benchmark
            Benchmark.Start();//Start
            //Benchmark.Abort();//Abort

            //CollatzConjectureBenchmark();
        }
        #endregion
        #endregion


        #region Conjucture calculator and Benchmark respectively
        /// <summary>
        /// This function applies the conjecture on the given number
        /// </summary>
        /// <param name="input">the number which is to be passed</param>
        #region Calc
        public void CollatzConjectureCalc(BigInteger input)
        {
            result = input;// setting the value of result as input

            int steps =0; // initializing steps to 0

           // var watch = Stopwatch.StartNew();//initializing stop watch

            ResultLabel.Text = ""; // clearing the log label

            var watch = Stopwatch.StartNew();
            while(result!=1 )
            {
                //Checking if even 
                if (result % 2 == 0)
                {
                    result = result / 2;//dividing the result by 2

                } else if (result%2 != 0) // if not even
                {
                    result = (3 * result) + 1; // Collatz Conjecture , equation
                }
                
                steps++; // incrementing the steps
            }
            watch.Stop(); // stopping the watch

            var elapsedtime = watch.ElapsedMilliseconds; // setting elapsed time to the time taken to execute the steps

            ResultLabel.Text += "End Result = "+result; // displaying the result
            ResultLabel.Text += "\nSteps taken = " + steps; // displaying the steps
            if (PreciseTiming.Checked)
            {
                ResultLabel.Text += "\nElapsed Time : " + elapsedtime + " ms";
            }
            else
            {
                #region precise time
                if (elapsedtime <= 1)
                {
                    ResultLabel.Text += "\nElapsed Time = -"; // displaying time elapsed
                }
                else
                {
                    if (elapsedtime >= 1000) //checking if elapsed time is more than 100ms 
                    {
                        elapsedtime = elapsedtime / 1000; // converting ms to seconds

                        if (elapsedtime >= 60) //checking if seconds is more then 60
                        {
                            elapsedtime = elapsedtime / 60; //converting seconds to minutes

                            if (elapsedtime >= 60)// checking if minutes is more than 60
                            {
                                elapsedtime = elapsedtime / 60; // converting minutes to hours

                                ResultLabel.Text += "\nElapsed Time = " + elapsedtime + "hours"; // displaying hours

                            }
                            else // else for third if
                            {
                                ResultLabel.Text += "\nElapsed Time = " + elapsedtime + "minutes"; //displaying minutes
                            }
                        }
                        else // else for second if 
                        {
                            ResultLabel.Text += "\nElapsed Time = " + elapsedtime + "seconds"; //displaying seconds
                        }
                    }
                    else // else for first if
                    {
                        ResultLabel.Text += "\nElapsed Time = " + elapsedtime + "ms"; //displaying ms
                    }
                }
            }
            #endregion

            
        }
        #endregion


        /// <summary>
        /// This benchmarks a set constant range of numbers ; default numbers checked : 5000000
        /// </summary>
        #region Benchmark
        private void CollatzConjectureBenchmark()
        {
            BigInteger counter = 10,steps=0; // setting values of counter and steps

            BigInteger result, input=5;   
            var watch = Stopwatch.StartNew();
            for (counter = 1; counter <= 5000000; counter++) //change the counter before comitting
            {
                result = input;
                while (result != 1) // loop will run till the result equals 1, after that it will loop as 4 , 2 ,1 so its of no use after 1
                {
                    if (result % 2 == 0)
                    {
                        result = result / 2;//dividing the result by 2

                    }
                    else if (result % 2 != 0) // if not even
                    {
                        result = (3 * result) + 1; // Collatz Conjecture , equation
                    }
                    
                    steps++; // incrementing steps
                }
                input++; // incrementing input for next number in result
            }
            watch.Stop();//stopping the watch
            var elapsedtime = watch.ElapsedMilliseconds; // setting the value of elapsed time 

            Form1 f1 = new Form1();
            if (PreciseTiming.Checked)
            {
                ResultLabel.Text += "\nElapsed time : " + elapsedtime;//displaying elapsed time
            }
            #region Precise time
            else
            {
                if (elapsedtime >= 1000) //checking if elapsed time is more than 100ms 
                {
                    elapsedtime = elapsedtime / 1000; // converting ms to seconds

                    if (elapsedtime >= 60) //checking if seconds is more then 60
                    {
                        elapsedtime = elapsedtime / 60; //converting seconds to minutes

                        if (elapsedtime >= 60)// checking if minutes is more than 60
                        {
                            elapsedtime = elapsedtime / 60; // converting minutes to hours

                            ResultLabel.Text += "\nElapsed Time = " + elapsedtime + "hours"; // displaying hours

                        }
                        else // else for third if
                        {
                            ResultLabel.Text += "\nElapsed Time = " + elapsedtime + "minutes"; //displaying minutes
                        }
                    }
                    else // else for second if 
                    {
                        ResultLabel.Text += "\nElapsed Time = " + elapsedtime + "seconds"; //displaying seconds
                    }
                }
                else // else for first if
                {
                    ResultLabel.Text += "\nElapsed Time = " + elapsedtime + "ms"; //displaying ms
                }
            }
            #endregion

            f1.ResultLabel.Text += "\nSteps taken : " + steps; //displaying steps taken
        }
        #endregion

        #endregion


        #region Miscellaneous : Radio button and Form 2 instance checker
        /// <summary>
        /// Radio buttons for changing the colors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Radio button for colors

        //Changing the text color to red
        private void RedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ResultLabel.ForeColor = System.Drawing.Color.Red;
            InputNumberBox.ForeColor = System.Drawing.Color.Red;
        }

        //Changing the text color to Green
        private void GreenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ResultLabel.ForeColor = System.Drawing.Color.Lime;
            InputNumberBox.ForeColor = System.Drawing.Color.Lime;
        }

        //Changing the text color to yello
        private void YelloRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ResultLabel.ForeColor = System.Drawing.Color.Yellow;
            InputNumberBox.ForeColor = System.Drawing.Color.Yellow;
        }

        //Changing the label color to Black
        private void BackcolorBlack_CheckedChanged(object sender, EventArgs e)
        {
            ResultLabel.BackColor = System.Drawing.Color.Black;
            InputNumberBox.BackColor = System.Drawing.Color.Black;

        }

        //Changing the label color to white
        private void BackColorWhite_CheckedChanged(object sender, EventArgs e)
        {
            ResultLabel.BackColor = System.Drawing.Color.White;
            InputNumberBox.BackColor = System.Drawing.Color.White;
        }

        #endregion


        /// <summary>
        /// Checks if the second form is open or not
        /// </summary>
        #region Is Form 2 Open
        Boolean IsForm2Open = false;
        private void SeriesButton_Click(object sender, EventArgs e)
        {
            Form f2 = new Form2();
            if (IsForm2Open == false)
            {
                
                f2.Show();
                IsForm2Open = true;
            }else
            {
                MessageBox.Show("One instance is already running", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            f2.FormClosed += new FormClosedEventHandler(Form2_Closed);
            
        }
        #endregion

        /// <summary>
        /// Handler for Form 2 closed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Form 2 Closed Handler
        public void Form2_Closed(object sender , EventArgs e)
        {
            IsForm2Open = false;
        }
        #endregion

        #endregion

    }
}
            