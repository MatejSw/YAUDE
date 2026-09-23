using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace YAUDE.Model
{
    public class DiagramElement
    {
        public string Name { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }
        public Color color { get; set; }
        public Visibility Visibility { get; set; }
        public List<Relationship> Relationships { get; set; }
        public string Type { get; set; }

        public DiagramElement()
        {
            Relationships = new List<Relationship>();
            Visibility = Visibility.Public;
        }

        public virtual void DrawRelationships(Graphics g, double zoom)
        {
            int sizeX, sizeY;

            zoom = Math.Min(zoom / 100, 1);

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

                if (relationship.Type == RelationshipType.Inheritance)
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
                    Point[] diamondPoints = { new Point(0, 0), new Point((int)(-6 / (double)zoom), (int)(-12 / (double)zoom)), new Point((int)(0 / (double)zoom), (int)(-24 / (double)zoom)), new Point((int)(6 / (double)zoom), (int)(-12 / (double)zoom)) };
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
                    endLine.CustomEndCap = new AdjustableArrowCap(10, 10, false);
                    endLine.DashStyle = DashStyle.Dash;
                }

                if (Math.Abs(vector.X) > Math.Abs(vector.Y))
                {
                    if (vector.X > 0)
                    {
                        g.DrawLine(startLineBg, new Point(center.X + this.Size.Width / 2, center.Y), new Point(center.X + vector.X / 2, center.Y));
                        g.DrawLine(startLine, new Point(center.X + this.Size.Width / 2, center.Y), new Point(center.X + vector.X / 2, center.Y));
                        g.DrawString(relationship.StartNote, new("Arial", 6), Brushes.Black, new Point(center.X + this.Size.Width / 2 + 2, center.Y - 12));
                        g.DrawLine(regLine, new Point(center.X + vector.X / 2, center.Y), new Point(center.X + vector.X / 2, center.Y + vector.Y));
                        g.DrawLine(endLineBg, center.X + vector.X / 2, center.Y + vector.Y, relationship.Target.Position.X, relationship.Target.Position.Y + relationship.Target.Size.Height / 2);
                        g.DrawLine(endLine, center.X + vector.X / 2, center.Y + vector.Y, relationship.Target.Position.X, relationship.Target.Position.Y + relationship.Target.Size.Height / 2);
                        g.DrawString(relationship.EndNote, new("Arial", 6), Brushes.Black, new Point(relationship.Target.Position.X - 2 - (int)g.MeasureString(relationship.EndNote, new Font("Arial", 6)).Width, relationship.Target.Position.Y + relationship.Target.Size.Height / 2 - 12));
                    }
                    else
                    {
                        g.DrawLine(startLineBg, new Point(center.X - this.Size.Width / 2, center.Y), new Point(center.X + vector.X / 2, center.Y));
                        g.DrawLine(startLine, new Point(center.X - this.Size.Width / 2, center.Y), new Point(center.X + vector.X / 2, center.Y));
                        g.DrawString(relationship.StartNote, new("Arial", 6), Brushes.Black, new Point(center.X - this.Size.Width / 2 - 2 - (int)g.MeasureString(relationship.StartNote, new Font("Arial", 6)).Width, center.Y - 12));
                        g.DrawLine(regLine, new Point(center.X + vector.X / 2, center.Y), new Point(center.X + vector.X / 2, center.Y + vector.Y));
                        g.DrawLine(endLineBg, center.X + vector.X / 2, center.Y + vector.Y, relationship.Target.Position.X + relationship.Target.Size.Width, relationship.Target.Position.Y + relationship.Target.Size.Height / 2);
                        g.DrawLine(endLine, center.X + vector.X / 2, center.Y + vector.Y, relationship.Target.Position.X + relationship.Target.Size.Width, relationship.Target.Position.Y + relationship.Target.Size.Height / 2);
                        g.DrawString(relationship.EndNote, new("Arial", 6), Brushes.Black, new Point(relationship.Target.Position.X + relationship.Target.Size.Width + 2, relationship.Target.Position.Y + relationship.Target.Size.Height / 2 - 12));
                    }
                }
                else
                {
                    if (vector.Y > 0)
                    {
                        g.DrawLine(startLineBg, new Point(center.X, center.Y + this.Size.Height / 2), new Point(center.X, center.Y + vector.Y / 2));
                        g.DrawLine(startLine, new Point(center.X, center.Y + this.Size.Height / 2), new Point(center.X, center.Y + vector.Y / 2));
                        g.DrawString(relationship.StartNote, new("Arial", 6), Brushes.Black, new Point(center.X + 5, center.Y + this.Size.Height / 2 + 6));
                        g.DrawLine(regLine, new Point(center.X, center.Y + vector.Y / 2), new Point(center.X + vector.X, center.Y + vector.Y / 2));
                        g.DrawLine(endLineBg, center.X + vector.X, center.Y + vector.Y / 2, relationship.Target.Position.X + relationship.Target.Size.Width / 2, relationship.Target.Position.Y);
                        g.DrawLine(endLine, center.X + vector.X, center.Y + vector.Y / 2, relationship.Target.Position.X + relationship.Target.Size.Width / 2, relationship.Target.Position.Y);
                        g.DrawString(relationship.EndNote, new("Arial", 6), Brushes.Black, new Point(relationship.Target.Position.X + relationship.Target.Size.Width / 2 + 5, relationship.Target.Position.Y - 12));
                    }
                    else
                    {
                        g.DrawLine(startLineBg, new Point(center.X, center.Y - this.Size.Height / 2), new Point(center.X, center.Y + vector.Y / 2));
                        g.DrawLine(startLine, new Point(center.X, center.Y - this.Size.Height / 2), new Point(center.X, center.Y + vector.Y / 2));
                        g.DrawString(relationship.StartNote, new("Arial", 6), Brushes.Black, new Point(center.X + 5, center.Y - this.Size.Height / 2 - 12));
                        g.DrawLine(regLine, new Point(center.X, center.Y + vector.Y / 2), new Point(center.X + vector.X, center.Y + vector.Y / 2));
                        g.DrawLine(endLineBg, center.X + vector.X, center.Y + vector.Y / 2, relationship.Target.Position.X + relationship.Target.Size.Width / 2, relationship.Target.Position.Y + relationship.Target.Size.Height);
                        g.DrawLine(endLine, center.X + vector.X, center.Y + vector.Y / 2, relationship.Target.Position.X + relationship.Target.Size.Width / 2, relationship.Target.Position.Y + relationship.Target.Size.Height);
                        g.DrawString(relationship.EndNote, new("Arial", 6), Brushes.Black, new Point(relationship.Target.Position.X + relationship.Target.Size.Width / 2 + 5, relationship.Target.Position.Y + relationship.Target.Size.Height + 6));
                    }
                }
            }
        }
        public virtual void Draw(Graphics g)
        {
            Pen outline = new(Color.Black, 2);

            switch (Visibility)
            {
                case Visibility.Public:
                    outline.DashStyle = DashStyle.Solid;
                    break;

                case Visibility.Private:
                    outline.DashStyle = DashStyle.Dot;
                    break;

                case Visibility.Protected:
                    outline.DashStyle = DashStyle.DashDot;
                    break;

                case Visibility.Internal:
                    outline.DashStyle = DashStyle.Dash;
                    break;
            }

            g.FillRectangle(new SolidBrush(color), Position.X, Position.Y, Size.Width, Size.Height);
            g.DrawRectangle(outline, Position.X, Position.Y, Size.Width, Size.Height);
        }

        protected virtual void CalculateSize(Graphics g)
        {

        }
    }
}
