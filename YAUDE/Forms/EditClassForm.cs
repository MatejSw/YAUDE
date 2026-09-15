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

        public EditClassForm(DiagramClass element)
        {
            InitializeComponent();
            diagramClass = element;

            attributeBindingList = new BindingList<YAUDE.Model.Attribute>(diagramClass.Attributes);
            methodBindingList = new BindingList<YAUDE.Model.Method>(diagramClass.Methods);

            dataGridView_attributes.DataSource = attributeBindingList;
            dataGridView_methods.DataSource = methodBindingList;
            textBox_className.Text = diagramClass.Name;

            button_deleteAttribute.Enabled = attributeBindingList.Count > 0;
            button_deleteMethod.Enabled = methodBindingList.Count > 0;

            this.Text = "Edit Class: " + diagramClass.Name;
        }

        private void button_addAttribute_Click(object sender, EventArgs e)
        {
            attributeBindingList.Add(new YAUDE.Model.Attribute { Name = "NewAttribute", Type = "string", Visibility = "public" });
            dataGridView_attributes.Focus();
            button_deleteAttribute.Enabled = attributeBindingList.Count > 0;
        }

        private void button_addMethod_Click(object sender, EventArgs e)
        {
            methodBindingList.Add(new YAUDE.Model.Method { Name = "NewMethod", ReturnType = "void", Visibility = "public" });
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
                if (!visibility.Contains(attribute.Visibility))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(dataGridView_attributes, $"Invalid visibility for attribute (Row {i + 1}).");
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
                if (!visibility.Contains(method.Visibility))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(dataGridView_methods, $"Invalid visibility for method (Row {i + 1}).");
                    return;
                }
            }

            e.Cancel = false;
            errorProvider1.SetError(dataGridView_methods, string.Empty);
        }
    }
}
