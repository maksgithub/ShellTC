using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class FileCreator:Creator
    {
        public override ListViewItem FactoryMathod()
        {
            return new File();
        }
    }
}
