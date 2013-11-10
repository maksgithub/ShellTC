using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    public class Person
    {
        public Person(string fName, string sName)
        {
            FName = fName;
            SName = sName;
        }
        public string FName {get;set;}
        public string SName { get; set; }
    }
}
