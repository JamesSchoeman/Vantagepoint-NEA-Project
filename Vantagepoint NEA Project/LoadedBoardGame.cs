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
    public partial class LoadedBoardGame : Form
    {
        public LoadedBoardGame()
        {
            InitializeComponent();
        }

        public static string filePath;
        
        public static string companyType;
        public static string companyName;
        public static string shareholders;
        public static string natureOfBusiness;
        public static int shareCapital;
        public static int timeLimit;
        public static int diceRollResult;
        public static int boardPosition;
        public bool regFeesPaid = false;
        public bool bankLoanTaken = false;
        public Button TakeLoanButton;
        public bool hasWebsite = false;

        DataTable data = new DataTable();
        SqlConnection dataconn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\SquaresDatabase.mdf;Integrated Security = True");
        static SqlCommand commandTest = new SqlCommand("select * from Squares");
        SqlDataAdapter adapter = new SqlDataAdapter(commandTest);

        Random rnd = new Random();

        private void Board_Game_Load(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            filePath = openFileDialog1.SafeFileName;

            DataToBeSaved loadedSave;
            XmlSerializer xs = new XmlSerializer(typeof(DataToBeSaved));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                loadedSave = xs.Deserialize(fs) as DataToBeSaved;
            }

            if (loadedSave != null)
            {
                companyType = loadedSave.saveCompanyType;
                companyName = loadedSave.saveCompanyName;
                shareholders = loadedSave.saveShareholders;
                natureOfBusiness = loadedSave.saveNatureOfBusiness;
                shareCapital = loadedSave.saveShareCapital;
                timeLimit = loadedSave.saveTimeLimit;
                boardPosition = loadedSave.saveBoardPosition;
                regFeesPaid = loadedSave.saveRegFeesPaid;
                bankLoanTaken = loadedSave.saveBankLoanTaken;
                hasWebsite = loadedSave.saveHasWebsite;
            }

            this.CNDisplay.Text = companyName;
            this.SHDisplay.Text = shareholders;
            this.NOBDisplay.Text = natureOfBusiness;
            this.CTDisplay.Text = companyType;

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

        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            diceRollResult = rnd.Next(1, 7);
            this.RollResultDisplay.Text = string.Concat(diceRollResult);

            boardPosition = boardPosition + diceRollResult;
            if (boardPosition > 36) {
                boardPosition = boardPosition - 36;
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
            squareNamevar = string.Concat(data.Rows[boardPosition - 1][1]);
            this.SquareNameDisplay.Text = squareNamevar;
            squareDescvar = string.Concat(data.Rows[boardPosition - 1][2]);
            this.DescriptionDisplay.Text = squareDescvar;
            
            if (boardPosition == 2)
            {
                Square2();
            }
            else if (boardPosition == 6)
            {
                Square6();
            }
            else if (boardPosition == 10)
            {
                Square10();
            }
            else if (boardPosition == 12)
            {
                Square12();
            }
            else if (boardPosition == 21)
            {
                Square21();
            }
            else if (boardPosition == 28)
            {
                Square28();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult answer;
            answer = MessageBox.Show("Are you sure you want to go back to the menu? ", "Are you sure? ", MessageBoxButtons.YesNo);

            if (answer == DialogResult.Yes)
            {
                MainMenu newMainMenu = new MainMenu();
                this.Hide();
                newMainMenu.ShowDialog();
                this.Close();
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
            if (hasWebsite == false)
            {
                Button PayForWebsite = new Button();
                PayForWebsite.Location = new System.Drawing.Point(422, 392);
                PayForWebsite.Size = new System.Drawing.Size(87, 46);
                PayForWebsite.Text = "Pay £20000";
                this.Controls.Add(PayForWebsite);
                PayForWebsite.Click += PayForWebsite_Click;
                RollDiceButton.Enabled = false;
            }
            else if (hasWebsite == true)
            {
                Button PayForWebsite = new Button();
                PayForWebsite.Location = new System.Drawing.Point(422, 392);
                PayForWebsite.Size = new System.Drawing.Size(87, 46);
                PayForWebsite.Text = "Renew for £10000";
                this.Controls.Add(PayForWebsite);
                PayForWebsite.Click += PayForWebsite_Click;
                RollDiceButton.Enabled = false;
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
    }
}
