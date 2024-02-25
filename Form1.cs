using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace NotePad
{
    public partial class NotePad : Form
    {
        private bool isSaved = false;
        private bool isSavedAs = false;
        private string filePath = "";
        ///private PrintDocument documentToPrint = new PrintDocument();

        private DialogResult MessageBoxYesNo(string text1, string text2)
        {
            DialogResult result = MessageBox.Show(text1, text2,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                return result;
            }
            return result;
        }

        public NotePad()
        {
            InitializeComponent();
        }

        private void NotePadLoad(object sender, EventArgs e)
        {
        }

        private void CreateNewFile(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void SaveFile(object sender, EventArgs e)
        {
            if (isSaved == false)
            {
                if (isSavedAs == false)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "Text file | *.txt";
                    save.Title = "Save text file";
                    save.ShowDialog();
                    filePath = save.FileName;
                    if (!filePath.Equals(""))
                    {
                        File.WriteAllText(filePath, richTextBox1.Rtf);
                        isSaved = true;
                        isSavedAs = true;
                    }
                }
                else
                {
                    File.WriteAllText(filePath, richTextBox1.Rtf);
                    isSaved = true;
                }
            }
        }


        private void ChangeTextBox(object sender, EventArgs e)
        {
            isSaved = false;
        }


        private void NotePad_ExitClick(object sender, FormClosingEventArgs e)
        {
            if (isSaved == false)
            {
                if (MessageBoxYesNo("Сохранить текущий файл?", "Сохранение") == DialogResult.Yes)
                {
                    SaveFile(sender, e);
                }
            }
        }


        private void MenuExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void MenuOpenClick(object sender, EventArgs e)
        {
            if (isSaved == false)
            {
                if (MessageBoxYesNo("Сохранить текущий файл переод открытием нового?", "Сохранение") == DialogResult.Yes)
                {
                    SaveFile(sender, e);
                }
            }
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "| *.txt";
            open.ShowDialog();
            if (!open.FileName.Equals(""))
            {
                richTextBox1.Rtf = File.ReadAllText(open.FileName);
                isSaved = true;
                isSavedAs = true;
            }
        }

        private void AboutProgrammClick(object sender, EventArgs e)
        {
            MessageBox.Show("NotePad\nDesigned by Karimov Adel\nKazan 2024",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }


        private void FontSettingClick(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            DialogResult result = fontDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!richTextBox1.SelectedText.Equals(""))
                {
                    richTextBox1.SelectionFont = fontDialog.Font;
                    isSaved = false;
                }
                else
                {
                    richTextBox1.SelectionFont = fontDialog.Font;
                    isSaved = false;
                }
            }
        }
    }
}