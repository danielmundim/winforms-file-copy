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

namespace Project
{
    public partial class Form1 : Form
    {
        private string SourceFolder { get; set; }
        private string TargetFolder { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Call(string source, string target)
        {
            lblFiles.Text = string.Empty;

            if (!System.IO.Directory.Exists(target))
            {
                System.IO.Directory.CreateDirectory(target);
            }


            if (Directory.Exists(source))
            {
                var dirs = Directory.GetDirectories(source);
                
                for (int i = 0; i < dirs.Length; i++)
                {
                    string folder = Path.GetFileName(Path.GetDirectoryName(dirs[i] + "\\"));

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(target + "\\" + folder);
                    }

                    CopyFiles(dirs[i], target + "\\" + folder);
                }
               CopyFiles(source, target);
            }

            MessageBox.Show("Done!");

        }

        private void CopyFiles(string dir, string target)
        {
            string[] files = System.IO.Directory.GetFiles(dir);
            foreach (string s in files)
            {
                string fileName = System.IO.Path.GetFileName(s);
                string destFile = System.IO.Path.Combine(target, fileName);
                if (!File.Exists(destFile))
                {
                    File.Copy(s, destFile, false);
                    lblFiles.Text += destFile + Environment.NewLine;
                    lblFiles.Refresh();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SourceFolder) && !string.IsNullOrEmpty(TargetFolder))
            {
                Call(SourceFolder, TargetFolder);
            }
            else
            {
                MessageBox.Show("You must select a source and a target folder to continue!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog(); 
            if (result == DialogResult.OK)
            {
                SourceFolder = folderBrowserDialog1.SelectedPath;
                label1.Text = SourceFolder;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog(); 
            if (result == DialogResult.OK)
            {
                TargetFolder = folderBrowserDialog1.SelectedPath;
                label2.Text = TargetFolder;
            }
        }
    }
}
