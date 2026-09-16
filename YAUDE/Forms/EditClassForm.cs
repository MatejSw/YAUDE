using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YAUDE.Model;

namespace YAUDE
{
    public partial class EditClassForm : Form
    {
        public DiagramClass diagramClass;
        private BindingList<YAUDE.Model.Attribute> attributeBindingList;
        private BindingList<YAUDE.Model.Method> methodBindingList;
        private List<string> classNames;

        public EditClassForm(DiagramClass element, List<string> names)
        {
            InitializeComponent();
            diagramClass = element;

            attributeBindingList = new BindingList<YAUDE.Model.Attribute>(diagramClass.Attributes);
            methodBindingList = new BindingList<YAUDE.Model.Method>(diagramClass.Methods);

            dataGridView_attributes.DataSource = attributeBindingList;
            dataGridView_methods.DataSource = methodBindingList;
            textBox_className.Text = diagramClass.Name;

            DataGridViewComboBoxColumn visibilityColumn = new DataGridViewComboBoxColumn();
            visibilityColumn.HeaderText = "Visibility";
            visibilityColumn.Items.AddRange(["Public", "Private", "Protected", "Internal"]);

            dataGridView_attributes.Columns[2].Visible = false;
            dataGridView_attributes.Columns.Insert(2, visibilityColumn);

            DataGridViewComboBoxColumn visibilityColumn2 = new DataGridViewComboBoxColumn();
            visibilityColumn2.HeaderText = "Visibility";
            visibilityColumn2.Items.AddRange(["Public", "Private", "Protected", "Internal"]);

            dataGridView_methods.Columns[3].Visible = false;
            dataGridView_methods.Columns.Insert(3, visibilityColumn2);

            button_deleteAttribute.Enabled = attributeBindingList.Count > 0;
            button_deleteMethod.Enabled = methodBindingList.Count > 0;

            classNames = names;

            this.Text = "Edit Class: " + diagramClass.Name;
        }

        private void button_addAttribute_Click(object sender, EventArgs e)
        {
            attributeBindingList.Add(new YAUDE.Model.Attribute { Name = $"NewAttribute{(attributeBindingList.Count == 0 ? "" : attributeBindingList.Count + 1)}", Type = "string", Visibility = Visibility.Public });
            dataGridView_attributes.Focus();
            button_deleteAttribute.Enabled = attributeBindingList.Count > 0;
        }

        private void button_addMethod_Click(object sender, EventArgs e)
        {
            methodBindingList.Add(new YAUDE.Model.Method { Name = $"NewMethod{(methodBindingList.Count == 0 ? "" : methodBindingList.Count + 1)}", ReturnType = "void", Visibility = Visibility.Public });
            dataGridView_methods.Focus();
            button_deleteMethod.Enabled = methodBindingList.Count > 0;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                this.DialogResult = DialogResult.OK;
                Dictionary<string, Visibility> visibility = new()
                {
                    {"Public", Visibility.Public  },
                    {"Private", Visibility.Private  },
                    {"Protected", Visibility.Protected  },
                    {"Internal", Visibility.Internal  }
                };

                for (int i = 0; i < attributeBindingList.Count; i++)
                {
                    attributeBindingList[i].Visibility = visibility[dataGridView_attributes.Rows[i].Cells[2].Value.ToString()];
                }
                for (int i = 0; i < methodBindingList.Count; i++)
                {
                    methodBindingList[i].Visibility = visibility[dataGridView_methods.Rows[i].Cells[3].Value.ToString()];
                }

                diagramClass.Name = textBox_className.Text;
                diagramClass.Attributes = new List<YAUDE.Model.Attribute>(attributeBindingList);
                diagramClass.Methods = new List<Method>(methodBindingList);
                this.Close();
            }
        }

        private void textBox_className_TextChanged(object sender, EventArgs e)
        {
            this.Text = "Edit Class: " + textBox_className.Text;
        }

        private void textBox_className_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_className.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox_className, "Class name cannot be empty.");
            }
            else if (classNames.Contains(textBox_className.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textBox_className, "An existing class already has this name.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox_className, string.Empty);
            }
        }

        private void button_deleteAttribute_Click(object sender, EventArgs e)
        {
            if (dataGridView_attributes.CurrentRow != null)
                attributeBindingList.RemoveAt(dataGridView_attributes.CurrentRow.Index);
            button_deleteAttribute.Enabled = attributeBindingList.Count > 0;
        }

        private void button_deleteMethod_Click(object sender, EventArgs e)
        {
            if (dataGridView_methods.CurrentRow != null)
                methodBindingList.RemoveAt(dataGridView_methods.CurrentRow.Index);
            button_deleteMethod.Enabled = methodBindingList.Count > 0;
        }

        private void dataGridView_attributes_Validating(object sender, CancelEventArgs e)
        {
            List<string> visibility = new List<string> { "public", "private", "protected", "internal" };
            for (int i = 0; i < attributeBindingList.Count; i++)
            {
                YAUDE.Model.Attribute attribute = attributeBindingList[i];
                if (attributeBindingList.Where(x => attributeBindingList.IndexOf(x) > i).Select(x => x.Name).Contains(attribute.Name))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(dataGridView_attributes, $"Attribute names can't repeat (Row {i + 1}).");
                    return;
                }
            }

            e.Cancel = false;
            errorProvider1.SetError(dataGridView_attributes, string.Empty);
        }

        private void dataGridView_methods_Validating(object sender, CancelEventArgs e)
        {
            List<string> visibility = new List<string> { "public", "private", "protected", "internal" };
            for (int i = 0; i < methodBindingList.Count; i++)
            {
                Method method = methodBindingList[i];
                if (methodBindingList.Where(x => methodBindingList.IndexOf(x) > i).Select(x => x.Name).Contains(method.Name))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(dataGridView_methods, $"Method names can't repeat (Row {i + 1}).");
                    return;
                }
            }

            e.Cancel = false;
            errorProvider1.SetError(dataGridView_methods, string.Empty);
        }
    }
}
