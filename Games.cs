using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Windows;

namespace XCI_Explorer
{
    public partial class Games : Form
    {

        public static string mydb = ("tools/bin/links.db");
        public static string mydatabase = ("Data Source=" + mydb);


        public Games()
        {
            InitializeComponent();
            //sqlversion();

            if (File.Exists(mydb))
            {
                comboBox1_additems();
                //populatelist(); //use this if combobox is removed
            }
            else
            {
                makedatabase();
                comboBox1_additems();
                //populatelist(); //use this if combobox is removed
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
                this.Text = ($"Games Database, Powered by SQLite version: {version}");
                con.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }


        }

        //count the amount of items shown in the listview box
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
                this.Text = ($"Games Database - Items in list: {countval}");
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        //create a database table if Games table does not exist
        static void makedatabase()
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Games(ID INTEGER PRIMARY KEY ASC, Name TEXT, Size TEXT, Title_ID TEXT, Game_Rev TEXT, Type TEXT, Install TEXT)";
                cmd.ExecuteNonQuery();

                con.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }

        }

        //read Games database table and populate the listview box
        private void populatelist()
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con)
                {
                    CommandText = "SELECT * from Games ORDER BY name ASC"
                };

                SQLiteDataReader sqReader = cmd.ExecuteReader();

                listView1.View = View.Details;
                listView1.Columns.Add("Name");
                listView1.Columns.Add("Size");
                listView1.Columns.Add("Title ID");
                listView1.Columns.Add("Revision");
                listView1.Columns.Add("Install");
                listView1.Columns.Add("Type");
                //listView1.Columns.Add("ID");

                while (sqReader.Read())
                {
                    string listname = (sqReader.GetString(1));
                    //listname = listname.ToLower(); //make name lowercase
                    listname = (char.ToUpper(listname[0]) + listname.Substring(1)); //make first letter uppercase
                    string listsize = (sqReader.GetString(2));
                    string listtitle = (sqReader.GetString(3));
                    string listrev = (sqReader.GetString(4));
                    string listtype = (sqReader.GetString(5));
                    string listinstall = (sqReader.GetString(6));
                    int listid = (sqReader.GetInt32(0));
                    string id = listid.ToString();
                    listView1.Items.Add(new ListViewItem(new string[] { listname, listsize, listtitle, listrev, listtype, listinstall, id }));
                }

                con.Close();

                data_amount();

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            }
            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        //remove item from database table
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
                string myid = (item.SubItems[6].Text);

                if (MessageBox.Show("Remove " + item.SubItems[0].Text + " from the database?", "Remove from database!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd.CommandText = "DELETE FROM Games WHERE ID = " + myid + ";";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();
                    populatelist();
                    data_amount();

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

        //search for item in the database table
        private void Search(object sender, EventArgs e)
        {
            try
            {
                string search = textBox_Search.Text.Replace("'", "''"); //don't break search if string contains '
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
                    cmd.CommandText = "SELECT * FROM Games WHERE Name LIKE '%" + search + "%' OR Title_ID LIKE '%" + search + "%' ORDER BY Name COLLATE NOCASE ";

                    SQLiteDataReader sqReader = cmd.ExecuteReader();

                    while (sqReader.Read())
                    {
                        string listname = (sqReader.GetString(1));
                        //listname = listname.ToLower(); //make name lowercase
                        listname = (char.ToUpper(listname[0]) + listname.Substring(1)); //make first letter uppercase
                        string listsize = (sqReader.GetString(2));
                        string listtitle = (sqReader.GetString(3));
                        string listrev = (sqReader.GetString(4));
                        string listtype = (sqReader.GetString(5));
                        string listinstall = (sqReader.GetString(6));
                        int listid = (sqReader.GetInt32(0));
                        string id = listid.ToString();
                        listView1.Items.Add(new ListViewItem(new string[] { listname, listsize, listtitle, listrev, listtype, listinstall, id }));
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

        //create items & values to put into the combobox
        private void comboBox1_additems()
        {
            try
            {
                List<select> sl = new List<select>();
                sl.Add(new select() { Text = "Name (A to Z)", Value = "Name ASC" });
                sl.Add(new select() { Text = "Name (Z to A)", Value = "Name DESC" });
                sl.Add(new select() { Text = "Title ID (Ascending)", Value = "Title_ID ASC" });
                sl.Add(new select() { Text = "Title ID (Descending)", Value = "Title_ID DESC" });
                sl.Add(new select() { Text = "Game - Update", Value = "Type ASC, Name" });
                sl.Add(new select() { Text = "Update - Game", Value = "Type DESC, Name" });
                sl.Add(new select() { Text = "NSP - XCI", Value = "Install ASC, Type, Name" });
                sl.Add(new select() { Text = "XCI - NSP", Value = "Install DESC, Type, Name" });
                sl.Add(new select() { Text = "Added to Database (Ascending)", Value = "ID ASC" });
                sl.Add(new select() { Text = "Added to Database (Descending)", Value = "ID DESC" });
                comboBox1.DataSource = sl;
                comboBox1.DisplayMember = "Text";
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        //when the comobox is selected - change the order of the listview box
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox_Search.Text = "";
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
                    CommandText = "SELECT * from Games ORDER BY " + t1 + ""
                };

                SQLiteDataReader sqReader = cmd.ExecuteReader();

                listView1.View = View.Details;
                listView1.Columns.Add("Name");
                listView1.Columns.Add("Size");
                listView1.Columns.Add("Title ID");
                listView1.Columns.Add("Revision");
                listView1.Columns.Add("Install");
                listView1.Columns.Add("Type");
                //listView1.Columns.Add("ID");

                while (sqReader.Read())
                {
                    string listname = (sqReader.GetString(1));
                    //listname = listname.ToLower(); //make name lowercase
                    listname = (char.ToUpper(listname[0]) + listname.Substring(1)); //make first letter uppercase
                    string listsize = (sqReader.GetString(2));
                    string listtitle = (sqReader.GetString(3));
                    string listrev = (sqReader.GetString(4));
                    string listtype = (sqReader.GetString(5));
                    string listinstall = (sqReader.GetString(6));
                    int listid = (sqReader.GetInt32(0));
                    string id = listid.ToString();
                    listView1.Items.Add(new ListViewItem(new string[] { listname, listsize, listtitle, listrev, listtype, listinstall, id }));
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

        private void googleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            string gamename = (item.SubItems[0].Text);

            try
            {
                if (string.IsNullOrEmpty(gamename))
                {
                    MessageBox.Show("Select a game first!");
                }
                else
                {
                    System.Diagnostics.Process.Start("http://www.google.com/search?q=" + gamename + " nintendo switch");
                }
            }

            catch
            {
                MessageBox.Show("Unable to search Google for " + gamename);
            }
        }

        private void youtubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            string gamename = (item.SubItems[0].Text);

            try
            {
                if (string.IsNullOrEmpty(gamename))
                {
                    MessageBox.Show("Select a game first!");
                }
                else
                {
                    System.Diagnostics.Process.Start("https://www.youtube.com/results?search_query=" + gamename + " nintendo switch");
                }
            }

            catch
            {
                MessageBox.Show("Unable to search Youtube for " + gamename);
            }
        }

        private void nintendoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            string gamename = (item.SubItems[0].Text);

            try
            {
                if (string.IsNullOrEmpty(gamename))
                {
                    MessageBox.Show("Select a game first!");
                }
                else
                {
                    System.Diagnostics.Process.Start("https://www.nintendo.com/search/#category=all&page=1&query=" + gamename);
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void tinfoilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            string gamename = (item.SubItems[2].Text);

            try
            {
                if (string.IsNullOrEmpty(gamename))
                {
                    MessageBox.Show("Select a game first!");
                }
                else
                {
                    System.Diagnostics.Process.Start("https://tinfoil.io/Title/" + gamename);
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            string gameid = (item.SubItems[0].Text);

            try
            {
                if (!string.IsNullOrEmpty(gameid))
                {
                    textBox_Search.Text = gameid;
                    Clipboard.SetText(item.SubItems[2].Text); //copy title id to clipboard for debugging...
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
                textBox_Search.Text = "";
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e) //start impementing print feature - finish this later
        {
            PrintDocument PrintDocument = new PrintDocument();
            PrintDocument.PrintPage += (object sender, PrintPageEventArgs e) =>
            {
                Font font = new Font("Arial", 12);
                float offset = e.MarginBounds.Top;
                foreach (ListViewItem Item in listView1.Items)
                {
                    // The 5.0f is to add a small space between lines
                    offset += (font.GetHeight() + 5.0f);
                    PointF location = new System.Drawing.PointF(e.MarginBounds.Left, offset);
                    e.Graphics.DrawString(Item.Text, font, Brushes.Black, location);
                }
            };
            PrintDocument.Print();
        }
    }

    // class added to store data for the combobox
    class select
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
