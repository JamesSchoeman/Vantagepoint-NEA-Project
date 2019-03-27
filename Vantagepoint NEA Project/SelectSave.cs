using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vantagepoint_NEA_Project
{
    public partial class SelectSave : Form
    {
        public SelectSave()
        {
            InitializeComponent();
        }

        List<string> allFiles = System.IO.Directory.GetFiles("Saves", ".xml").ToList();
        List<string> validFiles = new List<string>();

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            allFiles = System.IO.Directory.GetFiles("Saves").ToList();
            validFiles.Clear();
            foreach (int i in Enumerable.Range(0, allFiles.Count))
            {
                if (allFiles[i].Contains(".xml") == true && allFiles[i].Contains(SearchBar.Text) == true)
                {
                    validFiles.Add(allFiles[i]);
                }
            }
            SaveList.DataSource = validFiles;
        }
    }
}
