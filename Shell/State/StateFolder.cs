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
            ViewModel.Model.CurrentPath = string.Format("{0}\\{1}", ViewModel.Model.CurrentPath, currentDirectory);
        }
    }
}
