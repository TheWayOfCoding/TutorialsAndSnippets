using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
By: Scott Waldron
TheWayOfCoding.com

- All resources retain copyright (bitmaps, etc).
- You are not allowed to use these projects 
as tutorials or samples distributed with your own branding.
Basically, any use with the same theme as they were 
intended for with TheWayOfCoding.com is not allowed.

The MIT License (MIT)

Copyright (c) 2017

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int totalBallsNeeded = 6; //5 plus the extra shot
            int doubleBallCount = 104; //52 numbers times 2

            //Illinois Lotto Data
            //300 draw frequency history
            //MON 02/17/14 to SAT 01/16/16 range
            //http://www.illinoislottery.com/en-us/Lotto.html
            //lotto number range is from 01 to 52
            SortedDictionary<int, int> lottoNumberFrequencyTable = new SortedDictionary<int, int>();
            lottoNumberFrequencyTable.Add(1, 29);
            lottoNumberFrequencyTable.Add(2, 34);
            lottoNumberFrequencyTable.Add(3, 26);
            lottoNumberFrequencyTable.Add(4, 37);
            lottoNumberFrequencyTable.Add(5, 29);
            lottoNumberFrequencyTable.Add(6, 37);
            lottoNumberFrequencyTable.Add(7, 50);
            lottoNumberFrequencyTable.Add(8, 40);
            lottoNumberFrequencyTable.Add(9, 26);
            lottoNumberFrequencyTable.Add(10, 25);
            lottoNumberFrequencyTable.Add(11, 39);
            lottoNumberFrequencyTable.Add(12, 34);
            lottoNumberFrequencyTable.Add(13, 38);
            lottoNumberFrequencyTable.Add(14, 38);
            lottoNumberFrequencyTable.Add(15, 35);
            lottoNumberFrequencyTable.Add(16, 40);
            lottoNumberFrequencyTable.Add(17, 34);
            lottoNumberFrequencyTable.Add(18, 42);
            lottoNumberFrequencyTable.Add(19, 39);
            lottoNumberFrequencyTable.Add(20, 36);
            lottoNumberFrequencyTable.Add(21, 27);
            lottoNumberFrequencyTable.Add(22, 43);
            lottoNumberFrequencyTable.Add(23, 32);
            lottoNumberFrequencyTable.Add(24, 32);
            lottoNumberFrequencyTable.Add(25, 33);
            lottoNumberFrequencyTable.Add(26, 32);
            lottoNumberFrequencyTable.Add(27, 29);
            lottoNumberFrequencyTable.Add(28, 23);
            lottoNumberFrequencyTable.Add(29, 31);
            lottoNumberFrequencyTable.Add(30, 40);
            lottoNumberFrequencyTable.Add(31, 36);
            lottoNumberFrequencyTable.Add(32, 26);
            lottoNumberFrequencyTable.Add(33, 27);
            lottoNumberFrequencyTable.Add(34, 32);
            lottoNumberFrequencyTable.Add(35, 32);
            lottoNumberFrequencyTable.Add(36, 49);
            lottoNumberFrequencyTable.Add(37, 43);
            lottoNumberFrequencyTable.Add(38, 32);
            lottoNumberFrequencyTable.Add(39, 37);
            lottoNumberFrequencyTable.Add(40, 35);
            lottoNumberFrequencyTable.Add(41, 29);
            lottoNumberFrequencyTable.Add(42, 38);
            lottoNumberFrequencyTable.Add(43, 31);
            lottoNumberFrequencyTable.Add(44, 27);
            lottoNumberFrequencyTable.Add(45, 47);
            lottoNumberFrequencyTable.Add(46, 37);
            lottoNumberFrequencyTable.Add(47, 30);
            lottoNumberFrequencyTable.Add(48, 24);
            lottoNumberFrequencyTable.Add(49, 44);
            lottoNumberFrequencyTable.Add(50, 38);
            lottoNumberFrequencyTable.Add(51, 44);
            lottoNumberFrequencyTable.Add(52, 38);

            //add up the total range of frequencies 
            int totalFrequencyCount = 0;

            //an array that will hold frequency ranges for all possible balls
            //eg: [0][1] is for ball 01 with two associated array entries: [0] = 0 and [1] = 29
            //for ball 02 it would be [2] = 30 and [3] = 64 for that range
            int[] frequencyBlocks = new int[doubleBallCount];


            //keep track of which set of array entries we are working on
            int frequencyBlocksCounter = 0;
            
            foreach (KeyValuePair<int, int> entry in lottoNumberFrequencyTable.ToArray())
            {
                //generate block of numbers for weighted RNG

                //the starting point value for this lotto number
                frequencyBlocks[frequencyBlocksCounter] = totalFrequencyCount++;
                
                //increment the total frequency count variable to prep for the next step
                totalFrequencyCount += entry.Value;

                //the ending point for this lotto number
                frequencyBlocks[frequencyBlocksCounter + 1] = totalFrequencyCount;

                //jump to the next set of array entries
                frequencyBlocksCounter += 2; 
            }
            
            //fire up a random number generator
            Random r = new Random();

            //clear our result display text box
            textBox1.Text = "";

            //due to needing unique numbers, we need to track our successful pulls
            int totalBallsSelected = 0;

            //a place to store our successful pulls
            List<int> selectedBallValues = new List<int>();

            //get our six weighted random values with no duplicates
            while(totalBallsSelected < totalBallsNeeded)
            {
                //get a random number between 1 and the total sample size
                int randInRange = r.Next(1, totalFrequencyCount);

                //get a weighted random number result
                //loop through our custom array
                int currentBall = 1;
                for(int j = 0; j < doubleBallCount; j += 2)
                {
                    //find which block of weighted frequency this random number should fit in
                    //once found that range will relate back to a lotto number
                    if (randInRange >= frequencyBlocks[j] && randInRange < frequencyBlocks[j + 1])
                    {
                        //don't allow duplicates
                        if(!selectedBallValues.Contains(currentBall))
                        {
                            //save the successful pull into our result class instance
                            selectedBallValues.Add(currentBall);
                            totalBallsSelected++;
                        }
                        break; //exit the inner for loop after a successful pull
                    }
                    else
                    {
                        //after a successful pull, increment our total result counter
                        currentBall++;
                    }
                }
            }
            
            //write the results to the screen
            foreach(int singleItem in selectedBallValues)
            {
                textBox1.Text += singleItem.ToString() + "\r\n";
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
