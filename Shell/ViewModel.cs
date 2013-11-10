using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shell
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _selectedDisc;
        private ListViewItem _currentListViewItem;
        private static List<string> _pathHistory;
        public static ViewModel Model;

        public string CurrentPath
        {
            get
            {
                return _pathHistory.Last();
            }
            set
            {
                if (_pathHistory == null)
                {
                    _pathHistory = new List<string>();
                }
                _pathHistory.Add(value);
                NotifyPropertyChanged("ListViewItems");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ListViewItem CurrentListViewItem
        {
            get
            {
                return _currentListViewItem;
            }
            set
            {
                _currentListViewItem = value;
                NotifyPropertyChanged("CurrentListViewItem");
            }
        }

     
        public string  SelectedDisc
        {
            get
            {
                return _selectedDisc;
            }
            set
            {
                CurrentPath = _selectedDisc = value;
            }
        }

        public List<ListViewItem> ListViewItems
        {
            get
            {
                return OpenFolder(CurrentPath);
            }
        }

        public static List<ListViewItem> OpenFolder(string pathToFolder)
        {
            var listViewItems = new List<ListViewItem>();
            var directoryInfo = new DirectoryInfo(pathToFolder);
            foreach (var folder in directoryInfo.GetDirectories())
            {
                listViewItems.Add(new ListViewItem(folder.Name, folder.LastWriteTime));
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Exists)
                    listViewItems.Add(new ListViewItem(file.Name, Path.GetExtension(file.Name), file.Length, file.LastWriteTime));
            }
            return listViewItems;
        }

        public List<string> ComboBoxItems
        {
            get 
            {
                var logicalDiscs = new List<string>();
                foreach (var diskName in Directory.GetLogicalDrives().ToList())
                {
                    logicalDiscs.Add(diskName.Substring(0, 2));
                }
                return logicalDiscs;
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
