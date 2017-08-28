using System;
using System.Windows.Forms;

namespace MAF_Tuning_Helper_Tool
{
    public partial class CsvLoadForm : Form
    {
        public CsvLoadForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select CSV File";
                ofd.Filter = "CSV File | *.csv";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    CsvDataParser cdp = new CsvDataParser(ofd.FileName);
                    Hide();
                }
            }
        }
    }
}
