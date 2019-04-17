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

        //Initialises the subroutine and sets all its attributes to the correct state
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

                    CNDisplay.Text = BoardGame.companyName;
                    SHDisplay.Text = BoardGame.shareholders;
                    NOBDisplay.Text = BoardGame.natureOfBusiness;
                    CTDisplay.Text = BoardGame.companyType;

                    int minutesRemaining = (int)(Math.Floor(BoardGame.timeLimit / 60.0));
                    int secondsRemaining = (int)(Math.Floor(BoardGame.timeLimit - (minutesRemaining * 60.0)));

                    TimerMinutesDisplay.Text = string.Concat(minutesRemaining) + " minutes";
                    TimerSecondsDisplay.Text = string.Concat(secondsRemaining) + " seconds";

                    StockDisplay.Text = string.Concat(BoardGame.stock);
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

                    if (BoardGame.hasBEE == true)
                    {
                        if (AgreementsDisplay.Text == "")
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + "BEE Agreement";
                        }
                        else
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + ", BEE Agreement";
                        }
                    }

                    if (BoardGame.hasWebsite == true)
                    {
                        if (AgreementsDisplay.Text == "")
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + "Website";
                        }
                        else
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + ", Website";
                        }
                    }

                    if (BoardGame.hasHealthCare == true)
                    {
                        if (AgreementsDisplay.Text == "")
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + "Healthcare Agreement";
                        }
                        else
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + ", Healthcare Agreement";
                        }
                    }

                    if (BoardGame.hasPR == true)
                    {
                        if (AgreementsDisplay.Text == "")
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + "PR Agreement";
                        }
                        else
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + ", PR Agreement";
                        }
                    }

                    if (BoardGame.hasMarketing == true)
                    {
                        if (AgreementsDisplay.Text == "")
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + "Marketing Agreement";
                        }
                        else
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + ", Marketing Agreement";
                        }
                    }

                    if (BoardGame.hasInsurance == true)
                    {
                        if (AgreementsDisplay.Text == "")
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + "Insurance";
                        }
                        else
                        {
                            AgreementsDisplay.Text = AgreementsDisplay.Text + ", Insurance";
                        }
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

                CNDisplay.Text = LoadedBoardGame.companyName;
                SHDisplay.Text = LoadedBoardGame.shareholders;
                NOBDisplay.Text = LoadedBoardGame.natureOfBusiness;
                CTDisplay.Text = LoadedBoardGame.companyType;

                int minutesRemaining = (int)(Math.Floor(LoadedBoardGame.timeLimit / 60.0));
                int secondsRemaining = (int)(Math.Floor(LoadedBoardGame.timeLimit - (minutesRemaining * 60.0)));

                TimerMinutesDisplay.Text = string.Concat(minutesRemaining) + " minutes";
                TimerSecondsDisplay.Text = string.Concat(secondsRemaining) + " seconds";

                StockDisplay.Text = string.Concat(LoadedBoardGame.stock);
                StaffDisplay.Text = string.Concat(LoadedBoardGame.staff);

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

                if (LoadedBoardGame.hasBEE == true)
                {
                    if (AgreementsDisplay.Text == "")
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + "BEE Agreement";
                    }
                    else
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + ", BEE Agreement";
                    }
                }

                if (LoadedBoardGame.hasWebsite == true)
                {
                    if (AgreementsDisplay.Text == "")
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + "Website";
                    }
                    else
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + ", Website";
                    }
                }

                if (LoadedBoardGame.hasHealthCare == true)
                {
                    if (AgreementsDisplay.Text == "")
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + "Healthcare Agreement";
                    }
                    else
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + ", Healthcare Agreement";
                    }
                }

                if (LoadedBoardGame.hasPR == true)
                {
                    if (AgreementsDisplay.Text == "")
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + "PR Agreement";
                    }
                    else
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + ", PR Agreement";
                    }
                }

                if (LoadedBoardGame.hasMarketing == true)
                {
                    if (AgreementsDisplay.Text == "")
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + "Marketing Agreement";
                    }
                    else
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + ", Marketing Agreement";
                    }
                }

                if (LoadedBoardGame.hasInsurance == true)
                {
                    if (AgreementsDisplay.Text == "")
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + "Insurance";
                    }
                    else
                    {
                        AgreementsDisplay.Text = AgreementsDisplay.Text + ", Insurance";
                    }
                }
            }
            CapitalDisplay.Text = string.Concat(localCapital);
        }

        //Deducts 300000 from the user's capital and updates the display
        private void PayLoanButton_Click(object sender, EventArgs e)
        {
            localCapital = localCapital - 300000;
            PostLoanDisplay.Text = string.Concat(localCapital);
            PayLoanButton.Enabled = false;
            CloseButton.Enabled = true;
        }

        //Closes the program
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
