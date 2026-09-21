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
    public partial class EditNoteForm : Form
    {
        public DiagramNote diagramNote;
        public EditNoteForm(DiagramNote note)
        {
            InitializeComponent();

            diagramNote = note;
            richTextBox1.Text = note.Text;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            diagramNote.Text = richTextBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
