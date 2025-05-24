namespace DVlD_Project.Users
{
    partial class frmShowDatilesUser
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
            this.btnCloseN = new System.Windows.Forms.Button();
            this.ctrlUserCard1 = new DVlD_Project.Users.ctrlUserCard();
            this.SuspendLayout();
            // 
            // btnCloseN
            // 
            this.btnCloseN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCloseN.Image = global::DVlD_Project.Properties.Resources.Close_32;
            this.btnCloseN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCloseN.Location = new System.Drawing.Point(708, 437);
            this.btnCloseN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCloseN.Name = "btnCloseN";
            this.btnCloseN.Size = new System.Drawing.Size(126, 37);
            this.btnCloseN.TabIndex = 17;
            this.btnCloseN.Text = "Close";
            this.btnCloseN.UseVisualStyleBackColor = true;
            this.btnCloseN.Click += new System.EventHandler(this.btnCloseN_Click);
            // 
            // ctrlUserCard1
            // 
            this.ctrlUserCard1.BackColor = System.Drawing.Color.White;
            this.ctrlUserCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlUserCard1.Location = new System.Drawing.Point(-6, 14);
            this.ctrlUserCard1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlUserCard1.Name = "ctrlUserCard1";
            this.ctrlUserCard1.Size = new System.Drawing.Size(852, 433);
            this.ctrlUserCard1.TabIndex = 18;
            // 
            // frmShowDatilesUser
            // 
            this.AcceptButton = this.btnCloseN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCloseN;
            this.ClientSize = new System.Drawing.Size(859, 500);
            this.Controls.Add(this.btnCloseN);
            this.Controls.Add(this.ctrlUserCard1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmShowDatilesUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Show User Info";
            this.Load += new System.EventHandler(this.frmShowDatilesUser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCloseN;
        private ctrlUserCard ctrlUserCard1;
    }
}