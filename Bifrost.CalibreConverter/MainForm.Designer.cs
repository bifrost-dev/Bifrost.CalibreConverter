namespace Bifrost.CalibreConverter
{
    partial class MainForm
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
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._listViewInputFiles = new System.Windows.Forms.ListView();
            this._textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this._buttonSelectOutputFolder = new System.Windows.Forms.Button();
            this._groupBoxInputFiles = new System.Windows.Forms.GroupBox();
            this._groupBoxOutputFolder = new System.Windows.Forms.GroupBox();
            this._checkBoxOutIsInSource = new System.Windows.Forms.CheckBox();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this._toolStripButtonRemove = new System.Windows.Forms.ToolStripButton();
            this._toolStripButtonRemoveFinished = new System.Windows.Forms.ToolStripButton();
            this._toolStripComboOutFormat = new System.Windows.Forms.ToolStripComboBox();
            this._toolStripButtonConvert = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._toolStripButtonLog = new System.Windows.Forms.ToolStripButton();
            this._groupBoxInputFiles.SuspendLayout();
            this._groupBoxOutputFolder.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _listViewInputFiles
            // 
            this._listViewInputFiles.AllowDrop = true;
            this._listViewInputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._listViewInputFiles.FullRowSelect = true;
            this._listViewInputFiles.HideSelection = false;
            this._listViewInputFiles.Location = new System.Drawing.Point(6, 19);
            this._listViewInputFiles.Name = "_listViewInputFiles";
            this._listViewInputFiles.Size = new System.Drawing.Size(438, 214);
            this._listViewInputFiles.TabIndex = 0;
            this._listViewInputFiles.UseCompatibleStateImageBehavior = false;
            this._listViewInputFiles.View = System.Windows.Forms.View.Details;
            this._listViewInputFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListViewInputFilesDragDrop);
            this._listViewInputFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListViewInputFilesDragEnter);
            this._listViewInputFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewInputFilesKeyDown);
            // 
            // _textBoxOutputFolder
            // 
            this._textBoxOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxOutputFolder.Location = new System.Drawing.Point(6, 54);
            this._textBoxOutputFolder.Name = "_textBoxOutputFolder";
            this._textBoxOutputFolder.Size = new System.Drawing.Size(402, 20);
            this._textBoxOutputFolder.TabIndex = 4;
            // 
            // _buttonSelectOutputFolder
            // 
            this._buttonSelectOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonSelectOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this._buttonSelectOutputFolder.Location = new System.Drawing.Point(414, 52);
            this._buttonSelectOutputFolder.Name = "_buttonSelectOutputFolder";
            this._buttonSelectOutputFolder.Size = new System.Drawing.Size(30, 23);
            this._buttonSelectOutputFolder.TabIndex = 5;
            this._buttonSelectOutputFolder.Text = "...";
            this._buttonSelectOutputFolder.UseVisualStyleBackColor = true;
            this._buttonSelectOutputFolder.Click += new System.EventHandler(this.OnButtonSelectOutputFolderClick);
            // 
            // _groupBoxInputFiles
            // 
            this._groupBoxInputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBoxInputFiles.Controls.Add(this._listViewInputFiles);
            this._groupBoxInputFiles.Location = new System.Drawing.Point(12, 28);
            this._groupBoxInputFiles.Name = "_groupBoxInputFiles";
            this._groupBoxInputFiles.Size = new System.Drawing.Size(450, 239);
            this._groupBoxInputFiles.TabIndex = 10;
            this._groupBoxInputFiles.TabStop = false;
            this._groupBoxInputFiles.Text = "Input Files:";
            // 
            // _groupBoxOutputFolder
            // 
            this._groupBoxOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBoxOutputFolder.Controls.Add(this._checkBoxOutIsInSource);
            this._groupBoxOutputFolder.Controls.Add(this._textBoxOutputFolder);
            this._groupBoxOutputFolder.Controls.Add(this._buttonSelectOutputFolder);
            this._groupBoxOutputFolder.Location = new System.Drawing.Point(12, 273);
            this._groupBoxOutputFolder.Name = "_groupBoxOutputFolder";
            this._groupBoxOutputFolder.Size = new System.Drawing.Size(450, 92);
            this._groupBoxOutputFolder.TabIndex = 11;
            this._groupBoxOutputFolder.TabStop = false;
            this._groupBoxOutputFolder.Text = "Output Folder:";
            // 
            // _checkBoxOutIsInSource
            // 
            this._checkBoxOutIsInSource.AutoSize = true;
            this._checkBoxOutIsInSource.Location = new System.Drawing.Point(6, 23);
            this._checkBoxOutIsInSource.Name = "_checkBoxOutIsInSource";
            this._checkBoxOutIsInSource.Size = new System.Drawing.Size(109, 17);
            this._checkBoxOutIsInSource.TabIndex = 3;
            this._checkBoxOutIsInSource.Text = "Same as input file";
            this._checkBoxOutIsInSource.UseVisualStyleBackColor = true;
            this._checkBoxOutIsInSource.CheckedChanged += new System.EventHandler(this.CheckBoxOutIsInSourceCheckedChanged);
            // 
            // _toolStrip
            // 
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripButtonAdd,
            this._toolStripButtonRemove,
            this._toolStripButtonRemoveFinished,
            this._toolStripComboOutFormat,
            this._toolStripButtonConvert,
            this.toolStripSeparator1,
            this._toolStripButtonLog});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(474, 25);
            this._toolStrip.TabIndex = 15;
            // 
            // _toolStripButtonAdd
            // 
            this._toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonAdd.Image")));
            this._toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonAdd.Name = "_toolStripButtonAdd";
            this._toolStripButtonAdd.Size = new System.Drawing.Size(46, 22);
            this._toolStripButtonAdd.Text = "Add";
            this._toolStripButtonAdd.ToolTipText = "Add Books";
            this._toolStripButtonAdd.Click += new System.EventHandler(this.OnToolStripButtonAddClick);
            // 
            // _toolStripButtonRemove
            // 
            this._toolStripButtonRemove.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonRemove.Image")));
            this._toolStripButtonRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonRemove.Name = "_toolStripButtonRemove";
            this._toolStripButtonRemove.Size = new System.Drawing.Size(66, 22);
            this._toolStripButtonRemove.Text = "Remove";
            this._toolStripButtonRemove.ToolTipText = "Remove Selected Books";
            this._toolStripButtonRemove.Click += new System.EventHandler(this.OnToolStripButtonRemoveClick);
            // 
            // _toolStripButtonRemoveFinished
            // 
            this._toolStripButtonRemoveFinished.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonRemoveFinished.Image")));
            this._toolStripButtonRemoveFinished.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonRemoveFinished.Name = "_toolStripButtonRemoveFinished";
            this._toolStripButtonRemoveFinished.Size = new System.Drawing.Size(120, 22);
            this._toolStripButtonRemoveFinished.Text = "Remove Converted";
            this._toolStripButtonRemoveFinished.ToolTipText = "Remove Converted Books From List";
            this._toolStripButtonRemoveFinished.Click += new System.EventHandler(this.OnToolStripButtonRemoveFinishedClick);
            // 
            // _toolStripComboOutFormat
            // 
            this._toolStripComboOutFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._toolStripComboOutFormat.DropDownWidth = 140;
            this._toolStripComboOutFormat.Name = "_toolStripComboOutFormat";
            this._toolStripComboOutFormat.Size = new System.Drawing.Size(75, 25);
            this._toolStripComboOutFormat.ToolTipText = "Output Format";
            // 
            // _toolStripButtonConvert
            // 
            this._toolStripButtonConvert.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonConvert.Image")));
            this._toolStripButtonConvert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonConvert.Name = "_toolStripButtonConvert";
            this._toolStripButtonConvert.Size = new System.Drawing.Size(66, 22);
            this._toolStripButtonConvert.Text = "Convert";
            this._toolStripButtonConvert.ToolTipText = "Convert Books";
            this._toolStripButtonConvert.Click += new System.EventHandler(this.OnToolStripButtonConvertClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _toolStripButtonLog
            // 
            this._toolStripButtonLog.Image = ((System.Drawing.Image)(resources.GetObject("_toolStripButtonLog.Image")));
            this._toolStripButtonLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._toolStripButtonLog.Name = "_toolStripButtonLog";
            this._toolStripButtonLog.Size = new System.Drawing.Size(44, 22);
            this._toolStripButtonLog.Text = "Log";
            this._toolStripButtonLog.ToolTipText = "Show Log Window";
            this._toolStripButtonLog.Click += new System.EventHandler(this.OnToolStripButtonLogClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 377);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._groupBoxOutputFolder);
            this.Controls.Add(this._groupBoxInputFiles);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Calibre Converter";
            this._groupBoxInputFiles.ResumeLayout(false);
            this._groupBoxOutputFolder.ResumeLayout(false);
            this._groupBoxOutputFolder.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView _listViewInputFiles;
        private System.Windows.Forms.TextBox _textBoxOutputFolder;
        private System.Windows.Forms.Button _buttonSelectOutputFolder;
        private System.Windows.Forms.GroupBox _groupBoxInputFiles;
        private System.Windows.Forms.GroupBox _groupBoxOutputFolder;
        private System.Windows.Forms.CheckBox _checkBoxOutIsInSource;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton _toolStripButtonRemove;
        private System.Windows.Forms.ToolStripButton _toolStripButtonRemoveFinished;
        private System.Windows.Forms.ToolStripComboBox _toolStripComboOutFormat;
        private System.Windows.Forms.ToolStripButton _toolStripButtonConvert;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _toolStripButtonLog;
    }
}

