using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BiedaCommander
{
    public partial class Form1 : Form
    {
        private DriveInfo[] drives;
        private ListViewColumnSorter lvcSorter;
        private ImageList imgList = new ImageList();
        public Form1()
        {
            InitializeComponent();
            lvcSorter= new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvcSorter;
            this.listView2.ListViewItemSorter = lvcSorter;
            this.drives = DriveInfo.GetDrives();
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
                lvcSorter.SortColumn = colNum;
                lvcSorter.Order = SortOrder.Ascending;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string currentDir = Environment.CurrentDirectory;
            imgList.ImageSize = new Size(24, 24);
            imgList.Images.Add(Image.FromFile("img\\file-regular-24.png"));
            imgList.Images.Add(Image.FromFile("img\\folder-regular-24.png"));
            listView1.SmallImageList = imgList;
            listView2.SmallImageList = imgList;


            var directory = Directory.GetCurrentDirectory();
            var currentDrive = Path.GetPathRoot(directory);
            AddDriveToComboBox(comboBox1);
            AddDriveToComboBox(comboBox2);
            comboBox1.SelectedText = currentDrive;
            comboBox2.SelectedText = currentDrive;
            ItemLabelOperator.drawField(label1, listView1, directory);
            ItemLabelOperator.drawField(label2, listView2, directory);   
        }

        private void AddDriveToComboBox(ComboBox combobox)
        {
            foreach(var drive in drives)
                combobox.Items.Add(drive);
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
            int keyId = e.KeyValue;
            if (!ItemLabelOperator.usedKeyIds.Contains(keyId))
                return;

            string currentDir = label1.Text.ToString();
            string targetDir = label2.Text.ToString();
            
            ItemLabelOperator.fKeyActions(keyId, currentDir, targetDir, listView1);
            ItemLabelOperator.drawField(label1, listView1, label1.Text.ToString());
            ItemLabelOperator.drawField(label2, listView2, label2.Text.ToString());
        }

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            int keyId = e.KeyValue;
            if (!ItemLabelOperator.usedKeyIds.Contains(keyId))
                return;

            string currentDir = label2.Text.ToString();
            string targetDir = label1.Text.ToString();
            

            ItemLabelOperator.fKeyActions(keyId, currentDir, targetDir, listView2);
            ItemLabelOperator.drawField(label1, listView1, label1.Text.ToString());
            ItemLabelOperator.drawField(label2, listView2, label2.Text.ToString());
        }

        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            var fileName = listView1.SelectedItems[0].Text;
            ItemLabelOperator.doubleClickActions(listView1, label1, fileName);
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            var fileName = listView2.SelectedItems[0].Text;
            ItemLabelOperator.doubleClickActions(listView2, label2, fileName);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            var items = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (items == null)
            {
                var selectedItems = (List<ListViewItem>)e.Data.GetData(typeof(List<ListViewItem>));
                if (selectedItems == null)
                    return;

                foreach (var listviewItem in selectedItems)
                {
                    string currDir = Path.Combine(label2.Text, listviewItem.Text);
                    string targetDir = Path.Combine(label1.Text, listviewItem.Text);
                    FilesOperator.ChangeFileLocation(currDir, targetDir);
                }
                ItemLabelOperator.drawField(label1, listView1, label1.Text.ToString());
                ItemLabelOperator.drawField(label2, listView2, label2.Text.ToString());
            }
            else
                ItemLabelOperator.manageDragDrop(items, listView1, label1);
        }

        private void listView2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void listView2_DragDrop(object sender, DragEventArgs e)
        {
            var items = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (items == null)
            {
                var selectedItems = (List<ListViewItem>)e.Data.GetData(typeof(List<ListViewItem>));
                if (selectedItems == null)
                    return;

                foreach(var listviewItem in selectedItems)
                {
                    string currDir = Path.Combine(label1.Text, listviewItem.Text);
                    string targetDir = Path.Combine(label2.Text, listviewItem.Text);
                    FilesOperator.ChangeFileLocation(currDir, targetDir);
                }
                ItemLabelOperator.drawField(label1, listView1, label1.Text.ToString());
                ItemLabelOperator.drawField(label2, listView2, label2.Text.ToString());
            }
            else
                ItemLabelOperator.manageDragDrop(items, listView2, label2);
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if(label1.Text != "" && label1.Text != null)
                Clipboard.SetText(label1.Text);
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            if (label2.Text != "" && label2.Text != null)
                Clipboard.SetText(label2.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "" && comboBox1.SelectedItem.ToString() != null)
                ItemLabelOperator.drawField(label1, listView1, comboBox1.SelectedItem.ToString());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() != "" && comboBox2.SelectedItem.ToString() != null)
                ItemLabelOperator.drawField(label2, listView2, comboBox2.SelectedItem.ToString());
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var items = new List<ListViewItem>();
            items.Add((ListViewItem)e.Item);
            foreach(var lvitem in listView1.SelectedItems)
            {
                if (!items.Contains(lvitem))
                    items.Add((ListViewItem)lvitem);
            }

            listView1.DoDragDrop(items, DragDropEffects.Move);
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void listView2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void listView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var items = new List<ListViewItem>();
            items.Add((ListViewItem)e.Item);
            foreach (var lvitem in listView2.SelectedItems)
            {
                if (!items.Contains(lvitem))
                    items.Add((ListViewItem)lvitem);
            }

            listView2.DoDragDrop(items, DragDropEffects.Move);
        }
    }
}