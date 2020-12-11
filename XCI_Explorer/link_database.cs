using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
https://www.sqlitetutorial.net/
http://zetcode.com/csharp/sqlite/
*/

namespace XCI_Explorer.XCI_Explorer
{
    public partial class linkdb : Form
    {
        public static string mydb = ("tools/bin/links.db");
        public static string mydatabase = ("Data Source=" + mydb);
        // public static string mydatabase = @"URI=file:D:\Switch\Projects\XCI-Explorer\bin\Release\links.db";

        public linkdb()
        {
            InitializeComponent();
            sqlversion();
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            if (File.Exists(mydb))
            {
                comboBox1_additems();
                //populatelist(); //use this if combobox is removed
            }
            else
            {
                MessageBox.Show("No database found", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void populatelist()
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con)
                {
                    //CommandText = "SELECT * from links"
                    CommandText = "SELECT * from links ORDER BY name COLLATE NOCASE ASC"
                };

                SQLiteDataReader sqReader = cmd.ExecuteReader();

                listView1.View = View.Details;
                listView1.Columns.Add("Name");
                listView1.Columns.Add("Link");
                //listView1.Columns.Add("ID");

                //listView1.Columns[0].Width = 100;
                //listView1.Columns[1].Width = 430;

                while (sqReader.Read())
                {
                    string listitems = (sqReader.GetString(1)); //1 for names 2 for url 3 for about
                    string listurl = (sqReader.GetString(2)); //1 for names 2 for url 3 for about
                    string listabout = (sqReader.GetString(3)); //1 for names 2 for url 3 for about
                    int listid = (sqReader.GetInt32(0));
                    string id = listid.ToString();
                    //listBox1.Items.Add(string.Format(listitems, listurl));
                    listView1.Items.Add(new ListViewItem(new string[] { listitems, listurl, id, listabout }));
                }

                con.Close();

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void sqlversion()
        {
            try
            {
                //test database connection and version
                string database = "Data Source=:memory:";
                string stm = "SELECT SQLITE_VERSION()";
                using var con = new SQLiteConnection(database);
                con.Open();
                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();
                this.Text = ($"Website Database - Powered by SQLite version: {version}");
                con.Close();
                //end of test database
                //makedatabase(); //create a test database
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

                string txt_name = textBox_name.Text;
                string txt_url = textBox_url.Text;
                string txt_about = textBox_about.Text;

                if (textBox_name.Text == "" || textBox_name.Text == String.Empty || textBox_name.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the name box");
                    return;
                }

                if (textBox_url.Text == "" || textBox_url.Text == String.Empty || textBox_url.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the link box");
                    return;
                }

                else
                {
                    string http = "http";
                    bool b = txt_url.Contains(http); //return true if string contians http. 
                    if (!b)
                    {
                        txt_url = (http + "://" + txt_url); //add http if it's missing.
                    }
                    
                    txt_name = (char.ToUpper(txt_name[0]) + txt_name.Substring(1)); //make first letter uppercase
                    cmd.CommandText = "INSERT INTO links(name, url, about) VALUES(@name, @url, @about)";
                    cmd.Parameters.AddWithValue("@name", txt_name);
                    cmd.Parameters.AddWithValue("@url", txt_url);
                    cmd.Parameters.AddWithValue("@about", txt_about);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    populatelist();
                    MessageBox.Show(txt_name + " added to the database", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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

                string txt_name = textBox_name.Text;
                string txt_url = textBox_url.Text;
                string txt_about = textBox_about.Text;

                if (textBox_name.Text == "" || textBox_name.Text == String.Empty || textBox_name.TextLength == 0)
                {
                    MessageBox.Show("Please enter a value in the name box");
                    return;
                }

                if (textBox_url.Text == "" || textBox_url.Text == String.Empty || textBox_url.TextLength == 0)
                {

                    MessageBox.Show("Please enter a value in the link box");
                    return;
                }

                else
                {

                    if (listView1.SelectedItems.Count == 0)
                        return;

                    if (MessageBox.Show("Update " + txt_name + " on the database?", "Update!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ListViewItem item = listView1.SelectedItems[0];
                        string txt_id = item.SubItems[2].Text;

                        //cmd.CommandText = "UPDATE links set name = 'test', url = 'www', about = 'something' WHERE ID = 2";

                        cmd.CommandText = ("UPDATE links set name = '" + txt_name + "', url = '" + txt_url + "', about = '" + txt_about + "' WHERE ID = " + txt_id + "");
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        listView1.Items.Clear();
                        listView1.Columns.Clear();
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
                string myid = (item.SubItems[2].Text);

                if (MessageBox.Show("Remove " + item.SubItems[0].Text + " from the database?", "Remove from database!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd.CommandText = "DELETE FROM links WHERE ID = " + myid + ";";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    textBox_name.Text = "";
                    textBox_url.Text = "";
                    textBox_about.Text = "";
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            //fill the text boxes
            textBox_name.Text = item.Text;
            textBox_url.Text = item.SubItems[1].Text;
            textBox_about.Text = item.SubItems[3].Text; //id = 2
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count == 0)
                    return;

                ListViewItem item = listView1.SelectedItems[0];
                System.Diagnostics.Process.Start(item.SubItems[1].Text);
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
                sl.Add(new select() { Text = "Name (A to Z)", Value = "name COLLATE NOCASE ASC" });
                sl.Add(new select() { Text = "Name (Z to A)", Value = "name COLLATE NOCASE DESC" });
                sl.Add(new select() { Text = "Link (A to Z)", Value = "url COLLATE NOCASE ASC" });
                sl.Add(new select() { Text = "Link (Z to A)", Value = "url COLLATE NOCASE DESC" });
                sl.Add(new select() { Text = "Added (Ascending)", Value = "id COLLATE NOCASE ASC" });
                sl.Add(new select() { Text = "Added (Descending)", Value = "id COLLATE NOCASE DESC" });
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
                //get value from combobox - store as t1
                string t1 = "";
                select sl1 = comboBox1.SelectedItem as select;
                t1 = Convert.ToString(sl1.Value);

                listView1.Items.Clear();
                listView1.Columns.Clear();

                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);
                cmd.CommandText = "SELECT * from links ORDER BY " + t1 + "";

                SQLiteDataReader sqReader = cmd.ExecuteReader();

                listView1.View = View.Details;
                listView1.Columns.Add("Name");
                listView1.Columns.Add("Link");
                //listView1.Columns.Add("ID");

                //listView1.Columns[0].Width = 100;
                //listView1.Columns[1].Width = 430;

                while (sqReader.Read())
                {
                    string listitems = (sqReader.GetString(1));
                    string listurl = (sqReader.GetString(2));
                    string listabout = (sqReader.GetString(3)); //1 for names 2 for url 3 for about
                    int listid = (sqReader.GetInt32(0));
                    string id = listid.ToString();
                    listView1.Items.Add(new ListViewItem(new string[] { listitems, listurl, id, listabout }));
                }

                con.Close();

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


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
                string search = textBox_Search.Text.Replace("'", "''");
                if (String.IsNullOrEmpty(search))
                {
                    //MessageBox.Show("Please enter some text into the search box - I'm not a mind reader!", "Nothing to search for!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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

                    cmd.CommandText = "SELECT * FROM links WHERE Name LIKE '%" + search + "%'";

                    SQLiteDataReader sqReader = cmd.ExecuteReader();

                    while (sqReader.Read())
                    {
                        string listitems = (sqReader.GetString(1));
                        string listurl = (sqReader.GetString(2));
                        string listabout = (sqReader.GetString(3)); //1 for names 2 for url 3 for about
                        int listid = (sqReader.GetInt32(0));
                        string id = listid.ToString();
                        listView1.Items.Add(new ListViewItem(new string[] { listitems, listurl, id, listabout }));

                    }

                    con.Close();

                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }
    }

}
