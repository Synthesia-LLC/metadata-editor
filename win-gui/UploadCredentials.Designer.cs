namespace Synthesia
{
    partial class UploadCredentials
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadCredentials));
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.WarningLabel = new System.Windows.Forms.Label();
            this.SiteKeyLabel = new System.Windows.Forms.Label();
            this.SiteKeyBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(265, 91);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 3;
            this._okButton.Text = "&OK";
            this._okButton.UseVisualStyleBackColor = true;
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(346, 91);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 4;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // WarningLabel
            // 
            this.WarningLabel.AutoSize = true;
            this.WarningLabel.Location = new System.Drawing.Point(12, 9);
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(408, 13);
            this.WarningLabel.TabIndex = 0;
            this.WarningLabel.Text = "Enter your Synthesia site key below to upload this metadata to the Synthesia webs" +
                "ite.";
            // 
            // SiteKeyLabel
            // 
            this.SiteKeyLabel.AutoSize = true;
            this.SiteKeyLabel.Location = new System.Drawing.Point(12, 41);
            this.SiteKeyLabel.Name = "SiteKeyLabel";
            this.SiteKeyLabel.Size = new System.Drawing.Size(48, 13);
            this.SiteKeyLabel.TabIndex = 1;
            this.SiteKeyLabel.Text = "Site key:";
            // 
            // SiteKeyBox
            // 
            this.SiteKeyBox.Location = new System.Drawing.Point(66, 38);
            this.SiteKeyBox.Name = "SiteKeyBox";
            this.SiteKeyBox.Size = new System.Drawing.Size(354, 20);
            this.SiteKeyBox.TabIndex = 2;
            this.SiteKeyBox.TextChanged += new System.EventHandler(this.SiteKeyBox_TextChanged);
            // 
            // UploadCredentials
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(449, 146);
            this.ControlBox = false;
            this.Controls.Add(this.SiteKeyBox);
            this.Controls.Add(this.SiteKeyLabel);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this.WarningLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UploadCredentials";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Upload Metadata...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Label WarningLabel;
        private System.Windows.Forms.Label SiteKeyLabel;
        private System.Windows.Forms.TextBox SiteKeyBox;
    }
}