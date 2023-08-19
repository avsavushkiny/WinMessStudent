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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using System.Reflection.Emit;

namespace WinMessStudent
{
    public partial class Form1 : Form
    {
        string path_to_message_file;
        string path_to_preferences = "c:\\Winme";
        string path_to_preferences_file = "c:\\Winme\\preferences.txt";
        string text;

        private void message_box(string Text, String Title, MessageBoxIcon ICON)
        {
            MessageBox.Show(Text, Title,
                    MessageBoxButtons.OK,
                    ICON);
        }

        private void path_to_file()
        {
            try
            {
                path_to_message_file = File.ReadAllText("C:\\Winme\\preferences.txt");
                label1.Text = path_to_message_file;

                if (path_to_message_file == "")
                {
                    textBox1.Text = "The file preferences.txt is empty! Specify the path to the message file!";
                    textBox1.Enabled = false;

                    message_box("The file preferences.txt is empty! " +
                        "\nSpecify the path to the message file!", "Error", MessageBoxIcon.Error);

                    label1.Text = "Empty";
                }
            }
            catch
            {
                message_box("Unforeseen error related to the configuration file! " +
                    "\n Check the location of the preferences.txt file." +
                    "\n\n" + path_to_preferences_file,
                    "Error", MessageBoxIcon.Error);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                text = File.ReadAllText(path_to_message_file);
                textBox1.Text = text;
                Clipboard.SetText(text);

                message_box("Data copied to buffer!", "Information", MessageBoxIcon.Information);
            }
            catch
            {
                message_box("Message not accepted! Possible reasons:" +
                    "\n- the path to the message.txt file is not specified" +
                    "\n- problems accessing the message.txt file",
                    "Warning", MessageBoxIcon.Warning);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(path_to_preferences_file) == false)
            {
                System.IO.Directory.CreateDirectory(path_to_preferences);
                File.Create(path_to_preferences_file).Close();

                textBox1.Text = "The preferences.txt file has been created! Open the file " + path_to_preferences_file +
                    ", write the path to the message.txt file. For example: \\\\Server\\D\\message.txt";
                textBox1.Enabled = false;

                message_box("The preferences.txt file has been created! \n" +
                    "\n- Open the file " + path_to_preferences_file +
                    "\n- Write the path to the message.txt file" +
                    "\n\nFor example:" +
                    "\n\\\\Server\\D\\message.txt", "Information", MessageBoxIcon.Information);
            }
            else
            {
                label1.Text = path_to_message_file;
            }

            path_to_file();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(text);
            }
            catch
            {
                Process.Start("https://www.google.com/search?q=" + text);
            }
        }
    }
}
