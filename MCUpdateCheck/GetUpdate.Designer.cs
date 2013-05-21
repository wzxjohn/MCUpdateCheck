namespace MCUpdateCheck
{
    partial class GetUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetUpdate));
            this.downloadingLable = new System.Windows.Forms.Label();
            this.downloadingProcess = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // downloadingLable
            // 
            this.downloadingLable.AutoSize = true;
            this.downloadingLable.Location = new System.Drawing.Point(12, 9);
            this.downloadingLable.Name = "downloadingLable";
            this.downloadingLable.Size = new System.Drawing.Size(41, 12);
            this.downloadingLable.TabIndex = 0;
            this.downloadingLable.Text = "label1";
            // 
            // downloadingProcess
            // 
            this.downloadingProcess.Location = new System.Drawing.Point(14, 25);
            this.downloadingProcess.Name = "downloadingProcess";
            this.downloadingProcess.Size = new System.Drawing.Size(225, 23);
            this.downloadingProcess.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(164, 54);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // GetUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 86);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.downloadingProcess);
            this.Controls.Add(this.downloadingLable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GetUpdate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetUpdate_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label downloadingLable;
        private System.Windows.Forms.ProgressBar downloadingProcess;
        private System.Windows.Forms.Button cancelButton;
    }
}