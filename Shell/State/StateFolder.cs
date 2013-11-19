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
        public void Open(string currentDirectory)
        {
            if (ViewModel.Model.LeftCurrentPath.Length == 3)
            {
                ViewModel.Model.LeftCurrentPath = string.Format("{0}{1}", ViewModel.Model.LeftCurrentPath, currentDirectory);
            }
            else
            {
                ViewModel.Model.LeftCurrentPath = string.Format("{0}\\{1}", ViewModel.Model.LeftCurrentPath, currentDirectory);
            }
            ViewModel.ClearPathForwardHistory();
        }
    }
}
