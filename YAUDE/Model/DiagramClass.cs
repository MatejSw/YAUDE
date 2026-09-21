using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;
using System.Xml.Linq;

namespace YAUDE.Model
{
    public class DiagramClass : DiagramElement
    {
        public List<Attribute> Attributes { get; set; }
        public List<Method> Methods { get; set; }

        public DiagramClass() : base()
        {
            Attributes = new List<Attribute>();
            Methods = new List<Method>();
        }

        public override void Draw(Graphics g)
        {
            Dictionary<Visibility, string> visibilitySymbols = new Dictionary<Visibility, string>
            {
                { Visibility.Public, "+" },
                { Visibility.Private, "-" },
                { Visibility.Protected, "#" },
                { Visibility.Internal, "~" }
            };

            CalculateSize(g);
            // Draw the class rectangle
            g.FillRectangle(new SolidBrush(color), Position.X, Position.Y, Size.Width, Size.Height);
            g.DrawRectangle(Pens.Black, Position.X, Position.Y, Size.Width, Size.Height);
            // Draw the class name
            g.DrawString(Name, new Font("Arial Black", 10), Brushes.Black, Position.X + 5, Position.Y);
            // Draw the attributes
            g.DrawLine(Pens.Black, Position.X, Position.Y + 20, Position.X + Size.Width, Position.Y + 20);
            for (int i = 0; i < Attributes.Count; i++)
            {
                Attribute attribute = Attributes[i];
                g.DrawString($"{visibilitySymbols[attribute.Visibility]}{attribute.Name}: {attribute.Type}", new Font("Arial", 10), Brushes.Black, Position.X + 5, Position.Y + 25 + i * 20);
            }
            // Draw the methods
            g.DrawLine(Pens.Black, Position.X, Position.Y + 30 + Attributes.Count * 20, Position.X + Size.Width, Position.Y + 30 + Attributes.Count * 20);
            for (int i = 0; i < Methods.Count; i++)
            {
                Method method = Methods[i];
                if (method.ReturnType == "void")
                {
                    g.DrawString($"{visibilitySymbols[method.Visibility]}{method.Name}({method.Parameters})", new Font("Arial", 10), Brushes.Black, Position.X + 5, Position.Y + 35 + (Attributes.Count + i) * 20);
                }
                else
                {
                    g.DrawString($"{visibilitySymbols[method.Visibility]}{method.Name}({method.Parameters}): {method.ReturnType}", new Font("Arial", 10), Brushes.Black, Position.X + 5, Position.Y + 35 + (Attributes.Count + i) * 20);
                }
            }
        }

        protected override void CalculateSize(Graphics g)
        {
            int sizeX, sizeY;

            Dictionary<Visibility, string> visibilitySymbols = new Dictionary<Visibility, string>
            {
                { Visibility.Public, "+" },
                { Visibility.Private, "-" },
                { Visibility.Protected, "#" },
                { Visibility.Internal, "~" }
            };

            SizeF stringSize = g.MeasureString(Name, new Font("Arial Black", 10));
            sizeX = (int)stringSize.Width;
            sizeY = 60 + (Attributes.Count + Methods.Count) * 20;
            for (int i = 0; i < Attributes.Count; i++)
            {
                SizeF attrSize = g.MeasureString($"{visibilitySymbols[Attributes[i].Visibility]}{Attributes[i].Name}: {Attributes[i].Type}", new Font("Arial", 10));
                sizeX = Math.Max(sizeX, (int)attrSize.Width);
            }
            for (int i = 0; i < Methods.Count; i++)
            {
                SizeF methodSize = new SizeF(0, 0);
                if (Methods[i].ReturnType == "void")
                {
                    methodSize = g.MeasureString($"{visibilitySymbols[Methods[i].Visibility]}{Methods[i].Name}({Methods[i].Parameters})", new Font("Arial", 10));
                }
                else
                {
                    methodSize = g.MeasureString($"{visibilitySymbols[Methods[i].Visibility]}{Methods[i].Name}({Methods[i].Parameters}): {Methods[i].ReturnType}", new Font("Arial", 10));
                }
                sizeX = Math.Max(sizeX, (int)methodSize.Width);
            }

            Size = new Size(sizeX + 10, sizeY);
        }
    }
}
