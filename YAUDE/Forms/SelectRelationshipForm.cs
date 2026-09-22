using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YAUDE.Model;

namespace YAUDE.Forms
{
    public partial class SelectRelationshipForm : Form
    {
        public int index = 0;
        public SelectRelationshipForm(List<Relationship> relationships, List<DiagramElement> sources)
        {
            InitializeComponent();

            for (int i = 0; i < relationships.Count; i++)
            {
                Button button = new Button()
                {
                    Text = $"{sources[i].Name} --> {relationships[i].Target.Name} ({relationships[i].Type})",
                    Width = flowLayoutPanel1.Width - 1,
                    Height = 35,
                    Margin = new Padding(0, 12, 0, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = i
                };

                button.Click += (sender, e) => {
                    index = Convert.ToInt32(button.Tag);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                };

                flowLayoutPanel1.Controls.Add(button);
            }
        }

        public SelectRelationshipForm(List<Relationship> relationships, DiagramElement source)
        {
            InitializeComponent();

            for (int i = 0; i < relationships.Count; i++)
            {
                Button button = new Button()
                {
                    Text = $"{source.Name} --> {relationships[i].Target.Name} ({relationships[i].Type})",
                    Width = flowLayoutPanel1.Width - 1,
                    Height = 35,
                    Margin = new Padding(0, 12, 0, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = i
                };

                button.Click += (sender, e) => {
                    index = Convert.ToInt32(button.Tag);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                };

                flowLayoutPanel1.Controls.Add(button);
            }
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
