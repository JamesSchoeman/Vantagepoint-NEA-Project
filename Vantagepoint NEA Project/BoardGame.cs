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
        }

        public static string companyType = CreateCompany.companyType;
        public static string companyName = CreateCompany.companyName;
        public static string shareholders = CreateCompany.shareholders;
        public static string natureOfBusiness = CreateCompany.natureOfBusiness;
        public static int shareCapital = CreateCompany.shareCapital;
        public static int timeLimit = (CreateCompany.timeLimit * 60);
        public static int diceRollResult;
        public static int boardPosition = 1;
        public bool regFeesPaid = false;
        public bool bankLoanTaken = false;
        public Button TakeLoanButton;

        DataTable data = new DataTable();
        SqlConnection dataconn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\SquaresDatabase.mdf;Integrated Security = True");
        static SqlCommand commandTest = new SqlCommand("select * from Squares");
        SqlDataAdapter adapter = new SqlDataAdapter(commandTest);

        Random rnd = new Random();

        private void Board_Game_Load(object sender, EventArgs e)
        {
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
