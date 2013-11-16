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
        private ICommand _bindCommand;

        public IState ListViewItemState { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public DateTime LastWriteTime { get; set; }

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
             ListViewItemState.Open(Name);
        }
    }
}
