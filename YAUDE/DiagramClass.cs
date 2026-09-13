using System;
using System.Collections.Generic;
using System.Text;

namespace YAUDE
{
    public class DiagramClass
    {
        public string Name { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }
        public Color color { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<Method> Methods { get; set; }
        public List<string> Associations { get; set; }
        public List<string> Dependencies { get; set; }

        public DiagramClass()
        {
            Attributes = new List<Attribute>();
            Methods = new List<Method>();
            Associations = new List<string>();
            Dependencies = new List<string>();
        }
    }
}
