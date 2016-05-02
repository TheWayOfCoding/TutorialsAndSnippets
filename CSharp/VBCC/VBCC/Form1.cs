using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

Copyright (c) 2016 

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
namespace VBCC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IEnumerable<string> FilterFiles(string path, params string[] exts)
        {
            return exts.Select(x => "*." + x).SelectMany(x => Directory.EnumerateFiles(path, x));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //only parse VB6 frm, cls, and bas files
            var validExtensions = new[] { "frm", "cls", "bas" };

            txtResult.Text = "";
            txtLineCount.Text = "";

            if (folderSelector.ShowDialog() == DialogResult.OK)
            {
                //only parse files that end in our desired extensions
                IEnumerable<string> foundFiles = FilterFiles(folderSelector.SelectedPath, validExtensions);

                //loop through all of the files so we can count lines of code per project folder
                foreach (string value in foundFiles)
                {
                    string currentFile = File.ReadAllText(value);

                    //clear out any lines that are entirely comments
                    currentFile = Regex.Replace(currentFile, @"'(.*)", "", RegexOptions.Multiline);

                    //clear out any lines that are blank
                    currentFile = Regex.Replace(currentFile, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

                    //remove any blocks of empty spaces so that there are only single spaces
                    while (currentFile.Contains("  "))
                    {
                        currentFile = currentFile.Replace("  ", " ");
                    }

                    //confirm there are not empty lines
                    while (currentFile.Contains("\n\n"))
                    {
                        currentFile = currentFile.Replace("\n\n", "\n");
                    }
                    
                    //append the parsed code file text
                    txtResult.Text += currentFile;
                }

                //count the project's entire lines of code
                txtLineCount.Text = (txtResult.Lines.Count() - 1).ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
