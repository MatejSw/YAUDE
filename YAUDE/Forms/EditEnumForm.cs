using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Markup;
using YAUDE.Model;

namespace YAUDE.Forms
{
    public partial class EditEnumForm : Form
    {
        public DiagramEnum diagramEnum;
        private List<string> enumNames;

        public EditEnumForm(DiagramEnum diagram, List<string> names)
        {
            InitializeComponent();

            textBox_name.Text = diagram.Name;
            enumNames = names;
            diagramEnum = diagram;

            foreach (string item in diagram.Values)
            {
                richTextBox_values.Text += item;
                richTextBox_values.Text += "\n";
            }
            richTextBox_values.Text.Trim('\n');
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                diagramEnum.Name = textBox_name.Text;
                diagramEnum.Values = richTextBox_values.Text.Split('\n').ToList();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox_name_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_name.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox_name, "Enum name cannot be empty.");
            }
            else if (enumNames.Contains(textBox_name.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox_name, "An existing element already has this name.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox_name, string.Empty);
            }
        }
    }
}
