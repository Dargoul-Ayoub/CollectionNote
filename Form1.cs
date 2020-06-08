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

namespace CollectionNote
{
    public partial class Form1 : Form
    {
        Form2 NoteDialog;
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            NoteDialog = new Form2();
            NoteDialog.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.OwnerDraw = true;
            LoadListView();
            button3.Enabled = false;
        }

        public void LoadListView()
        {
            listView1.Clear();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Font = new Font(listView1.Font.FontFamily, 12);
           
           
            string ReadLine = "False";
            
           // item1.SubItems.Add("Nothing");
            if(File.Exists(Form2.pathSaveNotes))
            using(StreamReader read = File.OpenText(Form2.pathSaveNotes)) {
                    ReadLine = read.ReadLine();
                    while(ReadLine!=null) 
                    if (ReadLine.IndexOf("/") > 0)
                    {
                        string path = ReadLine.Substring(0, ReadLine.IndexOf("/"));
                        string date = ReadLine.Substring(path.Length+1, ReadLine.Length-path.Length-1);
                        if (@path != null && File.Exists(@path))
                        {
                                //RichText.LoadFile(@path);
                                // string x = RichText.Lines[0];
                                StreamReader readFile = File.OpenText(@path);
                                string x = readFile.ReadLine();
                               // readFile.Close();
                                
                            ListViewItem item1 = new ListViewItem(x);
                            item1.SubItems.Add(date);
                            item1.SubItems.Add(path);
                            listView1.Items.Add(item1);
                        }
                            ReadLine = read.ReadLine();
                        }
                }

            listView1.Columns.Add("Name of The File", listView1.Width / 3, HorizontalAlignment.Center);
            listView1.Columns.Add("Date", listView1.Width / 6, HorizontalAlignment.Center);
            listView1.Columns.Add("File Directory", listView1.Width / 2, HorizontalAlignment.Left);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadListView();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //string nameFile=listView1.SelectedItem
            //MessageBox.Show()
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          /*  foreach(int i in listView1.SelectedIndices)
            {
                string nameFile = listView1.SelectedItems[0].SubItems[0].Text;
                string DateFilze = listView1.SelectedItems[0].SubItems[1].Text;
                string file_Directory = listView1.SelectedItems[0].SubItems[2].Text;
                MessageBox.Show(nameFile + " " + DateFilze + " " + file_Directory);

            }*/

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
             string file_Directory = listView1.SelectedItems[0].SubItems[2].Text;
            Form2 form2 = new Form2(file_Directory);
            form2.ShowDialog();
        
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.SteelBlue, e.Bounds);
            e.Graphics.DrawRectangle(Pens.Black, e.Bounds);
            e.DrawText();

        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button3.Enabled = true;
                listView1.CheckBoxes = true;
            }
            else
            {
                button3.Enabled = false;
                listView1.CheckBoxes = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fileDir="";
            string Date_file ="";
            foreach(ListViewItem item in listView1.Items)
            {
                using (StreamWriter write = File.AppendText(Form2.copy_pathsaveNotes))
                {
                    if (!item.Checked)
                     {
                    fileDir = item.SubItems[2].Text;
                   Date_file = item.SubItems[1].Text;
                   
                        write.WriteLine(fileDir + "/" + Date_file);
                        write.Close();
                    }
                }
                
            }
           
                File.Delete(Form2.pathSaveNotes);
                File.Move(Form2.copy_pathsaveNotes, Form2.pathSaveNotes);
            
            LoadListView();
            checkBox1.Checked = false;
            listView1.CheckBoxes = false;
            button3.Enabled = false;


        }
    }
}