using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YAUDE.Model
{
    public class Method
    {
        public string Name { get; set; }
        [DisplayName("Return Type")]
        public string ReturnType { get; set; }
        public string Parameters { get; set; }
        public Visibility Visibility { get; set; }
    }
}
