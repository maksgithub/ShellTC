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
    public class ListViewItem
    {
        private string _name;
        private string _type;
        private long _size;
        private ICommand _bindCommand;
        private DateTime _lastWriteTime;
        private IState _listViewItemState;

        public IState ListViewItemState 
        {
            get
            {
                return _listViewItemState;
            }
            set
            {
                _listViewItemState = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            private set { value = _name; }
        }
        public string Type
        {
            get
            {
                return _type;
            }
            private set { value = _type; }
        }
        public long Size
        {
            get
            {
                return _size;
            }
            private set { value = _size; }
        }
        public DateTime LastWriteTime
        {
            get
            {
                return _lastWriteTime;
            }
            private set { value = _lastWriteTime; }
        }

        public ICommand Command
        {
            get
            {
                this._bindCommand = new Command(x => Open());
                return this._bindCommand;
            }
        }

        public void Open()
        {
             _listViewItemState.Open(Name);
        }

        public ListViewItem(string name,string type, long size, DateTime lastWriteTime)
        {
            _name=name;
            _type = type;
            _size = size;
            _lastWriteTime = lastWriteTime;
            _listViewItemState = new StateFile();
        }
        public ListViewItem(string name, DateTime lastWriteTime)
            : this(name, "<Папка>", 0,lastWriteTime)
        {
            _listViewItemState = new StateFolder();
        }
    }
}
