using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//ftp settings page - to store the ftp settings in the registry

namespace XCI_Explorer.XCI_Explorer
{
    public partial class ftp : Form
    {
        public ftp()
        {
            InitializeComponent();
            getkey();
        }

        private void getkey()
        {
            try
            {
                string ip = "";
                string port = "";
                string sx = "";
                string atmos = "";

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\XCI-Explorer");

                //if it does exist, retrieve the stored values  
                if (key != null)
                {
                    textBox_IP.Text = ((string)key.GetValue("IP"));
                    textBox_Port.Text = ((string)key.GetValue("Port"));
                    sx = ((string)key.GetValue("SXOS"));
                    atmos = ((string)key.GetValue("Atmos"));
                    key.Close();

                    if (sx == "1")
                    {
                        checkBox_SXOS.Checked = true;
                    }

                    if (atmos == "1")
                    {
                        checkBox_Atmos.Checked = true;
                    }

                }

                else
                {
                    //if the registry key does not exist - just put in some values
                    textBox_IP.Text = "192.168.0.2";
                    textBox_Port.Text = "5000";
                    checkBox_SXOS.Checked = true;
                    checkBox_Atmos.Checked = false;
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        } //get the current ftp settings from the registry

        private void sendkey() //update the registry key settings when the form closes
        {
            try
            {
                string ip = textBox_IP.Text;
                string port = textBox_Port.Text;
                string sx = "";
                string atmos = "";

                if (checkBox_SXOS.Checked)
                {
                    sx = "1";
                }

                else
                {
                    sx = "0";
                }

                if (checkBox_Atmos.Checked)
                {
                    atmos = "1";
                }

                else
                {
                    atmos = "0";
                }

                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\XCI-Explorer");

                //storing the values  
                key.SetValue("IP", ip);
                key.SetValue("Port", port);
                key.SetValue("SXOS", sx);
                key.SetValue("Atmos", atmos);
                key.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void ftp_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save values to the registry when we close the form
            sendkey();
        }
    }
}
