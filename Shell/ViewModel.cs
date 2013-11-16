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
        private static List<string> _pathBackwardHistory;
        private static List<string> _pathForwardHistory = new List<string>();
        public static ViewModel Model;
        private ICommand _commandBackward;
        private ICommand _commandForward;
        public event PropertyChangedEventHandler PropertyChanged;


        public string CurrentPath
        {
            get
            {
                if (_pathBackwardHistory == null || _pathBackwardHistory.Count == 0)
                {
                    _pathBackwardHistory = new List<string>();
                    _pathBackwardHistory.Add(ComboBoxItems.First()+"\\");
                }
                return _pathBackwardHistory.Last();
            }
            set
            {
                if (_pathBackwardHistory == null)
                {
                    _pathBackwardHistory = new List<string>();
                }
                //TODO:переместить NotifyPropertyChanged и _pathBackwardHistory.Add(value)
                _pathBackwardHistory.Add(value);
                NotifyPropertyChanged("ListViewItems");
                //NotifyPropertyChanged("SelectedDiskIndex");
                NotifyPropertyChanged("CurrentPathForTexBox");
            }
        }

        public List<ListViewItem> ListViewItems
        {
            get
            {
                return OpenFolder(CurrentPath);
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
                _selectedDisc = String.Format("{0}{1}", value, "\\");
                if(_pathBackwardHistory!=null && !_selectedDisc.Equals(_pathBackwardHistory.Last()))
                    CurrentPath=_selectedDisc;
                //NotifyPropertyChanged("SelectedDiskIndex");
            }
        }

        public int SelectedDiskIndex
        {
            get
            {
                //if (_pathHistory != null)
                //{
                //    var currentDisk = _pathHistory.Last().Substring(0, 2);
                //    var currentDiskIndex = ComboBoxItems.FindIndex(x => x == currentDisk);
                //    return currentDiskIndex;
                //}
                return 0;
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

        public ICommand CommandBackward
        {
            get
            {
                this._commandBackward = new Command(x => Backward());
                return this._commandBackward;
            }
        }

        public ICommand CommandForward
        {
            get
            {
                this._commandForward = new Command(x => Forvard());
                return this._commandForward;
            }
        }

        public void Backward()
        {
            if (_pathBackwardHistory.Count > 1)
            {
                _pathForwardHistory.Add(CurrentPath);
                _pathBackwardHistory.RemoveAt(_pathBackwardHistory.Count - 1);
                NotifyPropertyChanged("ListViewItems");
                //NotifyPropertyChanged("SelectedDiskIndex");
                NotifyPropertyChanged("CurrentPathForTexBox");
            }
        }

        public void Forvard()
        {
            if (_pathForwardHistory.Count > 0)
            {
                _pathBackwardHistory.Add(_pathForwardHistory.Last());
                _pathForwardHistory.RemoveAt(_pathForwardHistory.Count - 1);
                NotifyPropertyChanged("ListViewItems");
              //  NotifyPropertyChanged("SelectedDiskIndex");
                NotifyPropertyChanged("CurrentPathForTexBox");
            }
        }

        public static List<ListViewItem> OpenFolder(string pathToFolder)
        {
            var listViewItems = new List<ListViewItem>();
            var directoryInfo = new DirectoryInfo(pathToFolder);
            var fileCreator = new FileCreator();
            var folderCreator = new FolderCreator();
            foreach (var folder in directoryInfo.GetDirectories())
            {
                var listViewItemFolder = folderCreator.FactoryMathod();
                listViewItemFolder.Name = folder.Name;
                listViewItemFolder.LastWriteTime = folder.LastWriteTime;
                listViewItems.Add(listViewItemFolder);
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Exists)
                {
                    var listViewItemFile = fileCreator.FactoryMathod();
                    listViewItemFile.Name = file.Name;
                    listViewItemFile.Type = Path.GetExtension(file.Name);
                    listViewItemFile.Size = file.Length;
                    listViewItemFile.LastWriteTime = file.LastWriteTime;
                    listViewItems.Add(listViewItemFile);
                }
            }
            return listViewItems;
        }

        public static void ClearPathForwardHistory()
        {
            _pathForwardHistory.Clear();
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
