namespace YAUDE.Forms
{
    partial class EditEnumForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            textBox_name = new TextBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            button_deleteAttribute = new Button();
            button_addAttribute = new Button();
            button_ok = new Button();
            button_cancel = new Button();
            errorProvider1 = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 35);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // textBox_name
            // 
            textBox_name.Location = new Point(12, 53);
            textBox_name.Name = "textBox_name";
            textBox_name.Size = new Size(360, 23);
            textBox_name.TabIndex = 1;
            textBox_name.Validating += textBox_name_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 125);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 2;
            label2.Text = "Values";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 143);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(360, 166);
            dataGridView1.TabIndex = 3;
            // 
            // button_deleteAttribute
            // 
            button_deleteAttribute.Location = new Point(318, 113);
            button_deleteAttribute.Name = "button_deleteAttribute";
            button_deleteAttribute.Size = new Size(24, 24);
            button_deleteAttribute.TabIndex = 13;
            button_deleteAttribute.Text = "-";
            button_deleteAttribute.UseVisualStyleBackColor = true;
            button_deleteAttribute.Click += button_deleteAttribute_Click;
            // 
            // button_addAttribute
            // 
            button_addAttribute.Location = new Point(348, 113);
            button_addAttribute.Name = "button_addAttribute";
            button_addAttribute.Size = new Size(24, 24);
            button_addAttribute.TabIndex = 12;
            button_addAttribute.Text = "+";
            button_addAttribute.UseVisualStyleBackColor = true;
            button_addAttribute.Click += button_addAttribute_Click;
            // 
            // button_ok
            // 
            button_ok.Location = new Point(216, 341);
            button_ok.Name = "button_ok";
            button_ok.Size = new Size(75, 23);
            button_ok.TabIndex = 15;
            button_ok.Text = "OK";
            button_ok.UseVisualStyleBackColor = true;
            button_ok.Click += button_ok_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(297, 341);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(75, 23);
            button_cancel.TabIndex = 14;
            button_cancel.Text = "Cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // EditEnumForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 376);
            Controls.Add(button_ok);
            Controls.Add(button_cancel);
            Controls.Add(button_deleteAttribute);
            Controls.Add(button_addAttribute);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(textBox_name);
            Controls.Add(label1);
            Name = "EditEnumForm";
            Text = "EditEnumForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox_name;
        private Label label2;
        private DataGridView dataGridView1;
        private Button button_deleteAttribute;
        private Button button_addAttribute;
        private Button button_ok;
        private Button button_cancel;
        private ErrorProvider errorProvider1;
    }
}