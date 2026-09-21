using System;
using System.Collections.Generic;
using System.Text;

namespace YAUDE.Model
{
    public class DiagramEnum : DiagramElement
    {
        public List<string> Values { get; set; }

        public DiagramEnum() : base()
        {
            Values = new List<string>();
        }

        public override void Draw(Graphics g)
        {
            CalculateSize(g);
            // Draw the class rectangle
            g.FillRectangle(new SolidBrush(color), Position.X, Position.Y, Size.Width, Size.Height);
            g.DrawRectangle(Pens.Black, Position.X, Position.Y, Size.Width, Size.Height);
            // Draw the class name
            g.DrawString(Name, new Font("Arial Black", 10), Brushes.Black, Position.X + 5, Position.Y);
            g.DrawString("<< enum >>", new Font("Arial", 6), Brushes.Black, Position.X + 5, Position.Y + 15);
            // Draw the values
            g.DrawLine(Pens.Black, Position.X, Position.Y + 25, Position.X + Size.Width, Position.Y + 25);

            for (int i = 0; i < Values.Count; i++)
            {
                string value = Values[i];
                if (i != Values.Count - 1) g.DrawString($"{value},", new Font("Arial", 10), Brushes.Black, Position.X + 5, Position.Y + 30 + i * 20);
                else g.DrawString($"{value}", new Font("Arial", 10), Brushes.Black, Position.X + 5, Position.Y + 30 + i * 20);
            }
        }

        protected override void CalculateSize(Graphics g)
        {
            int sizeX, sizeY;

            SizeF stringSize = g.MeasureString(Name, new Font("Arial Black", 10));
            sizeX = (int)stringSize.Width;
            sizeY = 60 + (Values.Count) * 20;
            for (int i = 0; i < Values.Count; i++)
            {
                SizeF attrSize;
                if (i != Values.Count - 1) attrSize = g.MeasureString($"{Values[i]},", new Font("Arial", 10));
                else attrSize = g.MeasureString($"{Values[i]}", new Font("Arial", 10));
                sizeX = Math.Max(sizeX, (int)attrSize.Width);
            }

            Size = new Size(sizeX + 10, sizeY);
        }
    }
}
