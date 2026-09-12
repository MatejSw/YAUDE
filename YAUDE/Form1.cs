using Newtonsoft.Json;

namespace YAUDE
{
    public partial class Form1 : Form
    {
        private List<DiagramClass> elements = new List<DiagramClass>();
        private string selectedTool = "cursor"; // Default tool is cursor
        private string filePath = null;
        private Point mouseDownLocationFromElement;
        private Point mouseStartPos;
        private Point panStart;
        private Point pan;
        private int zoomLevel = 100; // Default zoom level is 100%
        private bool mouseDown = false;
        private DiagramClass selectedElement = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(pan.X, pan.Y);
            e.Graphics.ScaleTransform(zoomLevel / 100f, zoomLevel / 100f);

            foreach (DiagramClass element in elements)
            {

                SizeF stringSize = e.Graphics.MeasureString(element.Name, new Font("Arial Black", 10));
                int sizeX = (int)stringSize.Width;
                int sizeY = 60 + (element.Attributes.Count + element.Methods.Count) * 20;

                for (int i = 0; i < element.Attributes.Count; i++)
                {
                    SizeF attrSize = e.Graphics.MeasureString($"{element.Attributes[i].Name}: {element.Attributes[i].Type}", new Font("Arial", 10));
                    sizeX = Math.Max(sizeX, (int)attrSize.Width);
                }
                for (int i = 0; i < element.Methods.Count; i++)
                {
                    SizeF methodSize = new SizeF(0, 0);

                    if (element.Methods[i].ReturnType == "void")
                    {
                        methodSize = e.Graphics.MeasureString($"{element.Methods[i].Name}()", new Font("Arial", 10));
                    }
                    else
                    {
                        methodSize = e.Graphics.MeasureString($"{element.Methods[i].Name}(): {element.Methods[i].ReturnType}", new Font("Arial", 10));
                    }

                    sizeX = Math.Max(sizeX, (int)methodSize.Width);
                }

                e.Graphics.FillRectangle(Brushes.LightBlue, new Rectangle(element.Position, new Size(sizeX + 10, sizeY)));

                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(element.Position, new Size(sizeX + 10, sizeY)));

                e.Graphics.DrawLine(Pens.Black, element.Position.X, element.Position.Y + 20, element.Position.X + sizeX + 10, element.Position.Y + 20);
                e.Graphics.DrawLine(Pens.Black, element.Position.X, element.Position.Y + 40 + element.Attributes.Count * 20, element.Position.X + sizeX + 10, element.Position.Y + 40 + element.Attributes.Count * 20);

                e.Graphics.DrawString(element.Name, new Font("Arial Black", 10), Brushes.Black, element.Position.X + 5, element.Position.Y);

                for (int i = 0; i < element.Attributes.Count; i++)
                {
                    e.Graphics.DrawString($"{element.Attributes[i].Name}: {element.Attributes[i].Type}", new Font("Arial", 10), Brushes.Black, element.Position.X + 5, element.Position.Y + 20 + i * 20);
                }
                for (int i = 0; i < element.Methods.Count; i++)
                {
                    if (element.Methods[i].ReturnType == "void")
                    {
                        e.Graphics.DrawString($"{element.Methods[i].Name}()", new Font("Arial", 10), Brushes.Black, element.Position.X + 5, element.Position.Y + 40 + element.Attributes.Count * 20 + i * 20);
                    }
                    else
                    {
                        e.Graphics.DrawString($"{element.Methods[i].Name}(): {element.Methods[i].ReturnType}", new Font("Arial", 10), Brushes.Black, element.Position.X + 5, element.Position.Y + 40 + element.Attributes.Count * 20 + i * 20);
                    }
                }

                if (element == selectedElement)
                {
                    e.Graphics.DrawRectangle(Pens.Red, new Rectangle(element.Position, new Size(sizeX + 10, sizeY)));
                }

                element.Size = new Size(sizeX + 10, sizeY);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    string json = reader.ReadToEnd();
                    elements = JsonConvert.DeserializeObject<List<DiagramClass>>(json);
                }
                toolStripLabel_status.Text = $"Loaded diagram: {openFileDialog.SafeFileName}";
                filePath = openFileDialog.FileName;
                deselectTool();
                toolStripButton_cursor.Checked = true;
                selectedTool = "cursor";
                pictureBox1.Invalidate();
                this.Text = $"YAUDE - {Path.GetFileName(filePath)}";
                pan = new Point(0, 0);
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
            selectedElement = null;

            for (int i = elements.Count - 1; i >= 0; i--)
            {
                Rectangle elementRect = new Rectangle(elements[i].Position, elements[i].Size);
                if (elementRect.Contains(new Point((int)(e.X / ((double)zoomLevel / 100) - pan.X / ((double)zoomLevel / 100)), (int)(e.Y / ((double)zoomLevel / 100) - pan.Y / ((double)zoomLevel / 100)))))
                {
                    selectedElement = elements[i];
                    break;
                }
            }

            if (selectedTool == "cursor" && selectedElement != null)
            {
                mouseDownLocationFromElement = new Point((int)(e.X / ((double)zoomLevel / 100) - selectedElement.Position.X), (int)(e.Y / ((double)zoomLevel / 100) - selectedElement.Position.Y));
            }

            else if (selectedTool == "addElement")
            {
                addElement(e.Location);
            }

            if (selectedTool == "deleteElement" && selectedElement != null)
            {
                elements.Remove(selectedElement);
            }

            if (selectedTool == "pan" || e.Button == MouseButtons.Middle)
            {
                mouseStartPos = e.Location;
                panStart = pan;
            }

            if (e.Button == MouseButtons.Right)
            {
                RightClick(e.Location);
            }

            pictureBox1.Invalidate();
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
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && selectedTool == "cursor" && selectedElement != null)
            {
                selectedElement.Position = new Point((int)(e.X / ((double)zoomLevel / 100) - mouseDownLocationFromElement.X), (int)(e.Y / ((double)zoomLevel / 100) - mouseDownLocationFromElement.Y));
                pictureBox1.Invalidate(); // Refresh the PictureBox to show the moved class
            }
            if (mouseDown && (selectedTool == "pan" || e.Button == MouseButtons.Middle))
            {
                Pan(e.Location);
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (filePath == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                }
                else
                {
                    return; // User cancelled the save operation
                }
            }
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string json = JsonConvert.SerializeObject(elements, Formatting.Indented);
                writer.Write(json);
            }
            toolStripLabel_status.Text = $"Diagram saved as: {Path.GetFileName(filePath)}";
            this.Text = $"YAUDE - {Path.GetFileName(filePath)}";
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    string json = JsonConvert.SerializeObject(elements, Formatting.Indented);
                    writer.Write(json);
                }
                toolStripLabel_status.Text = $"Diagram saved as: {Path.GetFileName(saveFileDialog.FileName)}";
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filePath = null;
            elements.Clear();
            deselectTool();
            toolStripButton_cursor.Checked = true;
            selectedTool = "cursor";
            toolStripLabel_status.Text = "";
            pictureBox1.Invalidate();
            pan = new Point(0, 0);
            zoomLevel = 100;
            toolStripLabel_zoom.Text = $"{zoomLevel}%";
            this.Text = "YAUDE";
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (selectedElement != null)
            {
                EditClassForm editForm = new EditClassForm(selectedElement);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    selectedElement = editForm.diagramClass;
                    pictureBox1.Invalidate();
                }
            }
        }

        private void addElement(Point location)
        {
            DiagramClass newClass = new DiagramClass
            {
                Name = "New Class",
                Position = location
            };
            elements.Add(newClass);
            deselectTool();
            toolStripButton_cursor.Checked = true;
            selectedTool = "cursor";
            selectedElement = newClass;
            pictureBox1.Invalidate();
        }

        private void RightClick(Point location)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            if (selectedElement != null)
            {
                ToolStripMenuItem editItem = new ToolStripMenuItem("Edit Class");
                contextMenu.Items.Add(editItem);

                editItem.Click += (s, args) =>
                {
                    EditClassForm editForm = new EditClassForm(selectedElement);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        selectedElement = editForm.diagramClass;
                        pictureBox1.Invalidate();
                    }
                };

                ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
                contextMenu.Items.Add(deleteItem);

                deleteItem.Click += (s, args) =>
                {
                    elements.Remove(selectedElement);
                    pictureBox1.Invalidate();
                };
            }
            else
            {
                ToolStripMenuItem addItem = new ToolStripMenuItem("Add new Class");
                contextMenu.Items.Add(addItem);
                addItem.Click += (s, args) =>
                {
                    addElement(location);
                };
            }

            contextMenu.Show(pictureBox1, location);
        }

        private void Pan(Point point)
        {
            pan = new Point(panStart.X + (point.X - mouseStartPos.X), panStart.Y + (point.Y - mouseStartPos.Y));
            pictureBox1.Invalidate();
        }

        private void toolStripButton_zoomIn_Click(object sender, EventArgs e)
        {
            zoomLevel = Math.Min(zoomLevel + 10, 200); // Limit zoom level between 10% and 200%
            toolStripLabel_zoom.Text = $"{zoomLevel}%";
            pictureBox1.Invalidate();
        }

        private void toolStripButton_zoomOut_Click(object sender, EventArgs e)
        {
            zoomLevel = Math.Max(zoomLevel - 10, 10); // Limit zoom level between 10% and 200%
            toolStripLabel_zoom.Text = $"{zoomLevel}%";
            pictureBox1.Invalidate();
        }
    }
}
