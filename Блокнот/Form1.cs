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
using System.Reflection;
using Aspose.Words.Fonts;

namespace Блокнот
{
    public partial class Блокнот : Form
    {
        private string filename;
        public bool isFileChanged;
        public FontSettings fs;
        public Блокнот()
        {
            InitializeComponent();

            Init();
        }
        public void Init()
        {
            filename = "";
            isFileChanged = false;
            UpdateTextWithTitle();
            
        }

        public void CreatNewDocument(object sender, EventArgs e)
        {
            textBox1.Text = "";
            filename = "";
            isFileChanged = false;
            UpdateTextWithTitle();
        }
        public void OpenFile(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            openFileDialog1.FileName = "";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                    isFileChanged = false;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл");
                }
            }
        }
         
        public void SaveFile(string _filename)
        {
            if (_filename == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _filename = saveFileDialog1.FileName;
                }
            }
                try
                {
                    StreamWriter sw = new StreamWriter(_filename + ".txt");
                    sw.Write(textBox1.Text);
                    sw.Close();
                    filename = _filename;
                    isFileChanged = false;
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить файл");
                }
            UpdateTextWithTitle();
        }
        public void Save(object sender, EventArgs e)
        {
            SaveFile(filename);
        }
        public void SaveAs(object sender, EventArgs e)
        {
            SaveFile("");
        }
        private void OneTextChanged(object sender, EventArgs e)
        {
            this.Text = this.Text.Replace('*',' ');
            if (!isFileChanged)
            {
                isFileChanged = true;
                //this.Text = "*" + this.Text;
            }  
        }

        public void UpdateTextWithTitle()
        {
            if (filename != "")
                this.Text = filename + " - Блокнот";
            else this.Text = "Безымянный - Блокнот";
        }

        public void SaveUnsavedFile()
        {
            if (isFileChanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменение в файле?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if(result == DialogResult.Yes)
                {
                    SaveFile(filename);
                }
            }
        }

        public void CopyText()
        {
            Clipboard.SetText(textBox1.SelectedText);
        }

        public void CutText()
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
        }

        public void PasteText()
        {
            Clipboard.GetText();
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            CopyText();
        }

        private void OnCutClick(object sender, EventArgs e)
        {
            CutText();
        }

        private void OnPastClick(object sender, EventArgs e)
        {
            PasteText();
        }

        private void OnFormClosing(object sender, EventArgs e)
        {
            SaveUnsavedFile();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
               

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /*private void изменитьЦветToolStripMenuItem_Click(object sender, EventArgs e)
         *{
         *  if (fontDialog1.ShowDialog() == DialogResult.Cancel)
         *       return;
         *   //изменитьЦветToolStripMenuItem.ForeColor = fontDialog1.Color;
         *}
         */

        private void font(object sender, EventArgs e)
        {

        }

        private void настройкиШрифтаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            textBox1.Font = fontDialog1.Font;
        }
    }
}
