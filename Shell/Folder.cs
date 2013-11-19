using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public class Folder:ListViewItem
    {
        public Folder()
        {
            ListViewItemState = new Shell.State.StateFolder();
            Type = "<Папка>";
        }

        public override void Delete(string path)
        {
            System.IO.Directory.Delete(path, true);
        }

        public override bool RemoveAccess(string fullPath)
        {
            if (System.IO.Directory.Exists(fullPath))
                return true;
            return false;
        }
    }
}
