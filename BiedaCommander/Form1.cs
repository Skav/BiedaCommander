using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BiedaCommander
{
    public partial class Form1 : Form
    {
        private ListViewColumnSorter lvcSorter;
        public Form1()
        {
            InitializeComponent();
            lvcSorter= new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvcSorter;
            this.listView2.ListViewItemSorter = lvcSorter;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var directory = Directory.GetCurrentDirectory();
            drawField(label1, listView1, directory);   
            drawField(label2, listView2, directory);   
        }

        private void drawField(Label label, ListView view, string pathToDir)
        {
            if (pathToDir == null) { return; }      
            label.Text = pathToDir;

            fillColumns(view, pathToDir);
        }

        private void drawField(Label label, ListView view, DirectoryInfo pathToDir)
        {
            if(pathToDir.Parent == null){return;}

            label.Text = pathToDir.Parent.ToString();
            view.Items.Clear();

            fillColumns(view, pathToDir.Parent.ToString());
        }

        private void fillColumns(ListView view, string pathToDir)
        {
            view.Items.Clear();

            DirectoryInfo dir = new DirectoryInfo(pathToDir);
            FileSystemInfo[] allElements = dir.GetFileSystemInfos();

            if (dir.Parent != null)
            {
                var backItem = new ListViewItem("[..]");
                view.Items.Add(backItem);
            }

            foreach (var item in allElements)
            {
                var listViewItem = new ListViewItem(item.Name);
                listViewItem.SubItems.Add(item.CreationTime.ToString());
                view.Items.Add(listViewItem);
            }
        }

        public void SortColumn(int colNum)
        {
            if (colNum == lvcSorter.SortColumn)
            {
                if (lvcSorter.Order == SortOrder.Ascending)
                {
                    lvcSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvcSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                lvcSorter.SortColumn =colNum;
                lvcSorter.Order = SortOrder.Ascending;
            }

        }

        private void fKeyActions(int keyId, string currentDir, ListView view)
        {
            if (keyId == 118)
            {
                string name = Interaction.InputBox("Podaj nazwe pliku", "Nazwa pliku", "", 300, 300);

                if (name == null || name == "")
                {
                    MessageBox.Show("Musisz poda� nazwe folderu!");
                    return;
                }

                if (!IsValidFilename(name))
                {
                    MessageBox.Show("Nieprawid�owa nazwa folderu!");
                    return;
                }

                string newFilePath = $"{currentDir}\\{name}";

                if (Directory.Exists(newFilePath))
                    MessageBox.Show("Folder o podanej nazwie juz istnieje!");
                else
                    Directory.CreateDirectory(newFilePath);
            }
            else if (keyId == 119)
            {
                var selectedItems = view.SelectedItems.Count;
                var dirInfo = new DirectoryInfo(currentDir);

                for (int i = 0; i < selectedItems; i++)
                {
                    string filePath = $"{dirInfo.FullName}\\{view.SelectedItems[i].Text}";
                    var attr = File.GetAttributes(filePath);

                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        try
                        {
                            Directory.Delete(filePath);
                        }
                        catch (IOException ex)
                        {
                            DialogResult dr = MessageBox.Show("Folder nie jest pusty, usun�c mimo to?", "B�ad", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                                Directory.Delete(filePath, true);
                        }

                    }
                    else
                        File.Delete(filePath);
                }
            }
            drawField(label1, listView1, label1.Text.ToString());
            drawField(label2, listView2, label2.Text.ToString());
        }

        private void doubleClickActions(ListView view, Label label, string fileName)
        {
            if (fileName == "[..]")
            {
                if (label.Text == null || label.Text == "") { return; }
                var directory = new DirectoryInfo(label.Text);
                drawField(label, view, directory.Parent.ToString());
            }
            else
            {
                var currPath = label.Text;
                var fileFullPath = Path.Combine(currPath, fileName);
                var attr = File.GetAttributes(fileFullPath);

                if (!attr.HasFlag(FileAttributes.Directory))
                    return;

                drawField(label, view, fileFullPath);
            }
        }

        private bool IsValidFilename(string fileName)
        {
            return (!string.IsNullOrEmpty(fileName) &&
              fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                drawField(label1, listView1, folderBrowserDialog1.SelectedPath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                drawField(label2, listView2, folderBrowserDialog1.SelectedPath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text == null || label1.Text == "") { return; }
            var directory = new DirectoryInfo(label1.Text);
            drawField(label1, listView1, directory);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label2.Text == null || label2.Text == "") { return; }
            var directory = new DirectoryInfo(label2.Text);
            drawField(label2, listView2, directory);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        { 
            SortColumn(e.Column);
            this.listView1.Sort();
        }

        private void listView2_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {
            SortColumn(e.Column);
            this.listView2.Sort();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            string currentDir = label1.Text.ToString();
            int keyId = e.KeyValue;

            fKeyActions(keyId, currentDir, listView1);
        }

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            string currentDir = label2.Text.ToString();
            int keyId = e.KeyValue;

            fKeyActions(keyId, currentDir, listView2);
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            var fileName = listView1.SelectedItems[0].Text;
            doubleClickActions(listView1, label1, fileName);
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            var fileName = listView2.SelectedItems[0].Text;
            doubleClickActions(listView2, label2, fileName);
        }
    }
}