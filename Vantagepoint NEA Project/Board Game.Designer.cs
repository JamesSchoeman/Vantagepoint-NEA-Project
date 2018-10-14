namespace Vantagepoint_NEA_Project
{
    partial class Board_Game
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
            this.RollDiceButton = new System.Windows.Forms.Button();
            this.CompanyTypeLabel = new System.Windows.Forms.Label();
            this.NOBLabel = new System.Windows.Forms.Label();
            this.ShareholdersLabel = new System.Windows.Forms.Label();
            this.CompanyNameLabel = new System.Windows.Forms.Label();
            this.CNDisplay = new System.Windows.Forms.Label();
            this.SHDisplay = new System.Windows.Forms.Label();
            this.NOBDisplay = new System.Windows.Forms.Label();
            this.CTDisplay = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DescriptionDisplay = new System.Windows.Forms.Label();
            this.RollResultDisplay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // RollDiceButton
            // 
            this.RollDiceButton.Location = new System.Drawing.Point(12, 12);
            this.RollDiceButton.Name = "RollDiceButton";
            this.RollDiceButton.Size = new System.Drawing.Size(87, 46);
            this.RollDiceButton.TabIndex = 0;
            this.RollDiceButton.Text = "Roll Dice";
            this.RollDiceButton.UseVisualStyleBackColor = true;
            this.RollDiceButton.Click += new System.EventHandler(this.RollDiceButton_Click);
            // 
            // CompanyTypeLabel
            // 
            this.CompanyTypeLabel.AutoSize = true;
            this.CompanyTypeLabel.Font = new System.Drawing.Font("Kelson Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompanyTypeLabel.Location = new System.Drawing.Point(435, 125);
            this.CompanyTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CompanyTypeLabel.Name = "CompanyTypeLabel";
            this.CompanyTypeLabel.Size = new System.Drawing.Size(112, 19);
            this.CompanyTypeLabel.TabIndex = 16;
            this.CompanyTypeLabel.Text = "Company Type";
            // 
            // NOBLabel
            // 
            this.NOBLabel.AutoSize = true;
            this.NOBLabel.Font = new System.Drawing.Font("Kelson Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NOBLabel.Location = new System.Drawing.Point(435, 90);
            this.NOBLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NOBLabel.Name = "NOBLabel";
            this.NOBLabel.Size = new System.Drawing.Size(141, 19);
            this.NOBLabel.TabIndex = 15;
            this.NOBLabel.Text = "Nature of business";
            // 
            // ShareholdersLabel
            // 
            this.ShareholdersLabel.AutoSize = true;
            this.ShareholdersLabel.Font = new System.Drawing.Font("Kelson Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShareholdersLabel.Location = new System.Drawing.Point(435, 55);
            this.ShareholdersLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ShareholdersLabel.Name = "ShareholdersLabel";
            this.ShareholdersLabel.Size = new System.Drawing.Size(104, 19);
            this.ShareholdersLabel.TabIndex = 14;
            this.ShareholdersLabel.Text = "Shareholders";
            // 
            // CompanyNameLabel
            // 
            this.CompanyNameLabel.AutoSize = true;
            this.CompanyNameLabel.Font = new System.Drawing.Font("Kelson Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompanyNameLabel.Location = new System.Drawing.Point(435, 20);
            this.CompanyNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CompanyNameLabel.Name = "CompanyNameLabel";
            this.CompanyNameLabel.Size = new System.Drawing.Size(121, 19);
            this.CompanyNameLabel.TabIndex = 13;
            this.CompanyNameLabel.Text = "Company Name";
            // 
            // CNDisplay
            // 
            this.CNDisplay.AutoSize = true;
            this.CNDisplay.Font = new System.Drawing.Font("Kelson Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CNDisplay.Location = new System.Drawing.Point(584, 20);
            this.CNDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CNDisplay.Name = "CNDisplay";
            this.CNDisplay.Size = new System.Drawing.Size(49, 19);
            this.CNDisplay.TabIndex = 17;
            this.CNDisplay.Text = "-----";
            // 
            // SHDisplay
            // 
            this.SHDisplay.AutoSize = true;
            this.SHDisplay.Font = new System.Drawing.Font("Kelson Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SHDisplay.Location = new System.Drawing.Point(584, 55);
            this.SHDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SHDisplay.Name = "SHDisplay";
            this.SHDisplay.Size = new System.Drawing.Size(49, 19);
            this.SHDisplay.TabIndex = 18;
            this.SHDisplay.Text = "-----";
            // 
            // NOBDisplay
            // 
            this.NOBDisplay.AutoSize = true;
            this.NOBDisplay.Font = new System.Drawing.Font("Kelson Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NOBDisplay.Location = new System.Drawing.Point(584, 90);
            this.NOBDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.NOBDisplay.Name = "NOBDisplay";
            this.NOBDisplay.Size = new System.Drawing.Size(49, 19);
            this.NOBDisplay.TabIndex = 19;
            this.NOBDisplay.Text = "-----";
            // 
            // CTDisplay
            // 
            this.CTDisplay.AutoSize = true;
            this.CTDisplay.Font = new System.Drawing.Font("Kelson Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CTDisplay.Location = new System.Drawing.Point(584, 125);
            this.CTDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CTDisplay.Name = "CTDisplay";
            this.CTDisplay.Size = new System.Drawing.Size(49, 19);
            this.CTDisplay.TabIndex = 20;
            this.CTDisplay.Text = "-----";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(128, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // DescriptionDisplay
            // 
            this.DescriptionDisplay.AutoSize = true;
            this.DescriptionDisplay.Location = new System.Drawing.Point(125, 326);
            this.DescriptionDisplay.Name = "DescriptionDisplay";
            this.DescriptionDisplay.Size = new System.Drawing.Size(156, 13);
            this.DescriptionDisplay.TabIndex = 22;
            this.DescriptionDisplay.Text = "Square descriptions will go here";
            // 
            // RollResultDisplay
            // 
            this.RollResultDisplay.AutoSize = true;
            this.RollResultDisplay.Font = new System.Drawing.Font("Kelson Sans", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RollResultDisplay.Location = new System.Drawing.Point(38, 61);
            this.RollResultDisplay.Name = "RollResultDisplay";
            this.RollResultDisplay.Size = new System.Drawing.Size(35, 42);
            this.RollResultDisplay.TabIndex = 23;
            this.RollResultDisplay.Text = "-";
            // 
            // Board_Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RollResultDisplay);
            this.Controls.Add(this.DescriptionDisplay);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.CTDisplay);
            this.Controls.Add(this.NOBDisplay);
            this.Controls.Add(this.SHDisplay);
            this.Controls.Add(this.CNDisplay);
            this.Controls.Add(this.CompanyTypeLabel);
            this.Controls.Add(this.NOBLabel);
            this.Controls.Add(this.ShareholdersLabel);
            this.Controls.Add(this.CompanyNameLabel);
            this.Controls.Add(this.RollDiceButton);
            this.Name = "Board_Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Board Game";
            this.Load += new System.EventHandler(this.Board_Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RollDiceButton;
        private System.Windows.Forms.Label CompanyTypeLabel;
        private System.Windows.Forms.Label NOBLabel;
        private System.Windows.Forms.Label ShareholdersLabel;
        private System.Windows.Forms.Label CompanyNameLabel;
        private System.Windows.Forms.Label CNDisplay;
        private System.Windows.Forms.Label SHDisplay;
        private System.Windows.Forms.Label NOBDisplay;
        private System.Windows.Forms.Label CTDisplay;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label DescriptionDisplay;
        private System.Windows.Forms.Label RollResultDisplay;
    }
}