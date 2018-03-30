namespace Duplicator
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.AutorizeButton = new System.Windows.Forms.ToolStripMenuItem();
            this.FormClearButton = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveTemplateButton = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadTemplateButton = new System.Windows.Forms.ToolStripMenuItem();
            this.LinkRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DataAboutPostLabel = new System.Windows.Forms.Label();
            this.PostsDataGridView = new System.Windows.Forms.DataGridView();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTimePickerOnForm = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ResultRichTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.IntervalUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.AnalizeButton = new System.Windows.Forms.Button();
            this.PublicButton = new System.Windows.Forms.Button();
            this.CopyRadioButton = new System.Windows.Forms.RadioButton();
            this.RepostRadioButton = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PostsDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Moccasin;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutorizeButton,
            this.FormClearButton,
            this.SaveTemplateButton,
            this.LoadTemplateButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // AutorizeButton
            // 
            this.AutorizeButton.Name = "AutorizeButton";
            this.AutorizeButton.Size = new System.Drawing.Size(90, 20);
            this.AutorizeButton.Text = "Авторизация";
            // 
            // FormClearButton
            // 
            this.FormClearButton.Name = "FormClearButton";
            this.FormClearButton.Size = new System.Drawing.Size(112, 20);
            this.FormClearButton.Text = "Очистить форму";
            // 
            // SaveTemplateButton
            // 
            this.SaveTemplateButton.Name = "SaveTemplateButton";
            this.SaveTemplateButton.Size = new System.Drawing.Size(125, 20);
            this.SaveTemplateButton.Text = "Сохранить шаблон";
            // 
            // LoadTemplateButton
            // 
            this.LoadTemplateButton.Name = "LoadTemplateButton";
            this.LoadTemplateButton.Size = new System.Drawing.Size(121, 20);
            this.LoadTemplateButton.Text = "Загрузить шаблон";
            // 
            // LinkRichTextBox
            // 
            this.LinkRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkRichTextBox.Location = new System.Drawing.Point(148, 31);
            this.LinkRichTextBox.Multiline = false;
            this.LinkRichTextBox.Name = "LinkRichTextBox";
            this.LinkRichTextBox.Size = new System.Drawing.Size(283, 23);
            this.LinkRichTextBox.TabIndex = 15;
            this.LinkRichTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Введите ссылку на пост: ";
            // 
            // DataAboutPostLabel
            // 
            this.DataAboutPostLabel.AutoSize = true;
            this.DataAboutPostLabel.Location = new System.Drawing.Point(6, 27);
            this.DataAboutPostLabel.Name = "DataAboutPostLabel";
            this.DataAboutPostLabel.Size = new System.Drawing.Size(0, 13);
            this.DataAboutPostLabel.TabIndex = 8;
            // 
            // PostsDataGridView
            // 
            this.PostsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PostsDataGridView.BackgroundColor = System.Drawing.Color.RosyBrown;
            this.PostsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PostsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Group,
            this.Time});
            this.PostsDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.PostsDataGridView.Location = new System.Drawing.Point(6, 19);
            this.PostsDataGridView.Name = "PostsDataGridView";
            this.PostsDataGridView.RowHeadersVisible = false;
            this.PostsDataGridView.Size = new System.Drawing.Size(413, 247);
            this.PostsDataGridView.TabIndex = 1;
            // 
            // Group
            // 
            this.Group.HeaderText = "Группа";
            this.Group.Name = "Group";
            this.Group.Width = 307;
            // 
            // Time
            // 
            this.Time.HeaderText = "Время публикации";
            this.Time.Name = "Time";
            // 
            // dateTimePickerOnForm
            // 
            this.dateTimePickerOnForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerOnForm.Location = new System.Drawing.Point(611, 33);
            this.dateTimePickerOnForm.Name = "dateTimePickerOnForm";
            this.dateTimePickerOnForm.Size = new System.Drawing.Size(164, 20);
            this.dateTimePickerOnForm.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(501, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Дата публикации";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(854, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.ForeColor = System.Drawing.Color.Green;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ResultRichTextBox
            // 
            this.ResultRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultRichTextBox.Location = new System.Drawing.Point(425, 98);
            this.ResultRichTextBox.Name = "ResultRichTextBox";
            this.ResultRichTextBox.Size = new System.Drawing.Size(399, 168);
            this.ResultRichTextBox.TabIndex = 11;
            this.ResultRichTextBox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DataAboutPostLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(830, 116);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "О посте";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.RepostRadioButton);
            this.groupBox2.Controls.Add(this.CopyRadioButton);
            this.groupBox2.Controls.Add(this.IntervalUpDown);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.AnalizeButton);
            this.groupBox2.Controls.Add(this.PublicButton);
            this.groupBox2.Controls.Add(this.PostsDataGridView);
            this.groupBox2.Controls.Add(this.ResultRichTextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(830, 276);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Активная область";
            // 
            // IntervalUpDown
            // 
            this.IntervalUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IntervalUpDown.Location = new System.Drawing.Point(735, 18);
            this.IntervalUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.IntervalUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.IntervalUpDown.Name = "IntervalUpDown";
            this.IntervalUpDown.Size = new System.Drawing.Size(89, 20);
            this.IntervalUpDown.TabIndex = 14;
            this.IntervalUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(644, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Интервал(мин):";
            // 
            // AnalizeButton
            // 
            this.AnalizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalizeButton.Location = new System.Drawing.Point(425, 66);
            this.AnalizeButton.Name = "AnalizeButton";
            this.AnalizeButton.Size = new System.Drawing.Size(202, 25);
            this.AnalizeButton.TabIndex = 12;
            this.AnalizeButton.Text = "Анализ";
            this.AnalizeButton.UseVisualStyleBackColor = true;
            // 
            // PublicButton
            // 
            this.PublicButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PublicButton.Location = new System.Drawing.Point(633, 66);
            this.PublicButton.Name = "PublicButton";
            this.PublicButton.Size = new System.Drawing.Size(191, 25);
            this.PublicButton.TabIndex = 12;
            this.PublicButton.Text = "Опубликовать";
            this.PublicButton.UseVisualStyleBackColor = true;
            // 
            // CopyRadioButton
            // 
            this.CopyRadioButton.AutoSize = true;
            this.CopyRadioButton.Checked = true;
            this.CopyRadioButton.Location = new System.Drawing.Point(426, 20);
            this.CopyRadioButton.Name = "CopyRadioButton";
            this.CopyRadioButton.Size = new System.Drawing.Size(56, 17);
            this.CopyRadioButton.TabIndex = 15;
            this.CopyRadioButton.TabStop = true;
            this.CopyRadioButton.Text = "Копия";
            this.CopyRadioButton.UseVisualStyleBackColor = true;
            // 
            // RepostRadioButton
            // 
            this.RepostRadioButton.AutoSize = true;
            this.RepostRadioButton.Location = new System.Drawing.Point(426, 42);
            this.RepostRadioButton.Name = "RepostRadioButton";
            this.RepostRadioButton.Size = new System.Drawing.Size(61, 17);
            this.RepostRadioButton.TabIndex = 16;
            this.RepostRadioButton.Text = "Репост";
            this.RepostRadioButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 483);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dateTimePickerOnForm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LinkRichTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(870, 522);
            this.Name = "MainForm";
            this.Text = "Duplicator1.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PostsDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AutorizeButton;
        private System.Windows.Forms.RichTextBox LinkRichTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DataAboutPostLabel;
        private System.Windows.Forms.DataGridView PostsDataGridView;
        private System.Windows.Forms.DateTimePicker dateTimePickerOnForm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.RichTextBox ResultRichTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem FormClearButton;
        private System.Windows.Forms.Button PublicButton;
        private System.Windows.Forms.NumericUpDown IntervalUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.ToolStripMenuItem SaveTemplateButton;
        private System.Windows.Forms.ToolStripMenuItem LoadTemplateButton;
        private System.Windows.Forms.Button AnalizeButton;
        private System.Windows.Forms.RadioButton CopyRadioButton;
        private System.Windows.Forms.RadioButton RepostRadioButton;
    }
}

