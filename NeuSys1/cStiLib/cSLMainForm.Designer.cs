using System;

namespace cStiLib
{
    partial class cSLMainForm
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
            if (proxyExC != null)
            {
                try
                {
                    proxyExC.Subscribe(false);
                    proxyExC.Close();
                }
                catch (Exception)
                {
                }
                proxyExC = null;
            }
            if (proxyExS != null)
            {
                try
                {
                    proxyExS.Subscribe(false);
                    proxyExS.Close();
                }
                catch (Exception)
                {
                }
                proxyExS = null;
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
            this.cslmainmenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchOnlineServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csltab = new System.Windows.Forms.TabControl();
            this.experiment = new System.Windows.Forms.TabPage();
            this.exsstate = new System.Windows.Forms.Label();
            this.excstate = new System.Windows.Forms.Label();
            this.exdataGrid = new System.Windows.Forms.DataGridView();
            this.info = new System.Windows.Forms.Label();
            this.refreshbutton = new System.Windows.Forms.Button();
            this.stopbutton = new System.Windows.Forms.Button();
            this.runbutton = new System.Windows.Forms.Button();
            this.terminatebutton = new System.Windows.Forms.Button();
            this.invokebutton = new System.Windows.Forms.Button();
            this.experimentlist = new System.Windows.Forms.ComboBox();
            this.scripting = new System.Windows.Forms.TabPage();
            this.cslmainmenu.SuspendLayout();
            this.csltab.SuspendLayout();
            this.experiment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exdataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // cslmainmenu
            // 
            this.cslmainmenu.BackColor = System.Drawing.Color.DimGray;
            this.cslmainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.controlToolStripMenuItem});
            this.cslmainmenu.Location = new System.Drawing.Point(0, 0);
            this.cslmainmenu.Name = "cslmainmenu";
            this.cslmainmenu.Size = new System.Drawing.Size(620, 24);
            this.cslmainmenu.TabIndex = 0;
            this.cslmainmenu.Text = "cslmainmenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
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
            this.searchOnlineServiceToolStripMenuItem});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // searchOnlineServiceToolStripMenuItem
            // 
            this.searchOnlineServiceToolStripMenuItem.Name = "searchOnlineServiceToolStripMenuItem";
            this.searchOnlineServiceToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.searchOnlineServiceToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.searchOnlineServiceToolStripMenuItem.Text = "Search Online Service";
            this.searchOnlineServiceToolStripMenuItem.Click += new System.EventHandler(this.searchOnlineServiceToolStripMenuItem_Click);
            // 
            // csltab
            // 
            this.csltab.Controls.Add(this.experiment);
            this.csltab.Controls.Add(this.scripting);
            this.csltab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.csltab.Location = new System.Drawing.Point(0, 24);
            this.csltab.Name = "csltab";
            this.csltab.SelectedIndex = 0;
            this.csltab.Size = new System.Drawing.Size(620, 395);
            this.csltab.TabIndex = 1;
            // 
            // experiment
            // 
            this.experiment.Controls.Add(this.exsstate);
            this.experiment.Controls.Add(this.excstate);
            this.experiment.Controls.Add(this.exdataGrid);
            this.experiment.Controls.Add(this.info);
            this.experiment.Controls.Add(this.refreshbutton);
            this.experiment.Controls.Add(this.stopbutton);
            this.experiment.Controls.Add(this.runbutton);
            this.experiment.Controls.Add(this.terminatebutton);
            this.experiment.Controls.Add(this.invokebutton);
            this.experiment.Controls.Add(this.experimentlist);
            this.experiment.Location = new System.Drawing.Point(4, 22);
            this.experiment.Name = "experiment";
            this.experiment.Padding = new System.Windows.Forms.Padding(3);
            this.experiment.Size = new System.Drawing.Size(612, 369);
            this.experiment.TabIndex = 0;
            this.experiment.Text = "Experiment";
            this.experiment.UseVisualStyleBackColor = true;
            // 
            // exsstate
            // 
            this.exsstate.BackColor = System.Drawing.Color.LightYellow;
            this.exsstate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.exsstate.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exsstate.ForeColor = System.Drawing.Color.Red;
            this.exsstate.Location = new System.Drawing.Point(147, 13);
            this.exsstate.Name = "exsstate";
            this.exsstate.Size = new System.Drawing.Size(118, 20);
            this.exsstate.TabIndex = 9;
            this.exsstate.Text = "ExService Offline";
            // 
            // excstate
            // 
            this.excstate.BackColor = System.Drawing.Color.LightYellow;
            this.excstate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.excstate.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.excstate.ForeColor = System.Drawing.Color.Red;
            this.excstate.Location = new System.Drawing.Point(11, 13);
            this.excstate.Name = "excstate";
            this.excstate.Size = new System.Drawing.Size(123, 20);
            this.excstate.TabIndex = 8;
            this.excstate.Text = "ExConsole Offline";
            // 
            // exdataGrid
            // 
            this.exdataGrid.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.exdataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.exdataGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.exdataGrid.Location = new System.Drawing.Point(278, 3);
            this.exdataGrid.Name = "exdataGrid";
            this.exdataGrid.RowHeadersWidth = 18;
            this.exdataGrid.Size = new System.Drawing.Size(331, 363);
            this.exdataGrid.TabIndex = 7;
            // 
            // info
            // 
            this.info.AutoEllipsis = true;
            this.info.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info.ForeColor = System.Drawing.Color.Navy;
            this.info.Location = new System.Drawing.Point(7, 217);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(264, 147);
            this.info.TabIndex = 6;
            this.info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // refreshbutton
            // 
            this.refreshbutton.Location = new System.Drawing.Point(97, 181);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(69, 23);
            this.refreshbutton.TabIndex = 5;
            this.refreshbutton.Text = "Refresh";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // stopbutton
            // 
            this.stopbutton.Location = new System.Drawing.Point(151, 135);
            this.stopbutton.Name = "stopbutton";
            this.stopbutton.Size = new System.Drawing.Size(69, 23);
            this.stopbutton.TabIndex = 4;
            this.stopbutton.Text = "Stop";
            this.stopbutton.UseVisualStyleBackColor = true;
            this.stopbutton.Click += new System.EventHandler(this.stopbutton_Click);
            // 
            // runbutton
            // 
            this.runbutton.Location = new System.Drawing.Point(57, 135);
            this.runbutton.Name = "runbutton";
            this.runbutton.Size = new System.Drawing.Size(69, 23);
            this.runbutton.TabIndex = 3;
            this.runbutton.Text = "Run";
            this.runbutton.UseVisualStyleBackColor = true;
            this.runbutton.Click += new System.EventHandler(this.runbutton_Click);
            // 
            // terminatebutton
            // 
            this.terminatebutton.Location = new System.Drawing.Point(151, 83);
            this.terminatebutton.Name = "terminatebutton";
            this.terminatebutton.Size = new System.Drawing.Size(69, 23);
            this.terminatebutton.TabIndex = 2;
            this.terminatebutton.Text = "Terminate";
            this.terminatebutton.UseVisualStyleBackColor = true;
            this.terminatebutton.Click += new System.EventHandler(this.terminatebutton_Click);
            // 
            // invokebutton
            // 
            this.invokebutton.Location = new System.Drawing.Point(57, 83);
            this.invokebutton.Name = "invokebutton";
            this.invokebutton.Size = new System.Drawing.Size(69, 23);
            this.invokebutton.TabIndex = 1;
            this.invokebutton.Text = "Invoke";
            this.invokebutton.UseVisualStyleBackColor = true;
            this.invokebutton.Click += new System.EventHandler(this.invokebutton_Click);
            // 
            // experimentlist
            // 
            this.experimentlist.AllowDrop = true;
            this.experimentlist.DropDownHeight = 200;
            this.experimentlist.FormattingEnabled = true;
            this.experimentlist.IntegralHeight = false;
            this.experimentlist.Location = new System.Drawing.Point(66, 44);
            this.experimentlist.Name = "experimentlist";
            this.experimentlist.Size = new System.Drawing.Size(150, 21);
            this.experimentlist.TabIndex = 0;
            this.experimentlist.Text = "Select Experiment";
            // 
            // scripting
            // 
            this.scripting.Location = new System.Drawing.Point(4, 22);
            this.scripting.Name = "scripting";
            this.scripting.Padding = new System.Windows.Forms.Padding(3);
            this.scripting.Size = new System.Drawing.Size(612, 369);
            this.scripting.TabIndex = 1;
            this.scripting.Text = "Scripting";
            this.scripting.UseVisualStyleBackColor = true;
            // 
            // cSLMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 419);
            this.Controls.Add(this.csltab);
            this.Controls.Add(this.cslmainmenu);
            this.MainMenuStrip = this.cslmainmenu;
            this.Name = "cSLMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StiLib Control Client";
            this.cslmainmenu.ResumeLayout(false);
            this.cslmainmenu.PerformLayout();
            this.csltab.ResumeLayout(false);
            this.experiment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exdataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip cslmainmenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TabControl csltab;
        private System.Windows.Forms.TabPage experiment;
        private System.Windows.Forms.TabPage scripting;
        private System.Windows.Forms.ComboBox experimentlist;
        private System.Windows.Forms.Button refreshbutton;
        private System.Windows.Forms.Button stopbutton;
        private System.Windows.Forms.Button runbutton;
        private System.Windows.Forms.Button terminatebutton;
        private System.Windows.Forms.Button invokebutton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.Label exsstate;
        private System.Windows.Forms.Label excstate;
        private System.Windows.Forms.DataGridView exdataGrid;
        private System.Windows.Forms.ToolStripMenuItem searchOnlineServiceToolStripMenuItem;
    }
}

