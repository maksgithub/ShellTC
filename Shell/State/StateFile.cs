using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Shell;

namespace Shell.State
{
    public class StateFile:IState
    {
        public void Open(string currentFile)
        {
            MessageBox.Show("File Double Click");
        }
    }
}
