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
        private string _leftSelectedDisc;
        private string _rightSelectedDisc;
        private ListViewItem _leftCurrentListViewItem;
        private ListViewItem _rightCurrentListViewItem;
        private static List<string> _leftPathBackwardHistory;
        private static List<string> _leftPathForwardHistory = new List<string>();
        private static List<string> _rightPathBackwardHistory;
        private static List<string> _rightPathForwardHistory = new List<string>();
        public static ViewModel Model;
        private ICommand _leftCommandBackward;
        private ICommand _leftDeleteCommand;
        private ICommand _leftCommandForward;
        private ICommand _rightCommandBackward;
        private ICommand _rightCommandForward;
        private ICommand _rightDeleteCommand;
        private byte _selectedList;
        public event PropertyChangedEventHandler PropertyChanged;

        #region LeftListViewItem

            public List<string> LeftComboBoxItems
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

            public string LeftCurrentPathForTexBox
        {
            get
            {
                return LeftCurrentPath;
            }
        }

            public string LeftSelectedDisc
        {
            get
            {
                return _leftSelectedDisc;
            }
            set
            {
                _leftSelectedDisc = String.Format("{0}{1}", value, "\\").Substring(0, 3);
                if (_leftPathBackwardHistory != null && !_leftSelectedDisc.Equals(_leftPathBackwardHistory.Last()))
                    LeftCurrentPath = _leftSelectedDisc;
                //NotifyPropertyChanged("LeftSelectedDiskIndex");
            }
        }

            public string LeftCurrentPath
        {
            get
            {
                if (_leftPathBackwardHistory == null || _leftPathBackwardHistory.Count == 0)
                {
                    _leftPathBackwardHistory = new List<string>();
                    _leftPathBackwardHistory.Add(LeftComboBoxItems.First() + "\\");
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
                NotifyPropertyChanged("LeftSelectedDiskIndex");
                NotifyPropertyChanged("LeftCurrentPathForTexBox");
            }
        }

            public List<ListViewItem> LeftListViewItems
        {
            get
            {
                return OpenFolder(LeftCurrentPath);
            }
        }

            public ListViewItem LeftCurrentListViewItem
        {
            get
            {
                return _leftCurrentListViewItem;
            }
            set
            {
                _leftCurrentListViewItem = value;
            }
        }

            public int LeftSelectedDiskIndex
        {
            get
            {
                if (_leftPathBackwardHistory != null)
                {
                    var currentDisk = _leftPathBackwardHistory.Last().Substring(0, 2);
                    var currentDiskIndex = LeftComboBoxItems.FindIndex(x => x == currentDisk);
                    return currentDiskIndex;
                }
                return 0;
            }
        }

            public ICommand LeftCommandBackward
        {
            get
            {
                this._leftCommandBackward = new Command(x => LeftBackward());
                return this._leftCommandBackward;
            }
        }

            public ICommand LeftCommandForward
        {
            get
            {
                this._leftCommandForward = new Command(x => LeftForward());
                return this._leftCommandForward;
            }
        }

            public ICommand DleleteCommand
            {
                get
                {
                    this._leftDeleteCommand = new Command(x => LeftListViewItemDelete());
                    return this._leftDeleteCommand;
                }
            }

            

            public void LeftBackward()
        {
            if (_leftPathBackwardHistory.Count > 1)
            {
                //TODO:переместить NotifyPropertyChanged и _pathBackwardHistory.Add(value)
                _leftPathForwardHistory.Add(LeftCurrentPath);
                _leftPathBackwardHistory.RemoveAt(_leftPathBackwardHistory.Count - 1);
                NotifyPropertyChanged("LeftListViewItems");
                NotifyPropertyChanged("LeftSelectedDiskIndex");
                NotifyPropertyChanged("LeftCurrentPathForTexBox");
            }
        }

            public void LeftForward()
        {
            if (_leftPathForwardHistory.Count > 0)
            {
                //TODO:переместить NotifyPropertyChanged и _pathBackwardHistory.Add(value)
                _leftPathBackwardHistory.Add(_leftPathForwardHistory.Last());
                _leftPathForwardHistory.RemoveAt(_leftPathForwardHistory.Count - 1);
                NotifyPropertyChanged("LeftListViewItems");
                NotifyPropertyChanged("LeftSelectedDiskIndex");
                NotifyPropertyChanged("LeftCurrentPathForTexBox");
            }
        }

            public void LeftListViewItemDelete()
        {
            if (LeftCurrentListViewItem != null)
                LeftCurrentListViewItem.DeleteListViewItem(LeftCurrentPathForTexBox + @"\" + LeftCurrentListViewItem.Name);
            NotifyPropertyChanged("LeftListViewItems");
        }

        #endregion LeftListViewItem

        #region RightListViewItems

            public List<string> RightComboBoxItems
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

            public string RightCurrentPathForTexBox
        {
            get
            {
                return RightCurrentPath;
            }
        }

            public string RightSelectedDisc
        {
            get
            {
                return _rightSelectedDisc;
            }
            set
            {
                _rightSelectedDisc = String.Format("{0}{1}", value, "\\").Substring(0, 3);
                if (_rightPathBackwardHistory != null && !_rightSelectedDisc.Equals(_rightPathBackwardHistory.Last()))
                    RightCurrentPath = _rightSelectedDisc;
            }
        }

            public string RightCurrentPath
        {
            get
            {
                if (_rightPathBackwardHistory == null || _rightPathBackwardHistory.Count == 0)
                {
                    _rightPathBackwardHistory = new List<string>();
                    _rightPathBackwardHistory.Add(LeftComboBoxItems.First() + "\\");
                }
                return _rightPathBackwardHistory.Last();
            }
            set
            {
                if (_rightPathBackwardHistory == null)
                {
                    _rightPathBackwardHistory = new List<string>();
                }
                _rightPathBackwardHistory.Add(value);
                NotifyPropertyChanged("RightListViewItems");
                NotifyPropertyChanged("RightSelectedDiskIndex");
                NotifyPropertyChanged("RightCurrentPathForTexBox");
            }
        }

            public List<ListViewItem> RightListViewItems
        {
            get
            {
                return OpenFolder(RightCurrentPath);
            }
        }

            public ListViewItem RightCurrentListViewItem
            {
                get
                {
                    return _rightCurrentListViewItem;
                }
                set
                {
                    _rightCurrentListViewItem = value;
                }
            }

            public int RightSelectedDiskIndex
            {
                get
                {
                    if (_rightPathBackwardHistory != null)
                    {
                        var currentDisk = _rightPathBackwardHistory.Last().Substring(0, 2);
                        var currentDiskIndex = RightComboBoxItems.FindIndex(x => x == currentDisk);
                        return currentDiskIndex;
                    }
                    return 0;
                }
            }

            public ICommand RightCommandBackward
            {
                get
                {
                    this._rightCommandBackward = new Command(x => RightBackward());
                    return this._rightCommandBackward;
                }
            }

            public ICommand RightCommandForward
            {
                get
                {
                    this._rightCommandForward = new Command(x => RightForward());
                    return this._rightCommandForward;
                }
            }

            public ICommand RightListViewSelect
            {
                get
                {
                    return new Command(x => RightSelectListView());
                }
            }

            public void RightSelectListView()
            {
                _selectedList = 2; 
            }

            public void RightBackward()
            {
                if (_rightPathBackwardHistory.Count > 1)
                {
                    _rightPathForwardHistory.Add(RightCurrentPath);
                    _rightPathBackwardHistory.RemoveAt(_rightPathBackwardHistory.Count - 1);
                    NotifyPropertyChanged("RightListViewItems");
                    NotifyPropertyChanged("RightSelectedDiskIndex");
                    NotifyPropertyChanged("RightCurrentPathForTexBox");
                }
            }

            public void RightForward()
            {
                if (_rightPathForwardHistory.Count > 0)
                {
                    _rightPathBackwardHistory.Add(_rightPathForwardHistory.Last());
                    _rightPathForwardHistory.RemoveAt(_rightPathForwardHistory.Count - 1);
                    NotifyPropertyChanged("RightListViewItems");
                    NotifyPropertyChanged("RightSelectedDiskIndex");
                    NotifyPropertyChanged("RightCurrentPathForTexBox");
                }
            }

            public void RightListViewItemDelete()
            {
                if (RightCurrentListViewItem != null)
                    RightCurrentListViewItem.DeleteListViewItem(RightCurrentPathForTexBox + @"\" + RightCurrentListViewItem.Name);
                NotifyPropertyChanged("RightListViewItems");
            }

        #endregion RightListViewItems 

        

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

        public static void ClearLeftPathForwardHistory()
        {
            _leftPathForwardHistory.Clear();
        }

        public static void ClearRightPathForwardHistory()
        {
            _rightPathForwardHistory.Clear();
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
