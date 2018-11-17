namespace Vantagepoint_NEA_Project
{
    partial class EndOfMonth
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.VATLabel = new System.Windows.Forms.Label();
            this.CapitalDisplay = new System.Windows.Forms.Label();
            this.CapitalLabel = new System.Windows.Forms.Label();
            this.StaffLimitDisplay = new System.Windows.Forms.Label();
            this.StaffDisplay = new System.Windows.Forms.Label();
            this.StaffLabel = new System.Windows.Forms.Label();
            this.PayVATButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(701, 392);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(87, 46);
            this.CloseButton.TabIndex = 26;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // VATLabel
            // 
            this.VATLabel.AutoSize = true;
            this.VATLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.VATLabel.Location = new System.Drawing.Point(12, 34);
            this.VATLabel.Name = "VATLabel";
            this.VATLabel.Size = new System.Drawing.Size(270, 20);
            this.VATLabel.TabIndex = 27;
            this.VATLabel.Text = "Pay VAT of 10% of your share capital";
            // 
            // CapitalDisplay
            // 
            this.CapitalDisplay.AutoSize = true;
            this.CapitalDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CapitalDisplay.Location = new System.Drawing.Point(600, 34);
            this.CapitalDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CapitalDisplay.Name = "CapitalDisplay";
            this.CapitalDisplay.Size = new System.Drawing.Size(34, 20);
            this.CapitalDisplay.TabIndex = 34;
            this.CapitalDisplay.Text = "-----";
            // 
            // CapitalLabel
            // 
            this.CapitalLabel.AutoSize = true;
            this.CapitalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CapitalLabel.Location = new System.Drawing.Point(451, 34);
            this.CapitalLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CapitalLabel.Name = "CapitalLabel";
            this.CapitalLabel.Size = new System.Drawing.Size(58, 20);
            this.CapitalLabel.TabIndex = 33;
            this.CapitalLabel.Text = "Capital";
            // 
            // StaffLimitDisplay
            // 
            this.StaffLimitDisplay.AutoSize = true;
            this.StaffLimitDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StaffLimitDisplay.Location = new System.Drawing.Point(677, 69);
            this.StaffLimitDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StaffLimitDisplay.Name = "StaffLimitDisplay";
            this.StaffLimitDisplay.Size = new System.Drawing.Size(34, 20);
            this.StaffLimitDisplay.TabIndex = 46;
            this.StaffLimitDisplay.Text = "-----";
            // 
            // StaffDisplay
            // 
            this.StaffDisplay.AutoSize = true;
            this.StaffDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StaffDisplay.Location = new System.Drawing.Point(600, 69);
            this.StaffDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StaffDisplay.Name = "StaffDisplay";
            this.StaffDisplay.Size = new System.Drawing.Size(34, 20);
            this.StaffDisplay.TabIndex = 45;
            this.StaffDisplay.Text = "-----";
            // 
            // StaffLabel
            // 
            this.StaffLabel.AutoSize = true;
            this.StaffLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StaffLabel.Location = new System.Drawing.Point(451, 69);
            this.StaffLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StaffLabel.Name = "StaffLabel";
            this.StaffLabel.Size = new System.Drawing.Size(44, 20);
            this.StaffLabel.TabIndex = 44;
            this.StaffLabel.Text = "Staff";
            // 
            // PayVATButton
            // 
            this.PayVATButton.Location = new System.Drawing.Point(322, 22);
            this.PayVATButton.Name = "PayVATButton";
            this.PayVATButton.Size = new System.Drawing.Size(87, 46);
            this.PayVATButton.TabIndex = 47;
            this.PayVATButton.Text = "Pay VAT of -----";
            this.PayVATButton.UseVisualStyleBackColor = true;
            this.PayVATButton.Click += new System.EventHandler(this.PayVATButton_Click);
            // 
            // EndOfMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PayVATButton);
            this.Controls.Add(this.StaffLimitDisplay);
            this.Controls.Add(this.StaffDisplay);
            this.Controls.Add(this.StaffLabel);
            this.Controls.Add(this.CapitalDisplay);
            this.Controls.Add(this.CapitalLabel);
            this.Controls.Add(this.VATLabel);
            this.Controls.Add(this.CloseButton);
            this.Name = "EndOfMonth";
            this.Text = "EndOfMonth";
            this.Load += new System.EventHandler(this.EndOfMonth_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label VATLabel;
        private System.Windows.Forms.Label CapitalDisplay;
        private System.Windows.Forms.Label CapitalLabel;
        private System.Windows.Forms.Label StaffLimitDisplay;
        private System.Windows.Forms.Label StaffDisplay;
        private System.Windows.Forms.Label StaffLabel;
        private System.Windows.Forms.Button PayVATButton;
    }
}