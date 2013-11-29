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
        private static List<string> _leftPathBackwardHistory;
        private static List<string> _leftPathForwardHistory = new List<string>();
        private static List<string> _rightPathBackwardHistory;
        private static List<string> _rightPathForwardHistory = new List<string>();
        public static ViewModel Model;
        private ICommand _commandBackward;
        private ICommand _commandForward;
        private ICommand _deleteCommand;
        public event PropertyChangedEventHandler PropertyChanged;


        public string RightCurrentPath
        {
            get
            {
                if (_rightPathBackwardHistory == null || _rightPathBackwardHistory.Count == 0)
                {
                    _rightPathBackwardHistory = new List<string>();
                    _rightPathBackwardHistory.Add(ComboBoxItems.First() + "\\");
                }
                return _rightPathBackwardHistory.Last();
            }
            set
            {
                if (_rightPathBackwardHistory == null)
                {
                    _rightPathBackwardHistory = new List<string>();
                }
                _leftPathBackwardHistory.Add(value);
                NotifyPropertyChanged("RightListViewItems");
                NotifyPropertyChanged("SelectedDiskIndex");
                NotifyPropertyChanged("CurrentPathForTexBox");
            }
        }

        public string LeftCurrentPath
        {
            get
            {
                if (_leftPathBackwardHistory == null || _leftPathBackwardHistory.Count == 0)
                {
                    _leftPathBackwardHistory = new List<string>();
                    _leftPathBackwardHistory.Add(ComboBoxItems.First() + "\\");
                }
                return _leftPathBackwardHistory.Last();
            }
            set
            {
                if (_leftPathBackwardHistory == null)
                {
                    _leftPathBackwardHistory = new List<string>();
                }
                _leftPathBackwardHistory.Add(value);
                NotifyPropertyChanged("LeftListViewItems");
                NotifyPropertyChanged("SelectedDiskIndex");
                NotifyPropertyChanged("CurrentPathForTexBox");
            }
        }

        public List<ListViewItem> LeftListViewItems
        {
            get
            {
                return OpenFolder(LeftCurrentPath);
            }
        }

        public List<ListViewItem> RightListViewItems
        {
            get
            {
                return OpenFolder(RightCurrentPath);
            }
        }

        public string RightCurrentPathForTexBox
        {
            get
            {
                return LeftCurrentPath;
            }
        }

        public string CurrentPathForTexBox
        {
            get
            {
                return LeftCurrentPath;
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
                _selectedDisc = String.Format("{0}{1}", value, "\\").Substring(0,3);
                if (_leftPathBackwardHistory != null && !_selectedDisc.Equals(_leftPathBackwardHistory.Last()))
                    LeftCurrentPath = _selectedDisc;
                NotifyPropertyChanged("SelectedDiskIndex");
            }
        }

        public int SelectedDiskIndex
        {
            get
            {
                if (_leftPathBackwardHistory != null)
                {
                    var currentDisk = _leftPathBackwardHistory.Last().Substring(0, 2);
                    var currentDiskIndex = ComboBoxItems.FindIndex(x => x == currentDisk);
                    return currentDiskIndex;
                }
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
                this._commandForward = new Command(x => Forward());
                return this._commandForward;
            }
        }

        public ICommand DleleteCommand
        {
            get
            {
                this._deleteCommand = new Command(x => Delete());
                return this._deleteCommand;
            }
        }

        public void Delete()
        {
            if (CurrentListViewItem!=null)
                CurrentListViewItem.DeleteListViewItem(CurrentPathForTexBox + @"\" + CurrentListViewItem.Name);
            NotifyPropertyChanged("LeftListViewItems");
        }

        public void Backward()
        {
            if (_leftPathBackwardHistory.Count > 1)
            {
                //TODO:переместить NotifyPropertyChanged и _pathBackwardHistory.Add(value)
                _leftPathForwardHistory.Add(LeftCurrentPath);
                _leftPathBackwardHistory.RemoveAt(_leftPathBackwardHistory.Count - 1);
                NotifyPropertyChanged("LeftListViewItems");
                NotifyPropertyChanged("SelectedDiskIndex");
                NotifyPropertyChanged("CurrentPathForTexBox");
            }
        }

        public void Forward()
        {
            if (_leftPathForwardHistory.Count > 0)
            {
                //TODO:переместить NotifyPropertyChanged и _pathBackwardHistory.Add(value)
                _leftPathBackwardHistory.Add(_leftPathForwardHistory.Last());
                _leftPathForwardHistory.RemoveAt(_leftPathForwardHistory.Count - 1);
                NotifyPropertyChanged("LeftListViewItems");
                NotifyPropertyChanged("SelectedDiskIndex");
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
                if (folder.Exists)
                {
                    var listViewItemFolder = folderCreator.FactoryMathod();
                    listViewItemFolder.Name = folder.Name;
                    listViewItemFolder.LastWriteTime = folder.LastWriteTime;
                    listViewItems.Add(listViewItemFolder);
                }
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
            _leftPathForwardHistory.Clear();
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
