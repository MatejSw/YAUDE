using System;
using System.Collections.Generic;
using System.Text;

namespace YAUDE.Model
{
    public class Relationship
    {
        public DiagramClass Target { get; set; }
        public RelationshipType Type { get; set; }
    }
}
