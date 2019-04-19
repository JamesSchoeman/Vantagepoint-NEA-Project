using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Vantagepoint_NEA_Project
{
    public partial class EndOfMonth : Form
    {

        public string parentType = null;
        private float localShareCapital;
        private int localStaff;
        private string localCompanyType;
        private static List<int> localSalesOpportunities = new List<int>();
        private static List<int> opportunitiesToRemove = new List<int>();
        private static List<int> localSalesOrders = new List<int>();
        public float LastMonthCapital = new float();
        public float moneyRecieved = new float();
        public float moneyPaidOut = new float();
        public int salesOpportunitiesConverted = 0;
        public int salesOpportunitiesLost = 0;
        public int salesOrdersConverted = 0;
        Random rnd = new Random();

        SqlConnection dataConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\SquaresDatabase.mdf;Integrated Security = True");
        SqlDataAdapter dataAdapter = new SqlDataAdapter();

        DataTable tipsTable = new DataTable();
        static SqlCommand tipsCommand = new SqlCommand("select * from TipsTable");

        //Initialises the class and sets all its attributes to the correct state
        public EndOfMonth()
        {
            InitializeComponent();

            CloseButton.Enabled = false;
            SalesOpportunitiesButton.Enabled = false;
            PayVATButton.Enabled = false;
            SalariesButton.Enabled = false;
            
            foreach (var i in Application.OpenForms)
            {
                if (string.Concat(i) == "Vantagepoint_NEA_Project.BoardGame, Text: Board Game")
                {
                    parentType = "NonLoaded";
                    break;
                }
                if (string.Concat(i) == "Vantagepoint_NEA_Project.LoadedBoardGame, Text: Board Game")
                {
                    parentType = "Loaded";
                    break;
                }
            }

            if (parentType == "NonLoaded")
            {
                localShareCapital = BoardGame.shareCapital;
                localStaff = BoardGame.staff;
                localCompanyType = BoardGame.companyType;
                localSalesOpportunities = BoardGame.salesOpportunities;
                localSalesOrders = BoardGame.salesOrders;
                LastMonthCapital = BoardGame.lastMonthCapital;
                moneyPaidOut = BoardGame.moneyPaidOut;
                moneyRecieved = BoardGame.moneyRecieved;
            }
            else if (parentType == "Loaded")
            {
                localShareCapital = LoadedBoardGame.shareCapital;
                localStaff = LoadedBoardGame.staff;
                localCompanyType = LoadedBoardGame.companyType;
                localSalesOpportunities = LoadedBoardGame.salesOpportunities;
                localSalesOrders = LoadedBoardGame.salesOrders;
                LastMonthCapital = LoadedBoardGame.lastMonthCapital;
                moneyPaidOut = LoadedBoardGame.moneyPaidOut;
                moneyRecieved = LoadedBoardGame.moneyRecieved;
            }

            BubbleSort(localSalesOpportunities);
            CapitalDisplay.Text = string.Concat(localShareCapital);
            StaffDisplay.Text = string.Concat(localStaff);
            PayVATButton.Text = ("Pay VAT of -----");
            SalariesButton.Text = "Pay salaries of £" + string.Concat(localStaff * 25000);
            SalesOpportunitiesNumberDisplay.Text = string.Concat(localSalesOpportunities.Count);
            SalesOpportunitiesConvertedDisplay.Text = "0";
            SalesOpportunitiesLostDisplay.Text = "0";
            SalesOrdersNumbersDisplay.Text = string.Concat(localSalesOrders.Count);
            SalesOrdersConvertedDisplay.Text = "0";

            if (localCompanyType == "Sole Trader")
            {
                StaffLimitDisplay.Text = "/0";
            }
            else if (localCompanyType == "Partnership")
            {
                StaffLimitDisplay.Text = "/1";
            }
            else if (localCompanyType == "Limited")
            {
                StaffLimitDisplay.Text = "/3";
            }

            dataAdapter.UpdateCommand = tipsCommand;
            dataAdapter.SelectCommand = tipsCommand;
            tipsCommand.CommandType = CommandType.Text;
            tipsCommand.Connection = dataConnection;
            dataConnection.Open();
            dataAdapter.Fill(tipsTable);
            dataConnection.Close();

            toolTip1.SetToolTip(SalesOrdersButton, string.Concat(tipsTable.Rows[36]["SmallTip"]));
            toolTip1.Active = true;
            toolTip1.InitialDelay = 0;

            toolTip2.SetToolTip(SalesOpportunitiesButton, string.Concat(tipsTable.Rows[37]["SmallTip"]));
            toolTip2.Active = true;
            toolTip2.InitialDelay = 0;

            toolTip1.SetToolTip(SalariesButton, string.Concat(tipsTable.Rows[38]["SmallTip"]));
            toolTip1.Active = true;
            toolTip1.InitialDelay = 0;

            toolTip1.SetToolTip(PayVATButton, string.Concat(tipsTable.Rows[39]["SmallTip"]));
            toolTip1.Active = true;
            toolTip1.InitialDelay = 0;
        }

        //When the Close button is pressed, sets the parents' attributes to their updated values and then closes this form
        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (parentType == "NonLoaded")
            {
                BoardGame.shareCapital = localShareCapital;
                BoardGame.salesOpportunities = localSalesOpportunities;
                BoardGame.salesOrders = localSalesOrders;
                BoardGame.lastMonthCapital = localShareCapital;
                BoardGame.moneyPaidOut = 0;
                BoardGame.moneyRecieved = 0;
            }
            else if (parentType == "Loaded")
            {
                LoadedBoardGame.shareCapital = localShareCapital;
                LoadedBoardGame.salesOpportunities = localSalesOpportunities;
                LoadedBoardGame.salesOrders = localSalesOrders;
                LoadedBoardGame.lastMonthCapital = localShareCapital;
                LoadedBoardGame.moneyPaidOut = 0;
                LoadedBoardGame.moneyRecieved = 0;
            }
            this.Close();
        }

        //Updates the localShareCapital attribute and the text on the CapitalDisplay by whatever value is passed into the subroutine
        public void UpdateCapital(float amount)
        {
            localShareCapital = localShareCapital + amount;
            CapitalDisplay.Text = string.Concat(localShareCapital);

            if (amount > 0)
            {
                moneyRecieved = moneyRecieved + amount;
            }
            else if (amount < 0)
            {
                moneyPaidOut = moneyPaidOut + (amount * -1);
            }
        }

        //Reduces Share Capital by 10%, disables the VAT button and enables the subsequent button
        private void PayVATButton_Click(object sender, EventArgs e)
        {
            UpdateCapital(localShareCapital / -10);

            PayVATButton.Enabled = false;
            CloseButton.Enabled = true;
            CapitalChangeDisplay.Text = string.Concat(localShareCapital - LastMonthCapital);
            MoneyPaidOutDisplay.Text = string.Concat(moneyPaidOut);
            MoneyRecievedDisplay.Text = string.Concat(moneyRecieved);
        }

        //Converts sales orders into capital and then clears the sales orders list
        private void SalesOrdersButton_Click(object sender, EventArgs e)
        {
            foreach (int i in localSalesOrders)
            {
                UpdateCapital(i);
                salesOrdersConverted = salesOrdersConverted + 1;
                SalesOrdersConvertedDisplay.Text = string.Concat(salesOrdersConverted);
                MessageBox.Show("£" + string.Concat(i) + " sales order payout received. ", "Payout recieved");
            }
            localSalesOrders.Clear();
            SalesOrdersButton.Enabled = false;
            SalesOpportunitiesButton.Enabled = true;
        }

        //Shows a dialogue box displaying the user's current sales prospects
        private void ViewSalesOpportunities_Click(object sender, EventArgs e)
        {
            string toDisplay = null;
            foreach (var i in localSalesOpportunities)
            {
                toDisplay = ReadFromFile("salesOpportunities");
            }
            if (toDisplay == null)
            {
                toDisplay = "You have no sales pipeline. ";
            }
            MessageBox.Show(toDisplay, "Sales Opportunities");
        }

        //Shows a dialogue box displaying the user's current sales orders
        private void ViewSalesOrders_Click(object sender, EventArgs e)
        {
            string toDisplay = null;
            foreach (int i in localSalesOrders)
            {
                toDisplay = (toDisplay + "£" + string.Concat(i) + ", ");
            }
            MessageBox.Show(toDisplay, "Sales Orders");
        }

        //Converts sales pipeline to sales orders
        private void SalesOpportunitiesButton_Click(object sender, EventArgs e)
        {
            foreach (int i in localSalesOpportunities)
            {
                int chance = new int();
                chance = rnd.Next(1, 37);
                if (chance < 6)
                {
                    opportunitiesToRemove.Add(i);
                    salesOpportunitiesLost = salesOpportunitiesLost + 1;
                    SalesOpportunitiesLostDisplay.Text = string.Concat(salesOpportunitiesLost);
                    MessageBox.Show("£" + string.Concat(i) + " sales opportunity lost. ", "Sales opportunity lost");
                }
                else if (chance < 12)
                {
                    MessageBox.Show("£" + string.Concat(i) + " sales opportunity has failed to close, but has not been lost. ", "Sales opportunity failed to close");
                }
                else if (chance < 36)
                {
                    localSalesOrders.Add(i);
                    opportunitiesToRemove.Add(i);
                    salesOpportunitiesConverted = salesOpportunitiesConverted + 1;
                    SalesOpportunitiesConvertedDisplay.Text = string.Concat(salesOpportunitiesConverted);
                    MessageBox.Show("£" + string.Concat(i) + " sales opportunity successfully converted to sales order. ", "Sales opportunity closed");
                }
                else if (chance == 36)
                {
                    salesOpportunitiesLost = localSalesOpportunities.Count - salesOpportunitiesConverted;
                    SalesOpportunitiesLostDisplay.Text = string.Concat(salesOpportunitiesLost);
                    localSalesOpportunities.Clear();
                    MessageBox.Show("All sales opportunities lost due to unforeseen complications.", "All sales opportunities lost");
                    break;
                }
            }
            foreach (int i in opportunitiesToRemove)
            {
                localSalesOpportunities.Remove(i);
            }
            BubbleSort(localSalesOpportunities);
            opportunitiesToRemove.Clear();
            BubbleSort(localSalesOrders);

            if ((localStaff == 0) && (localCompanyType != "Sole Trader"))
            {
                PayVATButton.Text = ("Pay VAT of " + string.Concat(localShareCapital / 10));
                PayVATButton.Enabled = true;
            }
            else if (localStaff > 0)
            {
                SalariesButton.Enabled = true;
            }
            else if (localCompanyType == "Sole Trader")
            {
                CloseButton.Enabled = true;
                CapitalChangeDisplay.Text = string.Concat(localShareCapital - LastMonthCapital);
                MoneyPaidOutDisplay.Text = string.Concat(moneyPaidOut);
                MoneyRecievedDisplay.Text = string.Concat(moneyRecieved);
            }
            SalesOpportunitiesButton.Enabled = false;

            writeToFile(localSalesOpportunities, "salesOpportunities");
        }

        //Pays a salary of 25000 to each of the user's staff members
        private void SalariesButton_Click(object sender, EventArgs e)
        {
            UpdateCapital(localStaff * -25000);
            PayVATButton.Text = ("Pay VAT of " + string.Concat(localShareCapital / 10));
            PayVATButton.Enabled = true;
            SalariesButton.Enabled = false;
        }

        //Bubble sort subroutine
        public void BubbleSort(List<int> subject)
        {
            if (subject.Count != 0)
            {
                foreach (int i in Enumerable.Range(0, subject.Count - 1))
                {
                    bool swapped = false;

                    foreach (int j in Enumerable.Range(0, subject.Count - 1))
                    {
                        if (subject[j] > subject[j + 1])
                        {
                            int temp = subject[j];
                            subject[j] = subject[j + 1];
                            subject[j + 1] = temp;
                            swapped = true;
                        }
                    }

                    if (swapped == false)
                    {
                        break;
                    }
                }
            }
        }

        //Writes the specified list to a file
        public void writeToFile(List<int> subject, string name)
        {
            System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\SavedVariables\\");
            using (TextWriter tw = new StreamWriter(System.Environment.CurrentDirectory + "\\SavedVariables\\" + name + ".txt"))
            {
                foreach (int s in subject)
                {
                    tw.WriteLine(s);
                }
            }
        }

        //Reads the specified list from a file
        public string ReadFromFile(string name)
        {
            try
            {
                string[] lines = File.ReadAllLines(System.Environment.CurrentDirectory + "\\SavedVariables\\" + name + ".txt");
                string toDisplay = null;
                foreach (string line in lines)
                {
                    toDisplay = (toDisplay + "£" + line + ", ");
                }
                return toDisplay;
            }
            catch
            {
                string toDisplay = null;
                return toDisplay;
            }
        }
    }
}
