using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics.Contracts;
using Microsoft.Win32;
using System.Security.Principal;

namespace ADB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                TextWriter tw = new StreamWriter("C:\\Program Files\\Matarata-adb.txt");
                String a = folderBrowserDialog1.SelectedPath;
                tw.WriteLine(a);
                tw.Close();
                textBox2.Text = a;
                checkBox2.Checked = true;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!IsAdministrator())
            {
                MessageBox.Show("Run Program as administrator","Permission",MessageBoxButtons.OK);
                Application.Exit();
            }
            comboBox1.Items.Add("5555");
            comboBox1.Items.Add("5558");
            comboBox1.SelectedIndex = 0;
            label5.Visible = false;
            checkBox3.Checked = true;
            checkAndroidStudio();
            if (File.Exists("C:\\Program Files\\Matarata-adb.txt"))
            {
                TextReader tr = new StreamReader("C:\\Program Files\\Matarata-adb.txt");
                String txtFileFirstLine = tr.ReadLine();
                textBox2.Text = txtFileFirstLine;
                String txtFileSecondLine = tr.ReadLine();
                textBox1.Text = txtFileSecondLine;
                tr.Close();
                checkBox2.Checked = true;
                checkBox3.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkAndroidStudio();
            string txtbox2 = textBox2.Text;
            string txtbox = textBox1.Text;
            string cmbbox = comboBox1.Text;
            if (txtbox2 != "" && txtbox != "" && cmbbox != "")
            {
                checkBox3.Checked = true;
                if (File.Exists("C:\\Program Files\\Matarata-adb.txt"))
                {
                    System.Diagnostics.ProcessStartInfo hyp = new System.Diagnostics.ProcessStartInfo("cmd");
                    hyp.RedirectStandardInput = true;
                    hyp.RedirectStandardOutput = true;
                    hyp.UseShellExecute = false;
                    System.Diagnostics.Process proc = System.Diagnostics.Process.Start(hyp);

                    TextReader tr2 = new StreamReader("C:\\Program Files\\Matarata-adb.txt");
                    string txtfilematn = tr2.ReadLine();
                    string Firstline = txtfilematn.Substring(0, 2);
                    string Secondline = txtfilematn + "\\platform-tools";
                    tr2.Close();

                    proc.StandardInput.WriteLine(@Firstline);
                    proc.StandardInput.WriteLine(@"cd " + Secondline);
                    proc.StandardInput.WriteLine(@"adb connect " + txtbox + ":" + cmbbox);
                    proc.StandardInput.WriteLine(@"exit");
                    string output = proc.StandardOutput.ReadToEnd();
                    if (output.Contains("connected"))
                    {
                        label5.Visible = true;
                        label5.Text = "Connected !";
                        saveIPtoTxt();
                    }
                    else
                    {
                        label5.Visible = true;
                        label5.Text = "Not Connected!";
                    }
                }
                else
                {
                    MessageBox.Show("Required File Deleted\nSelect SDK Path again with Browse button !");
                }

            }
            else if (txtbox == "" || cmbbox == "")
            {
                checkBox3.Checked = false;
                MessageBox.Show("Please Fill All Fields!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtbox2 == "")
            {
                checkBox2.Checked = false;
                MessageBox.Show("Please Fill All Fields!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else{
                MessageBox.Show("Please Fill All Fields!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkAndroidStudio();
        }

        private void checkAndroidStudio()
        {
            if (System.Diagnostics.Process.GetProcessesByName("studio64").Length > 0)
            {
                label2.Text = "Android Studio is Running...";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                comboBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox1.Checked = true;
            }
            else
            {
                label2.Text = "Android Studio is not Running. Run it first...";
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                comboBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox1.Checked = false;
            }
        }
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public void saveIPtoTxt()
        {
            String txtfilematn = "";
            if (File.Exists("C:\\Program Files\\Matarata-adb.txt"))
            {
                TextReader tr = new StreamReader("C:\\Program Files\\Matarata-adb.txt");
                txtfilematn = tr.ReadLine();
                tr.Close();
                String IpText = textBox1.Text;
                TextWriter tw = new StreamWriter("C:\\Program Files\\Matarata-adb.txt");
                tw.WriteLine(txtfilematn);
                tw.WriteLine(IpText);
                tw.Close();
            }
        }

    }
}
