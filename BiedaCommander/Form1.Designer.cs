﻿namespace BiedaCommander
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.usuńPlikF8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stwórzFolderF7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zmieńNazweF6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.przenieśObokF5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Nazwa1 = new System.Windows.Forms.ColumnHeader();
            this.Data_utworzenia1 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.Nazwa2 = new System.Windows.Forms.ColumnHeader();
            this.Data_utworzenia2 = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuńPlikF8ToolStripMenuItem,
            this.stwórzFolderF7ToolStripMenuItem,
            this.zmieńNazweF6ToolStripMenuItem,
            this.przenieśObokF5ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(972, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // usuńPlikF8ToolStripMenuItem
            // 
            this.usuńPlikF8ToolStripMenuItem.Name = "usuńPlikF8ToolStripMenuItem";
            this.usuńPlikF8ToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.usuńPlikF8ToolStripMenuItem.Text = "Usuń plik (F8)";
            this.usuńPlikF8ToolStripMenuItem.Click += new System.EventHandler(this.usuńPlikF8ToolStripMenuItem_Click);
            // 
            // stwórzFolderF7ToolStripMenuItem
            // 
            this.stwórzFolderF7ToolStripMenuItem.Name = "stwórzFolderF7ToolStripMenuItem";
            this.stwórzFolderF7ToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.stwórzFolderF7ToolStripMenuItem.Text = "Stwórz folder (F7)";
            this.stwórzFolderF7ToolStripMenuItem.Click += new System.EventHandler(this.stwórzFolderF7ToolStripMenuItem_Click);
            // 
            // zmieńNazweF6ToolStripMenuItem
            // 
            this.zmieńNazweF6ToolStripMenuItem.Name = "zmieńNazweF6ToolStripMenuItem";
            this.zmieńNazweF6ToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.zmieńNazweF6ToolStripMenuItem.Text = "Zmień nazwe (F6)";
            this.zmieńNazweF6ToolStripMenuItem.Click += new System.EventHandler(this.zmieńNazweF6ToolStripMenuItem_Click);
            // 
            // przenieśObokF5ToolStripMenuItem
            // 
            this.przenieśObokF5ToolStripMenuItem.Name = "przenieśObokF5ToolStripMenuItem";
            this.przenieśObokF5ToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.przenieśObokF5ToolStripMenuItem.Text = "Przenieś obok (F5)";
            this.przenieśObokF5ToolStripMenuItem.Click += new System.EventHandler(this.przenieśObokF5ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comboBox2);
            this.splitContainer1.Panel2.Controls.Add(this.listView2);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(972, 426);
            this.splitContainer1.SplitterDistance = 487;
            this.splitContainer1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(40, 23);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Nazwa1,
            this.Data_utworzenia1});
            this.listView1.Location = new System.Drawing.Point(12, 35);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(470, 388);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.DragOver += new System.Windows.Forms.DragEventHandler(this.listView1_DragOver);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick_1);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // Nazwa1
            // 
            this.Nazwa1.Text = "Nazwa";
            this.Nazwa1.Width = 180;
            // 
            // Data_utworzenia1
            // 
            this.Data_utworzenia1.Text = "Data utworzenia";
            this.Data_utworzenia1.Width = 240;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(58, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(422, 23);
            this.label1.TabIndex = 0;
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(3, 6);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(40, 23);
            this.comboBox2.TabIndex = 7;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // listView2
            // 
            this.listView2.AllowDrop = true;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Nazwa2,
            this.Data_utworzenia2});
            this.listView2.Location = new System.Drawing.Point(3, 32);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(470, 388);
            this.listView2.TabIndex = 6;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView2_ColumnClick_1);
            this.listView2.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView2_ItemDrag);
            this.listView2.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView2_DragDrop);
            this.listView2.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView2_DragEnter);
            this.listView2.DragOver += new System.Windows.Forms.DragEventHandler(this.listView2_DragOver);
            this.listView2.DoubleClick += new System.EventHandler(this.listView2_DoubleClick);
            this.listView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView2_KeyDown);
            // 
            // Nazwa2
            // 
            this.Nazwa2.Text = "Nazwa";
            this.Nazwa2.Width = 120;
            // 
            // Data_utworzenia2
            // 
            this.Data_utworzenia2.Text = "Data utworzenia";
            this.Data_utworzenia2.Width = 240;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(49, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(424, 23);
            this.label2.TabIndex = 4;
            this.label2.DoubleClick += new System.EventHandler(this.label2_DoubleClick);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "BiedaCommander v1.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private SplitContainer splitContainer1;
        private Label label1;
        private ListView listView1;
        private ListView listView2;
        private Label label2;
        private ColumnHeader Nazwa1;
        private ColumnHeader Data_utworzenia1;
        private ColumnHeader Nazwa2;
        private ColumnHeader Data_utworzenia2;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ToolStripMenuItem usuńPlikF8ToolStripMenuItem;
        private ToolStripMenuItem stwórzFolderF7ToolStripMenuItem;
        private ToolStripMenuItem zmieńNazweF6ToolStripMenuItem;
        private ToolStripMenuItem przenieśObokF5ToolStripMenuItem;
    }
}