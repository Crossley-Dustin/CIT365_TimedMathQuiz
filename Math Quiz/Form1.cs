using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Math_Quiz
{
    public partial class Form1 : Form
    {
        // Random object to generate random numbers.
        Random randomizer = new Random();

        // To Store numbers for addition problem.
        int addend1;
        int addend2;

        // To store numbers for subtraction problem.
        int minuend;
        int subtrahend;

        // To store numbers for multiplication problem.
        int multiplicand;
        int multiplier;

        // To store numbers for division problem.
        int dividend;
        int divisor;

        // Keep track of remaining time
        int timeLeft;

        public Form1()
        {
            InitializeComponent();
            
            // Set date label.
            dateLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");
        }

        /// <summary>
        /// Start quiz by filling in all the problems
        /// and starting timer.
        /// </summary>
        public void StartTheQuiz()
        {
            // Fill in the addition problem.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert random numbers to strings to display in label controls.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // Initialize sum value.
            sum.Value = 0;

            // Reset timer color.
            timeLabel.BackColor = SystemColors.Control;

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true,
                // then user got answer right. Stop
                // timer and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!", "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Display remaining time in Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";

                // Change color of timeLabel when 5 seconds or less
                if (timeLeft <= 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                // If user ran out of time, stop timer,
                // show a MessageBox, and fill in answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                timeLabel.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// Check answer to see if the user got everything right.
        /// </summary>
        /// <returns>True if the answer is correct, false otherwise.</returns>
        private bool CheckTheAnswer()
        {
            if (CheckAdd()
                && CheckSubtract()
                && CheckMultiply()
                && CheckDivision())
                return true;
            else
                return false;
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control

            if (sender is NumericUpDown answerBox)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private bool CheckAdd()
        {
            // Check addition answer to see if correct
            return (addend1 + addend2 == sum.Value);
        }

        private bool CheckSubtract()
        {
            // Check subtraction answer to see if correct
            return (minuend - subtrahend == difference.Value);
        }

        private bool CheckMultiply()
        {
            // Check multiplication answer to see if correct
            return (multiplicand * multiplier == product.Value);
        }

        private bool CheckDivision()
        {
            // Check division answer to see if correct
            return (dividend / divisor == quotient.Value);
        }
        
        private void answerChanged(object sender, EventArgs e)
        {
            bool beepMe = false;
            bool clockRunning = timer1.Enabled;

            // Make sure sender is a NumericUpDown box.
            if (sender is NumericUpDown answerBox)
            {
                // Switch on the UpDown box name to check answer.
                switch (answerBox.Name)
                {
                    case "sum":
                        beepMe = CheckAdd();
                        break;
                    case "difference":
                        beepMe = CheckSubtract();
                        break;
                    case "product":
                        beepMe = CheckMultiply();
                        break;
                    case "quotient":
                        beepMe = CheckDivision();
                        break;
                }

                // If the answer is correct and the timer is running, beep the console.
                if (beepMe && clockRunning)
                {
                    Console.Beep();
                }
            }
        }
    }
}
