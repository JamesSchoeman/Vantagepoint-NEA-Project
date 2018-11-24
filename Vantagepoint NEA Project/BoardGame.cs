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
        public BoardGame()
        {
            InitializeComponent();

            this.CNDisplay.Text = companyName;
            this.SHDisplay.Text = shareholders;
            this.NOBDisplay.Text = natureOfBusiness;
            this.CTDisplay.Text = companyType;
            this.CapitalDisplay.Text = string.Concat(shareCapital);
            this.StockDisplay.Text = string.Concat(stock);
            this.StaffDisplay.Text = string.Concat(staff);
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

            commandTest.CommandType = CommandType.Text;
            commandTest.Connection = dataconn;
            dataconn.Open();
            adapter.Fill(data);
            dataconn.Close();

            string squareNamevar = "";
            string squareDescvar = "";
            squareNamevar = string.Concat(data.Rows[boardPosition - 1][1]);
            this.SquareNameDisplay.Text = squareNamevar;
            squareDescvar = string.Concat(data.Rows[boardPosition - 1][2]);
            this.DescriptionDisplay.Text = squareDescvar;

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
        public static int shareCapital = CreateCompany.shareCapital;
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
        public static bool hasPension = false;
        public static bool hasPR = false;
        public static bool hasMarketing = false;
        public static List<int> salesOpportunities = new List<int>();
        public static int stock = 0;
        public static int staff = 0;
        public static bool reportCapitalChanged = false;

        DataTable data = new DataTable();
        SqlConnection dataconn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\SquaresDatabase.mdf;Integrated Security = True");
        static SqlCommand commandTest = new SqlCommand("select * from Squares");
        SqlDataAdapter adapter = new SqlDataAdapter(commandTest);

        Random rnd = new Random();

        private void Board_Game_Load(object sender, EventArgs e)
        {
            
        }

        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            diceRollResult = rnd.Next(1, 7);
            this.RollResultDisplay.Text = string.Concat(diceRollResult);

            newBoardPosition = boardPosition + diceRollResult;
            if (newBoardPosition > 36)
            {
                newBoardPosition = newBoardPosition - 36;
                EndOfMonth monthlyReport = new EndOfMonth();
                monthlyReport.Show();
                
            }

            commandTest.CommandType = CommandType.Text;
            commandTest.Connection = dataconn;
            dataconn.Open();
            adapter.Fill(data);
            dataconn.Close();

            string squareNamevar = "";
            string squareDescvar = "";
            squareNamevar = string.Concat(data.Rows[newBoardPosition - 1][1]);
            this.SquareNameDisplay.Text = squareNamevar;
            squareDescvar = string.Concat(data.Rows[newBoardPosition - 1][2]);
            this.DescriptionDisplay.Text = squareDescvar;
            SquareDisplay.ImageLocation = ("Board Images\\" + newBoardPosition + ".JPG");
            SquareDisplay.SizeMode = PictureBoxSizeMode.Zoom;

            if (newBoardPosition == 2)
            {
                Square2();
            }
            else if (newBoardPosition == 6)
            {
                Square6();
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
            else if (newBoardPosition == 12)
            {
                Square12();
            }
            else if (newBoardPosition == 14)
            {
                StaffRecruitment();
            }
            else if (newBoardPosition == 16)
            {
                Square16();
            }
            else if (newBoardPosition == 18)
            {
                SalesOpportunity();
            }
            else if (newBoardPosition == 19)
            {
                Square19();
            }
            else if (newBoardPosition == 21)
            {
                Square21();
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
            else if (newBoardPosition == 30)
            {
                Square30();
            }
            else if (newBoardPosition == 35)
            {
                Square35();
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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult answer;
            answer = MessageBox.Show("Are you sure you want to quit? ", "Are you sure? ", MessageBoxButtons.YesNo);

            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public class DataToBeSaved
        {
            public string saveCompanyType;
            public string saveCompanyName;
            public string saveShareholders;
            public string saveNatureOfBusiness;
            public int saveShareCapital;
            public int saveTimeLimit;
            public int saveBoardPosition;
            public bool saveRegFeesPaid;
            public bool saveBankLoanTaken;
            public bool saveHasWebsite;
            public bool saveHasHealthCare;
            public bool saveHasPension;
            public bool saveHasPR;
            public bool saveHasMarketing;
            public List<int> saveSalesOpportunities;
            public int saveStock;
            public int saveStaff;
        }

        private void button2_Click(object sender, EventArgs e)
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
            newSave.saveHasPension = hasPension;
            newSave.saveHasPR = hasPR;
            newSave.saveHasMarketing = hasMarketing;
            newSave.saveSalesOpportunities = salesOpportunities;
            newSave.saveStock = stock;
            newSave.saveStaff = staff;

            XmlSerializer xs = new XmlSerializer(typeof(DataToBeSaved));
            using (System.IO.FileStream fs = new FileStream("savegame.xml", FileMode.Create))
            {
                xs.Serialize(fs, newSave);
            }
        }

        private void Square1()
        {
            if (regFeesPaid == false)
            {
                if (companyType == "Sole Trader")
                {
                    shareCapital = shareCapital - 5000;
                }
                else if (companyType == "Partnership")
                {
                    shareCapital = shareCapital - 10000;
                }
                else if (companyType == "Limited")
                {
                    shareCapital = shareCapital - 20000;
                }
                CapitalDisplay.Text = string.Concat(shareCapital);
                regFeesPaid = true;
            }
        }

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
            }
        }

        void TakeLoan_Click(object sender, EventArgs e)
        {
            shareCapital = shareCapital + 250000;
            CapitalDisplay.Text = string.Concat(shareCapital);
            bankLoanTaken = true;
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
            DoNotTakeLoanButton.Enabled = false;
            DoNotTakeLoanButton.Visible = false;
        }

        void DoNotTakeLoan_Click(object sender, EventArgs e)
        {
            RollDiceButton.Enabled = true;
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            TakeLoanButton.Visible = false;
            TakeLoanButton.Enabled = false;
        }

        private void Square6()
        {
            Button PayForEquipment = new Button();
            PayForEquipment.Location = new System.Drawing.Point(422, 392);
            PayForEquipment.Size = new System.Drawing.Size(87, 46);
            PayForEquipment.Text = "Pay £15000";
            this.Controls.Add(PayForEquipment);
            PayForEquipment.Click += PayForEquipment_Click;
            RollDiceButton.Enabled = false;
        }

        void PayForEquipment_Click(object sender, EventArgs e)
        {
            shareCapital = shareCapital - 15000;
            CapitalDisplay.Text = string.Concat(shareCapital);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
        }

        private void Square10()
        {
            Button PayForWebsite = new Button();
            PayForWebsite.Location = new System.Drawing.Point(422, 392);
            PayForWebsite.Size = new System.Drawing.Size(87, 46);               
            this.Controls.Add(PayForWebsite);
            PayForWebsite.Click += PayForWebsite_Click;
            RollDiceButton.Enabled = false;
            if (hasWebsite == false)
            {
                PayForWebsite.Text = "Pay £20000";
            }
            else if (hasWebsite == true)
            {
                PayForWebsite.Text = "Renew for £10000";
            } 
        }

        void PayForWebsite_Click(object sender, EventArgs e)
        {
            if (hasWebsite == false)
            {
                shareCapital = shareCapital - 20000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                hasWebsite = true;
            }
            else if (hasWebsite == true)
            {
                shareCapital = shareCapital - 10000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
            }
        }

        private void Square12()
        {
            Button PayForRates = new Button();
            PayForRates.Location = new System.Drawing.Point(422, 392);
            PayForRates.Size = new System.Drawing.Size(87, 46);
            PayForRates.Text = "Pay £15000";
            this.Controls.Add(PayForRates);
            PayForRates.Click += PayForRates_Click;
            RollDiceButton.Enabled = false;
        }

        void PayForRates_Click(object sender, EventArgs e)
        {
            shareCapital = shareCapital - 15000;
            CapitalDisplay.Text = string.Concat(shareCapital);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
        }

        private void Square21()
        {
            Button PayForPremises = new Button();
            PayForPremises.Location = new System.Drawing.Point(422, 392);
            PayForPremises.Size = new System.Drawing.Size(87, 46);
            PayForPremises.Text = "Pay £30000";
            this.Controls.Add(PayForPremises);
            PayForPremises.Click += PayForPremises_Click;
            RollDiceButton.Enabled = false;
        }

        void PayForPremises_Click(object sender, EventArgs e)
        {
            shareCapital = shareCapital - 30000;
            CapitalDisplay.Text = string.Concat(shareCapital);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
        }

        private void Square28()
        {
            Button PayForTelephone = new Button();
            PayForTelephone.Location = new System.Drawing.Point(422, 392);
            PayForTelephone.Size = new System.Drawing.Size(87, 46);
            PayForTelephone.Text = "Pay £20000";
            this.Controls.Add(PayForTelephone);
            PayForTelephone.Click += PayForTelephone_Click;
            RollDiceButton.Enabled = false;
        }

        void PayForTelephone_Click(object sender, EventArgs e)
        {
            shareCapital = shareCapital - 20000;
            CapitalDisplay.Text = string.Concat(shareCapital);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
        }

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
            }
        }

        void PayForHealthCare_Click(object sender, EventArgs e)
        {
            if (hasHealthCare == false)
            {
                shareCapital = shareCapital - 20000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                hasHealthCare = true;
            }
            else if (hasHealthCare == true)
            {
                shareCapital = shareCapital - 10000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
            }
        }

        private void Square30()
        {
            if (companyType != "Sole Trader")
            {
                if (hasPension == false)
                {
                    Button PayForPension = new Button();
                    PayForPension.Location = new System.Drawing.Point(422, 392);
                    PayForPension.Size = new System.Drawing.Size(87, 46);
                    PayForPension.Text = "Pay £30000";
                    this.Controls.Add(PayForPension);
                    PayForPension.Click += PayForPension_Click;
                    RollDiceButton.Enabled = false;
                }
                else if (hasPension == true)
                {
                    Button PayForPension = new Button();
                    PayForPension.Location = new System.Drawing.Point(422, 392);
                    PayForPension.Size = new System.Drawing.Size(87, 46);
                    PayForPension.Text = "Renew for £15000";
                    this.Controls.Add(PayForPension);
                    PayForPension.Click += PayForPension_Click;
                    RollDiceButton.Enabled = false;
                }
            }
        }

        void PayForPension_Click(object sender, EventArgs e)
        {
            if (hasPension == false)
            {
                shareCapital = shareCapital - 30000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                hasPension = true;
            }
            else if (hasPension == true)
            {
                shareCapital = shareCapital - 15000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
            }
        }

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
            }
        }

        void PayForPR_Click(object sender, EventArgs e)
        {
            if (hasPR == false)
            {
                shareCapital = shareCapital - 30000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                hasPR = true;
            }
            else if (hasPR == true)
            {
                shareCapital = shareCapital - 15000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
            }
        }

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
            }
        }

        void PayForMarketing_Click(object sender, EventArgs e)
        {
            if (hasMarketing == false)
            {
                shareCapital = shareCapital - 30000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
                hasMarketing = true;
            }
            else if (hasMarketing == true)
            {
                shareCapital = shareCapital - 15000;
                CapitalDisplay.Text = string.Concat(shareCapital);
                Button btn = sender as Button;
                btn.Enabled = false;
                btn.Visible = false;
                RollDiceButton.Enabled = true;
            }
        }

        void Square16()
        {
            Button BuyStockButton = new Button();
            BuyStockButton.Location = new System.Drawing.Point(422, 392);
            BuyStockButton.Size = new System.Drawing.Size(87, 46);
            BuyStockButton.Text = "Pay £50000";
            this.Controls.Add(BuyStockButton);
            BuyStockButton.Click += BuyStockButton_Click;
            RollDiceButton.Enabled = false;
        }

        void BuyStockButton_Click(object sender, EventArgs e)
        {
            shareCapital = shareCapital - 50000;
            CapitalDisplay.Text = string.Concat(shareCapital);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RollDiceButton.Enabled = true;
            stock = stock + 1;
            StockDisplay.Text = string.Concat(stock);
        }

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
            }
        }

        void RecruitStaff_Click(object sender, EventArgs e)
        {
            shareCapital = shareCapital - 25000;
            CapitalDisplay.Text = string.Concat(shareCapital);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            DoNotRecruitStaffButton.Enabled = false;
            DoNotRecruitStaffButton.Visible = false;
            staff = staff + 1;
            StaffDisplay.Text = string.Concat(staff);
            RollDiceButton.Enabled = true;
        }

        void DoNotRecruitStaff_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Visible = false;
            RecruitStaffButton.Enabled = false;
            RecruitStaffButton.Visible = false;
            RollDiceButton.Enabled = true;
        }

        private void SalesOpportunity()
        {
            int chance = new int();
            chance = rnd.Next(1, 4);
            if ((chance == 1) || (chance == 2))
            {
                chance = rnd.Next(1, 27);
                if (chance > 12)
                {
                    chance = rnd.Next(1, 12);
                    if (chance < 2)
                    {
                        if (hasPension == true)
                        {
                            salesOpportunities.Add(300000);
                            salesOpportunities.Sort();
                            MessageBox.Show("Thanks to your pension plan, you manage to secure a sales opportunity worth £300,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to not having a pension plan, you are unable to secure a sales opportunity. ");
                        }
                    }
                    else if (chance < 3)
                    {
                        if (hasWebsite == true)
                        {
                            salesOpportunities.Add(300000);
                            salesOpportunities.Sort();
                            MessageBox.Show("Thanks to your website, you manage to secure a sales opportunity worth £300,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to not having a pension plan, you are unable to secure a sales opportunity. ");
                        }
                    }
                    else if (chance < 4)
                    {
                        if ((hasWebsite == true) && (hasPension == true))
                        {
                            salesOpportunities.Add(300000);
                            salesOpportunities.Sort();
                            MessageBox.Show("Thanks to your pension plan and website, you manage to secure a sales opportunity worth £300,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to not having both a pension plan and website, you are unable to secure a sales opportunity. ");
                        }
                    }
                    else if (chance < 6)
                    {
                        if (staff > 0)
                        {
                            salesOpportunities.Add(100000);
                            salesOpportunities.Sort();
                            MessageBox.Show("Thanks to your sales staff, you are able to secure a sales opportunity worth £100,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to your lack of sales staff, you are unable to secure a sales opportunity. ");
                        }
                    }
                    else if (chance < 7)
                    {
                        if (stock > 0)
                        {
                            salesOpportunities.Add(300000);
                            salesOpportunities.Sort();
                            MessageBox.Show("Due to your stock, you are able to secure a sales opportunity worth £300,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to your lack of stock, you are unable to secure a sales opportunity. ");
                        }
                    }
                    else if (chance < 8)
                    {
                        if (staff > 0)
                        {
                            foreach (int i in Enumerable.Range(1, staff))
                            {
                                salesOpportunities.Add(50000);
                            }
                            salesOpportunities.Sort();
                            MessageBox.Show("Thanks to your sales staff, you have generated " + string.Concat(staff) + " sales opportunities, each worth £50,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to your lack of staff, you do not manage to generate any sales opportunities. ");
                        }
                    }
                    else if (chance < 9)
                    {
                        if (hasPR == true)
                        {
                            if (staff > 0)
                            {
                                foreach (int i in Enumerable.Range(1, staff))
                                {
                                    salesOpportunities.Add(100000);
                                }
                                salesOpportunities.Sort();
                                MessageBox.Show("Thanks to your sales staff and PR agreements, you have generated " + string.Concat(staff) + " sales opportunities, each worth £100,000. ");
                            }
                            else
                            {
                                MessageBox.Show("Sadly, due to not having both staff and PR agreements, you do not manage to generate any sales opportunities. ");
                            }
                        }
                    }
                    else if (chance < 10)
                    {
                        if (hasHealthCare == true)
                        {
                            salesOpportunities.Add(300000);
                            salesOpportunities.Sort();
                            MessageBox.Show("Thanks to your healthcare plan, you manage to secure a sales opportunity worth £300,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to not having a healthcare plan, you are unable to secure a sales opportunity. ");
                        }
                    }
                    else if (chance < 11)
                    {
                        if ((hasPension == true) && (hasHealthCare == true))
                        {
                            salesOpportunities.Add(500000);
                            salesOpportunities.Sort();
                            MessageBox.Show("Thanks to your pension plan and healthcare plan, you manage to secure a sales opportunity worth £500,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to not having both a healthcare plan and a pension plan, you are unable to secure a sales opportunity. ");
                        }
                    }
                    else if (chance < 12)
                    {
                        if (hasPR == true)
                        {
                            salesOpportunities.Add(300000);
                            salesOpportunities.Sort();
                            MessageBox.Show("Thanks to your PR agreement, you manage to secure a sales opportunity worth £300,000. ");
                        }
                        else
                        {
                            MessageBox.Show("Sadly, due to not having a PR agreement, you are unable to secure a sales opportunity. ");
                        }
                    }
                }
                else
                {
                    chance = rnd.Next(1, 16);
                    if (chance < 4)
                    {
                        salesOpportunities.Add(50000);
                        salesOpportunities.Sort();
                        MessageBox.Show("You manage to secure a sales opportunity worth £50,000. ");
                    }
                    else if (chance < 7)
                    {
                        salesOpportunities.Add(100000);
                        salesOpportunities.Sort();
                        MessageBox.Show("You manage to secure a sales opportunity worth £100,000. ");
                    }
                    else if (chance < 9)
                    {
                        shareCapital = shareCapital - 5000;
                        CapitalDisplay.Text = string.Concat(shareCapital);
                        salesOpportunities.Add(50000);
                        salesOpportunities.Sort();
                        MessageBox.Show("You manage to secure a sales opportunity worth £50,000 at the cost of £5000. ");
                    }
                    else if (chance < 11)
                    {
                        //Change best pipeline into order, collect 50,000 sales pipeline
                        MessageBox.Show("Test");
                    }
                    else if (chance < 12)
                    {
                        hasPR = false;
                        salesOpportunities.Add(200000);
                        salesOpportunities.Sort();
                        MessageBox.Show("You manage to secure a sales opportunity worth £200,000 at the cost of any PR agreement you might have. ");
                    }
                    else if (chance < 13)
                    {
                        hasWebsite = false;
                        salesOpportunities.Add(200000);
                        salesOpportunities.Sort();
                        MessageBox.Show("You manage to secure a sales opportunity worth £200,000 at the cost of any website agreement you might have. ");
                    }
                    else if (chance < 14)
                    {
                        hasMarketing = false;
                        salesOpportunities.Add(200000);
                        salesOpportunities.Sort();
                        MessageBox.Show("You manage to secure a sales opportunity worth £200,000 at the cost of any marketing agreement you might have. ");
                    }
                    else if (chance < 15)
                    {
                        //Pay 10,000 per sales staff (excluding self), collect Sales Training card, collect 50,000 sales pipeline
                        MessageBox.Show("Test");
                    }
                    else if (chance < 16)
                    {
                        //Pay 20,000, collect Sales Training card, collect 300,000 sales pipeline
                        MessageBox.Show("Test");
                    }
                }
            }
            else if (chance == 3)
            {
                chance = rnd.Next(1, 14);
                if (chance < 10)
                {
                    MessageBox.Show("Test");
                }
                else
                {
                    MessageBox.Show("Test");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult answer;
            answer = MessageBox.Show("Are you sure you want to finish this game? ", "Are you sure? ", MessageBoxButtons.YesNo);

            if (answer == DialogResult.Yes)
            {
                EndOfMonth finalMonthlyReport = new EndOfMonth();
                finalMonthlyReport.ShowDialog();

                FinishPage newFinishPage = new FinishPage();
                newFinishPage.ShowDialog();

                this.Hide();
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLimit = timeLimit - 1;

            int minutesRemaining = (int) (Math.Floor(timeLimit / 60.0));
            int secondsRemaining = (int) (Math.Floor(timeLimit - (minutesRemaining * 60.0)));

            TimerMinutesDisplay.Text = string.Concat(minutesRemaining) + "minutes";
            TimerSecondsDisplay.Text = string.Concat(secondsRemaining) + "seconds";

            if (timeLimit == 0)
            {
                FinishPage newFinishPage = new FinishPage();
                this.Hide();
                newFinishPage.ShowDialog();
                this.Close();
            }
        }

        private void ViewSalesOpportunities_Click(object sender, EventArgs e)
        {
            string toDisplay = null;
            foreach (var i in salesOpportunities)
            {
                toDisplay = (toDisplay + "£" + string.Concat(i) + ", ");
            }
            MessageBox.Show(toDisplay);
        }

    }
}
