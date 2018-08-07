namespace WinCacheGrind2
{
    partial class FDoc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDoc));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv = new System.Windows.Forms.TreeView();
            this.imageListtv = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tsLBL = new System.Windows.Forms.TabPage();
            this.lvLBL = new System.Windows.Forms.ListView();
            this.Function = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Self = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cumulative = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.File = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CalledFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsMerged = new System.Windows.Forms.TabPage();
            this.lvMergedInstances = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.lMerged = new System.Windows.Forms.Label();
            this.lvMerged = new System.Windows.Forms.ListView();
            this.Function1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AvgSelf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AvgCum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalSelf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalCum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Calls = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.iInfo = new System.Windows.Forms.PictureBox();
            this.lInfoFileName = new System.Windows.Forms.Label();
            this.lInfoName = new System.Windows.Forms.Label();
            this.lInfo = new System.Windows.Forms.Label();
            this.tb = new System.Windows.Forms.ToolStrip();
            this.aViewPercent = new System.Windows.Forms.ToolStripButton();
            this.aViewMS = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aViewFullPath = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.imageListtb = new System.Windows.Forms.ImageList(this.components);
            this.aViewHideFastFuncs = new System.Windows.Forms.ToolStripButton();
            this.aViewHideLibFuncs = new System.Windows.Forms.ToolStripButton();
            this.aViewGoToUpOneLevel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aTreeGoToRoot = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tsLBL.SuspendLayout();
            this.tsMerged.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iInfo)).BeginInit();
            this.tb.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 21);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(717, 424);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // tv
            // 
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.imageListtv;
            this.tv.Location = new System.Drawing.Point(3, 3);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(203, 421);
            this.tv.TabIndex = 1;
            // 
            // imageListtv
            // 
            this.imageListtv.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListtv.ImageStream")));
            this.imageListtv.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListtv.Images.SetKeyName(0, "UnknownProject_16x.png");
            this.imageListtv.Images.SetKeyName(1, "CSSourceFile_16x.png");
            this.imageListtv.Images.SetKeyName(2, "CurrentInstructionPointer_16x.png");
            this.imageListtv.Images.SetKeyName(3, "Method_Cube_16x.png");
            this.imageListtv.Images.SetKeyName(4, "WPFPageFunction_16x.png");
            this.imageListtv.Images.SetKeyName(5, "MethodPrivate_16x.png");
            this.imageListtv.Images.SetKeyName(6, "MethodSealed_16x.png");
            this.imageListtv.Images.SetKeyName(7, "MethodMoved_16x.png");
            this.imageListtv.Images.SetKeyName(8, "MethodProtect_16x.png");
            this.imageListtv.Images.SetKeyName(9, "NewPHPFileHere_16x.png");
            this.imageListtv.Images.SetKeyName(10, "ExtensionMethod_16x.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tsLBL);
            this.tabControl1.Controls.Add(this.tsMerged);
            this.tabControl1.Location = new System.Drawing.Point(1, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(500, 355);
            this.tabControl1.TabIndex = 0;
            // 
            // tsLBL
            // 
            this.tsLBL.Controls.Add(this.lvLBL);
            this.tsLBL.Location = new System.Drawing.Point(4, 22);
            this.tsLBL.Name = "tsLBL";
            this.tsLBL.Padding = new System.Windows.Forms.Padding(3);
            this.tsLBL.Size = new System.Drawing.Size(492, 329);
            this.tsLBL.TabIndex = 0;
            this.tsLBL.Text = "Line by Line";
            this.tsLBL.UseVisualStyleBackColor = true;
            // 
            // lvLBL
            // 
            this.lvLBL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLBL.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Function,
            this.Self,
            this.Cumulative,
            this.File,
            this.CalledFrom});
            this.lvLBL.LargeImageList = this.imageListtv;
            this.lvLBL.Location = new System.Drawing.Point(0, 0);
            this.lvLBL.MultiSelect = false;
            this.lvLBL.Name = "lvLBL";
            this.lvLBL.Size = new System.Drawing.Size(492, 277);
            this.lvLBL.TabIndex = 0;
            this.lvLBL.UseCompatibleStateImageBehavior = false;
            this.lvLBL.View = System.Windows.Forms.View.Details;
            // 
            // Function
            // 
            this.Function.Text = "Function";
            this.Function.Width = 180;
            // 
            // Self
            // 
            this.Self.Text = "Self";
            this.Self.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Cumulative
            // 
            this.Cumulative.Text = "Cum.";
            this.Cumulative.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // File
            // 
            this.File.Text = "File";
            this.File.Width = 120;
            // 
            // CalledFrom
            // 
            this.CalledFrom.Text = "Called From";
            this.CalledFrom.Width = 200;
            // 
            // tsMerged
            // 
            this.tsMerged.Controls.Add(this.lvMergedInstances);
            this.tsMerged.Controls.Add(this.panel3);
            this.tsMerged.Controls.Add(this.lvMerged);
            this.tsMerged.Location = new System.Drawing.Point(4, 22);
            this.tsMerged.Name = "tsMerged";
            this.tsMerged.Padding = new System.Windows.Forms.Padding(3);
            this.tsMerged.Size = new System.Drawing.Size(492, 329);
            this.tsMerged.TabIndex = 1;
            this.tsMerged.Text = "Overall";
            this.tsMerged.UseVisualStyleBackColor = true;
            // 
            // lvMergedInstances
            // 
            this.lvMergedInstances.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMergedInstances.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lvMergedInstances.Location = new System.Drawing.Point(0, 211);
            this.lvMergedInstances.Name = "lvMergedInstances";
            this.lvMergedInstances.Size = new System.Drawing.Size(492, 124);
            this.lvMergedInstances.TabIndex = 2;
            this.lvMergedInstances.UseCompatibleStateImageBehavior = false;
            this.lvMergedInstances.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Num.";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Self";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Cum.";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Called By";
            this.columnHeader4.Width = 180;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Called From";
            this.columnHeader5.Width = 200;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Stack trace";
            this.columnHeader6.Width = 2000;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lMerged);
            this.panel3.Location = new System.Drawing.Point(0, 195);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(500, 16);
            this.panel3.TabIndex = 1;
            // 
            // lMerged
            // 
            this.lMerged.AutoSize = true;
            this.lMerged.Location = new System.Drawing.Point(4, 2);
            this.lMerged.Name = "lMerged";
            this.lMerged.Size = new System.Drawing.Size(0, 13);
            this.lMerged.TabIndex = 0;
            // 
            // lvMerged
            // 
            this.lvMerged.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMerged.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Function1,
            this.AvgSelf,
            this.AvgCum,
            this.TotalSelf,
            this.TotalCum,
            this.Calls});
            this.lvMerged.LargeImageList = this.imageListtv;
            this.lvMerged.Location = new System.Drawing.Point(0, 0);
            this.lvMerged.Name = "lvMerged";
            this.lvMerged.Size = new System.Drawing.Size(492, 198);
            this.lvMerged.TabIndex = 0;
            this.lvMerged.UseCompatibleStateImageBehavior = false;
            this.lvMerged.View = System.Windows.Forms.View.Details;
            // 
            // Function1
            // 
            this.Function1.Text = "Function";
            this.Function1.Width = 186;
            // 
            // AvgSelf
            // 
            this.AvgSelf.Text = "Avg. Self";
            // 
            // AvgCum
            // 
            this.AvgCum.Text = "Avg. Cum.";
            // 
            // TotalSelf
            // 
            this.TotalSelf.Text = "Total Self";
            // 
            // TotalCum
            // 
            this.TotalCum.Text = "Total Cum.";
            this.TotalCum.Width = 68;
            // 
            // Calls
            // 
            this.Calls.Text = "Calls";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(2, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(498, 357);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.iInfo);
            this.panel2.Controls.Add(this.lInfoFileName);
            this.panel2.Controls.Add(this.lInfoName);
            this.panel2.Controls.Add(this.lInfo);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(498, 53);
            this.panel2.TabIndex = 3;
            // 
            // iInfo
            // 
            this.iInfo.Image = ((System.Drawing.Image)(resources.GetObject("iInfo.Image")));
            this.iInfo.Location = new System.Drawing.Point(2, 0);
            this.iInfo.Name = "iInfo";
            this.iInfo.Size = new System.Drawing.Size(16, 17);
            this.iInfo.TabIndex = 3;
            this.iInfo.TabStop = false;
            // 
            // lInfoFileName
            // 
            this.lInfoFileName.AutoSize = true;
            this.lInfoFileName.Location = new System.Drawing.Point(4, 20);
            this.lInfoFileName.Name = "lInfoFileName";
            this.lInfoFileName.Size = new System.Drawing.Size(0, 13);
            this.lInfoFileName.TabIndex = 2;
            // 
            // lInfoName
            // 
            this.lInfoName.AutoSize = true;
            this.lInfoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lInfoName.Location = new System.Drawing.Point(24, 4);
            this.lInfoName.Name = "lInfoName";
            this.lInfoName.Size = new System.Drawing.Size(0, 13);
            this.lInfoName.TabIndex = 1;
            // 
            // lInfo
            // 
            this.lInfo.AutoSize = true;
            this.lInfo.Location = new System.Drawing.Point(4, 36);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(0, 13);
            this.lInfo.TabIndex = 0;
            // 
            // tb
            // 
            this.tb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aViewGoToUpOneLevel,
            this.aTreeGoToRoot,
            this.toolStripSeparator3,
            this.aViewPercent,
            this.aViewMS,
            this.toolStripSeparator1,
            this.aViewFullPath,
            this.toolStripSeparator2,
            this.aViewHideFastFuncs,
            this.aViewHideLibFuncs});
            this.tb.Location = new System.Drawing.Point(0, 0);
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(719, 25);
            this.tb.TabIndex = 1;
            this.tb.Text = "toolStrip1";
            // 
            // aViewPercent
            // 
            this.aViewPercent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aViewPercent.Image = ((System.Drawing.Image)(resources.GetObject("aViewPercent.Image")));
            this.aViewPercent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aViewPercent.Name = "aViewPercent";
            this.aViewPercent.Size = new System.Drawing.Size(23, 22);
            this.aViewPercent.Text = "toolStripButton1";
            this.aViewPercent.ToolTipText = "Percentages";
            this.aViewPercent.Click += new System.EventHandler(this.aViewPercent_Click);
            // 
            // aViewMS
            // 
            this.aViewMS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aViewMS.Image = ((System.Drawing.Image)(resources.GetObject("aViewMS.Image")));
            this.aViewMS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aViewMS.Name = "aViewMS";
            this.aViewMS.Size = new System.Drawing.Size(23, 22);
            this.aViewMS.Text = "toolStripButton2";
            this.aViewMS.ToolTipText = "Milliseconds";
            this.aViewMS.Click += new System.EventHandler(this.aViewMS_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // aViewFullPath
            // 
            this.aViewFullPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aViewFullPath.Image = ((System.Drawing.Image)(resources.GetObject("aViewFullPath.Image")));
            this.aViewFullPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aViewFullPath.Name = "aViewFullPath";
            this.aViewFullPath.Size = new System.Drawing.Size(23, 22);
            this.aViewFullPath.Text = "Show Full Path";
            this.aViewFullPath.Click += new System.EventHandler(this.aViewFullPath_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // imageListtb
            // 
            this.imageListtb.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListtb.ImageStream")));
            this.imageListtb.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListtb.Images.SetKeyName(0, "Time_16x.png");
            this.imageListtb.Images.SetKeyName(1, "Time_16x_32.bmp");
            this.imageListtb.Images.SetKeyName(2, "Percentage_16x.png");
            this.imageListtb.Images.SetKeyName(3, "Percentage_16x_32.bmp");
            this.imageListtb.Images.SetKeyName(4, "NavigationPath_16x.png");
            this.imageListtb.Images.SetKeyName(5, "NavigationPath_16x_32.bmp");
            this.imageListtb.Images.SetKeyName(6, "HiddenFolder_16x.png");
            this.imageListtb.Images.SetKeyName(7, "HiddenFolder_16x_32.bmp");
            this.imageListtb.Images.SetKeyName(8, "MethodPrivate_16x.png");
            this.imageListtb.Images.SetKeyName(9, "MethodPrivate_16x_32.bmp");
            // 
            // aViewHideFastFuncs
            // 
            this.aViewHideFastFuncs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aViewHideFastFuncs.Image = ((System.Drawing.Image)(resources.GetObject("aViewHideFastFuncs.Image")));
            this.aViewHideFastFuncs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aViewHideFastFuncs.Name = "aViewHideFastFuncs";
            this.aViewHideFastFuncs.Size = new System.Drawing.Size(23, 22);
            this.aViewHideFastFuncs.Text = "Hide Fast Functions";
            this.aViewHideFastFuncs.Click += new System.EventHandler(this.aViewHideFastFuncs_Click);
            // 
            // aViewHideLibFuncs
            // 
            this.aViewHideLibFuncs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aViewHideLibFuncs.Image = ((System.Drawing.Image)(resources.GetObject("aViewHideLibFuncs.Image")));
            this.aViewHideLibFuncs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aViewHideLibFuncs.Name = "aViewHideLibFuncs";
            this.aViewHideLibFuncs.Size = new System.Drawing.Size(23, 22);
            this.aViewHideLibFuncs.Text = "Hide Library Functions";
            this.aViewHideLibFuncs.Click += new System.EventHandler(this.aViewHideLibFuncs_Click);
            // 
            // aViewGoToUpOneLevel
            // 
            this.aViewGoToUpOneLevel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aViewGoToUpOneLevel.Image = ((System.Drawing.Image)(resources.GetObject("aViewGoToUpOneLevel.Image")));
            this.aViewGoToUpOneLevel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aViewGoToUpOneLevel.Name = "aViewGoToUpOneLevel";
            this.aViewGoToUpOneLevel.Size = new System.Drawing.Size(23, 22);
            this.aViewGoToUpOneLevel.Text = "Up One Level";
            this.aViewGoToUpOneLevel.Click += new System.EventHandler(this.aViewGoToUpOneLevel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // aTreeGoToRoot
            // 
            this.aTreeGoToRoot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aTreeGoToRoot.Image = ((System.Drawing.Image)(resources.GetObject("aTreeGoToRoot.Image")));
            this.aTreeGoToRoot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aTreeGoToRoot.Name = "aTreeGoToRoot";
            this.aTreeGoToRoot.Size = new System.Drawing.Size(23, 22);
            this.aTreeGoToRoot.Text = "Go To Root";
            this.aTreeGoToRoot.Click += new System.EventHandler(this.aTreeGoToRoot_Click);
            // 
            // FDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tb);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FDoc";
            this.Size = new System.Drawing.Size(719, 474);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tsLBL.ResumeLayout(false);
            this.tsMerged.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iInfo)).EndInit();
            this.tb.ResumeLayout(false);
            this.tb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lInfoFileName;
        private System.Windows.Forms.Label lInfoName;
        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tsLBL;
        private System.Windows.Forms.ListView lvLBL;
        private System.Windows.Forms.ColumnHeader Function;
        private System.Windows.Forms.ColumnHeader Self;
        private System.Windows.Forms.ColumnHeader Cumulative;
        private System.Windows.Forms.ColumnHeader File;
        private System.Windows.Forms.ColumnHeader CalledFrom;
        private System.Windows.Forms.TabPage tsMerged;
        private System.Windows.Forms.ListView lvMergedInstances;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lMerged;
        private System.Windows.Forms.ListView lvMerged;
        private System.Windows.Forms.ColumnHeader Function1;
        private System.Windows.Forms.ColumnHeader AvgSelf;
        private System.Windows.Forms.ColumnHeader AvgCum;
        private System.Windows.Forms.ColumnHeader TotalSelf;
        private System.Windows.Forms.ColumnHeader TotalCum;
        private System.Windows.Forms.ColumnHeader Calls;
        private System.Windows.Forms.ToolStrip tb;
        private System.Windows.Forms.ToolStripButton aViewPercent;
        private System.Windows.Forms.PictureBox iInfo;
        private System.Windows.Forms.ImageList imageListtv;
        private System.Windows.Forms.ToolStripButton aViewMS;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ImageList imageListtb;
        private System.Windows.Forms.ToolStripButton aViewFullPath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton aViewHideFastFuncs;
        private System.Windows.Forms.ToolStripButton aViewHideLibFuncs;
        private System.Windows.Forms.ToolStripButton aViewGoToUpOneLevel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton aTreeGoToRoot;
    }
}
