using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public class FileCreator:Creator
    {
        public override ListViewItem FactoryMethod()
        {
            return new File();
        }
    }
}
