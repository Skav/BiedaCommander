using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

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
            view.Items.Clear();

            DirectoryInfo dir = new DirectoryInfo(pathToDir);
            FileSystemInfo[] allElements = dir.GetFileSystemInfos();
            foreach (var item in allElements)
            {
                var listViewItem = new ListViewItem(item.Name);
                listViewItem.SubItems.Add(item.CreationTime.ToString());
                view.Items.Add(listViewItem);
            }
        }

        private void drawField(Label label, ListView view, DirectoryInfo pathToDir)
        {
            if(pathToDir.Parent == null){return;}

            label.Text = pathToDir.Parent.ToString();
            view.Items.Clear();

            var dir = new DirectoryInfo(pathToDir.Parent.ToString());
            FileSystemInfo[] allElements = dir.GetFileSystemInfos();
            foreach (FileSystemInfo item in allElements)
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

                if (name == null || name == "" || IsValidFilename(name))
                {
                    MessageBox.Show("Nieprawid³owa nazwa folderu!");
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

                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        try
                        {
                            Directory.Delete(filePath);
                        }
                        catch (IOException ex)
                        {
                            DialogResult dr = MessageBox.Show("Folder nie jest pusty, usun¹c mimo to?", "B³ad", MessageBoxButtons.YesNo);
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

        private bool IsValidFilename(string fileName)
        {
            Regex containsABadCharacter = new Regex($"[{Regex.Escape(new string(Path.GetInvalidPathChars()))}]");
            if (containsABadCharacter.IsMatch(fileName)) { return false; };

            return true;
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

        private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
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
    }
}