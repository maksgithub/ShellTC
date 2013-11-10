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
        private ICommand _commandBackward;
        public event PropertyChangedEventHandler PropertyChanged;


        public string CurrentPath
        {
            get
            {
                if (_pathHistory == null || _pathHistory.Count == 0)
                {
                    _pathHistory = new List<string>();
                    _pathHistory.Add(_selectedDisc);
                }
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
                NotifyPropertyChanged("CurrentPathForTexBox");
            }
        }

        public string CurrentPathForTexBox
        {
            get
            {
                return CurrentPath;
            }
        }


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

        public ICommand CommandBackward
        {
            get
            {
                this._commandBackward = new Command(x => Backward());
                return this._commandBackward;
            }
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

        public int SelectedDiskIndex
        {
            get
            {
                if (CurrentPath != null)
                {
                    var currentDisk = CurrentPath.Substring(0, 2);
                    var currentDiskIndex = ComboBoxItems.FindIndex(x => x == currentDisk);
                    return currentDiskIndex;
                }
                return 0;
            }
        }


        public void Backward()
        {
            if (_pathHistory.Count > 1)
            {
                _pathHistory.RemoveAt(_pathHistory.Count - 1);
                NotifyPropertyChanged("ListViewItems");
                NotifyPropertyChanged("SelectedDiskIndex");

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



        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
