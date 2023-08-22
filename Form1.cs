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
using System.Reflection;

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

        private bool search_file_preferences()
        {
            if (File.Exists(path_to_preferences_file) == false)             //если файл не найден
            {
                System.IO.Directory.CreateDirectory(path_to_preferences);   //создаем директорию
                File.Create(path_to_preferences_file).Close();              //создаем файл  preferences.txt

                //textBox1.Text = "The preferences.txt file has been created! Open the file " + path_to_preferences_file +
                    //", write the path to the message.txt file. For example: \\\\Server\\D\\message.txt";
                textBox1.Enabled = false;

                message_box("The preferences.txt file has been created! \n" +
                    "\n- Open the file " + path_to_preferences_file +
                    "\n- Write the path to the message.txt file" +
                    "\n\nFor example:" +
                    "\n\\\\Server\\D\\message.txt", "Information", MessageBoxIcon.Information);

                label1.ForeColor = Color.DarkRed;
                label1.Text = "Error 1";

                return false;
            }
            else
            {
                label1.ForeColor = Color.DarkSlateGray;
                label1.Text = path_to_message_file;

                return true;
            }
        }
        
        private bool search_file_message()
        {
            path_to_message_file = File.ReadAllText("C:\\Winme\\preferences.txt");

            if (File.Exists(path_to_message_file) == true)
            {
                label1.ForeColor = Color.DarkSlateGray;
                label1.Text = path_to_message_file;

                if (path_to_message_file == "")
                {
                    //textBox1.Text = "The file preferences.txt is empty! Specify the path to the message file!";
                    textBox1.Enabled = false;

                    message_box("The file preferences.txt is empty!" +
                        "\nSpecify the path to the message file!", "Error", MessageBoxIcon.Error);

                    label1.ForeColor = Color.DarkRed;
                    label1.Text = "Error 2";

                    return false;
                }

                return true;
            }
            else
            {
                label1.ForeColor = Color.DarkRed;
                label1.Text = "Error 3";

                message_box("Message file not found." +
                    "\n\nPossible reasons:" +
                    "\n- the path to the message file is incorrect" +
                    "\n- the server (remote computer) is not responding or is turned off", "Error", MessageBoxIcon.Error);
                
                return false;
            }
        }

        public Form1()
        {
            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            Text = Text + " " + version.Major + "." + version.Minor + " (build " + version.Revision + ")"; //change form title
        }

        //load Form
        private void Form1_Load(object sender, EventArgs e)
        {
            search_file_preferences();
            search_file_message();
        }

        //butoon Read
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(path_to_message_file) == true)          //проверяем наличие файла, а вдруг сервер не работает?
                {
                    text = File.ReadAllText(path_to_message_file);      //считываем данные с текстового блока и кидаем в файл
                    textBox1.Text = text;
                    Clipboard.SetText(text);

                    textBox1.Enabled = true;

                    label1.ForeColor = Color.DarkSlateGray;
                    label1.Text = path_to_message_file;

                    //message_box("Data copied to buffer!", "Information", MessageBoxIcon.Information);
                }
                else
                {
                    label1.ForeColor = Color.DarkRed;
                    label1.Text = "Error 4";

                    textBox1.Enabled = false;

                    message_box("Message not accepted! Possible reasons:" +
                        "\n- the path to the message.txt file is not specified" +
                        "\n- problems accessing the message.txt file",
                        "Warning", MessageBoxIcon.Warning);
                }
            }
            catch
            {
                message_box("Message not accepted! Possible reasons:" +
                    "\n- the path to the message.txt file is not specified" +
                    "\n- problems accessing the message.txt file",
                    "Warning", MessageBoxIcon.Warning);
            }
        }

        //button to Browser
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
