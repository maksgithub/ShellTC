using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shell.State
{
    public class StateFolder:IState
    {
        public void Open(string currentDirectory)
        {
            ViewModel.OpenFolder(string.Format("{0}{1}", ViewModel.CurrentPath, currentDirectory));
            MessageBox.Show("Open folder");
        }
    }
}
