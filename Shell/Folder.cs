﻿using System;
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
    }
}
