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
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton_addElement = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripButton1 = new ToolStripButton();
            toolStrip_top = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem1 = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            generateToolStripMenuItem = new ToolStripMenuItem();
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
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // toolStrip_bottom
            // 
            toolStrip_bottom.Dock = DockStyle.Bottom;
            toolStrip_bottom.Items.AddRange(new ToolStripItem[] { toolStripButton_cursor, toolStripSeparator2, toolStripButton_addElement, toolStripSeparator3, toolStripButton1 });
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
            toolStripButton_cursor.Image = (Image)resources.GetObject("toolStripButton_cursor.Image");
            toolStripButton_cursor.ImageTransparentColor = Color.Magenta;
            toolStripButton_cursor.Name = "toolStripButton_cursor";
            toolStripButton_cursor.Size = new Size(23, 22);
            toolStripButton_cursor.Tag = "cursor";
            toolStripButton_cursor.Text = "Cursor";
            toolStripButton_cursor.Click += toolStripButton_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripButton_addElement
            // 
            toolStripButton_addElement.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton_addElement.Image = (Image)resources.GetObject("toolStripButton_addElement.Image");
            toolStripButton_addElement.ImageTransparentColor = Color.Magenta;
            toolStripButton_addElement.Name = "toolStripButton_addElement";
            toolStripButton_addElement.Size = new Size(23, 22);
            toolStripButton_addElement.Tag = "addElement";
            toolStripButton_addElement.Text = "Add element";
            toolStripButton_addElement.Click += toolStripButton_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Tag = "deleteElement";
            toolStripButton1.Text = "Delete element";
            toolStripButton1.Click += toolStripButton_Click;
            // 
            // toolStrip_top
            // 
            toolStrip_top.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripSeparator1, toolStripDropDownButton2 });
            toolStrip_top.Location = new Point(0, 0);
            toolStrip_top.Name = "toolStrip_top";
            toolStrip_top.Size = new Size(800, 25);
            toolStrip_top.TabIndex = 3;
            toolStrip_top.Text = "toolStrip2";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem1 });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(38, 22);
            toolStripDropDownButton1.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(103, 22);
            newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(103, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem1
            // 
            saveToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { saveAsToolStripMenuItem });
            saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            saveToolStripMenuItem1.Size = new Size(103, 22);
            saveToolStripMenuItem1.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(123, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStrip_top);
            Controls.Add(toolStrip_bottom);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
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
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem generateToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton_addElement;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButton1;
    }
}
