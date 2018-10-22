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
    public partial class CreateCompany : Form
    {

        public static string companyName;
        public static string shareholders;
        public static string natureOfBusiness;
        public static int shareCapital;
        public static string companyType;

        public CreateCompany()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainMenu newMainMenu = new MainMenu();
            this.Hide();
            newMainMenu.ShowDialog();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void CompanyTypeDescriptionLabel_Click(object sender, EventArgs e)
        {

        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            if ((this.CompanyNameInput.Text != "") && (this.ShareholdersInput.Text != "") && (this.NatureOfBusinessInput.Text != "") && (this.NatureOfBusinessInput.Text != "") && (this.ShareCapitalInput.Text != "") && (this.comboBox1.Text != ""))
            {
                companyName = CompanyNameInput.Text;
                shareholders = ShareholdersInput.Text;
                natureOfBusiness = NatureOfBusinessInput.Text;
                shareCapital = int.Parse(ShareCapitalInput.Text);
                companyType = comboBox1.Text;

                Board_Game newBoardGame = new Board_Game();
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
