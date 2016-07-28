namespace EquipmentsEditor.Forms
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开存档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成存档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LV_Countries = new System.Windows.Forms.ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_RecoverAll = new System.Windows.Forms.Button();
            this.btn_DeleteAll = new System.Windows.Forms.Button();
            this.tv_EquipmentType = new System.Windows.Forms.TreeView();
            this.gb_EquipmentType = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsForigen = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ob = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gb_EquipmentType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开存档ToolStripMenuItem,
            this.生成存档ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1065, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开存档ToolStripMenuItem
            // 
            this.打开存档ToolStripMenuItem.Name = "打开存档ToolStripMenuItem";
            this.打开存档ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.打开存档ToolStripMenuItem.Text = "打开存档";
            this.打开存档ToolStripMenuItem.Click += new System.EventHandler(this.打开存档ToolStripMenuItem_Click_1);
            // 
            // 生成存档ToolStripMenuItem
            // 
            this.生成存档ToolStripMenuItem.Name = "生成存档ToolStripMenuItem";
            this.生成存档ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.生成存档ToolStripMenuItem.Text = "生成存档";
            this.生成存档ToolStripMenuItem.Click += new System.EventHandler(this.生成存档ToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.LV_Countries);
            this.groupBox2.Location = new System.Drawing.Point(22, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 364);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "国家";
            // 
            // LV_Countries
            // 
            this.LV_Countries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_Countries.Location = new System.Drawing.Point(3, 17);
            this.LV_Countries.MultiSelect = false;
            this.LV_Countries.Name = "LV_Countries";
            this.LV_Countries.ShowItemToolTips = true;
            this.LV_Countries.Size = new System.Drawing.Size(159, 344);
            this.LV_Countries.TabIndex = 0;
            this.LV_Countries.UseCompatibleStateImageBehavior = false;
            this.LV_Countries.View = System.Windows.Forms.View.Tile;
            this.LV_Countries.Click += new System.EventHandler(this.LV_Countries_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(193, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(860, 364);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_RecoverAll);
            this.tabPage1.Controls.Add(this.btn_DeleteAll);
            this.tabPage1.Controls.Add(this.tv_EquipmentType);
            this.tabPage1.Controls.Add(this.gb_EquipmentType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(852, 338);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "后勤";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_RecoverAll
            // 
            this.btn_RecoverAll.Location = new System.Drawing.Point(286, 6);
            this.btn_RecoverAll.Name = "btn_RecoverAll";
            this.btn_RecoverAll.Size = new System.Drawing.Size(87, 23);
            this.btn_RecoverAll.TabIndex = 3;
            this.btn_RecoverAll.Text = "批量恢复缴获";
            this.btn_RecoverAll.UseVisualStyleBackColor = true;
            this.btn_RecoverAll.Click += new System.EventHandler(this.btn_RecoverAll_Click);
            // 
            // btn_DeleteAll
            // 
            this.btn_DeleteAll.Location = new System.Drawing.Point(194, 6);
            this.btn_DeleteAll.Name = "btn_DeleteAll";
            this.btn_DeleteAll.Size = new System.Drawing.Size(86, 23);
            this.btn_DeleteAll.TabIndex = 2;
            this.btn_DeleteAll.Text = "批量删除缴获";
            this.btn_DeleteAll.UseVisualStyleBackColor = true;
            this.btn_DeleteAll.Click += new System.EventHandler(this.btn_DeleteAll_Click);
            // 
            // tv_EquipmentType
            // 
            this.tv_EquipmentType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tv_EquipmentType.Location = new System.Drawing.Point(6, 3);
            this.tv_EquipmentType.Name = "tv_EquipmentType";
            this.tv_EquipmentType.Size = new System.Drawing.Size(176, 329);
            this.tv_EquipmentType.TabIndex = 0;
            this.tv_EquipmentType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_EquipmentType_AfterSelect);
            // 
            // gb_EquipmentType
            // 
            this.gb_EquipmentType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_EquipmentType.Controls.Add(this.dataGridView1);
            this.gb_EquipmentType.Location = new System.Drawing.Point(188, 35);
            this.gb_EquipmentType.Name = "gb_EquipmentType";
            this.gb_EquipmentType.Size = new System.Drawing.Size(658, 297);
            this.gb_EquipmentType.TabIndex = 0;
            this.gb_EquipmentType.TabStop = false;
            this.gb_EquipmentType.Text = "装备类型";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.TypeStr,
            this.Quantity,
            this.EName,
            this.CountryName,
            this.IsForigen,
            this.ob});
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(652, 277);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "Id";
            this.ID.HeaderText = "编号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // TypeStr
            // 
            this.TypeStr.DataPropertyName = "TypeStr";
            this.TypeStr.HeaderText = "代码";
            this.TypeStr.Name = "TypeStr";
            this.TypeStr.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "数量";
            this.Quantity.Name = "Quantity";
            // 
            // EName
            // 
            this.EName.DataPropertyName = "EquipmentName";
            this.EName.HeaderText = "名称";
            this.EName.Name = "EName";
            this.EName.ReadOnly = true;
            // 
            // CountryName
            // 
            this.CountryName.DataPropertyName = "CreatorName";
            this.CountryName.HeaderText = "所属国家";
            this.CountryName.Name = "CountryName";
            this.CountryName.ReadOnly = true;
            // 
            // IsForigen
            // 
            this.IsForigen.DataPropertyName = "IsForeignLease";
            this.IsForigen.HeaderText = "是否缴获";
            this.IsForigen.Name = "IsForigen";
            this.IsForigen.ReadOnly = true;
            // 
            // ob
            // 
            this.ob.DataPropertyName = "Obsolete";
            this.ob.FalseValue = "no";
            this.ob.HeaderText = "是否过时";
            this.ob.Name = "ob";
            this.ob.ReadOnly = true;
            this.ob.TrueValue = "yes";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(852, 338);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "陆军";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 404);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "SaveFileEditor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gb_EquipmentType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开存档ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView LV_Countries;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gb_EquipmentType;
        private System.Windows.Forms.TreeView tv_EquipmentType;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn EName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountryName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsForigen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ob;
        private System.Windows.Forms.Button btn_RecoverAll;
        private System.Windows.Forms.Button btn_DeleteAll;
        private System.Windows.Forms.ToolStripMenuItem 生成存档ToolStripMenuItem;
    }
}