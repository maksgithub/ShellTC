using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shell.State
{
    public class StateFolder:ListViewItem, IState
    {
        public void Open(string currentDirectory)
        {
            if (ViewModel.Model.CurrentPath.Length == 3)
            {
                ViewModel.Model.CurrentPath = string.Format("{0}{1}", ViewModel.Model.CurrentPath, currentDirectory);
            }
            else
            {
                ViewModel.Model.CurrentPath = string.Format("{0}\\{1}", ViewModel.Model.CurrentPath, currentDirectory);
            }
            ViewModel.ClearPathForwardHistory();
        }
    }
}
