using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using YAUDE.Forms;
using YAUDE.Model;
using YAUDE.Services;

namespace YAUDE
{
    public partial class Form1 : Form
    {
        public List<DiagramElement> elements = new List<DiagramElement>();
        private string selectedTool = "cursor"; // Default tool is cursor
        public string filePath = null;
        private Point mouseDownLocationFromElement;
        private Point mouseStartPos;
        private Point panStart;
        private Point pan;
        private int zoomLevel = 100; // Default zoom level is 100%
        private bool mouseDown = false;
        private bool justSaved = true;
        private DiagramElement selectedElement = null;
        private DiagramElement targetElement = null;
        private Dictionary<Visibility, string> visibilitySymbols = new Dictionary<Visibility, string>
        {
            { Visibility.Public, "+" },
            { Visibility.Private, "-" },
            { Visibility.Protected, "#" },
            { Visibility.Internal, "~" }
        };
        private RelationshipType relationship = RelationshipType.Association;
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform((int)(pan.X * ((double)zoomLevel / 100f) + pictureBox1.Width / 2), (int)(pan.Y * ((double)zoomLevel / 100f) + pictureBox1.Height / 2));
            e.Graphics.ScaleTransform(zoomLevel / 100f, zoomLevel / 100f);


            foreach (DiagramElement element in elements)
            {
                element.DrawRelationships(e.Graphics, zoomLevel);
            }

            foreach (DiagramElement element in elements)
            {
                element.Draw(e.Graphics);

                if (element == selectedElement)
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        e.Graphics.DrawRectangle(pen, new Rectangle(element.Position, element.Size));
                    }
                }
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
            Point location = new Point((int)((e.X - pictureBox1.Width / 2) / ((double)zoomLevel / 100) - pan.X), (int)((e.Y - pictureBox1.Height / 2) / ((double)zoomLevel / 100) - pan.Y));

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
                mouseDownLocationFromElement = new Point((int)((e.X - pictureBox1.Width / 2) / ((double)zoomLevel / 100) - selectedElement.Position.X), (int)((e.Y - pictureBox1.Height / 2) / ((double)zoomLevel / 100) - selectedElement.Position.Y));
            }

            else if (selectedTool == "addClass" && e.Button == MouseButtons.Left)
            {
                addClass(location);
            }

            else if (selectedTool == "addEnum" && e.Button == MouseButtons.Left)
            {
                addEnum(location);
            }

            else if (selectedTool == "addNote" && e.Button == MouseButtons.Left)
            {
                addNote(location);
            }

            if (selectedTool == "deleteElement" && selectedElement != null && e.Button == MouseButtons.Left)
            {
                DeleteSelectedElement();
            }

            if (selectedTool == "pan" || e.Button == MouseButtons.Middle)
            {
                mouseStartPos = e.Location;
                panStart = pan;
            }

            if (selectedTool == "addRelationship" && e.Button == MouseButtons.Left)
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
                    targetElement.Relationships.Add(new Relationship { Target = selectedElement, Type = relationship });
                    targetElement = null;
                    selectedElement = null;
                    justSaved = false;
                }
            }

            if (selectedTool == "editRelationship" && e.Button == MouseButtons.Left)
            {
                List<Relationship> relationships = new();
                List<DiagramElement> sources = new();

                foreach (DiagramElement element in elements)
                {
                    for (int i = 0; i < element.Relationships.Count; i++)
                    {
                        DiagramElement relatedElement = element.Relationships[i].Target;

                        Point point = new(element.Position.X + element.Size.Width / 2 + 5, element.Position.Y + element.Size.Height / 2 + 5);
                        Size size = new(relatedElement.Position.X + relatedElement.Size.Width / 2 - point.X, relatedElement.Position.Y + relatedElement.Size.Height / 2 - point.Y);

                        if (size.Width < 0) point = new(point.X + size.Width, point.Y);
                        if (size.Height < 0) point = new(point.X, point.Y + size.Height);

                        size = new(Math.Abs(size.Width) + 5, Math.Abs(size.Height) + 5);

                        Rectangle rectRelationship = new(point, size);

                        if (rectRelationship.Contains(location))
                        {
                            sources.Add(element);
                            relationships.Add(element.Relationships[i]);
                        }
                    }
                }

                if (relationships.Count > 0)
                {
                    if (relationships.Count == 1)
                    {
                        EditRelationshipForm editForm = new(relationships[0]);

                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            sources[0].Relationships[sources[0].Relationships.IndexOf(relationships[0])] = editForm.relationship;

                            pictureBox1.Invalidate();
                            deselectTool();
                            toolStripButton_cursor.Checked = true;
                            selectedTool = "cursor";
                        }
                    }
                    else
                    {
                        SelectRelationshipForm selectForm = new(relationships, sources);

                        if (selectForm.ShowDialog() == DialogResult.OK)
                        {
                            int index = selectForm.index;

                            EditRelationshipForm editForm = new(relationships[index]);

                            if (editForm.ShowDialog() == DialogResult.OK)
                            {
                                sources[index].Relationships[sources[index].Relationships.IndexOf(relationships[index])] = editForm.relationship;

                                pictureBox1.Invalidate();
                                deselectTool();
                                toolStripButton_cursor.Checked = true;
                                selectedTool = "cursor";
                            }
                        }
                    }
                }
            }

            if (selectedTool == "removeRelationships" && selectedElement != null && e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    elements[i].Relationships.RemoveAll(r => r.Target.Name == selectedElement.Name);
                }

                selectedElement.Relationships.Clear();

                selectedElement = null;
                justSaved = false;
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
                selectedElement.Position = new Point((int)((e.X - pictureBox1.Width / 2) / ((double)zoomLevel / 100) - mouseDownLocationFromElement.X), (int)((e.Y - pictureBox1.Height / 2) / ((double)zoomLevel / 100) - mouseDownLocationFromElement.Y));
                pictureBox1.Invalidate(); // Refresh the PictureBox to show the moved class
                justSaved = false;
            }
            if (mouseDown && (selectedTool == "pan" || e.Button == MouseButtons.Middle))
            {
                Pan(e.Location);
            }

            //toolStripLabel_status.Text = $"Mouse Position: {((int)((e.X - pictureBox1.Width / 2) / ((double)zoomLevel / 100) - pan.X / ((double)zoomLevel / 100)))}, {((int)((e.Y - pictureBox1.Height / 2) / ((double)zoomLevel / 100) - pan.Y / ((double)zoomLevel / 100)))}";
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
            int index = elements.IndexOf(selectedElement);
            if (selectedElement != null && selectedElement is DiagramClass selectedClass)
            {
                EditClassForm editForm = new EditClassForm(selectedClass, elements.Where(x => x != selectedElement).Select(x => x.Name).ToList());
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    elements[index] = editForm.diagramClass;
                    selectedElement = elements[index];

                    AutoAssignAssociations(selectedClass);

                    pictureBox1.Invalidate();
                    justSaved = false;
                }
            }

            if (selectedElement != null && selectedElement is DiagramEnum selectedEnum)
            {
                EditEnumForm editForm = new EditEnumForm(selectedEnum, elements.Where(x => x != selectedElement).Select(x => x.Name).ToList());
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    elements[index] = editForm.diagramEnum;
                    selectedElement = elements[index];

                    pictureBox1.Invalidate();
                    justSaved = false;
                }
            }

            if (selectedElement != null && selectedElement is DiagramNote selectedNote)
            {
                EditNoteForm editForm = new EditNoteForm(selectedNote);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    elements[index] = editForm.diagramNote;
                    selectedElement = elements[index];

                    pictureBox1.Invalidate();
                    justSaved = false;
                }
            }
        }

        private void addClass(Point location)
        {
            DiagramClass newClass = new DiagramClass()
            {
                Name = $"NewClass{(elements.Where(x => x.Type == "Class").Count() == 0 ? "" : elements.Where(x => x.Type == "Class").Count() + 1)}",
                Position = location,
                color = Color.LightBlue,
            };
            elements.Add(newClass);
            deselectTool();
            toolStripButton_cursor.Checked = true;
            selectedTool = "cursor";
            selectedElement = newClass;
            pictureBox1.Invalidate();
            justSaved = false;
        }

        private void addEnum(Point location)
        {
            DiagramEnum newClass = new DiagramEnum()
            {
                Name = $"NewEnum{(elements.Where(x => x.Type == "Enum").Count() == 0 ? "" : elements.Where(x => x.Type == "Enum").Count() + 1)}",
                Position = location,
                color = Color.LightCoral,
            };
            elements.Add(newClass);
            deselectTool();
            toolStripButton_cursor.Checked = true;
            selectedTool = "cursor";
            selectedElement = newClass;
            pictureBox1.Invalidate();
            justSaved = false;
        }

        private void addNote(Point location)
        {
            DiagramNote newClass = new DiagramNote()
            {
                Name = $"Note{elements.Where(x => x.Type == "Note").Count() + 1}",
                Position = location,
                color = Color.LightGreen,
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
            };
            elements.Add(newClass);
            deselectTool();
            toolStripButton_cursor.Checked = true;
            selectedTool = "cursor";
            selectedElement = newClass;
            pictureBox1.Invalidate();
            justSaved = false;
        }

        private void RightClick(Point location)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            if (selectedElement != null)
            {
                int index = elements.IndexOf(selectedElement);
                if (selectedElement is DiagramClass selectedClass)
                {
                    ToolStripMenuItem editItem = new ToolStripMenuItem("Edit Class");
                    contextMenu.Items.Add(editItem);

                    editItem.Click += (s, args) =>
                    {
                        EditClassForm editForm = new EditClassForm(selectedClass, elements.Where(x => x != selectedElement).Select(x => x.Name).ToList());
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            elements[index] = editForm.diagramClass;
                            selectedElement = elements[index];

                            AutoAssignAssociations(selectedClass);

                            pictureBox1.Invalidate();
                            justSaved = false;
                        }
                    };
                }

                if (selectedElement is DiagramEnum selectedEnum)
                {
                    ToolStripMenuItem editItem = new ToolStripMenuItem("Edit Enum");
                    contextMenu.Items.Add(editItem);

                    editItem.Click += (s, args) =>
                    {
                        EditEnumForm editForm = new EditEnumForm(selectedEnum, elements.Where(x => x != selectedElement).Select(x => x.Name).ToList());
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            elements[index] = editForm.diagramEnum;
                            selectedElement = elements[index];

                            pictureBox1.Invalidate();
                            justSaved = false;
                        }
                    };
                }

                if (selectedElement is DiagramNote selectedNote)
                {
                    ToolStripMenuItem editItem = new ToolStripMenuItem("Edit Note");
                    contextMenu.Items.Add(editItem);

                    editItem.Click += (s, args) =>
                    {
                        EditNoteForm editForm = new EditNoteForm(selectedNote);
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            elements[index] = editForm.diagramNote;
                            selectedElement = elements[index];

                            pictureBox1.Invalidate();
                            justSaved = false;
                        }
                    };
                }

                ToolStripMenuItem editColor = new ToolStripMenuItem("Edit Color");
                contextMenu.Items.Add(editColor);

                editColor.Click += (s, args) =>
                {
                    ColorDialog colorDialog = new ColorDialog();
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedElement.color = colorDialog.Color;
                        pictureBox1.Invalidate();
                        justSaved = false;
                    }
                };

                if (selectedElement.Relationships.Count > 0)
                {
                    ToolStripMenuItem editRelationships = new ToolStripMenuItem("Edit Relationships");
                    contextMenu.Items.Add(editRelationships);

                    editRelationships.Click += (s, args) =>
                    {
                        SelectRelationshipForm selectForm = new(selectedElement.Relationships, selectedElement);

                        while (selectForm.ShowDialog() == DialogResult.OK)
                        {
                            int relIndex = selectForm.index;

                            EditRelationshipForm editForm = new(selectedElement.Relationships[relIndex]);

                            if (editForm.ShowDialog() == DialogResult.OK)
                            {
                                elements[index].Relationships[selectedElement.Relationships.IndexOf(selectedElement.Relationships[relIndex])] = editForm.relationship;

                                pictureBox1.Invalidate();
                            }
                        }
                    };
                }

                ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
                contextMenu.Items.Add(deleteItem);

                deleteItem.Click += (s, args) =>
                {
                    DeleteSelectedElement();
                };
            }
            else
            {
                ToolStripMenuItem addClassItem = new ToolStripMenuItem("Add new Class");
                contextMenu.Items.Add(addClassItem);
                addClassItem.Click += (s, args) =>
                {
                    addClass(new Point((int)((location.X - pictureBox1.Width / 2) / ((double)zoomLevel / 100) - pan.X / ((double)zoomLevel / 100)), (int)((location.Y - pictureBox1.Height / 2) / ((double)zoomLevel / 100) - pan.Y / ((double)zoomLevel / 100))));
                };
                ToolStripMenuItem addEnumItem = new ToolStripMenuItem("Add new Enum");
                contextMenu.Items.Add(addEnumItem);
                addEnumItem.Click += (s, args) =>
                {
                    addEnum(new Point((int)((location.X - pictureBox1.Width / 2) / ((double)zoomLevel / 100) - pan.X / ((double)zoomLevel / 100)), (int)((location.Y - pictureBox1.Height / 2) / ((double)zoomLevel / 100) - pan.Y / ((double)zoomLevel / 100))));
                };
                ToolStripMenuItem addNoteItem = new ToolStripMenuItem("Add new Note");
                contextMenu.Items.Add(addNoteItem);
                addNoteItem.Click += (s, args) =>
                {
                    addNote(new Point((int)((location.X - pictureBox1.Width / 2) / ((double)zoomLevel / 100) - pan.X / ((double)zoomLevel / 100)), (int)((location.Y - pictureBox1.Height / 2) / ((double)zoomLevel / 100) - pan.Y / ((double)zoomLevel / 100))));
                };
            }

            contextMenu.Show(pictureBox1, location);
        }

        private void DeleteSelectedElement()
        {
            if (selectedElement != null)
            {
                elements.Remove(selectedElement);
                for (int i = 0; i < elements.Count; i++)
                {
                    elements[i].Relationships.RemoveAll(r => r.Target.Name == selectedElement?.Name);
                }
                selectedElement = null;

                pictureBox1.Invalidate();
                justSaved = false;
            }
        }

        private void Pan(Point point)
        {
            pan = new Point((int)(panStart.X + (point.X - mouseStartPos.X) / ((double)zoomLevel / 100f)), (int)(panStart.Y + (point.Y - mouseStartPos.Y) / ((double)zoomLevel / 100f)));
            pictureBox1.Invalidate();
        }

        private void toolStripButton_zoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void toolStripButton_zoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void ZoomIn()
        {
            zoomLevel = Math.Min(zoomLevel + 10, 200); // Limit zoom level between 10% and 200%
            toolStripLabel_zoom.Text = $"{zoomLevel}%";
            pictureBox1.Invalidate();
        }

        private void ZoomOut()
        {
            zoomLevel = Math.Max(zoomLevel - 10, 10); // Limit zoom level between 10% and 200%
            toolStripLabel_zoom.Text = $"{zoomLevel}%";
            pictureBox1.Invalidate();
        }

        private void PictureBox1_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn();
            }
            if (e.Delta < 0)
            {
                ZoomOut();
            }
        }

        private void AutoAssignAssociations(DiagramClass element)
        {
            bool autoAssign = false;

            for (int i = 0; i < element.Attributes.Count; i++)
            {
                string attributeType = element.Attributes[i].Type.Replace("List<", "").Replace(">", "");
                if (elements.Select(x => x.Name).Contains(attributeType) && !element.Relationships.Any(r => r.Target.Name == attributeType))
                {
                    if (!autoAssign)
                    {
                        if (MessageBox.Show("Class refrences an existing element. \n\n Would do like to auto-assing associations?", "Edit Class", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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
                        element.Relationships.Add(new Relationship { Target = elements.First(x => x.Name == attributeType), Type = RelationshipType.Association });
                    }
                }
            }

            for (int i = 0; i < element.Methods.Count; i++)
            {
                string methodType = element.Methods[i].ReturnType.Replace("List<", "").Replace(">", "");
                if (elements.Select(x => x.Name).Contains(methodType) && !element.Relationships.Any(r => r.Target.Name == methodType))
                {
                    if (!autoAssign)
                    {
                        if (MessageBox.Show("Class refrences an existing element. \n\n Would do like to auto-assing associations?", "Edit Class", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
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
                        element.Relationships.Add(new Relationship { Target = elements.First(x => x.Name == methodType), Type = RelationshipType.Association });
                    }
                }
            }
        }

        private void Save()
        {
            if (FileSaver.Save(this))
            {
                toolStripLabel_status.Text = $"Diagram saved as: {Path.GetFileName(filePath)}";
                this.Text = $"YAUDE - {Path.GetFileName(filePath)}";
                justSaved = true;
            }
        }

        private void SaveAs()
        {
            if (FileSaver.SaveAs(this))
            {
                toolStripLabel_status.Text = $"Diagram saved as: {Path.GetFileName(filePath)}";
                this.Text = $"YAUDE - {Path.GetFileName(filePath)}";
                justSaved = true;
            }
        }

        private void Open()
        {
            if (!SaveChanges()) return;

            elements = FileSaver.Open(this);
            if (elements.Count != 0)
            {
                toolStripLabel_status.Text = $"Loaded diagram: {Path.GetFileName(filePath)}";
                deselectTool();
                toolStripButton_cursor.Checked = true;
                selectedTool = "cursor";

                for (int i = 0; i < elements.Count; i++)
                {
                    List<Relationship> temp = elements[i].Relationships;

                    elements[i].Relationships = new List<Relationship>();
                    for (int j = 0; j < temp.Count; j++)
                    {
                        DiagramElement targetElement = elements.FirstOrDefault(x => x.Name == temp[j].Target.Name);
                        if (targetElement != null)
                        {
                            elements[i].Relationships.Add(new Relationship { Target = targetElement, Type = temp[j].Type });
                        }
                    }
                }

                pictureBox1.Invalidate();
                this.Text = $"YAUDE - {Path.GetFileName(filePath)}";
                pan = new Point(0, 0);
                justSaved = true;
            }
        }

        private void New()
        {
            if (!SaveChanges()) return;
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
            justSaved = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Dictionary<string, Keys> toolShortcuts = new Dictionary<string, Keys>
            {
                { "cursor", Keys.C },
                { "addClass", Keys.A },
                { "addEnum", Keys.E },
                { "addNote", Keys.N },
                { "deleteElement", Keys.D },
                { "pan", Keys.P },
                { "editRelationship", Keys.G },
                { "removeRelationships", Keys.R }
            };

            Dictionary<Keys, string> relationshipShortcuts = new Dictionary<Keys, string>()
            {
                { Keys.S, "addAssociation" },
                { Keys.I, "addInheritance" },
                { Keys.T, "addRealization" },
                { Keys.V, "addDependency" },
                { Keys.Q, "addAggregation" },
                { Keys.F, "addComposition" }
            };

            Dictionary<Keys, Action> shortcutActions = new Dictionary<Keys, Action>
            {
                { Keys.Delete, () => DeleteSelectedElement() },
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

            if (relationshipShortcuts.ContainsKey(keyData))
            {
                string relationshipName = relationshipShortcuts[keyData];
                deselectTool();
                foreach (object item in toolStripButton_relationships.DropDown.Items)
                {
                    if (item is ToolStripMenuItem menuItem && menuItem.Tag.ToString() == relationshipName)
                    {
                        ToolStripMenuItem_Click(menuItem, null);
                        break;
                    }
                }
            }

            if (shortcutActions.ContainsKey(keyData))
            {
                shortcutActions[keyData].Invoke();
            }

            if (keyData == Keys.Subtract)
            {
                ZoomOut();
            }
            if (keyData == Keys.Add)
            {
                ZoomIn();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void programVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramVersionForm versionForm = new ProgramVersionForm();
            versionForm.ShowDialog();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void toolStripButton_split_Click(object sender, EventArgs e)
        {
            deselectTool();
            selectedTool = "addRelationship";
            Dictionary<string, RelationshipType> pairs = new Dictionary<string, RelationshipType>()
            {
                { "addAssociation", RelationshipType.Association },
                { "addInheritance", RelationshipType.Inheritance },
                { "addRealization", RelationshipType.Realization },
                { "addDependency", RelationshipType.Dependency },
                { "addAggregation", RelationshipType.Aggregation },
                { "addComposition", RelationshipType.Composition }

            };
            relationship = pairs[((ToolStripButton)sender).Tag.ToString()];
            ((ToolStripButton)sender).Checked = true;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deselectTool();
            selectedTool = "addRelationship";
            Dictionary<string, RelationshipType> pairs = new Dictionary<string, RelationshipType>()
            {
                { "addAssociation", RelationshipType.Association },
                { "addInheritance", RelationshipType.Inheritance },
                { "addRealization", RelationshipType.Realization },
                { "addDependency", RelationshipType.Dependency },
                { "addAggregation", RelationshipType.Aggregation },
                { "addComposition", RelationshipType.Composition }

            };
            relationship = pairs[((ToolStripMenuItem)sender).Tag.ToString()];
            toolStripButton_addRelationship.Tag = ((ToolStripMenuItem)sender).Tag;
            toolStripButton_addRelationship.Image = ((ToolStripMenuItem)sender).Image;
            toolStripButton_addRelationship.Text = ((ToolStripMenuItem)sender).Text;
            toolStripButton_addRelationship.Checked = true;
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeGenerator.GenerateSingleFile(elements, "testfile.cs", "testNamespace");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveChanges()) e.Cancel = true;
        }

        private bool SaveChanges()
        {
            if (!justSaved)
                switch (MessageBox.Show("Would you like to save unsaved changes?", "Save changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                {
                    case DialogResult.Yes:
                        Save();
                        return true;

                    case DialogResult.No:
                        return true;

                    case DialogResult.Cancel:
                        return false;
                }
            return true;
        }
    }
}
