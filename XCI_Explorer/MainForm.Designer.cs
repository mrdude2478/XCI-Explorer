using System.ComponentModel;
using System.Windows.Forms;
using XCI_Explorer.Helpers;

namespace XCI_Explorer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TB_File = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PB_GameIcon = new System.Windows.Forms.PictureBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Keys = new System.Windows.Forms.TabPage();
            this.sortkey = new System.Windows.Forms.Button();
            this.WriteKeys = new System.Windows.Forms.Button();
            this.ShowKeys = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Partitions = new System.Windows.Forms.TabPage();
            this.LB_HashedRegionSize = new System.Windows.Forms.Label();
            this.LB_ActualHash = new System.Windows.Forms.Label();
            this.LB_ExpectedHash = new System.Windows.Forms.Label();
            this.B_Extract = new System.Windows.Forms.Button();
            this.LB_DataSize = new System.Windows.Forms.Label();
            this.LB_DataOffset = new System.Windows.Forms.Label();
            this.LB_SelectedData = new System.Windows.Forms.Label();
            this.TV_Partitions = new System.Windows.Forms.TreeView();
            this.Main = new System.Windows.Forms.TabPage();
            this.button_add_DB = new System.Windows.Forms.Button();
            this.ShowMD5 = new System.Windows.Forms.Button();
            this.Rename = new System.Windows.Forms.Button();
            this.B_TrimXCI = new System.Windows.Forms.Button();
            this.TB_Capacity = new System.Windows.Forms.TextBox();
            this.TB_SDKVer = new System.Windows.Forms.TextBox();
            this.TB_ProdCode = new System.Windows.Forms.TextBox();
            this.TB_Dev = new System.Windows.Forms.TextBox();
            this.TB_TID = new System.Windows.Forms.TextBox();
            this.TB_Name = new System.Windows.Forms.TextBox();
            this.TB_GameRev = new System.Windows.Forms.TextBox();
            this.TB_ExactUsedSpace = new System.Windows.Forms.TextBox();
            this.TB_ROMExactSize = new System.Windows.Forms.TextBox();
            this.TB_UsedSpace = new System.Windows.Forms.TextBox();
            this.TB_ROMSize = new System.Windows.Forms.TextBox();
            this.TB_MKeyRev = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.CB_RegionName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Base64 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.base64RTB = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.nxci = new System.Windows.Forms.TabPage();
            this.keepncaid = new System.Windows.Forms.CheckBox();
            this.usetmp = new System.Windows.Forms.CheckBox();
            this.titnamecheck = new System.Windows.Forms.CheckBox();
            this.outputstring = new System.Windows.Forms.TextBox();
            this.OutputDir = new System.Windows.Forms.Button();
            this.consolebox = new System.Windows.Forms.RichTextBox();
            this.Convert = new System.Windows.Forms.Button();
            this.ReKey = new System.Windows.Forms.TabPage();
            this.usetmp2 = new System.Windows.Forms.CheckBox();
            this.OutputDir2 = new System.Windows.Forms.Button();
            this.Convert2 = new System.Windows.Forms.Button();
            this.outputstring2 = new System.Windows.Forms.TextBox();
            this.consolebox2 = new System.Windows.Forms.RichTextBox();
            this.Spliter = new System.Windows.Forms.TabPage();
            this.checkBox_sxos = new System.Windows.Forms.CheckBox();
            this.richTextBox_split = new System.Windows.Forms.RichTextBox();
            this.Split = new System.Windows.Forms.Button();
            this.outputstring3 = new System.Windows.Forms.TextBox();
            this.OutputSpilt = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchGoogleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.youtubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nintendoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sAKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fat32FormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aESToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cheatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.binToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_GameIcon)).BeginInit();
            this.Keys.SuspendLayout();
            this.Partitions.SuspendLayout();
            this.Main.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Base64.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.nxci.SuspendLayout();
            this.ReKey.SuspendLayout();
            this.Spliter.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_File
            // 
            this.TB_File.AllowDrop = true;
            this.TB_File.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TB_File.BackColor = System.Drawing.SystemColors.Control;
            this.TB_File.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TB_File.CausesValidation = false;
            this.TB_File.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_File.Location = new System.Drawing.Point(3, 328);
            this.TB_File.Margin = new System.Windows.Forms.Padding(0);
            this.TB_File.Name = "TB_File";
            this.TB_File.ReadOnly = true;
            this.TB_File.ShortcutsEnabled = false;
            this.TB_File.Size = new System.Drawing.Size(602, 22);
            this.TB_File.TabIndex = 1;
            this.TB_File.TabStop = false;
            this.toolTip1.SetToolTip(this.TB_File, "You can also drag a game on to this box to load it.");
            this.TB_File.WordWrap = false;
            this.TB_File.DragDrop += new System.Windows.Forms.DragEventHandler(this.TB_File_DragDrop);
            this.TB_File.DragEnter += new System.Windows.Forms.DragEventHandler(this.TB_File_DragEnter);
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
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // PB_GameIcon
            // 
            this.PB_GameIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PB_GameIcon.BackColor = System.Drawing.Color.Transparent;
            this.PB_GameIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PB_GameIcon.BackgroundImage")));
            this.PB_GameIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PB_GameIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PB_GameIcon.Location = new System.Drawing.Point(323, 5);
            this.PB_GameIcon.Margin = new System.Windows.Forms.Padding(0);
            this.PB_GameIcon.Name = "PB_GameIcon";
            this.PB_GameIcon.Padding = new System.Windows.Forms.Padding(20);
            this.PB_GameIcon.Size = new System.Drawing.Size(264, 263);
            this.PB_GameIcon.TabIndex = 29;
            this.PB_GameIcon.TabStop = false;
            this.toolTip1.SetToolTip(this.PB_GameIcon, "Save this XCI or NSP icon to a png file");
            this.PB_GameIcon.Click += new System.EventHandler(this.PB_GameIcon_Click);
            this.PB_GameIcon.DragDrop += new System.Windows.Forms.DragEventHandler(this.TB_File_DragDrop);
            this.PB_GameIcon.DragEnter += new System.Windows.Forms.DragEventHandler(this.TB_File_DragEnter);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.Transparent;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox2.Location = new System.Drawing.Point(302, 115);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(12, 11);
            this.checkBox2.TabIndex = 27;
            this.toolTip1.SetToolTip(this.checkBox2, "Include Game Revision when you rename an XCI or NSP file");
            this.checkBox2.UseVisualStyleBackColor = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Location = new System.Drawing.Point(302, 94);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(12, 11);
            this.checkBox1.TabIndex = 26;
            this.toolTip1.SetToolTip(this.checkBox1, "Include Title ID when you rename an XCI or NSP file");
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // Keys
            // 
            this.Keys.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Keys.Controls.Add(this.sortkey);
            this.Keys.Controls.Add(this.WriteKeys);
            this.Keys.Controls.Add(this.ShowKeys);
            this.Keys.Controls.Add(this.richTextBox1);
            this.Keys.Location = new System.Drawing.Point(4, 22);
            this.Keys.Name = "Keys";
            this.Keys.Size = new System.Drawing.Size(592, 273);
            this.Keys.TabIndex = 2;
            this.Keys.Text = "Keys";
            // 
            // sortkey
            // 
            this.sortkey.Location = new System.Drawing.Point(78, 248);
            this.sortkey.Name = "sortkey";
            this.sortkey.Size = new System.Drawing.Size(75, 23);
            this.sortkey.TabIndex = 3;
            this.sortkey.Text = "Sort Keys";
            this.sortkey.UseVisualStyleBackColor = true;
            this.sortkey.Click += new System.EventHandler(this.SortKeys_Click);
            // 
            // WriteKeys
            // 
            this.WriteKeys.Location = new System.Drawing.Point(515, 248);
            this.WriteKeys.Name = "WriteKeys";
            this.WriteKeys.Size = new System.Drawing.Size(75, 23);
            this.WriteKeys.TabIndex = 2;
            this.WriteKeys.Text = "Write Keys";
            this.WriteKeys.UseVisualStyleBackColor = true;
            this.WriteKeys.Click += new System.EventHandler(this.WriteKeys_Click);
            // 
            // ShowKeys
            // 
            this.ShowKeys.Location = new System.Drawing.Point(2, 248);
            this.ShowKeys.Name = "ShowKeys";
            this.ShowKeys.Size = new System.Drawing.Size(75, 23);
            this.ShowKeys.TabIndex = 1;
            this.ShowKeys.Text = "Reload Keys";
            this.ShowKeys.UseVisualStyleBackColor = true;
            this.ShowKeys.Click += new System.EventHandler(this.ShowKeys_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.Lime;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(586, 243);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // Partitions
            // 
            this.Partitions.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Partitions.Controls.Add(this.LB_HashedRegionSize);
            this.Partitions.Controls.Add(this.LB_ActualHash);
            this.Partitions.Controls.Add(this.LB_ExpectedHash);
            this.Partitions.Controls.Add(this.B_Extract);
            this.Partitions.Controls.Add(this.LB_DataSize);
            this.Partitions.Controls.Add(this.LB_DataOffset);
            this.Partitions.Controls.Add(this.LB_SelectedData);
            this.Partitions.Controls.Add(this.TV_Partitions);
            this.Partitions.Location = new System.Drawing.Point(4, 22);
            this.Partitions.Name = "Partitions";
            this.Partitions.Padding = new System.Windows.Forms.Padding(3);
            this.Partitions.Size = new System.Drawing.Size(592, 273);
            this.Partitions.TabIndex = 1;
            this.Partitions.Text = "Partitions";
            // 
            // LB_HashedRegionSize
            // 
            this.LB_HashedRegionSize.AutoSize = true;
            this.LB_HashedRegionSize.Location = new System.Drawing.Point(0, 232);
            this.LB_HashedRegionSize.Name = "LB_HashedRegionSize";
            this.LB_HashedRegionSize.Size = new System.Drawing.Size(101, 13);
            this.LB_HashedRegionSize.TabIndex = 7;
            this.LB_HashedRegionSize.Text = "HashedRegionSize:";
            // 
            // LB_ActualHash
            // 
            this.LB_ActualHash.AutoSize = true;
            this.LB_ActualHash.Location = new System.Drawing.Point(0, 259);
            this.LB_ActualHash.Name = "LB_ActualHash";
            this.LB_ActualHash.Size = new System.Drawing.Size(68, 13);
            this.LB_ActualHash.TabIndex = 6;
            this.LB_ActualHash.Text = "Actual Hash:";
            this.LB_ActualHash.DoubleClick += new System.EventHandler(this.LB_ActualHash_DoubleClick);
            // 
            // LB_ExpectedHash
            // 
            this.LB_ExpectedHash.AutoSize = true;
            this.LB_ExpectedHash.Location = new System.Drawing.Point(0, 246);
            this.LB_ExpectedHash.Name = "LB_ExpectedHash";
            this.LB_ExpectedHash.Size = new System.Drawing.Size(73, 13);
            this.LB_ExpectedHash.TabIndex = 5;
            this.LB_ExpectedHash.Text = "Header Hash:";
            this.LB_ExpectedHash.DoubleClick += new System.EventHandler(this.LB_ExpectedHash_DoubleClick);
            // 
            // B_Extract
            // 
            this.B_Extract.Enabled = false;
            this.B_Extract.Location = new System.Drawing.Point(542, 248);
            this.B_Extract.Name = "B_Extract";
            this.B_Extract.Size = new System.Drawing.Size(48, 23);
            this.B_Extract.TabIndex = 4;
            this.B_Extract.Text = "Extract";
            this.B_Extract.UseVisualStyleBackColor = true;
            this.B_Extract.Click += new System.EventHandler(this.B_Extract_Click);
            // 
            // LB_DataSize
            // 
            this.LB_DataSize.AutoSize = true;
            this.LB_DataSize.Location = new System.Drawing.Point(0, 219);
            this.LB_DataSize.Name = "LB_DataSize";
            this.LB_DataSize.Size = new System.Drawing.Size(30, 13);
            this.LB_DataSize.TabIndex = 3;
            this.LB_DataSize.Text = "Size:";
            // 
            // LB_DataOffset
            // 
            this.LB_DataOffset.AutoSize = true;
            this.LB_DataOffset.Location = new System.Drawing.Point(1, 206);
            this.LB_DataOffset.Name = "LB_DataOffset";
            this.LB_DataOffset.Size = new System.Drawing.Size(38, 13);
            this.LB_DataOffset.TabIndex = 2;
            this.LB_DataOffset.Text = "Offset:";
            // 
            // LB_SelectedData
            // 
            this.LB_SelectedData.AutoSize = true;
            this.LB_SelectedData.Location = new System.Drawing.Point(0, 193);
            this.LB_SelectedData.Name = "LB_SelectedData";
            this.LB_SelectedData.Size = new System.Drawing.Size(54, 13);
            this.LB_SelectedData.TabIndex = 1;
            this.LB_SelectedData.Text = "FileName:";
            // 
            // TV_Partitions
            // 
            this.TV_Partitions.BackColor = System.Drawing.Color.Black;
            this.TV_Partitions.Dock = System.Windows.Forms.DockStyle.Top;
            this.TV_Partitions.ForeColor = System.Drawing.Color.Lime;
            this.TV_Partitions.HideSelection = false;
            this.TV_Partitions.LineColor = System.Drawing.Color.Lime;
            this.TV_Partitions.Location = new System.Drawing.Point(3, 3);
            this.TV_Partitions.Name = "TV_Partitions";
            this.TV_Partitions.Size = new System.Drawing.Size(586, 190);
            this.TV_Partitions.TabIndex = 0;
            this.TV_Partitions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_Partitions_AfterSelect);
            // 
            // Main
            // 
            this.Main.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Main.Controls.Add(this.button_add_DB);
            this.Main.Controls.Add(this.PB_GameIcon);
            this.Main.Controls.Add(this.ShowMD5);
            this.Main.Controls.Add(this.Rename);
            this.Main.Controls.Add(this.checkBox2);
            this.Main.Controls.Add(this.checkBox1);
            this.Main.Controls.Add(this.B_TrimXCI);
            this.Main.Controls.Add(this.TB_Capacity);
            this.Main.Controls.Add(this.TB_SDKVer);
            this.Main.Controls.Add(this.TB_ProdCode);
            this.Main.Controls.Add(this.TB_Dev);
            this.Main.Controls.Add(this.TB_TID);
            this.Main.Controls.Add(this.TB_Name);
            this.Main.Controls.Add(this.TB_GameRev);
            this.Main.Controls.Add(this.TB_ExactUsedSpace);
            this.Main.Controls.Add(this.TB_ROMExactSize);
            this.Main.Controls.Add(this.TB_UsedSpace);
            this.Main.Controls.Add(this.TB_ROMSize);
            this.Main.Controls.Add(this.TB_MKeyRev);
            this.Main.Controls.Add(this.label3);
            this.Main.Controls.Add(this.label2);
            this.Main.Controls.Add(this.label8);
            this.Main.Controls.Add(this.label11);
            this.Main.Controls.Add(this.label10);
            this.Main.Controls.Add(this.CB_RegionName);
            this.Main.Controls.Add(this.label1);
            this.Main.Controls.Add(this.label9);
            this.Main.Controls.Add(this.label7);
            this.Main.Controls.Add(this.label6);
            this.Main.Controls.Add(this.label5);
            this.Main.Controls.Add(this.label4);
            this.Main.Location = new System.Drawing.Point(4, 22);
            this.Main.Margin = new System.Windows.Forms.Padding(0);
            this.Main.Name = "Main";
            this.Main.Size = new System.Drawing.Size(592, 273);
            this.Main.TabIndex = 0;
            this.Main.Text = "Main";
            // 
            // button_add_DB
            // 
            this.button_add_DB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_add_DB.Enabled = false;
            this.button_add_DB.Location = new System.Drawing.Point(165, 239);
            this.button_add_DB.Name = "button_add_DB";
            this.button_add_DB.Size = new System.Drawing.Size(75, 29);
            this.button_add_DB.TabIndex = 30;
            this.button_add_DB.Text = "Add to DB";
            this.button_add_DB.UseVisualStyleBackColor = true;
            this.button_add_DB.Click += new System.EventHandler(this.button_add_DB_Click);
            // 
            // ShowMD5
            // 
            this.ShowMD5.BackColor = System.Drawing.Color.Transparent;
            this.ShowMD5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ShowMD5.Enabled = false;
            this.ShowMD5.Location = new System.Drawing.Point(86, 239);
            this.ShowMD5.Name = "ShowMD5";
            this.ShowMD5.Size = new System.Drawing.Size(75, 29);
            this.ShowMD5.TabIndex = 28;
            this.ShowMD5.Text = "Show MD5";
            this.ShowMD5.UseVisualStyleBackColor = false;
            this.ShowMD5.Click += new System.EventHandler(this.ShowMD5_Click);
            // 
            // Rename
            // 
            this.Rename.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Rename.Enabled = false;
            this.Rename.Location = new System.Drawing.Point(244, 239);
            this.Rename.Name = "Rename";
            this.Rename.Size = new System.Drawing.Size(75, 29);
            this.Rename.TabIndex = 23;
            this.Rename.Text = "Ren Game";
            this.Rename.UseVisualStyleBackColor = true;
            this.Rename.Click += new System.EventHandler(this.RenameGame_Click);
            // 
            // B_TrimXCI
            // 
            this.B_TrimXCI.BackColor = System.Drawing.Color.Transparent;
            this.B_TrimXCI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_TrimXCI.Enabled = false;
            this.B_TrimXCI.Location = new System.Drawing.Point(7, 239);
            this.B_TrimXCI.Name = "B_TrimXCI";
            this.B_TrimXCI.Size = new System.Drawing.Size(75, 29);
            this.B_TrimXCI.TabIndex = 21;
            this.B_TrimXCI.Text = "Trim XCI";
            this.B_TrimXCI.UseVisualStyleBackColor = false;
            this.B_TrimXCI.Click += new System.EventHandler(this.B_TrimXCI_Click);
            // 
            // TB_Capacity
            // 
            this.TB_Capacity.Location = new System.Drawing.Point(77, 173);
            this.TB_Capacity.Name = "TB_Capacity";
            this.TB_Capacity.ReadOnly = true;
            this.TB_Capacity.Size = new System.Drawing.Size(242, 20);
            this.TB_Capacity.TabIndex = 3;
            // 
            // TB_SDKVer
            // 
            this.TB_SDKVer.Location = new System.Drawing.Point(77, 194);
            this.TB_SDKVer.Name = "TB_SDKVer";
            this.TB_SDKVer.ReadOnly = true;
            this.TB_SDKVer.Size = new System.Drawing.Size(242, 20);
            this.TB_SDKVer.TabIndex = 5;
            // 
            // TB_ProdCode
            // 
            this.TB_ProdCode.Location = new System.Drawing.Point(77, 152);
            this.TB_ProdCode.Name = "TB_ProdCode";
            this.TB_ProdCode.ReadOnly = true;
            this.TB_ProdCode.Size = new System.Drawing.Size(242, 20);
            this.TB_ProdCode.TabIndex = 20;
            // 
            // TB_Dev
            // 
            this.TB_Dev.Location = new System.Drawing.Point(77, 68);
            this.TB_Dev.Name = "TB_Dev";
            this.TB_Dev.ReadOnly = true;
            this.TB_Dev.Size = new System.Drawing.Size(242, 20);
            this.TB_Dev.TabIndex = 24;
            // 
            // TB_TID
            // 
            this.TB_TID.Location = new System.Drawing.Point(77, 89);
            this.TB_TID.Name = "TB_TID";
            this.TB_TID.ReadOnly = true;
            this.TB_TID.Size = new System.Drawing.Size(242, 20);
            this.TB_TID.TabIndex = 0;
            // 
            // TB_Name
            // 
            this.TB_Name.Location = new System.Drawing.Point(77, 5);
            this.TB_Name.Name = "TB_Name";
            this.TB_Name.Size = new System.Drawing.Size(242, 20);
            this.TB_Name.TabIndex = 22;
            // 
            // TB_GameRev
            // 
            this.TB_GameRev.Location = new System.Drawing.Point(77, 110);
            this.TB_GameRev.Name = "TB_GameRev";
            this.TB_GameRev.ReadOnly = true;
            this.TB_GameRev.Size = new System.Drawing.Size(242, 20);
            this.TB_GameRev.TabIndex = 16;
            // 
            // TB_ExactUsedSpace
            // 
            this.TB_ExactUsedSpace.Location = new System.Drawing.Point(151, 47);
            this.TB_ExactUsedSpace.Name = "TB_ExactUsedSpace";
            this.TB_ExactUsedSpace.ReadOnly = true;
            this.TB_ExactUsedSpace.Size = new System.Drawing.Size(168, 20);
            this.TB_ExactUsedSpace.TabIndex = 13;
            // 
            // TB_ROMExactSize
            // 
            this.TB_ROMExactSize.Location = new System.Drawing.Point(151, 26);
            this.TB_ROMExactSize.Name = "TB_ROMExactSize";
            this.TB_ROMExactSize.ReadOnly = true;
            this.TB_ROMExactSize.Size = new System.Drawing.Size(168, 20);
            this.TB_ROMExactSize.TabIndex = 12;
            // 
            // TB_UsedSpace
            // 
            this.TB_UsedSpace.Location = new System.Drawing.Point(77, 47);
            this.TB_UsedSpace.Name = "TB_UsedSpace";
            this.TB_UsedSpace.ReadOnly = true;
            this.TB_UsedSpace.Size = new System.Drawing.Size(73, 20);
            this.TB_UsedSpace.TabIndex = 11;
            // 
            // TB_ROMSize
            // 
            this.TB_ROMSize.Location = new System.Drawing.Point(77, 26);
            this.TB_ROMSize.Name = "TB_ROMSize";
            this.TB_ROMSize.ReadOnly = true;
            this.TB_ROMSize.Size = new System.Drawing.Size(73, 20);
            this.TB_ROMSize.TabIndex = 10;
            // 
            // TB_MKeyRev
            // 
            this.TB_MKeyRev.Location = new System.Drawing.Point(77, 131);
            this.TB_MKeyRev.Name = "TB_MKeyRev";
            this.TB_MKeyRev.ReadOnly = true;
            this.TB_MKeyRev.Size = new System.Drawing.Size(242, 20);
            this.TB_MKeyRev.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "SDK Rev:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Capacity:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Prod Code:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 218);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Language:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Developer:";
            // 
            // CB_RegionName
            // 
            this.CB_RegionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_RegionName.FormattingEnabled = true;
            this.CB_RegionName.Location = new System.Drawing.Point(77, 215);
            this.CB_RegionName.Name = "CB_RegionName";
            this.CB_RegionName.Size = new System.Drawing.Size(242, 21);
            this.CB_RegionName.TabIndex = 17;
            this.CB_RegionName.SelectedIndexChanged += new System.EventHandler(this.CB_RegionName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title ID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Game Rev:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Used Space:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "ROM Size:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "MKey Rev:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabControl1.Controls.Add(this.Main);
            this.tabControl1.Controls.Add(this.Partitions);
            this.tabControl1.Controls.Add(this.Keys);
            this.tabControl1.Controls.Add(this.Base64);
            this.tabControl1.Controls.Add(this.nxci);
            this.tabControl1.Controls.Add(this.ReKey);
            this.tabControl1.Controls.Add(this.Spliter);
            this.tabControl1.Location = new System.Drawing.Point(4, 29);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(600, 299);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Base64
            // 
            this.Base64.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Base64.Controls.Add(this.button3);
            this.Base64.Controls.Add(this.base64RTB);
            this.Base64.Controls.Add(this.button2);
            this.Base64.Controls.Add(this.button1);
            this.Base64.Location = new System.Drawing.Point(4, 22);
            this.Base64.Name = "Base64";
            this.Base64.Padding = new System.Windows.Forms.Padding(3);
            this.Base64.Size = new System.Drawing.Size(592, 273);
            this.Base64.TabIndex = 3;
            this.Base64.Text = "Base64";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(162, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // base64RTB
            // 
            this.base64RTB.BackColor = System.Drawing.Color.White;
            this.base64RTB.ContextMenuStrip = this.contextMenuStrip3;
            this.base64RTB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.base64RTB.ForeColor = System.Drawing.Color.Black;
            this.base64RTB.Location = new System.Drawing.Point(5, 37);
            this.base64RTB.Name = "base64RTB";
            this.base64RTB.Size = new System.Drawing.Size(581, 231);
            this.base64RTB.TabIndex = 2;
            this.base64RTB.Text = "";
            this.base64RTB.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.base64RTB_LinkClicked);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip3.Name = "contextMenuStrip1";
            this.contextMenuStrip3.Size = new System.Drawing.Size(103, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem1.Text = "Copy";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem2.Text = "Paste";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(84, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Encode";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Decode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // nxci
            // 
            this.nxci.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.nxci.Controls.Add(this.keepncaid);
            this.nxci.Controls.Add(this.usetmp);
            this.nxci.Controls.Add(this.titnamecheck);
            this.nxci.Controls.Add(this.outputstring);
            this.nxci.Controls.Add(this.OutputDir);
            this.nxci.Controls.Add(this.consolebox);
            this.nxci.Controls.Add(this.Convert);
            this.nxci.Location = new System.Drawing.Point(4, 22);
            this.nxci.Name = "nxci";
            this.nxci.Size = new System.Drawing.Size(592, 273);
            this.nxci.TabIndex = 4;
            this.nxci.Text = "4nxci";
            // 
            // keepncaid
            // 
            this.keepncaid.AutoSize = true;
            this.keepncaid.Location = new System.Drawing.Point(391, 8);
            this.keepncaid.Name = "keepncaid";
            this.keepncaid.Size = new System.Drawing.Size(80, 17);
            this.keepncaid.TabIndex = 6;
            this.keepncaid.Text = "Keep ncaid";
            this.keepncaid.UseVisualStyleBackColor = true;
            // 
            // usetmp
            // 
            this.usetmp.AutoSize = true;
            this.usetmp.Checked = true;
            this.usetmp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.usetmp.Location = new System.Drawing.Point(474, 8);
            this.usetmp.Name = "usetmp";
            this.usetmp.Size = new System.Drawing.Size(118, 17);
            this.usetmp.TabIndex = 5;
            this.usetmp.Text = "Output Dir as Temp";
            this.usetmp.UseVisualStyleBackColor = true;
            // 
            // titnamecheck
            // 
            this.titnamecheck.AutoSize = true;
            this.titnamecheck.Checked = true;
            this.titnamecheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.titnamecheck.Location = new System.Drawing.Point(292, 8);
            this.titnamecheck.Name = "titnamecheck";
            this.titnamecheck.Size = new System.Drawing.Size(96, 17);
            this.titnamecheck.TabIndex = 4;
            this.titnamecheck.Text = "Use TitleName";
            this.titnamecheck.UseVisualStyleBackColor = true;
            // 
            // outputstring
            // 
            this.outputstring.Location = new System.Drawing.Point(5, 30);
            this.outputstring.Name = "outputstring";
            this.outputstring.Size = new System.Drawing.Size(582, 20);
            this.outputstring.TabIndex = 3;
            // 
            // OutputDir
            // 
            this.OutputDir.Location = new System.Drawing.Point(83, 4);
            this.OutputDir.Name = "OutputDir";
            this.OutputDir.Size = new System.Drawing.Size(75, 23);
            this.OutputDir.TabIndex = 2;
            this.OutputDir.Text = "Output Dir";
            this.OutputDir.UseVisualStyleBackColor = true;
            this.OutputDir.Click += new System.EventHandler(this.OutputDir_Click);
            // 
            // consolebox
            // 
            this.consolebox.BackColor = System.Drawing.Color.Black;
            this.consolebox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consolebox.ForeColor = System.Drawing.Color.Lime;
            this.consolebox.Location = new System.Drawing.Point(5, 54);
            this.consolebox.Name = "consolebox";
            this.consolebox.ReadOnly = true;
            this.consolebox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.consolebox.Size = new System.Drawing.Size(582, 213);
            this.consolebox.TabIndex = 1;
            this.consolebox.Text = "";
            this.consolebox.WordWrap = false;
            this.consolebox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.consolebox_LinkClicked);
            // 
            // Convert
            // 
            this.Convert.Location = new System.Drawing.Point(5, 4);
            this.Convert.Name = "Convert";
            this.Convert.Size = new System.Drawing.Size(75, 23);
            this.Convert.TabIndex = 0;
            this.Convert.Text = "Create NSP";
            this.Convert.UseVisualStyleBackColor = true;
            this.Convert.Click += new System.EventHandler(this.button4_Click);
            // 
            // ReKey
            // 
            this.ReKey.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ReKey.Controls.Add(this.usetmp2);
            this.ReKey.Controls.Add(this.OutputDir2);
            this.ReKey.Controls.Add(this.Convert2);
            this.ReKey.Controls.Add(this.outputstring2);
            this.ReKey.Controls.Add(this.consolebox2);
            this.ReKey.Location = new System.Drawing.Point(4, 22);
            this.ReKey.Name = "ReKey";
            this.ReKey.Size = new System.Drawing.Size(592, 273);
            this.ReKey.TabIndex = 5;
            this.ReKey.Text = "RenXpack";
            // 
            // usetmp2
            // 
            this.usetmp2.AutoSize = true;
            this.usetmp2.Checked = true;
            this.usetmp2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.usetmp2.Location = new System.Drawing.Point(474, 8);
            this.usetmp2.Name = "usetmp2";
            this.usetmp2.Size = new System.Drawing.Size(118, 17);
            this.usetmp2.TabIndex = 7;
            this.usetmp2.Text = "Output Dir as Temp";
            this.usetmp2.UseVisualStyleBackColor = true;
            // 
            // OutputDir2
            // 
            this.OutputDir2.Location = new System.Drawing.Point(83, 4);
            this.OutputDir2.Name = "OutputDir2";
            this.OutputDir2.Size = new System.Drawing.Size(75, 23);
            this.OutputDir2.TabIndex = 6;
            this.OutputDir2.Text = "Output Dir";
            this.OutputDir2.UseVisualStyleBackColor = true;
            this.OutputDir2.Click += new System.EventHandler(this.OutputDir2_Click);
            // 
            // Convert2
            // 
            this.Convert2.Location = new System.Drawing.Point(5, 4);
            this.Convert2.Name = "Convert2";
            this.Convert2.Size = new System.Drawing.Size(75, 23);
            this.Convert2.TabIndex = 5;
            this.Convert2.Text = "Create NSP";
            this.Convert2.UseVisualStyleBackColor = true;
            this.Convert2.Click += new System.EventHandler(this.Convert2_Click);
            // 
            // outputstring2
            // 
            this.outputstring2.Location = new System.Drawing.Point(5, 30);
            this.outputstring2.Name = "outputstring2";
            this.outputstring2.Size = new System.Drawing.Size(582, 20);
            this.outputstring2.TabIndex = 4;
            // 
            // consolebox2
            // 
            this.consolebox2.BackColor = System.Drawing.Color.Black;
            this.consolebox2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consolebox2.ForeColor = System.Drawing.Color.Lime;
            this.consolebox2.Location = new System.Drawing.Point(5, 54);
            this.consolebox2.Name = "consolebox2";
            this.consolebox2.ReadOnly = true;
            this.consolebox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.consolebox2.Size = new System.Drawing.Size(582, 213);
            this.consolebox2.TabIndex = 2;
            this.consolebox2.Text = "";
            this.consolebox2.WordWrap = false;
            this.consolebox2.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.consolebox2_LinkClicked);
            // 
            // Spliter
            // 
            this.Spliter.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Spliter.Controls.Add(this.checkBox_sxos);
            this.Spliter.Controls.Add(this.richTextBox_split);
            this.Spliter.Controls.Add(this.Split);
            this.Spliter.Controls.Add(this.outputstring3);
            this.Spliter.Controls.Add(this.OutputSpilt);
            this.Spliter.Location = new System.Drawing.Point(4, 22);
            this.Spliter.Name = "Spliter";
            this.Spliter.Size = new System.Drawing.Size(592, 273);
            this.Spliter.TabIndex = 6;
            this.Spliter.Text = "File Splitter";
            // 
            // checkBox_sxos
            // 
            this.checkBox_sxos.AutoSize = true;
            this.checkBox_sxos.Checked = true;
            this.checkBox_sxos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_sxos.Location = new System.Drawing.Point(469, 8);
            this.checkBox_sxos.Name = "checkBox_sxos";
            this.checkBox_sxos.Size = new System.Drawing.Size(123, 17);
            this.checkBox_sxos.TabIndex = 12;
            this.checkBox_sxos.Text = "Create SXOS folders";
            this.checkBox_sxos.UseVisualStyleBackColor = true;
            // 
            // richTextBox_split
            // 
            this.richTextBox_split.BackColor = System.Drawing.Color.Black;
            this.richTextBox_split.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_split.ForeColor = System.Drawing.Color.Lime;
            this.richTextBox_split.Location = new System.Drawing.Point(5, 54);
            this.richTextBox_split.Name = "richTextBox_split";
            this.richTextBox_split.ReadOnly = true;
            this.richTextBox_split.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox_split.Size = new System.Drawing.Size(582, 213);
            this.richTextBox_split.TabIndex = 11;
            this.richTextBox_split.Text = "";
            this.richTextBox_split.WordWrap = false;
            // 
            // Split
            // 
            this.Split.Location = new System.Drawing.Point(83, 4);
            this.Split.Name = "Split";
            this.Split.Size = new System.Drawing.Size(75, 23);
            this.Split.TabIndex = 9;
            this.Split.Text = "Split";
            this.Split.UseVisualStyleBackColor = true;
            this.Split.Click += new System.EventHandler(this.Split_Click);
            // 
            // outputstring3
            // 
            this.outputstring3.Location = new System.Drawing.Point(5, 30);
            this.outputstring3.Name = "outputstring3";
            this.outputstring3.Size = new System.Drawing.Size(582, 20);
            this.outputstring3.TabIndex = 8;
            // 
            // OutputSpilt
            // 
            this.OutputSpilt.Location = new System.Drawing.Point(5, 4);
            this.OutputSpilt.Name = "OutputSpilt";
            this.OutputSpilt.Size = new System.Drawing.Size(75, 23);
            this.OutputSpilt.TabIndex = 7;
            this.OutputSpilt.Text = "Output Dir";
            this.OutputSpilt.UseVisualStyleBackColor = true;
            this.OutputSpilt.Click += new System.EventHandler(this.OutputSpilt_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchGoogleToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(110, 26);
            // 
            // searchGoogleToolStripMenuItem
            // 
            this.searchGoogleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleToolStripMenuItem,
            this.youtubeToolStripMenuItem,
            this.nintendoToolStripMenuItem});
            this.searchGoogleToolStripMenuItem.Name = "searchGoogleToolStripMenuItem";
            this.searchGoogleToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.searchGoogleToolStripMenuItem.Text = "Search";
            // 
            // googleToolStripMenuItem
            // 
            this.googleToolStripMenuItem.Name = "googleToolStripMenuItem";
            this.googleToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.googleToolStripMenuItem.Text = "Google";
            this.googleToolStripMenuItem.Click += new System.EventHandler(this.googleToolStripMenuItem_Click);
            // 
            // youtubeToolStripMenuItem
            // 
            this.youtubeToolStripMenuItem.Name = "youtubeToolStripMenuItem";
            this.youtubeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.youtubeToolStripMenuItem.Text = "YouTube";
            this.youtubeToolStripMenuItem.Click += new System.EventHandler(this.youtubeToolStripMenuItem_Click);
            // 
            // nintendoToolStripMenuItem
            // 
            this.nintendoToolStripMenuItem.Name = "nintendoToolStripMenuItem";
            this.nintendoToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.nintendoToolStripMenuItem.Text = "Nintendo";
            this.nintendoToolStripMenuItem.Click += new System.EventHandler(this.nintendoToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadGameToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.exploreToolStripMenuItem,
            this.certToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(608, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadGameToolStripMenuItem
            // 
            this.loadGameToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.loadGameToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loadGameToolStripMenuItem.BackgroundImage")));
            this.loadGameToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loadGameToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.loadGameToolStripMenuItem.Text = "Load";
            this.loadGameToolStripMenuItem.Click += new System.EventHandler(this.loadGameToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sAKToolStripMenuItem,
            this.fat32FormatToolStripMenuItem,
            this.toolStripSeparator1,
            this.aESToolToolStripMenuItem,
            this.databaseStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // sAKToolStripMenuItem
            // 
            this.sAKToolStripMenuItem.Name = "sAKToolStripMenuItem";
            this.sAKToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sAKToolStripMenuItem.Text = "SAK";
            this.sAKToolStripMenuItem.Click += new System.EventHandler(this.sAKToolStripMenuItem_Click);
            // 
            // fat32FormatToolStripMenuItem
            // 
            this.fat32FormatToolStripMenuItem.Name = "fat32FormatToolStripMenuItem";
            this.fat32FormatToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fat32FormatToolStripMenuItem.Text = "Fat32 Format";
            this.fat32FormatToolStripMenuItem.Click += new System.EventHandler(this.fat32FormatToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // aESToolToolStripMenuItem
            // 
            this.aESToolToolStripMenuItem.Name = "aESToolToolStripMenuItem";
            this.aESToolToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aESToolToolStripMenuItem.Text = "Cobra Crypt";
            this.aESToolToolStripMenuItem.Click += new System.EventHandler(this.aESToolToolStripMenuItem_Click);
            // 
            // databaseStripMenuItem
            // 
            this.databaseStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gamesToolStripMenuItem,
            this.linksToolStripMenuItem,
            this.cheatsToolStripMenuItem});
            this.databaseStripMenuItem.Name = "databaseStripMenuItem";
            this.databaseStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.databaseStripMenuItem.Text = "Database";
            // 
            // cheatsToolStripMenuItem
            // 
            this.cheatsToolStripMenuItem.Name = "cheatsToolStripMenuItem";
            this.cheatsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cheatsToolStripMenuItem.Text = "Cheats";
            this.cheatsToolStripMenuItem.Click += new System.EventHandler(this.cheatsToolStripMenuItem_Click);
            // 
            // gamesToolStripMenuItem
            // 
            this.gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
            this.gamesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gamesToolStripMenuItem.Text = "Games";
            this.gamesToolStripMenuItem.Click += new System.EventHandler(this.gamesToolStripMenuItem_Click);
            // 
            // linksToolStripMenuItem
            // 
            this.linksToolStripMenuItem.Name = "linksToolStripMenuItem";
            this.linksToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.linksToolStripMenuItem.Text = "Links";
            this.linksToolStripMenuItem.Click += new System.EventHandler(this.linksToolStripMenuItem_Click);
            // 
            // exploreToolStripMenuItem
            // 
            this.exploreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem1,
            this.binToolStripMenuItem});
            this.exploreToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.exploreToolStripMenuItem.Name = "exploreToolStripMenuItem";
            this.exploreToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.exploreToolStripMenuItem.Text = "Explore";
            // 
            // toolsToolStripMenuItem1
            // 
            this.toolsToolStripMenuItem1.Name = "toolsToolStripMenuItem1";
            this.toolsToolStripMenuItem1.Size = new System.Drawing.Size(101, 22);
            this.toolsToolStripMenuItem1.Text = "Tools";
            this.toolsToolStripMenuItem1.Click += new System.EventHandler(this.toolsToolStripMenuItem1_Click);
            // 
            // binToolStripMenuItem
            // 
            this.binToolStripMenuItem.Name = "binToolStripMenuItem";
            this.binToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.binToolStripMenuItem.Text = "Bin";
            this.binToolStripMenuItem.Click += new System.EventHandler(this.binToolStripMenuItem_Click);
            // 
            // certToolStripMenuItem
            // 
            this.certToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.certToolStripMenuItem.Name = "certToolStripMenuItem";
            this.certToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.certToolStripMenuItem.Text = "Cert";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownHeight = 400;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Location = new System.Drawing.Point(444, 1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.MouseHover += new System.EventHandler(this.MouseHover);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(608, 353);
            this.ContextMenuStrip = this.contextMenuStrip2;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.TB_File);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XCI Explorer (Mod)";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_GameIcon)).EndInit();
            this.Keys.ResumeLayout(false);
            this.Partitions.ResumeLayout(false);
            this.Partitions.PerformLayout();
            this.Main.ResumeLayout(false);
            this.Main.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Base64.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.nxci.ResumeLayout(false);
            this.nxci.PerformLayout();
            this.ReKey.ResumeLayout(false);
            this.ReKey.PerformLayout();
            this.Spliter.ResumeLayout(false);
            this.Spliter.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private long[] SecureSize;
        private long[] NormalSize;
        private long[] SecureOffset;
        private long[] NormalOffset;
        private string[] SecureName = { };
        private long gameNcaOffset;
        private long gameNcaSize;
        private long PFS0Offset;
        private long PFS0Size;
        private long selectedOffset;
        private long selectedSize;
        private TreeViewFileSystem TV_Parti;
        private BetterTreeNode rootNode;
        public TextBox TB_File;
        private BackgroundWorker backgroundWorker1;
        private ToolTip toolTip1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private TabPage Keys;
        private Button sortkey;
        private Button WriteKeys;
        private RichTextBox richTextBox1;
        private TabPage Partitions;
        private Label LB_HashedRegionSize;
        private Label LB_ActualHash;
        private Label LB_ExpectedHash;
        private Button B_Extract;
        private Label LB_DataSize;
        private Label LB_DataOffset;
        private Label LB_SelectedData;
        private TreeView TV_Partitions;
        private TabPage Main;
        private Button Rename;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button B_TrimXCI;
        private TextBox TB_Capacity;
        private TextBox TB_SDKVer;
        private TextBox TB_ProdCode;
        private TextBox TB_Dev;
        private TextBox TB_TID;
        private TextBox TB_Name;
        private TextBox TB_GameRev;
        private TextBox TB_ExactUsedSpace;
        private TextBox TB_ROMExactSize;
        private TextBox TB_UsedSpace;
        private TextBox TB_ROMSize;
        private TextBox TB_MKeyRev;
        private Label label3;
        private Label label2;
        private Label label8;
        private Label label11;
        private Label label10;
        private ComboBox CB_RegionName;
        private Label label1;
        private Label label9;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private TabControl tabControl1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem searchGoogleToolStripMenuItem;
        private ToolStripMenuItem googleToolStripMenuItem;
        private ToolStripMenuItem youtubeToolStripMenuItem;
        private ToolStripMenuItem nintendoToolStripMenuItem;
        private Button ShowMD5;
        private TabPage Base64;
        private Button button2;
        private Button button1;
        private RichTextBox base64RTB;
        private Button button3;
        private Button ShowKeys;
        private TabPage nxci;
        private RichTextBox consolebox;
        private Button Convert;
        private Button OutputDir;
        private TextBox outputstring;
        private CheckBox titnamecheck;
        private CheckBox usetmp;
        private CheckBox keepncaid;
        private TabPage ReKey;
        private CheckBox usetmp2;
        private Button OutputDir2;
        private Button Convert2;
        private TextBox outputstring2;
        private RichTextBox consolebox2;
        private TabPage Spliter;
        private TextBox outputstring3;
        private Button OutputSpilt;
        private Button Split;
        private RichTextBox richTextBox_split;
        private CheckBox checkBox_sxos;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem sAKToolStripMenuItem;
        private ToolStripMenuItem exploreToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem1;
        private ToolStripMenuItem binToolStripMenuItem;
        private ToolStripMenuItem fat32FormatToolStripMenuItem;
        private ToolStripMenuItem loadGameToolStripMenuItem;
        private PictureBox PB_GameIcon;
        private ToolStripMenuItem certToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip3;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ComboBox comboBox1;
        private ToolStripMenuItem aESToolToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem databaseStripMenuItem;
        private ToolStripMenuItem linksToolStripMenuItem;
        private ToolStripMenuItem gamesToolStripMenuItem;
        private Button button_add_DB;
        private ToolStripMenuItem cheatsToolStripMenuItem;
    }
}