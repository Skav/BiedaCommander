using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiedaCommander
{
    public static class ItemLabelOperator
    {
        public static readonly int[] usedKeyIds = new int[] { 119, 118, 117, 116 };
        public static void drawField(Label label, ListView view, string pathToDir)
        {
            if (pathToDir == null || pathToDir == "") { return; }

            label.Text = pathToDir;
            fillColumns(view, label, pathToDir);     
        }

        private static void fillColumns(ListView view, Label label, string pathToDir)
        {
            view.Items.Clear();

            DirectoryInfo dir = new DirectoryInfo(pathToDir);
            FileSystemInfo[] allElements;
            try
            {
                allElements = dir.GetFileSystemInfos();
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show("Folder nie istnieje, przenoszenie do katalogu głownego");
                string mainDir = Path.GetPathRoot(dir.FullName);
                var mainDirInfo = new DirectoryInfo(mainDir);
                allElements = mainDirInfo.GetFileSystemInfos();
                label.Text = mainDirInfo.FullName;
            }
            catch(IOException e)
            {
                MessageBox.Show("Nie można załadować dysku/folderu");
                return;
            }

            if (dir.Parent != null)
            {
                var backItem = new ListViewItem("[..]");
                backItem.SubItems.Add("");
                view.Items.Add(backItem);
            }

            foreach (var item in allElements)
            {
                var listViewItem = new ListViewItem(item.Name);
                if (item.Attributes == FileAttributes.Directory)
                    listViewItem.ImageIndex = 1;
                else
                    listViewItem.ImageIndex = 0;
                listViewItem.SubItems.Add(item.CreationTime.ToString());
                view.Items.Add(listViewItem);
            }
        }

        public static void fKeyActions(int keyId, string currentDir, string targetDir, ListView view)
        {
            switch (keyId)
            {
                case 116:
                    FilesOperator.ChangeLocation(view, currentDir, targetDir);
                    break;
                case 117:
                    FilesOperator.ChangeName(view.SelectedItems, currentDir);
                    break;
                case 118:
                    FilesOperator.CreateDirectory(currentDir);
                    break;
                case 119:
                    FilesOperator.RemoveFile(view.SelectedItems, currentDir);
                    break;
            }
        }

        public static void doubleClickActions(ListView view, Label label, string fileName)
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

        public static void manageDragDrop(string[] items, ListView view, Label label)
        {
            foreach (string path in items)
            {
                if (!File.Exists(path))
                    if (!Directory.Exists(path)) continue;


                var currentDir = label.Text;
                var fileName = Path.GetFileName(path);

                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                    FilesOperator.copyFolder(currentDir, path, fileName);
                else
                    FilesOperator.copyFile(currentDir, path, fileName);
            }

            drawField(label, view, label.Text);
        }
    }
}
