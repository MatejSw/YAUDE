namespace YAUDE
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            toolStrip_bottom = new ToolStrip();
            toolStripButton_cursor = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripButton2 = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton_addElement = new ToolStripButton();
            toolStripButton1 = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripLabel_status = new ToolStripLabel();
            toolStripButton_addAssociation = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            toolStripButton_zoomIn = new ToolStripButton();
            toolStripButton_zoomOut = new ToolStripButton();
            toolStripLabel_zoom = new ToolStripLabel();
            toolStrip_top = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem1 = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            generateToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            toolStripDropDownButton3 = new ToolStripDropDownButton();
            programVersionToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            toolStrip_bottom.SuspendLayout();
            toolStrip_top.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = SystemColors.ControlLight;
            pictureBox1.Location = new Point(0, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 397);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.DoubleClick += pictureBox1_DoubleClick;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // toolStrip_bottom
            // 
            toolStrip_bottom.Dock = DockStyle.Bottom;
            toolStrip_bottom.Items.AddRange(new ToolStripItem[] { toolStripButton_cursor, toolStripSeparator4, toolStripButton2, toolStripSeparator2, toolStripButton_addElement, toolStripButton1, toolStripSeparator3, toolStripLabel_status, toolStripButton_addAssociation, toolStripButton4, toolStripButton5, toolStripSeparator5, toolStripButton_zoomIn, toolStripButton_zoomOut, toolStripLabel_zoom });
            toolStrip_bottom.Location = new Point(0, 425);
            toolStrip_bottom.Name = "toolStrip_bottom";
            toolStrip_bottom.Size = new Size(800, 25);
            toolStrip_bottom.TabIndex = 2;
            toolStrip_bottom.Text = "toolStrip1";
            // 
            // toolStripButton_cursor
            // 
            toolStripButton_cursor.Checked = true;
            toolStripButton_cursor.CheckState = CheckState.Checked;
            toolStripButton_cursor.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_cursor.Image = Properties.Resources.cursor;
            toolStripButton_cursor.ImageTransparentColor = Color.Magenta;
            toolStripButton_cursor.Name = "toolStripButton_cursor";
            toolStripButton_cursor.Size = new Size(23, 22);
            toolStripButton_cursor.Tag = "cursor";
            toolStripButton_cursor.Text = "Cursor (C)";
            toolStripButton_cursor.Click += toolStripButton_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = Properties.Resources.pan;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Tag = "pan";
            toolStripButton2.Text = "Pan (P)";
            toolStripButton2.Click += toolStripButton_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripButton_addElement
            // 
            toolStripButton_addElement.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_addElement.Image = Properties.Resources.addElement;
            toolStripButton_addElement.ImageTransparentColor = Color.Magenta;
            toolStripButton_addElement.Name = "toolStripButton_addElement";
            toolStripButton_addElement.Size = new Size(23, 22);
            toolStripButton_addElement.Tag = "addElement";
            toolStripButton_addElement.Text = "Add element (A)";
            toolStripButton_addElement.Click += toolStripButton_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = Properties.Resources.deleteElement;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Tag = "deleteElement";
            toolStripButton1.Text = "Delete element (D)";
            toolStripButton1.Click += toolStripButton_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // toolStripLabel_status
            // 
            toolStripLabel_status.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel_status.Name = "toolStripLabel_status";
            toolStripLabel_status.Size = new Size(0, 22);
            // 
            // toolStripButton_addAssociation
            // 
            toolStripButton_addAssociation.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_addAssociation.Image = Properties.Resources.addAssociation;
            toolStripButton_addAssociation.ImageTransparentColor = Color.Magenta;
            toolStripButton_addAssociation.Name = "toolStripButton_addAssociation";
            toolStripButton_addAssociation.Size = new Size(23, 22);
            toolStripButton_addAssociation.Tag = "addAssociation";
            toolStripButton_addAssociation.Text = "Add Association (S)";
            toolStripButton_addAssociation.Click += toolStripButton_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = Properties.Resources.addDependency;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 22);
            toolStripButton4.Tag = "addDependency";
            toolStripButton4.Text = "Add Dependency (E)";
            toolStripButton4.Click += toolStripButton_Click;
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = Properties.Resources.removeRelationships;
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(23, 22);
            toolStripButton5.Tag = "removeRelationships";
            toolStripButton5.Text = "Remove Relationships (R)";
            toolStripButton5.Click += toolStripButton_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 25);
            // 
            // toolStripButton_zoomIn
            // 
            toolStripButton_zoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_zoomIn.Image = Properties.Resources.zoomIn;
            toolStripButton_zoomIn.ImageTransparentColor = Color.Magenta;
            toolStripButton_zoomIn.Name = "toolStripButton_zoomIn";
            toolStripButton_zoomIn.Size = new Size(23, 22);
            toolStripButton_zoomIn.Text = "Zoom In";
            toolStripButton_zoomIn.Click += toolStripButton_zoomIn_Click;
            // 
            // toolStripButton_zoomOut
            // 
            toolStripButton_zoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_zoomOut.Image = Properties.Resources.zoomOut;
            toolStripButton_zoomOut.ImageTransparentColor = Color.Magenta;
            toolStripButton_zoomOut.Name = "toolStripButton_zoomOut";
            toolStripButton_zoomOut.Size = new Size(23, 22);
            toolStripButton_zoomOut.Text = "Zoom Out";
            toolStripButton_zoomOut.Click += toolStripButton_zoomOut_Click;
            // 
            // toolStripLabel_zoom
            // 
            toolStripLabel_zoom.Name = "toolStripLabel_zoom";
            toolStripLabel_zoom.Size = new Size(35, 22);
            toolStripLabel_zoom.Text = "100%";
            // 
            // toolStrip_top
            // 
            toolStrip_top.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripSeparator1, toolStripDropDownButton2, toolStripSeparator6, toolStripDropDownButton3 });
            toolStrip_top.Location = new Point(0, 0);
            toolStrip_top.Name = "toolStrip_top";
            toolStrip_top.Size = new Size(800, 25);
            toolStrip_top.TabIndex = 3;
            toolStrip_top.Text = "toolStrip2";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem1, saveAsToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(38, 22);
            toolStripDropDownButton1.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            newToolStripMenuItem.Size = new Size(195, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            openToolStripMenuItem.Size = new Size(195, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem1
            // 
            saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            saveToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+S";
            saveToolStripMenuItem1.Size = new Size(195, 22);
            saveToolStripMenuItem1.Text = "Save";
            saveToolStripMenuItem1.Click += saveToolStripMenuItem1_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+S";
            saveAsToolStripMenuItem.Size = new Size(195, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { generateToolStripMenuItem });
            toolStripDropDownButton2.Image = (Image)resources.GetObject("toolStripDropDownButton2.Image");
            toolStripDropDownButton2.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            toolStripDropDownButton2.Size = new Size(48, 22);
            toolStripDropDownButton2.Text = "Code";
            // 
            // generateToolStripMenuItem
            // 
            generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            generateToolStripMenuItem.Size = new Size(207, 22);
            generateToolStripMenuItem.Text = "Generate (Coming soon!)";
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 25);
            // 
            // toolStripDropDownButton3
            // 
            toolStripDropDownButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton3.DropDownItems.AddRange(new ToolStripItem[] { programVersionToolStripMenuItem });
            toolStripDropDownButton3.Image = (Image)resources.GetObject("toolStripDropDownButton3.Image");
            toolStripDropDownButton3.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            toolStripDropDownButton3.Size = new Size(45, 22);
            toolStripDropDownButton3.Text = "Help";
            // 
            // programVersionToolStripMenuItem
            // 
            programVersionToolStripMenuItem.Name = "programVersionToolStripMenuItem";
            programVersionToolStripMenuItem.Size = new Size(180, 22);
            programVersionToolStripMenuItem.Text = "Program Version";
            programVersionToolStripMenuItem.Click += programVersionToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStrip_top);
            Controls.Add(toolStrip_bottom);
            Controls.Add(pictureBox1);
            KeyPreview = true;
            Name = "Form1";
            Text = "YAUDE";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            toolStrip_bottom.ResumeLayout(false);
            toolStrip_bottom.PerformLayout();
            toolStrip_top.ResumeLayout(false);
            toolStrip_top.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private ToolStrip toolStrip_bottom;
        private ToolStripButton toolStripButton_cursor;
        private ToolStrip toolStrip_top;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem generateToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton_addElement;
        private ToolStripButton toolStripButton1;
        private ToolStripLabel toolStripLabel_status;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripButton2;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton toolStripButton_zoomIn;
        private ToolStripButton toolStripButton_zoomOut;
        private ToolStripLabel toolStripLabel_zoom;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton_addAssociation;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripDropDownButton toolStripDropDownButton3;
        private ToolStripMenuItem programVersionToolStripMenuItem;
    }
}
