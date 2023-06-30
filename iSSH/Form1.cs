using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;

namespace iSSH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            string host = hostTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string localFilePath = Path.Combine(Application.StartupPath, "SystemVersion.plist");
            string remoteFilePath = "/System/Library/CoreServices/SystemVersion.plist";

            try
            {
                using (var client = new SftpClient(host, username, password))
                {
                    client.Connect();

                    if (client.IsConnected)
                    {
                        using (var fileStream = File.OpenRead(localFilePath))
                        {
                            client.UploadFile(fileStream, remoteFilePath);
                        }

                        client.Disconnect();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("SystemVersion.plist"))
            {
                Console.WriteLine(".plist file exists");
            }
            else
            {
                Console.WriteLine(".plist file not exists");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string host = hostTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                using (var client = new SshClient(host, username, password))
                {
                    client.Connect();

                    if (client.IsConnected)
                    {
                        // Przykładowe komendy SSH
                        string[] commands = new string[]
                        {
                            "halt"
                        };

                        foreach (string command in commands)
                        {
                            var commandResult = client.RunCommand(command);
                            Console.WriteLine(commandResult.Result);
                        }

                        MessageBox.Show("Now turn on device and go to system update, wait and that itssxaaaaaaaaaaa", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        client.Disconnect();
                    }
                }
            }
            

            catch (Exception ex)
            {
                Console.WriteLine("Błąd: " + ex.Message);
            }
        }
    }
}
