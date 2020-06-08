using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollectionNote
{
    public partial class Form2 : Form
    {
        SaveFileDialog save;
        OpenFileDialog openFileDialog;
        bool ClickButtonSave = false;
        bool fileExiste = false;
        bool TextSaved = false;
		int Text_Length;
        FontDialog font;
        ColorDialog color;
       // public static string pathSaveNotes = @"C:\Users\Devman\Desktop\CollectionNote.FilesDirectroy.txt";
        //  public static string copy_pathsaveNotes = @"C:\Users\Devman\Desktop\CollectionNote.FilesDirectroy_copey.txt";
        public static string pathSaveNotes = Application.StartupPath + "\\FileDirectory.files";
        public static string copy_pathsaveNotes = Application.StartupPath + "\\FileDirectory_copey.files";

        int j = 0;
        string pattern;
        Regex regex3;
        MatchCollection matchCollection;
        string selected_Text;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string path)
        {
            InitializeComponent();
            OpenFilie_of_ItemSelected(path);
        }



        private void SaveFile()
        {
            if (!fileExiste && !ClickButtonSave)
            {
                if (!TextSaved && textBox1.Text != string.Empty)
                    if (MessageBox.Show("Do you want save this file ?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        save = new SaveFileDialog();
                        save.Filter = "type(*.txt)|*.txt"; 
                        save.DefaultExt = "txt";
                        save.ShowDialog();
                        if (save.FileName != string.Empty)
                        {
                            TextSaved = true;
                            save.DefaultExt = "txt";
                            StreamWriter write = File.CreateText(save.FileName);
                            write.Write(textBox1.Text);
                            write.Close();
                            SaveDatePath(save.FileName);
                        }
                    }
            }
            else if (!TextSaved && !fileExiste && ClickButtonSave)
            {
                ClickButtonSave = false;
                save = new SaveFileDialog();
                save.Filter = "type(*.txt)|*.txt"; 
                save.DefaultExt = "txt";
                save.ShowDialog();
                if (save.FileName != string.Empty)
                {
                    StreamWriter write = File.CreateText(save.FileName);
                    write.Write(textBox1.Text);
                    write.Close();
                    TextSaved = true;
                    SaveDatePath(save.FileName);

                }
            }
            else if (fileExiste && !TextSaved && ClickButtonSave)
            {
                ClickButtonSave = false;
                if (openFileDialog.FileName != string.Empty)
                {
                    StreamWriter write = File.CreateText(openFileDialog.FileName);
                    write.Write(textBox1.Text);
                    write.Close();
                    TextSaved = true;
                    //  TextOpened = richTextBox1.Text;
                    SaveDatePath(openFileDialog.FileName);
                    MessageBox.Show("saved");

                }
            }
            else if (fileExiste && !ClickButtonSave && !TextSaved)
            {
				if(textBox1.Text.Length!=Text_Length)
                if (MessageBox.Show("Do you want to save the modification ?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                        if (openFileDialog.FileName != string.Empty)
                        {
                            StreamWriter write = File.CreateText(openFileDialog.FileName);
                            write.Write(textBox1.Text);
                            write.Close();
                            SaveDatePath(openFileDialog.FileName);
                            TextSaved = true;
                        }
                }

            }
        }

        private void OpenFile()
        {
            openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "type(*.txt)|*.txt"; 
            openFileDialog.DefaultExt = "txt";

            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != string.Empty)
            {
                StreamReader read = File.OpenText(openFileDialog.FileName);
                textBox1.Text=read.ReadToEnd();
                read.Close();
                Text_Length = textBox1.Text.Length;
                textBox1.SelectionStart = Text_Length;
                fileExiste = true;
            }
        }

        private void OpenFilie_of_ItemSelected(string p)
        {
            openFileDialog = new OpenFileDialog(); 
            openFileDialog.FileName = @p;
            StreamReader read = File.OpenText(@p);
            textBox1.Text = read.ReadToEnd();
            read.Close();
			Text_Length = textBox1.Text.Length;
            textBox1.SelectionStart = Text_Length;
            fileExiste = true;

        }


        private void NewFile()
        {
            textBox1.Text = String.Empty;
            fileExiste = false; 
        }

        private void FontText()
        {
         
            selected_Text = textBox1.SelectedText;
            font = new FontDialog();
            font.ShowDialog();
            if (font.Font != null)
                if (selected_Text != string.Empty)
                    textBox1.Font = font.Font;
                else
                {
                    MessageBox.Show("Text.");
                    string text  = textBox1.Text;
                    textBox1.Font = font.Font;
                    textBox1.Text = text;
                }
        }

        private void TextColorOrBackColor()
        {
            color = new ColorDialog();
            color.Color = textBox1.ForeColor;
            color.ShowDialog();
            if (color.Color != null)
            {
                textBox1.BackColor = color.Color;
            }
        }

     
        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Lucida Console", 12);
            toolStripTextBox1.Font = new Font("Lucida Console", 10);
            toolStripStatusLabel3.Text = DateTime.Now.TimeOfDay.ToString("h\\:mm");
            toolStripStatusLabel2.Text = DateTime.Now.Date.ToString("d");
            newFileToolStripMenuItem.ShortcutKeys = (Keys.N | Keys.Control);
            ouvrirFileToolStripMenuItem.ShortcutKeys = (Keys.O | Keys.Control);
            enregistrerToolStripMenuItem.ShortcutKeys = (Keys.S | Keys.Control);
        }



       

    
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            SaveFile();

            NewFile();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            SaveFile();

            OpenFile();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            ClickButtonSave = true;
            SaveFile();
        }

        private void Form2_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            SaveFile();


        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.TimeOfDay.ToString("h\\:mm");
            toolStripStatusLabel2.Text = DateTime.Now.Date.ToString("d");
        }

        private void SaveDatePath(string savepath)
        {
            string writeline_savepath;
            using (StreamWriter write = File.AppendText(copy_pathsaveNotes))
            {
                writeline_savepath = savepath + "/" + toolStripStatusLabel2.Text + " " + toolStripStatusLabel3.Text;
                write.WriteLine(writeline_savepath);
                write.Close();
            }
            if (File.Exists(pathSaveNotes))
                using (StreamReader read = File.OpenText(pathSaveNotes))
                {
                    string readline = read.ReadLine();
                    while (readline!= null)
                    {

                        string path = readline.Substring(0, readline.IndexOf("/"));
                        if (path != savepath)
                        {
                            if(File.Exists(@path))
                            using (StreamWriter write = File.AppendText(copy_pathsaveNotes))
                            {
                                write.WriteLine(readline);
                                write.Close();
                            }
                        }

                       readline= read.ReadLine();
                    }

                    read.Close();
                }
            File.Delete(pathSaveNotes);
            File.Move(copy_pathsaveNotes, pathSaveNotes);
          ///  File.SetAttributes(pathSaveNotes, FileAttributes.Hidden);
        }
        
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            j = 0;
             pattern = @toolStripTextBox1.Text;
             regex3 = new Regex(pattern);
             matchCollection = regex3.Matches(textBox1.Text);
            if (matchCollection.Count ==0)
            {
                toolStripTextBox1.BackColor = Color.Red;
                toolStripTextBox1.Focus();
            }
            else if(matchCollection.Count!=0)
            {
                textBox1.Focus();
                textBox1.Select(matchCollection[j].Index, toolStripTextBox1.Text.Length);
                if (toolStripTextBox1.Text != string.Empty)
                    toolStripButton8.Visible = true;
            }
            
           
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            j++;
            if (j < matchCollection.Count)
            {
                textBox1.Focus();
                textBox1.Select(matchCollection[j].Index, toolStripTextBox1.Text.Length);
               
            }
            else
            {
                j = 0;
                textBox1.Focus();
                textBox1.Select(matchCollection[j].Index, toolStripTextBox1.Text.Length);

            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            toolStripButton8.Visible = false;
            toolStripTextBox1.BackColor = Color.LightGreen;
        }

        private void newFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           
                SaveFile();

                NewFile();
            
        }

        private void ouvrirFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFile();

            OpenFile();
        }

        private void enregistrerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ClickButtonSave = true;
            SaveFile();
        }

        private void caractereToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FontText();
        }

        private void arrierePlanToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            TextColorOrBackColor();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FontText();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            TextColorOrBackColor();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextSaved = false;
        }
        int i = 0;
    }
}