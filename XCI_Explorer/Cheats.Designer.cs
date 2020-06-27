namespace XCI_Explorer.XCI_Explorer
{
    partial class Cheats
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cheats));
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip_ftp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fTPToSwitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_titleid = new System.Windows.Forms.TextBox();
            this.textBox_build = new System.Windows.Forms.TextBox();
            this.richTextBox_cheat = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.button_add = new System.Windows.Forms.Button();
            this.button_remove = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_create = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label_name = new System.Windows.Forms.Label();
            this.label_search = new System.Windows.Forms.Label();
            this.label_titleid = new System.Windows.Forms.Label();
            this.label_buildid = new System.Windows.Forms.Label();
            this.label_sort = new System.Windows.Forms.Label();
            this.richTextBox_notes = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Copy_toolStripMenuItem_Notes = new System.Windows.Forms.ToolStripMenuItem();
            this.Paste_toolStripMenuItem_Notes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.CSVFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cheatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxCheatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.werWolvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_clear = new System.Windows.Forms.Button();
            this.fTPRemoveCheatFromSwitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_ftp.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.Black;
            this.listView1.ContextMenuStrip = this.contextMenuStrip_ftp;
            this.listView1.ForeColor = System.Drawing.Color.Lime;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(5, 99);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(490, 170);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // contextMenuStrip_ftp
            // 
            this.contextMenuStrip_ftp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fTPToSwitchToolStripMenuItem,
            this.fTPRemoveCheatFromSwitchToolStripMenuItem});
            this.contextMenuStrip_ftp.Name = "contextMenuStrip_ftp";
            this.contextMenuStrip_ftp.Size = new System.Drawing.Size(236, 70);
            // 
            // fTPToSwitchToolStripMenuItem
            // 
            this.fTPToSwitchToolStripMenuItem.Name = "fTPToSwitchToolStripMenuItem";
            this.fTPToSwitchToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.fTPToSwitchToolStripMenuItem.Text = "FTP copy cheat to Switch";
            this.fTPToSwitchToolStripMenuItem.Click += new System.EventHandler(this.fTPToSwitchToolStripMenuItem_Click);
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(5, 39);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(310, 20);
            this.textBox_name.TabIndex = 1;
            // 
            // textBox_titleid
            // 
            this.textBox_titleid.Location = new System.Drawing.Point(5, 73);
            this.textBox_titleid.Name = "textBox_titleid";
            this.textBox_titleid.Size = new System.Drawing.Size(152, 20);
            this.textBox_titleid.TabIndex = 2;
            // 
            // textBox_build
            // 
            this.textBox_build.Location = new System.Drawing.Point(163, 73);
            this.textBox_build.Name = "textBox_build";
            this.textBox_build.Size = new System.Drawing.Size(152, 20);
            this.textBox_build.TabIndex = 3;
            // 
            // richTextBox_cheat
            // 
            this.richTextBox_cheat.AcceptsTab = true;
            this.richTextBox_cheat.BackColor = System.Drawing.Color.Black;
            this.richTextBox_cheat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_cheat.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox_cheat.DetectUrls = false;
            this.richTextBox_cheat.ForeColor = System.Drawing.Color.Lime;
            this.richTextBox_cheat.Location = new System.Drawing.Point(501, 99);
            this.richTextBox_cheat.Name = "richTextBox_cheat";
            this.richTextBox_cheat.Size = new System.Drawing.Size(206, 345);
            this.richTextBox_cheat.TabIndex = 4;
            this.richTextBox_cheat.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // textBox_search
            // 
            this.textBox_search.Location = new System.Drawing.Point(319, 39);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(176, 20);
            this.textBox_search.TabIndex = 5;
            this.textBox_search.TextChanged += new System.EventHandler(this.Search);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(501, 37);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(66, 23);
            this.button_add.TabIndex = 6;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_remove
            // 
            this.button_remove.Location = new System.Drawing.Point(641, 37);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(66, 23);
            this.button_remove.TabIndex = 7;
            this.button_remove.Text = "Remove";
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(571, 37);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(66, 23);
            this.button_update.TabIndex = 8;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // button_create
            // 
            this.button_create.Location = new System.Drawing.Point(607, 71);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(100, 23);
            this.button_create.TabIndex = 10;
            this.button_create.Text = "Cheat to File";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(319, 73);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(176, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(3, 26);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(35, 13);
            this.label_name.TabIndex = 12;
            this.label_name.Text = "Name";
            // 
            // label_search
            // 
            this.label_search.AutoSize = true;
            this.label_search.Location = new System.Drawing.Point(317, 26);
            this.label_search.Name = "label_search";
            this.label_search.Size = new System.Drawing.Size(41, 13);
            this.label_search.TabIndex = 13;
            this.label_search.Text = "Search";
            // 
            // label_titleid
            // 
            this.label_titleid.AutoSize = true;
            this.label_titleid.Location = new System.Drawing.Point(3, 60);
            this.label_titleid.Name = "label_titleid";
            this.label_titleid.Size = new System.Drawing.Size(41, 13);
            this.label_titleid.TabIndex = 14;
            this.label_titleid.Text = "Title ID";
            // 
            // label_buildid
            // 
            this.label_buildid.AutoSize = true;
            this.label_buildid.Location = new System.Drawing.Point(161, 60);
            this.label_buildid.Name = "label_buildid";
            this.label_buildid.Size = new System.Drawing.Size(44, 13);
            this.label_buildid.TabIndex = 15;
            this.label_buildid.Text = "Build ID";
            // 
            // label_sort
            // 
            this.label_sort.AutoSize = true;
            this.label_sort.Location = new System.Drawing.Point(317, 60);
            this.label_sort.Name = "label_sort";
            this.label_sort.Size = new System.Drawing.Size(26, 13);
            this.label_sort.TabIndex = 16;
            this.label_sort.Text = "Sort";
            // 
            // richTextBox_notes
            // 
            this.richTextBox_notes.BackColor = System.Drawing.Color.Black;
            this.richTextBox_notes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_notes.ContextMenuStrip = this.contextMenuStrip2;
            this.richTextBox_notes.ForeColor = System.Drawing.Color.Lime;
            this.richTextBox_notes.Location = new System.Drawing.Point(6, 274);
            this.richTextBox_notes.Name = "richTextBox_notes";
            this.richTextBox_notes.Size = new System.Drawing.Size(490, 170);
            this.richTextBox_notes.TabIndex = 17;
            this.richTextBox_notes.Text = "Notes:";
            this.richTextBox_notes.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_notes_LinkClicked);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Copy_toolStripMenuItem_Notes,
            this.Paste_toolStripMenuItem_Notes});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(103, 48);
            // 
            // Copy_toolStripMenuItem_Notes
            // 
            this.Copy_toolStripMenuItem_Notes.Name = "Copy_toolStripMenuItem_Notes";
            this.Copy_toolStripMenuItem_Notes.Size = new System.Drawing.Size(102, 22);
            this.Copy_toolStripMenuItem_Notes.Text = "Copy";
            this.Copy_toolStripMenuItem_Notes.Click += new System.EventHandler(this.Copy_toolStripMenuItem_Notes_Click);
            // 
            // Paste_toolStripMenuItem_Notes
            // 
            this.Paste_toolStripMenuItem_Notes.Name = "Paste_toolStripMenuItem_Notes";
            this.Paste_toolStripMenuItem_Notes.Size = new System.Drawing.Size(102, 22);
            this.Paste_toolStripMenuItem_Notes.Text = "Paste";
            this.Paste_toolStripMenuItem_Notes.Click += new System.EventHandler(this.Paste_toolStripMenuItem_Notes_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CSVFilesToolStripMenuItem,
            this.cheatsToolStripMenuItem,
            this.exploreToolStripMenuItem,
            this.fTPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(713, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // CSVFilesToolStripMenuItem
            // 
            this.CSVFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.CSVFilesToolStripMenuItem.Name = "CSVFilesToolStripMenuItem";
            this.CSVFilesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.CSVFilesToolStripMenuItem.Text = "CSV Files";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // cheatsToolStripMenuItem
            // 
            this.cheatsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maxCheatsToolStripMenuItem,
            this.werWolvToolStripMenuItem});
            this.cheatsToolStripMenuItem.Name = "cheatsToolStripMenuItem";
            this.cheatsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.cheatsToolStripMenuItem.Text = "Cheats";
            // 
            // maxCheatsToolStripMenuItem
            // 
            this.maxCheatsToolStripMenuItem.Name = "maxCheatsToolStripMenuItem";
            this.maxCheatsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.maxCheatsToolStripMenuItem.Text = "MaxCheats";
            this.maxCheatsToolStripMenuItem.Click += new System.EventHandler(this.maxCheatsToolStripMenuItem_Click);
            // 
            // werWolvToolStripMenuItem
            // 
            this.werWolvToolStripMenuItem.Name = "werWolvToolStripMenuItem";
            this.werWolvToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.werWolvToolStripMenuItem.Text = "WerWolv";
            this.werWolvToolStripMenuItem.Click += new System.EventHandler(this.werWolvToolStripMenuItem_Click);
            // 
            // exploreToolStripMenuItem
            // 
            this.exploreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.binFolderToolStripMenuItem});
            this.exploreToolStripMenuItem.Name = "exploreToolStripMenuItem";
            this.exploreToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.exploreToolStripMenuItem.Text = "Explore";
            // 
            // binFolderToolStripMenuItem
            // 
            this.binFolderToolStripMenuItem.Name = "binFolderToolStripMenuItem";
            this.binFolderToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.binFolderToolStripMenuItem.Text = "Bin Folder";
            this.binFolderToolStripMenuItem.Click += new System.EventHandler(this.binFolderToolStripMenuItem_Click);
            // 
            // fTPToolStripMenuItem
            // 
            this.fTPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.fTPToolStripMenuItem.Name = "fTPToolStripMenuItem";
            this.fTPToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fTPToolStripMenuItem.Text = "FTP";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(501, 71);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(100, 23);
            this.button_clear.TabIndex = 19;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click);
            // 
            // fTPRemoveCheatFromSwitchToolStripMenuItem
            // 
            this.fTPRemoveCheatFromSwitchToolStripMenuItem.Name = "fTPRemoveCheatFromSwitchToolStripMenuItem";
            this.fTPRemoveCheatFromSwitchToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.fTPRemoveCheatFromSwitchToolStripMenuItem.Text = "FTP remove cheat from Switch";
            this.fTPRemoveCheatFromSwitchToolStripMenuItem.Click += new System.EventHandler(this.fTPRemoveCheatFromSwitchToolStripMenuItem_Click);
            // 
            // Cheats
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(713, 449);
            this.Controls.Add(this.button_clear);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.richTextBox_notes);
            this.Controls.Add(this.label_sort);
            this.Controls.Add(this.label_buildid);
            this.Controls.Add(this.label_titleid);
            this.Controls.Add(this.label_search);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.textBox_search);
            this.Controls.Add(this.richTextBox_cheat);
            this.Controls.Add(this.textBox_build);
            this.Controls.Add(this.textBox_titleid);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Cheats";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cheat Database";
            this.contextMenuStrip_ftp.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_titleid;
        private System.Windows.Forms.TextBox textBox_build;
        private System.Windows.Forms.RichTextBox richTextBox_cheat;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_create;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_search;
        private System.Windows.Forms.Label label_titleid;
        private System.Windows.Forms.Label label_buildid;
        private System.Windows.Forms.Label label_sort;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox_notes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem Copy_toolStripMenuItem_Notes;
        private System.Windows.Forms.ToolStripMenuItem Paste_toolStripMenuItem_Notes;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CSVFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cheatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maxCheatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem werWolvToolStripMenuItem;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.ToolStripMenuItem exploreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fTPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_ftp;
        private System.Windows.Forms.ToolStripMenuItem fTPToSwitchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fTPRemoveCheatFromSwitchToolStripMenuItem;
    }
}