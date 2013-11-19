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

        public override void Delete(string path)
        {
            System.IO.File.Delete(path);
        }

        public override bool RemoveAccess(string fullPath)
        {
            if(System.IO.File.Exists(fullPath))
                return true;
            return false;
        }
    }
}
