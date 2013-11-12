using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public class FolderCreator:Creator
    {
        public override ListViewItem FactoryMethod()
        {
            return new Folder();
        }
    }
}
