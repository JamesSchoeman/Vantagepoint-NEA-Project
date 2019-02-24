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
            this.CapitalLabel = new System.Windows.Forms.Label();
            this.CapitalDisplay = new System.Windows.Forms.Label();
            this.PostLoanLabel = new System.Windows.Forms.Label();
            this.PostLoanDisplay = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PayLoanButton
            // 
            this.PayLoanButton.Location = new System.Drawing.Point(368, 12);
            this.PayLoanButton.Name = "PayLoanButton";
            this.PayLoanButton.Size = new System.Drawing.Size(101, 70);
            this.PayLoanButton.TabIndex = 0;
            this.PayLoanButton.Text = "Bank loan repayment - £250000 + 20%";
            this.PayLoanButton.UseVisualStyleBackColor = true;
            this.PayLoanButton.Click += new System.EventHandler(this.PayLoanButton_Click);
            // 
            // CapitalLabel
            // 
            this.CapitalLabel.AutoSize = true;
            this.CapitalLabel.Location = new System.Drawing.Point(12, 29);
            this.CapitalLabel.Name = "CapitalLabel";
            this.CapitalLabel.Size = new System.Drawing.Size(75, 13);
            this.CapitalLabel.TabIndex = 1;
            this.CapitalLabel.Text = "Share capital: ";
            // 
            // CapitalDisplay
            // 
            this.CapitalDisplay.AutoSize = true;
            this.CapitalDisplay.Location = new System.Drawing.Point(205, 29);
            this.CapitalDisplay.Name = "CapitalDisplay";
            this.CapitalDisplay.Size = new System.Drawing.Size(22, 13);
            this.CapitalDisplay.TabIndex = 2;
            this.CapitalDisplay.Text = "-----";
            // 
            // PostLoanLabel
            // 
            this.PostLoanLabel.AutoSize = true;
            this.PostLoanLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostLoanLabel.Location = new System.Drawing.Point(5, 85);
            this.PostLoanLabel.Name = "PostLoanLabel";
            this.PostLoanLabel.Size = new System.Drawing.Size(453, 55);
            this.PostLoanLabel.TabIndex = 3;
            this.PostLoanLabel.Text = "Final balance/score:";
            // 
            // PostLoanDisplay
            // 
            this.PostLoanDisplay.AutoSize = true;
            this.PostLoanDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostLoanDisplay.Location = new System.Drawing.Point(12, 155);
            this.PostLoanDisplay.Name = "PostLoanDisplay";
            this.PostLoanDisplay.Size = new System.Drawing.Size(104, 55);
            this.PostLoanDisplay.TabIndex = 4;
            this.PostLoanDisplay.Text = "-----";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(687, 368);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(101, 70);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Finish";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FinishPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PostLoanDisplay);
            this.Controls.Add(this.PostLoanLabel);
            this.Controls.Add(this.CapitalDisplay);
            this.Controls.Add(this.CapitalLabel);
            this.Controls.Add(this.PayLoanButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FinishPage";
            this.Text = "FinishPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PayLoanButton;
        private System.Windows.Forms.Label CapitalLabel;
        private System.Windows.Forms.Label CapitalDisplay;
        private System.Windows.Forms.Label PostLoanLabel;
        private System.Windows.Forms.Label PostLoanDisplay;
        private System.Windows.Forms.Button CloseButton;
    }
}