using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiedaCommander
{
    public static class ItemLabelOperator
    {
        public static void drawField(Label label, ListView view, string pathToDir)
        {
            if (pathToDir == null || pathToDir == "") { return; }
            label.Text = pathToDir;

            fillColumns(view, pathToDir);
        }

        private static void fillColumns(ListView view, string pathToDir)
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

        public static void fKeyActions(int keyId, string currentDir, ListView view)
        {
            if (keyId == 118)
            {
                FilesOperator.CreateFile(currentDir);

            }
            else if (keyId == 119)
            {
                FilesOperator.RemoveFile(view.SelectedItems, currentDir);
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
