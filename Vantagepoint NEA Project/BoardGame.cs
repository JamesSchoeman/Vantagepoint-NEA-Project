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

            XmlSerializer xs = new XmlSerializer(typeof(DataToBeSaved));
            using (System.IO.FileStream fs = new FileStream("savegame.xml", FileMode.Create))
            {
                xs.Serialize(fs, newSave);
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
    }
}
