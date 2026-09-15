using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YAUDE
{
    public partial class ProgramVersionForm : Form
    {
        public ProgramVersionForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Image image = Properties.Resources.logo;
            e.Graphics.DrawImage(image, pictureBox1.ClientRectangle);
        }
    }
}
