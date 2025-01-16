using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logical_Operators
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String var = richTextBox1.Text;
            // split the input on the basis of space
            String[] words = var.Split(' ');
            // Regular Expression for operators
            Regex regex1 = new Regex(@"^[&]{2}$");
            Regex regex2 = new Regex(@"^[|]{2}$");
            Regex regex3 = new Regex(@"^[!]{1}$");
           


            for (int i = 0; i < words.Length; i++)
            {
                Match match1 = regex1.Match(words[i]);
                Match match2 = regex2.Match(words[i]);
                Match match3 = regex3.Match(words[i]);
                if (match1.Success || match2.Success || match3.Success)
                {
                    richTextBox2.Text += words[i] + " ";
                }
                else
                {
                    MessageBox.Show("invalid " + words[i]);
                }
            }
        }
    }
}