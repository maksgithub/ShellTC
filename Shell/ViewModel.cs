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
        private string _currentDirectory;
        private string _currentPath;
        private static string _directory;
        private static string _selectedFolder;

        public event PropertyChangedEventHandler PropertyChanged;

        public string currentPass
        {
            get
            {
                return _currentPath;
            }
            set 
            {
                if (_currentPath == null)
                    _currentPath = SelectedDirectory;
                if(_selectedDisc!=null)
                    _currentPath += "/"+SelectedDirectory;
            }
        }

        public string SelectedDirectory { get; set; }
        

        public ListViewItem CurrentListViewItem
        {
            get
            {
                return _currentListViewItem;
            }
            set
            {
                _currentListViewItem = value;
                CurrentDirectory = CurrentListViewItem.Name;
                NotifyPropertyChanged("CurrentListViewItem");
            }
        }

        public string CurrentDirectory
        {
            get
            {
                return _directory;
            }
            set
            {
                _directory = CurrentListViewItem.Name;
                NotifyPropertyChanged("CurrentDirectory");
            }
        }

        public string SelectedFolder
        {
            get
            {
                return _selectedFolder;
            }
            set
            {
                _selectedFolder = _currentListViewItem.Name;
            }
        }

        public string SelectedDisc
        {
            get
            {
                return _selectedDisc;
            }
            set
            {
                _selectedDisc = value;
                _directory = value;
                NotifyPropertyChanged("ListViewItems");
            }
        }

        public List<ListViewItem> ListViewItems
        {
            get
            {
                return OpenFolder(_selectedDisc);
            }
        }

        public List<ListViewItem> OpenFolder(string pathToFolder)
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
                var logicalDiscs = Directory.GetLogicalDrives().ToList();
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
