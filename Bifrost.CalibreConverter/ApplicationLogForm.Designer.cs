namespace Bifrost.CalibreConverter
{
    partial class ApplicationLogForm
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
            this._buttonClose = new System.Windows.Forms.Button();
            this._tabControlLogContainer = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // _buttonClose
            // 
            this._buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonClose.Location = new System.Drawing.Point(691, 429);
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Size = new System.Drawing.Size(75, 23);
            this._buttonClose.TabIndex = 1;
            this._buttonClose.Text = "Close";
            this._buttonClose.UseVisualStyleBackColor = true;
            this._buttonClose.Click += new System.EventHandler(this.OnButtonCloseClick);
            // 
            // _tabControlLogContainer
            // 
            this._tabControlLogContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tabControlLogContainer.Location = new System.Drawing.Point(12, 12);
            this._tabControlLogContainer.Name = "_tabControlLogContainer";
            this._tabControlLogContainer.SelectedIndex = 0;
            this._tabControlLogContainer.Size = new System.Drawing.Size(754, 411);
            this._tabControlLogContainer.TabIndex = 0;
            // 
            // ApplicationLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonClose;
            this.ClientSize = new System.Drawing.Size(778, 464);
            this.Controls.Add(this._tabControlLogContainer);
            this.Controls.Add(this._buttonClose);
            this.Name = "ApplicationLogForm";
            this.Text = "ApplicationLogForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _buttonClose;
        private System.Windows.Forms.TabControl _tabControlLogContainer;
    }
}