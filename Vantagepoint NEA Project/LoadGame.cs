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
    public partial class LoadGame : Form
    {
        public LoadGame()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainMenu newMainMenu = new MainMenu();
            this.Hide();
            newMainMenu.ShowDialog();
            this.Close();
        }
    }
}
