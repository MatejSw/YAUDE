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
            button_ok = new Button();
            button_cancel = new Button();
            errorProvider1 = new ErrorProvider(components);
            richTextBox_values = new RichTextBox();
            comboBox1 = new ComboBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // textBox_name
            // 
            textBox_name.Location = new Point(12, 37);
            textBox_name.Name = "textBox_name";
            textBox_name.Size = new Size(360, 23);
            textBox_name.TabIndex = 1;
            textBox_name.TextChanged += textBox_name_TextChanged;
            textBox_name.Validating += textBox_name_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 140);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 2;
            label2.Text = "Values";
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
            // richTextBox_values
            // 
            richTextBox_values.Location = new Point(12, 158);
            richTextBox_values.Name = "richTextBox_values";
            richTextBox_values.Size = new Size(360, 177);
            richTextBox_values.TabIndex = 16;
            richTextBox_values.Text = "";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Public", "Private", "Protected", "Internal" });
            comboBox1.Location = new Point(12, 99);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(360, 23);
            comboBox1.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 79);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 17;
            label4.Text = "Visiblity";
            // 
            // EditEnumForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 376);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(richTextBox_values);
            Controls.Add(button_ok);
            Controls.Add(button_cancel);
            Controls.Add(label2);
            Controls.Add(textBox_name);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "EditEnumForm";
            Text = "Edit Enum";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox_name;
        private Label label2;
        private Button button_ok;
        private Button button_cancel;
        private ErrorProvider errorProvider1;
        private RichTextBox richTextBox_values;
        private ComboBox comboBox1;
        private Label label4;
    }
}