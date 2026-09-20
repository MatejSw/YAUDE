using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;
using System.Xml.Linq;

namespace YAUDE.Model
{
    public class DiagramClass
    {
        public string Name { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }
        public Color color { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<Method> Methods { get; set; }
        public List<Relationship> Relationships { get; set; }

        public DiagramClass()
        {
            Attributes = new List<Attribute>();
            Methods = new List<Method>();
            Relationships = new List<Relationship>();
        }

        public void DrawRelationships(Graphics g, double zoom)
        {
            int sizeX, sizeY;

            zoom = zoom / 100;

            CalculateSize(g);

            for (int i = 0; i < Relationships.Count(); i++)
            {
                Relationship relationship = Relationships[i];
                Point vector = new Point(relationship.Target.Position.X + relationship.Target.Size.Width / 2 - (Position.X + Size.Width / 2), relationship.Target.Position.Y + relationship.Target.Size.Height / 2 - (Position.Y + Size.Height / 2));
                Point center = new Point(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);

                Pen startLine = new Pen(Color.Black, 2);
                Pen startLineBg = new Pen(Color.White, 2);
                Pen endLine = new Pen(Color.Black, 2);
                Pen endLineBg = new Pen(Color.White, 2);
                Pen regLine = new Pen(Color.Black, 2);

                if(relationship.Type == RelationshipType.Inheritance)
                {
                    Point[] trianglePoints = { new Point(0, 0), new Point((int)(-8 / (double)zoom), (int)(-15 / (double)zoom)), new Point((int)(8 / (double)zoom), (int)(-15 / (double)zoom)) };
                    GraphicsPath path = new GraphicsPath();
                    path.AddPolygon(trianglePoints);
                    Point[] trianglePointsFill = { new Point(0, 0), new Point(-8, -15), new Point(8, -15) };
                    GraphicsPath fill = new GraphicsPath();
                    fill.AddPolygon(trianglePointsFill);
                    endLine.CustomEndCap = new CustomLineCap(null, path);
                    endLineBg.CustomEndCap = new CustomLineCap(fill, null);
                }
                else if (relationship.Type == RelationshipType.Realization)
                {
                    startLine.DashStyle = DashStyle.Dash;
                    Point[] trianglePoints = { new Point(0, 0), new Point((int)(-8 / (double)zoom), (int)(-15 / (double)zoom)), new Point((int)(8 / (double)zoom), (int)(-15 / (double)zoom)) };
                    GraphicsPath path = new GraphicsPath();
                    path.AddPolygon(trianglePoints);
                    Point[] trianglePointsFill = { new Point(0, 0), new Point(-8, -15), new Point(8, -15) };
                    GraphicsPath fill = new GraphicsPath();
                    fill.AddPolygon(trianglePointsFill);
                    endLine.CustomEndCap = new CustomLineCap(null, path);
                    endLineBg.CustomEndCap = new CustomLineCap(fill, null);
                    endLineBg.Color = Color.LightBlue;
                    endLine.DashStyle = DashStyle.Dash;
                    endLineBg.DashStyle = DashStyle.Dash;
                    regLine.DashStyle = DashStyle.Dash;
                }
                else if (relationship.Type == RelationshipType.Aggregation)
                {
                    Point[] diamondPoints = { new Point(0, 0), new Point((int)(-6 / (double)zoom), (int)(-12 / (double)zoom)), new Point((int)(0 / (double)zoom), (int)(-24 / (double)zoom)), new Point((int)(6 / (double)zoom), (int)(-12 / (double)zoom))};
                    GraphicsPath path = new GraphicsPath();
                    path.AddPolygon(diamondPoints);
                    Point[] diamondPointsFill = { new Point(0, 0), new Point(-6, -12), new Point(0, -24), new Point(6, -12) };
                    GraphicsPath fill = new GraphicsPath();
                    fill.AddPolygon(diamondPointsFill);
                    startLineBg.CustomStartCap = new CustomLineCap(fill, null);
                    startLine.CustomStartCap = new CustomLineCap(null, path);
                }
                else if (relationship.Type == RelationshipType.Composition)
                {
                    Point[] diamondPointsFill = { new Point(0, 0), new Point(-6, -12), new Point(0, -24), new Point(6, -12) };
                    GraphicsPath fill = new GraphicsPath();
                    fill.AddPolygon(diamondPointsFill);
                    startLine.CustomStartCap = new CustomLineCap(fill, null);
                }
                else if (relationship.Type == RelationshipType.Dependency)
                {
                    startLine.DashStyle = DashStyle.Dash;
                    regLine.DashStyle = DashStyle.Dash;
                    endLine.CustomEndCap = new AdjustableArrowCap(10,10,false);
                    endLine.DashStyle = DashStyle.Dash;
                }

                if (Math.Abs(vector.X) > Math.Abs(vector.Y))
                {
                    if (vector.X > 0)
                    {
                        g.DrawLine(startLineBg, new Point(center.X + this.Size.Width / 2, center.Y), new Point(center.X + vector.X / 2, center.Y));
                        g.DrawLine(startLine, new Point(center.X + this.Size.Width / 2, center.Y), new Point(center.X + vector.X / 2, center.Y));
                        g.DrawLine(regLine, new Point(center.X + vector.X / 2, center.Y), new Point(center.X + vector.X / 2, center.Y + vector.Y));
                        g.DrawLine(endLineBg, center.X + vector.X / 2, center.Y + vector.Y, relationship.Target.Position.X, relationship.Target.Position.Y + relationship.Target.Size.Height / 2);
                        g.DrawLine(endLine, center.X + vector.X / 2, center.Y + vector.Y, relationship.Target.Position.X, relationship.Target.Position.Y + relationship.Target.Size.Height / 2);
                    }
                    else
                    {
                        g.DrawLine(startLineBg, new Point(center.X - this.Size.Width / 2, center.Y), new Point(center.X + vector.X / 2, center.Y));
                        g.DrawLine(startLine, new Point(center.X - this.Size.Width / 2, center.Y), new Point(center.X + vector.X / 2, center.Y));
                        g.DrawLine(regLine, new Point(center.X + vector.X / 2, center.Y), new Point(center.X + vector.X / 2, center.Y + vector.Y));
                        g.DrawLine(endLineBg, center.X + vector.X / 2, center.Y + vector.Y, relationship.Target.Position.X + relationship.Target.Size.Width, relationship.Target.Position.Y + relationship.Target.Size.Height / 2);
                        g.DrawLine(endLine, center.X + vector.X / 2, center.Y + vector.Y, relationship.Target.Position.X + relationship.Target.Size.Width, relationship.Target.Position.Y + relationship.Target.Size.Height / 2);
                    }
                }
                else
                {
                    if (vector.Y > 0)
                    {
                        g.DrawLine(startLineBg, new Point(center.X, center.Y + this.Size.Height / 2), new Point(center.X, center.Y + vector.Y / 2));
                        g.DrawLine(startLine, new Point(center.X, center.Y + this.Size.Height / 2), new Point(center.X, center.Y + vector.Y / 2));
                        g.DrawLine(regLine, new Point(center.X, center.Y + vector.Y / 2), new Point(center.X + vector.X, center.Y + vector.Y / 2));
                        g.DrawLine(endLineBg, center.X + vector.X, center.Y + vector.Y / 2, relationship.Target.Position.X + relationship.Target.Size.Width / 2, relationship.Target.Position.Y);
                        g.DrawLine(endLine, center.X + vector.X, center.Y + vector.Y / 2, relationship.Target.Position.X + relationship.Target.Size.Width / 2, relationship.Target.Position.Y);
                    }
                    else
                    {
                        g.DrawLine(startLineBg, new Point(center.X, center.Y - this.Size.Height / 2), new Point(center.X, center.Y + vector.Y / 2));
                        g.DrawLine(startLine, new Point(center.X, center.Y - this.Size.Height / 2), new Point(center.X, center.Y + vector.Y / 2));
                        g.DrawLine(regLine, new Point(center.X, center.Y + vector.Y / 2), new Point(center.X + vector.X, center.Y + vector.Y / 2));
                        g.DrawLine(endLineBg, center.X + vector.X, center.Y + vector.Y / 2, relationship.Target.Position.X + relationship.Target.Size.Width / 2, relationship.Target.Position.Y + relationship.Target.Size.Height);
                        g.DrawLine(endLine, center.X + vector.X, center.Y + vector.Y / 2, relationship.Target.Position.X + relationship.Target.Size.Width / 2, relationship.Target.Position.Y + relationship.Target.Size.Height);
                    }
                }
            }
        }

        public void Draw(Graphics g)
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

        private void CalculateSize(Graphics g)
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
