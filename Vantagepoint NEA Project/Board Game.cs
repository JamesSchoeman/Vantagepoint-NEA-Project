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

namespace Vantagepoint_NEA_Project
{
    public partial class Board_Game : Form
    {
        public Board_Game()
        {
            InitializeComponent();
        }

        public string companyType = CreateCompany.companyType;
        public string companyName = CreateCompany.companyName;
        public string shareholders = CreateCompany.shareholders;
        public string natureOfBusiness = CreateCompany.natureOfBusiness;
        public int shareCapital = CreateCompany.shareCapital;
        public int diceRollResult;

        private void Board_Game_Load(object sender, EventArgs e)
        {
            this.CNDisplay.Text = companyName;
            this.SHDisplay.Text = shareholders;
            this.NOBDisplay.Text = natureOfBusiness;
            this.CTDisplay.Text = companyType;
        }

        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            diceRollResult = rnd.Next(1, 7);
            this.RollResultDisplay.Text = string.Concat(diceRollResult);

            

            DataTable data = new DataTable();
            SqlConnection dataconn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\SquaresDatabase.mdf;Integrated Security = True");
            SqlCommand commandTest = new SqlCommand("select * from Squares");
            commandTest.CommandType = CommandType.Text;
            commandTest.Connection = dataconn;
            SqlDataAdapter adapter = new SqlDataAdapter(commandTest);
            dataconn.Open();
            adapter.Fill(data);
            dataconn.Close();
            string squareNamevar = "";
            string squareDescvar = "";
            squareNamevar = string.Concat(data.Rows[diceRollResult - 1][1]);
            this.SquareNameDisplay.Text = squareNamevar;
            squareDescvar = string.Concat(data.Rows[diceRollResult - 1][2]);
            this.DescriptionDisplay.Text = squareDescvar;
        }
    }
}
