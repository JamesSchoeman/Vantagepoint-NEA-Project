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

        private void Board_Game_Load(object sender, EventArgs e)
        {
            this.CNDisplay.Text = companyName;
            this.SHDisplay.Text = shareholders;
            this.NOBDisplay.Text = natureOfBusiness;
            this.CTDisplay.Text = companyType;
        }

    }
}
