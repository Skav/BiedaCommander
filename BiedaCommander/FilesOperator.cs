using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiedaCommander
{
    public static class FilesOperator
    {
        public static void copyFile(string destination, string source, string fileName)
        {
            if (File.Exists(Path.Combine(destination, fileName)))
            {
                DialogResult dr = MessageBox.Show($"Plik o nazwie {fileName} juz istnieje, nadpisac?", "Plik istnieje", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    File.Copy(source, Path.Combine(destination, fileName), true);
                else
                    return;
            }
            else
                File.Copy(source, Path.Combine(destination, fileName));
        }

        public static void copyFolder(string destination, string source, string fileName)
        {
            var fullDestinationPath = Path.Combine(destination, fileName);
            if (Directory.Exists(fullDestinationPath))
            {
                DialogResult dr = MessageBox.Show($"Folder o nazwie {fileName} juz istnieje, usunąc i stworzyć nowy?", "Folder istnieje", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    Directory.Delete(fullDestinationPath, true);
                    CloneDirectory(source, fullDestinationPath);
                }
                else
                    return;
            }
            else
                CloneDirectory(source, fullDestinationPath);
        }

        private static void CloneDirectory(string source, string destination)
        {
            foreach (var directory in Directory.GetDirectories(source))
            {
                var newDirectory = Path.Combine(destination, Path.GetFileName(directory));
                Directory.CreateDirectory(newDirectory);
                CloneDirectory(directory, newDirectory);
            }

            foreach (var file in Directory.GetFiles(source))
            {
                File.Copy(file, Path.Combine(destination, Path.GetFileName(file)));
            }
        }

        public static void CreateDirectory(string fileName, string currentDir)
        {
            if (fileName == null || fileName == "")
            {
                MessageBox.Show("Musisz podać nazwe folderu!");
                return;
            }

            if (!FilesOperator.IsValidFilename(fileName))
            {
                MessageBox.Show("Nieprawidłowa nazwa folderu!");
                return;
            }

            string newFilePath = $"{currentDir}\\{fileName}";

            if (Directory.Exists(newFilePath))
                MessageBox.Show("Folder o podanej nazwie juz istnieje!");
            else
                Directory.CreateDirectory(newFilePath);
        }

        public static void CreateDirectory(string currentDir)
        {
            string fileName = Interaction.InputBox("Podaj nazwe folderu", "Nazwa folderu", "", 300, 300);
            CreateDirectory(fileName, currentDir);
        }

        public static void RemoveFile(ListView.SelectedListViewItemCollection fileList, string currentDir)
        {
            var selectedItems = fileList.Count;
            var dirInfo = new DirectoryInfo(currentDir);

            var errMessages = new List<string> { };
            for (int i = 0; i < selectedItems; i++)
            {
                string filePath = $"{dirInfo.FullName}\\{fileList[i].Text}";

                if (!File.Exists(filePath))
                {
                    if (!Directory.Exists(filePath))
                    {
                        errMessages.Add(fileList[i].Text);
                        continue;
                    }
                }

                var attr = File.GetAttributes(filePath);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    try
                    {
                        Directory.Delete(filePath);
                    }
                    catch (IOException ex)
                    {
                        DialogResult dr = MessageBox.Show("Folder nie jest pusty, usunąc mimo to?", "Bład", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                            Directory.Delete(filePath, true);
                    }

                }
                else
                    File.Delete(filePath);
            }

            if (errMessages.Count > 0)
            {
                string listOfFiles = "";
                foreach (var errFile in errMessages)
                    listOfFiles += $"{errFile},";

                listOfFiles.Remove(listOfFiles.Length - 1);
                MessageBox.Show($"Nie można było znaleść plików: {listOfFiles}");
            }
        }

        public static void ChangeName(ListView.SelectedListViewItemCollection selectedFiles, string filePath)
        {

            if (selectedFiles.Count == 0)
                return;

            string fileName = selectedFiles[0].Text;
            string fullFilePath = Path.Combine(filePath, fileName);
            string newFileName = Interaction.InputBox("Podaj nową nazwe", "Nazwa pliku", "", 300, 300);

            if (!IsValidFilename(newFileName))
            {
                MessageBox.Show("Nieprawidłowa nazwa pliku!");
                return;
            }
            string newFullFilePath = Path.Combine(filePath, newFileName);

            if (File.Exists(newFullFilePath) || Directory.Exists(newFullFilePath))
            {
                MessageBox.Show("Plik o podanej nazwie juz istnieje!");
                return;
            }

            
            if (File.GetAttributes(fullFilePath).HasFlag(FileAttributes.Directory))
                Directory.Move(fullFilePath, newFullFilePath);
            else
                File.Move(fullFilePath, newFullFilePath);
        }

        private static void MoveDirectory(string currPath, string newPath)
        {
            if (Directory.Exists(newPath))
                MessageBox.Show($"Folder o nazwie {Path.GetFileName(currPath)} już istnieje");
            else
            {
                var currPathInfo = new DirectoryInfo(currPath);
                var newPathInfo = new DirectoryInfo(newPath);
                bool isParent = false;
                while (newPathInfo.Parent != null)
                {
                    if (newPathInfo.Parent.FullName == currPathInfo.FullName)
                    {
                        isParent = true;
                        break;
                    }
                    else newPathInfo = newPathInfo.Parent;
                }

                if (isParent)
                    MessageBox.Show($"Nie możesz przenieść folderu do wnetrza jego samego!");
                else
                    Directory.Move(currPath, newPath);
            }
        }

        private static void MoveFile(string currPath, string newPath)
        {
            if (File.Exists(newPath))
                MessageBox.Show($"Plik o nazwie {Path.GetFileName(currPath)} już istnieje");
            else
                File.Move(currPath, newPath);
        }

        public static void ChangeLocation(ListView view, string currentDir, string targetDir)
        {
            for (int i = 0; i < view.SelectedItems.Count; i++)
            {
                string filePath = Path.Combine(currentDir, view.SelectedItems[i].Text);
                string newFilePath = Path.Combine(targetDir, view.SelectedItems[i].Text);

                if (Path.Equals(filePath, newFilePath))
                    continue;

                if (File.GetAttributes(filePath).HasFlag(FileAttributes.Directory))
                    MoveDirectory(filePath, newFilePath);
                else
                    try
                    {
                        MoveFile(filePath, newFilePath);
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show($"Plik {view.SelectedItems[i].Text} nie może być przeniesiony, ponieważ jest używany przez inny proces");
                        continue;
                    }
            }
        }

        public static void ChangeFileLocation(string currentFileDir, string targetDir)
        {

            if (Path.Equals(currentFileDir, targetDir))
                return;

            if (File.GetAttributes(currentFileDir).HasFlag(FileAttributes.Directory))
                MoveDirectory(currentFileDir, targetDir);
            else
                try
                {
                    MoveFile(currentFileDir, targetDir);
                }
                catch (IOException e)
                {
                    MessageBox.Show($"Plik {Path.GetFileName(currentFileDir)} nie może być przeniesiony, ponieważ jest używany przez inny proces");
                }
        }

        private static bool IsValidFilename(string fileName)
        {
            return (!string.IsNullOrEmpty(fileName) &&
              fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0);
        }
    }
}
