using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace XCI_Explorer
{
    public partial class MD5Check : Form
    {
        // code to add progress bar: https://github.com/MehdiBela/BetterCoderMD5HashProgress/blob/master/Form1.cs
        public static string SetValueForMD5 = "";

        public MD5Check()
        {
            InitializeComponent();
        }

        private void MD5Check_Load(object sender, EventArgs e)
        {
            bool b1 = string.IsNullOrEmpty(MainForm.SetValueForText2);
            if (!b1)
            {
                textBox1.Text = ("Game: " + MainForm.SetValueForText2);
            }
            else
            {
                //show filename instead of the game name
                textBox1.Text = (MainForm.SetValueForText1);
                MessageBox.Show("This is probably a DLC file or the file is corrupted.", "Game name not found!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                textBox2.Text = ("Calculating MD5 Value - Please Wait!");
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                textBox2.Text = ("Dude, I'm already busy - Please Wait!");
                backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string fullPath;
            try
            {
                fullPath = MainForm.SetValueForText1;
                using (var md5 = MD5.Create())
                {
                    using (var keyfile = File.OpenRead(fullPath))
                    using (FileStream stream = keyfile)
                    using (var bufferedStream = new BufferedStream(stream, 1024 * 32))
                    {
                        if (!fullPath.Equals(""))
                        {
                            string output = "";
                            //byte[] checksum = md5.ComputeHash(keyfile);
                            byte[] checksum = md5.ComputeHash(bufferedStream);
                            output = BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
                            string myval = (output);
                            SetValueForMD5 = myval;

                            //Use this code if we want SHA256 values instead of MDA - we can also use a buffer.
                            /*
                            string output = "";
                            var sha = new SHA256Managed();
                            byte[] checksum = sha.ComputeHash(bufferedStream);
                            output = BitConverter.ToString(checksum).Replace("-", String.Empty);
                            string myval = (output);
                            SetValueForMD5 = myval;
                            */
                        }
                        else
                        {
                            MessageBox.Show("Can't read md5 of file");
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string mymd5 = SetValueForMD5;
            textBox2.Text = ("MD5: " + mymd5.ToUpper());
            textBox2.ForeColor = Color.Black;
            Clipboard.SetText(SetValueForMD5);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
                this.Close();
            }
        }
    }
}
