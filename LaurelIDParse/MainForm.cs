using IdParser;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;



namespace LaurelIDParse
{
    public partial class MainForm : Form
    {
        BindingList<LaurelFormat> lst;
        public MainForm()
        {
            InitializeComponent();
            lst = new BindingList<LaurelFormat>();
            dataGridView1.DataSource = lst;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        public void ParseText(string[] txt)
        {
            try
            {
                string s = "";
                for (int i = 0; i < txt.Count(); i++)
                {

                    if (txt[i] == "\u001e")
                    {
                        s += System.Uri.UnescapeDataString(txt[i]) + "\r";
                    }
                    else
                    {
                        s += txt[i] + "\n";
                    }
                }

                LaurelFormat l = new LaurelFormat();
                //l.SetLaurelFormat(txt);

                IdParser.IdentificationCard idCard = IdParser.IdParser.Parse(s);
                l.FIRST_NAME = idCard.FirstName;
                l.MIDDLE_INITIAL = idCard.MiddleName;
                l.LAST_NAME = idCard.LastName;
                l.LAST_NAME = idCard.LastName;
                l.LAST_NAME = idCard.LastName;
                l.LAST_NAME = idCard.LastName;
                l.NAME_SUFFIX = idCard.NameSuffix;
                l.STREET_ADDRESS = (idCard.StreetLine1 + ' ' + idCard.StreetLine2).Trim();
                //l.PO_BOX = "";
                l.CITY = idCard.City;
                l.STATE = idCard.JurisdictionCode;
                l.ZIP_CODE = idCard.PostalCode;
                l.FRN = "";
                l.SSN = "";
                l.E_MAIL = "";
                l.TELEPHONE = "";

                bool updated = false;
                //look for a match on the name first and ask about updating
                foreach (LaurelFormat ll in lst)
                {
                    if (ll.FIRST_NAME.ToUpper() == l.FIRST_NAME && ll.LAST_NAME.ToUpper() == l.LAST_NAME)
                    {
                        DialogResult dr = MessageBox.Show("Would you like to update the existing record", "", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            ll.FIRST_NAME = idCard.FirstName;
                            ll.MIDDLE_INITIAL = idCard.MiddleName;
                            ll.LAST_NAME = idCard.LastName;
                            ll.LAST_NAME = idCard.LastName;
                            ll.LAST_NAME = idCard.LastName;
                            ll.LAST_NAME = idCard.LastName;
                            ll.NAME_SUFFIX = idCard.NameSuffix;
                            ll.STREET_ADDRESS = (idCard.StreetLine1 + ' ' + idCard.StreetLine2).Trim();
                            //l.PO_BOX = "";
                            ll.CITY = idCard.City;
                            ll.STATE = idCard.JurisdictionCode;
                            ll.ZIP_CODE = idCard.PostalCode;
                            updated = true;
                            dataGridView1.Refresh();
                        }
                        break;
                    }

                }

                if (!updated)
                {
                    lst.Add(l);
                }
                //dataGridView1.DataSource = lst.ToList();
                return;

            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LINQtoCSV.CsvContext cc = new LINQtoCSV.CsvContext();
                cc.Write<LaurelFormat>(lst, saveFileDialog1.FileName);
            }
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            lst.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lst.Clear();
            if(File.Exists("save.csv"))
            {
                LINQtoCSV.CsvContext cc = new LINQtoCSV.CsvContext();
                var q = cc.Read<LaurelFormat>("save.csv").ToList();
                foreach(LaurelFormat qq in q)
                {
                    lst.Add(qq);
                }
            }
            textBox1.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LINQtoCSV.CsvContext cc = new LINQtoCSV.CsvContext();
            cc.Write<LaurelFormat>(lst, "save.csv");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Lines.Length > 5) && (textBox1.Lines[textBox1.Lines.Length - 1] == "") && (textBox1.Lines[textBox1.Lines.Length - 2] == "") && (textBox1.Lines[textBox1.Lines.Length - 3] == ""))
            {
                ParseText(textBox1.Lines);
                textBox1.Text = "";
            }
        }
    }
}
