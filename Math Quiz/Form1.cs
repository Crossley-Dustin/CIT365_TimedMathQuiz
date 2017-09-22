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

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start quiz by filling in all the problems
        /// and starting timer
        /// </summary>
        public void StartTheQuiz()
        {
            // Fill in the addition problem
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert random numbers to strings to display in label controls
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // initialize sum value
            sum.Value = 0;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }
    }
}
