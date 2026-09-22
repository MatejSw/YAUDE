using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using YAUDE.Model;

namespace YAUDE.Forms
{
    public partial class EditRelationshipForm : Form
    {
        public Relationship relationship;

        public EditRelationshipForm(Relationship diagramRelationship)
        {
            InitializeComponent();

            relationship = new()
            {
                Type = diagramRelationship.Type,
                Target = diagramRelationship.Target,
                StartNote = diagramRelationship.StartNote,
                EndNote = diagramRelationship.EndNote
            };

            comboBox1.SelectedItem = relationship.Type.ToString();

            textBox1.Text = relationship.StartNote;
            textBox2.Text = relationship.EndNote;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            Dictionary<string, RelationshipType> types = new()
            {
                { "Association", RelationshipType.Association },
                { "Inheritance", RelationshipType.Inheritance },
                { "Realization", RelationshipType.Realization },
                { "Dependency", RelationshipType.Dependency },
                { "Aggregation", RelationshipType.Aggregation },
                { "Composition", RelationshipType.Composition }
            };

            relationship.StartNote = textBox1.Text;
            relationship.EndNote = textBox2.Text;

            relationship.Type = types[comboBox1.SelectedItem.ToString()];

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen startLine = new Pen(Color.Black, 2);
            Pen startLineBg = new Pen(Color.White, 2);
            Pen endLine = new Pen(Color.Black, 2);
            Pen endLineBg = new Pen(Color.White, 2);
            Pen regLine = new Pen(Color.Black, 2);

            if (relationship.Type == RelationshipType.Inheritance)
            {
                Point[] trianglePoints = { new Point(0, 0), new Point(-8, -15), new Point(8, -15) };
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
                Point[] trianglePoints = { new Point(0, 0), new Point(-8, -15), new Point(8, -15) };
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
                Point[] diamondPoints = { new Point(0, 0), new Point(-6, -12), new Point(0, -24), new Point(6, -12) };
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
            e.Graphics.FillRectangle(Brushes.LightBlue, 0, pictureBox1.Height / 3, 30, pictureBox1.Height / 3);
            e.Graphics.DrawRectangle(Pens.Black, 0, pictureBox1.Height / 3, 30, pictureBox1.Height / 3);
            e.Graphics.FillRectangle(Brushes.LightBlue, pictureBox1.Width - 30, pictureBox1.Height / 3, 30, pictureBox1.Height / 3);
            e.Graphics.DrawRectangle(Pens.Black, pictureBox1.Width - 30, pictureBox1.Height / 3, 30, pictureBox1.Height / 3);
            e.Graphics.DrawRectangle(Pens.Black, new(new(0, 0), new(pictureBox1.Width - 1, pictureBox1.Height - 1)));
            e.Graphics.DrawLine(startLineBg, new(30, pictureBox1.Height / 2), new(pictureBox1.Width / 2, pictureBox1.Height / 2));
            e.Graphics.DrawLine(startLine, new(30, pictureBox1.Height / 2), new(pictureBox1.Width / 2, pictureBox1.Height / 2));
            e.Graphics.DrawLine(endLineBg, new(pictureBox1.Width / 2, pictureBox1.Height / 2), new(pictureBox1.Width - 30, pictureBox1.Height / 2));
            e.Graphics.DrawLine(endLine, new(pictureBox1.Width / 2, pictureBox1.Height / 2), new(pictureBox1.Width - 30, pictureBox1.Height / 2));
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Dictionary<string, RelationshipType> types = new()
            {
                { "Association", RelationshipType.Association },
                { "Inheritance", RelationshipType.Inheritance },
                { "Realization", RelationshipType.Realization },
                { "Dependency", RelationshipType.Dependency },
                { "Aggregation", RelationshipType.Aggregation },
                { "Composition", RelationshipType.Composition }
            };
            relationship.Type = types[comboBox1.SelectedItem.ToString()];
            pictureBox1.Invalidate();
        }
    }
}
