using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YAUDE.Model
{
    public class Attribute
    {
        public string Name { get; set; }
        public string Type { get; set; }
        [Column("Visibility")]
        public Visibility Visibility { get; set; }
    }
}
