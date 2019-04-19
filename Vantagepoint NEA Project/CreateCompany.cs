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
    public partial class CreateCompany : Form
    {

        public static string companyName;
        public static string shareholders;
        public static string natureOfBusiness;
        public static float shareCapital;
        public static string companyType;
        public static int timeLimit;

        SqlConnection dataConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\SquaresDatabase.mdf;Integrated Security = True");
        SqlDataAdapter dataAdapter = new SqlDataAdapter();

        DataTable tipsTable = new DataTable();
        static SqlCommand tipsCommand = new SqlCommand("select * from TipsTable");

        //This sibroutine initialises the class
        public CreateCompany()
        {
            InitializeComponent();
            LogoDisplay.ImageLocation = "Board Images\\VantagePointLogo.JPG";
            CompanyTypeHeaderLabel.Visible = false;
            CompanyTypeLabel.Visible = false;
            CompanyDescriptionLabel.Visible = false;
            FeesLabel.Visible = false;
            SalesStaffLabel.Visible = false;
            ShareCapitalInput.Visible = false;
            CompanyTypeHeaderLabel.Visible = false;
            FeesHeader.Visible = false;
            SalesStaffHeader.Visible = false;

            dataAdapter.UpdateCommand = tipsCommand;
            dataAdapter.SelectCommand = tipsCommand;
            tipsCommand.CommandType = CommandType.Text;
            tipsCommand.Connection = dataConnection;
            dataConnection.Open();
            dataAdapter.Fill(tipsTable);
            dataConnection.Close();

            toolTip1.SetToolTip(comboBox2, string.Concat(tipsTable.Rows[43]["SmallTip"]));
            toolTip1.Active = true;
            toolTip1.InitialDelay = 0;

            toolTip2.SetToolTip(comboBox1, string.Concat(tipsTable.Rows[44]["SmallTip"]));
            toolTip2.Active = true;
            toolTip2.InitialDelay = 0;
        }

        //Called when the Back button is clicked; creates an instance of the MainMenu class and then closes this form
        private void button1_Click(object sender, EventArgs e)
        {
            MainMenu newMainMenu = new MainMenu();
            this.Hide();
            newMainMenu.ShowDialog();
            this.Close();
        }

        //When the user selects a different company type, this subroutine is called to display the relevant company description
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompanyTypeHeaderLabel.Visible = true;
            CompanyTypeLabel.Visible = true;
            CompanyDescriptionLabel.Visible = true;
            FeesLabel.Visible = true;
            SalesStaffLabel.Visible = true;
            ShareCapitalInput.Visible = true;
            CompanyTypeHeaderLabel.Visible = true;
            FeesHeader.Visible = true;
            SalesStaffHeader.Visible = true;

            if (this.comboBox1.Text == "Sole Trader")
            {
                this.CompanyTypeLabel.Text = ("Sole Trader");
                this.FeesLabel.Text = ("£5000");
                this.SalesStaffLabel.Text = ("0");
                this.CompanyDescriptionLabel.Text = ("As a sole trader, you are exempt from BEE requirements, VAT penalties and VAT payments. \nYou cannot recruit more sales staff. Collect a BEE certificate on registration. ");
                this.CompanyDescriptionLabel.Left = (ClientSize.Width / 2) - (CompanyDescriptionLabel.Width / 2);
            }
            else if (this.comboBox1.Text == "Partnership")
            {
                this.CompanyTypeLabel.Text = ("Partnership");
                this.FeesLabel.Text = ("£10,000");
                this.SalesStaffLabel.Text = ("1");
                this.CompanyDescriptionLabel.Text = ("Can recruit 1 sales person. ");
                this.CompanyDescriptionLabel.Left = (ClientSize.Width / 2) - (CompanyDescriptionLabel.Width / 2);
            }
            else if (this.comboBox1.Text == "Limited")
            {
                this.CompanyTypeLabel.Text = ("Limited");
                this.FeesLabel.Text = ("£20,000");
                this.SalesStaffLabel.Text = ("3");
                this.CompanyDescriptionLabel.Text = ("Can recruit up to 3 sales staff. ");
                this.CompanyDescriptionLabel.Left = (ClientSize.Width / 2) - (CompanyDescriptionLabel.Width / 2);
            }
        }

        //When the Register and Pay Fees button is clicked, this subroutine is called; it checks whether all fields are filled, and if so, passes the entered information to a new instance of the BoardGame class and then closes this form. If not, it tells the user to fill all fields. 
        private void GoButton_Click(object sender, EventArgs e)
        {
            if ((this.CompanyNameInput.Text != "") && (this.ShareholdersInput.Text != "") && (this.NatureOfBusinessInput.Text != "") && (this.NatureOfBusinessInput.Text != "") && (this.ShareCapitalInput.Text != "") && (this.comboBox1.Text != "") && (this.comboBox2.Text != ""))
            {
                companyName = CompanyNameInput.Text;
                shareholders = ShareholdersInput.Text;
                natureOfBusiness = NatureOfBusinessInput.Text;
                shareCapital = float.Parse(ShareCapitalInput.Text);
                companyType = comboBox1.Text;

                if (comboBox2.Text != "None")
                {
                    timeLimit = int.Parse(comboBox2.Text);
                }
                else
                {
                    timeLimit = 0;
                }
                
                BoardGame newBoardGame = new BoardGame();
                this.Hide();
                newBoardGame.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill all fields. ", "Error");
            }

        }

    }
}
