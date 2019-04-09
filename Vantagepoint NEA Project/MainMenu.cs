using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Vantagepoint_NEA_Project
{
    //The Main Menu class
    public partial class MainMenu : Form
    {
        //This subroutine initialises the class and sets the image location
        public MainMenu()
        {
            InitializeComponent();
            LogoDisplay.ImageLocation = "Board Images\\VantagePointLogo.JPG";
        }

        //This subroutine runs when the Exit button is clicked; it closes the program
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //When the New Game button is clicked, this subroutine is called; an instance of the CreateCompany form is created and then this form closes
        private void button1_Click_1(object sender, EventArgs e)
        {
            CreateCompany newCreateCompany = new CreateCompany();
            this.Hide();
            newCreateCompany.ShowDialog();
            this.Close();
        }

        //When the Load Game button is clicked, this subroutine is called; it creates an instance of the LoadedBoardGame form and then closes this form
        private void button2_Click(object sender, EventArgs e)
        {
            LoadedBoardGame newLoaded = new LoadedBoardGame();
            this.Hide();
            newLoaded.ShowDialog();
            this.Close();
        }

    }
}
