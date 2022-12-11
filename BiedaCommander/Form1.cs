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
            if(e.Column == lvcSorter.SortColumn)
            {
                if(lvcSorter.Order == SortOrder.Ascending)
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
                lvcSorter.SortColumn= e.Column;
                lvcSorter.Order= SortOrder.Ascending;
            }

            this.listView1.Sort();
        }
        
    }
}