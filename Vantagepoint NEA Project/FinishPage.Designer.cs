namespace Vantagepoint_NEA_Project
{
    partial class FinishPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PayLoanButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PayLoanButton
            // 
            this.PayLoanButton.Location = new System.Drawing.Point(12, 12);
            this.PayLoanButton.Name = "PayLoanButton";
            this.PayLoanButton.Size = new System.Drawing.Size(87, 46);
            this.PayLoanButton.TabIndex = 0;
            this.PayLoanButton.Text = "Pay back bank loan";
            this.PayLoanButton.UseVisualStyleBackColor = true;
            this.PayLoanButton.Click += new System.EventHandler(this.PayLoanButton_Click);
            // 
            // FinishPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PayLoanButton);
            this.Name = "FinishPage";
            this.Text = "FinishPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PayLoanButton;
    }
}