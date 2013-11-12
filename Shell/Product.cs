using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public abstract class Product
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set { value = _name; }
        }
    }
}
