﻿namespace Synthesia
{
    partial class MetadataEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetadataEditor));
            this.SongList = new System.Windows.Forms.ListBox();
            this.AddSong = new System.Windows.Forms.Button();
            this.RemoveSong = new System.Windows.Forms.Button();
            this.UniqueIdBox = new System.Windows.Forms.TextBox();
            this.TitleBox = new System.Windows.Forms.TextBox();
            this.SubtitleBox = new System.Windows.Forms.TextBox();
            this.ComposerBox = new System.Windows.Forms.TextBox();
            this.LicenseBox = new System.Windows.Forms.TextBox();
            this.CopyrightBox = new System.Windows.Forms.TextBox();
            this.ArrangerBox = new System.Windows.Forms.TextBox();
            this.DifficultyBox = new System.Windows.Forms.NumericUpDown();
            this.RatingBox = new System.Windows.Forms.NumericUpDown();
            this.UniqueIdLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ComposerLabel = new System.Windows.Forms.Label();
            this.SubtitleLabel = new System.Windows.Forms.Label();
            this.DifficultyLabel = new System.Windows.Forms.Label();
            this.LicenseLabel = new System.Windows.Forms.Label();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.ArrangerLabel = new System.Windows.Forms.Label();
            this.TagsLabel = new System.Windows.Forms.Label();
            this.RatingLabel = new System.Windows.Forms.Label();
            this.TagList = new System.Windows.Forms.ListBox();
            this.TagBox = new System.Windows.Forms.TextBox();
            this.RemoveTag = new System.Windows.Forms.Button();
            this.AddTag = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportFingerHintsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Tip = new System.Windows.Forms.ToolTip(this.components);
            this.DifficultyRangeLabel = new System.Windows.Forms.Label();
            this.RatingRangeLabel = new System.Windows.Forms.Label();
            this.SaveMetadataDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenMetadataDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenSongDialog = new System.Windows.Forms.OpenFileDialog();
            this.SongListLabel = new System.Windows.Forms.Label();
            this.PropertiesGroup = new System.Windows.Forms.GroupBox();
            this.FingerHintBox = new System.Windows.Forms.TextBox();
            this.FingerHintLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DifficultyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RatingBox)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.PropertiesGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // SongList
            // 
            this.SongList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.SongList.FormattingEnabled = true;
            this.SongList.Location = new System.Drawing.Point(12, 46);
            this.SongList.Name = "SongList";
            this.SongList.Size = new System.Drawing.Size(300, 446);
            this.SongList.TabIndex = 2;
            this.SongList.SelectedIndexChanged += new System.EventHandler(this.SongList_SelectedIndexChanged);
            // 
            // AddSong
            // 
            this.AddSong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddSong.Location = new System.Drawing.Point(12, 504);
            this.AddSong.Name = "AddSong";
            this.AddSong.Size = new System.Drawing.Size(217, 23);
            this.AddSong.TabIndex = 3;
            this.AddSong.Text = "Add Song...";
            this.Tip.SetToolTip(this.AddSong, "Browse to a song file on your hard drive to add to this metadata file");
            this.AddSong.UseVisualStyleBackColor = true;
            this.AddSong.Click += new System.EventHandler(this.AddSong_Click);
            // 
            // RemoveSong
            // 
            this.RemoveSong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemoveSong.Location = new System.Drawing.Point(235, 504);
            this.RemoveSong.Name = "RemoveSong";
            this.RemoveSong.Size = new System.Drawing.Size(77, 23);
            this.RemoveSong.TabIndex = 4;
            this.RemoveSong.Text = "Remove";
            this.Tip.SetToolTip(this.RemoveSong, "Removes the current song metadata entry from this file");
            this.RemoveSong.UseVisualStyleBackColor = true;
            this.RemoveSong.Click += new System.EventHandler(this.RemoveSong_Click);
            // 
            // UniqueIdBox
            // 
            this.UniqueIdBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.UniqueIdBox.Location = new System.Drawing.Point(70, 24);
            this.UniqueIdBox.Name = "UniqueIdBox";
            this.UniqueIdBox.ReadOnly = true;
            this.UniqueIdBox.Size = new System.Drawing.Size(316, 20);
            this.UniqueIdBox.TabIndex = 1;
            // 
            // TitleBox
            // 
            this.TitleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleBox.Location = new System.Drawing.Point(70, 50);
            this.TitleBox.Name = "TitleBox";
            this.TitleBox.Size = new System.Drawing.Size(316, 20);
            this.TitleBox.TabIndex = 4;
            this.TitleBox.TextChanged += new System.EventHandler(this.TitleBox_TextChanged);
            // 
            // SubtitleBox
            // 
            this.SubtitleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SubtitleBox.Location = new System.Drawing.Point(70, 76);
            this.SubtitleBox.Name = "SubtitleBox";
            this.SubtitleBox.Size = new System.Drawing.Size(316, 20);
            this.SubtitleBox.TabIndex = 6;
            this.SubtitleBox.TextChanged += new System.EventHandler(this.SubtitleBox_TextChanged);
            // 
            // ComposerBox
            // 
            this.ComposerBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComposerBox.Location = new System.Drawing.Point(70, 118);
            this.ComposerBox.Name = "ComposerBox";
            this.ComposerBox.Size = new System.Drawing.Size(316, 20);
            this.ComposerBox.TabIndex = 8;
            this.ComposerBox.TextChanged += new System.EventHandler(this.ComposerBox_TextChanged);
            // 
            // LicenseBox
            // 
            this.LicenseBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LicenseBox.Location = new System.Drawing.Point(70, 196);
            this.LicenseBox.Name = "LicenseBox";
            this.LicenseBox.Size = new System.Drawing.Size(316, 20);
            this.LicenseBox.TabIndex = 14;
            this.LicenseBox.TextChanged += new System.EventHandler(this.LicenseBox_TextChanged);
            // 
            // CopyrightBox
            // 
            this.CopyrightBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyrightBox.Location = new System.Drawing.Point(70, 170);
            this.CopyrightBox.Name = "CopyrightBox";
            this.CopyrightBox.Size = new System.Drawing.Size(316, 20);
            this.CopyrightBox.TabIndex = 12;
            this.CopyrightBox.TextChanged += new System.EventHandler(this.CopyrightBox_TextChanged);
            // 
            // ArrangerBox
            // 
            this.ArrangerBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ArrangerBox.Location = new System.Drawing.Point(70, 144);
            this.ArrangerBox.Name = "ArrangerBox";
            this.ArrangerBox.Size = new System.Drawing.Size(316, 20);
            this.ArrangerBox.TabIndex = 10;
            this.ArrangerBox.TextChanged += new System.EventHandler(this.ArrangerBox_TextChanged);
            // 
            // DifficultyBox
            // 
            this.DifficultyBox.Location = new System.Drawing.Point(70, 238);
            this.DifficultyBox.Name = "DifficultyBox";
            this.DifficultyBox.Size = new System.Drawing.Size(64, 20);
            this.DifficultyBox.TabIndex = 16;
            this.DifficultyBox.ValueChanged += new System.EventHandler(this.DifficultyBox_ValueChanged);
            // 
            // RatingBox
            // 
            this.RatingBox.Location = new System.Drawing.Point(70, 264);
            this.RatingBox.Name = "RatingBox";
            this.RatingBox.Size = new System.Drawing.Size(64, 20);
            this.RatingBox.TabIndex = 19;
            this.RatingBox.ValueChanged += new System.EventHandler(this.RatingBox_ValueChanged);
            // 
            // UniqueIdLabel
            // 
            this.UniqueIdLabel.AutoSize = true;
            this.UniqueIdLabel.Location = new System.Drawing.Point(6, 27);
            this.UniqueIdLabel.Name = "UniqueIdLabel";
            this.UniqueIdLabel.Size = new System.Drawing.Size(58, 13);
            this.UniqueIdLabel.TabIndex = 0;
            this.UniqueIdLabel.Text = "&Unique ID:";
            this.Tip.SetToolTip(this.UniqueIdLabel, "MD5 hash of song file contents");
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(6, 53);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(30, 13);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "&Title:";
            // 
            // ComposerLabel
            // 
            this.ComposerLabel.AutoSize = true;
            this.ComposerLabel.Location = new System.Drawing.Point(6, 121);
            this.ComposerLabel.Name = "ComposerLabel";
            this.ComposerLabel.Size = new System.Drawing.Size(57, 13);
            this.ComposerLabel.TabIndex = 7;
            this.ComposerLabel.Text = "&Composer:";
            // 
            // SubtitleLabel
            // 
            this.SubtitleLabel.AutoSize = true;
            this.SubtitleLabel.Location = new System.Drawing.Point(6, 79);
            this.SubtitleLabel.Name = "SubtitleLabel";
            this.SubtitleLabel.Size = new System.Drawing.Size(45, 13);
            this.SubtitleLabel.TabIndex = 5;
            this.SubtitleLabel.Text = "&Subtitle:";
            // 
            // DifficultyLabel
            // 
            this.DifficultyLabel.AutoSize = true;
            this.DifficultyLabel.Location = new System.Drawing.Point(6, 240);
            this.DifficultyLabel.Name = "DifficultyLabel";
            this.DifficultyLabel.Size = new System.Drawing.Size(50, 13);
            this.DifficultyLabel.TabIndex = 15;
            this.DifficultyLabel.Text = "&Difficulty:";
            this.Tip.SetToolTip(this.DifficultyLabel, "Percentage ");
            // 
            // LicenseLabel
            // 
            this.LicenseLabel.AutoSize = true;
            this.LicenseLabel.Location = new System.Drawing.Point(6, 199);
            this.LicenseLabel.Name = "LicenseLabel";
            this.LicenseLabel.Size = new System.Drawing.Size(47, 13);
            this.LicenseLabel.TabIndex = 13;
            this.LicenseLabel.Text = "&License:";
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.AutoSize = true;
            this.CopyrightLabel.Location = new System.Drawing.Point(6, 173);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(54, 13);
            this.CopyrightLabel.TabIndex = 11;
            this.CopyrightLabel.Text = "Co&pyright:";
            // 
            // ArrangerLabel
            // 
            this.ArrangerLabel.AutoSize = true;
            this.ArrangerLabel.Location = new System.Drawing.Point(6, 147);
            this.ArrangerLabel.Name = "ArrangerLabel";
            this.ArrangerLabel.Size = new System.Drawing.Size(50, 13);
            this.ArrangerLabel.TabIndex = 9;
            this.ArrangerLabel.Text = "&Arranger:";
            // 
            // TagsLabel
            // 
            this.TagsLabel.AutoSize = true;
            this.TagsLabel.Location = new System.Drawing.Point(6, 374);
            this.TagsLabel.Name = "TagsLabel";
            this.TagsLabel.Size = new System.Drawing.Size(34, 13);
            this.TagsLabel.TabIndex = 21;
            this.TagsLabel.Text = "Ta&gs:";
            // 
            // RatingLabel
            // 
            this.RatingLabel.AutoSize = true;
            this.RatingLabel.Location = new System.Drawing.Point(6, 266);
            this.RatingLabel.Name = "RatingLabel";
            this.RatingLabel.Size = new System.Drawing.Size(41, 13);
            this.RatingLabel.TabIndex = 18;
            this.RatingLabel.Text = "&Rating:";
            // 
            // TagList
            // 
            this.TagList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TagList.FormattingEnabled = true;
            this.TagList.Location = new System.Drawing.Point(70, 397);
            this.TagList.Name = "TagList";
            this.TagList.Size = new System.Drawing.Size(238, 82);
            this.TagList.TabIndex = 24;
            this.TagList.SelectedIndexChanged += new System.EventHandler(this.TagList_SelectedIndexChanged);
            // 
            // TagBox
            // 
            this.TagBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TagBox.Location = new System.Drawing.Point(70, 371);
            this.TagBox.Name = "TagBox";
            this.TagBox.Size = new System.Drawing.Size(238, 20);
            this.TagBox.TabIndex = 22;
            this.TagBox.TextChanged += new System.EventHandler(this.TagBox_TextChanged);
            // 
            // RemoveTag
            // 
            this.RemoveTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveTag.Enabled = false;
            this.RemoveTag.Location = new System.Drawing.Point(314, 398);
            this.RemoveTag.Name = "RemoveTag";
            this.RemoveTag.Size = new System.Drawing.Size(72, 23);
            this.RemoveTag.TabIndex = 25;
            this.RemoveTag.Text = "Remove";
            this.RemoveTag.UseVisualStyleBackColor = true;
            this.RemoveTag.Click += new System.EventHandler(this.RemoveTag_Click);
            // 
            // AddTag
            // 
            this.AddTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddTag.Enabled = false;
            this.AddTag.Location = new System.Drawing.Point(314, 369);
            this.AddTag.Name = "AddTag";
            this.AddTag.Size = new System.Drawing.Size(72, 23);
            this.AddTag.TabIndex = 23;
            this.AddTag.Text = "Add";
            this.AddTag.UseVisualStyleBackColor = true;
            this.AddTag.Click += new System.EventHandler(this.AddTag_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(722, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMenu,
            this.OpenMenu,
            this.SaveMenu,
            this.SaveAsMenu,
            this.toolStripMenuItem1,
            this.ImportFingerHintsMenu,
            this.toolStripMenuItem2,
            this.ExitMenu});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "&File";
            // 
            // NewMenu
            // 
            this.NewMenu.Name = "NewMenu";
            this.NewMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewMenu.Size = new System.Drawing.Size(186, 22);
            this.NewMenu.Text = "&New";
            this.NewMenu.Click += new System.EventHandler(this.NewMenu_Click);
            // 
            // OpenMenu
            // 
            this.OpenMenu.Name = "OpenMenu";
            this.OpenMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenMenu.Size = new System.Drawing.Size(186, 22);
            this.OpenMenu.Text = "&Open...";
            this.OpenMenu.Click += new System.EventHandler(this.OpenMenu_Click);
            // 
            // SaveMenu
            // 
            this.SaveMenu.Name = "SaveMenu";
            this.SaveMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMenu.Size = new System.Drawing.Size(186, 22);
            this.SaveMenu.Text = "&Save";
            this.SaveMenu.Click += new System.EventHandler(this.SaveMenu_Click);
            // 
            // SaveAsMenu
            // 
            this.SaveAsMenu.Name = "SaveAsMenu";
            this.SaveAsMenu.Size = new System.Drawing.Size(186, 22);
            this.SaveAsMenu.Text = "Save &As...";
            this.SaveAsMenu.Click += new System.EventHandler(this.SaveAsMenu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // ImportFingerHintsMenu
            // 
            this.ImportFingerHintsMenu.Name = "ImportFingerHintsMenu";
            this.ImportFingerHintsMenu.Size = new System.Drawing.Size(186, 22);
            this.ImportFingerHintsMenu.Text = "&Import Finger Hints...";
            this.ImportFingerHintsMenu.Click += new System.EventHandler(this.ImportFingerHintsMenu_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(183, 6);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.Size = new System.Drawing.Size(186, 22);
            this.ExitMenu.Text = "E&xit";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenu});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(44, 20);
            this.HelpMenu.Text = "&Help";
            // 
            // AboutMenu
            // 
            this.AboutMenu.Name = "AboutMenu";
            this.AboutMenu.Size = new System.Drawing.Size(116, 22);
            this.AboutMenu.Text = "&About...";
            this.AboutMenu.Click += new System.EventHandler(this.AboutMenu_Click);
            // 
            // DifficultyRangeLabel
            // 
            this.DifficultyRangeLabel.AutoSize = true;
            this.DifficultyRangeLabel.Location = new System.Drawing.Point(140, 240);
            this.DifficultyRangeLabel.Name = "DifficultyRangeLabel";
            this.DifficultyRangeLabel.Size = new System.Drawing.Size(103, 13);
            this.DifficultyRangeLabel.TabIndex = 17;
            this.DifficultyRangeLabel.Text = "(1-100 or 0 for none)";
            // 
            // RatingRangeLabel
            // 
            this.RatingRangeLabel.AutoSize = true;
            this.RatingRangeLabel.Location = new System.Drawing.Point(140, 266);
            this.RatingRangeLabel.Name = "RatingRangeLabel";
            this.RatingRangeLabel.Size = new System.Drawing.Size(103, 13);
            this.RatingRangeLabel.TabIndex = 20;
            this.RatingRangeLabel.Text = "(1-100 or 0 for none)";
            // 
            // SaveMetadataDialog
            // 
            this.SaveMetadataDialog.Filter = "Synthesia Metadata Files (*.synthesia)|*.synthesia|All Files (*.*)|*.*";
            this.SaveMetadataDialog.Title = "Save Metadata File";
            // 
            // OpenMetadataDialog
            // 
            this.OpenMetadataDialog.Filter = "Synthesia Metadata Files|*.synthesia;*.xml|All Files (*.*)|*.*";
            this.OpenMetadataDialog.Title = "Open Metadata File";
            // 
            // OpenSongDialog
            // 
            this.OpenSongDialog.Filter = "Supported Songs|*.mid;*.midi;*.kar|All Files (*.*)|*.*";
            this.OpenSongDialog.Multiselect = true;
            // 
            // SongListLabel
            // 
            this.SongListLabel.AutoSize = true;
            this.SongListLabel.Location = new System.Drawing.Point(12, 32);
            this.SongListLabel.Name = "SongListLabel";
            this.SongListLabel.Size = new System.Drawing.Size(182, 13);
            this.SongListLabel.TabIndex = 1;
            this.SongListLabel.Text = "Songs described in this metadata file:";
            // 
            // PropertiesGroup
            // 
            this.PropertiesGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertiesGroup.Controls.Add(this.FingerHintBox);
            this.PropertiesGroup.Controls.Add(this.FingerHintLabel);
            this.PropertiesGroup.Controls.Add(this.UniqueIdLabel);
            this.PropertiesGroup.Controls.Add(this.UniqueIdBox);
            this.PropertiesGroup.Controls.Add(this.TitleBox);
            this.PropertiesGroup.Controls.Add(this.RatingRangeLabel);
            this.PropertiesGroup.Controls.Add(this.SubtitleBox);
            this.PropertiesGroup.Controls.Add(this.DifficultyRangeLabel);
            this.PropertiesGroup.Controls.Add(this.ComposerBox);
            this.PropertiesGroup.Controls.Add(this.RemoveTag);
            this.PropertiesGroup.Controls.Add(this.ArrangerBox);
            this.PropertiesGroup.Controls.Add(this.AddTag);
            this.PropertiesGroup.Controls.Add(this.CopyrightBox);
            this.PropertiesGroup.Controls.Add(this.TagBox);
            this.PropertiesGroup.Controls.Add(this.LicenseBox);
            this.PropertiesGroup.Controls.Add(this.TagList);
            this.PropertiesGroup.Controls.Add(this.DifficultyBox);
            this.PropertiesGroup.Controls.Add(this.TagsLabel);
            this.PropertiesGroup.Controls.Add(this.RatingBox);
            this.PropertiesGroup.Controls.Add(this.RatingLabel);
            this.PropertiesGroup.Controls.Add(this.TitleLabel);
            this.PropertiesGroup.Controls.Add(this.DifficultyLabel);
            this.PropertiesGroup.Controls.Add(this.SubtitleLabel);
            this.PropertiesGroup.Controls.Add(this.LicenseLabel);
            this.PropertiesGroup.Controls.Add(this.ComposerLabel);
            this.PropertiesGroup.Controls.Add(this.CopyrightLabel);
            this.PropertiesGroup.Controls.Add(this.ArrangerLabel);
            this.PropertiesGroup.Location = new System.Drawing.Point(318, 32);
            this.PropertiesGroup.Name = "PropertiesGroup";
            this.PropertiesGroup.Size = new System.Drawing.Size(392, 495);
            this.PropertiesGroup.TabIndex = 5;
            this.PropertiesGroup.TabStop = false;
            this.PropertiesGroup.Text = "Properties";
            // 
            // FingerHintBox
            // 
            this.FingerHintBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FingerHintBox.Location = new System.Drawing.Point(70, 304);
            this.FingerHintBox.Multiline = true;
            this.FingerHintBox.Name = "FingerHintBox";
            this.FingerHintBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FingerHintBox.Size = new System.Drawing.Size(316, 44);
            this.FingerHintBox.TabIndex = 27;
            this.FingerHintBox.TextChanged += new System.EventHandler(this.FingerHintBox_TextChanged);
            // 
            // FingerHintLabel
            // 
            this.FingerHintLabel.AutoSize = true;
            this.FingerHintLabel.Location = new System.Drawing.Point(7, 307);
            this.FingerHintLabel.Name = "FingerHintLabel";
            this.FingerHintLabel.Size = new System.Drawing.Size(44, 13);
            this.FingerHintLabel.TabIndex = 26;
            this.FingerHintLabel.Text = "&Fingers:";
            // 
            // MetadataEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 539);
            this.Controls.Add(this.PropertiesGroup);
            this.Controls.Add(this.SongListLabel);
            this.Controls.Add(this.RemoveSong);
            this.Controls.Add(this.AddSong);
            this.Controls.Add(this.SongList);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(720, 500);
            this.Name = "MetadataEditor";
            this.Text = "[set in code]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MetadataEditor_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MetadataEditor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MetadataEditor_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MetadataEditor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DifficultyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RatingBox)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.PropertiesGroup.ResumeLayout(false);
            this.PropertiesGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox SongList;
        private System.Windows.Forms.Button AddSong;
        private System.Windows.Forms.Button RemoveSong;
        private System.Windows.Forms.TextBox UniqueIdBox;
        private System.Windows.Forms.TextBox TitleBox;
        private System.Windows.Forms.TextBox SubtitleBox;
        private System.Windows.Forms.TextBox ComposerBox;
        private System.Windows.Forms.TextBox LicenseBox;
        private System.Windows.Forms.TextBox CopyrightBox;
        private System.Windows.Forms.TextBox ArrangerBox;
        private System.Windows.Forms.NumericUpDown DifficultyBox;
        private System.Windows.Forms.NumericUpDown RatingBox;
        private System.Windows.Forms.Label UniqueIdLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label ComposerLabel;
        private System.Windows.Forms.Label SubtitleLabel;
        private System.Windows.Forms.Label DifficultyLabel;
        private System.Windows.Forms.Label LicenseLabel;
        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.Label ArrangerLabel;
        private System.Windows.Forms.Label TagsLabel;
        private System.Windows.Forms.Label RatingLabel;
        private System.Windows.Forms.ListBox TagList;
        private System.Windows.Forms.TextBox TagBox;
        private System.Windows.Forms.Button RemoveTag;
        private System.Windows.Forms.Button AddTag;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem NewMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveAsMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExitMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.ToolTip Tip;
        private System.Windows.Forms.Label DifficultyRangeLabel;
        private System.Windows.Forms.Label RatingRangeLabel;
        private System.Windows.Forms.SaveFileDialog SaveMetadataDialog;
        private System.Windows.Forms.OpenFileDialog OpenMetadataDialog;
        private System.Windows.Forms.OpenFileDialog OpenSongDialog;
        private System.Windows.Forms.Label SongListLabel;
        private System.Windows.Forms.GroupBox PropertiesGroup;
        private System.Windows.Forms.TextBox FingerHintBox;
        private System.Windows.Forms.Label FingerHintLabel;
        private System.Windows.Forms.ToolStripMenuItem ImportFingerHintsMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}

