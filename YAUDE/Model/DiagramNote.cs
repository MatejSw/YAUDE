using System;
using System.Collections.Generic;
using System.Text;

namespace YAUDE.Model
{
    public class DiagramNote : DiagramElement
    {
        public string Text { get; set; }
        private int wordsPerLine = 6;

        public DiagramNote() : base()
        {
            Type = "Note";
        }

        public override void Draw(Graphics g)
        {
            CalculateSize(g);
            base.Draw(g);

            List<string> sections = Text.Split('\n').ToList();
            int offset = 0;

            for (int i = 0; i < sections.Count; i++)
            {
                List<string> words = sections[i].Split(' ').ToList();

                for (int j = 0; j < words.Count / wordsPerLine + 1; j++)
                {
                    List<string> temp = new();
                    for (int k = 0; k < (j != words.Count / wordsPerLine ? wordsPerLine : words.Count - j * wordsPerLine); k++)
                    {
                        temp.Add(words[k + j * wordsPerLine]);
                    }
                    g.DrawString(string.Join(" ", temp), new Font("Arial", 8), Brushes.Black, Position.X, Position.Y + offset);
                    offset += (int)g.MeasureString(string.Join(" ", temp), new Font("Arial", 8)).Height;
                }

                if (words.Count == 1) offset += 12;
            }
        }

        protected override void CalculateSize(Graphics g)
        {
            int sizeX = 35, sizeY = 0;

            SizeF sizeF = new();

            List<string> sections = Text.Split('\n').ToList();

            for (int i = 0; i < sections.Count; i++)
            {
                List<string> words = sections[i].Split(' ').ToList();

                for (int j = 0; j < words.Count / wordsPerLine + 1; j++)
                {
                    List<string> temp = new();
                    for (int k = 0; k < (j != words.Count / wordsPerLine ? wordsPerLine : words.Count - j * wordsPerLine); k++)
                    {
                        temp.Add(words[k + j * wordsPerLine]);
                    }
                    sizeF = g.MeasureString(string.Join(" ", temp), new Font("Arial", 8));
                    sizeX = Math.Max(sizeX, (int)(sizeF.Width));
                }
                sizeY = (words.Count / wordsPerLine + 1) * 15 + (sections.Count) * 15;
            }

            Size = new Size(sizeX + 5, sizeY);
        }
    }
}
