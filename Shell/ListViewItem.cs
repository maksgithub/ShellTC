using Shell.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Shell
{
    public abstract class ListViewItem
    {
        private ICommand _listViewItemDoubleClickCommand;
        private byte _selectedList;
        public IState ListViewItemState { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public DateTime LastWriteTime { get; set; }

        public ICommand LeftListViewSelect
        {
            get
            {
                return new Command(x => leftListViewSelect());
            }
        }

        public ICommand RightListViewSelect
        {
            get
            {
                return new Command(x => rightListViewSelect());
            }
        }

        private void leftListViewSelect()
        {
            _selectedList = 1;
        }

        private void rightListViewSelect()
        {
            _selectedList = 2;
        }

        public ICommand ListViewItemDoubleClickCommand
        {
            get
            {
                this._listViewItemDoubleClickCommand = new Command(x => 
                    {
                        if (x.ToString() == "1")
                        {
                            ViewModel.Model.LeftCurrentPath = string.Format(@"{0}\{1}", ViewModel.Model.LeftCurrentPath, Name);
                            ViewModel.ClearLeftPathForwardHistory();
                        }
                        if (x.ToString() == "2")
                        {
                            ViewModel.Model.RightCurrentPath = string.Format(@"{0}\{1}", ViewModel.Model.RightCurrentPath, Name);
                            ViewModel.ClearRightPathForwardHistory();
                        }
                    });
                return this._listViewItemDoubleClickCommand;
            }
        }

        public void Open(string currentPath)
        {
             ListViewItemState.Open(currentPath);
        }

        public abstract void Delete(string fullPath);
        public abstract bool RemoveAccess(string fullPath);

        public void DeleteListViewItem(string fullPath)
        {
            if (RemoveAccess(fullPath))
            {
                Delete(fullPath);
            }
            else
            {
                MessageBox.Show("You can not delete");
            }
        }
    }
}
