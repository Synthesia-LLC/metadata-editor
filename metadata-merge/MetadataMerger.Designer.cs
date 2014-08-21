namespace Synthesia
{
   partial class MetadataMerger
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetadataMerger));
         this.SaveButton = new System.Windows.Forms.Button();
         this.LogText = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // SaveButton
         // 
         this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.SaveButton.Enabled = false;
         this.SaveButton.Location = new System.Drawing.Point(558, 233);
         this.SaveButton.Name = "SaveButton";
         this.SaveButton.Size = new System.Drawing.Size(97, 23);
         this.SaveButton.TabIndex = 1;
         this.SaveButton.Text = "&Save";
         this.SaveButton.UseVisualStyleBackColor = true;
         this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
         // 
         // LogText
         // 
         this.LogText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.LogText.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.LogText.Location = new System.Drawing.Point(12, 12);
         this.LogText.Multiline = true;
         this.LogText.Name = "LogText";
         this.LogText.ReadOnly = true;
         this.LogText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.LogText.Size = new System.Drawing.Size(643, 215);
         this.LogText.TabIndex = 0;
         this.LogText.Text = "Start by dragging the master metadata file to this window.\r\nFiles dragged in afte" +
    "r that will have their metadata added to the master.";
         // 
         // MetadataMerger
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(667, 268);
         this.Controls.Add(this.LogText);
         this.Controls.Add(this.SaveButton);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MinimumSize = new System.Drawing.Size(501, 216);
         this.Name = "MetadataMerger";
         this.Text = "Synthesia Metadata Merger";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MetadataMerger_FormClosing);
         this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MetadataMerger_DragDrop);
         this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MetadataMerger_DragEnter);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button SaveButton;
      private System.Windows.Forms.TextBox LogText;
   }
}

