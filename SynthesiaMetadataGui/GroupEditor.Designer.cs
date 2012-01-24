namespace Synthesia
{
    partial class GroupEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupEditor));
            this.GroupList = new System.Windows.Forms.TreeView();
            this.GroupContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CreateTopLevelGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateSubGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.RemoveGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveSongsFromGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.labelGroups = new System.Windows.Forms.Label();
            this.labelSongs = new System.Windows.Forms.Label();
            this.labelInstructions = new System.Windows.Forms.Label();
            this.GroupNameBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.SongList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this._closeButton = new System.Windows.Forms.Button();
            this.MoveDownButton = new System.Windows.Forms.Button();
            this.MoveUpButton = new System.Windows.Forms.Button();
            this.TreeImages = new System.Windows.Forms.ImageList(this.components);
            this.GroupContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupList
            // 
            this.GroupList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupList.ContextMenuStrip = this.GroupContextMenu;
            this.GroupList.HideSelection = false;
            this.GroupList.ImageIndex = 0;
            this.GroupList.ImageList = this.TreeImages;
            this.GroupList.Location = new System.Drawing.Point(12, 25);
            this.GroupList.Name = "GroupList";
            this.GroupList.PathSeparator = "_%*%_";
            this.GroupList.SelectedImageIndex = 0;
            this.GroupList.Size = new System.Drawing.Size(277, 401);
            this.GroupList.TabIndex = 1;
            this.GroupList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.GroupList_AfterSelect);
            // 
            // GroupContextMenu
            // 
            this.GroupContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateTopLevelGroupMenu,
            this.CreateSubGroupMenu,
            this.toolStripMenuItem1,
            this.RemoveGroupMenu,
            this.RemoveSongsFromGroupMenu});
            this.GroupContextMenu.Name = "GroupContextMenu";
            this.GroupContextMenu.Size = new System.Drawing.Size(299, 98);
            this.GroupContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.GroupContextMenu_Opening);
            // 
            // CreateTopLevelGroupMenu
            // 
            this.CreateTopLevelGroupMenu.Name = "CreateTopLevelGroupMenu";
            this.CreateTopLevelGroupMenu.Size = new System.Drawing.Size(298, 22);
            this.CreateTopLevelGroupMenu.Text = "Create new top-level group";
            this.CreateTopLevelGroupMenu.Click += new System.EventHandler(this.CreateTopLevelGroupMenu_Click);
            // 
            // CreateSubGroupMenu
            // 
            this.CreateSubGroupMenu.Name = "CreateSubGroupMenu";
            this.CreateSubGroupMenu.Size = new System.Drawing.Size(298, 22);
            this.CreateSubGroupMenu.Text = "Create group under [group set in code]";
            this.CreateSubGroupMenu.Click += new System.EventHandler(this.CreateSubGroupMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(295, 6);
            // 
            // RemoveGroupMenu
            // 
            this.RemoveGroupMenu.Name = "RemoveGroupMenu";
            this.RemoveGroupMenu.Size = new System.Drawing.Size(298, 22);
            this.RemoveGroupMenu.Text = "Remove [group set in code]";
            this.RemoveGroupMenu.Click += new System.EventHandler(this.RemoveGroupMenu_Click);
            // 
            // RemoveSongsFromGroupMenu
            // 
            this.RemoveSongsFromGroupMenu.Name = "RemoveSongsFromGroupMenu";
            this.RemoveSongsFromGroupMenu.Size = new System.Drawing.Size(298, 22);
            this.RemoveSongsFromGroupMenu.Text = "Remove all songs from [group set in code]";
            this.RemoveSongsFromGroupMenu.Click += new System.EventHandler(this.RemoveSongsFromGroupMenu_Click);
            // 
            // labelGroups
            // 
            this.labelGroups.AutoSize = true;
            this.labelGroups.Location = new System.Drawing.Point(12, 9);
            this.labelGroups.Name = "labelGroups";
            this.labelGroups.Size = new System.Drawing.Size(69, 13);
            this.labelGroups.TabIndex = 0;
            this.labelGroups.Text = "Song &Groups";
            // 
            // labelSongs
            // 
            this.labelSongs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSongs.AutoSize = true;
            this.labelSongs.Location = new System.Drawing.Point(381, 9);
            this.labelSongs.Name = "labelSongs";
            this.labelSongs.Size = new System.Drawing.Size(96, 13);
            this.labelSongs.TabIndex = 5;
            this.labelSongs.Text = "Unassigned &Songs";
            // 
            // labelInstructions
            // 
            this.labelInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.Location = new System.Drawing.Point(12, 467);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(510, 13);
            this.labelInstructions.TabIndex = 7;
            this.labelInstructions.Text = "Right-click group box to manage groups.  Use the Add/Remove buttons to assign son" +
                "gs to selected group.";
            // 
            // GroupNameBox
            // 
            this.GroupNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupNameBox.Location = new System.Drawing.Point(15, 436);
            this.GroupNameBox.Name = "GroupNameBox";
            this.GroupNameBox.Size = new System.Drawing.Size(274, 20);
            this.GroupNameBox.TabIndex = 2;
            this.GroupNameBox.TextChanged += new System.EventHandler(this.GroupNameBox_TextChanged);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddButton.Location = new System.Drawing.Point(295, 218);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(83, 23);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "< &Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RemoveButton.Location = new System.Drawing.Point(295, 247);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(83, 23);
            this.RemoveButton.TabIndex = 4;
            this.RemoveButton.Text = "&Remove >";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // SongList
            // 
            this.SongList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SongList.FormattingEnabled = true;
            this.SongList.Location = new System.Drawing.Point(384, 25);
            this.SongList.Name = "SongList";
            this.SongList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.SongList.Size = new System.Drawing.Size(296, 433);
            this.SongList.TabIndex = 6;
            this.SongList.SelectedIndexChanged += new System.EventHandler(this.SongList_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(295, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "PRINT!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._closeButton.Location = new System.Drawing.Point(605, 462);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 9;
            this._closeButton.Text = "&Close";
            this._closeButton.UseVisualStyleBackColor = true;
            // 
            // MoveDownButton
            // 
            this.MoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveDownButton.Image = global::Synthesia.Properties.Resources.arrowDown;
            this.MoveDownButton.Location = new System.Drawing.Point(295, 54);
            this.MoveDownButton.Name = "MoveDownButton";
            this.MoveDownButton.Size = new System.Drawing.Size(28, 23);
            this.MoveDownButton.TabIndex = 12;
            this.MoveDownButton.UseVisualStyleBackColor = true;
            this.MoveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // MoveUpButton
            // 
            this.MoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MoveUpButton.Image = global::Synthesia.Properties.Resources.arrowUp;
            this.MoveUpButton.Location = new System.Drawing.Point(295, 25);
            this.MoveUpButton.Name = "MoveUpButton";
            this.MoveUpButton.Size = new System.Drawing.Size(28, 23);
            this.MoveUpButton.TabIndex = 11;
            this.MoveUpButton.UseVisualStyleBackColor = true;
            this.MoveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // TreeImages
            // 
            this.TreeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeImages.ImageStream")));
            this.TreeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.TreeImages.Images.SetKeyName(0, "group.png");
            this.TreeImages.Images.SetKeyName(1, "song.png");
            // 
            // GroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(705, 506);
            this.Controls.Add(this.SongList);
            this.Controls.Add(this.MoveDownButton);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.MoveUpButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.GroupNameBox);
            this.Controls.Add(this._closeButton);
            this.Controls.Add(this.labelSongs);
            this.Controls.Add(this.labelGroups);
            this.Controls.Add(this.GroupList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(721, 280);
            this.Name = "GroupEditor";
            this.Text = "Song Group Editor";
            this.GroupContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView GroupList;
        private System.Windows.Forms.Label labelGroups;
        private System.Windows.Forms.Label labelSongs;
        private System.Windows.Forms.Label labelInstructions;
        private System.Windows.Forms.ContextMenuStrip GroupContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CreateTopLevelGroupMenu;
        private System.Windows.Forms.ToolStripMenuItem CreateSubGroupMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveGroupMenu;
        private System.Windows.Forms.TextBox GroupNameBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem RemoveSongsFromGroupMenu;
        private System.Windows.Forms.ListBox SongList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.Button MoveUpButton;
        private System.Windows.Forms.Button MoveDownButton;
        private System.Windows.Forms.ImageList TreeImages;
    }
}