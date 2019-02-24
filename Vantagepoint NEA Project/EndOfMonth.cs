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
        Random rnd = new Random();

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
            }
            else if (parentType == "Loaded")
            {
                localShareCapital = LoadedBoardGame.shareCapital;
                localStaff = LoadedBoardGame.staff;
                localCompanyType = LoadedBoardGame.companyType;
                localSalesOpportunities = LoadedBoardGame.salesOpportunities;
                localSalesOrders = LoadedBoardGame.salesOrders;
            }

            BubbleSort(localSalesOpportunities);
            CapitalDisplay.Text = string.Concat(localShareCapital);
            StaffDisplay.Text = string.Concat(localStaff);
            PayVATButton.Text = ("Pay VAT of -----");
            SalariesButton.Text = "Pay salaries of £" + string.Concat(localStaff * 25000);

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
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (parentType == "NonLoaded")
            {
                BoardGame.shareCapital = localShareCapital;
                BoardGame.salesOpportunities = localSalesOpportunities;
                BoardGame.salesOrders = localSalesOrders;
            }
            else if (parentType == "Loaded")
            {
                LoadedBoardGame.shareCapital = localShareCapital;
                LoadedBoardGame.salesOpportunities = localSalesOpportunities;
                LoadedBoardGame.salesOrders = localSalesOrders;
            }
            this.Close();
        }

        public void UpdateCapital(float amount)
        {
            localShareCapital = localShareCapital + amount;
            CapitalDisplay.Text = string.Concat(localShareCapital);
        }

        private void PayVATButton_Click(object sender, EventArgs e)
        {
            UpdateCapital(localShareCapital / -10);

            PayVATButton.Enabled = false;
            CloseButton.Enabled = true;
        }

        private void SalesOrdersButton_Click(object sender, EventArgs e)
        {
            foreach (int i in localSalesOrders)
            {
                UpdateCapital(i);
                MessageBox.Show("£" + string.Concat(i) + " sales order payout received. ", "Payout recieved");
            }
            localSalesOrders.Clear();
            SalesOrdersButton.Enabled = false;
            SalesOpportunitiesButton.Enabled = true;
        }

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

        private void ViewSalesOrders_Click(object sender, EventArgs e)
        {
            string toDisplay = null;
            foreach (int i in localSalesOrders)
            {
                toDisplay = (toDisplay + "£" + string.Concat(i) + ", ");
            }
            MessageBox.Show(toDisplay, "Sales Orders");
        }

        private void SalesOpportunitiesButton_Click(object sender, EventArgs e)
        {
            foreach (int i in localSalesOpportunities)
            {
                int chance = new int();
                chance = rnd.Next(1, 37);
                if (chance < 6)
                {
                    opportunitiesToRemove.Add(i);
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
                    MessageBox.Show("£" + string.Concat(i) + " sales opportunity successfully converted to sales order. ", "Sales opportunity closed");
                }
                else if (chance == 36)
                {
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

            if ((localCompanyType == "Sole Trader") || (localStaff == 0))
            {
                PayVATButton.Text = ("Pay VAT of " + string.Concat(localShareCapital / 10));
                PayVATButton.Enabled = true;
            }
            else
            {
                SalariesButton.Enabled = true;
            }
            SalesOpportunitiesButton.Enabled = false;

            writeToFile(localSalesOpportunities, "salesOpportunities");
        }

        private void SalariesButton_Click(object sender, EventArgs e)
        {
            UpdateCapital(localStaff * -25000);
            PayVATButton.Text = ("Pay VAT of " + string.Concat(localShareCapital / 10));
            PayVATButton.Enabled = true;
            SalariesButton.Enabled = false;
        }

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
