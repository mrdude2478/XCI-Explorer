using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using XCI_Explorer.Helpers;
using XCI_Explorer.XCI_Explorer;
using XTSSharp;

namespace XCI_Explorer
{
    public partial class MainForm : Form
    {
        public List<char> chars = new List<char>();
        public static string SetValueForText1 = "";
        public static string SetValueForText2 = "";
        public byte[] NcaHeaderEncryptionKey1_Prod;
        public byte[] NcaHeaderEncryptionKey2_Prod;
        public new bool ShowIcon { get; set; }
        public string Mkey;
        public double UsedSize;
        private Image[] Icons = new Image[16];
        private string[] Language = new string[16] {
            "American English",
            "British English",
            "Japanese",
            "French",
            "German",
            "Latin American Spanish",
            "Spanish",
            "Italian",
            "Dutch",
            "Canadian French",
            "Portuguese",
            "Russian",
            "Korean",
            "Taiwanese",
            "Chinese",
            "???"
        };

        public static string mydb = ("tools/bin/links.db");
        public static string mydatabase = ("Data Source=" + mydb);

        public MainForm()
        {
            InitializeComponent();
            makedatabase(); //make database tables if they are missing.
            comboBox1_additems();

            this.Text = "XCI Explorer v" + getAssemblyVersion() + "d (Mod)";
            tabControl1.Controls.Remove(Partitions); //disable partitions tab
            tabControl1.Controls.Remove(nxci);//disable 4nxci
            tabControl1.Controls.Remove(ReKey);//disable 4nxci
            tabControl1.Controls.Remove(Spliter);//disable 4nxci
            //menuStrip1.BackColor = Color.DeepSkyBlue;
            //menuStrip1.ForeColor = Color.Black;
            //TB_File.AllowDrop = true;
            certToolStripMenuItem.Visible = false;
            LB_SelectedData.Text = "";
            LB_DataOffset.Text = "";
            LB_DataSize.Text = "";
            LB_HashedRegionSize.Text = "";
            LB_ActualHash.Text = "";
            LB_ExpectedHash.Text = "";

            //MAC - Set Current Directory to application directory so it can find the keys
            String startupPath = Application.StartupPath;
            Directory.SetCurrentDirectory(startupPath);


            try
            {
                if (!File.Exists("tools/bin/keys.dat"))
                {
                    string xyz = Microsoft.VisualBasic.Interaction.InputBox("Please enter the password to download keys.dat", "Password", "");

                    string hash = CalculateMD5Hash(xyz);
                    string pass = "D218C0A0995E571C8B12B768E75E48A1"; //mrdude

                    if (hash == pass)
                    {
                        using (var client = new WebClient())
                        {

                            client.DownloadFile(Util.Base64Decode("aHR0cHM6Ly9wYXN0ZWJpbi5jb20vcmF3L1ROeThTWENp"), "tools/bin/keys.dat");
                        }

                        using (var md5 = MD5.Create())
                        {
                            using (var keyfile = File.OpenRead("tools/bin/keys.dat"))
                            {
                                string output = "";
                                byte[] checksum = md5.ComputeHash(keyfile);
                                output = BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
                                string md5val = "e3e8e3519d55b7ec87a0f7280db80003";
                                if (!output.Equals(md5val))
                                {
                                    MessageBox.Show("Downloaded tools/bin/keys.dat MD5 Check is wrong\n\nPlease add them manually instead");
                                    File.Delete("tools/bin/keys.dat");
                                    Environment.Exit(0);
                                }

                            }
                        }

                        if (!File.Exists("tools/bin/keys.dat"))
                        {
                            MessageBox.Show("Please include tools/bin/keys.dat");
                            //Environment.Exit(0);
                        }
                    }

                    else
                    {
                        MessageBox.Show("Put keys.dat into the tools/bin directory to never show the password box again.", "Wrong Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
                Environment.Exit(0);
            }

            if (!File.Exists($"tools{Path.DirectorySeparatorChar}/bin/hactool.exe"))
            {
                Directory.CreateDirectory("tools");
                MessageBox.Show("hactool.exe is missing.\nPlease include hactool.exe in the 'tools\bin' folder.");
                Environment.Exit(0);
            }

            getKey();

            //MAC - Set the double clicked file name into the UI and process file
            String[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                TB_File.Text = args[1];
                Application.DoEvents();
                ProcessFile();
            }
        }

        private string getAssemblyVersion()
        {
            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string[] versionArray = assemblyVersion.Split('.');

            assemblyVersion = string.Join(".", versionArray.Take(3));

            return assemblyVersion;
        }

        private void getKey()
        {
            try
            {
                string text = (from x in File.ReadAllLines("tools/bin/keys.dat")
                               select x.Split('=') into x
                               where x.Length > 1
                               select x).ToDictionary((string[] x) => x[0].Trim(), (string[] x) => x[1])["header_key"].Replace(" ", "");
                NcaHeaderEncryptionKey1_Prod = Util.StringToByteArray(text.Remove(32, 32));
                NcaHeaderEncryptionKey2_Prod = Util.StringToByteArray(text.Remove(0, 32));
            }
            catch
            {
                MessageBox.Show("Unable to read keys from tools/bin/keys.dat - check your file for errors\nmost features will not work until you add a working tools/bin/keys.dat file.", "No keys!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Environment.Exit(0);
            }
        }

        public bool getMKey()
        {
            Dictionary<string, string> dictionary = (from x in File.ReadAllLines("tools/bin/keys.dat")
                                                     select x.Split('=') into x
                                                     where x.Length > 1
                                                     select x).ToDictionary((string[] x) => x[0].Trim(), (string[] x) => x[1]);
            Mkey = "master_key_";
            string MkeyL = "master_key_";
            if (NCA.NCA_Headers[0].MasterKeyRev == 0 || NCA.NCA_Headers[0].MasterKeyRev == 1)
            {
                Mkey += "00";
            }
            else if (NCA.NCA_Headers[0].MasterKeyRev < 17)
            {
                int num = NCA.NCA_Headers[0].MasterKeyRev - 1;
                string capchar = num.ToString("X");
                Mkey = Mkey + "0" + capchar;
                char cache1 = capchar[0];
                int cache2 = cache1 + 32;
                char lowchar = (char)cache2;
                MkeyL = MkeyL + "0" + lowchar;
            }
            else if (NCA.NCA_Headers[0].MasterKeyRev >= 17)
            {
                int num2 = NCA.NCA_Headers[0].MasterKeyRev - 1;
                Mkey += num2.ToString();
            }
            try
            {
                Mkey = dictionary[Mkey].Replace(" ", "");
                return true;
            }
            catch
            {
                int flag = 1;
            }
            try
            {
                MkeyL = dictionary[MkeyL].Replace(" ", "");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ProcessFile()
        {
            try
            {
                LB_SelectedData.Text = "";
                LB_DataOffset.Text = "";
                LB_DataSize.Text = "";
                LB_HashedRegionSize.Text = "";
                LB_ExpectedHash.Text = "";
                LB_ActualHash.Text = "";
                B_Extract.Enabled = false;

                if (CheckNSP())
                {
                    B_TrimXCI.Enabled = false;
                    ShowMD5.Enabled = true;
                    button_add_DB.Enabled = true;
                    tabControl1.Controls.Remove(Partitions);
                    tabControl1.Controls.Remove(nxci);
                    if (tabControl1.TabPages.Contains(ReKey) == false)
                    {
                        tabControl1.Controls.Add(ReKey);
                    }
                    LoadNSP();
                }

                else if (CheckXCI())
                {
                    B_TrimXCI.Enabled = true;
                    ShowMD5.Enabled = true;
                    button_add_DB.Enabled = true;
                    //don't add tab page if it already exists,
                    if (tabControl1.TabPages.Contains(Partitions) == false)
                    {
                        tabControl1.Controls.Add(Partitions);
                    }
                    if (tabControl1.TabPages.Contains(nxci) == false)
                    {
                        tabControl1.Controls.Add(nxci);
                    }
                    LoadXCI();
                }
                else
                {
                    TB_File.Text = null;
                    MessageBox.Show("Unsupported file.");
                }

                //emergency remove these if other code fails
                if (File.Exists("meta"))
                {
                    File.Delete("meta");
                }

                if (Directory.Exists("data"))
                {
                    Directory.Delete("data", true);
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("An error shows that, " + error.Message + "\n\nThe loaded file is probably corrupt or unsupported!");
            }
        }

        private void LoadXCI()
        {
            string[] array = new string[5]
            {
                "B",
                "KB",
                "MB",
                "GB",
                "TB"
            };
            double num = (double)new FileInfo(TB_File.Text).Length;
            TB_ROMExactSize.Text = "(" + num.ToString() + " bytes)";
            int num2 = 0;
            while (num >= 1024.0 && num2 < array.Length - 1)
            {
                num2++;
                num /= 1024.0;
            }
            TB_ROMSize.Text = $"{num:0.##} {array[num2]}";
            double num3 = UsedSize = (double)(XCI.XCI_Headers[0].CardSize2 * 512 + 512);
            TB_ExactUsedSpace.Text = "(" + num3.ToString() + " bytes)";

            //disable trimming if both strings are exact
            bool compstrings = (TB_ROMExactSize.Text.Equals(TB_ExactUsedSpace.Text));

            if (compstrings)
            {
                B_TrimXCI.Enabled = false;
            }

            num2 = 0;
            while (num3 >= 1024.0 && num2 < array.Length - 1)
            {
                num2++;
                num3 /= 1024.0;
            }
            TB_UsedSpace.Text = $"{num3:0.##} {array[num2]}";
            TB_Capacity.Text = Util.GetCapacity(XCI.XCI_Headers[0].CardSize1);
            LoadPartitions();
            LoadNCAData();
            LoadGameInfos();
        }

        public void LoadNSP()
        {
            CB_RegionName.Items.Clear();
            CB_RegionName.Enabled = true;
            TB_TID.Text = "";
            TB_Capacity.Text = "";
            TB_MKeyRev.Text = "";
            TB_SDKVer.Text = "";
            TB_GameRev.Text = "";
            TB_ProdCode.Text = "";
            TB_Name.Text = "";
            TB_Dev.Text = "";
            PB_GameIcon.BackgroundImage = null;
            Array.Clear(Icons, 0, Icons.Length);
            TV_Partitions.Nodes.Clear();
            FileInfo fi = new FileInfo(TB_File.Text);
            string contentType = "";

            // Maximum number of files in NSP to read in
            const int MAXFILES = 250;

            //Get File Size
            string[] array_fs = new string[5] { "B", "KB", "MB", "GB", "TB" };
            double num_fs = (double)fi.Length;
            int num2_fs = 0;
            TB_ROMExactSize.Text = "(" + num_fs.ToString() + " bytes)";
            TB_ExactUsedSpace.Text = TB_ROMExactSize.Text;

            while (num_fs >= 1024.0 && num2_fs < array_fs.Length - 1)
            {
                num2_fs++;
                num_fs /= 1024.0;
            }
            TB_ROMSize.Text = $"{num_fs:0.##} {array_fs[num2_fs]}";
            TB_UsedSpace.Text = TB_ROMSize.Text;

            Process process = new Process();
            try
            {
                certToolStripMenuItem.Visible = false;
                FileStream fileStream = File.OpenRead(TB_File.Text);
                string ncaTarget = "";
                string xmlVersion = "";

                List<char> chars = new List<char>();
                byte[] array = new byte[16];
                byte[] array2 = new byte[24];
                fileStream.Read(array, 0, 16);
                PFS0.PFS0_Headers[0] = new PFS0.PFS0_Header(array);
                if (!PFS0.PFS0_Headers[0].Magic.Contains("PFS0"))
                {
                    return;
                }
                PFS0.PFS0_Entry[] array3;
                array3 = new PFS0.PFS0_Entry[Math.Max(PFS0.PFS0_Headers[0].FileCount, MAXFILES)]; //Dump of TitleID 01009AA000FAA000 reports more than 10000000 files here, so it breaks the program. Standard is to have only 20 files

                for (int m = 0; m < PFS0.PFS0_Headers[0].FileCount; m++)
                {
                    fileStream.Position = 16 + 24 * m;
                    fileStream.Read(array2, 0, 24);
                    array3[m] = new PFS0.PFS0_Entry(array2);

                    if (m == MAXFILES - 1) //Dump of TitleID 01009AA000FAA000 reports more than 10000000 files here, so it breaks the program. Standard is to have only 20 files
                    {
                        break;
                    }
                }
                for (int n = 0; n < PFS0.PFS0_Headers[0].FileCount; n++)
                {
                    fileStream.Position = 16 + 24 * PFS0.PFS0_Headers[0].FileCount + array3[n].Name_ptr;
                    int num4;
                    while ((num4 = fileStream.ReadByte()) != 0 && num4 != 0)
                    {
                        chars.Add((char)num4);
                    }
                    array3[n].Name = new string(chars.ToArray());
                    chars.Clear();

                    // Console.WriteLine("FC: " + PFS0.PFS0_Headers[0].FileCount.ToString() + " Name: " + array3[n].Name);

                    if (array3[n].Name.EndsWith(".cnmt.xml"))
                    {
                        byte[] array4 = new byte[array3[n].Size];
                        fileStream.Position = 16 + 24 * PFS0.PFS0_Headers[0].FileCount + PFS0.PFS0_Headers[0].StringTableSize + array3[n].Offset;
                        fileStream.Read(array4, 0, (int)array3[n].Size);

                        XDocument xml = XDocument.Parse(Encoding.UTF8.GetString(array4));
                        TB_TID.Text = xml.Element("ContentMeta").Element("Id").Value.Remove(1, 2).ToUpper();
                        contentType = xml.Element("ContentMeta").Element("Type").Value;

                        if (contentType == "Patch")
                            xmlVersion = "v" + xml.Element("ContentMeta").Element("Version").Value;

                        if (contentType != "AddOnContent")
                        {
                            foreach (XElement xe in xml.Descendants("Content"))
                            {
                                if (xe.Element("Type").Value != "Control")
                                {
                                    continue;
                                }

                                ncaTarget = xe.Element("Id").Value + ".nca";
                                break;
                            }
                        }
                        else //This is a DLC
                        {
                            foreach (XElement xe in xml.Descendants("Content"))
                            {
                                if (xe.Element("Type").Value != "Meta")
                                {
                                    continue;
                                }

                                ncaTarget = xe.Element("Id").Value + ".cnmt.nca";
                                break;
                            }
                        }
                    }

                    if (n == MAXFILES - 1) //Dump of TitleID 01009AA000FAA000 reports more than 10000000 files here, so it breaks the program. Standard is to have only 20 files
                    {
                        break;
                    }
                }

                if (String.IsNullOrEmpty(ncaTarget))
                {
                    //Missing content metadata xml. Read from content metadata nca instead
                    for (int n = 0; n < PFS0.PFS0_Headers[0].FileCount; n++)
                    {
                        if (array3[n].Name.EndsWith(".cnmt.nca"))
                        {
                            try
                            {
                                File.Delete("meta");
                                Directory.Delete("data", true);
                            }
                            catch { }

                            using (FileStream fileStream2 = File.OpenWrite("meta"))
                            {
                                fileStream.Position = 16 + 24 * PFS0.PFS0_Headers[0].FileCount + PFS0.PFS0_Headers[0].StringTableSize + array3[n].Offset;
                                byte[] buffer = new byte[8192];
                                long num = array3[n].Size;
                                int num4;
                                while ((num4 = fileStream.Read(buffer, 0, 8192)) > 0 && num > 0)
                                {
                                    fileStream2.Write(buffer, 0, num4);
                                    num -= num4;
                                }
                                fileStream2.Close();
                            }

                            process = new Process();
                            process.StartInfo = new ProcessStartInfo
                            {
                                WindowStyle = ProcessWindowStyle.Hidden,
                                FileName = $"tools{Path.DirectorySeparatorChar}/bin/hactool.exe",
                                Arguments = "-k tools/bin/keys.dat --section0dir=data meta",
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            };
                            process.Start();

                            string masterkey = "";
                            while (!process.StandardOutput.EndOfStream)
                            {
                                string output = process.StandardOutput.ReadLine();
                                if (output.StartsWith("Master Key Revision"))
                                {
                                    masterkey = Regex.Replace(output, @"\s+", " ");
                                }
                            }
                            process.WaitForExit();

                            if (!Directory.Exists("data"))
                            {
                                MessageBox.Show(masterkey + " is missing!");
                            }
                            else
                            {
                                try
                                {
                                    string[] cnmt = Directory.GetFiles("data", "*.cnmt");
                                    if (cnmt.Length != 0)
                                    {
                                        using (FileStream fileStream3 = File.OpenRead(cnmt[0]))
                                        {
                                            byte[] buffer = new byte[32];
                                            byte[] buffer2 = new byte[56];
                                            CNMT.CNMT_Header[] array7 = new CNMT.CNMT_Header[1];

                                            fileStream3.Read(buffer, 0, 32);
                                            array7[0] = new CNMT.CNMT_Header(buffer);

                                            byte[] TitleID = BitConverter.GetBytes(array7[0].TitleID);
                                            Array.Reverse(TitleID);
                                            TB_TID.Text = BitConverter.ToString(TitleID).Replace("-", "");
                                            xmlVersion = "v" + array7[0].TitleVersion.ToString();

                                            if (array7[0].Type == (byte)CNMT.CNMT_Header.TitleType.REGULAR_APPLICATION)
                                            {
                                                contentType = "Application";
                                            }
                                            else if (array7[0].Type == (byte)CNMT.CNMT_Header.TitleType.UPDATE_TITLE)
                                            {
                                                contentType = "Patch";
                                            }
                                            else if (array7[0].Type == (byte)CNMT.CNMT_Header.TitleType.ADD_ON_CONTENT)
                                            {
                                                contentType = "AddOnContent";
                                            }

                                            fileStream3.Position = array7[0].Offset + 32;
                                            CNMT.CNMT_Entry[] array9 = new CNMT.CNMT_Entry[array7[0].ContentCount];
                                            for (int k = 0; k < array7[0].ContentCount; k++)
                                            {
                                                fileStream3.Read(buffer2, 0, 56);
                                                array9[k] = new CNMT.CNMT_Entry(buffer2);
                                                if (array9[k].Type == (byte)CNMT.CNMT_Entry.ContentType.CONTROL || array9[k].Type == (byte)CNMT.CNMT_Entry.ContentType.DATA)
                                                {
                                                    ncaTarget = BitConverter.ToString(array9[k].NcaId).ToLower().Replace("-", "") + ".nca";
                                                    break;
                                                }
                                            }

                                            fileStream3.Close();
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }

                for (int n = 0; n < PFS0.PFS0_Headers[0].FileCount; n++)
                {
                    if (array3[n].Name.Equals(ncaTarget))
                    {
                        Directory.CreateDirectory("tmp");

                        byte[] array5 = new byte[64 * 1024];
                        fileStream.Position = 16 + 24 * PFS0.PFS0_Headers[0].FileCount + PFS0.PFS0_Headers[0].StringTableSize + array3[n].Offset;

                        using (Stream output = File.Create($"tmp{Path.DirectorySeparatorChar}" + ncaTarget))
                        {
                            long Size = array3[n].Size;
                            int result = 0;
                            while ((result = fileStream.Read(array5, 0, (int)Math.Min(array5.Length, Size))) > 0)
                            {
                                output.Write(array5, 0, result);
                                Size -= result;
                            }
                        }

                        break;
                    }

                    if (n == MAXFILES - 1) //Dump of TitleID 01009AA000FAA000 reports more than 10000000 files here, so it breaks the program. Standard is to have only 20 files
                    {
                        break;
                    }
                }

                fileStream.Close();

                if (contentType != "AddOnContent")
                {
                    process = new Process();
                    process.StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = $"tools{Path.DirectorySeparatorChar}/bin/hactool.exe",
                        Arguments = "-k tools/bin/keys.dat --romfsdir=tmp tmp/" + ncaTarget
                    };

                    process.Start();
                    process.WaitForExit();
                    process.Close();
                    byte[] flux = new byte[200];

                    try
                    {
                        byte[] source = File.ReadAllBytes($"tmp{Path.DirectorySeparatorChar}control.nacp");
                        NACP.NACP_Datas[0] = new NACP.NACP_Data(source.Skip(0x3000).Take(0x1000).ToArray());

                        for (int i = 0; i < NACP.NACP_Strings.Length; i++)
                        {
                            NACP.NACP_Strings[i] = new NACP.NACP_String(source.Skip(i * 0x300).Take(0x300).ToArray());

                            if (NACP.NACP_Strings[i].Check != 0)
                            {
                                CB_RegionName.Items.Add(Language[i]);
                                string icon_filename = $"tmp{Path.DirectorySeparatorChar}icon_" + Language[i].Replace(" ", "") + ".dat";
                                if (File.Exists(icon_filename))
                                {
                                    using (Bitmap original = new Bitmap(icon_filename))
                                    {
                                        Icons[i] = new Bitmap(original);
                                        PB_GameIcon.BackgroundImage = Icons[i];
                                    }
                                }
                            }
                        }
                        if (xmlVersion.Trim() == "")
                        {
                            TB_GameRev.Text = NACP.NACP_Datas[0].GameVer.Replace("\0", "");
                        }
                        else
                        {
                            //TB_GameRev.Text = NACP.NACP_Datas[0].GameVer.Replace("\0", "") + " (" + xmlVersion + ")";

                            TB_GameRev.Text = xmlVersion + " (" + NACP.NACP_Datas[0].GameVer.Replace("\0", "") + ")";
                            string stringToCheck = xmlVersion;

                            int maxupdates = 50; //max number of game updates
                            string[] stringArray = new string[maxupdates];
                            int val = 65536;

                            for (int i = 0; i < maxupdates; i++)
                            {
                                stringArray[i] = (val).ToString();
                                val = (val + 65536);
                            }


                            foreach (string x in stringArray)
                            {
                                if (stringToCheck.Contains(x))
                                {
                                    int number = System.Convert.ToInt32(x);
                                    int divideby = 65536;
                                    TB_GameRev.Text = "Update " + ((double)number / divideby) + " (" + xmlVersion + ")";
                                }
                            }

                        }
                        TB_ProdCode.Text = NACP.NACP_Datas[0].GameProd.Replace("\0", "");
                        if (TB_ProdCode.Text == "")
                        {
                            TB_ProdCode.Text = "No Prod. ID";
                        }


                        for (int z = 0; z < NACP.NACP_Strings.Length; z++)
                        {
                            if (NACP.NACP_Strings[z].GameName.Replace("\0", "") != "")
                            {
                                TB_Name.Text = NACP.NACP_Strings[z].GameName.Replace("\0", "");
                                break;
                            }
                        }
                        for (int z = 0; z < NACP.NACP_Strings.Length; z++)
                        {
                            if (NACP.NACP_Strings[z].GameAuthor.Replace("\0", "") != "")
                            {
                                TB_Dev.Text = NACP.NACP_Strings[z].GameAuthor.Replace("\0", "");
                                break;
                            }
                        }
                    }
                    catch { }

                    /*if (contentType == "Patch") {
                    }*/
                }
                else
                {
                    TB_GameRev.Text = "";
                    TB_ProdCode.Text = "No Prod. ID";
                }

                // Lets get SDK Version, Distribution Type and Masterkey revision
                // This is far from the best aproach, but it's what we have for now
                process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = $"tools{Path.DirectorySeparatorChar}/bin/hactool.exe",
                    Arguments = "-k tools/bin/keys.dat tmp/" + ncaTarget,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                process.Start();
                StreamReader sr = process.StandardOutput;

                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] strArray;
                    str = sr.ReadLine();
                    strArray = str.Split(':');
                    if (strArray[0] == "SDK Version")
                    {
                        TB_SDKVer.Text = strArray[1].Trim();
                    }
                    else if (strArray[0] == "Master Key Revision")
                    {
                        string MasterKey = strArray[1].Trim();

                        if (MasterKey.Contains("Unknown"))
                        {
                            int keyblob;
                            if (int.TryParse(new string(MasterKey.TakeWhile(Char.IsDigit).ToArray()), out keyblob))
                            {
                                MasterKey = Util.GetMkey((byte)(keyblob + 1)).Replace("MasterKey", "");
                            }

                            String keyval = "MasterKey 0x" + MasterKey.Replace("MasterKey", "");
                            TB_MKeyRev.Text = keyval.ToUpper();
                        }
                        else
                        {
                            int keyvalue;
                            char masterkeychar = MasterKey[2];

                            if (masterkeychar - 'A' < 0)
                            {
                                keyvalue = masterkeychar - '0' + 1;
                            }
                            else
                            {
                                keyvalue = masterkeychar - 'A' + 10 + 1;
                            }

                            MasterKey = Util.GetMkey((byte)keyvalue);
                            String keyval = "MasterKey 0x" + MasterKey.Replace("MasterKey", "");
                            TB_MKeyRev.Text = keyval.ToUpper();
                        }
                        break;
                    }
                }
                process.WaitForExit();
                process.Close();
            }
            catch { }
            if (Directory.Exists("tmp"))
            {
                Directory.Delete("tmp", true);
            }

            TB_Capacity.Text = "eShop";

            if (TB_Name.Text.Trim() != "")
            {
                CB_RegionName.SelectedIndex = 0;
            }
        }

        private void LoadGameInfos()
        {
            CB_RegionName.Items.Clear();
            CB_RegionName.Enabled = true;
            TB_Name.Text = "";
            TB_Dev.Text = "";
            PB_GameIcon.BackgroundImage = null;
            Array.Clear(Icons, 0, Icons.Length);
            try
            {
                if (getMKey())
                {
                    certToolStripMenuItem.Visible = true;
                    using (FileStream fileStream = File.OpenRead(TB_File.Text))
                    {
                        List<string> ncaTarget = new List<string>();
                        string GameRevision = "";

                        for (int si = 0; si < SecureSize.Length; si++)
                        {
                            if (SecureSize[si] > 0x4E20000) continue;

                            if (SecureName[si].EndsWith(".cnmt.nca"))
                            {
                                if (File.Exists("meta"))
                                {
                                    File.Delete("meta");
                                }

                                if (Directory.Exists("data"))
                                {
                                    Directory.Delete("data", true);
                                }

                                using (FileStream fileStream2 = File.OpenWrite("meta"))
                                {
                                    fileStream.Position = SecureOffset[si];
                                    byte[] buffer = new byte[8192];
                                    long num = SecureSize[si];
                                    int num4;
                                    while ((num4 = fileStream.Read(buffer, 0, 8192)) > 0 && num > 0)
                                    {
                                        fileStream2.Write(buffer, 0, num4);
                                        num -= num4;
                                    }
                                    fileStream2.Close();
                                }

                                Process process = new Process();
                                process.StartInfo = new ProcessStartInfo
                                {
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    FileName = $"tools{Path.DirectorySeparatorChar}/bin/hactool.exe",
                                    Arguments = "-k tools/bin/keys.dat --section0dir=data meta"
                                };
                                process.Start();
                                process.WaitForExit();

                                string[] cnmt = Directory.GetFiles("data", "*.cnmt");
                                if (cnmt.Length != 0)
                                {
                                    using (FileStream fileStream3 = File.OpenRead(cnmt[0]))
                                    {
                                        byte[] buffer = new byte[32];
                                        byte[] buffer2 = new byte[56];
                                        CNMT.CNMT_Header[] array7 = new CNMT.CNMT_Header[1];

                                        fileStream3.Read(buffer, 0, 32);
                                        array7[0] = new CNMT.CNMT_Header(buffer);

                                        fileStream3.Position = array7[0].Offset + 32;
                                        CNMT.CNMT_Entry[] array9 = new CNMT.CNMT_Entry[array7[0].ContentCount];
                                        for (int k = 0; k < array7[0].ContentCount; k++)
                                        {
                                            fileStream3.Read(buffer2, 0, 56);
                                            array9[k] = new CNMT.CNMT_Entry(buffer2);
                                            if (array9[k].Type == (byte)CNMT.CNMT_Entry.ContentType.CONTROL)
                                            {
                                                ncaTarget.Add(BitConverter.ToString(array9[k].NcaId).ToLower().Replace("-", "") + ".nca");
                                                break;
                                            }
                                        }

                                        fileStream3.Close();
                                    }
                                }
                            }
                        }

                        for (int si = 0; si < SecureSize.Length; si++)
                        {
                            if (SecureSize[si] > 0x4E20000) continue;

                            if (ncaTarget.Contains(SecureName[si]))
                            {
                                if (File.Exists("meta"))
                                {
                                    File.Delete("meta");
                                }

                                if (Directory.Exists("data"))
                                {
                                    Directory.Delete("data", true);
                                }

                                using (FileStream fileStream2 = File.OpenWrite("meta"))
                                {
                                    fileStream.Position = SecureOffset[si];
                                    byte[] buffer = new byte[8192];
                                    long num = SecureSize[si];
                                    int num2;
                                    while ((num2 = fileStream.Read(buffer, 0, 8192)) > 0 && num > 0)
                                    {
                                        fileStream2.Write(buffer, 0, num2);
                                        num -= num2;
                                    }
                                    fileStream2.Close();
                                }

                                Process process = new Process();
                                process.StartInfo = new ProcessStartInfo
                                {
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    FileName = $"tools{Path.DirectorySeparatorChar}/bin/hactool.exe",
                                    Arguments = "-k tools/bin/keys.dat --romfsdir=data meta"
                                };
                                process.Start();
                                process.WaitForExit();

                                if (File.Exists($"data{Path.DirectorySeparatorChar}control.nacp"))
                                {
                                    byte[] source = File.ReadAllBytes($"data{Path.DirectorySeparatorChar}control.nacp");
                                    NACP.NACP_Datas[0] = new NACP.NACP_Data(source.Skip(0x3000).Take(0x1000).ToArray());

                                    string GameVer = NACP.NACP_Datas[0].GameVer.Replace("\0", "");
                                    Version version1, version2;
                                    if (!Version.TryParse(Regex.Replace(GameRevision, @"[^\d.].*$", ""), out version1))
                                    {
                                        version1 = new Version();
                                    }
                                    if (!Version.TryParse(Regex.Replace(GameVer, @"[^\d.].*$", ""), out version2))
                                    {
                                        version2 = new Version();
                                    }
                                    if (version2.CompareTo(version1) > 0)
                                    {
                                        GameRevision = GameVer;

                                        for (int i = 0; i < NACP.NACP_Strings.Length; i++)
                                        {
                                            NACP.NACP_Strings[i] = new NACP.NACP_String(source.Skip(i * 0x300).Take(0x300).ToArray());
                                            if (NACP.NACP_Strings[i].Check != 0 && !CB_RegionName.Items.Contains(Language[i]))
                                            {
                                                CB_RegionName.Items.Add(Language[i]);
                                                string icon_filename = $"data{Path.DirectorySeparatorChar}icon_" + Language[i].Replace(" ", "") + ".dat";
                                                if (File.Exists(icon_filename))
                                                {
                                                    using (Bitmap original = new Bitmap(icon_filename))
                                                    {
                                                        Icons[i] = new Bitmap(original);
                                                        PB_GameIcon.BackgroundImage = Icons[i];
                                                    }
                                                }
                                            }
                                        }
                                        TB_ProdCode.Text = NACP.NACP_Datas[0].GameProd;
                                        if (TB_ProdCode.Text == "")
                                        {
                                            TB_ProdCode.Text = "No Prod. ID";
                                        }
                                        try
                                        {
                                            File.Delete("meta");
                                            Directory.Delete("data", true);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }

                        TB_GameRev.Text = GameRevision;
                        CB_RegionName.SelectedIndex = 0;

                        fileStream.Close();
                    }
                }

                else
                {
                    TB_Dev.Text = Mkey + " not found";
                    TB_Name.Text = Mkey + " not found";
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void LoadNCAData()
        {
            NCA.NCA_Headers[0] = new NCA.NCA_Header(DecryptNCAHeader(gameNcaOffset));
            TB_TID.Text = "0" + NCA.NCA_Headers[0].TitleID.ToString("X");
            TB_SDKVer.Text = $"{NCA.NCA_Headers[0].SDKVersion4}.{NCA.NCA_Headers[0].SDKVersion3}.{NCA.NCA_Headers[0].SDKVersion2}.{NCA.NCA_Headers[0].SDKVersion1}";

            String keyval = Util.GetMkey(NCA.NCA_Headers[0].MasterKeyRev);
            keyval = "MasterKey 0x" + keyval.Replace("MasterKey", "");
            TB_MKeyRev.Text = keyval.ToUpper();



        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2 + 2);
            hex.Append("0x");
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string SHA256Bytes(byte[] ba)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] hashValue;
            hashValue = mySHA256.ComputeHash(ba);
            return ByteArrayToString(hashValue);
        }

        private void LoadPartitions()
        {
            string actualHash;
            byte[] hashBuffer;
            long offset;

            TV_Partitions.Nodes.Clear();
            TV_Parti = new TreeViewFileSystem(TV_Partitions);
            rootNode = new BetterTreeNode("root");
            rootNode.Offset = -1L;
            rootNode.Size = -1L;
            TV_Partitions.Nodes.Add(rootNode);
            bool LogoPartition = false;
            FileStream fileStream = new FileStream(TB_File.Text, FileMode.Open, FileAccess.Read);
            HFS0.HSF0_Entry[] array = new HFS0.HSF0_Entry[HFS0.HFS0_Headers[0].FileCount];
            fileStream.Position = XCI.XCI_Headers[0].HFS0OffsetPartition + 16 + 64 * HFS0.HFS0_Headers[0].FileCount;
            long num = XCI.XCI_Headers[0].HFS0OffsetPartition + XCI.XCI_Headers[0].HFS0SizeParition;
            byte[] array2 = new byte[64];
            byte[] array3 = new byte[16];
            byte[] array4 = new byte[24];
            for (int i = 0; i < HFS0.HFS0_Headers[0].FileCount; i++)
            {
                fileStream.Position = XCI.XCI_Headers[0].HFS0OffsetPartition + 16 + 64 * i;
                fileStream.Read(array2, 0, 64);
                array[i] = new HFS0.HSF0_Entry(array2);
                fileStream.Position = XCI.XCI_Headers[0].HFS0OffsetPartition + 16 + 64 * HFS0.HFS0_Headers[0].FileCount + array[i].Name_ptr;
                int num2;
                while ((num2 = fileStream.ReadByte()) != 0 && num2 != 0)
                {
                    chars.Add((char)num2);
                }
                array[i].Name = new string(chars.ToArray());
                chars.Clear();
                offset = num + array[i].Offset;
                hashBuffer = new byte[array[i].HashedRegionSize];
                fileStream.Position = offset;
                fileStream.Read(hashBuffer, 0, array[i].HashedRegionSize);
                actualHash = SHA256Bytes(hashBuffer);

                TV_Parti.AddFile(array[i].Name + ".hfs0", rootNode, offset, array[i].Size, array[i].HashedRegionSize, ByteArrayToString(array[i].Hash), actualHash);
                BetterTreeNode betterTreeNode = TV_Parti.AddDir(array[i].Name, rootNode);
                HFS0.HFS0_Header[] array5 = new HFS0.HFS0_Header[1];
                fileStream.Position = array[i].Offset + num;
                fileStream.Read(array3, 0, 16);
                array5[0] = new HFS0.HFS0_Header(array3);
                if (array[i].Name == "secure")
                {
                    SecureSize = new long[array5[0].FileCount];
                    SecureOffset = new long[array5[0].FileCount];
                    SecureName = new string[array5[0].FileCount];
                }
                if (array[i].Name == "normal")
                {
                    NormalSize = new long[array5[0].FileCount];
                    NormalOffset = new long[array5[0].FileCount];
                }
                if (array[i].Name == "logo")
                {
                    if (array5[0].FileCount > 0)
                    {
                        LogoPartition = true;
                    }
                }
                HFS0.HSF0_Entry[] array6 = new HFS0.HSF0_Entry[array5[0].FileCount];
                for (int j = 0; j < array5[0].FileCount; j++)
                {
                    fileStream.Position = array[i].Offset + num + 16 + 64 * j;
                    fileStream.Read(array2, 0, 64);
                    array6[j] = new HFS0.HSF0_Entry(array2);
                    fileStream.Position = array[i].Offset + num + 16 + 64 * array5[0].FileCount + array6[j].Name_ptr;
                    while ((num2 = fileStream.ReadByte()) != 0 && num2 != 0)
                    {
                        chars.Add((char)num2);
                    }
                    array6[j].Name = new string(chars.ToArray());
                    chars.Clear();
                    if (array[i].Name == "secure")
                    {
                        SecureSize[j] = array6[j].Size;
                        SecureOffset[j] = array[i].Offset + array6[j].Offset + num + 16 + array5[0].StringTableSize + array5[0].FileCount * 64;
                        SecureName[j] = array6[j].Name;
                    }
                    if (array[i].Name == "normal")
                    {
                        NormalSize[j] = array6[j].Size;
                        NormalOffset[j] = array[i].Offset + array6[j].Offset + num + 16 + array5[0].StringTableSize + array5[0].FileCount * 64;
                    }
                    offset = array[i].Offset + array6[j].Offset + num + 16 + array5[0].StringTableSize + array5[0].FileCount * 64;
                    hashBuffer = new byte[array6[j].HashedRegionSize];
                    fileStream.Position = offset;
                    fileStream.Read(hashBuffer, 0, array6[j].HashedRegionSize);
                    actualHash = SHA256Bytes(hashBuffer);

                    TV_Parti.AddFile(array6[j].Name, betterTreeNode, offset, array6[j].Size, array6[j].HashedRegionSize, ByteArrayToString(array6[j].Hash), actualHash);
                    TreeNode[] array7 = TV_Partitions.Nodes.Find(betterTreeNode.Text, true);
                    if (array7.Length != 0)
                    {
                        TV_Parti.AddFile(array6[j].Name, (BetterTreeNode)array7[0], 0L, 0L);
                    }
                }
            }
            long num3 = -9223372036854775808L;
            for (int k = 0; k < SecureSize.Length; k++)
            {
                if (SecureSize[k] > num3)
                {
                    gameNcaSize = SecureSize[k];
                    gameNcaOffset = SecureOffset[k];
                    num3 = SecureSize[k];
                }
            }
            PFS0Offset = gameNcaOffset + 32768;
            fileStream.Position = PFS0Offset;
            fileStream.Read(array3, 0, 16);
            PFS0.PFS0_Headers[0] = new PFS0.PFS0_Header(array3);
            if (PFS0.PFS0_Headers[0].FileCount == 2 || !LogoPartition)
            {
                PFS0.PFS0_Entry[] array8;
                try
                {
                    array8 = new PFS0.PFS0_Entry[PFS0.PFS0_Headers[0].FileCount];
                }
                catch (Exception ex)
                {
                    array8 = new PFS0.PFS0_Entry[0];
                    Debug.WriteLine("Partitions Error: " + ex.Message);
                }
                for (int m = 0; m < PFS0.PFS0_Headers[0].FileCount; m++)
                {
                    fileStream.Position = PFS0Offset + 16 + 24 * m;
                    fileStream.Read(array4, 0, 24);
                    array8[m] = new PFS0.PFS0_Entry(array4);
                    PFS0Size += array8[m].Size;
                }
                TV_Parti.AddFile("boot.psf0", rootNode, PFS0Offset, 16 + 24 * PFS0.PFS0_Headers[0].FileCount + 64 + PFS0Size);
                BetterTreeNode betterTreeNode2 = TV_Parti.AddDir("boot", rootNode);
                for (int n = 0; n < PFS0.PFS0_Headers[0].FileCount; n++)
                {
                    fileStream.Position = PFS0Offset + 16 + 24 * PFS0.PFS0_Headers[0].FileCount + array8[n].Name_ptr;
                    int num4;
                    while ((num4 = fileStream.ReadByte()) != 0 && num4 != 0)
                    {
                        chars.Add((char)num4);
                    }
                    array8[n].Name = new string(chars.ToArray());
                    chars.Clear();
                    TV_Parti.AddFile(array8[n].Name, betterTreeNode2, PFS0Offset + array8[n].Offset + 16 + PFS0.PFS0_Headers[0].StringTableSize + PFS0.PFS0_Headers[0].FileCount * 24, array8[n].Size);
                    TreeNode[] array9 = TV_Partitions.Nodes.Find(betterTreeNode2.Text, true);
                    if (array9.Length != 0)
                    {
                        TV_Parti.AddFile(array8[n].Name, (BetterTreeNode)array9[0], 0L, 0L);
                    }
                }
            }
            fileStream.Close();
        }

        private void TV_Partitions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BetterTreeNode betterTreeNode = (BetterTreeNode)TV_Partitions.SelectedNode;
            if (betterTreeNode.Offset != -1)
            {
                selectedOffset = betterTreeNode.Offset;
                selectedSize = betterTreeNode.Size;
                string expectedHash = betterTreeNode.ExpectedHash;
                string actualHash = betterTreeNode.ActualHash;
                long HashedRegionSize = betterTreeNode.HashedRegionSize;

                LB_DataOffset.Text = "Offset: 0x" + selectedOffset.ToString("X");
                LB_SelectedData.Text = "File: " + e.Node.Text;
                if (backgroundWorker1.IsBusy != true)
                {
                    B_Extract.Enabled = true;
                }
                string[] array = new string[5]
                {
                    "B",
                    "KB",
                    "MB",
                    "GB",
                    "TB"
                };
                double num = (double)selectedSize;
                int num2 = 0;
                while (num >= 1024.0 && num2 < array.Length - 1)
                {
                    num2++;
                    num /= 1024.0;
                }
                LB_DataSize.Text = "Size:   0x" + selectedSize.ToString("X") + " (" + num.ToString() + array[num2] + ")";

                if (HashedRegionSize != 0)
                {
                    LB_HashedRegionSize.Text = "HashedRegionSize: 0x" + HashedRegionSize.ToString("X");
                }
                else
                {
                    LB_HashedRegionSize.Text = "";
                }

                if (!string.IsNullOrEmpty(expectedHash))
                {
                    LB_ExpectedHash.Text = "Header Hash: " + expectedHash.Substring(0, 32);
                }
                else
                {
                    LB_ExpectedHash.Text = "";
                }

                if (!string.IsNullOrEmpty(actualHash))
                {
                    LB_ActualHash.Text = "Actual Hash: " + actualHash.Substring(0, 32);
                    if (actualHash == expectedHash)
                    {
                        LB_ActualHash.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        LB_ActualHash.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    LB_ActualHash.Text = "";
                }

            }
            else
            {
                LB_SelectedData.Text = "";
                LB_DataOffset.Text = "";
                LB_DataSize.Text = "";
                LB_HashedRegionSize.Text = "";
                LB_ExpectedHash.Text = "";
                LB_ActualHash.Text = "";
                B_Extract.Enabled = false;
            }
        }

        public bool CheckXCI()
        {
            FileStream fileStream = new FileStream(TB_File.Text, FileMode.Open, FileAccess.Read);
            byte[] array = new byte[61440];
            byte[] array2 = new byte[16];
            fileStream.Read(array, 0, 61440);
            XCI.XCI_Headers[0] = new XCI.XCI_Header(array);
            if (!XCI.XCI_Headers[0].Magic.Contains("HEAD"))
            {
                return false;
            }
            fileStream.Position = XCI.XCI_Headers[0].HFS0OffsetPartition;
            fileStream.Read(array2, 0, 16);
            HFS0.HFS0_Headers[0] = new HFS0.HFS0_Header(array2);
            fileStream.Close();
            return true;
        }

        public bool CheckNSP()
        {
            try
            {
                FileStream fileStream = File.OpenRead(TB_File.Text);
                byte[] array = new byte[16];
                fileStream.Read(array, 0, 16);
                PFS0.PFS0_Headers[0] = new PFS0.PFS0_Header(array);
                fileStream.Close();
                if (!PFS0.PFS0_Headers[0].Magic.Contains("PFS0"))
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }

            catch
            {
                TB_File.Text = null;
                MessageBox.Show("Unsupported file.");
                return false;
            }
        }

        private void B_Extract_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string myText = LB_SelectedData.Text;
            string mynewText = myText.Replace("File:", "");
            saveFileDialog.FileName = mynewText;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (backgroundWorker1.IsBusy != true)
                {
                    B_Extract.Enabled = false;
                    B_TrimXCI.Enabled = false;

                    // Start the asynchronous operation.
                    backgroundWorker1.RunWorkerAsync(saveFileDialog.FileName);

                    MessageBox.Show("Extracting NCA\nPlease wait...");
                }
            }
        }

        public byte[] DecryptNCAHeader(long offset)
        {
            byte[] array = new byte[3072];
            if (File.Exists(TB_File.Text))
            {
                FileStream fileStream = new FileStream(TB_File.Text, FileMode.Open, FileAccess.Read);
                fileStream.Position = offset;
                fileStream.Read(array, 0, 3072);
                File.WriteAllBytes(TB_File.Text + ".tmp", array);
                Xts xts = XtsAes128.Create(NcaHeaderEncryptionKey1_Prod, NcaHeaderEncryptionKey2_Prod);
                using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(TB_File.Text + ".tmp")))
                {
                    using (XtsStream xtsStream = new XtsStream(binaryReader.BaseStream, xts, 512))
                    {
                        xtsStream.Read(array, 0, 3072);
                    }
                }
                File.Delete(TB_File.Text + ".tmp");
                fileStream.Close();
            }
            return array;
        }

        private void CB_RegionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int num = Array.FindIndex(Language, (string element) => element.StartsWith(CB_RegionName.Text, StringComparison.Ordinal));
                // Icons for 1-2 Switch in some languages are "missing"
                // This just shows the first real icon instead of a blank
                if (Icons[num] != null)
                {
                    PB_GameIcon.BackgroundImage = Icons[num];
                }
                else
                {
                    for (int i = 0; i < CB_RegionName.Items.Count; i++)
                    {
                        if (Icons[i] != null)
                        {
                            PB_GameIcon.BackgroundImage = Icons[i];
                            break;
                        }
                    }
                }
                TB_Name.Text = NACP.NACP_Strings[num].GameName;
                TB_Dev.Text = NACP.NACP_Strings[num].GameAuthor;
            }
            catch
            {
                MessageBox.Show("Unable to read index - maybe the file is corrupt");
            }
        }

        private void B_TrimXCI_Click(object sender, EventArgs e)
        {
            if (Util.checkFile(TB_File.Text))
            {
                if (MessageBox.Show("Trim XCI?", "XCI Explorer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!TB_ROMExactSize.Text.Equals(TB_ExactUsedSpace.Text))
                    {
                        FileStream fileStream = new FileStream(TB_File.Text, FileMode.Open, FileAccess.Write);
                        fileStream.SetLength((long)UsedSize);
                        fileStream.Close();
                        MessageBox.Show("Done.");
                        string[] array = new string[5]
                        {
                            "B",
                            "KB",
                            "MB",
                            "GB",
                            "TB"
                        };
                        double num = (double)new FileInfo(TB_File.Text).Length;
                        TB_ROMExactSize.Text = "(" + num.ToString() + " bytes)";
                        int num2 = 0;
                        while (num >= 1024.0 && num2 < array.Length - 1)
                        {
                            num2++;
                            num /= 1024.0;
                        }
                        TB_ROMSize.Text = $"{num:0.##} {array[num2]}";
                        double num3 = UsedSize = (double)(XCI.XCI_Headers[0].CardSize2 * 512 + 512);
                        TB_ExactUsedSpace.Text = "(" + num3.ToString() + " bytes)";
                        num2 = 0;
                        while (num3 >= 1024.0 && num2 < array.Length - 1)
                        {
                            num2++;
                            num3 /= 1024.0;
                        }
                        TB_UsedSpace.Text = $"{num3:0.##} {array[num2]}";
                    }
                    else
                    {
                        MessageBox.Show("No trimming needed!");
                    }
                }
            }
            else
            {
                MessageBox.Show("File not found");
            }
        }

        private void LB_ExpectedHash_DoubleClick(object sender, EventArgs e)
        {
            BetterTreeNode betterTreeNode = (BetterTreeNode)TV_Partitions.SelectedNode;
            if (betterTreeNode.Offset != -1)
            {
                Clipboard.SetText(betterTreeNode.ExpectedHash);
            }
        }

        private void LB_ActualHash_DoubleClick(object sender, EventArgs e)
        {
            BetterTreeNode betterTreeNode = (BetterTreeNode)TV_Partitions.SelectedNode;
            if (betterTreeNode.Offset != -1)
            {
                Clipboard.SetText(betterTreeNode.ActualHash);
            }
        }

        private void TB_File_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (backgroundWorker1.IsBusy != true)
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    TB_File.Text = files[0];
                    uint maxsize = (4294901760);
                    String inputFile = TB_File.Text;
                    if (TB_File.Text.Contains(".nsp") || TB_File.Text.Contains(".xci") || TB_File.Text.Contains(".nsz"))
                    {
                        ProcessFile();
                        Rename.Enabled = true;
                        button_add_DB.Enabled = true;

                        using (Stream input = File.OpenRead(inputFile))
                        {
                            if (maxsize < input.Length)
                            {
                                if (tabControl1.TabPages.Contains(Spliter) == false)
                                {
                                    tabControl1.Controls.Add(Spliter);
                                }
                            }
                            else
                            {
                                if (tabControl1.TabPages.Contains(Spliter) == true)
                                {
                                    tabControl1.Controls.Remove(Spliter);
                                }
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("This program only supports - xci, nsp & nsz files.");
                    }
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void TB_File_DragEnter(object sender, DragEventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string fileName = (string)e.Argument;

            using (FileStream fileStream = File.OpenRead(TB_File.Text))
            {
                using (FileStream fileStream2 = File.OpenWrite(fileName))
                {
                    new BinaryReader(fileStream);
                    new BinaryWriter(fileStream2);
                    fileStream.Position = selectedOffset;
                    byte[] buffer = new byte[8192];
                    long num = selectedSize;
                    int num2;
                    while ((num2 = fileStream.Read(buffer, 0, 8192)) > 0 && num > 0)
                    {
                        fileStream2.Write(buffer, 0, num2);
                        num -= num2;
                    }
                    fileStream.Close();
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            B_Extract.Enabled = true;
            B_TrimXCI.Enabled = true;

            if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                MessageBox.Show("Done extracting NCA!");
            }
        }

        private void RenameGame_Click(object sender, EventArgs e)
        {
            string myText = TB_Name.Text;
            if (!String.IsNullOrEmpty(myText))
            {
                string mynewText = myText.Replace(":", "").Replace("*", "").Replace("?", "").Replace("/", "").Replace("|", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace("\"", ""); //remove illegal char names on windows
                string mynewText2 = (mynewText);
                mynewText2 = (char.ToUpper(mynewText2[0]) + mynewText2.Substring(1)); //make first letter uppercase
                string extension;
                string fullPath;
                string filename;
                string gameid;
                string gameversion;
                if (checkBox1.Checked)
                {
                    gameid = (" [" + TB_TID.Text + "]");
                }
                else
                {
                    gameid = ("");
                }
                if (checkBox2.Checked)
                {
                    if (TB_GameRev.Text.Contains("Update"))
                    {
                        gameversion = (" [" + TB_GameRev.Text + "]");
                    }
                    else
                    {
                        gameversion = (" [Version " + TB_GameRev.Text + "]");
                    }
                }
                else
                {
                    gameversion = ("");
                }
                fullPath = Path.GetFullPath(TB_File.Text);
                extension = Path.GetExtension(TB_File.Text);
                filename = Path.GetFileName(TB_File.Text);
                string removefile = fullPath.Replace(filename, ""); //remove spaces
                string myfile = (removefile + mynewText2 + gameid + gameversion + extension);
                //Clipboard.SetText(myfile); //copy text to clipboard
                File.Move(TB_File.Text, myfile);
                MessageBox.Show("Renamed game to: " + mynewText2 + gameid + gameversion, "Game Renamed", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                Rename.Enabled = false;
                //button_add_DB.Enabled = false;
                ShowMD5.Enabled = false;
                tabControl1.Controls.Remove(nxci);//disable 4nxci
                tabControl1.Controls.Remove(ReKey);//disable 4nxci
                tabControl1.Controls.Remove(Spliter);//disable 4nxci

            }
            else
            {
                MessageBox.Show("Load a game first", "Nothing to rename", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void ShowKeys_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists("tools/bin/keys.dat"))
                {
                    MessageBox.Show("tools/bin/keys.dat does not exist");
                }
                else
                {
                    string filetext = File.ReadAllText("tools/bin/keys.dat");
                    richTextBox1.Text = filetext;
                }
            }
            catch
            {
                MessageBox.Show("Unable to show keys");
            }
        }

        private void WriteKeys_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("You are about to overwrite the contents of tools/bin/keys.dat with the content show in this page.\n\nPress Yes to do this or No to preserve your current tools/bin/keys.dat file.", "XCI Explorer", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string RichTextBoxContents = richTextBox1.Text;
                    File.WriteAllText("tools/bin/keys.dat", RichTextBoxContents);
                }
                else
                {
                    //do nothing
                }
            }
            catch
            {
                MessageBox.Show("Unable to write to tools/bin/keys.dat");
            }
        }

        private void SortKeys_Click(object sender, EventArgs e)
        {

            try
            {
                if (!File.Exists("tools/bin/keys.dat"))
                {
                    MessageBox.Show("tools/bin/keys.dat does not exist");
                }
                else
                {
                    richTextBox1.Clear();
                    string inFile = "tools/bin/keys.dat";
                    var contents = File.ReadAllLines(inFile);
                    Array.Sort(contents);
                    for (int i = 0; i < contents.Length;)
                    {
                        richTextBox1.Text += contents[i];
                        richTextBox1.Text += ("\n"); //add a newline to our array
                        i++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unable to sort keys");
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void googleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string gamename = TB_Name.Text;
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
            string gamename = TB_Name.Text;
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
            string gamename = TB_Name.Text;
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

        private void ShowMD5_Click(object sender, EventArgs e)
        {
            try
            {
                string fullPath;
                fullPath = Path.GetFullPath(TB_File.Text);
                SetValueForText1 = fullPath; //gamepath
                SetValueForText2 = TB_Name.Text; //gamename
                Form form2 = new MD5Check();
                form2.ShowDialog();

            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            base64RTB.Copy();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            base64RTB.Paste();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string BoxContents = base64RTB.Text;
                string decoded = Util.Base64Decode(BoxContents);
                base64RTB.Text = decoded;
            }

            catch
            {
                MessageBox.Show("Unable to Decode string", "Not a base64 encoded string", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string BoxContents = base64RTB.Text;
                string encoded = Util.Base64Encode(BoxContents);
                base64RTB.Text = encoded;
            }

            catch
            {
                MessageBox.Show("Unable to Encode string");
            }
        }

        private void base64RTB_LinkClicked(object sender, LinkClickedEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                base64RTB.Clear();
                base64RTB.Update();
            }

            catch
            {
                MessageBox.Show("Unable to clear");
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == Keys)
                {
                    ShowKeys_Click(sender, new EventArgs());
                }

                if (tabControl1.SelectedTab == nxci)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    outputstring.Text = path;
                }
                if (tabControl1.SelectedTab == ReKey)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    outputstring2.Text = path;
                }
                if (tabControl1.SelectedTab == Spliter)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    outputstring3.Text = path;
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                consolebox.Clear();
                using (Process sortProcess = new Process())
                {
                    String rename_id = " -r ";
                    String output = outputstring.Text;
                    String outdir = ("--outdir=" + '"' + output + '"' + " ");
                    String myargs = (@"-k tools/bin/keys.dat " + outdir + '"' + TB_File.Text + '"');
                    String exeName = $"tools{Path.DirectorySeparatorChar}/bin/4nxci.exe";

                    if (usetmp.Checked)
                    {
                        System.IO.Directory.CreateDirectory(output + "\\temp");
                        myargs = (myargs + " --t=" + '"' + output + "\\temp" + '"' + " ");
                    }

                    if (titnamecheck.Checked)
                    {
                        myargs = (myargs + rename_id);
                    }

                    if (keepncaid.Checked)
                    {
                        myargs = (myargs + " --keepncaid");
                    }

                    sortProcess.StartInfo.FileName = exeName;
                    sortProcess.StartInfo.Arguments = myargs;
                    sortProcess.StartInfo.CreateNoWindow = true;
                    sortProcess.StartInfo.UseShellExecute = false;
                    sortProcess.StartInfo.RedirectStandardOutput = true;

                    // Set event handler
                    sortProcess.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);

                    // Start the process.
                    sortProcess.Start();

                    // Start the asynchronous read
                    sortProcess.BeginOutputReadLine();
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        void SortOutputHandler(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine(e.Data);
            this.BeginInvoke(new MethodInvoker(() =>
            {
                consolebox.AppendText(e.Data ?? string.Empty);
                consolebox.AppendText("\n");
                consolebox.ScrollToCaret();
            }));

        }

        void SortOutputHandler2(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine(e.Data);
            this.BeginInvoke(new MethodInvoker(() =>
            {
                consolebox2.AppendText(e.Data ?? string.Empty);
                consolebox2.AppendText("\n");
                consolebox2.ScrollToCaret();
            }));

        }

        private void OutputDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                String folderName = openFolderDialog.SelectedPath;
                outputstring.Text = folderName;
                if (String.IsNullOrEmpty(outputstring.Text))
                {
                    Convert.Enabled = false;
                }
                else
                {
                    Convert.Enabled = true;
                }
            }
        }

        private void Convert2_Click(object sender, EventArgs e)
        {
            try
            {
                consolebox2.Clear();
                using (Process sortProcess = new Process())
                {
                    String output = outputstring2.Text;
                    String outdir = ("--outdir=" + '"' + output + '"' + " ");
                    String myargs = (@"-k tools/bin/keys.dat " + outdir + '"' + TB_File.Text + '"');
                    String exeName = $"tools{Path.DirectorySeparatorChar}/bin/renxpack.exe";

                    if (usetmp2.Checked)
                    {
                        System.IO.Directory.CreateDirectory(output + "\\temp");
                        myargs = (myargs + " --t=" + '"' + output + "\\temp" + '"' + " ");
                    }

                    sortProcess.StartInfo.FileName = exeName;
                    sortProcess.StartInfo.Arguments = myargs;
                    sortProcess.StartInfo.CreateNoWindow = true;
                    sortProcess.StartInfo.UseShellExecute = false;
                    sortProcess.StartInfo.RedirectStandardOutput = true;

                    // Set event handler
                    sortProcess.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler2);

                    // Start the process.
                    sortProcess.Start();

                    // Start the asynchronous read
                    sortProcess.BeginOutputReadLine();
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void OutputDir2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                String folderName = openFolderDialog.SelectedPath;
                outputstring2.Text = folderName;
                if (String.IsNullOrEmpty(outputstring2.Text))
                {
                    Convert2.Enabled = false;
                }
                else
                {
                    Convert2.Enabled = true;
                }
            }
        }

        private void consolebox_LinkClicked(object sender, LinkClickedEventArgs e)
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

        private void consolebox2_LinkClicked(object sender, LinkClickedEventArgs e)
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

        private void OutputSpilt_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                String folderName = openFolderDialog.SelectedPath;
                outputstring3.Text = folderName;

            }
        }

        private void Split_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => Splitxci()); //create a new task so we don't freeze the gui.
        }

        private void Splitxci()
        {
            String inputFile = TB_File.Text;
            string getextension = Path.GetExtension(inputFile);
            const int BUFFER_SIZE = (8 * 1024 * 1024);
            byte[] buffer = new byte[BUFFER_SIZE];
            String filename = Path.GetFileNameWithoutExtension(inputFile);
            String path = outputstring3.Text;
            int chunkSize = (2147450880); //this size gets looped twice to make max filesize for fat32 drives
            uint maxsize = (4294901760);



            using (Stream input = File.OpenRead(inputFile))
            {
                try
                {
                    if (maxsize < input.Length)
                    {
                        if (checkBox_sxos.Checked)
                        {
                            //create sx folders & change path
                            System.IO.Directory.CreateDirectory(path + "\\" + "sxos");
                        }


                        if (getextension.Equals(".xci"))
                        {
                            if (checkBox_sxos.Checked)
                            {
                                string mydir = (path + "\\" + "sxos" + "\\" + "xci");
                                //create sx folders & change path
                                System.IO.Directory.CreateDirectory(mydir);
                                getextension = (mydir + "\\" + filename + ".xc");
                            }

                            else
                            {
                                getextension = (path + "\\" + filename + ".xc");
                            }
                        }

                        if (getextension.Equals(".nsp"))
                        {
                            if (checkBox_sxos.Checked)
                            {
                                string mydir = (path + "\\" + "sxos" + "\\" + "nsp");
                                //create sx folders & change path
                                System.IO.Directory.CreateDirectory(mydir);
                                getextension = (mydir + "\\" + filename + ".ns");
                            }

                            else
                            {
                                System.IO.Directory.CreateDirectory(path + "\\" + filename + getextension);
                                string newpath = (path + "\\" + filename + getextension);
                                getextension = (newpath + "\\" + "0");
                            }
                        }

                        int index = 0;
                        while (input.Position < input.Length)
                        {
                            using (Stream output = File.Create(getextension + index))
                            {
                                for (int i = 0; i < 2; i++) //do twice to make 4gb files
                                {
                                    int remaining = chunkSize, bytesRead;
                                    while (remaining > 0 && (bytesRead = input.Read(buffer, 0,
                                            Math.Min(remaining, BUFFER_SIZE))) > 0)
                                    {
                                        output.Write(buffer, 0, bytesRead);
                                        remaining -= bytesRead;
                                        long calc = (input.Position);
                                        long length = (input.Length);
                                        string s = ("Splitting: " + filename + "\n\nProcessed: " + calc.ToString() + " of " + input.Length + " bytes");
                                        Showprogress(calc, length, s); //create a new task so we don't freeze the gui
                                    }
                                }
                            }
                            index++;
                        }
                    }

                    else
                    {
                        long calc = (0);
                        long length = (0);
                        string s = ("There's no need to split: " + filename + ", it will already fit on a fat32 partition.");
                        Showprogress(calc, length, s);
                    }

                }

                catch (Exception error)
                {
                    MessageBox.Show("Error is: " + error.Message);
                }
            }

            void Showprogress(long poss, long length, string aString)
            {
                Task.Factory.StartNew(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        long MBsize = 1024 * 1024;
                        int MBlength = (int)(length / MBsize);
                        int MBposs = (int)(poss / MBsize);
                        int left = (MBlength - MBposs);
                        richTextBox_split.Text = (aString + "\n\n" + "Megabytes to go: " + left.ToString());
                        if (left == 0)
                        {
                            string text = ("\n\nSplit Completed");
                            richTextBox_split.AppendText(text);
                        }
                    });
                });
            }

        }

        private void toolsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string mydir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = mydir,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void binToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = "tools\\bin",
                UseShellExecute = true,
                Verb = "open"
            });
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Switch Game File (*.xci, *.nsp, *.nsz)|*.xci;*.nsp;*.nsz|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TB_File.Text = openFileDialog.FileName;
                string ext = Path.GetExtension(TB_File.Text);
                bool myfile = ext.Contains("xcz");
                if (myfile)
                {
                    MessageBox.Show("xcz files are not supported yet.");
                }

                else
                {
                    uint maxsize = (4294901760);
                    String inputFile = TB_File.Text;
                    using (Stream input = File.OpenRead(inputFile))
                    {
                        if (maxsize < input.Length)
                        {
                            if (tabControl1.TabPages.Contains(Spliter) == false)
                            {
                                tabControl1.Controls.Add(Spliter);
                            }
                        }
                        else
                        {
                            if (tabControl1.TabPages.Contains(Spliter) == true)
                            {
                                tabControl1.Controls.Remove(Spliter);
                            }
                        }
                    }

                    ProcessFile();
                    Rename.Enabled = true;
                    button_add_DB.Enabled = true;
                }
            }
        }

        private void PB_GameIcon_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    //put code here
                    String gamename = TB_Name.Text;
                    String loaded = TB_TID.Text;

                    if (loaded == "")
                    {
                        MessageBox.Show("Game is not loaded - please load a valid game", "No game loaded", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }

                    else
                    {
                        if (gamename == "")
                        {
                            MessageBox.Show("Game name is empty - please load a valid game or enter a name manually", "No game loaded", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            string filePath = "";
                            saveFileDialog.FileName = Path.GetFileName(gamename);
                            saveFileDialog.Filter = "PNG (*.png)|*.png";
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                filePath = saveFileDialog.FileName;
                                PB_GameIcon.BackgroundImage.Save(filePath);
                                MessageBox.Show(gamename + " icon saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            }
                        }
                    }
                }
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Util.checkFile(TB_File.Text))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "gamecard_cert.dat (*.dat)|*.dat";
                saveFileDialog.FileName = Path.GetFileName("gamecard_cert.dat");
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fileStream = new FileStream(TB_File.Text, FileMode.Open, FileAccess.Read);
                    byte[] array = new byte[512];
                    fileStream.Position = 28672L;
                    fileStream.Read(array, 0, 512);
                    File.WriteAllBytes(saveFileDialog.FileName, array);
                    fileStream.Close();
                    MessageBox.Show("cert successfully exported to:\n\n" + saveFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("File not found");
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Util.checkFile(TB_File.Text))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "gamecard_cert (*.dat)|*.dat|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK && new FileInfo(openFileDialog.FileName).Length == 512)
                {
                    using (Stream stream = File.Open(TB_File.Text, FileMode.Open))
                    {
                        stream.Position = 28672L;
                        stream.Write(File.ReadAllBytes(openFileDialog.FileName), 0, 512);
                    }
                    MessageBox.Show("Cert successfully imported from:\n\n" + openFileDialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("File not found");
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Util.checkFile(TB_File.Text))
            {
                CertForm cert = new CertForm(this);
                cert.Text = "Cert Data - " + TB_File.Text;
                cert.Show();
            }
            else
            {
                MessageBox.Show("File not found");
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Util.checkFile(TB_File.Text))
            {
                if (MessageBox.Show("The cert will be deleted permanently.\nContinue?", "XCI Explorer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (Stream stream = File.Open(TB_File.Text, FileMode.Open))
                    {
                        byte[] array = new byte[512];
                        for (int i = 0; i < array.Length; i++)
                        {
                            array[i] = byte.MaxValue;
                        }
                        stream.Position = 28672L;
                        stream.Write(array, 0, array.Length);
                        MessageBox.Show("Cert deleted.");
                    }
                }
            }
            else
            {
                MessageBox.Show("File not found");
            }
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string myval = (string)comboBox1.SelectedValue;
            System.Diagnostics.Process.Start(myval);
        }

        private void comboBox1_additems()
        {
            try
            {
                //connect to datbase
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con)
                {
                    //CommandText = "SELECT * from links"
                    CommandText = "SELECT * from links ORDER BY name COLLATE NOCASE ASC"
                };

                SQLiteDataReader sqReader = cmd.ExecuteReader();

                //Build a list
                var dataSource = new List<Language>();

                while (sqReader.Read())
                {
                    string listitems = (sqReader.GetString(1)); //1 for names 2 for url
                    string listurl = (sqReader.GetString(2)); //1 for names 2 for url
                    dataSource.Add(new Language() { LinkName = listitems, LinkValue = listurl });
                }

                //Setup data binding
                this.comboBox1.DataSource = dataSource;
                this.comboBox1.DisplayMember = "LinkName";
                this.comboBox1.ValueMember = "LinkValue";

                // make it readonly
                this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

                con.Close(); //disconnet from database
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void aESToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("tools\\bin\\aes-tool\\crypto.html");
        }

        public void MouseHover(object sender, EventArgs e)
        {
            comboBox1_additems();
        }

        static void makedatabase()
        {
            try
            {
                using var con = new SQLiteConnection(mydatabase);
                con.Open();
                using var cmd = new SQLiteCommand(con);

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Games(ID INTEGER PRIMARY KEY ASC, Name TEXT, Size TEXT, Title_ID TEXT, Game_Rev TEXT, Type TEXT, Install TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cheats(id INTEGER PRIMARY KEY ASC, name TEXT, titleid TEXT, build TEXT, cheat TEXT, notes TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS links(id INTEGER PRIMARY KEY ASC, name TEXT, url TEXT, about TEXT)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Programs(id INTEGER PRIMARY KEY ASC, name TEXT, url TEXT, about TEXT)";
                cmd.ExecuteNonQuery();

                con.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }

        }

        public void CloseIt() //used for autoclosing messagebox below in void below
        {
            System.Threading.Thread.Sleep(2000);
            Microsoft.VisualBasic.Interaction.AppActivate(
                 System.Diagnostics.Process.GetCurrentProcess().Id);
            System.Windows.Forms.SendKeys.SendWait(" ");
        }

        private void button_add_DB_Click(object sender, EventArgs e)
        {
            try
            {
                //test if name string is empty (dlc)
                string game_name = TB_Name.Text.ToLower(); //make the string lower case
                game_name = (char.ToUpper(game_name[0]) + game_name.Substring(1)); //make first letter uppercase
                string game_size = TB_ROMSize.Text;
                string game_id = TB_TID.Text;
                string game_rev = TB_GameRev.Text;
                string game_type = "";
                string game_install = TB_Capacity.Text;

                if (String.IsNullOrEmpty(game_name))
                {
                    //don't add empty data to the database
                    MessageBox.Show("No data was added to the database\n\nNo game name is present - this is probably dlc.", "Nothing added to the database!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                //put the database code in here...
                else
                {
                    if (game_rev.Contains("Update"))
                    {
                        game_type = "Update";
                    }

                    else
                    {
                        game_type = "Game";
                    }

                    if (game_install.Contains("eShop"))
                    {
                        game_install = "NSP";
                    }

                    else
                    {
                        game_install = "XCI";
                    }

                    //put datbase code in here

                    //connect to database
                    using var con = new SQLiteConnection(mydatabase);
                    con.Open();
                    using var cmd = new SQLiteCommand(con);

                    //create table if it does not exist
                    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Games(ID INTEGER PRIMARY KEY ASC, Name TEXT, Size TEXT, Title_ID TEXT, Game_Rev TEXT, Type TEXT, Install TEXT)";
                    cmd.ExecuteNonQuery();

                    //put the data into the database
                    cmd.CommandText = "INSERT INTO Games(Name, Size, Title_ID, Game_Rev, Type, Install) VALUES(@Name, @Size, @Title_ID, @Game_Rev, @Type, @Install)";
                    cmd.Parameters.AddWithValue("@Name", game_name);
                    cmd.Parameters.AddWithValue("@Size", game_size);
                    cmd.Parameters.AddWithValue("@Title_ID", game_id);
                    cmd.Parameters.AddWithValue("@Game_Rev", game_rev);
                    cmd.Parameters.AddWithValue("@Type", game_type);
                    cmd.Parameters.AddWithValue("@Install", game_install);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    //close the database
                    con.Close();
                    (new System.Threading.Thread(CloseIt)).Start(); //automatically close the messagebox below
                    MessageBox.Show("Game Name: " + game_name + "\n" + "Game Size: " + game_size + "\n" + "Title ID: " + game_id + "\n" + "Game Revision: " + game_rev + "\n" + "Type: " + game_type + "\n" + "Install Type: " + game_install + "\n\nAdded to database.");
                }

                //disable button so we don't add the same db entry.
                button_add_DB.Enabled = false;
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void gamesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Form Games = new Games();
                this.Hide();
                Games.ShowDialog();
                this.Show();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void websitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form database = new linkdb();
                this.Hide();
                database.ShowDialog();
                this.Show();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void cheatsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Form Cheats = new Cheats();
                this.Hide();
                Cheats.ShowDialog();
                this.Show();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void gamesToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Form Games = new Games();
                this.Hide();
                Games.ShowDialog();
                this.Show();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void cheatsToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Form Cheats = new Cheats();
                this.Hide();
                Cheats.ShowDialog();
                this.Show();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }

        }

        private void websitesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Form database = new linkdb();
                this.Hide();
                database.ShowDialog();
                this.Show();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Form Programs = new Programs();
                this.Hide();
                Programs.ShowDialog();
                this.Show();
            }

            catch (Exception error)
            {
                MessageBox.Show("Error is: " + error.Message);
            }
        }
    }

    public class Language
    {
        public string LinkName { get; set; }
        public string LinkValue { get; set; }
    }

}
