namespace Synthesia
{
    partial class ImportSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSelection));
            this.WarningLabel = new System.Windows.Forms.Label();
            this.CheckFingerHints = new System.Windows.Forms.CheckBox();
            this.CheckHandParts = new System.Windows.Forms.CheckBox();
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WarningLabel
            // 
            this.WarningLabel.Location = new System.Drawing.Point(12, 95);
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(457, 78);
            this.WarningLabel.TabIndex = 2;
            this.WarningLabel.Text = resources.GetString("WarningLabel.Text");
            // 
            // CheckFingerHints
            // 
            this.CheckFingerHints.AutoSize = true;
            this.CheckFingerHints.Checked = true;
            this.CheckFingerHints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckFingerHints.Location = new System.Drawing.Point(12, 12);
            this.CheckFingerHints.Name = "CheckFingerHints";
            this.CheckFingerHints.Size = new System.Drawing.Size(114, 17);
            this.CheckFingerHints.TabIndex = 0;
            this.CheckFingerHints.Text = "Import Finger Hints";
            this.CheckFingerHints.UseVisualStyleBackColor = true;
            this.CheckFingerHints.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // CheckHandParts
            // 
            this.CheckHandParts.AutoSize = true;
            this.CheckHandParts.Checked = true;
            this.CheckHandParts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckHandParts.Location = new System.Drawing.Point(12, 35);
            this.CheckHandParts.Name = "CheckHandParts";
            this.CheckHandParts.Size = new System.Drawing.Size(185, 17);
            this.CheckHandParts.TabIndex = 1;
            this.CheckHandParts.Text = "Import Hand Parts (left/right/both)";
            this.CheckHandParts.UseVisualStyleBackColor = true;
            this.CheckHandParts.CheckedChanged += new System.EventHandler(this.CheckChanged);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(382, 176);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 4;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._okButton.Location = new System.Drawing.Point(301, 176);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 3;
            this._okButton.Text = "&OK";
            this._okButton.UseVisualStyleBackColor = true;
            // 
            // ImportSelection
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(502, 227);
            this.ControlBox = false;
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this.CheckHandParts);
            this.Controls.Add(this.CheckFingerHints);
            this.Controls.Add(this.WarningLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportSelection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import from Synthesia...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WarningLabel;
        private System.Windows.Forms.CheckBox CheckFingerHints;
        private System.Windows.Forms.CheckBox CheckHandParts;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
    }
}