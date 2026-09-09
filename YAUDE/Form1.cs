namespace YAUDE
{
    public partial class Form1 : Form
    {
        private List<DiagramClass> elements = new List<DiagramClass>();
        private string selectedTool = "cursor"; // Default tool is cursor
        private Point mouseDownLocationFromElement;
        private bool mouseDown = false;
        private DiagramClass selectedElement = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var element in elements)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(element.Position, new Size(100, 50)));
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(element.Position, new Size(100, 50)));
                SizeF stringSize = e.Graphics.MeasureString(element.Name, new Font("Arial", 10));
                e.Graphics.DrawString(element.Name, new Font("Arial", 10), Brushes.Black, element.Position.X + (50 - stringSize.Width / 2), element.Position.Y);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
            }
        }

        private void deselectTool()
        {
            foreach (object item in this.toolStrip_bottom.Items)
            {
                if (item is ToolStripButton button)
                {
                    button.Checked = false;
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;

            if (selectedTool == "cursor")
            {
                foreach (var element in elements)
                {
                    Rectangle elementRect = new Rectangle(element.Position, new Size(100, 50));
                    if (elementRect.Contains(e.Location))
                    {
                        selectedElement = element;
                        mouseDownLocationFromElement = new Point(e.X - element.Position.X, e.Y - element.Position.Y);
                    }
                }
            }

            else if (selectedTool == "addElement")
            {
                DiagramClass newClass = new DiagramClass
                {
                    Name = "New Class",
                    Position = e.Location
                };
                elements.Add(newClass);
                pictureBox1.Invalidate(); // Refresh the PictureBox to show the new class
                deselectTool();
                toolStripButton_cursor.Checked = true; // Switch back to cursor tool after adding an element
                selectedTool = "cursor";
            }

            if (selectedTool == "deleteElement")
            {
                for (int i = elements.Count - 1; i >= 0; i--)
                {
                    Rectangle elementRect = new Rectangle(elements[i].Position, new Size(100, 50));
                    if (elementRect.Contains(e.Location))
                    {
                        elements.RemoveAt(i);
                        pictureBox1.Invalidate(); // Refresh the PictureBox to show the removed class
                        break; // Exit the loop after deleting one element
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip contextMenu = new ContextMenuStrip();

                ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
                contextMenu.Items.Add(deleteItem);

                deleteItem.Click += (s, args) =>
                {
                    for (int i = elements.Count - 1; i >= 0; i--)
                    {
                        Rectangle elementRect = new Rectangle(elements[i].Position, new Size(100, 50));
                        if (elementRect.Contains(e.Location))
                        {
                            elements.RemoveAt(i);
                            pictureBox1.Invalidate(); // Refresh the PictureBox to show the removed class
                            break; // Exit the loop after deleting one element
                        }
                    }
                };

                contextMenu.Show(pictureBox1, e.Location);
            }
        }

        private void toolStripButton_Click(object sender, EventArgs e)
        {
            deselectTool();
            ((ToolStripButton)sender).Checked = true;
            selectedTool = ((ToolStripButton)sender).Tag.ToString();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            selectedElement = null;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && selectedTool == "cursor" && selectedElement != null)
            {
                selectedElement.Position = new Point(e.X - mouseDownLocationFromElement.X, e.Y - mouseDownLocationFromElement.Y);
                pictureBox1.Invalidate(); // Refresh the PictureBox to show the moved class
            }
        }
    }
}
