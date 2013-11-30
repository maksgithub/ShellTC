using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shell.State
{
    public class StateFolder: IState
    {
        public void Open(string currentPath)
        {
            ViewModel.Model.LeftCurrentPath = currentPath;
        }
    }
}
