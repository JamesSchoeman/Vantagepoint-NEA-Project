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

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Vantagepoint_NEA_Project
{
    //The Main Menu class
    public partial class MainMenu : Form
    {
        SqlConnection dataConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\SquaresDatabase.mdf;Integrated Security = True");
        SqlDataAdapter dataAdapter = new SqlDataAdapter();

        DataTable tipsTable = new DataTable();
        static SqlCommand tipsCommand = new SqlCommand("select * from TipsTable");

        //This subroutine initialises the class and sets the image location
        public MainMenu()
        {
            InitializeComponent();
            LogoDisplay.ImageLocation = "Board Images\\VantagePointLogo.JPG";

            dataAdapter.UpdateCommand = tipsCommand;
            dataAdapter.SelectCommand = tipsCommand;
            tipsCommand.CommandType = CommandType.Text;
            tipsCommand.Connection = dataConnection;
            dataConnection.Open();
            dataAdapter.Fill(tipsTable);
            dataConnection.Close();

            toolTip1.SetToolTip(NewGameButton, string.Concat(tipsTable.Rows[41]["SmallTip"]));
            toolTip1.Active = true;
            toolTip1.InitialDelay = 0;

            toolTip2.SetToolTip(LoadGameButton, string.Concat(tipsTable.Rows[42]["SmallTip"]));
            toolTip2.Active = true;
            toolTip2.InitialDelay = 0;

        }

        //This subroutine runs when the Exit button is clicked; it closes the program
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //When the New Game button is clicked, this subroutine is called; an instance of the CreateCompany form is created and then this form closes
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            CreateCompany newCreateCompany = new CreateCompany();
            this.Hide();
            newCreateCompany.ShowDialog();
            this.Close();
        }

        //When the Load Game button is clicked, this subroutine is called; it creates an instance of the LoadedBoardGame form and then closes this form
        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            LoadedBoardGame newLoaded = new LoadedBoardGame();
            this.Hide();
            newLoaded.ShowDialog();
            this.Close();
        }

    }
}
