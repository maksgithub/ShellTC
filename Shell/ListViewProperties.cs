using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Shell
{
    public static class ListViewProperties
    {
        public static List<ListViewItem> DataContext()
        {
            var logicalDiscs = Directory.GetLogicalDrives();
            var directoryInfo = new DirectoryInfo("D:\\");
            var result = new List<ListViewItem> { };
            foreach (var folder in directoryInfo.GetDirectories())
            {
                result.Add(new ListViewItem(folder.Name, folder.LastWriteTime));
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                if(file.Exists)
                    result.Add(new ListViewItem(file.Name, Path.GetExtension(file.Name), file.Length, file.LastWriteTime));
            }
            return result;
        }
    }
}