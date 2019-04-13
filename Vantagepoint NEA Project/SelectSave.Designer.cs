namespace Vantagepoint_NEA_Project
{
    partial class SelectSave
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
            this.SaveList = new System.Windows.Forms.ListBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SearchBar = new System.Windows.Forms.TextBox();
            this.ConfirmSave = new System.Windows.Forms.Button();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.GamesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SaveList
            // 
            this.SaveList.FormattingEnabled = true;
            this.SaveList.Location = new System.Drawing.Point(12, 39);
            this.SaveList.Name = "SaveList";
            this.SaveList.Size = new System.Drawing.Size(491, 95);
            this.SaveList.TabIndex = 0;
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(509, 39);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(87, 46);
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // SearchBar
            // 
            this.SearchBar.Location = new System.Drawing.Point(12, 418);
            this.SearchBar.Name = "SearchBar";
            this.SearchBar.Size = new System.Drawing.Size(311, 20);
            this.SearchBar.TabIndex = 2;
            // 
            // ConfirmSave
            // 
            this.ConfirmSave.Location = new System.Drawing.Point(701, 392);
            this.ConfirmSave.Name = "ConfirmSave";
            this.ConfirmSave.Size = new System.Drawing.Size(87, 46);
            this.ConfirmSave.TabIndex = 3;
            this.ConfirmSave.Text = "Confirm";
            this.ConfirmSave.UseVisualStyleBackColor = true;
            this.ConfirmSave.Click += new System.EventHandler(this.ConfirmSave_Click);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Location = new System.Drawing.Point(12, 402);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(41, 13);
            this.SearchLabel.TabIndex = 4;
            this.SearchLabel.Text = "Search";
            // 
            // GamesLabel
            // 
            this.GamesLabel.AutoSize = true;
            this.GamesLabel.Location = new System.Drawing.Point(12, 23);
            this.GamesLabel.Name = "GamesLabel";
            this.GamesLabel.Size = new System.Drawing.Size(74, 13);
            this.GamesLabel.TabIndex = 5;
            this.GamesLabel.Text = "Saved Games";
            // 
            // SelectSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GamesLabel);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.ConfirmSave);
            this.Controls.Add(this.SearchBar);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.SaveList);
            this.Name = "SelectSave";
            this.Text = "SelectSave";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox SaveList;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.TextBox SearchBar;
        private System.Windows.Forms.Button ConfirmSave;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.Label GamesLabel;
    }
}