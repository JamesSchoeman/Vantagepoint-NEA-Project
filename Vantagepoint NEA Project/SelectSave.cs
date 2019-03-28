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
            RefreshSaves();
        }

        List<string> allFiles = System.IO.Directory.GetFiles("Saves").ToList();

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshSaves();
        }

        private void RefreshSaves()
        {
            allFiles = System.IO.Directory.GetFiles("Saves").ToList();
            SaveList.Items.Clear();
            foreach (int i in Enumerable.Range(0, allFiles.Count))
            {
                if (allFiles[i].Contains(".xml") == true && allFiles[i].Contains(SearchBar.Text) == true)
                {
                    SaveList.Items.Add(allFiles[i].Remove(allFiles[i].Count() - 4, 4).Remove(0, 6));
                }
            }
        }

        private void ConfirmSave_Click(object sender, EventArgs e)
        {
            LoadedBoardGame.filePath = "Saves\\" + SaveList.SelectedItem + ".xml";
            this.Close();
        }
    }
}
