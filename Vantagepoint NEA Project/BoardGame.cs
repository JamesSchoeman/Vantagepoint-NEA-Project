using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;

namespace Vantagepoint_NEA_Project
{
    public partial class BoardGame : Form
    {
        //Initialises the program and sets all attributes to their correct values
        public BoardGame()
        {
            InitializeComponent();

            this.CNDisplay.Text = companyName;
            this.SHDisplay.Text = shareholders;
            this.NOBDisplay.Text = natureOfBusiness;
            this.CTDisplay.Text = companyType;
            this.StockDisplay.Text = string.Concat(stock);
            this.StaffDisplay.Text = string.Concat(staff);
            this.CapitalDisplay.Text = string.Concat(shareCapital);
            if (companyType == "Sole Trader")
            {
                StaffLimitDisplay.Text = "/0";
            }
            else if (companyType == "Partnership")
            {
                StaffLimitDisplay.Text = "/1";
            }
            else if (companyType == "Limited")
            {
                StaffLimitDisplay.Text = "/3";
            }
            SquareDisplay.ImageLocation = ("Board Images\\" + boardPosition + ".JPG");
            SquareDisplay.SizeMode = PictureBoxSizeMode.Zoom;
            BoardDisplay.ImageLocation = ("Board Images\\VP Board.JPG");
            BoardDisplay.SizeMode = PictureBoxSizeMode.Zoom;

            dataAdapter.UpdateCommand = squaresTableCommand;
            dataAdapter.SelectCommand = squaresTableCommand;
            squaresTableCommand.CommandType = CommandType.Text;
            squaresTableCommand.Connection = dataConnection;
            dataConnection.Open();
            dataAdapter.Fill(squaresTable);
            dataConnection.Close();

            dataAdapter.UpdateCommand = cashFlowCommand;
            dataAdapter.SelectCommand = cashFlowCommand;
            cashFlowCommand.CommandType = CommandType.Text;
            cashFlowCommand.Connection = dataConnection;
            dataConnection.Open();
            dataAdapter.Fill(cashFlowTable);
            dataConnection.Close();

            dataAdapter.UpdateCommand = luckyBreakCommand;
            dataAdapter.SelectCommand = luckyBreakCommand;
            luckyBreakCommand.CommandType = CommandType.Text;
            luckyBreakCommand.Connection = dataConnection;
            dataConnection.Open();
            dataAdapter.Fill(luckyBreakTable);
            dataConnection.Close();

            dataAdapter.UpdateCommand = moralDilemmasCommand;
            dataAdapter.SelectCommand = moralDilemmasCommand;
            moralDilemmasCommand.CommandType = CommandType.Text;
            moralDilemmasCommand.Connection = dataConnection;
            dataConnection.Open();
            dataAdapter.Fill(moralDilemmasTable);
            dataConnection.Close();

            dataAdapter.UpdateCommand = penaltyCommand;
            dataAdapter.SelectCommand = penaltyCommand;
            penaltyCommand.CommandType = CommandType.Text;
            penaltyCommand.Connection = dataConnection;
            dataConnection.Open();
            dataAdapter.Fill(penaltyTable);
            dataConnection.Close();

            string squareNamevar = "";
            string squareDescvar = "";
            squareNamevar = string.Concat(squaresTable.Rows[boardPosition - 1][1]);
            this.SquareNameDisplay.Text = squareNamevar;
            squareDescvar = string.Concat(squaresTable.Rows[boardPosition - 1][2]);
            this.DescriptionDisplay.Text = squareDescvar;

            if (companyType == "Sole Trader")
            {
                hasBEE = true;
            }

            if (timeLimit == 0)
            {
                timer1.Stop();
                TimerMinutesDisplay.Text = "No time limit";
                TimerSecondsDisplay.Text = "";
            }

            Square1();
        }

        public static string companyType = CreateCompany.companyType;
        public static string companyName = CreateCompany.companyName;
        public static string shareholders = CreateCompany.shareholders;
        public static string natureOfBusiness = CreateCompany.natureOfBusiness;
        public static float shareCapital = CreateCompany.shareCapital;
        public static int timeLimit = (CreateCompany.timeLimit * 60);
        public static int diceRollResult;
        public static int boardPosition = 1;
        public static int newBoardPosition;
        public static bool regFeesPaid = false;
        public static bool bankLoanTaken = false;
        public Button TakeLoanButton;
        public Button DoNotTakeLoanButton;
        public Button RecruitStaffButton;
        public Button DoNotRecruitStaffButton;
        public static bool hasWebsite = false;
        public static bool hasHealthCare = false;
        public static bool hasBEE = false;
        public static bool hasPR = false;
        public static bool hasMarketing = false;
        public static bool hasInsurance = false;
        public static List<int> salesOpportunities = new List<int>();
        public static List<int> salesOrders = new List<int>();
        public static int stock = 0;
        public static int staff = 0;

        SqlConnection dataConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\SquaresDatabase.mdf;Integrated Security = True");
        SqlDataAdapter dataAdapter = new SqlDataAdapter();

        DataTable squaresTable = new DataTable();
        static SqlCommand squaresTableCommand = new SqlCommand("select * from Squares");

        DataTable cashFlowTable = new DataTable();
        static SqlCommand cashFlowCommand = new SqlCommand("select * from CashFlow");

        DataTable luckyBreakTable = new DataTable();
        static SqlCommand luckyBreakCommand = new SqlCommand("select * from LuckyBreak");

        DataTable moralDilemmasTable = new DataTable();
        static SqlCommand moralDilemmasCommand = new SqlCommand("select * from MoralDilemmas");

        DataTable penaltyTable = new DataTable();
        static SqlCommand penaltyCommand = new SqlCommand("select * from Penalty");

        Random rnd = new Random();

        private void Board_Game_Load(object sender, EventArgs e)
        {

        }

        //Checks if the player has gone bankrupt, rolls the dice, moves the player, enacts end of month procedures if necessary, calls the subroutine of the square the player has landed on
        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            foreach (int i in Enumerable.Range(0, salesOpportunities.Count))
            {
                Console.WriteLine(string.Concat(salesOpportunities[i]));
            }

            if (shareCapital < 0)
            {
                MessageBox.Show("You have insufficient share capital to continue.", "Bankrupt!");
                FinishGame();
            }

            if (companyType == "Sole Trader")
            {
                hasBEE = true;
            }

            diceRollResult = rnd.Next(1, 7);
            this.RollResultDisplay.Text = string.Concat(diceRollResult);

            newBoardPosition = boardPosition + diceRollResult;
            if ((newBoardPosition > 36) && (boardPosition < 36))
            {
                newBoardPosition = newBoardPosition - 36;
                EndOfMonth monthlyReport = new EndOfMonth();
                monthlyReport.ShowDialog(this);
                CapitalDisplay.Text = string.Concat(shareCapital);
                foreach (int i in Enumerable.Range(0, staff + 1))
                {
                    SalesOpportunity();
                }
            }
            else if (newBoardPosition > 36)
            {
                newBoardPosition = newBoardPosition - 36;
            }

            string squareNamevar = "";
            string squareDescvar = "";
            squareNamevar = string.Concat(squaresTable.Rows[newBoardPosition - 1][1]);
            this.SquareNameDisplay.Text = squareNamevar;
            squareDescvar = string.Concat(squaresTable.Rows[newBoardPosition - 1][2]);
            this.DescriptionDisplay.Text = squareDescvar;
            SquareDisplay.ImageLocation = ("Board Images\\" + newBoardPosition + ".JPG");
            SquareDisplay.SizeMode = PictureBoxSizeMode.Zoom;

            if (newBoardPosition == 2)
            {
                Square2();
            }
            else if (newBoardPosition == 3)
            {
                LuckyBreak();
            }
            else if (newBoardPosition == 4)
            {
                CashFlow();
            }
            else if (newBoardPosition == 5)
            {
                Penalty();
            }
            else if (newBoardPosition == 6)
            {
                Square6();
            }
            else if (newBoardPosition == 7)
            {
                CashFlow();
            }
            else if (newBoardPosition == 8)
            {
                StaffRecruitment();
            }
            else if (newBoardPosition == 9)
            {
                SalesOpportunity();
            }
            else if (newBoardPosition == 10)
            {
                Square10();
            }
            else if (newBoardPosition == 11)
            {
                CashFlow();
            }
            else if (newBoardPosition == 12)
            {
                Square12();
            }
            else if (newBoardPosition == 13)
            {
                LuckyBreak();
            }
            else if (newBoardPosition == 14)
            {
                StaffRecruitment();
            }
            else if (newBoardPosition == 15)
            {
                Penalty();
            }
            else if (newBoardPosition == 16)
            {
                Square16();
            }
            else if (newBoardPosition == 17)
            {
                GetInsurance();
            }
            else if (newBoardPosition == 18)
            {
                SalesOpportunity();
            }
            else if (newBoardPosition == 19)
            {
                Square19();
            }
            else if (newBoardPosition == 20)
            {
                CashFlow();
            }
            else if (newBoardPosition == 21)
            {
                Square21();
            }
            else if (newBoardPosition == 22)
            {
                LuckyBreak();
            }
            else if (newBoardPosition == 23)
            {
                CashFlow();
            }
            else if (newBoardPosition == 24)
            {
                Penalty();
            }
            else if (newBoardPosition == 25)
            {
                StaffRecruitment();
            }
            else if (newBoardPosition == 26)
            {
                Square26();
            }
            else if (newBoardPosition == 27)
            {
                SalesOpportunity();
            }
            else if (newBoardPosition == 28)
            {
                Square28();
            }
            else if (newBoardPosition == 29)
            {
                CashFlow();
            }
            else if (newBoardPosition == 30)
            {
                Square30();
            }
            else if (newBoardPosition == 31)
            {
                LuckyBreak();
            }
            else if (newBoardPosition == 32)
            {
                StaffRecruitment();
            }
            else if (newBoardPosition == 33)
            {
                Penalty();
            }
            else if (newBoardPosition == 34)
            {
                CashFlow();
            }
            else if (newBoardPosition == 35)
            {
                Square35();
            }
            else if (newBoardPosition == 36)
            {
                EndOfMonth monthlyReport = new EndOfMonth();
                monthlyReport.ShowDialog(this);
                CapitalDisplay.Text = string.Concat(shareCapital);
                foreach (int i in Enumerable.Range(0, staff + 1))
                {
                    SalesOpportunity();
                }
            }

            if ((boardPosition < 9) && (newBoardPosition > 9))
            {
                SalesOpportunity();
            }

            if ((boardPosition < 18) && (newBoardPosition > 18))
            {
                SalesOpportunity();
            }

            if ((boardPosition < 27) && (newBoardPosition > 27))
            {
                SalesOpportunity();
            }

            boardPosition = newBoardPosition;

        }

        //Quits the game, after checking with the player
        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            DialogResult answer;
            answer = MessageBox.Show("Are you sure you want to quit? ", "Are you sure? ", MessageBoxButtons.YesNo);

            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //Defines the class to be used to save data
        public class DataToBeSaved
        {
            public string saveCompanyType;
            public string saveCompanyName;
            public string saveShareholders;
            public string saveNatureOfBusiness;
            public float saveShareCapital;
            public int saveTimeLimit;
            public int saveBoardPosition;
            public bool saveRegFeesPaid;
            public bool saveBankLoanTaken;
            public bool saveHasWebsite;
            public bool saveHasHealthCare;
            public bool saveHasBEE;
            public bool saveHasPR;
            public bool saveHasMarketing;
            public List<int> saveSalesOpportunities;
            public List<int> saveSalesOrders;
            public int saveStock;
            public int saveStaff;
            public bool saveHasInsurance;
        }

        //Calls the SaveGame subroutine
        private void SaveGameButton_Click(object sender, EventArgs e)
        {
            SaveGame();
        }

        //Charges the player a different amount of money depending on their company type
        private void Square1()
        {
            if (regFeesPaid == false)
            {
                if (companyType == "Sole Trader")
                {
                    UpdateCapital(-5000);
                }
                else if (companyType == "Partnership")
                {
                    UpdateCapital(-10000);
                }
                else if (companyType == "Limited")
                {
                    UpdateCapital(-20000);
                }
                regFeesPaid = true;
            }
        }

        //Offers the player a bank loan, if they have not already taken one
        private void Square2()
        {
            if (bankLoanTaken == false)
            {
                Button TakeLoan = new Button();
                TakeLoan.Location = new System.Drawing.Point(422, 340);
                TakeLoan.Size = new System.Drawing.Size(87, 46);
                TakeLoan.Text = "Take loan";
                TakeLoanButton = TakeLoan;
                Button DoNotTakeLoan = new Button();
                DoNotTakeLoan.Location = new System.Drawing.Point(422, 392);
                DoNotTakeLoan.Size = new System.Drawing.Size(87, 46);
                DoNotTakeLoan.Text = "Continue";
                DoNotTakeLoanButton = DoNotTakeLoan;
                this.Controls.Add(TakeLoan);
                this.Controls.Add(DoNotTakeLoan);
                TakeLoan.Click += TakeLoan_Click;
                DoNotTakeLoan.Click += DoNotTakeLoan_Click;
                RollDiceButton.Enabled = false;
                SaveGameButton.Enabled = false;
                FinishGameButton.Enabled = false;
                ExitGameButton.Enabled = false;
            }
        }

        //Gives the player the bank loan, should they accept the offer
        void TakeLoan_Click(object sender, EventArgs e)
        {
            UpdateCapital(250000);
            bankLoanTaken = true;
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
            DoNotTakeLoanButton.Enabled = false;
            DoNotTakeLoanButton.Visible = false;
        }

        //Moves the program on, should the player reject the offer of a bank loan
        void DoNotTakeLoan_Click(object sender, EventArgs e)
        {
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            TakeLoanButton.Visible = false;
            TakeLoanButton.Enabled = false;
        }

        //Creates button
        private void Square6()
        {
            Button PayForEquipment = new Button();
            PayForEquipment.Location = new System.Drawing.Point(422, 392);
            PayForEquipment.Size = new System.Drawing.Size(87, 46);
            PayForEquipment.Text = "Pay £15000";
            this.Controls.Add(PayForEquipment);
            PayForEquipment.Click += PayForEquipment_Click;
            RollDiceButton.Enabled = false;
            SaveGameButton.Enabled = false;
            FinishGameButton.Enabled = false;
            ExitGameButton.Enabled = false;

        }

        //Deducts set amount of capital
        void PayForEquipment_Click(object sender, EventArgs e)
        {
            UpdateCapital(-15000);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
        }

        //Creates button whose contents differ depending on a whether the player already has a website
        private void Square10()
        {
            Button PayForWebsite = new Button();
            PayForWebsite.Location = new System.Drawing.Point(422, 392);
            PayForWebsite.Size = new System.Drawing.Size(87, 46);
            this.Controls.Add(PayForWebsite);
            PayForWebsite.Click += PayForWebsite_Click;
            RollDiceButton.Enabled = false;
            SaveGameButton.Enabled = false;
            FinishGameButton.Enabled = false;
            ExitGameButton.Enabled = false;
            if (hasWebsite == false)
            {
                PayForWebsite.Text = "Pay £20000";
            }
            else if (hasWebsite == true)
            {
                PayForWebsite.Text = "Renew for £10000";
            }
        }
        
        //Deducts different amount of money depending on a whether the player already has a website
        void PayForWebsite_Click(object sender, EventArgs e)
        {
            if (hasWebsite == false)
            {
                UpdateCapital(-20000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
                hasWebsite = true;
            }
            else if (hasWebsite == true)
            {
                UpdateCapital(-10000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
            }
        }
        //Creates button
        private void Square12()
        {
            Button PayForRates = new Button();
            PayForRates.Location = new System.Drawing.Point(422, 392);
            PayForRates.Size = new System.Drawing.Size(87, 46);
            PayForRates.Text = "Pay £15000";
            this.Controls.Add(PayForRates);
            PayForRates.Click += PayForRates_Click;
            RollDiceButton.Enabled = false;
            SaveGameButton.Enabled = false;
            FinishGameButton.Enabled = false;
            ExitGameButton.Enabled = false;
        }

        //Deducts set amount of capital
        void PayForRates_Click(object sender, EventArgs e)
        {
            UpdateCapital(-15000);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
        }

        //Creates button
        private void Square21()
        {
            Button PayForPremises = new Button();
            PayForPremises.Location = new System.Drawing.Point(422, 392);
            PayForPremises.Size = new System.Drawing.Size(87, 46);
            PayForPremises.Text = "Pay £30000";
            this.Controls.Add(PayForPremises);
            PayForPremises.Click += PayForPremises_Click;
            RollDiceButton.Enabled = false;
            SaveGameButton.Enabled = false;
            FinishGameButton.Enabled = false;
            ExitGameButton.Enabled = false;
        }

        //Deducts set amount of capital
        void PayForPremises_Click(object sender, EventArgs e)
        {
            UpdateCapital(-30000);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
        }

        //Creates button
        private void Square28()
        {
            Button PayForTelephone = new Button();
            PayForTelephone.Location = new System.Drawing.Point(422, 392);
            PayForTelephone.Size = new System.Drawing.Size(87, 46);
            PayForTelephone.Text = "Pay £20000";
            this.Controls.Add(PayForTelephone);
            PayForTelephone.Click += PayForTelephone_Click;
            RollDiceButton.Enabled = false;
            SaveGameButton.Enabled = false;
            FinishGameButton.Enabled = false;
            ExitGameButton.Enabled = false;
        }

        //Deducts set amount of capital
        void PayForTelephone_Click(object sender, EventArgs e)
        {
            UpdateCapital(-20000);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
        }

        //Creates button whose contents differ depending on a whether the player already has a healthcare agreement
        private void Square19()
        {
            if (hasHealthCare == false)
            {
                Button PayForHealthCare = new Button();
                PayForHealthCare.Location = new System.Drawing.Point(422, 392);
                PayForHealthCare.Size = new System.Drawing.Size(87, 46);
                PayForHealthCare.Text = "Pay £20000";
                this.Controls.Add(PayForHealthCare);
                PayForHealthCare.Click += PayForHealthCare_Click;
                RollDiceButton.Enabled = false;
                SaveGameButton.Enabled = false;
                FinishGameButton.Enabled = false;
                ExitGameButton.Enabled = false;
            }
            else if (hasHealthCare == true)
            {
                Button PayForHealthCare = new Button();
                PayForHealthCare.Location = new System.Drawing.Point(422, 392);
                PayForHealthCare.Size = new System.Drawing.Size(87, 46);
                PayForHealthCare.Text = "Renew for £10000";
                this.Controls.Add(PayForHealthCare);
                PayForHealthCare.Click += PayForHealthCare_Click;
                RollDiceButton.Enabled = false;
                SaveGameButton.Enabled = false;
                FinishGameButton.Enabled = false;
                ExitGameButton.Enabled = false;
            }
        }

        //Deducts different amount of money depending on a whether the player already has a healthcare agreement
        void PayForHealthCare_Click(object sender, EventArgs e)
        {
            if (hasHealthCare == false)
            {
                UpdateCapital(-20000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
                hasHealthCare = true;
            }
            else if (hasHealthCare == true)
            {
                UpdateCapital(-10000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
            }
        }

        //Creates button if the company type is not sole trader and whose contents differ depending on whether the user already has a BEE agreement
        private void Square30()
        {
            if (companyType != "Sole Trader")
            {
                if (hasBEE == false)
                {
                    Button PayForBEE = new Button();
                    PayForBEE.Location = new System.Drawing.Point(422, 392);
                    PayForBEE.Size = new System.Drawing.Size(87, 46);
                    PayForBEE.Text = "Pay £30000";
                    this.Controls.Add(PayForBEE);
                    PayForBEE.Click += PayForBEE_Click;
                    RollDiceButton.Enabled = false;
                    SaveGameButton.Enabled = false;
                    FinishGameButton.Enabled = false;
                    ExitGameButton.Enabled = false;
                }
                else if (hasBEE == true)
                {
                    Button PayForBEE = new Button();
                    PayForBEE.Location = new System.Drawing.Point(422, 392);
                    PayForBEE.Size = new System.Drawing.Size(87, 46);
                    PayForBEE.Text = "Renew for £15000";
                    this.Controls.Add(PayForBEE);
                    PayForBEE.Click += PayForBEE_Click;
                    RollDiceButton.Enabled = false;
                    SaveGameButton.Enabled = false;
                    FinishGameButton.Enabled = false;
                    ExitGameButton.Enabled = false;
                }
            }
        }

        //Deducts different amount of money depending on a whether the player already has a BEE agreement
        void PayForBEE_Click(object sender, EventArgs e)
        {
            if (hasBEE == false)
            {
                UpdateCapital(-30000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
                hasBEE = true;
            }
            else if (hasBEE == true)
            {
                UpdateCapital(-15000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
            }
        }

        //Creates button whose contents differ depending on whether the user already has a PR agreement
        private void Square26()
        {
            if (hasPR == false)
            {
                Button PayForPR = new Button();
                PayForPR.Location = new System.Drawing.Point(422, 392);
                PayForPR.Size = new System.Drawing.Size(87, 46);
                PayForPR.Text = "Pay £30000";
                this.Controls.Add(PayForPR);
                PayForPR.Click += PayForPR_Click;
                RollDiceButton.Enabled = false;
                SaveGameButton.Enabled = false;
                FinishGameButton.Enabled = false;
                ExitGameButton.Enabled = false;
            }
            else if (hasPR == true)
            {
                Button PayForPR = new Button();
                PayForPR.Location = new System.Drawing.Point(422, 392);
                PayForPR.Size = new System.Drawing.Size(87, 46);
                PayForPR.Text = "Renew for £15000";
                this.Controls.Add(PayForPR);
                PayForPR.Click += PayForPR_Click;
                RollDiceButton.Enabled = false;
                SaveGameButton.Enabled = false;
                FinishGameButton.Enabled = false;
                ExitGameButton.Enabled = false;
            }
        }

        //Deducts different amount of money depending on a whether the player already has a PR agreement
        void PayForPR_Click(object sender, EventArgs e)
        {
            if (hasPR == false)
            {
                UpdateCapital(-30000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
                hasPR = true;
            }
            else if (hasPR == true)
            {
                UpdateCapital(-15000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
            }
        }

        //Creates button whose contents differ depending on whether the user already has a Marketing agreement
        private void Square35()
        {
            if (hasMarketing == false)
            {
                Button PayForMarketing = new Button();
                PayForMarketing.Location = new System.Drawing.Point(422, 392);
                PayForMarketing.Size = new System.Drawing.Size(87, 46);
                PayForMarketing.Text = "Pay £30000";
                this.Controls.Add(PayForMarketing);
                PayForMarketing.Click += PayForMarketing_Click;
                RollDiceButton.Enabled = false;
                SaveGameButton.Enabled = false;
                FinishGameButton.Enabled = false;
                ExitGameButton.Enabled = false;
            }
            else if (hasMarketing == true)
            {
                Button PayForMarketing = new Button();
                PayForMarketing.Location = new System.Drawing.Point(422, 392);
                PayForMarketing.Size = new System.Drawing.Size(87, 46);
                PayForMarketing.Text = "Renew for £15000";
                this.Controls.Add(PayForMarketing);
                PayForMarketing.Click += PayForMarketing_Click;
                RollDiceButton.Enabled = false;
                SaveGameButton.Enabled = false;
                FinishGameButton.Enabled = false;
                ExitGameButton.Enabled = false;
            }
        }

        //Deducts different amount of money depending on a whether the player already has a Marketing agreement
        void PayForMarketing_Click(object sender, EventArgs e)
        {
            if (hasMarketing == false)
            {
                UpdateCapital(-30000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
                hasMarketing = true;
            }
            else if (hasMarketing == true)
            {
                UpdateCapital(-15000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
            }
        }

        //Creates button
        void Square16()
        {
            Button BuyStockButton = new Button();
            BuyStockButton.Location = new System.Drawing.Point(422, 392);
            BuyStockButton.Size = new System.Drawing.Size(87, 46);
            BuyStockButton.Text = "Pay £50000";
            this.Controls.Add(BuyStockButton);
            BuyStockButton.Click += BuyStockButton_Click;
            RollDiceButton.Enabled = false;
            SaveGameButton.Enabled = false;
            FinishGameButton.Enabled = false;
            ExitGameButton.Enabled = false;
        }

        //Deducts set amount of capital
        void BuyStockButton_Click(object sender, EventArgs e)
        {
            UpdateCapital(-50000);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
            stock = stock + 1;
            StockDisplay.Text = string.Concat(stock);
        }

        //Creates button if the company type is not sole trader and disables it if the user has staff equal to or greater than the staff cap for their company type
        void StaffRecruitment()
        {
            if (companyType != "Sole Trader")
            {
                Button RecruitStaff = new Button();
                RecruitStaff.Location = new System.Drawing.Point(422, 392);
                RecruitStaff.Size = new System.Drawing.Size(87, 46);
                RecruitStaff.Text = "Pay £25000";
                this.Controls.Add(RecruitStaff);
                RecruitStaff.Click += RecruitStaff_Click;
                if ((companyType == "Partnership") && (staff > 0))
                {
                    RecruitStaff.Enabled = false;
                }
                else if ((companyType == "Limited") && (staff > 2))
                {
                    RecruitStaff.Enabled = false;
                }
                RecruitStaffButton = RecruitStaff;
                Button DoNotRecruitStaff = new Button();
                DoNotRecruitStaff.Location = new System.Drawing.Point(422, 340);
                DoNotRecruitStaff.Size = new System.Drawing.Size(87, 46);
                DoNotRecruitStaff.Text = "Do not recruit staff";
                this.Controls.Add(DoNotRecruitStaff);
                DoNotRecruitStaff.Click += DoNotRecruitStaff_Click;
                DoNotRecruitStaffButton = DoNotRecruitStaff;
                RollDiceButton.Enabled = false;
                SaveGameButton.Enabled = false;
                FinishGameButton.Enabled = false;
                ExitGameButton.Enabled = false;
            }
        }

        //Deducts set amount of capital
        void RecruitStaff_Click(object sender, EventArgs e)
        {
            UpdateCapital(-25000);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            DoNotRecruitStaffButton.Enabled = false;
            DoNotRecruitStaffButton.Visible = false;
            staff = staff + 1;
            StaffDisplay.Text = string.Concat(staff);
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
        }

        //Advances the game should the user choose not to recruit staff
        void DoNotRecruitStaff_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RecruitStaffButton.Enabled = false;
            RecruitStaffButton.Visible = false;
            RollDiceButton.Enabled = true;
            SaveGameButton.Enabled = true;
            FinishGameButton.Enabled = true;
            ExitGameButton.Enabled = true;
        }

        //Creates button whose contents differ depending on whether the user already has Insurance
        private void GetInsurance()
        {
            Button PayForInsurance = new Button();
            PayForInsurance.Location = new System.Drawing.Point(422, 392);
            PayForInsurance.Size = new System.Drawing.Size(87, 46);
            this.Controls.Add(PayForInsurance);
            PayForInsurance.Click += PayForInsurance_Click;
            RollDiceButton.Enabled = false;
            SaveGameButton.Enabled = false;
            FinishGameButton.Enabled = false;
            ExitGameButton.Enabled = false;
            if (hasInsurance == false)
            {
                PayForInsurance.Text = "Pay £30000";
            }
            else if (hasInsurance == true)
            {
                PayForInsurance.Text = "Renew for £15000";
            }
        }

        //Deducts different amount of money depending on a whether the player already has Insurance
        void PayForInsurance_Click(object sender, EventArgs e)
        {
            if (hasInsurance == false)
            {
                UpdateCapital(-30000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
                hasInsurance = true;
            }
            else if (hasInsurance == true)
            {
                UpdateCapital(-15000);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                SaveGameButton.Enabled = true;
                FinishGameButton.Enabled = true;
                ExitGameButton.Enabled = true;
            }
        }

        //Selects a random sales pipeline from the sales pipeline database table, and adds it to the sales pipeling list should the player meet the requirements to recieve it
        private void SalesOpportunity()
        {
            int chance = new int();
            chance = rnd.Next(1, 27);
            if (chance > 12)
            {
                chance = rnd.Next(1, 12);
                if (chance < 2)
                {
                    if (hasBEE == true)
                    {
                        salesOpportunities.Add(300000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Thanks to your BEE status, you manage to secure a sales opportunity worth £300,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having BEE status, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 3)
                {
                    if (hasWebsite == true)
                    {
                        salesOpportunities.Add(300000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Thanks to your website, you manage to secure a sales opportunity worth £300,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having BEE status, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 4)
                {
                    if ((hasWebsite == true) && (hasBEE == true))
                    {
                        salesOpportunities.Add(300000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Thanks to your BEE status and website, you manage to secure a sales opportunity worth £300,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having both BEE status and website, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 6)
                {
                    if (staff > 0)
                    {
                        salesOpportunities.Add(100000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Thanks to your staff, you manage to secure a sales opportunity worth £100,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having staff, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 7)
                {
                    if (stock > 0)
                    {
                        salesOpportunities.Add(300000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Thanks to your stock, you manage to secure a sales opportunity worth £300,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having stock, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 8)
                {
                    if (staff > 0)
                    {
                        foreach (int i in Enumerable.Range(0, staff))
                        {
                            salesOpportunities.Add(50000);
                        }
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Each of your staff are able to secure a sales opportunity worth £50,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having staff, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 9)
                {
                    if (hasPR == true)
                    {
                        if (staff > 0)
                        {
                            foreach (int i in Enumerable.Range(0, staff))
                            {
                                salesOpportunities.Add(10000);
                            }
                            salesOpportunities = MergeSort(salesOpportunities);
                            writeToFile(salesOpportunities, nameof(salesOpportunities));
                            MessageBox.Show("Each of your staff are able to secure a sales opportunity worth £100,000. ", "Passed or landed on a sales opportunity square!");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to not having staff, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                        }
                    }
                }
                else if (chance < 10)
                {
                    if (hasHealthCare == true)
                    {
                        salesOpportunities.Add(300000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Thanks to your healthcare plan, you manage to secure a sales opportunity worth £300,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having a healthcare plan, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 11)
                {
                    if ((hasBEE == true) && (hasHealthCare == true))
                    {
                        salesOpportunities.Add(500000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Thanks to your BEE status and healthcare plan, you manage to secure a sales opportunity worth £500,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having both a healthcare plan and BEE status, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 12)
                {
                    if (hasPR == true)
                    {
                        salesOpportunities.Add(300000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("Thanks to your PR agreement, you manage to secure a sales opportunity worth £300,000. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having a PR agreement, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
            }
            else
            {
                chance = rnd.Next(1, 16);
                if (chance < 4)
                {
                    salesOpportunities.Add(50000);
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                    MessageBox.Show("You manage to secure a sales opportunity worth £50,000. ", "Passed or landed on a sales opportunity square!");
                }
                else if (chance < 7)
                {
                    salesOpportunities.Add(100000);
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                    MessageBox.Show("You manage to secure a sales opportunity worth £100,000. ", "Passed or landed on a sales opportunity square!");
                }
                else if (chance < 9)
                {
                    UpdateCapital(-5000);
                    salesOpportunities.Add(50000);
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                    MessageBox.Show("You manage to secure a sales opportunity worth £50,000 at the cost of £5000. ", "Passed or landed on a sales opportunity square!");
                }
                else if (chance < 11)
                {
                    if (salesOpportunities.Count() > 0)
                    {
                        salesOrders.Add(salesOpportunities.Max());
                        salesOpportunities.Remove(salesOpportunities.Max());
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        salesOrders.Sort();
                    }
                    salesOpportunities.Add(50000);
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                    MessageBox.Show("You manage to secure a sales opportunity worth £50,000, and convert your best sales opportunity into an order. ", "Passed or landed on a sales opportunity square!");
                }
                else if (chance < 12)
                {
                    hasPR = false;
                    salesOpportunities.Add(200000);
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                    MessageBox.Show("You manage to secure a sales opportunity worth £200,000 at the cost of any PR agreement you might have. ", "Passed or landed on a sales opportunity square!");
                }
                else if (chance < 13)
                {
                    hasWebsite = false;
                    salesOpportunities.Add(200000);
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                    MessageBox.Show("You manage to secure a sales opportunity worth £200,000 at the cost of any website agreement you might have. ", "Passed or landed on a sales opportunity square!");
                }
                else if (chance < 14)
                {
                    hasMarketing = false;
                    salesOpportunities.Add(200000);
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                    MessageBox.Show("You manage to secure a sales opportunity worth £200,000 at the cost of any marketing agreement you might have. ", "Passed or landed on a sales opportunity square!");
                }
                else if (chance < 15)
                {
                    if (staff > 0)
                    {
                        foreach (int i in Enumerable.Range(0, staff))
                        {
                            UpdateCapital(-10000);
                        }
                        salesOpportunities.Add(50000);
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                        MessageBox.Show("You are able to secure a sales opportunity worth £50,000, at the cost of £10,000 for each of your staff. ", "Passed or landed on a sales opportunity square!");
                    }
                    else
                    {
                        MessageBox.Show("Sadly, due to not having staff, you are unable to secure a sales opportunity. ", "Passed or landed on a sales opportunity square!");
                    }
                }
                else if (chance < 16)
                {
                    UpdateCapital(-20000);
                    salesOpportunities.Add(300000);
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                    MessageBox.Show("You manage to secure a sales opportunity worth £300,000 at the cost of £20000. ", "Passed or landed on a sales opportunity square!");
                }
            }
        }

        //Selects a random cash flow card from the Cash Flow database table and alters the player's capital to carry out the selected card
        public void CashFlow()
        {
            int cardNumber = new int();
            cardNumber = rnd.Next(0, cashFlowTable.Rows.Count);
            if (float.Parse(string.Concat(cashFlowTable.Rows[cardNumber][2])) != 0)
            {
                UpdateCapital(float.Parse(string.Concat(cashFlowTable.Rows[cardNumber][2])));
            }
            else if (float.Parse(string.Concat(cashFlowTable.Rows[cardNumber][2])) == 0)
            {
                shareCapital = shareCapital * float.Parse(string.Concat(cashFlowTable.Rows[cardNumber][3]));
                CapitalDisplay.Text = "£" + string.Concat(shareCapital);
            }
            MessageBox.Show(string.Concat(cashFlowTable.Rows[cardNumber][1]), "Cash Flow!");
        }

        //Selects a random Lucky Break card from the Lucky Break database table and carries it out if the company meets the requirements
        public void LuckyBreak()
        {
            bool eligible = true;
            string notEligibleReason = null;
            int cardNumber = new int();
            cardNumber = rnd.Next(0, luckyBreakTable.Rows.Count);
            string moralDilemma = string.Concat(luckyBreakTable.Rows[cardNumber]["MoralDilemma"]);

            MessageBox.Show(string.Concat(luckyBreakTable.Rows[cardNumber][1]), "Lucky Break!");

            if ((companyType == "Sole Trader") && (string.Concat(luckyBreakTable.Rows[cardNumber][1]).Contains("VAT") == true))
            {
                eligible = false;
                notEligibleReason = "SoleTraderVAT";
            }

            if (moralDilemma == "Yes")
            {
                DialogResult answer;
                answer = MessageBox.Show("Do you take the deal? ", "Take the deal? ", MessageBoxButtons.YesNo);

                if (answer == DialogResult.Yes)
                {

                    int dilemmaNumber = rnd.Next(0, moralDilemmasTable.Rows.Count);
                    MessageBox.Show(string.Concat(moralDilemmasTable.Rows[dilemmaNumber]["Description"]), "Moral Dilemma!");
                    if (string.Concat(moralDilemmasTable.Rows[dilemmaNumber]["GetDeal"]) == "No")
                    {
                        eligible = false;
                        if (notEligibleReason == null)
                        {
                            notEligibleReason = "DilemmaLostDeal";
                        }       
                    }
                    if (moralDilemmasTable.Rows[dilemmaNumber]["DiceNumber"] != DBNull.Value && moralDilemmasTable.Rows[dilemmaNumber]["Multiplier"] != DBNull.Value)
                    {
                        int diceNumber = int.Parse(string.Concat(moralDilemmasTable.Rows[dilemmaNumber]["DiceNumber"]));
                        int multiplier = int.Parse(string.Concat(moralDilemmasTable.Rows[dilemmaNumber]["Multiplier"]));
                        UpdateCapital(-(rnd.Next(diceNumber, (diceNumber * 6) + 1) * multiplier));
                    }
                    if (moralDilemmasTable.Rows[dilemmaNumber]["StaffChange"] != DBNull.Value && staff > 0)
                    {
                        staff = staff + int.Parse(string.Concat(moralDilemmasTable.Rows[dilemmaNumber]["StaffChange"]));
                        StaffDisplay.Text = string.Concat(staff);
                    }
                    if (string.Concat(moralDilemmasTable.Rows[dilemmaNumber]["LoseBEE"]) == "Yes")
                    {
                        hasBEE = false;
                    }
                    if (string.Concat(moralDilemmasTable.Rows[dilemmaNumber]["LoseMarketing"]) == "Yes")
                    {
                        hasMarketing = false;
                    }
                    if (string.Concat(moralDilemmasTable.Rows[dilemmaNumber]["LosePR"]) == "Yes")
                    {
                        hasPR = false;
                    }
                }
                else
                {
                    eligible = false;
                    if (notEligibleReason == null)
                    {
                        notEligibleReason = "DilemmaRejected";
                    }  
                }
            }

            if (companyType == "Sole Trader" && string.Concat(luckyBreakTable.Rows[cardNumber]["ExcludeSoleTrader"]) == "Yes")
            {
                eligible = false;
                if (notEligibleReason == null)
                {
                    notEligibleReason = "Sole Trader";
                }
            }
            else if (hasHealthCare == false && string.Concat(luckyBreakTable.Rows[cardNumber]["NeedsHealthcare"]) == "Yes")
            {
                eligible = false;
                if (notEligibleReason == null)
                {
                    notEligibleReason = "Healthcare";
                }
            }
            else if (hasBEE == false && string.Concat(luckyBreakTable.Rows[cardNumber]["NeedsBEE"]) == "Yes")
            {
                eligible = false;
                if (notEligibleReason == null)
                {
                    notEligibleReason = "BEE";
                }
            }
            //Check for sales training when implemented

            if (eligible == true)
            {
                if (float.Parse(string.Concat(luckyBreakTable.Rows[cardNumber]["CapitalChange"])) != 0)
                {
                    UpdateCapital(float.Parse(string.Concat(luckyBreakTable.Rows[cardNumber]["CapitalChange"])));
                }
                if (luckyBreakTable.Rows[cardNumber]["StockChange"] != DBNull.Value)
                {
                    stock += int.Parse(string.Concat(luckyBreakTable.Rows[cardNumber]["StockChange"]));
                    StockDisplay.Text = string.Concat(stock);
                }
                if (luckyBreakTable.Rows[cardNumber]["SalesOrder"] != DBNull.Value)
                {
                    salesOrders.Add(int.Parse(string.Concat(luckyBreakTable.Rows[cardNumber]["SalesOrder"])));
                }
                if (luckyBreakTable.Rows[cardNumber]["SalesPipeline"] != DBNull.Value)
                {
                    salesOpportunities.Add(int.Parse(string.Concat(luckyBreakTable.Rows[cardNumber]["SalesPipeline"])));
                    salesOpportunities = MergeSort(salesOpportunities);
                    writeToFile(salesOpportunities, nameof(salesOpportunities));
                }
                if (string.Concat(luckyBreakTable.Rows[cardNumber]["LoseBEE"]) == "Yes")
                {
                    hasBEE = false;
                    //Change BEE display
                }
                if (string.Concat(luckyBreakTable.Rows[cardNumber]["LoseMarketing"]) == "Yes")
                {
                    hasMarketing = false;
                    //Change marketing display
                }
                if (luckyBreakTable.Rows[cardNumber]["StaffChange"] != DBNull.Value)
                {
                    if (companyType == "Sole Trader")
                    {
                        MessageBox.Show("Due to being a sole trader, you weren't able to recruit any staff. ", "Recruitment failed!");
                    }
                    else if (companyType == "Partnership" && staff > 0)
                    {
                        MessageBox.Show("Due to being in a partnership, you were unable to recruit any more staff. ", "Recruitment failed!");
                    }
                    else if (companyType == "Limited" && staff > 2)
                    {
                        MessageBox.Show("Recruiting any more staff would take you over your staff limit. ", "Recruitment failed!");
                    }
                    else
                    {
                        staff += int.Parse(string.Concat(luckyBreakTable.Rows[cardNumber]["StaffChange"]));
                        StaffDisplay.Text = string.Concat(staff);
                    }
                }
                if (luckyBreakTable.Rows[cardNumber]["CloseBestPipeline"] != DBNull.Value)
                {
                    if (salesOpportunities.Count > 0)
                    {
                        UpdateCapital(salesOpportunities.Max());
                        salesOpportunities.Remove(salesOpportunities.Max());
                        salesOpportunities = MergeSort(salesOpportunities);
                        writeToFile(salesOpportunities, nameof(salesOpportunities));
                    }
                    else
                    {
                        MessageBox.Show("You have no sales opportunities, making this lucky break ineffectual. ", "No sales opportunities");
                    }
                }
                if (luckyBreakTable.Rows[cardNumber]["MoveMonthEnd"] != DBNull.Value)
                {
                    if (newBoardPosition >= 2 && newBoardPosition <= 8)
                    {
                        SalesOpportunity();
                        SalesOpportunity();
                        SalesOpportunity();
                    }
                    else if (newBoardPosition >= 9 && newBoardPosition <= 17)
                    {
                        SalesOpportunity();
                        SalesOpportunity();
                    }
                    else if (newBoardPosition >= 18 && newBoardPosition <= 26)
                    {
                        SalesOpportunity();
                    }
                    newBoardPosition = 36;
                    string squareNamevar = "";
                    string squareDescvar = "";
                    squareNamevar = string.Concat(squaresTable.Rows[newBoardPosition - 1][1]);
                    this.SquareNameDisplay.Text = squareNamevar;
                    squareDescvar = string.Concat(squaresTable.Rows[newBoardPosition - 1][2]);
                    this.DescriptionDisplay.Text = squareDescvar;
                    SquareDisplay.ImageLocation = ("Board Images\\" + newBoardPosition + ".JPG");
                    SquareDisplay.SizeMode = PictureBoxSizeMode.Zoom;
                    EndOfMonth monthlyReport = new EndOfMonth();
                    monthlyReport.ShowDialog(this);
                    CapitalDisplay.Text = string.Concat(shareCapital);
                    foreach (int i in Enumerable.Range(0, staff + 1))
                    {
                        SalesOpportunity();
                    }

                }
                
            }
            else if (eligible == false)
            {
                if (notEligibleReason == "DilemmaRejected")
                {
                    MessageBox.Show("You did not get the lucky break as you elected not to go through with the deal. ", "Lucky Break!");
                }
                if (notEligibleReason == "Sole Trader")
                {
                    MessageBox.Show("Your lucky break fell through due to being a Sole Trader. ", "Lucky Break!");
                }
                if (notEligibleReason == "Healthcare")
                {
                    MessageBox.Show("Your lucky break fell through due to not having a healthcare plan. ", "Lucky Break!");
                }
                if (notEligibleReason == "BEE")
                {
                    MessageBox.Show("Your lucky break fell through due to not having BEE accreditation. ", "Lucky Break!");
                }
                if (notEligibleReason == "SoleTraderVAT")
                {
                    MessageBox.Show("Your lucky break fell through due to VAT not applying to Sole Traders. ", "Lucky Break!");
                }
                //Sales training reason when implemented
            }
            
        }

        //Alters the player's capital by the value passed in, and updates the capital display
        public void UpdateCapital(float amount)
        {
            shareCapital = shareCapital + amount;
            CapitalDisplay.Text = "£" + string.Concat(shareCapital);

            if (shareCapital < 0)
            {
                FinishGame();
            }
        }

        //Creates a new instance of the FinishPage class and, when that is closed, exits the program
        private void FinishGame()
        {
            SaveGame();

            FinishPage newFinishPage = new FinishPage();
            newFinishPage.ShowDialog();

            this.Hide();
            this.Close();
            Application.Exit();
        }

        //Checks whether the player really wants to finish the game, and if they do, calls the FinishGame subroutine
        private void FinishGameButton_Click(object sender, EventArgs e)
        {
            DialogResult answer;
            answer = MessageBox.Show("Are you sure you want to finish this game? ", "Are you sure? ", MessageBoxButtons.YesNo);

            if (answer == DialogResult.Yes)
            {
                FinishGame();
            }
        }

        //Advances the timer and checks whether the player has run out of time. If they have, it finishes the game
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (regFeesPaid == true)
            {
                timeLimit = timeLimit - 1;

                int minutesRemaining = (int)(Math.Floor(timeLimit / 60.0));
                int secondsRemaining = (int)(Math.Floor(timeLimit - (minutesRemaining * 60.0)));

                TimerMinutesDisplay.Text = string.Concat(minutesRemaining) + " minutes";
                TimerSecondsDisplay.Text = string.Concat(secondsRemaining) + " seconds";

                if (salesOpportunities.Count == 0)
                {
                    ViewSalesOpportunities.Enabled = false;
                }
                else
                {
                    ViewSalesOpportunities.Enabled = true;
                }

                if (salesOrders.Count == 0)
                {
                    ViewSalesOrders.Enabled = false;
                }
                else
                {
                    ViewSalesOrders.Enabled = true;
                }

                if ((hasBEE == true) || (hasHealthCare == true) || (hasInsurance == true) || (hasMarketing == true) || (hasPR == true) || (hasWebsite == true))
                {
                    AgreementsButton.Enabled = true;
                }
                else
                {
                    AgreementsButton.Enabled = false;
                }

                if (timeLimit <= 0)
                {
                    FinishPage newFinishPage = new FinishPage();
                    this.Hide();
                    newFinishPage.ShowDialog();
                    this.Close();
                }
            }
        }

        //Shows a dialogue box displaying the user's current sales prospects
        private void ViewSalesOpportunities_Click(object sender, EventArgs e)
        {
            string toDisplay = null;
            foreach (var i in salesOpportunities)
            {
                toDisplay = ReadFromFile(nameof(salesOpportunities));
            }
            if (toDisplay == null)
            {
                toDisplay = "You have no sales pipeline. ";
            }
            MessageBox.Show(toDisplay, "Sales Opportunities");
        }

        //Shows a dialogue box displaying the user's current sales orders
        private void ViewSalesOrders_Click(object sender, EventArgs e)
        {
            string toDisplay = null;

            if (salesOrders.Count != 0)
            {
                foreach (int i in salesOrders)
                {
                    toDisplay = (toDisplay + "£" + string.Concat(i) + ", ");
                }
            }
            else
            {
                toDisplay = "You have no sales orders. ";
            }
            
            MessageBox.Show(toDisplay, "Sales Orders");
        }

        //Saves the game
        private void SaveGame()
        {
            DataToBeSaved newSave = new DataToBeSaved();
            newSave.saveCompanyType = companyType;
            newSave.saveCompanyName = companyName;
            newSave.saveShareholders = shareholders;
            newSave.saveNatureOfBusiness = natureOfBusiness;
            newSave.saveShareCapital = shareCapital;
            newSave.saveTimeLimit = timeLimit;
            newSave.saveBoardPosition = boardPosition;
            newSave.saveRegFeesPaid = regFeesPaid;
            newSave.saveBankLoanTaken = bankLoanTaken;
            newSave.saveHasWebsite = hasWebsite;
            newSave.saveHasHealthCare = hasHealthCare;
            newSave.saveHasBEE = hasBEE;
            newSave.saveHasPR = hasPR;
            newSave.saveHasMarketing = hasMarketing;
            newSave.saveSalesOpportunities = salesOpportunities;
            newSave.saveSalesOrders = salesOrders;
            newSave.saveStock = stock;
            newSave.saveStaff = staff;
            newSave.saveHasInsurance = hasInsurance;

            /*
            XmlSerializer xs = new XmlSerializer(typeof(DataToBeSaved));
            System.IO.Directory.CreateDirectory("Saves");
            using (System.IO.FileStream fs = new FileStream("Saves\\" + companyName + "OLDMETHOD.xml", FileMode.Create))
            {
                xs.Serialize(fs, newSave);
            }
            */

            System.IO.Directory.CreateDirectory("Saves");
            using (TextWriter tw = new StreamWriter("Saves\\" + companyName + ".xml"))
            {
                tw.WriteLine("<?xml version=\"1.0\"?>");
                tw.WriteLine("<" + newSave.GetType().Name + " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
                tw.WriteLine("  <" + nameof(newSave.saveCompanyType) + ">" + string.Concat(newSave.saveCompanyType) + "</" + nameof(newSave.saveCompanyType) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveCompanyName) + ">" + string.Concat(newSave.saveCompanyName) + "</" + nameof(newSave.saveCompanyName) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveShareholders) + ">" + string.Concat(newSave.saveShareholders) + "</" + nameof(newSave.saveShareholders) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveNatureOfBusiness) + ">" + string.Concat(newSave.saveNatureOfBusiness) + "</" + nameof(newSave.saveNatureOfBusiness) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveShareCapital) + ">" + string.Concat(newSave.saveShareCapital) + "</" + nameof(newSave.saveShareCapital) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveTimeLimit) + ">" + string.Concat(newSave.saveTimeLimit) + "</" + nameof(newSave.saveTimeLimit) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveBoardPosition) + ">" + string.Concat(newSave.saveBoardPosition) + "</" + nameof(newSave.saveBoardPosition) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveRegFeesPaid) + ">" + string.Concat(newSave.saveRegFeesPaid).ToLower() + "</" + nameof(newSave.saveRegFeesPaid) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveBankLoanTaken) + ">" + string.Concat(newSave.saveBankLoanTaken).ToLower() + "</" + nameof(newSave.saveBankLoanTaken) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveHasWebsite) + ">" + string.Concat(newSave.saveHasWebsite).ToLower() + "</" + nameof(newSave.saveHasWebsite) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveHasHealthCare) + ">" + string.Concat(newSave.saveHasHealthCare).ToLower() + "</" + nameof(newSave.saveHasHealthCare) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveHasBEE) + ">" + string.Concat(newSave.saveHasBEE).ToLower() + "</" + nameof(newSave.saveHasBEE) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveHasPR) + ">" + string.Concat(newSave.saveHasPR).ToLower() + "</" + nameof(newSave.saveHasPR) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveHasMarketing) + ">" + string.Concat(newSave.saveHasMarketing).ToLower() + "</" + nameof(newSave.saveHasMarketing) + ">");
                if (newSave.saveSalesOpportunities.Count != 0)
                {
                    tw.WriteLine("  <" + nameof(newSave.saveSalesOpportunities) + ">");
                    foreach (int i in Enumerable.Range(0, newSave.saveSalesOpportunities.Count))
                    {
                        tw.WriteLine("    <int>" + string.Concat(newSave.saveSalesOpportunities[i]) + "</int>");
                    }
                    tw.WriteLine("  </" + nameof(newSave.saveSalesOpportunities) + ">");
                }
                else
                {
                    tw.WriteLine("  <saveSalesOpportunities />");
                }
                if (newSave.saveSalesOrders.Count != 0)
                {
                    tw.WriteLine("  <" + nameof(newSave.saveSalesOrders) + ">");
                    foreach (int i in Enumerable.Range(0, newSave.saveSalesOrders.Count))
                    {
                        tw.WriteLine("    <int>" + string.Concat(newSave.saveSalesOrders[i]) + "</int>");
                    }
                    tw.WriteLine("  </" + nameof(newSave.saveSalesOrders) + ">");
                }
                else
                {
                    tw.WriteLine("  <saveSalesOrders />");
                }
                tw.WriteLine("  <" + nameof(newSave.saveStock) + ">" + string.Concat(newSave.saveStock) + "</" + nameof(newSave.saveStock) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveStaff) + ">" + string.Concat(newSave.saveStaff) + "</" + nameof(newSave.saveStaff) + ">");
                tw.WriteLine("  <" + nameof(newSave.saveHasInsurance) + ">" + string.Concat(newSave.saveHasInsurance).ToLower() + "</" + nameof(newSave.saveHasInsurance) + ">");
                tw.WriteLine("</" + newSave.GetType().Name + ">");
            }

        }

        //Displays a dialogue box showing all the player's current agreements
        private void AgreementsButton_Click(object sender, EventArgs e)
        {
            string toDisplay = "";

            if (hasBEE == true)
            {
                toDisplay = "BEE accreditation";
            }
            if (hasHealthCare == true)
            {
                if (toDisplay != "")
                {
                    toDisplay += ", ";
                }
                toDisplay += "Healthcare agreement";
            }
            if (hasInsurance == true)
            {
                if (toDisplay != "")
                {
                    toDisplay += ", ";
                }
                toDisplay += "Insurance";
            }
            if (hasMarketing == true)
            {
                if (toDisplay != "")
                {
                    toDisplay += ", ";
                }
                toDisplay += "Marketing agreement";
            }
            if (hasPR == true)
            {
                if (toDisplay != "")
                {
                    toDisplay += ", ";
                }
                toDisplay += "PR agreement";
            }
            if (hasWebsite == true)
            {
                if (toDisplay != "")
                {
                    toDisplay += ", ";
                }
                toDisplay += "Website";
            }

            MessageBox.Show(toDisplay, "Current agreements and holdings");
        }

        //Bubble sort algorithm
        public void BubbleSort(List<int> subject)
        {
            if (subject.Count != 0)
            {
                foreach (int i in Enumerable.Range(0, subject.Count - 1))
                {
                    bool swapped = false;

                    foreach (int j in Enumerable.Range(0, subject.Count - 1))
                    {
                        if (subject[j] > subject[j + 1])
                        {
                            int temp = subject[j];
                            subject[j] = subject[j + 1];
                            subject[j + 1] = temp;
                            swapped = true;
                        }
                    }

                    if (swapped == false)
                    {
                        break;
                    }
                }
            }
        }

        //Part of mergesort algorithm
        public List<int> MergeSort(List<int> subject)
        {
            if (subject.Count != 0)
            {
                if (subject.Count == 1)
                {
                    return subject;
                }

                List<int> firstHalf = new List<int>();
                foreach (int i in Enumerable.Range(0, int.Parse(string.Concat(Math.Floor(Convert.ToDecimal((subject.Count / 2)))))))
                {
                    firstHalf.Add(subject[i]);
                }

                List<int> secondHalf = new List<int>();
                foreach (int i in Enumerable.Range(int.Parse(string.Concat(Math.Floor(Convert.ToDecimal((subject.Count / 2))))), int.Parse(string.Concat(Math.Floor(Convert.ToDecimal((subject.Count / 2)))))))
                {
                    secondHalf.Add(subject[i]);
                }

                firstHalf = MergeSort(firstHalf);
                secondHalf = MergeSort(secondHalf);

                return Merge(firstHalf, secondHalf);
            }
            else
            {
                return subject;
            }
        }

        //Part of mergesort algorithm
        public List<int> Merge(List<int> firstHalf, List<int> secondHalf)
        {
            List<int> sorted = new List<int>();

            while (firstHalf.Count != 0 && secondHalf.Count != 0)
            {
                if (firstHalf[0] > secondHalf[0])
                {
                    sorted.Add(secondHalf[0]);
                    secondHalf.Remove(secondHalf[0]);
                }
                else
                {
                    sorted.Add(firstHalf[0]);
                    firstHalf.Remove(firstHalf[0]);
                }
            }

            while (firstHalf.Count != 0)
            {
                sorted.Add(firstHalf[0]);
                firstHalf.Remove(firstHalf[0]);
            }

            while (secondHalf.Count != 0)
            {
                sorted.Add(secondHalf[0]);
                secondHalf.Remove(secondHalf[0]);
            }

            return sorted;
        }

        //Writes specified list to file
        public void writeToFile(List<int> subject, string name)
        {
            System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\SavedVariables\\");
            using (TextWriter tw = new StreamWriter(System.Environment.CurrentDirectory + "\\SavedVariables\\" + name + ".txt"))
            {
                foreach (int s in subject)
                {
                    tw.WriteLine(s);
                }
            }
        }

        //Reads specified file to list
        public string ReadFromFile(string name)
        {
            try
            {
                string[] lines = File.ReadAllLines(System.Environment.CurrentDirectory + "\\SavedVariables\\" + name + ".txt");
                string toDisplay = null;
                foreach (string line in lines)
                {
                    toDisplay = (toDisplay + "£" + line + ", ");
                }
                return toDisplay;
            }
            catch
            {
                string toDisplay = null;
                return toDisplay;
            }
        }

        //Selects a random penalty card from the Penalty database table and carries out its contents
        public void Penalty()
        {
            int penaltyNumber = new int();
            int toPay = new int();
            bool insurance = false;
            penaltyNumber = rnd.Next(0, penaltyTable.Rows.Count);

            MessageBox.Show(string.Concat(penaltyTable.Rows[penaltyNumber]["Description"]), "Penalty!");
            if(hasInsurance == true && string.Concat(penaltyTable.Rows[penaltyNumber]["Insurance"]) == "Yes")
            {
                MessageBox.Show("Your insurance takes effect! An excess of 20% is payable. ", "Penalty");
                insurance = true;
            }

            if ((penaltyTable.Rows[penaltyNumber]["CashLimited"] != DBNull.Value) && (penaltyTable.Rows[penaltyNumber]["CashPartnership"] != DBNull.Value) && (penaltyTable.Rows[penaltyNumber]["CashSoleTrader"] != DBNull.Value))
            {
                if (companyType == "Limited")
                {
                    toPay = int.Parse(string.Concat(penaltyTable.Rows[penaltyNumber]["CashLimited"]));
                }
                else if (companyType == "Partnership")
                {
                    toPay = int.Parse(string.Concat(penaltyTable.Rows[penaltyNumber]["CashPartnership"]));
                }
                else if (companyType == "Sole Trader")
                {
                    toPay = int.Parse(string.Concat(penaltyTable.Rows[penaltyNumber]["CashSoleTrader"]));
                }
            }

            if (toPay < 0)
            {
                if (penaltyTable.Rows[penaltyNumber]["CashMultiplyEmployees"] != DBNull.Value)
                {
                    if (insurance == true)
                    {
                        UpdateCapital((toPay * staff) / 5);
                    }
                    else
                    {
                        UpdateCapital(toPay * staff);
                    }
                }
                else
                {
                    if (insurance == true)
                    {
                        UpdateCapital(toPay / 5);
                    }
                    else
                    {
                        UpdateCapital(toPay);
                    }
                }
            }

            if (string.Concat(penaltyTable.Rows[penaltyNumber]["LoseBestProspect"]) == "Yes")
            {
                salesOpportunities.Remove(salesOpportunities.Max());
                salesOpportunities = MergeSort(salesOpportunities);
                writeToFile(salesOpportunities, nameof(salesOpportunities));
            }

        }
    }
}
