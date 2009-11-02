namespace sTDT
{
    partial class sTDTMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sTDTMainForm));
            this.stdtmainmenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordIdleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewIdleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.onlineAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offlineAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axServerSelect1 = new AxTTankInterfaces.AxServerSelect();
            this.axTankSelect1 = new AxTTankInterfaces.AxTankSelect();
            this.axBlockSelect1 = new AxTTankInterfaces.AxBlockSelect();
            this.axEventSelect1 = new AxTTankInterfaces.AxEventSelect();
            this.on = new System.Windows.Forms.Button();
            this.off = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.Button();
            this.record = new System.Windows.Forms.Button();
            this.stdtmainmenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axServerSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTankSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axBlockSelect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axEventSelect1)).BeginInit();
            this.SuspendLayout();
            // 
            // stdtmainmenu
            // 
            this.stdtmainmenu.BackColor = System.Drawing.Color.OrangeRed;
            this.stdtmainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.controlToolStripMenuItem});
            this.stdtmainmenu.Location = new System.Drawing.Point(0, 0);
            this.stdtmainmenu.Name = "stdtmainmenu";
            this.stdtmainmenu.Size = new System.Drawing.Size(715, 24);
            this.stdtmainmenu.TabIndex = 0;
            this.stdtmainmenu.Text = "stdtmainmenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordIdleToolStripMenuItem,
            this.previewIdleToolStripMenuItem,
            this.toolStripSeparator2,
            this.onlineAnalysisToolStripMenuItem,
            this.offlineAnalysisToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // recordIdleToolStripMenuItem
            // 
            this.recordIdleToolStripMenuItem.Name = "recordIdleToolStripMenuItem";
            this.recordIdleToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.recordIdleToolStripMenuItem.Text = "Record/Idle";
            this.recordIdleToolStripMenuItem.Click += new System.EventHandler(this.recordIdleToolStripMenuItem_Click);
            // 
            // previewIdleToolStripMenuItem
            // 
            this.previewIdleToolStripMenuItem.Name = "previewIdleToolStripMenuItem";
            this.previewIdleToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.previewIdleToolStripMenuItem.Text = "Preview/Idle";
            this.previewIdleToolStripMenuItem.Click += new System.EventHandler(this.previewIdleToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // onlineAnalysisToolStripMenuItem
            // 
            this.onlineAnalysisToolStripMenuItem.Name = "onlineAnalysisToolStripMenuItem";
            this.onlineAnalysisToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.onlineAnalysisToolStripMenuItem.Text = "OnlineAnalysis";
            this.onlineAnalysisToolStripMenuItem.Click += new System.EventHandler(this.onlineAnalysisToolStripMenuItem_Click);
            // 
            // offlineAnalysisToolStripMenuItem
            // 
            this.offlineAnalysisToolStripMenuItem.Name = "offlineAnalysisToolStripMenuItem";
            this.offlineAnalysisToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.offlineAnalysisToolStripMenuItem.Text = "OfflineAnalysis";
            this.offlineAnalysisToolStripMenuItem.Click += new System.EventHandler(this.offlineAnalysisToolStripMenuItem_Click);
            // 
            // axServerSelect1
            // 
            this.axServerSelect1.Enabled = true;
            this.axServerSelect1.Location = new System.Drawing.Point(-1, 27);
            this.axServerSelect1.Name = "axServerSelect1";
            this.axServerSelect1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axServerSelect1.OcxState")));
            this.axServerSelect1.Size = new System.Drawing.Size(120, 214);
            this.axServerSelect1.TabIndex = 1;
            this.axServerSelect1.ServerChanged += new AxTTankInterfaces.@__ServerSelect_ServerChangedEventHandler(this.axServerSelect1_ServerChanged);
            // 
            // axTankSelect1
            // 
            this.axTankSelect1.Enabled = true;
            this.axTankSelect1.Location = new System.Drawing.Point(125, 27);
            this.axTankSelect1.Name = "axTankSelect1";
            this.axTankSelect1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTankSelect1.OcxState")));
            this.axTankSelect1.Size = new System.Drawing.Size(262, 214);
            this.axTankSelect1.TabIndex = 2;
            this.axTankSelect1.TankChanged += new AxTTankInterfaces.@__TankSelect_TankChangedEventHandler(this.axTankSelect1_TankChanged);
            // 
            // axBlockSelect1
            // 
            this.axBlockSelect1.Enabled = true;
            this.axBlockSelect1.Location = new System.Drawing.Point(-1, 247);
            this.axBlockSelect1.Name = "axBlockSelect1";
            this.axBlockSelect1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axBlockSelect1.OcxState")));
            this.axBlockSelect1.Size = new System.Drawing.Size(388, 195);
            this.axBlockSelect1.TabIndex = 3;
            this.axBlockSelect1.BlockChanged += new AxTTankInterfaces.@__BlockSelect_BlockChangedEventHandler(this.axBlockSelect1_BlockChanged);
            // 
            // axEventSelect1
            // 
            this.axEventSelect1.Enabled = true;
            this.axEventSelect1.Location = new System.Drawing.Point(-1, 448);
            this.axEventSelect1.Name = "axEventSelect1";
            this.axEventSelect1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axEventSelect1.OcxState")));
            this.axEventSelect1.Size = new System.Drawing.Size(388, 131);
            this.axEventSelect1.TabIndex = 4;
            this.axEventSelect1.ActEventChanged += new AxTTankInterfaces.@__EventSelect_ActEventChangedEventHandler(this.axEventSelect1_ActEventChanged);
            // 
            // on
            // 
            this.on.BackColor = System.Drawing.Color.LightGray;
            this.on.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.on.ForeColor = System.Drawing.Color.Blue;
            this.on.Location = new System.Drawing.Point(609, 532);
            this.on.Name = "on";
            this.on.Size = new System.Drawing.Size(82, 35);
            this.on.TabIndex = 14;
            this.on.Text = "On_Line";
            this.on.UseVisualStyleBackColor = false;
            this.on.Click += new System.EventHandler(this.on_Click);
            // 
            // off
            // 
            this.off.BackColor = System.Drawing.Color.LightGray;
            this.off.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.off.ForeColor = System.Drawing.Color.Red;
            this.off.Location = new System.Drawing.Point(609, 491);
            this.off.Name = "off";
            this.off.Size = new System.Drawing.Size(82, 35);
            this.off.TabIndex = 13;
            this.off.Text = "Off_Line";
            this.off.UseVisualStyleBackColor = false;
            this.off.Click += new System.EventHandler(this.off_Click);
            // 
            // preview
            // 
            this.preview.BackColor = System.Drawing.Color.LightGray;
            this.preview.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preview.ForeColor = System.Drawing.Color.Blue;
            this.preview.Location = new System.Drawing.Point(420, 532);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(82, 35);
            this.preview.TabIndex = 12;
            this.preview.Text = "Preview";
            this.preview.UseVisualStyleBackColor = false;
            this.preview.Click += new System.EventHandler(this.preview_Click);
            // 
            // record
            // 
            this.record.BackColor = System.Drawing.Color.LightGray;
            this.record.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.record.ForeColor = System.Drawing.Color.Red;
            this.record.Location = new System.Drawing.Point(420, 491);
            this.record.Name = "record";
            this.record.Size = new System.Drawing.Size(82, 35);
            this.record.TabIndex = 11;
            this.record.Text = "Record";
            this.record.UseVisualStyleBackColor = false;
            this.record.Click += new System.EventHandler(this.record_Click);
            // 
            // sTDTMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 581);
            this.Controls.Add(this.on);
            this.Controls.Add(this.off);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.record);
            this.Controls.Add(this.axEventSelect1);
            this.Controls.Add(this.axBlockSelect1);
            this.Controls.Add(this.axTankSelect1);
            this.Controls.Add(this.axServerSelect1);
            this.Controls.Add(this.stdtmainmenu);
            this.MainMenuStrip = this.stdtmainmenu;
            this.Name = "sTDTMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDT Signal Client";
            this.stdtmainmenu.ResumeLayout(false);
            this.stdtmainmenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axServerSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTankSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axBlockSelect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axEventSelect1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip stdtmainmenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private AxTTankInterfaces.AxServerSelect axServerSelect1;
        private AxTTankInterfaces.AxTankSelect axTankSelect1;
        private AxTTankInterfaces.AxBlockSelect axBlockSelect1;
        private AxTTankInterfaces.AxEventSelect axEventSelect1;
        private System.Windows.Forms.Button on;
        private System.Windows.Forms.Button off;
        private System.Windows.Forms.Button preview;
        private System.Windows.Forms.Button record;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordIdleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewIdleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem onlineAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offlineAnalysisToolStripMenuItem;
    }
}

