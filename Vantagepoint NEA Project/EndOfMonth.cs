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
    public partial class EndOfMonth : Form
    {
        public EndOfMonth()
        {
            InitializeComponent();

            foreach (var i in Application.OpenForms)
            {
                if (string.Concat(i) == "Vantagepoint_NEA_Project.BoardGame, Text: Board Game")
                {
                    parentType = "NonLoaded";
                    Console.WriteLine("NonLoaded");
                    break;
                }
                if (string.Concat(i) == "Vantagepoint_NEA_Project.LoadedBoardGame, Text: Board Game")
                {
                    parentType = "Loaded";
                    Console.WriteLine("Loaded");
                    Console.WriteLine(i);
                    break;
                }
            }

            if (parentType == "NonLoaded")
            {
                CapitalDisplay.Text = string.Concat(BoardGame.shareCapital);
                StaffDisplay.Text = string.Concat(BoardGame.staff);
                if (BoardGame.companyType == "Sole Trader")
                {
                    StaffLimitDisplay.Text = "/0";
                }
                else if (BoardGame.companyType == "Partnership")
                {
                    StaffLimitDisplay.Text = "/1";
                }
                else if (BoardGame.companyType == "Limited")
                {
                    StaffLimitDisplay.Text = "/3";
                }
            }
        }

        public string parentType = null;

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EndOfMonth_Load(object sender, EventArgs e)
        {
            
        }

        private void PayVATButton_Click(object sender, EventArgs e)
        {

        }
    }
}
