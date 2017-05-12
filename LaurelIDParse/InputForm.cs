using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaurelIDParse
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }
        public string[] lines;

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Lines.Length > 5) && (textBox1.Lines[textBox1.Lines.Length - 1] == "") && (textBox1.Lines[textBox1.Lines.Length - 2] == "") && (textBox1.Lines[textBox1.Lines.Length - 3] == ""))
            {
                lines = textBox1.Lines;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
