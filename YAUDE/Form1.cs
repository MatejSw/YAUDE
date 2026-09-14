using Newtonsoft.Json;
using System.Drawing.Drawing2D;

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
        private DiagramClass targetElement = null;
        private Dictionary<string, string> visibilitySymbols = new Dictionary<string, string>
        {
            { "public", "+" },
            { "private", "-" },
            { "protected", "#" },
            { "internal", "~" }
        };
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
                int sizeX, sizeY;

                CalculateSize(element, e.Graphics, out sizeX, out sizeY);

                for (int i = 0; i < element.Associations.Count; i++)
                {
                    if (!elements.Select(x => x.Name).Contains(element.Associations[i]))
                    {
                        element.Associations.RemoveAt(i);
                        i--;
                        continue;
                    }
                    DiagramClass associatedClass = elements.First(x => x.Name == element.Associations[i]);
                    e.Graphics.DrawLine(Pens.Black, element.Position.X + sizeX / 2, element.Position.Y + sizeY / 2, associatedClass.Position.X + associatedClass.Size.Width / 2, associatedClass.Position.Y + associatedClass.Size.Height / 2);
                }
                for (int i = 0; i < element.Dependencies.Count; i++)
                {
                    if (!elements.Select(x => x.Name).Contains(element.Dependencies[i]))
                    {
                        element.Dependencies.RemoveAt(i);
                        i--;
                        continue;
                    }
                    DiagramClass associatedClass = elements.First(x => x.Name == element.Dependencies[i]);
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(10 * zoomLevel / 100f, 10 * zoomLevel / 100f);
                    Pen pen = new Pen(Color.Black, 1);
                    pen.CustomEndCap = bigArrow;
                    e.Graphics.DrawLine(pen, element.Position.X + sizeX / 2, element.Position.Y + sizeY / 2, (int)((associatedClass.Position.X + associatedClass.Size.Width / 2) + (associatedClass.Size.Width / 2 * Math.Min(1, Math.Max(-1, (element.Position.X - associatedClass.Position.X) * 0.015)))), (int)((associatedClass.Position.Y + associatedClass.Size.Height / 2) + (associatedClass.Size.Height / 2 * Math.Min(1, Math.Max(-1, (element.Position.Y - associatedClass.Position.Y) * 0.015)))));
                }
            }

            foreach (DiagramClass element in elements)
            {
                int sizeX, sizeY;
                CalculateSize(element, e.Graphics, out sizeX, out sizeY);

                e.Graphics.FillRectangle(new SolidBrush(element.color), new Rectangle(element.Position, new Size(sizeX + 10, sizeY)));

                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(element.Position, new Size(sizeX + 10, sizeY)));

                e.Graphics.DrawLine(Pens.Black, element.Position.X, element.Position.Y + 20, element.Position.X + sizeX + 10, element.Position.Y + 20);
                e.Graphics.DrawLine(Pens.Black, element.Position.X, element.Position.Y + 40 + element.Attributes.Count * 20, element.Position.X + sizeX + 10, element.Position.Y + 40 + element.Attributes.Count * 20);

                e.Graphics.DrawString(element.Name, new Font("Arial Black", 10), Brushes.Black, element.Position.X + 5, element.Position.Y);

                for (int i = 0; i < element.Attributes.Count; i++)
                {
                    e.Graphics.DrawString($"{visibilitySymbols[element.Attributes[i].Visibility]}{element.Attributes[i].Name}: {element.Attributes[i].Type}", new Font("Arial", 10), Brushes.Black, element.Position.X + 5, element.Position.Y + 20 + i * 20);
                }
                for (int i = 0; i < element.Methods.Count; i++)
                {
                    if (element.Methods[i].ReturnType == "void")
                    {
                        e.Graphics.DrawString($"{visibilitySymbols[element.Methods[i].Visibility]}{element.Methods[i].Name}({element.Methods[i].Parameters})", new Font("Arial", 10), Brushes.Black, element.Position.X + 5, element.Position.Y + 40 + element.Attributes.Count * 20 + i * 20);
                    }
                    else
                    {
                        e.Graphics.DrawString($"{visibilitySymbols[element.Methods[i].Visibility]}{element.Methods[i].Name}({element.Methods[i].Parameters}): {element.Methods[i].ReturnType}", new Font("Arial", 10), Brushes.Black, element.Position.X + 5, element.Position.Y + 40 + element.Attributes.Count * 20 + i * 20);
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
            Open();
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
            Point location = new Point((int)(e.X / ((double)zoomLevel / 100) - pan.X / ((double)zoomLevel / 100)), (int)(e.Y / ((double)zoomLevel / 100) - pan.Y / ((double)zoomLevel / 100)));

            if (e.Button != MouseButtons.Middle)
            {
                selectedElement = null;

                for (int i = elements.Count - 1; i >= 0; i--)
                {
                    Rectangle elementRect = new Rectangle(elements[i].Position, elements[i].Size);
                    if (elementRect.Contains(location))
                    {
                        selectedElement = elements[i];
                        break;
                    }
                }
            }

            if (selectedTool == "cursor" && selectedElement != null && e.Button == MouseButtons.Left)
            {
                mouseDownLocationFromElement = new Point((int)(e.X / ((double)zoomLevel / 100) - selectedElement.Position.X), (int)(e.Y / ((double)zoomLevel / 100) - selectedElement.Position.Y));
            }

            else if (selectedTool == "addElement" && e.Button == MouseButtons.Left)
            {
                addElement(location);
            }

            if (selectedTool == "deleteElement" && selectedElement != null && e.Button == MouseButtons.Left)
            {
                elements.Remove(selectedElement);
            }

            if (selectedTool == "pan" || e.Button == MouseButtons.Middle)
            {
                mouseStartPos = e.Location;
                panStart = pan;
            }

            if (selectedTool == "addAssociation" && e.Button == MouseButtons.Left)
            {
                if (selectedElement == null)
                {
                    targetElement = null;
                }
                else if (targetElement == null)
                {
                    targetElement = selectedElement;
                }
                if (targetElement != null && selectedElement != null && targetElement != selectedElement)
                {
                    targetElement.Associations.Add(selectedElement.Name);
                    targetElement = null;
                    selectedElement = null;
                }
            }

            if (selectedTool == "addDependency" && e.Button == MouseButtons.Left)
            {
                if (selectedElement == null)
                {
                    targetElement = null;
                }
                else if (targetElement == null)
                {
                    targetElement = selectedElement;
                }
                if (targetElement != null && selectedElement != null && targetElement != selectedElement)
                {
                    targetElement.Dependencies.Add(selectedElement.Name);
                    targetElement = null;
                    selectedElement = null;
                }
            }

            if (selectedTool == "removeRelationships" && selectedElement != null && e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    if (elements[i].Associations.Contains(selectedElement.Name))
                    {
                        elements[i].Associations.Remove(selectedElement.Name);
                    }
                }

                selectedElement.Associations.Clear();
                selectedElement.Dependencies.Clear();

                selectedElement = null;
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
            if (mouseDown && selectedTool == "cursor" && selectedElement != null && e.Button == MouseButtons.Left)
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
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (selectedElement != null)
            {
                EditClassForm editForm = new EditClassForm(selectedElement);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    selectedElement = editForm.diagramClass;

                    AutoAssignAssociations(selectedElement);

                    pictureBox1.Invalidate();
                }
            }
        }

        private void addElement(Point location)
        {
            DiagramClass newClass = new DiagramClass
            {
                Name = "NewClass",
                Position = location,
                color = Color.LightBlue,
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

                        AutoAssignAssociations(selectedElement);

                        pictureBox1.Invalidate();
                    }
                };

                ToolStripMenuItem editColor = new ToolStripMenuItem("Edit Color");
                contextMenu.Items.Add(editColor);

                editColor.Click += (s, args) =>
                {
                    ColorDialog colorDialog = new ColorDialog();
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedElement.color = colorDialog.Color;
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
                    addElement(new Point((int)(location.X / ((double)zoomLevel / 100) - pan.X / ((double)zoomLevel / 100)), (int)(location.Y / ((double)zoomLevel / 100) - pan.Y / ((double)zoomLevel / 100))));
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

        private void CalculateSize(DiagramClass element, Graphics g, out int sizeX, out int sizeY)
        {
            SizeF stringSize = g.MeasureString(element.Name, new Font("Arial Black", 10));
            sizeX = (int)stringSize.Width;
            sizeY = 60 + (element.Attributes.Count + element.Methods.Count) * 20;
            for (int i = 0; i < element.Attributes.Count; i++)
            {
                SizeF attrSize = g.MeasureString($"{visibilitySymbols[element.Attributes[i].Visibility]}{element.Attributes[i].Name}: {element.Attributes[i].Type}", new Font("Arial", 10));
                sizeX = Math.Max(sizeX, (int)attrSize.Width);
            }
            for (int i = 0; i < element.Methods.Count; i++)
            {
                SizeF methodSize = new SizeF(0, 0);
                if (element.Methods[i].ReturnType == "void")
                {
                    methodSize = g.MeasureString($"{visibilitySymbols[element.Methods[i].Visibility]}{element.Methods[i].Name}({element.Methods[i].Parameters})", new Font("Arial", 10));
                }
                else
                {
                    methodSize = g.MeasureString($"{visibilitySymbols[element.Methods[i].Visibility]}{element.Methods[i].Name}({element.Methods[i].Parameters}): {element.Methods[i].ReturnType}", new Font("Arial", 10));
                }
                sizeX = Math.Max(sizeX, (int)methodSize.Width);
            }
        }

        private void AutoAssignAssociations(DiagramClass element)
        {
            bool autoAssign = false;

            for (int i = 0; i < element.Attributes.Count; i++)
            {
                string attributeType = element.Attributes[i].Type.Replace("List<", "").Replace(">", "");
                if (elements.Select(x => x.Name).Contains(attributeType) && !element.Associations.Contains(elements.Select(x => x.Name).First(x => x == attributeType)))
                {
                    if (!autoAssign)
                    {
                        if (MessageBox.Show("Class refrences an existing class. \n\n Would do like to auto-assing associations?", "Edit Class", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            autoAssign = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (autoAssign)
                    {
                        element.Associations.Add(elements.Select(x => x.Name).First(x => x == attributeType));
                    }
                }
            }

            for (int i = 0; i < element.Methods.Count; i++)
            {
                string methodType = element.Methods[i].ReturnType.Replace("List<", "").Replace(">", "");
                if (elements.Select(x => x.Name).Contains(methodType) && !element.Associations.Contains(elements.Select(x => x.Name).First(x => x == methodType)))
                {
                    if (!autoAssign)
                    {
                        if (MessageBox.Show("Class refrences an existing class. \n\n Would do like to auto-assing associations?", "Edit Class", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            autoAssign = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (autoAssign)
                    {
                        element.Associations.Add(elements.Select(x => x.Name).First(x => x == methodType));
                    }
                }
            }
        }

        private void Save()
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

        private void SaveAs()
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
                filePath = saveFileDialog.FileName;
                this.Text = $"YAUDE - {Path.GetFileName(filePath)}";
            }
        }

        private void Open()
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

        private void New()
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Dictionary<string, Keys> toolShortcuts = new Dictionary<string, Keys>
            {
                { "cursor", Keys.C },
                { "addElement", Keys.A },
                { "deleteElement", Keys.D },
                { "pan", Keys.P },
                { "addAssociation", Keys.S },
                { "addDependency", Keys.E },
                { "removeRelationships", Keys.R }
            };

            Dictionary<Keys, Action> shortcutActions = new Dictionary<Keys, Action>
            {
                { Keys.Delete, () => { if (selectedElement != null) { elements.Remove(selectedElement); selectedElement = null; pictureBox1.Invalidate(); } } },
                { (Keys.Control | Keys.S), () => Save() },
                { (Keys.Control | Keys.Shift | Keys.S), () => SaveAs() },
                { (Keys.Control | Keys.N), () => New() },
                { (Keys.Control | Keys.O), () => Open() }
            };

            if (toolShortcuts.ContainsValue(keyData))
            {
                string toolName = toolShortcuts.FirstOrDefault(x => x.Value == keyData).Key;
                deselectTool();
                foreach (object item in this.toolStrip_bottom.Items)
                {
                    if (item is ToolStripButton button && button.Tag.ToString() == toolName)
                    {
                        button.Checked = true;
                        selectedTool = toolName;
                        break;
                    }
                }
            }

            if (shortcutActions.ContainsKey(keyData))
            {
                shortcutActions[keyData].Invoke();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void programVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramVersionForm versionForm = new ProgramVersionForm();
            versionForm.ShowDialog();
        }
    }
}
