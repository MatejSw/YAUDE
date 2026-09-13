namespace YAUDE
{
    partial class EditClassForm
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
            textBox_className = new TextBox();
            label1 = new Label();
            button_addAttribute = new Button();
            dataGridView_attributes = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            dataGridView_methods = new DataGridView();
            button_addMethod = new Button();
            button_cancel = new Button();
            button_ok = new Button();
            errorProvider1 = new ErrorProvider(components);
            button_deleteAttribute = new Button();
            button_deleteMethod = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_attributes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_methods).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // textBox_className
            // 
            textBox_className.Location = new Point(17, 36);
            textBox_className.Name = "textBox_className";
            textBox_className.Size = new Size(456, 23);
            textBox_className.TabIndex = 0;
            textBox_className.TextChanged += textBox_className_TextChanged;
            textBox_className.Validating += textBox_className_Validating;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 15);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 1;
            label1.Text = "Class Name";
            // 
            // button_addAttribute
            // 
            button_addAttribute.Location = new Point(449, 88);
            button_addAttribute.Name = "button_addAttribute";
            button_addAttribute.Size = new Size(24, 24);
            button_addAttribute.TabIndex = 3;
            button_addAttribute.Text = "+";
            button_addAttribute.UseVisualStyleBackColor = true;
            button_addAttribute.Click += button_addAttribute_Click;
            // 
            // dataGridView_attributes
            // 
            dataGridView_attributes.AllowUserToAddRows = false;
            dataGridView_attributes.AllowUserToDeleteRows = false;
            dataGridView_attributes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_attributes.Location = new Point(17, 118);
            dataGridView_attributes.Name = "dataGridView_attributes";
            dataGridView_attributes.Size = new Size(456, 109);
            dataGridView_attributes.TabIndex = 4;
            dataGridView_attributes.Validating += dataGridView_attributes_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 93);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 5;
            label2.Text = "Attributes";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 246);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 8;
            label3.Text = "Methods";
            // 
            // dataGridView_methods
            // 
            dataGridView_methods.AllowUserToAddRows = false;
            dataGridView_methods.AllowUserToDeleteRows = false;
            dataGridView_methods.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_methods.Location = new Point(17, 271);
            dataGridView_methods.Name = "dataGridView_methods";
            dataGridView_methods.Size = new Size(456, 109);
            dataGridView_methods.TabIndex = 7;
            dataGridView_methods.Validating += dataGridView_methods_Validating;
            // 
            // button_addMethod
            // 
            button_addMethod.Location = new Point(449, 241);
            button_addMethod.Name = "button_addMethod";
            button_addMethod.Size = new Size(24, 24);
            button_addMethod.TabIndex = 6;
            button_addMethod.Text = "+";
            button_addMethod.UseVisualStyleBackColor = true;
            button_addMethod.Click += button_addMethod_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(398, 415);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(75, 23);
            button_cancel.TabIndex = 9;
            button_cancel.Text = "Cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // button_ok
            // 
            button_ok.Location = new Point(317, 415);
            button_ok.Name = "button_ok";
            button_ok.Size = new Size(75, 23);
            button_ok.TabIndex = 10;
            button_ok.Text = "OK";
            button_ok.UseVisualStyleBackColor = true;
            button_ok.Click += button_ok_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // button_deleteAttribute
            // 
            button_deleteAttribute.Location = new Point(419, 88);
            button_deleteAttribute.Name = "button_deleteAttribute";
            button_deleteAttribute.Size = new Size(24, 24);
            button_deleteAttribute.TabIndex = 11;
            button_deleteAttribute.Text = "-";
            button_deleteAttribute.UseVisualStyleBackColor = true;
            button_deleteAttribute.Click += button_deleteAttribute_Click;
            // 
            // button_deleteMethod
            // 
            button_deleteMethod.Location = new Point(419, 241);
            button_deleteMethod.Name = "button_deleteMethod";
            button_deleteMethod.Size = new Size(24, 24);
            button_deleteMethod.TabIndex = 12;
            button_deleteMethod.Text = "-";
            button_deleteMethod.UseVisualStyleBackColor = true;
            button_deleteMethod.Click += button_deleteMethod_Click;
            // 
            // EditClassForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            ClientSize = new Size(485, 450);
            Controls.Add(button_deleteMethod);
            Controls.Add(button_deleteAttribute);
            Controls.Add(button_ok);
            Controls.Add(button_cancel);
            Controls.Add(label3);
            Controls.Add(dataGridView_methods);
            Controls.Add(button_addMethod);
            Controls.Add(label2);
            Controls.Add(dataGridView_attributes);
            Controls.Add(button_addAttribute);
            Controls.Add(label1);
            Controls.Add(textBox_className);
            ImeMode = ImeMode.On;
            Name = "EditClassForm";
            Text = "EditClassForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView_attributes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_methods).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_className;
        private Label label1;
        private Button button_addAttribute;
        private DataGridView dataGridView_attributes;
        private Label label2;
        private Label label3;
        private DataGridView dataGridView_methods;
        private Button button_addMethod;
        private Button button_cancel;
        private Button button_ok;
        private ErrorProvider errorProvider1;
        private Button button_deleteMethod;
        private Button button_deleteAttribute;
    }
}