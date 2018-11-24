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

        public string parentType = null;
        public bool paidVat = false;
        public bool convertedOrders = false;
        public bool convertedOpportunities = false;
        public bool paidStaff = false;


        public EndOfMonth()
        {
            InitializeComponent();

            paidVat = false;
            convertedOrders = false;
            convertedOpportunities = false;
            paidStaff = false;

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
                PayVATButton.Text = "Pay VAT of £" + string.Concat(BoardGame.shareCapital / 10);
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
            else if (parentType == "Loaded")
            {
                CapitalDisplay.Text = string.Concat(LoadedBoardGame.shareCapital);
                StaffDisplay.Text = string.Concat(LoadedBoardGame.staff);
                PayVATButton.Text = "Pay VAT of £" + string.Concat(LoadedBoardGame.shareCapital / 10);
                if (LoadedBoardGame.companyType == "Sole Trader")
                {
                    StaffLimitDisplay.Text = "/0";
                }
                else if (LoadedBoardGame.companyType == "Partnership")
                {
                    StaffLimitDisplay.Text = "/1";
                }
                else if (LoadedBoardGame.companyType == "Limited")
                {
                    StaffLimitDisplay.Text = "/3";
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EndOfMonth_Load(object sender, EventArgs e)
        {
            
        }

        private void PayVATButton_Click(object sender, EventArgs e)
        {
            if (parentType == "NonLoaded")
            {
                BoardGame.shareCapital = BoardGame.shareCapital - (BoardGame.shareCapital / 10);
                CapitalDisplay.Text = string.Concat(BoardGame.shareCapital);
            }
            else if (parentType == "Loaded")
            {
                LoadedBoardGame.shareCapital = LoadedBoardGame.shareCapital - (LoadedBoardGame.shareCapital / 10);
                CapitalDisplay.Text = string.Concat(LoadedBoardGame.shareCapital);
            }
            paidVat = true;
            PayVATButton.Enabled = false;
        }
    }
}
