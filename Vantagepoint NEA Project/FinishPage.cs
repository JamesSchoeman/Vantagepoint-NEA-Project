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
        public float localCapital = new float();

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
                localCapital = BoardGame.shareCapital;
                if (BoardGame.bankLoanTaken == false)
                {
                    PayLoanButton.Enabled = false;
                    CloseButton.Enabled = true;
                    PostLoanDisplay.Text = string.Concat(BoardGame.shareCapital);
                }
            }

            if (parentType == "Loaded")
            {
                localCapital = LoadedBoardGame.shareCapital;
                if (LoadedBoardGame.bankLoanTaken == false)
                {
                    PayLoanButton.Enabled = false;
                    CloseButton.Enabled = true;
                    PostLoanDisplay.Text = string.Concat(LoadedBoardGame.shareCapital);
                }
            }
            CapitalDisplay.Text = "£" + string.Concat(localCapital);
        }

        private void PayLoanButton_Click(object sender, EventArgs e)
        {
            localCapital = localCapital - 300000;
            PostLoanDisplay.Text = "£" + string.Concat(localCapital);
            PayLoanButton.Enabled = false;
            CloseButton.Enabled = true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
