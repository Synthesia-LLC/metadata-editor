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
         this.ButtonCancel = new System.Windows.Forms.Button();
         this.ButtonOK = new System.Windows.Forms.Button();
         this.CheckParts = new System.Windows.Forms.CheckBox();
         this.ComboImportFrom = new System.Windows.Forms.ComboBox();
         this.ImportLabel = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // WarningLabel
         // 
         this.WarningLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.WarningLabel.Location = new System.Drawing.Point(12, 147);
         this.WarningLabel.Name = "WarningLabel";
         this.WarningLabel.Size = new System.Drawing.Size(445, 78);
         this.WarningLabel.TabIndex = 5;
         this.WarningLabel.Text = resources.GetString("WarningLabel.Text");
         // 
         // CheckFingerHints
         // 
         this.CheckFingerHints.AutoSize = true;
         this.CheckFingerHints.Checked = true;
         this.CheckFingerHints.CheckState = System.Windows.Forms.CheckState.Checked;
         this.CheckFingerHints.Location = new System.Drawing.Point(12, 66);
         this.CheckFingerHints.Name = "CheckFingerHints";
         this.CheckFingerHints.Size = new System.Drawing.Size(114, 17);
         this.CheckFingerHints.TabIndex = 2;
         this.CheckFingerHints.Text = "Import Finger Hints";
         this.CheckFingerHints.UseVisualStyleBackColor = true;
         this.CheckFingerHints.CheckedChanged += new System.EventHandler(this.CheckChanged);
         // 
         // CheckHandParts
         // 
         this.CheckHandParts.AutoSize = true;
         this.CheckHandParts.Checked = true;
         this.CheckHandParts.CheckState = System.Windows.Forms.CheckState.Checked;
         this.CheckHandParts.Location = new System.Drawing.Point(12, 89);
         this.CheckHandParts.Name = "CheckHandParts";
         this.CheckHandParts.Size = new System.Drawing.Size(280, 17);
         this.CheckHandParts.TabIndex = 3;
         this.CheckHandParts.Text = "Import Hand Parts (old left/right/both \"configHashes\")";
         this.CheckHandParts.UseVisualStyleBackColor = true;
         this.CheckHandParts.CheckedChanged += new System.EventHandler(this.CheckChanged);
         // 
         // ButtonCancel
         // 
         this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.ButtonCancel.Location = new System.Drawing.Point(379, 239);
         this.ButtonCancel.Name = "ButtonCancel";
         this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
         this.ButtonCancel.TabIndex = 7;
         this.ButtonCancel.Text = "&Cancel";
         this.ButtonCancel.UseVisualStyleBackColor = true;
         // 
         // ButtonOK
         // 
         this.ButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.ButtonOK.Location = new System.Drawing.Point(298, 239);
         this.ButtonOK.Name = "ButtonOK";
         this.ButtonOK.Size = new System.Drawing.Size(75, 23);
         this.ButtonOK.TabIndex = 6;
         this.ButtonOK.Text = "&OK";
         this.ButtonOK.UseVisualStyleBackColor = true;
         // 
         // CheckParts
         // 
         this.CheckParts.AutoSize = true;
         this.CheckParts.Checked = true;
         this.CheckParts.CheckState = System.Windows.Forms.CheckState.Checked;
         this.CheckParts.Location = new System.Drawing.Point(12, 112);
         this.CheckParts.Name = "CheckParts";
         this.CheckParts.Size = new System.Drawing.Size(319, 17);
         this.CheckParts.TabIndex = 4;
         this.CheckParts.Text = "Import Parts (new left/right/background/discard hand splitting)";
         this.CheckParts.UseVisualStyleBackColor = true;
         // 
         // ComboImportFrom
         // 
         this.ComboImportFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.ComboImportFrom.FormattingEnabled = true;
         this.ComboImportFrom.Items.AddRange(new object[] {
            "Synthesia (official release, typical data folder)",
            "SynthesiaDev (used by development previews)"});
         this.ComboImportFrom.Location = new System.Drawing.Point(9, 27);
         this.ComboImportFrom.Name = "ComboImportFrom";
         this.ComboImportFrom.Size = new System.Drawing.Size(353, 21);
         this.ComboImportFrom.TabIndex = 1;
         // 
         // ImportLabel
         // 
         this.ImportLabel.AutoSize = true;
         this.ImportLabel.Location = new System.Drawing.Point(12, 11);
         this.ImportLabel.Name = "ImportLabel";
         this.ImportLabel.Size = new System.Drawing.Size(65, 13);
         this.ImportLabel.TabIndex = 0;
         this.ImportLabel.Text = "Import From:";
         // 
         // ImportSelection
         // 
         this.AcceptButton = this.ButtonOK;
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
         this.CancelButton = this.ButtonCancel;
         this.ClientSize = new System.Drawing.Size(487, 278);
         this.ControlBox = false;
         this.Controls.Add(this.ImportLabel);
         this.Controls.Add(this.ComboImportFrom);
         this.Controls.Add(this.CheckParts);
         this.Controls.Add(this.ButtonOK);
         this.Controls.Add(this.ButtonCancel);
         this.Controls.Add(this.CheckHandParts);
         this.Controls.Add(this.CheckFingerHints);
         this.Controls.Add(this.WarningLabel);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ImportSelection";
         this.ShowIcon = false;
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Import from Synthesia...";
         this.Load += new System.EventHandler(this.ImportSelection_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WarningLabel;
        private System.Windows.Forms.CheckBox CheckFingerHints;
        private System.Windows.Forms.CheckBox CheckHandParts;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.CheckBox CheckParts;
        private System.Windows.Forms.ComboBox ComboImportFrom;
        private System.Windows.Forms.Label ImportLabel;
    }
}