using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public class File:ListViewItem
    {
        public File()
        {
            ListViewItemState = new Shell.State.StateFile();
        }
    }
}
