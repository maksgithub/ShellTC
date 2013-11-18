using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Shell;
using System.Diagnostics;

namespace Shell.State
{
    public class StateFile:IState
    {
        public void Open(string currentFile)
        {
            Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " + ViewModel.Model.CurrentPath + @"\" + currentFile);
        }
    }
}
