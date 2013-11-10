using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shell.State
{
    public class StateFolder:IState
    {
        public void Open()
        {
            MessageBox.Show("Open folder");
            //var vm = new ViewModel();
            //var listViewItems = new List<ListViewItem>();
            //var directoryInfo = new DirectoryInfo(String.Format("{0}{1}", vm.CurrentDirectory, vm.SelectedFolder));
            //foreach (var folder in directoryInfo.GetDirectories())
            //{
            //    listViewItems.Add(new ListViewItem(folder.Name, folder.LastWriteTime));
            //}
            //foreach (var file in directoryInfo.GetFiles())
            //{
            //    if (file.Exists)
            //        listViewItems.Add(new ListViewItem(file.Name, Path.GetExtension(file.Name), file.Length, file.LastWriteTime));
            //}
        }
    }
}
