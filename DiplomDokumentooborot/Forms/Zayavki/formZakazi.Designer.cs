namespace DiplomDokumentooborot.Forms
{
    partial class FormZakazi
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
            this.components = new System.ComponentModel.Container();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.новаяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вРаботеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отмененаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выполненаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.неВыполненаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(433, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 35);
            this.button2.TabIndex = 4;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 35);
            this.button1.TabIndex = 5;
            this.button1.Text = "Добавить заявку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.SlateBlue;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(13, 87);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 351);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяToolStripMenuItem,
            this.вРаботеToolStripMenuItem,
            this.отмененаToolStripMenuItem,
            this.выполненаToolStripMenuItem,
            this.неВыполненаToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 114);
            // 
            // новаяToolStripMenuItem
            // 
            this.новаяToolStripMenuItem.Name = "новаяToolStripMenuItem";
            this.новаяToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.новаяToolStripMenuItem.Text = "Новая";
            this.новаяToolStripMenuItem.Click += new System.EventHandler(this.новаяToolStripMenuItem_Click);
            // 
            // вРаботеToolStripMenuItem
            // 
            this.вРаботеToolStripMenuItem.Name = "вРаботеToolStripMenuItem";
            this.вРаботеToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.вРаботеToolStripMenuItem.Text = "В работе ";
            this.вРаботеToolStripMenuItem.Click += new System.EventHandler(this.вРаботеToolStripMenuItem_Click);
            // 
            // отмененаToolStripMenuItem
            // 
            this.отмененаToolStripMenuItem.Name = "отмененаToolStripMenuItem";
            this.отмененаToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.отмененаToolStripMenuItem.Text = "Отменена";
            this.отмененаToolStripMenuItem.Click += new System.EventHandler(this.отмененаToolStripMenuItem_Click);
            // 
            // выполненаToolStripMenuItem
            // 
            this.выполненаToolStripMenuItem.Name = "выполненаToolStripMenuItem";
            this.выполненаToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.выполненаToolStripMenuItem.Text = "Выполнена";
            this.выполненаToolStripMenuItem.Click += new System.EventHandler(this.выполненаToolStripMenuItem_Click);
            // 
            // неВыполненаToolStripMenuItem
            // 
            this.неВыполненаToolStripMenuItem.Name = "неВыполненаToolStripMenuItem";
            this.неВыполненаToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.неВыполненаToolStripMenuItem.Text = "Не выполнена";
            this.неВыполненаToolStripMenuItem.Click += new System.EventHandler(this.неВыполненаToolStripMenuItem_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(222, 30);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(168, 35);
            this.button4.TabIndex = 6;
            this.button4.Text = "Обновить данные";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(624, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 35);
            this.button3.TabIndex = 7;
            this.button3.Text = "Сделать отчет";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormZakazi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(801, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormZakazi";
            this.Text = "FormZakazi";
            this.Load += new System.EventHandler(this.FormZakazi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem новаяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вРаботеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отмененаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выполненаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem неВыполненаToolStripMenuItem;
        private System.Windows.Forms.Button button3;
    }
}