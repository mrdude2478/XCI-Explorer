using FluentFTP;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace XCI_Explorer.XCI_Explorer
{
    public partial class Cheats : Form
    {
        public static string mydb = ("tools/bin/links.db");
        public static string mydatabase = ("Data Source=" + mydb);

        public Cheats()
        {
            InitializeComponent();
            populatelist();
            comboBox1_additems();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                richTextBox_cheat.Copy();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox_cheat.Paste();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void Copy_toolStripMenuItem_Notes_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                richTextBox_notes.Copy();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void Paste_toolStripMenuItem_Notes_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox_notes.Paste();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void populatelist()
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cheats(id INTEGER PRIMARY KEY ASC, name TEXT, titleid TEXT, build TEXT, cheat TEXT, notes TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT * from cheats ORDER BY name COLLATE NOCASE ASC";
                cmd.ExecuteNonQuery();

                SQLiteDataReader sqReader = cmd.ExecuteReader();

                listView1.View = View.Details;
                listView1.Columns.Add("Name");
                listView1.Columns.Add("Title ID");
                listView1.Columns.Add("Build ID");
                //listView1.Columns.Add("ID");

                while (sqReader.Read())
                {
                    string listname = (sqReader.GetString(1));
                    string listtitleid = (sqReader.GetString(2));
                    string listbuild = (sqReader.GetString(3));
                    string listcheat = (sqReader.GetString(4));
                    string listnotes = (sqReader.GetString(5));
                    int listid = (sqReader.GetInt32(0));
                    string id = listid.ToString();
                    listView1.Items.Add(new ListViewItem(new string[] { listname, listtitleid, listbuild, listcheat, listnotes, id }));
                }

                con.Close();

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                data_amount();

            }
            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                    return;

                ListViewItem item = listView1.SelectedItems[0];
                //fill the text boxes
                textBox_name.Text = item.Text;
                textBox_titleid.Text = item.SubItems[1].Text;
                textBox_build.Text = item.SubItems[2].Text;
                richTextBox_cheat.Text = item.SubItems[3].Text;
                richTextBox_notes.Text = item.SubItems[4].Text;
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);

                string txt_name = (textBox_name.Text).Replace("'", "");
                string txt_title = textBox_titleid.Text;
                string txt_build = textBox_build.Text;
                richTextBox_cheat.Text = richTextBox_cheat.Text.Replace("'", "");
                richTextBox_notes.Text = richTextBox_notes.Text.Replace("'", "");
                string txt_cheat = richTextBox_cheat.Text;
                string txt_notes = richTextBox_notes.Text;

                if (textBox_name.Text == "" || textBox_name.Text == String.Empty || textBox_name.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the Name box");
                    return;
                }

                if (textBox_titleid.Text == "" || textBox_titleid.Text == String.Empty || textBox_titleid.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Title ID box");
                    return;
                }

                if (textBox_build.Text == "" || textBox_build.Text == String.Empty || textBox_build.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Build ID box");
                    return;
                }

                if (richTextBox_cheat.Text == "" || richTextBox_cheat.Text == String.Empty || richTextBox_cheat.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Cheat box");
                    return;
                }

                else
                {
                    cmd.CommandText = "INSERT INTO cheats(name, titleid, build, cheat, notes) VALUES(@name, @titleid, @build, @cheat, @notes)";
                    cmd.Parameters.AddWithValue("@name", txt_name);
                    cmd.Parameters.AddWithValue("@titleid", txt_title);
                    cmd.Parameters.AddWithValue("@build", txt_build.ToLower());
                    cmd.Parameters.AddWithValue("@cheat", txt_cheat);
                    cmd.Parameters.AddWithValue("@notes", txt_notes);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    clear();
                    populatelist();
                    MessageBox.Show(txt_name + " added to the database", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);

                if (listView1.SelectedItems.Count == 0)
                    return;

                ListViewItem item = listView1.SelectedItems[0];
                string myid = (item.SubItems[5].Text);

                if (MessageBox.Show("Remove " + item.SubItems[0].Text + " from the database?", "Remove from database!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd.CommandText = "DELETE FROM cheats WHERE ID = " + myid + ";";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    clear();
                    populatelist();

                    MessageBox.Show(item.SubItems[0].Text + " removed from the database", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }

                else
                {
                    return;
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);

                string txt_name = (textBox_name.Text).Replace("'", "");
                string txt_title = textBox_titleid.Text;
                string txt_build = textBox_build.Text;
                richTextBox_cheat.Text = richTextBox_cheat.Text.Replace("'", "");
                richTextBox_notes.Text = richTextBox_notes.Text.Replace("'", "");
                string txt_cheat = richTextBox_cheat.Text;
                string txt_notes = richTextBox_notes.Text;

                if (textBox_name.Text == "" || textBox_name.Text == String.Empty || textBox_name.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the Name box");
                    return;
                }

                if (textBox_titleid.Text == "" || textBox_titleid.Text == String.Empty || textBox_titleid.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Title ID box");
                    return;
                }

                if (textBox_build.Text == "" || textBox_build.Text == String.Empty || textBox_build.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Build ID box");
                    return;
                }

                if (richTextBox_cheat.Text == "" || richTextBox_cheat.Text == String.Empty || richTextBox_cheat.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Cheat box");
                    return;
                }

                else
                {

                    if (listView1.SelectedItems.Count == 0)
                        return;

                    if (MessageBox.Show("Update " + txt_name + " on the database?", "Update!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ListViewItem item = listView1.SelectedItems[0];
                        string txt_id = item.SubItems[5].Text;

                        cmd.CommandText = ("UPDATE cheats set name = '" + txt_name + "', titleid = '" + txt_title + "', build = '" + txt_build.ToLower() + "', cheat = '" + txt_cheat + "', notes = '" + txt_notes + "' WHERE ID = " + txt_id + "");
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        listView1.Items.Clear();
                        listView1.Columns.Clear();
                        clear();
                        populatelist();
                        MessageBox.Show(txt_name + " updated on the database", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }

                    else
                    {
                        return;
                    }


                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void Search(object sender, EventArgs e)
        {
            try
            {
                string search = textBox_search.Text.Replace("'", "''"); //don't break search if string contains '
                if (String.IsNullOrEmpty(search))
                {
                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    populatelist();
                }
                else
                {
                    listView1.Items.Clear();
                    // database stuff
                    using var con = new SQLiteConnection(mydatabase);
                    con.Open();
                    using var cmd = new SQLiteCommand(con);

                    //cmd.CommandText = "SELECT * FROM Games WHERE Name LIKE '%" + search + "%'";
                    cmd.CommandText = "SELECT * FROM cheats WHERE name LIKE '%" + search + "%' OR titleid LIKE '%" + search + "%' ORDER BY name COLLATE NOCASE ";

                    SQLiteDataReader sqReader = cmd.ExecuteReader();

                    while (sqReader.Read())
                    {
                        string listname = (sqReader.GetString(1));
                        string listtitle = (sqReader.GetString(2));
                        string listbuild = (sqReader.GetString(3));
                        string listcheat = (sqReader.GetString(4));
                        string listnotes = (sqReader.GetString(5));
                        int listid = (sqReader.GetInt32(0));
                        string id = listid.ToString();
                        listView1.Items.Add(new ListViewItem(new string[] { listname, listtitle, listbuild, listcheat, listnotes, id }));
                    }

                    con.Close();

                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    data_amount();
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void data_amount()
        {
            int i = 0;
            try
            {
                for (int x = 0; x < listView1.Items.Count; x++)
                {
                    i++;
                }

                string countval = i.ToString();
                this.Text = ($"Cheats Database - Items in list: {countval}");
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void comboBox1_additems()
        {
            try
            {
                List<select> sl = new List<select>();
                sl.Add(new select() { Text = "Name (A to Z)", Value = "name ASC" });
                sl.Add(new select() { Text = "Name (Z to A)", Value = "name DESC" });
                sl.Add(new select() { Text = "Title ID (Ascending)", Value = "titleid ASC" });
                sl.Add(new select() { Text = "Title ID (Descending)", Value = "titleid DESC" });
                comboBox1.DataSource = sl;
                comboBox1.DisplayMember = "Text";
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox_search.Text = "";
                //get value from combobox - store as t1
                string t1 = "";
                select sl1 = comboBox1.SelectedItem as select;
                t1 = Convert.ToString(sl1.Value);

                listView1.Items.Clear();
                listView1.Columns.Clear();

                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con)
                {
                    CommandText = "SELECT * from cheats ORDER BY " + t1 + ""
                };

                SQLiteDataReader sqReader = cmd.ExecuteReader();

                listView1.View = View.Details;
                listView1.Columns.Add("Name");
                listView1.Columns.Add("Title ID");
                listView1.Columns.Add("Build ID");
                //listView1.Columns.Add("ID");

                while (sqReader.Read())
                {
                    string listname = (sqReader.GetString(1));
                    string listtitleid = (sqReader.GetString(2));
                    string listbuild = (sqReader.GetString(3));
                    string listcheat = (sqReader.GetString(4));
                    string listnotes = (sqReader.GetString(5));
                    int listid = (sqReader.GetInt32(0));
                    string id = listid.ToString();
                    listView1.Items.Add(new ListViewItem(new string[] { listname, listtitleid, listbuild, listcheat, listnotes, id }));
                }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                data_amount();

                con.Close();


            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }

        }

        class select
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        private void werWolvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/WerWolv/EdiZon_CheatsConfigsAndScripts/tree/master/Cheats");
            }

            catch
            {
                MessageBox.Show("Unable to open link");
            }
        }

        private void richTextBox_notes_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }

            catch
            {
                MessageBox.Show("Unable to open link");
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //richTextBox_notes.Clear();
                using (Process cheatexp = new Process())
                {
                    string path = Directory.GetCurrentDirectory();
                    String args = ("sqlite3.exe -header -csv links.db " + '"' + "select* from cheats" + '"' + " > " + '"' + "cheats.csv" + '"');
                    //sqlite3.exe -header -csv links.db "select * from cheats;" > "cheats.csv"
                    String folder = ($"tools{Path.DirectorySeparatorChar}/bin/");
                    cheatexp.StartInfo.WorkingDirectory = folder;
                    cheatexp.StartInfo.FileName = "cmd.exe";
                    cheatexp.StartInfo.Verb = "runas";
                    cheatexp.StartInfo.Arguments = "/C " + args;
                    cheatexp.StartInfo.CreateNoWindow = true;
                    cheatexp.StartInfo.UseShellExecute = true;
                    cheatexp.StartInfo.RedirectStandardOutput = false; //set to false if using shell execute

                    // Start the process.
                    cheatexp.Start();
                    cheatexp.WaitForExit();

                    MessageBox.Show("cheats.csv created in " + path + "\\tools\\bin");
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("You are about to replace ALL items in your cheat database\n\nDo you want to continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV File (*.csv)|*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string checkfile = openFileDialog.FileName; //full path with filename
                    string cheats = openFileDialog.SafeFileName; //just filename

                    string path = ($"tools{Path.DirectorySeparatorChar}/bin/import.txt");
                    string table = "temp";

                    if (!File.Exists(checkfile))
                    {
                        MessageBox.Show("Please create cheats.csv first");
                        return;
                    }

                    if (File.Exists(path)) //make sure to remove this file so we don't get errors.
                    {
                        File.Delete(path);
                    }

                    if (!File.Exists(path))
                    {
                        //empty the table first so we get a clean import
                        using var con = new SQLiteConnection(mydatabase);
                        con.Open();
                        using var cmd = new SQLiteCommand(con);

                        cmd.CommandText = ("DROP TABLE IF EXISTS " + table);
                        cmd.ExecuteNonQuery();

                        //fix file path for csv file
                        String pathfix = checkfile.Replace("\\", "//");

                        //create a script to use with sqlite.exe
                        string[] lines = { ".mode csv cheats", ".import " + '"' + pathfix + '"' + " " + '"' + table + '"', ".exit" };
                        System.IO.File.WriteAllLines(path, lines);

                        //run the script we just created
                        using (Process cheatimport = new Process())
                        {
                            String args = ("sqlite3.exe links.db < import.txt");
                            //sqlite3.exe -header -csv links.db "select * from cheats;" > "cheats.csv"
                            cheatimport.StartInfo.WorkingDirectory = ($"tools{Path.DirectorySeparatorChar}/bin/");
                            cheatimport.StartInfo.FileName = "cmd.exe";
                            cheatimport.StartInfo.Verb = "runas";
                            cheatimport.StartInfo.Arguments = "/C " + args;
                            cheatimport.StartInfo.CreateNoWindow = true;
                            cheatimport.StartInfo.UseShellExecute = true;
                            cheatimport.StartInfo.RedirectStandardOutput = false; //set to false if using shell execute

                            // Start the process.
                            cheatimport.Start();
                            cheatimport.WaitForExit();
                        }

                        //add code to convert id table from text to int
                        //https://www.techonthenet.com/sqlite/tables/alter_table.php
                        //remove cheats table
                        cmd.CommandText = ("DROP TABLE IF EXISTS cheats");
                        cmd.ExecuteNonQuery();

                        //create new empty cheats table
                        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cheats(id INTEGER PRIMARY KEY ASC, name TEXT, titleid TEXT, build TEXT, cheat TEXT, notes TEXT)";
                        cmd.ExecuteNonQuery();

                        //copy data from temp table into cheats table
                        cmd.CommandText = @"INSERT INTO cheats(name, titleid, build, cheat, notes) SELECT name, titleid, build, cheat, notes FROM " + '"' + table + '"';
                        cmd.ExecuteNonQuery();

                        //remove our temp table
                        cmd.CommandText = ("DROP TABLE IF EXISTS " + '"' + table + '"');
                        cmd.ExecuteNonQuery();

                        File.Delete(path);
                        con.Close();

                        listView1.Items.Clear();
                        listView1.Columns.Clear();
                        textBox_name.Text = "";
                        textBox_titleid.Text = "";
                        textBox_build.Text = "";
                        textBox_search.Text = "";
                        richTextBox_cheat.Text = "";
                        richTextBox_notes.Text = "";
                        populatelist();

                        MessageBox.Show(cheats + " imported into the database successfully");
                    }

                    else
                    {
                        return;
                    }



                }

                else
                {
                    return;
                }

            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void clear()
        {
            try
            {
                textBox_name.Text = "";
                textBox_titleid.Text = "";
                textBox_build.Text = "";
                textBox_search.Text = "";
                richTextBox_cheat.Text = "";
                richTextBox_notes.Text = "Notes:";
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            try
            {
                string txt_name = textBox_name.Text;
                string txt_title = textBox_titleid.Text;
                string txt_build = textBox_build.Text;
                string txt_cheat = richTextBox_cheat.Text;

                if (textBox_name.Text == "" || textBox_name.Text == String.Empty || textBox_name.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the Name box");
                    return;
                }

                if (textBox_titleid.Text == "" || textBox_titleid.Text == String.Empty || textBox_titleid.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Title ID box");
                    return;
                }

                if (textBox_build.Text == "" || textBox_build.Text == String.Empty || textBox_build.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Build ID box");
                    return;
                }

                if (richTextBox_cheat.Text == "" || richTextBox_cheat.Text == String.Empty || richTextBox_cheat.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the Cheat box");
                    return;
                }

                String folder = ($"tools{Path.DirectorySeparatorChar}/bin/cheats");
                String titlefold = ($"tools{Path.DirectorySeparatorChar}/bin/cheats/titles");

                bool exists = System.IO.Directory.Exists(folder);

                if (!exists)
                    System.IO.Directory.CreateDirectory(folder);

                bool titles = System.IO.Directory.Exists(titlefold);

                if (!titles)
                    System.IO.Directory.CreateDirectory(titlefold);


                DirectoryInfo dir = new DirectoryInfo(titlefold);
                DirectoryInfo dis = dir.CreateSubdirectory(txt_title); //subdir of dir

                DirectoryInfo dir2 = new DirectoryInfo(titlefold + "/" + txt_title);
                DirectoryInfo dis2 = dir2.CreateSubdirectory("cheats"); //subdir of dir2

                DirectoryInfo dir3 = new DirectoryInfo(titlefold + "/" + txt_title + "/cheats/");

                System.IO.File.WriteAllText(dir3 + txt_build + ".txt", txt_cheat);

                String Dir = Directory.GetCurrentDirectory();

                MessageBox.Show("Cheat file for " + txt_name + " Created in:" + "\n\n" + Dir + "\\toos\\bin\\cheats", "Yipee!", MessageBoxButtons.OK, MessageBoxIcon.Information);



            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void binFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = "tools\\bin",
                    UseShellExecute = true,
                    Verb = "open"
                });
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form ftp = new ftp();
            //this.Hide();
            ftp.ShowDialog();
            //this.Show();
        }

        private void ftp()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\XCI-Explorer");

                //if reg key does exist, retrieve the stored values  
                if (key != null)
                {
                    string upload_folder = "";
                    string ip = ((string)key.GetValue("IP"));
                    string port = ((string)key.GetValue("Port"));
                    string sx = ((string)key.GetValue("SXOS"));
                    string atmos = ((string)key.GetValue("Atmos"));
                    key.Close();

                    if (sx == "0" && atmos == "0")
                    {
                        MessageBox.Show("Open the FTP settings and choose a firmware to send the cheat to");
                        return;
                    }

                    //create the cheat text file first so we have something to ftp to our switch...
                    string txt_name = textBox_name.Text;
                    string txt_title = textBox_titleid.Text;
                    string txt_build = textBox_build.Text;
                    string txt_cheat = richTextBox_cheat.Text;
                    richTextBox_notes.Text = ""; //clear this so we can see what was sent later in program

                    if (textBox_name.Text == "" || textBox_name.Text == String.Empty || textBox_name.TextLength == 0)
                    {
                        MessageBox.Show("Please enter a value in the Name box");
                        return;
                    }

                    if (textBox_titleid.Text == "" || textBox_titleid.Text == String.Empty || textBox_titleid.TextLength == 0)
                    {

                        MessageBox.Show("Please enter a value in the Title ID box");
                        return;
                    }

                    if (textBox_build.Text == "" || textBox_build.Text == String.Empty || textBox_build.TextLength == 0)
                    {

                        MessageBox.Show("Please enter a value in the Build ID box");
                        return;
                    }

                    if (richTextBox_cheat.Text == "" || richTextBox_cheat.Text == String.Empty || richTextBox_cheat.TextLength == 0)
                    {

                        MessageBox.Show("Please enter a value in the Cheat box");
                        return;
                    }

                    String folder = ($"tools{Path.DirectorySeparatorChar}/bin/cheats");
                    String titlefold = ($"tools{Path.DirectorySeparatorChar}/bin/cheats/titles");

                    bool exists = System.IO.Directory.Exists(folder);

                    if (!exists)
                        System.IO.Directory.CreateDirectory(folder);

                    bool titles = System.IO.Directory.Exists(titlefold);

                    if (!titles)
                        System.IO.Directory.CreateDirectory(titlefold);


                    DirectoryInfo dir = new DirectoryInfo(titlefold);
                    DirectoryInfo dis = dir.CreateSubdirectory(txt_title); //subdir of dir

                    DirectoryInfo dir2 = new DirectoryInfo(titlefold + "/" + txt_title);
                    DirectoryInfo dis2 = dir2.CreateSubdirectory("cheats"); //subdir of dir2

                    DirectoryInfo dir3 = new DirectoryInfo(titlefold + "/" + txt_title + "/cheats/");

                    System.IO.File.WriteAllText(dir3 + txt_build + ".txt", txt_cheat);

                    String Dir = Directory.GetCurrentDirectory();

                    //cheat should now be created so lets ftp it now as we have all the information we need to do that...
                    //ftp guide: https://github.com/robinrodricks/FluentFTP/wiki

                    string loader = (titlefold + "/" + txt_title).Replace("//", "\\");
                    string cfw = "";

                    var client = new FtpClient
                    {
                        Host = ip,
                        Port = Int16.Parse(port),
                        Credentials = new NetworkCredential("anonymous", "anonymous"),
                    };

                    client.ConnectTimeout = 1000;
                    client.Connect();

                    if (sx == "1" && atmos == "0")
                    {
                        upload_folder = ("sxos" + "/titles/" + txt_title);
                        if (client.DirectoryExists(upload_folder) == true)
                        {
                            client.DeleteDirectory(upload_folder); //remove file first so we can update any new version.
                        }
                        client.CreateDirectory(upload_folder);
                        client.UploadDirectory(loader, upload_folder, FtpFolderSyncMode.Mirror);
                        cfw = "SXOS";
                    }

                    if (sx == "0" && atmos == "1")
                    {
                        upload_folder = ("atmosphere" + "/contents/" + txt_title);
                        if (client.DirectoryExists(upload_folder) == true)
                        {
                            client.DeleteDirectory(upload_folder); //remove file first so we can update any new version.
                        }
                        client.CreateDirectory(upload_folder);
                        client.UploadDirectory(loader, upload_folder, FtpFolderSyncMode.Update);
                        cfw = "AtmosphereNX";
                    }

                    if (sx == "1" && atmos == "1")
                    {
                        upload_folder = ("sxos" + "/titles/" + txt_title);
                        if (client.DirectoryExists(upload_folder) == true)
                        {
                            client.DeleteDirectory(upload_folder); //remove file first so we can update any new version.
                        }
                        client.CreateDirectory(upload_folder);
                        client.UploadDirectory(loader, upload_folder, FtpFolderSyncMode.Update);

                        upload_folder = ("atmosphere" + "/contents/" + txt_title);
                        if (client.DirectoryExists(upload_folder) == true)
                        {
                            client.DeleteDirectory(upload_folder); //remove file first so we can update any new version.
                        }
                        client.CreateDirectory(upload_folder);
                        client.UploadDirectory(loader, upload_folder, FtpFolderSyncMode.Update);
                        cfw = "SXOS & AtmosphereNX";
                    }

                    string message = ("Connected to:" + ip + ":" + port + "\n\n" + "FTP System type: " + client.SystemType);
                    message = (message + "\n\n" + "Cheat Uploaded: " + txt_name + " (" + txt_title + ")\n\n" + "Custom firmware selected: " + cfw);
                    richTextBox_notes.Text = message;

                    client.Disconnect();
                }

                else
                {
                    MessageBox.Show("Please enter some ftp settings first");
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message);
            }
        }

        private void ftp_remove()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\XCI-Explorer");

                //if reg key does exist, retrieve the stored values  
                if (key != null)
                {
                    string upload_folder = "";
                    string ip = ((string)key.GetValue("IP"));
                    string port = ((string)key.GetValue("Port"));
                    string sx = ((string)key.GetValue("SXOS"));
                    string atmos = ((string)key.GetValue("Atmos"));
                    key.Close();

                    if (sx == "0" && atmos == "0")
                    {
                        MessageBox.Show("Open the FTP settings and choose a firmware to send the cheat to");
                        return;
                    }

                    string txt_name = textBox_name.Text;
                    string txt_title = textBox_titleid.Text;
                    string txt_build = textBox_build.Text;
                    string txt_cheat = richTextBox_cheat.Text;
                    richTextBox_notes.Text = ""; //clear this so we can see what was sent later in program

                    if (textBox_name.Text == "" || textBox_name.Text == String.Empty || textBox_name.TextLength == 0)
                    {
                        MessageBox.Show("Please enter a value in the Name box");
                        return;
                    }

                    if (textBox_titleid.Text == "" || textBox_titleid.Text == String.Empty || textBox_titleid.TextLength == 0)
                    {

                        MessageBox.Show("Please enter a value in the Title ID box");
                        return;
                    }

                    if (textBox_build.Text == "" || textBox_build.Text == String.Empty || textBox_build.TextLength == 0)
                    {

                        MessageBox.Show("Please enter a value in the Build ID box");
                        return;
                    }

                    if (richTextBox_cheat.Text == "" || richTextBox_cheat.Text == String.Empty || richTextBox_cheat.TextLength == 0)
                    {

                        MessageBox.Show("Please enter a value in the Cheat box");
                        return;
                    }

                    String folder = ($"tools{Path.DirectorySeparatorChar}/bin/cheats");
                    String titlefold = ($"tools{Path.DirectorySeparatorChar}/bin/cheats/titles");

                    //ftp guide: https://github.com/robinrodricks/FluentFTP/wiki

                    string loader = (titlefold + "/" + txt_title).Replace("//", "\\");
                    string cfw = "";

                    var client = new FtpClient
                    {
                        Host = ip,
                        Port = Int16.Parse(port),
                        Credentials = new NetworkCredential("anonymous", "anonymous"),
                    };

                    client.ConnectTimeout = 1000;
                    client.Connect();

                    if (sx == "1" && atmos == "0")
                    {
                        upload_folder = ("sxos" + "/titles/" + txt_title);
                        if (client.DirectoryExists(upload_folder) == true)
                        {
                            client.DeleteDirectory(upload_folder);
                        }
                        else
                        {
                            MessageBox.Show(txt_title + " does not exist - nothing to remove");
                            return;
                        }
                        cfw = "SXOS";
                    }

                    if (sx == "0" && atmos == "1")
                    {
                        upload_folder = ("atmosphere" + "/contents/" + txt_title);
                        if (client.DirectoryExists(upload_folder) == true)
                        {
                            client.DeleteDirectory(upload_folder);
                        }
                        else
                        {
                            MessageBox.Show(txt_title + " does not exist - nothing to remove");
                            return;
                        }
                        cfw = "AtmosphereNX";
                    }

                    if (sx == "1" && atmos == "1")
                    {
                        upload_folder = ("sxos" + "/titles/" + txt_title);
                        if (client.DirectoryExists(upload_folder) == true)
                        {
                            client.DeleteDirectory(upload_folder);
                        }
                        else
                        {
                            MessageBox.Show(txt_title + " does not exist - nothing to remove");
                            return;
                        }

                        upload_folder = ("atmosphere" + "/contents/" + txt_title);
                        if (client.DirectoryExists(upload_folder) == true)
                        {
                            client.DeleteDirectory(upload_folder);
                        }
                        else
                        {
                            MessageBox.Show(txt_title + " does not exist - nothing to remove");
                            return;
                        }
                        cfw = "SXOS & AtmosphereNX";
                    }

                    string message = ("Connected to:" + ip + ":" + port + "\n\n" + "FTP System type: " + client.SystemType);
                    message = (message + "\n\n" + "Cheat Removed: " + txt_name + " (" + txt_title + ")\n\n" + "Custom firmware selected: " + cfw);
                    richTextBox_notes.Text = message;

                    client.Disconnect();
                }

                else
                {
                    MessageBox.Show("Please enter some ftp settings first");
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message);
            }
        }

        private void fTPToSwitchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ftp();
        }

        private void fTPRemoveCheatFromSwitchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ftp_remove();
        }

        private void cheatslipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.cheatslips.com/");
            }

            catch
            {
                MessageBox.Show("Unable to open link");
            }
        }
    }
}
