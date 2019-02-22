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
    public partial class FinishPage : Form
    {
        public string parentType = null;

        public FinishPage()
        {
            InitializeComponent();

            foreach (var i in Application.OpenForms)
            {
                if (string.Concat(i) == "Vantagepoint_NEA_Project.BoardGame, Text: Board Game")
                {
                    parentType = "NonLoaded";
                    break;
                }
                if (string.Concat(i) == "Vantagepoint_NEA_Project.LoadedBoardGame, Text: Board Game")
                {
                    parentType = "Loaded";
                    break;
                }
            }
            
            if (parentType == "NonLoaded")
            {
                
            }

            if (parentType == "Loaded")
            {

            }
        }

        private void PayLoanButton_Click(object sender, EventArgs e)
        {

        }
    }
}
